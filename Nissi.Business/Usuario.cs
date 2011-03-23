using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class Usuario
    {
        /// <summary>
        /// Validar conexão do Usuário
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <param name="identUsuario"></param>
        /// <returns></returns>
        public static string Autenticar(string login, string senha, out UsuarioVO identUsuario)
        {
            identUsuario = new UsuarioVO {Funcionario = new FuncionarioVO(){Login = login}};

            //Retornar dados do Usuário
            identUsuario.Funcionario = new FuncionarioData().Lista(identUsuario.Funcionario);
            string mensagem = string.Empty;

            if (identUsuario.Funcionario.CodFuncionario > 0)
            {
                if (identUsuario.Funcionario.Senha == null)
                {                    
                    mensagem = "primeiroacesso";
                }
                else
                {
                    #region Verificar Senha informada
                    byte[] senhaAplicacao = CriptografarSenha(senha);

                    for (int i = 0; i < identUsuario.Funcionario.Senha.Length; i++)
                    {
                        if (identUsuario.Funcionario.Senha[i] != senhaAplicacao[i])
                            mensagem = "A Senha não confere.";
                    }
                    #endregion

                    #region Verificar Perfil do Usuário
                    if (string.IsNullOrEmpty(mensagem))
                    {
                        bool liberarAcesso = false;

                        //Retornar lista de Perfis do Usuário
                        var listaPerfilUsuarioAdm = new UsuarioData().ListaPerfilAdministracao(identUsuario);
                        var listaPerfilAcesso = new PerfilAcessoData().Listar(new PerfilAcessoVO());
                        //Percorrer perfis do Usuário)
                        foreach (PerfilAcessoVO identPerfilTemp in listaPerfilUsuarioAdm)
                        {
                            var temp = identPerfilTemp;
                            liberarAcesso = listaPerfilAcesso.Any(perfilAcessoVO => temp.CodPerfilAcesso == perfilAcessoVO.CodPerfilAcesso);
                        }

                        if (!liberarAcesso)
                            mensagem = "Usuário não possui perfil de acesso";
                        else
                            identUsuario.PerfilAcessos = listaPerfilUsuarioAdm;
                    }
                    #endregion                   
                }

            }
            else
                mensagem = "Usuário não encontrado ou sem Senha cadastrada!";

            return mensagem;
        }        
        public static IList<PerfilAcessoVO> ListarPerfilNaoAssociado(int codUsuario)
        {
            return new UsuarioData().ListarPerfilNaoAssociado(codUsuario);
        }
        public static IList<PerfilAcessoVO> ListarPerfilAssociado(int codUsuario)
        {
            return new UsuarioData().ListarPerfilAssociado(codUsuario);
        }

        public static void Incluir(int codUsuario, List<PerfilAcessoVO> listaAssociados, int codUsuarioInc)
        {
            new UsuarioData().Incluir(codUsuario, listaAssociados, codUsuarioInc);
        }
        public static void Excluir(int codUsuario, List<PerfilAcessoVO> listaAssociar)
        {
            new UsuarioData().Excluir(codUsuario, listaAssociar);
        }
        public static string ListarMenuUsuario(int codUsuario)
        {
            return new UsuarioData().ListarMenuUsuario(codUsuario);
        }
        /// <summary>
        /// Método usado para Criprografar a string passada como parametro.
        /// </summary>
        /// <returns>
        /// Retorna a string criptografada em SHA1
        /// </returns>
        public static byte[] CriptografarSenha(string senha)
        {
            SHA1 shaM = new SHA1Managed();
            return shaM.ComputeHash(Encoding.ASCII.GetBytes(senha));
        }
        public static void AlterarSenha(Model.UsuarioVO identUsuario)
        {
            new UsuarioData().AlterarSenha(identUsuario);            
        }
    }
}
