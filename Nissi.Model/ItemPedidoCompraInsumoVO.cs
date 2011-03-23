using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela ItemPedidoCompra
    /// </summary>
// ReSharper disable InconsistentNaming
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class ItemPedidoCompraInsumoVO
// ReSharper restore InconsistentNaming
    {
        #region Campos

        public ItemPedidoCompraInsumoVO()
        {
            CodItemPedidoCompraInsumo = 0;
            PedidoCompraVo = new PedidoCompraVO();
            ProdutoInsumoVo = new ProdutoInsumoVO();
            UnidadeVo = new UnidadeVO();
            Valor = 0;
            Qtd = 0;
            Ipi = 0;
            DataCadastro = DateTime.Now;
            UsuarioInc = 1;
            DataAlteracao = DateTime.Now;
            UsuarioAlt = 1;
        }
        

        #endregion
        #region Propriedades

        public decimal? ResistenciaTracao { get; set; }
        public string Especificacao { get; set; }
        public UnidadeVO UnidadeVo{get;set;}

        public int CodItemPedidoCompraInsumo { get; set; }

        public PedidoCompraVO PedidoCompraVo { get; set; }

        public ProdutoInsumoVO ProdutoInsumoVo { get; set; }

        public decimal Qtd { get; set; }

        public decimal Valor { get; set; }

        public decimal? Ipi { get; set; }

        public DateTime DataCadastro { get; set; }

        public int UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }

        public decimal CalcIpi
        {
            get
            {
                Ipi = Ipi == null? 0 : Ipi.Value;
                return (decimal) ((Ipi * Valor / 100) * Qtd);
            }
        }

        public decimal CalcTotalItem { get { return Qtd*Valor+CalcIpi; } }
        #endregion

        #region ToString()
        public override string ToString()
        {
            return "ItemPedidoCompraVO: " + CodItemPedidoCompraInsumo;
        }
        #endregion
    }
}


// ------------------------------------------------------------------------- // 

