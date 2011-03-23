using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class ItemEntradaEstoqueVO
    {
        public ItemEntradaEstoqueVO()
        {
            MateriaPrimaVo = new MateriaPrimaVO();
            BitolaVo = new BitolaVO();
            EntradaEstoqueVo = new EntradaEstoqueVO();
            UnidadeVo = new UnidadeVO();
            CertificadoScanneado = new byte[0];
            DataCadastro = DateTime.Now;
            DataAlteracao = DateTime.Now;
            CodUsuarioAlt = 1;
            CodUsuarioInc = 1;
            Ipi = 0;
            Lote = 0;
        }
        public int CodItemEntradaEstoque { get; set; }
        public int CodEntradaEstoque { get { return EntradaEstoqueVo.CodEntradaEstoque; } }
        public int Lote { get; set; }
        public MateriaPrimaVO MateriaPrimaVo { get; set; }
        public BitolaVO BitolaVo { get; set; }
        public EntradaEstoqueVO EntradaEstoqueVo { get; set; }
        public UnidadeVO UnidadeVo { get; set; }
        public string Certificado { get; set; }
        public string Corrida { get; set; }
        public string Fornecedor
        {
            get { return EntradaEstoqueVo.Fornecedor.RazaoSocial;}
        }
        public string Descricao
        {
            get { return MateriaPrimaVo.Descricao; }
        }
        public string Unidade
        {
            get { return UnidadeVo.TipoUnidade; }
        }
        public DateTime Entregue
        {
            get { return EntradaEstoqueVo.DataEntrada; }
        }
        public decimal? QtdPedidoCompra { get; set; }
        public decimal Qtd { get; set; }
        public decimal Valor { get; set; }
        public string Observacao { get; set; }
        public decimal? C { get; set; }
        public decimal? Si { get; set; }
        public decimal? Mn { get; set; }
        public decimal? P { get; set; }
        public decimal? S { get; set; }
        public decimal? Cr { get; set; }
        public decimal? Ni { get; set; }
        public decimal? Mo { get; set; }
        public decimal? Cu { get; set; }
        public decimal? Ti { get; set; }
        public decimal? N2 { get; set; }
        public decimal? Al { get; set; }
        public decimal? Co { get; set; }
        public decimal? Resistencia { get; set; }
        public decimal? Dureza { get; set; }
        public decimal? Nota { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CodUsuarioInc { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int CodUsuarioAlt { get; set; }
        public byte[] CertificadoScanneado { get; set; }
        public decimal? Ipi { get; set; }
        public string Especificacao { get; set; }
        public int Tipo { get { return (int) EntradaEstoqueVo.Tipo; } }
        public int CodPessoa { get { return EntradaEstoqueVo.Fornecedor.CodPessoa; } }
        public decimal CalcIpi
        {
            get
            {
                Ipi = Ipi == null ? 0 : Ipi.Value;
                return (decimal)((Ipi * Valor / 100) * Qtd);
            }
        }

        public decimal CalcTotalItem { get { return Qtd * Valor + CalcIpi; } }
    }
}
