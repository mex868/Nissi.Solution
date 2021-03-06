﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class ProdutoNFData: NissiBaseData
    {
        /// <summary>
        /// Método para executar a proc pr_relatorio_produtonf 
        /// </summary>
        public List<ProdutoNFVO> Lista(string codigo, string Op, DateTime? dataEmissaoIni, DateTime? dataEmissaoFim)
        {
            OpenCommand("pr_relatorio_produtonf");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@Codigo", DbType.String, codigo);
                AddInParameter("@OP", DbType.String, Op);
                AddInParameter("@DataEmissaoIni", DbType.DateTime, dataEmissaoIni);
                AddInParameter("@DataEmissaoFim", DbType.DateTime, dataEmissaoFim);

                List<ProdutoNFVO> lstProdutonfVO = new List<ProdutoNFVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        ProdutoNFVO produtonfVO = new ProdutoNFVO();

                        produtonfVO.CodNF = GetReaderValue<int?>(dr, "CodNF");
                        produtonfVO.NF = GetReaderValue<int?>(dr, "NF");
                        produtonfVO.DataEmissao = GetReaderValue<DateTime?>(dr, "DataEmissao");
                        produtonfVO.Qtd = GetReaderValue<decimal?>(dr, "Qtd");
                        produtonfVO.Valor = GetReaderValue<decimal?>(dr, "Valor");
                        produtonfVO.TotalItem = GetReaderValue<decimal?>(dr, "TotalItem");
                        produtonfVO.Descricao = GetReaderValue<string>(dr, "Descricao");
                        produtonfVO.Codigo = GetReaderValue<string>(dr, "Codigo");
                        produtonfVO.CodPedido = GetReaderValue<int?>(dr, "CodPedido");
                        produtonfVO.IndStatus = GetReaderValue<string>(dr, "IndStatus");
                        produtonfVO.CodPedidoCliente = GetReaderValue<string>(dr, "CodPedidoCliente");
                        produtonfVO.OP = GetReaderValue<string>(dr, "OP");
                        lstProdutonfVO.Add(produtonfVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstProdutonfVO;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 


    }
}
