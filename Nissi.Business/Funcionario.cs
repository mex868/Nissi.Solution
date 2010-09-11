using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;
using System.Security.Cryptography;

namespace Nissi.Business
{
    public class Funcionario
    {
        #region Método de Autenticação de Usuário
        /// <summary>
        /// Validar conexão do Usuário
        /// Objeto/Parâmetros: (login, senha, out FuncionarioVO identFuncionario)
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <param name="identFuncionario"></param>
        /// <returns></returns>
        public string Autenticar(string login, string senha, out FuncionarioVO identFuncionario)
        {
            identFuncionario = new FuncionarioVO();
            identFuncionario.Login = login;

            //Retornar dados do Usuário
            identFuncionario = new FuncionarioData().Lista(identFuncionario);

            string mensagem = string.Empty;

            if (identFuncionario != null && identFuncionario.CodFuncionario > 0)
            {
                #region Verificar Senha informada
                byte[] senhaAplicacao = CriptografarSenha(senha);

                for (int i = 0; i < identFuncionario.Senha.Length; i++)
                {
                    if (identFuncionario.Senha[i] != senhaAplicacao[i])
                        mensagem = "A Senha não confere.";
                }
                #endregion

                #region Recupera Pefil do usuario
                if (string.IsNullOrEmpty(mensagem))
                {
                    //Retornar lista de Perfis do Usuário
                    List<PerfilAcessoVO> listaPerfilFuncionario = new FuncionarioData().ListaPerfil(identFuncionario);
                    identFuncionario.Perfils = listaPerfilFuncionario;
                }
                #endregion
            }
            else
                mensagem = "Usuário não encontrado ou sem Senha cadastrada!";

            return mensagem;
        }
        #endregion

        #region Método usado para Criprografar a string passada como parametro.
        /// <summary>
        /// Método usado para Criprografar a string passada como parametro.
        /// </summary>
        /// <returns>
        /// Retorna a string criptografada em SHA1
        /// </returns>
        private byte[] CriptografarSenha(string senha)
        {
            SHA1 shaM = new SHA1Managed();
            return shaM.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(senha));
        }
        #endregion

        #region Métodos de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_funcionario
        /// Objeto/Parâmetro: (identFuncionario)
        /// Valores: identFuncionario.CodFuncionario,
        /// identFuncionario.CPF,
        /// identFuncionario.RG,
        /// identFuncionario.Nome
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<FuncionarioVO> Listar(FuncionarioVO identFuncionario)
        {
            return new FuncionarioData().Listar(identFuncionario);
        }
    /// <summary>
    /// Reinicia a senha do usuário
    /// </summary>
    /// <param name="identFunc">Passar codFuncionário</param>
        public void ReiniciarSenha(FuncionarioVO identFunc)
        {
            new FuncionarioData().ReiniciarSenha(identFunc);
        }

        #endregion

        #region Métodos de Inclusão
        /// <summary>
        /// Método para executar a proc pr_incluir_funcionario
        /// Objeto/Parâmetros: (identFuncionario, codUsuarioOperacao)
        /// Valores: identFuncionario.Cep.CodCep,
        /// identFuncionario.RazaoSocial,
        /// identFuncionario.NomeFantasia,
        /// identFuncionario.Tipo,
        /// identFuncionario.CNPJ,
        /// identFuncionario.InscricaoEstadual,
        /// identFuncionario.Numero,
        /// identFuncionario.Complemento,
        /// identFuncionario.Telefone,
        /// identFuncionario.Fax,
        /// identFuncionario.Celular,
        /// identFuncionario.Contato,
        /// identFuncionario.Email,
        /// identFuncionario.Site,
        /// identFuncionario.Observacao,
        /// codUsuarioOperacao,
        /// identFuncionario.Ativo,
        /// identFuncionario.Cargo.CodCargo,
        /// identFuncionario.Departamento.CodDepartamento,
        /// identFuncionario.AcessaSistema,
        /// identFuncionario.Login,
        /// identFuncionario.Senha,
        /// identFuncionario.DataAdmissao,
        /// identFuncionario.DataDemissao
        /// </summary>
        public int Incluir(FuncionarioVO identFuncionario, int codUsuarioOperacao)
        {
           return new FuncionarioData().Incluir(identFuncionario, codUsuarioOperacao);
        }


        // ------------------------------------------------------------------------- //
        #endregion

        #region Métodos de Alteração
        /// <summary>
        /// Método para executar a proc pr_alterar_funcionario 
        /// Objeto/Parâmetros: (identFuncionario, codUsuarioOperacao)
        /// Valores: identFuncionario.CodFuncionario,
        /// identFuncionario.CodPessoa,
        /// identFuncionario.Cep.CodCep,
        /// identFuncionario.RazaoSocial,
        /// identFuncionario.NomeFantasia,
        /// identFuncionario.Tipo,
        /// identFuncionario.CNPJ,
        /// identFuncionario.InscricaoEstadual,
        /// identFuncionario.Numero,
        /// identFuncionario.Complemento,
        /// identFuncionario.Telefone,
        /// identFuncionario.Fax,
        /// identFuncionario.Celular,
        /// identFuncionario.Contato,
        /// identFuncionario.Email,
        /// identFuncionario.Site,
        /// identFuncionario.Observacao,
        /// codUsuarioOperacao,
        /// identFuncionario.Ativo,
        /// identFuncionario.Cargo.CodCargo,
        /// identFuncionario.Departamento.CodDepartamento,
        /// identFuncionario.AcessaSistema,
        /// identFuncionario.Login,
        /// identFuncionario.Senha,
        /// identFuncionario.DataAdmissao,
        /// identFuncionario.DataDemissao
        /// </summary>
        public void Alterar(FuncionarioVO identFuncionario, int codUsuarioOperacao)
        {
            new FuncionarioData().Alterar(identFuncionario, codUsuarioOperacao);
        }

        // ------------------------------------------------------------------------- // 
        #endregion

        #region Métodos de Exclusão
        /// <summary>
        /// Método para executar a proc pr_excluir_pessoa
        /// Objeto/Parâmetros: (codPessoa)
        /// </summary>
        public void Excluir(int? codPessoa, string tipo)
        {
            new FuncionarioData().Excluir(codPessoa,tipo);
        }
        #endregion

        public List<FuncionarioVO> Listar()
        {
            return new FuncionarioData().Lista();
        }
    }
}
