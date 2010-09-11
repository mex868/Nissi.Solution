using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;
using Nissi.Util;

namespace Nissi.DataAccess
{
    public class ProdutoData : NissiBaseData
    {
        #region Métodos de Listagem
        /// <summary>
        /// Método para listar os produtos
        /// </summary>
        /// <returns></returns>
        public List<ProdutoVO> Lista(ProdutoVO identProduto)
        {
            OpenCommand("pr_selecionar_produto");
            try
            {
                if (identProduto.CodProduto > 0)
                    AddInParameter("CodProduto", DbType.Int32, identProduto.CodProduto);
                if (!string.IsNullOrEmpty(identProduto.Codigo))
                    AddInParameter("Codigo", DbType.String, identProduto.Codigo);
                if (!string.IsNullOrEmpty(identProduto.Descricao))
                    AddInParameter("Descricao", DbType.String, identProduto.Descricao);

                IDataReader dr = ExecuteReader();
                List<ProdutoVO> lProduto = new List<ProdutoVO>();

                try
                {
                    while (dr.Read())
                    {
                        ProdutoVO tempProduto = new ProdutoVO();
                        tempProduto.CodProduto = GetReaderValue<int?>(dr, "CodProduto");
                        tempProduto.Unidade.CodUnidade = GetReaderValue<int?>(dr, "CodUnidade");
                        tempProduto.Descricao = GetReaderValue<string>(dr, "Descricao");
                        tempProduto.Codigo = GetReaderValue<string>(dr, "Codigo");
                        tempProduto.Unidade.Descricao = GetReaderValue<string>(dr, "Unidade");
                        tempProduto.NCM = GetReaderValue<string>(dr, "NCM");

                        ListaICMS(ref tempProduto, tempProduto.CodProduto);
                        
                        lProduto.Add(tempProduto);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lProduto;
            }
            finally
            {
                CloseCommand();
            }
        }

        /// <summary>
        /// Método para listar os icms dos produtos
        /// </summary>
        /// <returns></returns>
        private void ListaICMS(ref ProdutoVO tempProduto, int? CodProduto)
        {
            OpenCommand("pr_selecionar_produtoicms");
            try
            {

            if (CodProduto > 0)
                AddInParameter("CodProduto", DbType.Int32, CodProduto);

            IDataReader dr2 = ExecuteReader();

                try
                {
                    while (dr2.Read())
                    {
                        ICMSVO tempICMS = new ICMSVO();
                        tempICMS.CodProduto = GetReaderValue<int?>(dr2, "CodProduto");
                        tempICMS.CodTipoTributacao = GetReaderValue<string>(dr2, "CodTipoTributacao");
                        tempICMS.CodOrigem = GetReaderValue<int?>(dr2, "CodOrigem");
                        tempICMS.CodBaseCalculo = GetReaderValue<int?>(dr2, "CodBaseCalculo");
                        tempICMS.CodBaseCalculoICMSST = GetReaderValue<int?>(dr2, "CodBaseCalculoICMSST");
                        tempICMS.Aliquota = GetReaderValue<decimal?>(dr2, "Aliquota");
                        tempICMS.PercentualReducao = GetReaderValue<decimal?>(dr2, "PercentualReducao");
                        tempICMS.AliquotaST = GetReaderValue<decimal?>(dr2, "AliquotaST");
                        tempICMS.PercentualReducaoST = GetReaderValue<decimal?>(dr2, "PercentualReducaoST");
                        tempICMS.PercentualMargemST = GetReaderValue<decimal?>(dr2, "PercentualMargemST");
                        tempProduto.ICMS.Add(tempICMS);
                    }
                }
                finally
                {
                    dr2.Close();
                }
            }
            finally
            {
                CloseCommand();
            }

        }

        
        #endregion

        #region Método de Exclusão

        /// <summary>
        /// Método para excluir um produto
        /// </summary>
        /// <param name="identProduto">passar codproduto</param>
        public void Exclui(ProdutoVO identProduto)
        {
            OpenCommand("pr_excluir_produto");
            try
            {
                AddInParameter("CodProduto", DbType.Int32, identProduto.CodProduto);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        #region Métodos de Alteração
        /// <summary>
        /// Método para executar a proc pr_alterar_produto
        /// Objeto/Parametros: (identProduto, codUsuarioOperacao)
        /// Valore: identProduto.CodProduto,
        ///identProduto.Unidade.CodUnidade
        ///codUsuarioOperacao
        /// </summary>
        /// <param name="identProduto"></param>
        public void Altera(ProdutoVO identProduto)
        {
            OpenCommand("pr_alterar_produto");
            try
            {
                AddInParameter("CodProduto", DbType.Int32, identProduto.CodProduto);
                AddInParameter("Descricao", DbType.String, identProduto.Descricao);
                AddInParameter("CodUnidade", DbType.Int32, identProduto.Unidade.CodUnidade);
                AddInParameter("NCM", DbType.String, identProduto.NCM);
                AddInParameter("UsuarioAlt", DbType.Int32, 1);
                AddInParameter("Codigo", DbType.String, identProduto.Codigo);
                AddInParameter("xmlProdutoICMS", DbType.String, identProduto.ToXml());
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        #region Método de Inclusão
        /// <summary>
        /// Método para executar a proc pr_alterar_produto
        /// Objeto/Parametros: (identProduto, codUsuarioOperacao)
        /// Valore: identProduto.CodProduto,
        ///identProduto.Unidade.CodUnidade
        ///codUsuarioOperacao
        /// </summary>
        /// <param name="identProduto"></param>
        public int Inclui(ProdutoVO identProduto)
        {
            OpenCommand("pr_incluir_produto", true);
            try
            {
                int codProduto = int.MinValue;
                AddInParameter("Descricao", DbType.String, identProduto.Descricao);
                AddInParameter("CodUnidade", DbType.Int32, identProduto.Unidade.CodUnidade);
                AddInParameter("NCM", DbType.String, identProduto.NCM);
                AddInParameter("UsuarioInc", DbType.Int32, 1);
                AddInParameter("Codigo", DbType.String, identProduto.Codigo);
                AddInParameter("xmlProdutoICMS", DbType.String, identProduto.ToXml());
                
                ExecuteNonQuery();
                codProduto = GetReturnValue();
                return codProduto;
            }
            finally
            {
                CloseCommand();
            }
        }

        #endregion

    }
}
