using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class ClienteData : NissiBaseData
    {
        #region Métodos de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_cliente
        /// Objeto/Parametros: (identCliente)
        /// Valores: identCliente.CodPessoa,
        /// identCliente.CNPJ,
        /// identCliente.RazaoSocial,
        /// identCliente.NomeFantasia
        /// </summary>
        public List<ClienteVO> Listar(ClienteVO identCliente)
        {
            OpenCommand("pr_selecionar_cliente");
            try
            {
                
                // Parâmetros de entrada
                if (identCliente.CodPessoa > 0)
                AddInParameter("@CodPessoa", DbType.Int32, identCliente.CodPessoa);

                if (!string.IsNullOrEmpty(identCliente.CNPJ))
                AddInParameter("@CNPJ", DbType.String, identCliente.CNPJ);

                if (!string.IsNullOrEmpty(identCliente.RazaoSocial))
                AddInParameter("@RazaoSocial", DbType.String, identCliente.RazaoSocial);

                if (!string.IsNullOrEmpty(identCliente.NomeFantasia))
                AddInParameter("@NomeFantasia", DbType.String, identCliente.NomeFantasia);

                if (!string.IsNullOrEmpty(identCliente.CodRef))
                    AddInParameter("@CodRef", DbType.String, identCliente.CodRef);
                AddInParameter("@IndPessoaTipo", DbType.Boolean,identCliente.IndPessoaTipo);
                List<ClienteVO> lstClienteVO = new List<ClienteVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        ClienteVO clienteVO = new ClienteVO();
                        clienteVO.CodPessoa = GetReaderValue<int?>(dr, "CodPessoa");
                        clienteVO.Cep.CodCep = GetReaderValue<string>(dr, "CodCep");
                        clienteVO.Funcionario.CodFuncionario = GetReaderValue<int?>(dr, "CodFuncionario");
                        clienteVO.RazaoSocial = GetReaderValue<string>(dr, "RazaoSocial");
                        clienteVO.NomeFantasia = GetReaderValue<string>(dr, "NomeFantasia");
                        clienteVO.Tipo = GetReaderValue<string>(dr, "Tipo");
                        clienteVO.CNPJ = GetReaderValue<string>(dr, "CNPJ");
                        clienteVO.InscricaoEstadual = GetReaderValue<string>(dr, "InscricaoEstadual");
                        clienteVO.Numero = GetReaderValue<string>(dr, "Numero");
                        clienteVO.Complemento = GetReaderValue<string>(dr, "Complemento");
                        clienteVO.Telefone = GetReaderValue<string>(dr, "Telefone");
                        clienteVO.Fax = GetReaderValue<string>(dr, "Fax");
                        clienteVO.Celular = GetReaderValue<string>(dr, "Celular");
                        clienteVO.Contato = GetReaderValue<string>(dr, "Contato");
                        clienteVO.Email = GetReaderValue<string>(dr, "Email");
                        clienteVO.Site = GetReaderValue<string>(dr, "Site");
                        clienteVO.Observacao = GetReaderValue<string>(dr, "Observacao");
                        clienteVO.DataCadastro = GetReaderValue<DateTime>(dr, "DataCadastro");
                        clienteVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        clienteVO.DataAlteracao = GetReaderValue<DateTime>(dr, "DataAlteracao");
                        clienteVO.UsuarioAlt = GetReaderValue<int>(dr, "UsuarioAlt");
                        clienteVO.Ativo = GetReaderValue<bool>(dr, "Ativo");
                        clienteVO.IndPessoaTipo = GetReaderValue<bool>(dr, "IndPessoaTipo");
                        clienteVO.CodRef = GetReaderValue<string>(dr, "CodRef");
                        clienteVO.EmailNFE = GetReaderValue<string>(dr, "EmailNFE");
                        clienteVO.CepCobranca = GetReaderValue<string>(dr, "CepCobranca");
                        clienteVO.EnderecoCobranca = GetReaderValue<string>(dr, "EnderecoCobranca");
                        ListarCep(ref clienteVO);
                        lstClienteVO.Add(clienteVO);
                    }
                }
                finally
                {
                    dr.Close();
                }
                return lstClienteVO;
            }
            finally
            {
                CloseCommand();
            }
        }

        private void ListarCep(ref ClienteVO clienteVO)
        {
            CEPVO tempCep = new CEPData().Lista(clienteVO.Cep);
            clienteVO.Cep = tempCep;
        }

        public List<ClienteVO> ListaClientePorNome(ClienteVO identClienteVo)
        {
            OpenCommand("pr_selecionar_cliente");
            try
            {
                if (!string.IsNullOrEmpty(identClienteVo.RazaoSocial))
                    AddInParameter("@RazaoSocial", DbType.String, identClienteVo.RazaoSocial);
                List<ClienteVO> lstClienteVo = new List<ClienteVO>();
                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        ClienteVO clienteVo = new ClienteVO();
                        clienteVo.CodPessoa = GetReaderValue<int>(dr, "CodPessoa");
                        clienteVo.RazaoSocial = GetReaderValue<string>(dr, "RazaoSocial");
                        lstClienteVo.Add(clienteVo);
                    }
                }
                finally
                {
                    dr.Close();
                }
                return lstClienteVo;
            }
            finally
            {
                CloseCommand();
            }

        }

        public List<ClienteVO> ListaClientePorNomeFantasia(ClienteVO identCliente)
        {
            OpenCommand("pr_selecionar_cliente");
            try
            {
                if (!string.IsNullOrEmpty(identCliente.RazaoSocial))
                    AddInParameter("@NomeFantasia", DbType.String, identCliente.NomeFantasia);
                List<ClienteVO> lstClienteVo = new List<ClienteVO>();
                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        ClienteVO clienteVo = new ClienteVO();
                        clienteVo.CodPessoa = GetReaderValue<int>(dr, "CodPessoa");
                        clienteVo.RazaoSocial = GetReaderValue<string>(dr, "NomeFantasia");
                        lstClienteVo.Add(clienteVo);
                    }
                }
                finally
                {
                    dr.Close();
                }
                return lstClienteVo;
            }
            finally
            {
                CloseCommand();
            }
        }
 
        // ------------------------------------------------------------------------- // 
        #endregion
        #region Métodos de Listagem de Transportadoras
        /// <summary>
        /// Método para executar a proc pr_selecionar_tranportadoraporcliente 
        /// Objeto/Parametros: (codPessoa)
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<TransportadoraVO> ListarPorCliente(int? codPessoa)
        {
            OpenCommand("pr_selecionar_tranportadoraporcliente");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodPessoa", DbType.Int32, codPessoa);


                List<TransportadoraVO> lstTransportadoraVO = new List<TransportadoraVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        TransportadoraVO transportadoraVO = new TransportadoraVO();

                        transportadoraVO.CodPessoa = GetReaderValue<int?>(dr, "CodPessoa");
                        transportadoraVO.CodTransportadora = GetReaderValue<int?>(dr, "CodTransportadora");
                        transportadoraVO.RazaoSocial = GetReaderValue<string>(dr, "RazaoSocial");
                        lstTransportadoraVO.Add(transportadoraVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstTransportadoraVO;
            }
            finally
            {
                CloseCommand();
            }
        }

        // ------------------------------------------------------------------------- // 
        #endregion 
        #region Método de Vinculação da Tranportadora com o Cliente
        /// <summary>
	    /// Método para executar a proc pr_incluir_clientetransportadora 
        /// Objeto/Parametros: (codPessoa, codTransportadora, usuarioInc)
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
	    public void IncluirVinculacao(int? codPessoa, int? codTransportadora, int? usuarioInc)
	    {	
		    OpenCommand("pr_incluir_clientetransportadora");
		    try
		    {
			    // Parâmetros de entrada
			    AddInParameter("@CodPessoa", DbType.Int32, codPessoa);
			    AddInParameter("@CodTransportadora", DbType.Int32, codTransportadora);
			    AddInParameter("@UsuarioInc", DbType.Int32, usuarioInc);
			    ExecuteNonQuery();
		    }            
		    finally
		    {
			    CloseCommand();
		    }				
	    }


 // ------------------------------------------------------------------------- // 
        #endregion
        #region Método que retira a Vinculação da Transportadora com o Cliente
        /// <summary>
        /// Método para executar a proc pr_excluir_clientetransportadora 
        /// Objeto/Parametros: (codPessoa, codTransportadora)
        /// </summary>
        public void ExcluirVinculacao(int? codPessoa, int? codTransportadora)
        {
            OpenCommand("pr_excluir_clientetransportadora");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodPessoa", DbType.Int32, codPessoa);
                AddInParameter("@CodTransportadora", DbType.Int32, codTransportadora);

                ExecuteNonQuery();
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
        /// Método para executar a proc pr_incluir_cliente 
        /// Objeto/Parametros: (identCliente, codUsuarioOperacao)
        /// Valore: identCliente.Cep.CodCep,
        /// identCliente.RazaoSocial,
        /// identCliente.NomeFantasia,
        /// identCliente.Tipo,
        /// identCliente.CNPJ,
        /// identCliente.InscricaoEstadual,
        /// identCliente.Numero,
        /// identCliente.Complemento,
        /// identCliente.Telefone,
        /// identCliente.Fax,
        /// identCliente.Celular,
        /// identCliente.Contato,
        /// identCliente.Email,
        /// identCliente.Site,
        /// identCliente.Observacao,
        /// codUsuarioOperacao,
        /// identCliente.Funcionario.CodFuncionario,
        /// identCliente.Ativo,
        /// identCliente.IndPessoaTipo,
        /// identCliente.CodRef
        /// </summary>
        public int Incluir(ClienteVO identCliente, string xmlLista, int codUsuarioOperacao)
        {
            int codPessoa = 0;
            OpenCommand("pr_incluir_cliente", true);
            try
            {
                // Parâmetros de entrada

                AddInParameter("@CodCep", DbType.String, identCliente.Cep.CodCep);
                AddInParameter("@RazaoSocial", DbType.String, identCliente.RazaoSocial);
                AddInParameter("@NomeFantasia", DbType.String, identCliente.NomeFantasia);
                AddInParameter("@Tipo", DbType.StringFixedLength, "C");
                AddInParameter("@CNPJ", DbType.String, identCliente.CNPJ);
                AddInParameter("@InscricaoEstadual", DbType.String, identCliente.InscricaoEstadual);
                AddInParameter("@Numero", DbType.String, identCliente.Numero);
                AddInParameter("@Complemento", DbType.String, identCliente.Complemento);
                AddInParameter("@Telefone", DbType.StringFixedLength, identCliente.Telefone);
                AddInParameter("@Fax", DbType.StringFixedLength, identCliente.Fax);
                AddInParameter("@Celular", DbType.StringFixedLength, identCliente.Celular);
                AddInParameter("@Contato", DbType.String, identCliente.Contato);
                AddInParameter("@Email", DbType.String, identCliente.Email);
                AddInParameter("@Site", DbType.String, identCliente.Site);
                AddInParameter("@Observacao", DbType.String, identCliente.Observacao);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@CodFuncionario", DbType.Int32, identCliente.Funcionario.CodFuncionario);
                AddInParameter("@Ativo", DbType.Boolean, identCliente.Ativo);
                AddInParameter("@IndPessoaTipo", DbType.Boolean, identCliente.IndPessoaTipo);
                AddInParameter("@xmlTransportadora", DbType.String, xmlLista);
                AddInParameter("@CodRef", DbType.String, identCliente.CodRef);
                AddInParameter("@EmailNFE", DbType.String, identCliente.EmailNFE);
                AddInParameter("@CepCobranca", DbType.String, identCliente.CepCobranca);
                AddInParameter("@EnderecoCobranca", DbType.String, identCliente.EnderecoCobranca);
                ExecuteNonQuery();
                codPessoa = GetReturnValue();
                return codPessoa;
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
        /// Método para executar a proc pr_alterar_cliente 
        /// Objeto/Parametros: (identCliente, codUsuarioOperacao)
        /// Valore: identCliente.CodPessoa, 
        /// identCliente.Cep.CodCep,
        /// identCliente.RazaoSocial,
        /// identCliente.NomeFantasia,
        /// identCliente.Tipo,
        /// identCliente.CNPJ,
        /// identCliente.InscricaoEstadual,
        /// identCliente.Numero,
        /// identCliente.Complemento,
        /// identCliente.Telefone,
        /// identCliente.Fax,
        /// identCliente.Celular,
        /// identCliente.Contato,
        /// identCliente.Email,
        /// identCliente.Site,
        /// identCliente.Observacao,
        /// codUsuarioOperacao,
        /// identCliente.Funcionario.CodFuncionario,
        /// identCliente.Ativo,
        /// identCliente.IndPessoaTipo,
        /// identCliente.CodRef
        /// </summary>
        public void Alterar(ClienteVO identCliente,string xmlLista, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_cliente");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodPessoa", DbType.Int32, identCliente.CodPessoa);
                // Parâmetros de entrada
                AddInParameter("@CodCep", DbType.String, identCliente.Cep.CodCep);
                AddInParameter("@RazaoSocial", DbType.String, identCliente.RazaoSocial);
                AddInParameter("@NomeFantasia", DbType.String, identCliente.NomeFantasia);
                AddInParameter("@Tipo", DbType.StringFixedLength, "C");
                AddInParameter("@CNPJ", DbType.String, identCliente.CNPJ);
                AddInParameter("@InscricaoEstadual", DbType.String, identCliente.InscricaoEstadual);
                AddInParameter("@Numero", DbType.String, identCliente.Numero);
                AddInParameter("@Complemento", DbType.String, identCliente.Complemento);
                AddInParameter("@Telefone", DbType.StringFixedLength, identCliente.Telefone);
                AddInParameter("@Fax", DbType.StringFixedLength, identCliente.Fax);
                AddInParameter("@Celular", DbType.StringFixedLength, identCliente.Celular);
                AddInParameter("@Contato", DbType.String, identCliente.Contato);
                AddInParameter("@Email", DbType.String, identCliente.Email);
                AddInParameter("@Site", DbType.String, identCliente.Site);
                AddInParameter("@Observacao", DbType.String, identCliente.Observacao);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@CodFuncionario", DbType.Int32, identCliente.Funcionario.CodFuncionario);
                AddInParameter("@Ativo", DbType.Boolean, identCliente.Ativo);
                AddInParameter("@IndPessoaTipo", DbType.Boolean, identCliente.IndPessoaTipo);
                AddInParameter("@xmlTransportadora", DbType.String, xmlLista);
                AddInParameter("@CodRef", DbType.String, identCliente.CodRef);
                AddInParameter("@EmailNFE", DbType.String, identCliente.EmailNFE);
                AddInParameter("@CepCobranca", DbType.String, identCliente.CepCobranca);
                AddInParameter("@EnderecoCobranca", DbType.String, identCliente.EnderecoCobranca);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
        #region Métodos de Exclusão
        // ------------------------------------------------------------------------- // 

	/// <summary>
	/// Método para executar a proc pr_excluir_transportadora
    /// Objeto/Parametros: (codPessoa)
	/// </summary>
	public void Excluir(int? codPessoa)
	{	
		OpenCommand("pr_excluir_cliente");
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


 // ------------------------------------------------------------------------- // 


        #endregion



    }
}
