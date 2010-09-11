using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class FuncionarioData : NissiBaseData
    {

        #region Métodos de Listagem

        /// <summary>
        /// Listar usuário com dados da conexão
        /// Objeto/Parâmetros: (identFuncionario)
        /// Valores: identFuncionario.Login
        /// </summary>
        /// <param name="identFuncionario"></param>
        /// <returns></returns>
        public FuncionarioVO Lista(FuncionarioVO identFuncionario)
        {
            OpenCommand("pr_selecionar_funcionario_login");

            try
            {
                AddInParameter("Login", DbType.String, identFuncionario.Login);

                FuncionarioVO identFuncionarioTemp = new FuncionarioVO();

                IDataReader dr = ExecuteReader();

                try
                {
                    if (dr.Read())
                    {
                        identFuncionarioTemp.CodFuncionario = GetReaderValue<int>(dr, "CodFuncionario");
                        identFuncionarioTemp.RazaoSocial = GetReaderValue<string>(dr, "RazaoSocial");
                        identFuncionarioTemp.Login = GetReaderValue<string>(dr, "Login");
                        identFuncionarioTemp.Senha = GetReaderValue<byte[]>(dr, "Senha");
                    }
                }
                finally
                {
                    dr.Close();
                }

                return identFuncionarioTemp;
            }
            finally
            {
                CloseCommand();
            }
        }

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
		OpenCommand("pr_selecionar_funcionario");
		try
		{
			// Parâmetros de entrada
            AddInParameter("@CodFuncionario", DbType.Int32, identFuncionario.CodFuncionario);
            AddInParameter("@CPF", DbType.String, identFuncionario.CPF);
            AddInParameter("@RG", DbType.String, identFuncionario.RG);
            AddInParameter("@Nome", DbType.String, identFuncionario.Nome);

            List<FuncionarioVO> lstFuncionarioVO = new List<FuncionarioVO>();
            
			IDataReader dr = ExecuteReader();            
			try
			{
				while (dr.Read())
				{
                    FuncionarioVO funcionarioVO = new FuncionarioVO();
					
					funcionarioVO.CodFuncionario = GetReaderValue<int>(dr, "CodFuncionario");
					funcionarioVO.CodPessoa = GetReaderValue<int?>(dr, "CodPessoa");
					funcionarioVO.Cargo.CodCargo = GetReaderValue<short?>(dr, "CodCargo");
                    funcionarioVO.Cargo.Nome = GetReaderValue<string>(dr, "Cargo");
					funcionarioVO.Departamento.CodDepartamento = GetReaderValue<short?>(dr, "CodDepartamento");
                    funcionarioVO.Departamento.Nome = GetReaderValue<string>(dr, "Departamento");
					funcionarioVO.AcessaSistema = GetReaderValue<bool>(dr, "AcessaSistema");
					funcionarioVO.Login = GetReaderValue<string>(dr, "Login");
					funcionarioVO.Senha = GetReaderValue<byte[]>(dr, "Senha");
					funcionarioVO.DataAdmissao = GetReaderValue<DateTime>(dr, "DataAdmissao");
					funcionarioVO.DataDemissao = GetReaderValue<DateTime?>(dr, "DataDemissao");
					funcionarioVO.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
					funcionarioVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
					funcionarioVO.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");
					funcionarioVO.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");
					funcionarioVO.Ativo = GetReaderValue<bool?>(dr, "Ativo");
					funcionarioVO.Cep.CodCep = GetReaderValue<string>(dr, "CodCep");
					funcionarioVO.Nome = GetReaderValue<string>(dr, "RazaoSocial");
                    funcionarioVO.Apelido = GetReaderValue<string>(dr, "NomeFantasia");
					funcionarioVO.Tipo = GetReaderValue<string>(dr, "Tipo");
					funcionarioVO.CNPJ = GetReaderValue<string>(dr, "CNPJ");
					funcionarioVO.InscricaoEstadual = GetReaderValue<string>(dr, "InscricaoEstadual");
					funcionarioVO.Numero = GetReaderValue<string>(dr, "Numero");
					funcionarioVO.Complemento = GetReaderValue<string>(dr, "Complemento");
					funcionarioVO.Telefone = GetReaderValue<string>(dr, "Telefone");
					funcionarioVO.Fax = GetReaderValue<string>(dr, "Fax");
					funcionarioVO.Celular = GetReaderValue<string>(dr, "Celular");
					funcionarioVO.Contato = GetReaderValue<string>(dr, "Contato");
					funcionarioVO.Email = GetReaderValue<string>(dr, "Email");
					funcionarioVO.Site = GetReaderValue<string>(dr, "Site");
					funcionarioVO.Observacao = GetReaderValue<string>(dr, "Observacao");
					
					lstFuncionarioVO.Add(funcionarioVO);
				}
			}
			finally
			{
				dr.Close();
			} 
			
			return lstFuncionarioVO;
		}            
		finally
		{
			CloseCommand();
		}				
	}
    public List<PerfilAcessoVO> ListaPerfil(FuncionarioVO identFuncionario)
    {
        OpenCommand("pr_selecionar_perfil_Funcionario");

        try
        {
            AddInParameter("CodUsu", DbType.Int32, identFuncionario.CodFuncionario);

            List<PerfilAcessoVO> listaPerfil = new List<PerfilAcessoVO>();

            IDataReader dr = ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    PerfilAcessoVO identPerfil = new PerfilAcessoVO();
                    identPerfil.CodPerfilAcesso = GetReaderValue<short>(dr, "CodPerfilAcesso");
                    listaPerfil.Add(identPerfil);
                }
            }
            finally
            {
                dr.Close();
            }

            return listaPerfil;
        }
        finally
        {
            CloseCommand();
        }

    }


 // ------------------------------------------------------------------------- // 
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
    public void Incluir(FuncionarioVO identFuncionario, int codUsuarioOperacao)
    {
        OpenCommand("pr_incluir_funcionario");
        try
        {
            // Parâmetros de entrada
            AddInParameter("@CodCep", DbType.String, identFuncionario.Cep.CodCep);
            AddInParameter("@RazaoSocial", DbType.String, identFuncionario.RazaoSocial);
            AddInParameter("@NomeFantasia", DbType.String, identFuncionario.NomeFantasia);
            AddInParameter("@Tipo", DbType.StringFixedLength, "E");
            AddInParameter("@CNPJ", DbType.String, identFuncionario.CNPJ);
            AddInParameter("@InscricaoEstadual", DbType.String, identFuncionario.InscricaoEstadual);
            AddInParameter("@Numero", DbType.String, identFuncionario.Numero);
            AddInParameter("@Complemento", DbType.String, identFuncionario.Complemento);
            AddInParameter("@Telefone", DbType.StringFixedLength, identFuncionario.Telefone);
            AddInParameter("@Fax", DbType.StringFixedLength, identFuncionario.Fax);
            AddInParameter("@Celular", DbType.StringFixedLength, identFuncionario.Celular);
            AddInParameter("@Contato", DbType.String, identFuncionario.Contato);
            AddInParameter("@Email", DbType.String, identFuncionario.Email);
            AddInParameter("@Site", DbType.String, identFuncionario.Site);
            AddInParameter("@Observacao", DbType.String, identFuncionario.Observacao);
            AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
            AddInParameter("@Ativo", DbType.Boolean, identFuncionario.Ativo);
            AddInParameter("@CodCargo", DbType.Int32, identFuncionario.Cargo.CodCargo);
            AddInParameter("@CodDepartamento", DbType.Int32, identFuncionario.Departamento.CodDepartamento);
            AddInParameter("@AcessaSistema", DbType.Boolean, identFuncionario.AcessaSistema);
            AddInParameter("@Login", DbType.String, identFuncionario.Login);
            AddBinaryParameter("@Senha", identFuncionario.Senha);
            AddInParameter("@DataAdmissao", DbType.DateTime, identFuncionario.DataAdmissao);
            AddInParameter("@DataDemissao", DbType.DateTime, identFuncionario.DataDemissao);

            ExecuteNonQuery();
        }
        finally
        {
            CloseCommand();
        }
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
        OpenCommand("pr_alterar_funcionario");
        try
        {
            //Parâmetros de entrada
            AddInParameter("@CodFuncionario", DbType.Int32, identFuncionario.CodFuncionario);
            AddInParameter("@CodPessoa", DbType.Int32, identFuncionario.CodPessoa);
            AddInParameter("@CodCep", DbType.String, identFuncionario.Cep.CodCep);
            AddInParameter("@RazaoSocial", DbType.String, identFuncionario.RazaoSocial);
            AddInParameter("@NomeFantasia", DbType.String, identFuncionario.NomeFantasia);
            AddInParameter("@Tipo", DbType.StringFixedLength, "E");
            AddInParameter("@CNPJ", DbType.String, identFuncionario.CNPJ);
            AddInParameter("@InscricaoEstadual", DbType.String, identFuncionario.InscricaoEstadual);
            AddInParameter("@Numero", DbType.String, identFuncionario.Numero);
            AddInParameter("@Complemento", DbType.String, identFuncionario.Complemento);
            AddInParameter("@Telefone", DbType.StringFixedLength, identFuncionario.Telefone);
            AddInParameter("@Fax", DbType.StringFixedLength, identFuncionario.Fax);
            AddInParameter("@Celular", DbType.StringFixedLength, identFuncionario.Celular);
            AddInParameter("@Contato", DbType.String, identFuncionario.Contato);
            AddInParameter("@Email", DbType.String, identFuncionario.Email);
            AddInParameter("@Site", DbType.String, identFuncionario.Site);
            AddInParameter("@Observacao", DbType.String, identFuncionario.Observacao);
            AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);
            AddInParameter("@Ativo", DbType.Boolean, identFuncionario.Ativo);
            AddInParameter("@CodCargo", DbType.Int32, identFuncionario.Cargo.CodCargo);
            AddInParameter("@CodDepartamento", DbType.Int32, identFuncionario.Departamento.CodDepartamento);
            AddInParameter("@AcessaSistema", DbType.Boolean, identFuncionario.AcessaSistema);
            AddInParameter("@Login", DbType.String, identFuncionario.Login);
            AddBinaryParameter("@Senha", identFuncionario.Senha);
            AddInParameter("@DataAdmissao", DbType.DateTime, identFuncionario.DataAdmissao);
            AddInParameter("@DataDemissao", DbType.DateTime, identFuncionario.DataDemissao);

            ExecuteNonQuery();
        }
        finally
        {
            CloseCommand();
        }
    }

    // ------------------------------------------------------------------------- // 
        #endregion
        #region Métodos de Exclusão
    /// <summary>
    /// Método para executar a proc pr_excluir_pessoa
    /// Objeto/Parâmetros: (codPessoa)
    /// </summary>
    public void Excluir(int? codPessoa,string tipo)
    {
        OpenCommand("pr_excluir_funcionario");
        try
        {
            // Parâmetros de entrada
            AddInParameter("@CodPessoa", DbType.Int32, codPessoa);

            ExecuteNonQuery();
        }
        finally
        {
            CloseCommand();
        }
    }
    #endregion
    }
}
