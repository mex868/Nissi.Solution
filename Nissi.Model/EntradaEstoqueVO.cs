using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable()]
    public class EntradaEstoqueVO
    {
        public EntradaEstoqueVO()
        {
            CodEntradaEstoque = 0;
            Fornecedor = new FornecedorVO();
            PedidoCompra = new PedidoCompraVO();
            DataEmissao = DateTime.Now;
            DataEntrada = DateTime.Now;
            DataCadastro = DateTime.Now;
            CodUsuarioInc = 1;

        }
        public int CodEntradaEstoque{get; set;}
        public FornecedorVO Fornecedor { get; set; }
        public PedidoCompraVO PedidoCompra { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataNotaFiscal { get; set; }
        public string NotaFiscal { get; set; }
        public List<ItemEntradaEstoqueVO> Itens { get; set; }
        public int CodUsuarioInc { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CodUsuarioAlt { get; set; }
        public DateTime DataAlteracao { get; set; }
        public TypePedido Tipo { get; set; }
       
    }
}
