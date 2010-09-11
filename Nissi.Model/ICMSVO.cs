using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nissi.Model
{
    [Serializable] //deve ser serializavel para armazenar em viewstate
    public class ICMSVO
    {
        #region Variaveis
        private int? codProduto;// int './CodProduto',
        private string codTipoTributacao; // char(2) './CodTipoTributacao',
        private int? codOrigem; // int './CodOrigem',
        private int? codBaseCalculo; // int './CodBaseCalculo',
        private int? codBaseCalculoICMSST; // int './CodBaseCalculoICMSST',
        private decimal? aliquota; // decimal './Aliquota',
        private decimal? percentualReducao; // decimal './PercentualReducao',
        private decimal? aliquotaST; // decimal './AliquotaST',
        private decimal? percentualReducaoST; // decimal './PercentualReducaoST',
        private decimal? percentualMargemST; // decimal './PercentualMargemST'
        #endregion

        #region Propriedades
        public int? CodProduto
        {
            get { return codProduto; }
            set { codProduto = value; }
        }

        public string CodTipoTributacao
        {
            get { return codTipoTributacao; }
            set { codTipoTributacao = value; }
        }

        public int? CodOrigem
        {
            get { return codOrigem; }
            set { codOrigem = value; }
        }
        
        public int? CodBaseCalculo
        {
            get { return codBaseCalculo; }
            set { codBaseCalculo = value; }
        }

        public int? CodBaseCalculoICMSST
        {
            get { return codBaseCalculoICMSST; }
            set { codBaseCalculoICMSST = value; }
        }

        public decimal? Aliquota
        {
            get { return aliquota; }
            set { aliquota = value; }
        }

        public decimal? PercentualReducao
        {
            get { return percentualReducao; }
            set { percentualReducao = value; }
        }

        public decimal? AliquotaST
        {
            get { return aliquotaST; }
            set { aliquotaST = value; }
        }

        public decimal? PercentualReducaoST
        {
            get { return percentualReducaoST; }
            set { percentualReducaoST = value; }
        }

        public decimal? PercentualMargemST
        {
            get { return percentualMargemST; }
            set { percentualMargemST = value; }
        }
        #endregion
    }

    public class TipoTributacao
    {
        public TipoTributacao(string codigoTributacao, string descricaoTributacao)
        {
            Codigo = codigoTributacao;
            Descricao = descricaoTributacao;
        }

        public string Codigo { get; private set; }
        public string Descricao { get; private set; }

        public static List<TipoTributacao> GetListaTipoTributacao()
        {
            List<TipoTributacao> listaTipoTributacao = new List<TipoTributacao>();
            listaTipoTributacao.Add(new TipoTributacao("00", "ICMS 00 - Tributada Integramente"));
            listaTipoTributacao.Add(new TipoTributacao("10", "ICMS 10 - Tributada com cobrança do ICMS por Substituição Tributaria"));
            listaTipoTributacao.Add(new TipoTributacao("20", "ICMS 20 - Com Redução da Base de Calculo"));
            listaTipoTributacao.Add(new TipoTributacao("30", "ICMS 30 - Isenta ou não tributada e com cobrança de ICMS por Substituição"));
            listaTipoTributacao.Add(new TipoTributacao("40", "ICMS 40 - Isenta"));
            listaTipoTributacao.Add(new TipoTributacao("41", "ICMS 41 - Não Tributada"));
            listaTipoTributacao.Add(new TipoTributacao("50", "ICMS 50 - Suspensão"));
            listaTipoTributacao.Add(new TipoTributacao("51", "ICMS 51 - Diferimento"));
            listaTipoTributacao.Add(new TipoTributacao("60", "ICMS 60 - Cobrado anteriormente por substituição tributária"));
            listaTipoTributacao.Add(new TipoTributacao("70", "ICMS 70 - Com redução da base e cálculo e cobrança do ICMS por substituição tributária"));
            listaTipoTributacao.Add(new TipoTributacao("90", "ICMS 90 - Outras"));

            return listaTipoTributacao;
        }
    }

    public class OrigemMercadoria
    {
        public OrigemMercadoria(int codigoOrigemMercadoria, string descricaoOrigemMercadoria)
        {
            Codigo = codigoOrigemMercadoria;
            Descricao = descricaoOrigemMercadoria;
        }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        public static List<OrigemMercadoria> GetListaOrigemMercadoria()
        {
            List<OrigemMercadoria> listaOrigemMercadoria = new List<OrigemMercadoria>();
            listaOrigemMercadoria.Add(new OrigemMercadoria(0, "Nacional"));
            listaOrigemMercadoria.Add(new OrigemMercadoria(1, "Estrangeira – Importação direta"));
            listaOrigemMercadoria.Add(new OrigemMercadoria(2, "Estrangeira – Adquirida no mercado interno"));
            return listaOrigemMercadoria;
        }
    }

    public class ModalidadeBaseCalculoICMS
    {
        public ModalidadeBaseCalculoICMS(int codigoModalidadeBaseCalculoICMS, string descricaoModalidadeBaseCalculoICMS)
        {
            Codigo = codigoModalidadeBaseCalculoICMS;
            Descricao = descricaoModalidadeBaseCalculoICMS;
        }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        public static List<ModalidadeBaseCalculoICMS> GetListaModalidadeBaseCalculoICMS()
        {
            List<ModalidadeBaseCalculoICMS> listaModalidadeBaseCalculoICMS = new List<ModalidadeBaseCalculoICMS>();
            listaModalidadeBaseCalculoICMS.Add(new ModalidadeBaseCalculoICMS(-1, ""));
            listaModalidadeBaseCalculoICMS.Add(new ModalidadeBaseCalculoICMS(0, "Margem Valor Agregado (%)"));
            listaModalidadeBaseCalculoICMS.Add(new ModalidadeBaseCalculoICMS(1, "Pauta (Valor)"));
            listaModalidadeBaseCalculoICMS.Add(new ModalidadeBaseCalculoICMS(2, "Preço Tabelado Máx. (valor)"));
            listaModalidadeBaseCalculoICMS.Add(new ModalidadeBaseCalculoICMS(3, "Valor da Operação"));
            return listaModalidadeBaseCalculoICMS;
        }
    }

    public class ModalidadeBaseCalculoICMSST
    {
        public ModalidadeBaseCalculoICMSST(int codigoModalidadeBaseCalculoICMSST, string descricaoModalidadeBaseCalculoICMSST)
        {
            Codigo = codigoModalidadeBaseCalculoICMSST;
            Descricao = descricaoModalidadeBaseCalculoICMSST;
        }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        public static List<ModalidadeBaseCalculoICMSST> GetListaModalidadeBaseCalculoICMSST()
        {
            List<ModalidadeBaseCalculoICMSST> listaModalidadeBaseCalculoICMSST = new List<ModalidadeBaseCalculoICMSST>();
            listaModalidadeBaseCalculoICMSST.Add(new ModalidadeBaseCalculoICMSST(-1, ""));
            listaModalidadeBaseCalculoICMSST.Add(new ModalidadeBaseCalculoICMSST(0, "Preço tabelado ou máximo sugerido"));
            listaModalidadeBaseCalculoICMSST.Add(new ModalidadeBaseCalculoICMSST(1, "Lista Negativa (valor)"));
            listaModalidadeBaseCalculoICMSST.Add(new ModalidadeBaseCalculoICMSST(2, "Lista Positiva (valor)"));
            listaModalidadeBaseCalculoICMSST.Add(new ModalidadeBaseCalculoICMSST(3, "Lista Neutra (valor)"));
            listaModalidadeBaseCalculoICMSST.Add(new ModalidadeBaseCalculoICMSST(4, "Margem Valor Agregado (%)"));
            listaModalidadeBaseCalculoICMSST.Add(new ModalidadeBaseCalculoICMSST(5, "Pauta (valor)"));
            return listaModalidadeBaseCalculoICMSST;
        }
    }

}
