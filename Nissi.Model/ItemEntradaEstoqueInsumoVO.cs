using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class ItemEntradaEstoqueInsumoVO
    {
 #region Campos

        public ItemEntradaEstoqueInsumoVO()
        {
            CodItemEntradaEstoqueInsumo = 0;
            EntradaEstoqueVO = new EntradaEstoqueVO();
            ProdutoInsumoVo = new ProdutoInsumoVO();
            UnidadeVo = new UnidadeVO();
            Valor = 0;
            Qtd = 0;
            Ipi = 0;
            Lote = 0;
            DataCadastro = DateTime.Now;
            UsuarioInc = 1;
            DataAlteracao = DateTime.Now;
            UsuarioAlt = 1;
            Certificado = string.Empty;
            Corrida = string.Empty;
        }
        

        #endregion
        #region Propriedades
        public int Lote { get; set; }

        public decimal? ResistenciaTracao { get; set; }

        public string Especificacao { get; set; }

        public UnidadeVO UnidadeVo{get;set;}

        public int CodItemEntradaEstoqueInsumo { get; set; }

        public EntradaEstoqueVO EntradaEstoqueVO { get; set; }

        public ProdutoInsumoVO ProdutoInsumoVo { get; set; }

        public string Certificado { get; set; }

        public string Corrida { get; set; }

        public decimal? QtdPedidoCompra { get; set; }

        public decimal Qtd { get; set; }

        public decimal Valor { get; set; }

        public decimal? Ipi { get; set; }

        public string Observacao{ get; set; }

        public byte[] CertificadoScanneado { get; set; }

        public decimal? Nota { get; set; }

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
            return "ItemEntradaEstoqueVO: " + CodItemEntradaEstoqueInsumo;
        }
        #endregion
    }
 
}
