using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
    public class FornecedorData: NissiBaseData
    {
        #region Métodos de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_fornecedor
        /// Objeto/Parâmetros: (identFornecedor)
        /// Valores: identFornecedor.CodPessoa,
        /// identFornecedor.CNPJ,
        /// identFornecedor.RazaoSocial,
        /// identFornecedor.NomeFantasia
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<FornecedorVO> Listar(FornecedorVO identFornecedor)
        {
            OpenCommand("pr_selecionar_fornecedor");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodPessoa", DbType.Int32, identFornecedor.CodPessoa);
                AddInParameter("@CNPJ", DbType.String, identFornecedor.CNPJ);
                AddInParameter("@RazaoSocial", DbType.String, identFornecedor.RazaoSocial);
                AddInParameter("@NomeFantasia", DbType.String, identFornecedor.NomeFantasia);

                List<FornecedorVO> lstFornecedorVO = new List<FornecedorVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        FornecedorVO fornecedorVO = new FornecedorVO();

                        fornecedorVO.CodPessoa = GetReaderValue<int?>(dr, "CodPessoa");
                        fornecedorVO.CodFornecedor = GetReaderValue<int?>(dr, "CodFornecedor");
                        fornecedorVO.Cep.CodCep = GetReaderValue<string>(dr, "CodCep");
                        fornecedorVO.RazaoSocial = GetReaderValue<string>(dr, "RazaoSocial");
                        fornecedorVO.NomeFantasia = GetReaderValue<string>(dr, "NomeFantasia");
                        fornecedorVO.Tipo = GetReaderValue<string>(dr, "Tipo");
                        fornecedorVO.CNPJ = GetReaderValue<string>(dr, "CNPJ");
                        fornecedorVO.InscricaoEstadual = GetReaderValue<string>(dr, "InscricaoEstadual");
                        fornecedorVO.Numero = GetReaderValue<string>(dr, "Numero");
                        fornecedorVO.Complemento = GetReaderValue<string>(dr, "Complemento");
                        fornecedorVO.Telefone = GetReaderValue<string>(dr, "Telefone");
                        fornecedorVO.Fax = GetReaderValue<string>(dr, "Fax");
                        fornecedorVO.Celular = GetReaderValue<string>(dr, "Celular");
                        fornecedorVO.Contato = GetReaderValue<string>(dr, "Contato");
                        fornecedorVO.Email = GetReaderValue<string>(dr, "Email");
                        fornecedorVO.Site = GetReaderValue<string>(dr, "Site");
                        fornecedorVO.Observacao = GetReaderValue<string>(dr, "Observacao");
                        fornecedorVO.DataCadastro = GetReaderValue<DateTime>(dr, "DataCadastro");
                        fornecedorVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        fornecedorVO.DataAlteracao = GetReaderValue<DateTime>(dr, "DataAlteracao");
                        fornecedorVO.UsuarioAlt = GetReaderValue<int>(dr, "UsuarioAlt");
                        fornecedorVO.Ativo = GetReaderValue<bool>(dr, "Ativo");
                        fornecedorVO.IndPessoaTipo = GetReaderValue<bool>(dr, "IndPessoaTipo");
                        lstFornecedorVO.Add(fornecedorVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstFornecedorVO;
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
        /// Método para executar a proc pr_incluir_fornecedor 
        /// Objeto/Parâmetros: (identFornecedor, codUsuarioOperacao)
        /// Valores: identFornecedor.Cep.CodCep,
        ///  identFornecedor.NomeFantasia,
        ///  identFornecedor.Tipo,
        ///  identFornecedor.CNPJ,
        ///  identFornecedor.InscricaoEstadual,
        ///  identFornecedor.Numero,
        ///  identFornecedor.Complemento,
        ///  identFornecedor.Telefone,
        ///  dentFornecedor.Fax,
        ///  identFornecedor.Celular,
        ///  identFornecedor.Contato,
        ///  identFornecedor.Email,
        ///  identFornecedor.Site,
        ///  identFornecedor.Observacao,
        ///  identFornecedor.PrazoEntrega,
        ///  identFornecedor.FormaPgto.CodFormaPgto,
        ///  identFornecedor.Departamento.CodDepartamento,
        ///  identFornecedor.TipoFornecimento.CodTipoFornecimento,
        ///  codUsuarioOperacao,
        ///  identFornecedor.Ativo,
        ///  identFornecedor.IndPessoaTipo
        /// </summary>
        public void Incluir(FornecedorVO identFornecedor, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_fornecedor");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodCep", DbType.String, identFornecedor.Cep.CodCep);
                AddInParameter("@RazaoSocial", DbType.String, identFornecedor.RazaoSocial);
                AddInParameter("@NomeFantasia", DbType.String, identFornecedor.NomeFantasia);
                AddInParameter("@Tipo", DbType.StringFixedLength, "F");
                AddInParameter("@CNPJ", DbType.String, identFornecedor.CNPJ);
                AddInParameter("@InscricaoEstadual", DbType.String, identFornecedor.InscricaoEstadual);
                AddInParameter("@Numero", DbType.String, identFornecedor.Numero);
                AddInParameter("@Complemento", DbType.String, identFornecedor.Complemento);
                AddInParameter("@Telefone", DbType.StringFixedLength, identFornecedor.Telefone);
                AddInParameter("@Fax", DbType.StringFixedLength, identFornecedor.Fax);
                AddInParameter("@Celular", DbType.StringFixedLength, identFornecedor.Celular);
                AddInParameter("@Contato", DbType.String, identFornecedor.Contato);
                AddInParameter("@Email", DbType.String, identFornecedor.Email);
                AddInParameter("@Site", DbType.String, identFornecedor.Site);
                AddInParameter("@Observacao", DbType.String, identFornecedor.Observacao);
                AddInParameter("@PrazoEntrega", DbType.Int32, identFornecedor.PrazoEntrega);
                AddInParameter("@CodFormaPgto", DbType.Int32, identFornecedor.FormaPgto.CodFormaPgto);
                AddInParameter("@CodDepartamento", DbType.Int16, identFornecedor.Departamento.CodDepartamento);
                AddInParameter("@CodTipoFornecimento", DbType.Double, identFornecedor.TipoFornecimento.CodTipoFornecimento);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, identFornecedor.Ativo);
                AddInParameter("@IndPessoaTipo", DbType.Boolean, identFornecedor.IndPessoaTipo);
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
        /// Método para executar a proc pr_alterar_fornecedor 
        /// Objeto/Parâmetros: (identFornecedor, codUsuarioOperacao)
        /// Valores: identFornecedor.CodPessoa, 
        ///  identFornecedor.CodFornecedor,
        ///  identFornecedor.Cep.CodCep,
        ///  identFornecedor.NomeFantasia,
        ///  identFornecedor.CNPJ,
        ///  identFornecedor.InscricaoEstadual,
        ///  identFornecedor.Numero,
        ///  identFornecedor.Complemento,
        ///  identFornecedor.Telefone,
        ///  dentFornecedor.Fax,
        ///  identFornecedor.Celular,
        ///  identFornecedor.Contato,
        ///  identFornecedor.Email,
        ///  identFornecedor.Site,
        ///  identFornecedor.Observacao,
        ///  identFornecedor.PrazoEntrega,
        ///  identFornecedor.FormaPgto.CodFormaPgto,
        ///  identFornecedor.Departamento.CodDepartamento,
        ///  identFornecedor.TipoFornecimento.CodTipoFornecimento,
        ///  codUsuarioOperacao,
        ///  identFornecedor.Ativo,
        ///  identFornecedor.IndPessoaTipo
        /// </summary>
        public void Alterar(FornecedorVO identFornecedor, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_fornecedor");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@Codfornecedor", DbType.Int32, identFornecedor.CodFornecedor);
                AddInParameter("@CodPessoa", DbType.Int32, identFornecedor.CodPessoa);
                // Parâmetros de entrada
                AddInParameter("@CodCep", DbType.String, identFornecedor.Cep.CodCep);
                AddInParameter("@RazaoSocial", DbType.String, identFornecedor.RazaoSocial);
                AddInParameter("@NomeFantasia", DbType.String, identFornecedor.NomeFantasia);
                AddInParameter("@Tipo", DbType.StringFixedLength, "F");
                AddInParameter("@CNPJ", DbType.String, identFornecedor.CNPJ);
                AddInParameter("@InscricaoEstadual", DbType.String, identFornecedor.InscricaoEstadual);
                AddInParameter("@Numero", DbType.String, identFornecedor.Numero);
                AddInParameter("@Complemento", DbType.String, identFornecedor.Complemento);
                AddInParameter("@Telefone", DbType.StringFixedLength, identFornecedor.Telefone);
                AddInParameter("@Fax", DbType.StringFixedLength, identFornecedor.Fax);
                AddInParameter("@Celular", DbType.StringFixedLength, identFornecedor.Celular);
                AddInParameter("@Contato", DbType.String, identFornecedor.Contato);
                AddInParameter("@Email", DbType.String, identFornecedor.Email);
                AddInParameter("@Site", DbType.String, identFornecedor.Site);
                AddInParameter("@Observacao", DbType.String, identFornecedor.Observacao);
                AddInParameter("@PrazoEntrega", DbType.Int32, identFornecedor.PrazoEntrega);
                AddInParameter("@CodFormaPgto", DbType.Int32, identFornecedor.FormaPgto.CodFormaPgto);
                AddInParameter("@CodDepartamento", DbType.Int16, identFornecedor.Departamento.CodDepartamento);
                AddInParameter("@CodTipoFornecimento", DbType.Double, identFornecedor.TipoFornecimento.CodTipoFornecimento);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, identFornecedor.Ativo);
                AddInParameter("@IndPessoaTipo", DbType.Boolean, identFornecedor.IndPessoaTipo);
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
        /// Método para executar a proc pr_excluir_fornecedor
        /// Objeto/Parâmetros: (codPessoa)
        /// </summary>
        public void Excluir(int? codPessoa)
        {
            OpenCommand("pr_excluir_fornecedor");
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
