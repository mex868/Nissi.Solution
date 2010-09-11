using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nissi.Model
{
        /// <summary>
        /// Essa classe representa os campos da tabela ItemNotaFiscal
        /// </summary>
        [Serializable()] //deve ser serializavel para armazenar em viewstate
        [XmlRoot(ElementName = "ItemNotaFiscal")]
        public class ItemNotaFiscalVO 
        {
            #region Campos
            private int? _codItemNotaFiscal = null;
            private int? _codNF = null;
            private ProdutoVO _produto = null;
            private ICMSVO _icms = null;
            private int? _codPedido = null;
            private string _codigo = String.Empty;
            private decimal? _qtd = null;
            private decimal? _valor = null;
            private decimal? _iCMS = null;
            private decimal? _iPI = null;
            private decimal? _rED = null;
            private decimal? _cOM = null;
            private decimal? _desconto = null;
            private decimal? _baseICMS = null;
            private string _observacao = String.Empty;
            private bool? _calcICMSSobIpi = null;
            private int? _usuarioInc = null;
            private DateTime? _dataCadastro = null;
            private int? _usuarioAlt = null;
            private DateTime? _dataAlteracao = null;
            private bool? _indMov = null;
            private string _unidade = String.Empty;
            #endregion
            #region Propriedades
            public int? CodItemNotaFiscal
            {
                get { return _codItemNotaFiscal; }
                set { _codItemNotaFiscal = value; }
            }

            public int? CodNF
            {
                get { return _codNF; }
                set { _codNF = value; }
            }
            [XmlElement(ElementName="Produto")]
            public ProdutoVO Produto
            {
                get { 
                    if (_produto == null) 
                        _produto = new ProdutoVO();
                    return _produto;
                    }
                set { _produto = value; }
            }
            [XmlElement(ElementName="ProdutoICMS")]
            public ICMSVO Icms
            {
                get { if (_icms == null) 
                        _icms = new ICMSVO();
                    return _icms; }
                set { _icms = value; }
            }

            public int? CodPedido
            {
                get { return _codPedido; }
                set { _codPedido = value; }
            }

            public string Codigo
            {
                get { return _codigo; }
                set { _codigo = value; }
            }

            public decimal? Qtd
            {
                get { return _qtd; }
                set { _qtd = value; }
            }

            public decimal? Valor
            {
                get { return _valor; }
                set { _valor = value; }
            }

            public decimal? ICMS
            {
                get { return _iCMS; }
                set { _iCMS = value; }
            }

            public decimal? IPI
            {
                get { return _iPI; }
                set { _iPI = value; }
            }

            public decimal? RED
            {
                get { return _rED; }
                set { _rED = value; }
            }

            public decimal? COM
            {
                get { return _cOM; }
                set { _cOM = value; }
            }

            public decimal? Desconto
            {
                get { return _desconto; }
                set { _desconto = value; }
            }

            public decimal? BaseICMS
            {
                get { return _baseICMS; }
                set { _baseICMS = value; }
            }

            public string Observacao
            {
                get { return _observacao; }
                set { _observacao = value; }
            }

            public bool? CalcICMSSobIpi
            {
                get { return _calcICMSSobIpi; }
                set { _calcICMSSobIpi = value; }
            }

            public int? UsuarioInc
            {
                get { return _usuarioInc; }
                set { _usuarioInc = value; }
            }

            public DateTime? DataCadastro
            {
                get { return _dataCadastro; }
                set { _dataCadastro = value; }
            }

            public int? UsuarioAlt
            {
                get { return _usuarioAlt; }
                set { _usuarioAlt = value; }
            }

            public DateTime? DataAlteracao
            {
                get { return _dataAlteracao; }
                set { _dataAlteracao = value; }
            }

            public bool? indMov
            {
                get { return _indMov; }
                set { _indMov = value; }
            }

            public string Unidade
            {
                get { return _unidade; }
                set { _unidade = value; }
            }
            public decimal? TotalItem
            {
                get { return Qtd*Valor; }
            }
            public decimal? CalcIcms
            {
                get { return (ICMS * Valor / 100)*Qtd; }
            }
            public decimal? CalcIPI
            {
                get { return (IPI * Valor / 100)*IPI; }
            }
            public decimal? CalcIcmsSobIpiValor
            {
                get { return Valor + (IPI * Valor / 100) * (ICMS / 100); }
            }
            #endregion


            /// <summary>
            /// Retorna um texto que identifique essa instância da classe
            /// </summary>
            #region ToString()
            public override string ToString()
            {
                return "ItemNotaFiscalVO: " + CodItemNotaFiscal.ToString();
            }
            #endregion
        }

}
