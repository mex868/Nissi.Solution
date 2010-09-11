using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
    public class TransportadoraData: NissiBaseData
    {
        #region Métodos de Listagem

        /// <summary>
        /// Método para executar a proc pr_selecionar_transportadora - 
        /// Objeto/Parametros: identTransportadora,
        /// Valores: identTransportadora.CodPessoa,
        /// identTransportadora.CNPJ,
        /// identTransportadora.RazaoSocial,
        /// identTransportadora.NomeFantasia,
        /// </summary>
        public List<TransportadoraVO> Listar(TransportadoraVO identTransportadora)
        {
            OpenCommand("pr_selecionar_transportadora");
            try
            {
                // Parâmetros de entrada
                if (identTransportadora.CodPessoa > 0)
                    AddInParameter("@CodPessoa", DbType.Int32, identTransportadora.CodPessoa);

                if(!string.IsNullOrEmpty(identTransportadora.CNPJ))
                    AddInParameter("@CNPJ", DbType.String, identTransportadora.CNPJ);

                if (!string.IsNullOrEmpty(identTransportadora.RazaoSocial))
                AddInParameter("@RazaoSocial", DbType.String, identTransportadora.RazaoSocial);

                if (!string.IsNullOrEmpty(identTransportadora.NomeFantasia))
                AddInParameter("@NomeFantasia", DbType.String, identTransportadora.NomeFantasia);

                if (identTransportadora.CodTransportadora > 0)
                    AddInParameter("@CodTransportadora", DbType.Int32, identTransportadora.CodTransportadora);

                List<TransportadoraVO> lstTransportadoraVO = new List<TransportadoraVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        TransportadoraVO transportadoraVO = new TransportadoraVO();

                        transportadoraVO.CodPessoa = GetReaderValue<int?>(dr, "CodPessoa");
                        transportadoraVO.CodTransportadora = GetReaderValue<int?>(dr, "CodTransportadora");
                        transportadoraVO.Cep.CodCep = GetReaderValue<string>(dr, "CodCep");
                        transportadoraVO.RazaoSocial = GetReaderValue<string>(dr, "RazaoSocial");
                        transportadoraVO.NomeFantasia = GetReaderValue<string>(dr, "NomeFantasia");
                        transportadoraVO.Tipo = GetReaderValue<string>(dr, "Tipo");
                        transportadoraVO.CNPJ = GetReaderValue<string>(dr, "CNPJ");
                        transportadoraVO.InscricaoEstadual = GetReaderValue<string>(dr, "InscricaoEstadual");
                        transportadoraVO.Numero = GetReaderValue<string>(dr, "Numero");
                        transportadoraVO.Complemento = GetReaderValue<string>(dr, "Complemento");
                        transportadoraVO.Telefone = GetReaderValue<string>(dr, "Telefone");
                        transportadoraVO.Fax = GetReaderValue<string>(dr, "Fax");
                        transportadoraVO.Celular = GetReaderValue<string>(dr, "Celular");
                        transportadoraVO.Contato = GetReaderValue<string>(dr, "Contato");
                        transportadoraVO.Email = GetReaderValue<string>(dr, "Email");
                        transportadoraVO.Site = GetReaderValue<string>(dr, "Site");
                        transportadoraVO.Observacao = GetReaderValue<string>(dr, "Observacao");
                        transportadoraVO.DataCadastro = GetReaderValue<DateTime>(dr, "DataCadastro");
                        transportadoraVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        transportadoraVO.DataAlteracao = GetReaderValue<DateTime>(dr, "DataAlteracao");
                        if (dr["Custo"]!= DBNull.Value)
                            transportadoraVO.Custo = Convert.ToDouble(dr["Custo"]) > 0 ? Convert.ToDouble(dr["Custo"]) : double.MinValue;
                        transportadoraVO.UsuarioAlt = GetReaderValue<int>(dr, "UsuarioAlt");
                        transportadoraVO.Ativo = GetReaderValue<bool>(dr, "Ativo");
                        transportadoraVO.IndPessoaTipo = GetReaderValue<bool>(dr, "IndPessoaTipo");
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
        #region Métodos de Inclusão
        /// <summary>
        /// Método para executar a proc pr_incluir_transportadora 
        ///Objeto/Parametros: (identTransportadora,CodUsuarioOperacao)
        /// Valores: identTransportadora.Cep.CodCep,
        /// identTransportadora.RazaoSocial,
        /// identTransportadora.NomeFantasia,
        /// identTransportadora.Tipo,
        /// identTransportadora.CNPJ,
        /// identTransportadora.InscricaoEstadual,
        /// identTransportadora.Numero,
        /// identTransportadora.Complemento,
        /// identTransportadora.Telefone,
        ///identTransportadora.Fax,
        ///identTransportadora.Celular,
        ///identTransportadora.Contato,
        ///identTransportadora.Email,
        ///identTransportadora.Site,
        ///identTransportadora.Observacao,
        ///identTransportadora.Custo,
        ///codUsuarioOperacao,
        ///identTransportadora.Ativo,
        ///identTransportadora.IndPessoaTipo
        /// </summary>
        public void Incluir(TransportadoraVO identTransportadora, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_transportadora");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodCep", DbType.String, identTransportadora.Cep.CodCep);
                AddInParameter("@RazaoSocial", DbType.String, identTransportadora.RazaoSocial);
                AddInParameter("@NomeFantasia", DbType.String, identTransportadora.NomeFantasia);
                AddInParameter("@Tipo", DbType.StringFixedLength, "T");
                AddInParameter("@CNPJ", DbType.String, identTransportadora.CNPJ);
                AddInParameter("@InscricaoEstadual", DbType.String, identTransportadora.InscricaoEstadual);
                AddInParameter("@Numero", DbType.String, identTransportadora.Numero);
                AddInParameter("@Complemento", DbType.String, identTransportadora.Complemento);
                AddInParameter("@Telefone", DbType.StringFixedLength, identTransportadora.Telefone);
                AddInParameter("@Fax", DbType.StringFixedLength, identTransportadora.Fax);
                AddInParameter("@Celular", DbType.StringFixedLength, identTransportadora.Celular);
                AddInParameter("@Contato", DbType.String, identTransportadora.Contato);
                AddInParameter("@Email", DbType.String, identTransportadora.Email);
                AddInParameter("@Site", DbType.String, identTransportadora.Site);
                AddInParameter("@Observacao", DbType.String, identTransportadora.Observacao);
                AddInParameter("@Custo", DbType.Double, identTransportadora.Custo);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, identTransportadora.Ativo);
                AddInParameter("@IndPessoaTipo", DbType.Boolean, identTransportadora.IndPessoaTipo);
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
        /// Método para executar a proc pr_alterar_transportadora 
        ///Objeto/Parametros: (identTransportadora,CodUsuarioOperacao)
        /// Valores:identTransportadora.CodTransportadora,
        /// identTransportadora.CodPessoa,
        /// identTransportadora.Cep.CodCep,
        /// identTransportadora.RazaoSocial,
        /// identTransportadora.NomeFantasia,
        /// identTransportadora.CNPJ,
        /// identTransportadora.InscricaoEstadual,
        /// identTransportadora.Numero,
        /// identTransportadora.Complemento,
        /// identTransportadora.Telefone,
        /// identTransportadora.Fax,
        /// identTransportadora.Celular,
        /// identTransportadora.Contato,
        /// identTransportadora.Email,
        /// identTransportadora.Site,
        /// identTransportadora.Observacao,
        /// identTransportadora.Custo,
        /// codUsuarioOperacao,
        /// identTransportadora.Ativo,
        /// identTransportadora.IndPessoaTipo
        /// </summary>
        public void Alterar(TransportadoraVO identTransportadora, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_transportadora");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodTransportadora", DbType.Int32, identTransportadora.CodTransportadora);
                AddInParameter("@CodPessoa", DbType.Int32, identTransportadora.CodPessoa);
                // Parâmetros de entrada
                AddInParameter("@CodCep", DbType.String, identTransportadora.Cep.CodCep);
                AddInParameter("@RazaoSocial", DbType.String, identTransportadora.RazaoSocial);
                AddInParameter("@NomeFantasia", DbType.String, identTransportadora.NomeFantasia);
                AddInParameter("@Tipo", DbType.StringFixedLength, "T");
                AddInParameter("@CNPJ", DbType.String, identTransportadora.CNPJ);
                AddInParameter("@InscricaoEstadual", DbType.String, identTransportadora.InscricaoEstadual);
                AddInParameter("@Numero", DbType.String, identTransportadora.Numero);
                AddInParameter("@Complemento", DbType.String, identTransportadora.Complemento);
                AddInParameter("@Telefone", DbType.StringFixedLength, identTransportadora.Telefone);
                AddInParameter("@Fax", DbType.StringFixedLength, identTransportadora.Fax);
                AddInParameter("@Celular", DbType.StringFixedLength, identTransportadora.Celular);
                AddInParameter("@Contato", DbType.String, identTransportadora.Contato);
                AddInParameter("@Email", DbType.String, identTransportadora.Email);
                AddInParameter("@Site", DbType.String, identTransportadora.Site);
                AddInParameter("@Observacao", DbType.String, identTransportadora.Observacao);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, identTransportadora.Ativo);
                AddInParameter("@IndPessoaTipo", DbType.Boolean, identTransportadora.IndPessoaTipo); 
                
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
            OpenCommand("pr_excluir_transportadora");
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
