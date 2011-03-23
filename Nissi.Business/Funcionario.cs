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

        public List<FuncionarioVO> ListaFuncionarioPorNome(FuncionarioVO identFuncionarioVo)
        {
            return new FuncionarioData().ListaFuncionarioPorNome(identFuncionarioVo);
        }
    }
}
