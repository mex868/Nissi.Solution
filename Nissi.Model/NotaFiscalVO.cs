using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nissi.Model
{

        /// <summary>
        /// Essa classe representa os campos da tabela NotaFiscal
        /// </summary>
        [Serializable()]
        [XmlRoot(ElementName="NotaFiscal")]
        public class NotaFiscalVO : NissiBaseVO
        {
            #region Campos
            private int? _codNF = null;
            private int? _nF = null;
            private EmitenteVO _emitente = null;
            private ClienteVO _cliente = null;
            private bool? _indTipo = null;
            private FuncionarioVO _funcionario = null;
            private int? _codBanco = null;
            private TransportadoraVO _transportadora;
            private CFOPVO _cFOP = null;
            private string _naturezaOperacao;
            private int? _codPed = null;
            private MensagemNFVO _mensagemNF = null;
            private DateTime? _dataEmissao = null;
            private DateTime? _dataEntradaSaida = null;
            private string _numeroPedido = String.Empty;
            private DateTime? _hora = null;
            private bool? _indEntradaSaida = null;
            private string _iESUBTRI = String.Empty;
            private decimal? _bASCALICMSSUB = 0;
            private bool? _indBaixa = null;
            private decimal? _valorFrete = 0;
            private decimal? _valorSeguro = 0;
            private decimal? _outDespAce = 0;
            private bool? _indFretePorConta = null;
            private string _placaVeiculo = String.Empty;
            private string _uF = String.Empty;
            private string _especie = String.Empty;
            private string _marca = String.Empty;
            private string _numero = string.Empty;
            private decimal? _pesoBruto = null;
            private string _pesoLiquido = String.Empty;
            private string _semPedido = String.Empty;
            private string _observacao = String.Empty;
            private string _observacao2 = String.Empty;
            private bool? _indMovimento = null;
            private bool? _indVendaBeneficiamento = null;
            private int? _usuarioInc = null;
            private int? _usuarioAlt = null;
            private bool? _indFaturamento = null;
            private int? _codCFOP2 = null;
            private bool? _indVendaFaturamento = null;
            private string _vendedor = String.Empty;
            private DateTime? _dataCadastro;
            private DateTime? _dataAlteracao;
            private List<ItemNotaFiscalVO> _itens;
            private List<DuplicataVO> _duplicatas;
            private short _status;
            private decimal? _valTotalIpi = 0;
            private decimal? _calcFrete = 0;
            private decimal? _valTotalImcs = 0;
            private decimal? _valTotalProduto = 0;
            private decimal? _baseCalcIcms = 0;
            private string _qtdTotal = string.Empty;
            private decimal? _valTotalNota = 0;
            private decimal? _valTotalDesc = 0;
            private string _qtdVolumes = null;
            private decimal? _icms = 0;
            private string _serie = String.Empty;
            private string _indFinalidadeNF = String.Empty;
            private string _refNFe = String.Empty;

            private NfeVO _nfe = null;

            #endregion
            #region Propriedades
            public int? CodNF
            {
                get { return _codNF; }
                set { _codNF = value; }
            }

            public int? NF
            {
                get { return _nF; }
                set { _nF = value; }
            }
            [XmlElement(ElementName="Emitente")]
            public EmitenteVO Emitente
            {
                get {
                    if (_emitente == null)
                        _emitente = new EmitenteVO();
                    return _emitente; }
                set { _emitente = value; }
            }
            [XmlElement(ElementName="Cliente")]
            public ClienteVO Cliente
            {
                get {
                    if (_cliente == null)
                        _cliente = new ClienteVO();
                    return _cliente; }
                set { _cliente = value; }
            }

            public bool? IndTipo
            {
                get { return _indTipo; }
                set { _indTipo = value; }
            }
            [XmlElement(ElementName="Funcionario")]
            public FuncionarioVO Funcionario
            {
                get
                {
                    if (_funcionario == null)
                        _funcionario = new FuncionarioVO();
                        return _funcionario; }
                set { _funcionario = value; }
            }

            public int? CodBanco
            {
                get { return _codBanco; }
                set { _codBanco = value; }
            }

            [XmlElement(ElementName="Transportadora")]
            public TransportadoraVO Transportadora
            {
                get
                {
                    if (_transportadora == null)
                        _transportadora = new TransportadoraVO();
                        return _transportadora; }
                set { _transportadora = value; }
            }

            public CFOPVO CFOP
            {
                get
                {
                    if (_cFOP == null)
                        _cFOP = new CFOPVO();
                    return _cFOP; }
                set { _cFOP = value; }
            }

            public string NaturezaOperacao
            {
                get { return _naturezaOperacao; }
                set { _naturezaOperacao = value; }
            }

            public int? CodPed
            {
                get { return _codPed; }
                set { _codPed = value; }
            }

            public MensagemNFVO MensagemNF
            {
                get
                {
                    if (_mensagemNF == null)
                        _mensagemNF = new MensagemNFVO();
                        return _mensagemNF; }
                set { _mensagemNF = value; }
            }

            public DateTime? DataEmissao
            {
                get { return _dataEmissao; }
                set { _dataEmissao = value; }
            }

            public DateTime? DataEntradaSaida
            {
                get { return _dataEntradaSaida; }
                set { _dataEntradaSaida = value; }
            }

            public string NumeroPedido
            {
                get { return _numeroPedido; }
                set { _numeroPedido = value; }
            }

            public DateTime? Hora
            {
                get { return _hora; }
                set { _hora = value; }
            }

            public bool? IndEntradaSaida
            {
                get { return _indEntradaSaida; }
                set { _indEntradaSaida = value; }
            }

            public string IESUBTRI
            {
                get { return _iESUBTRI; }
                set { _iESUBTRI = value; }
            }

            public decimal? BASCALICMSSUB
            {
                get { return _bASCALICMSSUB; }
                set { _bASCALICMSSUB = value; }
            }

            public bool? IndBaixa
            {
                get { return _indBaixa; }
                set { _indBaixa = value; }
            }

            public decimal? ValorFrete
            {
                get { return _valorFrete; }
                set { _valorFrete = value; }
            }

            public decimal? ValorSeguro
            {
                get { return _valorSeguro; }
                set { _valorSeguro = value; }
            }

            public decimal? OutDespAce
            {
                get { return _outDespAce; }
                set { _outDespAce = value; }
            }

            public bool? IndFretePorConta
            {
                get { return _indFretePorConta; }
                set { _indFretePorConta = value; }
            }

            public string PlacaVeiculo
            {
                get { return _placaVeiculo; }
                set { _placaVeiculo = value; }
            }

            public string UF
            {
                get { return _uF; }
                set { _uF = value; }
            }

            public string Especie
            {
                get { return _especie; }
                set { _especie = value; }
            }

            public string Marca
            {
                get { return _marca; }
                set { _marca = value; }
            }

            public string Numero
            {
                get { return _numero; }
                set { _numero = value; }
            }

            public decimal? PesoBruto
            {
                get { return _pesoBruto; }
                set { _pesoBruto = value; }
            }

            public string QtdTotal
            {
                get { return _qtdTotal; }
                set { _qtdTotal = value; }
            }

            public string PesoLiquido
            {
                get { return _pesoLiquido; }
                set { _pesoLiquido = value; }
            }

            public string SemPedido
            {
                get { return _semPedido; }
                set { _semPedido = value; }
            }

            public string Observacao
            {
                get { return _observacao; }
                set { _observacao = value; }
            }

            public string Observacao2
            {
                get { return _observacao2; }
                set { _observacao2 = value; }
            }

            public bool? indMovimento
            {
                get { return _indMovimento; }
                set { _indMovimento = value; }
            }

            public bool? IndVendaBeneficiamento
            {
                get { return _indVendaBeneficiamento; }
                set { _indVendaBeneficiamento = value; }
            }

            public int? UsuarioInc
            {
                get { return _usuarioInc; }
                set { _usuarioInc = value; }
            }

            public int? UsuarioAlt
            {
                get { return _usuarioAlt; }
                set { _usuarioAlt = value; }
            }

            public bool? IndFaturamento
            {
                get { return _indFaturamento; }
                set { _indFaturamento = value; }
            }

            public int? CodCFOP2
            {
                get { return _codCFOP2; }
                set { _codCFOP2 = value; }
            }

            public bool? IndVendaFaturamento
            {
                get { return _indVendaFaturamento; }
                set { _indVendaFaturamento = value; }
            }

            public string Vendedor
            {
                get { return _vendedor; }
                set { _vendedor = value; }
            }
            public DateTime? DataCadastro
            {
                get { return _dataCadastro; }
                set { _dataCadastro = value; }
            }
            public DateTime? DataAlteracao
            {
                get { return _dataAlteracao; }
                set { _dataAlteracao = value; }
            }
            public short Status
            {
                get { return _status; }
                set { _status = value; }
            }

            [XmlElement(ElementName="ItemNotaFiscal")]
            public List<ItemNotaFiscalVO> Itens
            {
                get
                {
                    if (_itens == null)
                        _itens = new List<ItemNotaFiscalVO>();
                        return _itens; }
                set { _itens = value; }
            }

            [XmlElement(ElementName="Duplicata")]
            public List<DuplicataVO> Duplicatas
            {
                get
                {
                    if (_duplicatas == null)
                        _duplicatas = new List<DuplicataVO>();
                    return _duplicatas; }
                set { _duplicatas = value; }
            }
            public decimal? ValTotalIpi
            {
                get { return _valTotalIpi; }
                set { _valTotalIpi = value; }
            }
            public decimal? CalcFrete
            {
                get { return _calcFrete; }
                set { _calcFrete = value; }
            }
            public decimal? ValTotalImcs
            {
                get { return _valTotalImcs; }
                set { _valTotalImcs = value; }
            }
            public decimal? ValTotalProduto
            {
                get { return _valTotalProduto; }
                set { _valTotalProduto = value; }
            }
            public decimal? BaseCalcIcms
            {
                get { return _baseCalcIcms; }
                set { _baseCalcIcms = value; }
            }
            public decimal? ValTotalNota
            {
                get { return _valTotalNota; }
                set { _valTotalNota = value; }
            }
            public decimal? ValTotalDesc
            {
                get { return _valTotalDesc; }
                set { _valTotalDesc = value; }
            }
            public string QtdVolumes
            {
                get { return _qtdVolumes; }
                set { _qtdVolumes = value; }
            }

            public decimal? ICMS
            {
                get { return _icms; }
                set { _icms = value; }
            }

            public string Serie
            {
                get { return _serie; }
                set { _serie = value; }
            }

            public string IndFinalidadeNF
            {
                get { return _indFinalidadeNF; }
                set { _indFinalidadeNF = value; }
            }

            public string RefNFe
            {
                get { return _refNFe; }
                set { _refNFe = value; }
            }

            public NfeVO NFe
            {
                get
                {
                    if (_nfe == null)
                        _nfe = new NfeVO();
                        return _nfe; }
                set { _nfe = value; }
            }
            #endregion
        }
}
