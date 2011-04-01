using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela PedidoCompra
    /// </summary>
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class PedidoCompraVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

        public PedidoCompraVO()
        {
            CodPedidoCompra = 0;
            Fornecedor = new FornecedorVO();
            DataEmissao = DateTime.Now;
            PrazoEntrega = new PrazoEntregaVO();
            CondicaoFornecimento = String.Empty;
            Observacao = String.Empty;
            FormaPgto = new FormaPgtoVO();
            TipoRetirada = 0;
            FuncionarioAprovador = new FuncionarioVO();
            FuncionarioComprador = new FuncionarioVO();
            DataCadastro = null;
            DataPrazoEntrega = null;
            UsuarioInc = null;
            DataAlteracao = null;
            UsuarioAlt = null;
            ItemPedidoCompraVo = new List<ItemPedidoCompraVO>();
        }

        public DateTime? DataPrazoEntrega{ get; set; }

        #endregion
        #region Propriedades

        public int CodPedidoCompra { get; set; }

        public FornecedorVO Fornecedor { get; set; }

        public DateTime DataEmissao { get; set; }

        public string CondicaoFornecimento { get; set; }

        public string Observacao { get; set; }

        public PrazoEntregaVO PrazoEntrega { get; set; }

        public FormaPgtoVO FormaPgto { get; set; }

        public short TipoRetirada { get; set; }

        public FuncionarioVO FuncionarioComprador { get; set; }

        public FuncionarioVO FuncionarioAprovador { get; set; }

        public DateTime? DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }
        public TypePedido Tipo { get; set; }
        public int? UsuarioAlt { get; set; }
        public List<ItemPedidoCompraVO> ItemPedidoCompraVo { get; set; }

        #endregion

        #region ToString()
        public override string ToString()
        {
            return "PedidoCompraVO: " + CodPedidoCompra.ToString();
        }
        #endregion
    }
}


// ------------------------------------------------------------------------- // 

