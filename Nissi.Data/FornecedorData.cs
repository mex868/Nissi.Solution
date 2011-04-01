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
                if (identFornecedor.CodFornecedor > 0)
                    AddInParameter("@CodFornecedor", DbType.Int32, identFornecedor.CodFornecedor);
                // Parâmetros de entrada
                if (identFornecedor.CodPessoa > 0)
                    AddInParameter("@CodPessoa", DbType.Int32, identFornecedor.CodPessoa);

                if (!string.IsNullOrEmpty(identFornecedor.CNPJ))
                    AddInParameter("@CNPJ", DbType.String, identFornecedor.CNPJ);

                if (!string.IsNullOrEmpty(identFornecedor.RazaoSocial))
                AddInParameter("@RazaoSocial", DbType.String, identFornecedor.RazaoSocial);

                if (!string.IsNullOrEmpty(identFornecedor.NomeFantasia))
                AddInParameter("@NomeFantasia", DbType.String, identFornecedor.NomeFantasia);

                List<FornecedorVO> lstFornecedorVO = new List<FornecedorVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        FornecedorVO fornecedorVO = new FornecedorVO();

                        fornecedorVO.CodPessoa = GetReaderValue<int>(dr, "CodPessoa");
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
                        fornecedorVO.Ativo = GetReaderValue<bool>(dr, "Ativo");
                        fornecedorVO.IndPessoaTipo = GetReaderValue<bool>(dr, "IndPessoaTipo");
                        fornecedorVO.Banco.NumConta = GetReaderValue<string>(dr, "NumConta");
                        fornecedorVO.Banco.TipoConta = GetReaderValue<bool>(dr, "IndTipoConta");
                        fornecedorVO.Banco.Agencia = GetReaderValue<int?>(dr, "NumAgencia");
                        fornecedorVO.Banco.CodBanco = GetReaderValue<int?>(dr, "CodBanco");
                        fornecedorVO.TipoFornecimento.CodTipoFornecimento = GetReaderValue<short?>(dr, "CodTipoFornecimento");
                        ListarCep(ref fornecedorVO);
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


        private void ListarCep(ref FornecedorVO fornecedorVO)
        {
            CEPVO tempCep = new CEPData().Lista(fornecedorVO.Cep);
            fornecedorVO.Cep = tempCep;
        }
        // ------------------------------------------------------------------------- // 
        public List<FornecedorVO> ListaFornecedorPorNome(FornecedorVO identfornecedorVo)
        {
            OpenCommand("pr_selecionar_fornecedor");
            try
            {
                if (!string.IsNullOrEmpty(identfornecedorVo.RazaoSocial))
                    AddInParameter("@RazaoSocial", DbType.String, identfornecedorVo.RazaoSocial);
                List<FornecedorVO> lstFornecedorVo = new List<FornecedorVO>();
                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        FornecedorVO fornecedorVo = new FornecedorVO();
                        fornecedorVo.CodPessoa = GetReaderValue<int>(dr, "CodPessoa");
                        fornecedorVo.RazaoSocial = GetReaderValue<string>(dr, "RazaoSocial");
                        lstFornecedorVo.Add(fornecedorVo);
                    }
                }
                finally
                {
                    dr.Close();
                }
                return lstFornecedorVo;
            }
            finally
            {
                CloseCommand();
            }
        }

        public List<FornecedorVO> ListaFornecedorNomeFantasia(FornecedorVO identfornecedorVo)
        {
            OpenCommand("pr_selecionar_fornecedor");
            try
            {
                if (!string.IsNullOrEmpty(identfornecedorVo.RazaoSocial))
                    AddInParameter("@NomeFantasia", DbType.String, identfornecedorVo.NomeFantasia);
                List<FornecedorVO> lstFornecedorVo = new List<FornecedorVO>();
                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        FornecedorVO clienteVo = new FornecedorVO();
                        clienteVo.CodPessoa = GetReaderValue<int>(dr, "CodPessoa");
                        clienteVo.RazaoSocial = GetReaderValue<string>(dr, "NomeFantasia");
                        lstFornecedorVo.Add(clienteVo);
                    }
                }
                finally
                {
                    dr.Close();
                }
                return lstFornecedorVo;
            }
            finally
            {
                CloseCommand();
            }
        }
        public string PegarEmail(int codFornecedor)
        {
            OpenCommand("pr_pegar_email");
            try
            {
                AddInParameter("@CodCliente", DbType.Int32, codFornecedor);
                PessoaVO pessoaVo = new PessoaVO();
                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        pessoaVo.Email = GetReaderValue<string>(dr, "Email");
                    }
                }
                finally
                {
                    dr.Close();
                }
                return pessoaVo.Email;
            }
            finally
            {
                CloseCommand();
            }
        }
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
                AddInParameter("@CodTipoFornecimento", DbType.Int32, identFornecedor.TipoFornecimento.CodTipoFornecimento);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, identFornecedor.Ativo);
                AddInParameter("@IndPessoaTipo", DbType.Boolean, identFornecedor.IndPessoaTipo);

                if (identFornecedor.Banco.CodBanco > 0)
                    AddInParameter("@CodBanco", DbType.Int32, identFornecedor.Banco.CodBanco);

                AddInParameter("@NumAgencia", DbType.Int32, identFornecedor.Banco.Agencia);
                AddInParameter("@NumConta", DbType.String, identFornecedor.Banco.NumConta);
                AddInParameter("@IndTipoConta", DbType.Boolean, identFornecedor.Banco.TipoConta);
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
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, identFornecedor.Ativo);
                AddInParameter("@CodTipoFornecimento", DbType.Int32, identFornecedor.TipoFornecimento.CodTipoFornecimento);
                AddInParameter("@IndPessoaTipo", DbType.Boolean, identFornecedor.IndPessoaTipo);
                AddInParameter("@CodBanco", DbType.Int32, identFornecedor.Banco.CodBanco);
                AddInParameter("@NumAgencia", DbType.Int32, identFornecedor.Banco.Agencia);
                AddInParameter("@NumConta", DbType.String, identFornecedor.Banco.NumConta);
                AddInParameter("@IndTipoConta", DbType.Boolean, identFornecedor.Banco.TipoConta);


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
