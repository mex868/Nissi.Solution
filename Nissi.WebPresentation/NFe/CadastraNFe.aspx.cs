using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using Nissi.Model;
using Nissi.Business;
using Nissi.Util;
using System.Globalization;

public partial class CadastrarNFe : BasePage
{
    #region Propriedades
    private ClienteVO DadosRazaoSocial
    {
        set {
            if (value.CodPessoa > 0)
            {
                value.Cep = PreencherCamposCEP(value.Cep);
                hdfCodigoCliente.Value = value.CodRef;
                hdfIdRazaoSocial.Value = value.CodPessoa.ToString();
                txtRazaoSocial.Text = value.RazaoSocial;
                txtCEP.Text = value.Cep.CodCep;
                txtCNPJ.Text = value.CNPJ;
                txtInscricaoEstatual.Text = value.InscricaoEstadual;
                txtEndereco.Text = value.Cep.NomEndereco +
                                                    (value.Numero.Trim().Equals("") ? "" : ", " + value.Numero) +
                                                    (value.Complemento.Trim().Equals("") ? "" : " - " + value.Complemento);
                txtBairro.Text = value.Cep.Bairro.NomBairro;
                txtMunicipio.Text = value.Cep.Cidade.NomCidade;
                txtUF.Text = value.Cep.Cidade.UF.CodUF;
                txtFoneFax.Text = value.Telefone + (value.Fax.Trim().Equals("") ? " " : " / " + value.Fax);
                txtEnderecoCobranca.Text = value.EnderecoCobranca;
                txtCepCobranca.Text = value.CepCobranca;
            }
        }
        get
        {
            ClienteVO clienteVO = new ClienteVO();
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                clienteVO.CodPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
            return clienteVO;
        }
    }

    private TransportadoraVO DadosClienteTransportadora
    {
        set
        {
            if (value.CodPessoa > 0)
            {
                value.Cep = PreencherCamposCEP(value.Cep);
                txtTransportadora.Text = value.RazaoSocial;
                txtCNPJTransportadora.Text = value.CNPJ;
                txtEnderecoTransportadora.Text = value.Cep.NomEndereco +
                                            (value.Numero.Trim().Equals("") ? "" : ", " + value.Numero) +
                                            (value.Complemento.Trim().Equals("") ? "" : " - " + value.Complemento);
                txtMunicipioTransportadora.Text = value.Cep.Cidade.NomCidade;
                txtBairroTransportadora.Text = value.Cep.Bairro.NomBairro;
                ddlUFTransportadora1.SelectedValue = value.Cep.Cidade.UF.CodUF;
                txtInscricaoTransportadora.Text = value.InscricaoEstadual;
                hdnCodTransportadora.Value = value.CodTransportadora.ToString();
            }
        }
        get
        {
            TransportadoraVO transportadoraVO = new TransportadoraVO();
            if(!string.IsNullOrEmpty(hdnCodTransportadora.Value))
                transportadoraVO.CodTransportadora = Convert.ToInt32(hdnCodTransportadora.Value);

            return transportadoraVO;
        }
    }
    private EmitenteVO DadosEmitente
    {
        set
        {
            hdfCodEmitente.Value = value.CodEmitente.ToString();
        }
        get
        {
            EmitenteVO identEmitente = new EmitenteVO();
            if (!string.IsNullOrEmpty(hdfCodEmitente.Value))
                identEmitente.CodEmitente = Convert.ToInt32(hdfCodEmitente.Value);
            identEmitente = new Emitente().Listar(identEmitente)[0];
            return identEmitente;
        }
    }
    private CFOPVO DadosCFOP
    {
        set
        {
            hdfCodCFOP.Value = value.CodCFOP.ToString();
            txtCFOP.Text = value.CFOP;
            txtNaturezaOperacao.Text = value.NaturezaOperacao;
        }
        get
        {
            CFOPVO cfopVO = new CFOPVO();
            if (!string.IsNullOrEmpty(hdfCodCFOP.Value))
                cfopVO.CodCFOP = Convert.ToInt32(hdfCodCFOP.Value);

            return cfopVO;
        }
    }
    private NotaFiscalVO DadosNotaFiscal
    {
        set
        {
            hdfCodNF.Value = value.CodNF.ToString();
            hdfSerie.Value = value.Serie;
            txtSerie.Text = value.Serie;
            txtEmissao.Text = value.DataEmissao.Value.ToString("dd/MM/yyyy");
            if (value.DataEntradaSaida != null)
                txtSaida.Text = value.DataEntradaSaida.Value.ToString("dd/MM/yyyy");
            if (value.Hora != null)
                txtHora.Text = value.Hora.Value.ToString("hh:mm");
            ddlTipoDocumento.SelectedValue = (value.IndEntradaSaida == true ? "1" : "0");
            ddlFreteConta.SelectedValue = (value.IndFretePorConta == true ? "1" : "0");
            ddlFinalidadeEmissao.SelectedValue = value.IndFinalidadeNF;
            txtNF.Text = value.NF.ToString().PadLeft(8, '0');
            txtPlaca.Text = value.PlacaVeiculo;
            ddlUFTransportadora1.SelectedValue = value.UF;
            txtQuantidade.Text = value.QtdVolumes;
            txtEspecie.Text = value.Especie;
            txtMarca.Text = value.Marca;
            txtNumero.Text = value.Numero;
            txtBruto.Text = value.PesoBruto.ToString();
            txtLiquido.Text = value.PesoLiquido.ToString();
            txtObservacao.Text = value.Observacao;
            ddlMensagemNF.SelectedValue = value.MensagemNF.CodMensagemNF.ToString();
            txtValorFrete.Text = value.ValorFrete.ToString();
            txtValorSeguro.Text = value.ValorSeguro.ToString();
            txtOutrasDespesas.Text = value.OutDespAce.ToString();
            DadosCFOP = value.CFOP;
            DadosEmitente = value.Emitente;
            DadosRazaoSocial = value.Cliente;
            List<TransportadoraVO> lstTransportadora = new Cliente().ListarPorCliente(value.Cliente.CodPessoa);
            if (lstTransportadora.Count > 0)
            {
                Geral.CarregarDDL(ref ddlTransportadora, lstTransportadora.ToArray(), "CodPessoa", "RazaoSocial");
                if (value.Transportadora.CodTransportadora != null)
                    ddlTransportadora.SelectedValue = value.Transportadora.CodTransportadora.ToString();
            }
            DadosClienteTransportadora = value.Transportadora;
            grdFatura.DataSource = value.Duplicatas;
            grdFatura.DataBind();
            grdProduto.DataSource = value.Itens;
            grdProduto.DataBind();
        }
        get 
        {
            NotaFiscalVO identNotaFiscal = new NotaFiscalVO();
            ItemNotaFiscalVO[] lstitemNotaFiscalVO = (ItemNotaFiscalVO[])Session["lstItemNotaFiscal"];
            List<ItemNotaFiscalVO> newlstitemNotaFiscalVO = new List<ItemNotaFiscalVO>(lstitemNotaFiscalVO);
            DuplicataVO[] lstDuplicata = (DuplicataVO[])ViewState["lstDuplicata"];
            List<DuplicataVO> newlstDuplicata = new List<DuplicataVO>(lstDuplicata);
            if (!string.IsNullOrEmpty(hdfSerie.Value))
                identNotaFiscal.Serie = hdfSerie.Value;
            if (!string.IsNullOrEmpty(hdfCodNF.Value))
                identNotaFiscal.CodNF = Convert.ToInt32(hdfCodNF.Value);
            identNotaFiscal.NF = int.Parse(txtNF.Text); //Número da Nota Fiscal gerado no sistema conforme a série que foi escolhida
            identNotaFiscal.Emitente.CodEmitente = DadosEmitente.CodEmitente; //Pega do Cadastro de Emitente que já existe no sistema
            identNotaFiscal.Cliente.CodPessoa = DadosRazaoSocial.CodPessoa;
            identNotaFiscal.IndTipo = false;
            identNotaFiscal.Funcionario = DadosRazaoSocial.Funcionario; //Esse dado vem do cadastro de cliente no caso é o vendedor
            identNotaFiscal.CodBanco = 1; // Aqui no caso seria se o cliente tivesse cadastro de banco
            if (DadosClienteTransportadora.CodTransportadora != null)
                identNotaFiscal.Transportadora = DadosClienteTransportadora;
            identNotaFiscal.CFOP = DadosCFOP;
            identNotaFiscal.CodPed = null; //Este campo vai ser utilizado futuramente quando existir o Modúlo de Vendas
            identNotaFiscal.Itens = newlstitemNotaFiscalVO;
            identNotaFiscal.Duplicatas = newlstDuplicata;
            if (ddlMensagemNF.SelectedIndex > 0)
            {
                MensagemNFVO DadosMensagem = new MensagemNFVO();
                DadosMensagem.CodMensagemNF = Convert.ToInt32(ddlMensagemNF.SelectedValue);
                identNotaFiscal.MensagemNF = DadosMensagem;
            }
            if (!string.IsNullOrEmpty(txtEmissao.Text))
                identNotaFiscal.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
            if (!string.IsNullOrEmpty(txtSaida.Text))
                identNotaFiscal.DataEntradaSaida = Convert.ToDateTime(txtSaida.Text);
            identNotaFiscal.NumeroPedido = string.Empty; //Este campo vai ser utilizado futuramente quando existir o Modúlo de Vendas
            if (!string.IsNullOrEmpty(txtHora.Text) && txtHora.Text != "__:__")
                identNotaFiscal.Hora = Convert.ToDateTime(txtHora.Text);
            identNotaFiscal.IndEntradaSaida = (ddlTipoDocumento.SelectedValue =="1" ? true : false);
            identNotaFiscal.IESUBTRI = string.Empty;//Tem empresa que precisa utilizar essa campo
            identNotaFiscal.BASCALICMSSUB = null;//Tem empresa que precisa utilizar essa campo
            identNotaFiscal.IndBaixa = false; //Algumas empresas a baixa da nota fiscal é depois da impressão da mesma
            identNotaFiscal.ValorFrete = strToDecimal(txtValorFrete.Text);
            identNotaFiscal.ValorSeguro = strToDecimal(txtValorSeguro.Text);
            identNotaFiscal.OutDespAce = strToDecimal(txtOutrasDespesas.Text);
            identNotaFiscal.IndFretePorConta = (ddlFreteConta.SelectedValue == "1" ? true : false);
            identNotaFiscal.PlacaVeiculo = txtPlaca.Text;
            identNotaFiscal.UF = ddlUFTransportadora1.SelectedValue;
            identNotaFiscal.Especie = txtEspecie.Text;
            identNotaFiscal.Marca = txtMarca.Text;
            identNotaFiscal.Numero = txtNumero.Text;
            identNotaFiscal.PesoBruto = strToDecimal(txtBruto.Text);
            identNotaFiscal.QtdVolumes = txtQuantidade.Text;
            identNotaFiscal.PesoLiquido = txtLiquido.Text;
            identNotaFiscal.SemPedido = string.Empty; //Tem empresa que precisa utilizar esse campo 
            identNotaFiscal.Observacao = txtObservacao.Text;
            //identNotaFiscal.Observacao2 = txtObservacao.Text;
            identNotaFiscal.indMovimento = false;
            identNotaFiscal.IndVendaBeneficiamento = false;
            identNotaFiscal.UsuarioInc = UsuarioAtivo.CodFuncionario.Value;
            if (identNotaFiscal.Duplicatas.Count > 0)
            {
                var dup = identNotaFiscal.Duplicatas.SingleOrDefault(d => d.Dias == 0);
                if (dup != null)
                    identNotaFiscal.IndFaturamento = false;
                else
                    identNotaFiscal.IndFaturamento = true;
            }
            else
                identNotaFiscal.IndFaturamento = null;


            identNotaFiscal.CodCFOP2 = 1; //Tem empresa que precisa colocar 2 CFOPs na Nota Fiscal
            identNotaFiscal.IndVendaFaturamento = false;
            identNotaFiscal.IndFinalidadeNF = ddlFinalidadeEmissao.SelectedValue;
            //identNotaFiscal.Vendedor = DadosRazaoSocial.Funcionario.Nome; //aqui vem o nome do Vendedor 
            return identNotaFiscal; 
        }
    }
    #endregion

    private decimal strToDecimal(string valor)
    {
        decimal dec = 0;
        if (!string.IsNullOrEmpty(valor))
            dec = Convert.ToDecimal(valor);
        return dec;
    }

    #region Eventos
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarCombos();
            //cria a referência a variável que vai ser armazenada na Session
            List<ItemNotaFiscalVO> lstItemNotaFiscal = new List<ItemNotaFiscalVO>();
            //cria referência a variável que vai ser armazenada no ViewState
            List<DuplicataVO> lstDuplicata = new List<DuplicataVO>();
            //cria referência que vai resgatar o valor da Session
            NotaFiscalVO identNotaFiscal = new NotaFiscalVO();
            hdfTipoAcao.Value = Request.QueryString["acao"] != null?Request.QueryString["acao"].ToString():"";
            if (hdfTipoAcao.Value.Equals("Incluir"))
            {
                identNotaFiscal = new NotaFiscal().gerar_numero_nf();
                hdfSerie.Value = identNotaFiscal.Serie;
                txtSerie.Text = identNotaFiscal.Serie;
                txtNF.Text = identNotaFiscal.NF.ToString().PadLeft(8, '0');
                txtEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlFreteConta.SelectedValue = "1";
                ddlTipoDocumento.SelectedValue = "1";
                btnVoltar.Enabled = false;
            }
            else
            if (hdfTipoAcao.Value.Equals("Editar"))
            {
                identNotaFiscal.CodNF = Convert.ToInt32(Request.QueryString["CodNF"].ToString());
                identNotaFiscal = new NotaFiscal().ListarTudo(identNotaFiscal)[0];
                DadosNotaFiscal = identNotaFiscal;
                lstItemNotaFiscal = identNotaFiscal.Itens;
                lstDuplicata = identNotaFiscal.Duplicatas;
                btnVoltar.Enabled = true;
                if (identNotaFiscal.NFe.IndStatus != "0" && !string.IsNullOrEmpty(identNotaFiscal.NFe.IndStatus))
                    btnSalvar.Enabled = false;
            }
            btnIncluirProduto.Attributes.Add("onclick", "ChamaPopup();");
            //criar Session para armazenar valores do grid dos Itens da Nota Fiscal
            //este grid só salvará quando salvar a Nota Fiscal inteira
            Session.Add("lstItemNotaFiscal", lstItemNotaFiscal.ToArray());
            //criar ViewState para armazenar valores do grid das Duplicatas
            //este grid só salvará quando salvar a Nota Fiscal inteira
            ViewState.Add("lstDuplicata", lstDuplicata.ToArray());
            Master.PosicionarFoco(txtEmissao);
            hdfTipoAcaoFatura.Value = "Incluir";
		}
        CarregarValoresItemNF();
        ExecutarScript(new StringBuilder("OcultarBotaoCarregarValores();"));
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (grdProduto.Rows.Count > 0)
        {
            CalcularValores();
            int codnf = 0;
            if (hdfTipoAcao.Value.Equals("Incluir"))
            {
              codnf = new NotaFiscal().Incluir(DadosNotaFiscal, UsuarioAtivo.CodFuncionario.Value);
              txtNF.Text =
              txtNumeroFatura.Text = new NotaFiscal().ListarNumeroNf(codnf).ToString().PadLeft(8, '0');
              hdfCodNF.Value = codnf.ToString();
            }
            else
            {
                new NotaFiscal().Alterar(DadosNotaFiscal, UsuarioAtivo.CodFuncionario.Value);
                codnf = int.Parse(hdfCodNF.Value);
            }
            if (DadosNotaFiscal.Duplicatas.Count <= 0)
            {
                TabContainer1.ActiveTabIndex = 1;
                hdfTipoAcao.Value = "Editar";
            }
            else
                Response.Redirect("ListaNFe.aspx?CodNF=" + codnf.ToString());
        }
        else
            MensagemCliente("Não foi associado nenhum produto a Nota Fiscal!");
    }

    protected void btnCarregarValores_Click(object sender, EventArgs e)
    {
        ClienteVO identCliente = new ClienteVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identCliente.IndPessoaTipo = null;
        identCliente.CodPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
        DadosRazaoSocial = new Cliente().Listar(identCliente).First();
        
        //lista transportadoras do cliente
        List<TransportadoraVO> listaTransportadoraCliente = new List<TransportadoraVO>();
        listaTransportadoraCliente = new Cliente().ListarPorCliente(identCliente.CodPessoa);

        if (listaTransportadoraCliente.Count > 0)
        {
            Geral.CarregarDDL(ref ddlTransportadora, listaTransportadoraCliente.ToArray(), "CodPessoa", "RazaoSocial");
            
            //se retornar uma transportadora, carrega ítem na combo e preenche dados da transportadora
            if (listaTransportadoraCliente.Count == 1)
            {
                ddlTransportadora.SelectedIndex = 1;
                ddlTransportadora_SelectedIndexChanged(sender, e);
            }
        }
    }

    protected void ddlTransportadora_SelectedIndexChanged(object sender, EventArgs e)
    {
        TransportadoraVO identTransportadora = new TransportadoraVO();
        if (ddlTransportadora.SelectedIndex > 0)
        {
            identTransportadora.CodPessoa = Convert.ToInt32(ddlTransportadora.SelectedValue);
            DadosClienteTransportadora = new Transportadora().Listar(identTransportadora)[0];
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimparCampos();
        Response.Redirect("ListaNFe.aspx");
    }

    protected void btnIncluirFatura_Click(object sender, EventArgs e)
    {
        if (!txtNumeroFatura.Text.Equals("00000000"))
        {
            //será incluido no grid de ICMS manualmente (não incluirá no banco ainda)
            //pois só deverá ser incluido no banco quando salvar o produto

            DuplicataVO[] lstDuplicata = (DuplicataVO[])ViewState["lstDuplicata"];
            List<DuplicataVO> newlstDuplicata = new List<DuplicataVO>(lstDuplicata);
            decimal valor = decimal.Parse(txtValorLiquido.Text);

            //se for edição de ICMS, atualizar o list
            if (hdfTipoAcaoFatura.Value.Equals("Incluir"))
            {
                /************************************************************************
                Se a ação for inclusão, simplesmente verifica se o ítem já foi cadastrado
                se já for, exibe mensagem e não inclui o ítem
                /***********************************************************************/
                DuplicataVO result = newlstDuplicata.Find(
                delegate(DuplicataVO bk)
                {
                    return bk.Dias == int.Parse(txtDias.Text);
                }
                );
                if (result != null)
                {

                    MensagemCliente("Dias Já cadastrado!");
                    return;
                }
                /************************************************************************/
                /************************************************************************/

                //senão, incluir novo ítem no list
                DuplicataVO lstDuplicataAux = new DuplicataVO();
                if (!txtDias.Text.Trim().Equals(""))
                    lstDuplicataAux.Dias = int.Parse(txtDias.Text);
                lstDuplicataAux.Vencimento = DateTime.Now.Date.AddDays(Convert.ToDouble(lstDuplicataAux.Dias));
                newlstDuplicata.Add(lstDuplicataAux);
                int i = 1;
                foreach (DuplicataVO item in newlstDuplicata)
                {
                    if (!txtNumeroFatura.Text.Trim().Equals(""))
                        item.Numero = txtNumeroFatura.Text + "-" + Letra(i).ToString();
                    item.Valor = valor / newlstDuplicata.Count();
                    i++;
                }

            }
            else
            {
                int linha = Convert.ToInt32(ViewState["LinhaSelecionadaDuplicata"]);
                int iLinhaFor = 0;
                int i = 1;
                foreach (DuplicataVO item in newlstDuplicata)
                {
                    if (iLinhaFor == linha)
                    {
                        /************************************************************************
                        Se a ação for alteração, verifica se o ítem já está cadastrado, se já estiver
                        será impedido, desde que não seja ele mesmo
                        /***********************************************************************/
                        int iLinhaSelecionada = 0;
                        foreach (DuplicataVO item2 in newlstDuplicata)
                        {
                            if ((item2.Dias == int.Parse(txtDias.Text)) &&
                                (iLinhaSelecionada != linha))
                            {
                                MensagemCliente("Dias Já cadastrado!");
                                return;
                            }
                            iLinhaSelecionada++;
                        }
                        /************************************************************************
                        
                         /***********************************************************************/
                        item.Dias = int.Parse(txtDias.Text);
                        item.Vencimento = DateTime.Now.Date.AddDays(Convert.ToDouble(item.Dias));
                        //sai do for
                    }
                    if (!txtNumeroFatura.Text.Trim().Equals(""))
                        item.Numero = txtNumeroFatura.Text + "-" + Letra(i).ToString();
                    item.Valor = valor / newlstDuplicata.Count;
                    iLinhaFor++;
                    i++;
                }
            }
            grdFatura.DataSource = newlstDuplicata;
            grdFatura.DataBind();
            //atualiza viewstate
            ViewState["lstDuplicata"] = newlstDuplicata.ToArray();
            txtDias.Text = string.Empty;
            Master.PosicionarFoco(txtDias);
            hdfTipoAcaoFatura.Value = "Incluir";
            //LimparCamposICMS();
        }
        else
            MensagemCliente("O número da fatura não pode ser 00000000");
    }

    private string Letra(int numero)
    {
        string letra = string.Empty;
        switch (numero)
        {
            case 1: letra = "A";
                break;
            case 2: letra = "B";
                break;
            case 3: letra = "C";
                break;
            case 4: letra = "D";
                break;
            case 5: letra = "E";
                break;
            case 6: letra = "F";
                break;
            case 7: letra = "G";
                break;
            case 8: letra = "H";
                break;
            case 9: letra = "I";
                break;
            case 10: letra = "J";
                break;
            case 11: letra = "K";
                break;
            case 12: letra = "L";
                break;
            default:
                letra =
                string.Empty;
                break;
        }
        return letra;
    }

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        if (TabContainer1.ActiveTabIndex == 4) //aba Cálculo de Imposto
        {
            CalcularValores();
        }
        if (TabContainer1.ActiveTabIndex == 1) //aba Cálculo de Imposto
        {
            CalcularValores();
        }
    }

    protected void campo_TextChanged(object sender, EventArgs e)
    {
        CalcularValores();
    }
    #endregion

    #region Métodos Privados
    //Todo: Está função poderia ser feita no próprio ClienteData.cs
    //ver com Max
    private CEPVO PreencherCamposCEP(CEPVO lCEPVO)
    {
        if (!string.IsNullOrEmpty(lCEPVO.CodCep))
        {
            CEPVO lCEP = new CEP().Listar(lCEPVO);
            lCEPVO.Bairro = lCEP.Bairro;
            lCEPVO.Cidade = lCEP.Cidade;
            lCEPVO.NomEndereco = lCEP.NomEndereco;
            lCEPVO.TipoLogradouro = lCEP.TipoLogradouro;
        }

        return lCEPVO;
    }

    private void CarregarCombos()
    {
        Geral.CarregarDDL(ref ddlUFTransportadora1, new UnidadeFederacao().Listar().ToArray(), "CodUF", "NomUF");
        Geral.CarregarDDL(ref ddlUFTransportadora2, new UnidadeFederacao().Listar().ToArray(), "CodUF", "NomUF");
        Geral.CarregarDDL(ref ddlMensagemNF, new MensagemNF().Listar(new MensagemNFVO()).ToArray(), "CodMensagemNF", "Descricao");
    }

    private void LimparCampos()
    {
        Session.Remove("ItemNF");
        Session.Remove("AcaoProduto");
    }

    private void CarregarValoresItemNF()
    {
        if (Session["ItemNF"] != null)
        {
            ItemNotaFiscalVO[] lstitemNotaFiscalVO = (ItemNotaFiscalVO[])Session["lstItemNotaFiscal"];
            ItemNotaFiscalVO itemNotaFiscalVO = (ItemNotaFiscalVO)Session["ItemNF"];

            if (Session["AcaoProduto"].Equals("Incluir"))
            {
                //***********************************************************************************************
                //inclui o ítem da nf (Session["ItemNF"]) na list com itens (Session["lstItemNotaFiscal"])
                //***********************************************************************************************
                List<ItemNotaFiscalVO> newlstitemNotaFiscalVO = new List<ItemNotaFiscalVO>(lstitemNotaFiscalVO);
                newlstitemNotaFiscalVO.Add(itemNotaFiscalVO);
                Session.Add("lstItemNotaFiscal", newlstitemNotaFiscalVO.ToArray());
                //***********************************************************************************************

                //***********************************************************************************************
                //carrega valores nos campos
                //***********************************************************************************************
                grdProduto.DataSource = newlstitemNotaFiscalVO;
                grdProduto.DataBind();
                //***********************************************************************************************

            }
            else //ação Editar
            {
                //***********************************************************************************************
                //altera o ítem da list com itens (Session["lstItemNotaFiscal"]) com os valores do ítem da nf (Session["ItemNF"])
                //***********************************************************************************************
                var res = lstitemNotaFiscalVO.Single(t => t.Produto.CodProduto == itemNotaFiscalVO.Produto.CodProduto && t.CodItemNotaFiscal == itemNotaFiscalVO.CodItemNotaFiscal);
                res.Produto.Descricao = itemNotaFiscalVO.Produto.Descricao;
                res.Produto.Codigo = itemNotaFiscalVO.Produto.Codigo;
                res.CodItemNotaFiscal = itemNotaFiscalVO.CodItemNotaFiscal;
                res.Produto.Unidade.CodUnidade = itemNotaFiscalVO.Produto.Unidade.CodUnidade;
                res.Produto.NCM = itemNotaFiscalVO.Produto.NCM;

                res.Codigo = itemNotaFiscalVO.Codigo;
                res.Unidade = itemNotaFiscalVO.Unidade;
                res.Qtd = itemNotaFiscalVO.Qtd;
                res.Valor = itemNotaFiscalVO.Valor;
                res.Desconto = itemNotaFiscalVO.Desconto;
                res.ICMS = itemNotaFiscalVO.ICMS;
                res.BaseICMS = itemNotaFiscalVO.BaseICMS;
                res.IPI = itemNotaFiscalVO.IPI;
                res.Icms.CodTipoTributacao = itemNotaFiscalVO.Icms.CodTipoTributacao;
                res.CalcICMSSobIpi = itemNotaFiscalVO.CalcICMSSobIpi;
                res.Produto.ICMS = itemNotaFiscalVO.Produto.ICMS;
                res.CodPedidoCliente = itemNotaFiscalVO.CodPedidoCliente;
                res.OP = itemNotaFiscalVO.OP;
                //***********************************************************************************************

                Session["lstItemNotaFiscal"] = lstitemNotaFiscalVO;

                //***********************************************************************************************
                //carrega valores nos campos
                //***********************************************************************************************
                grdProduto.DataSource = lstitemNotaFiscalVO;
                grdProduto.DataBind();
                //***********************************************************************************************
            }

            Session.Remove("ItemNF");
            Session.Remove("AcaoProduto");
        }
    }

    private void CalcularValores()
    {
        NotaFiscalVO notaFiscalVO = DadosNotaFiscal;
        ItemNotaFiscalVO[] lstitemNotaFiscalVO = (ItemNotaFiscalVO[])Session["lstItemNotaFiscal"];
        List<ItemNotaFiscalVO> newlstitemNotaFiscalVO = new List<ItemNotaFiscalVO>(lstitemNotaFiscalVO);

        notaFiscalVO.Itens = newlstitemNotaFiscalVO;

        notaFiscalVO = NotaFiscal.CalcTotais(notaFiscalVO);
        txtNumeroFatura.Text = txtNF.Text;
        txtBaseCalculo.Text = notaFiscalVO.BaseCalcIcms.ToString(); //Base de Cálculo ICMS
        txtValorIcms.Text = notaFiscalVO.ValTotalImcs.ToString(); //Valor do ICMS
        txtTotalProduto.Text = notaFiscalVO.ValTotalProduto.ToString(); //Valor Total dos Produtos
        txtValorIPI.Text = notaFiscalVO.ValTotalIpi.ToString(); //Valor do IPI
        txtValorTotalNota.Text = notaFiscalVO.ValTotalNota.ToString(); //Valor Total da Nota
        txtValorTotalDesconto.Text = notaFiscalVO.ValTotalDesc.ToString(); //Valor do Desconto
        txtValorOriginal.Text = txtTotalProduto.Text;
        txtValorLiquido.Text = txtValorTotalNota.Text;
        //txtBaseCalculoSub.Text = "0"; //Base Cálc. ICMS Subst.
        //txtValorIcmsSub.Text = "0"; //Valor do ICMS Subst.
        //txtValorFrete.Text = ""; //Valor do Frete
        //txtValorSeguro.Text = ""; //Valor do Seguro
        //txtOutrasDespesas.Text = ""; //Outras Despesas

        //Campos do CalcTotais que não estão sendo utilizados
        //ICMS
        //QtdTotal
    }

    #endregion

    
    #region Métodos do Grid Produto
    protected void grdProduto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdProduto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ////forma de pegar o index do datagrid
        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        int linha = row.RowIndex;
        ////armazena em viewstate a linha selecionada para posterior atualização
        ViewState["LinhaSelecionadaItemNotaFiscal"] = linha;

        ItemNotaFiscalVO[] lstItemNotaFiscal = (ItemNotaFiscalVO[])Session["lstItemNotaFiscal"];
        List<ItemNotaFiscalVO> newlstItemNotaFiscal = new List<ItemNotaFiscalVO>(lstItemNotaFiscal);

        if (e.CommandName == "Editar")
        {
            //hdfTipoAcaoItemNF.Value = "Editar";

            //ExecutarScript(new StringBuilder("javascript:window.showModalDialog('" + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "NFe/CadastraItemNFe.aspx?CodProduto=" + e.CommandArgument + "','','dialogWidth:800px;dialogHeight:600px,scroll:yes,center:yes,status:no')"));
            

            ////busca no vo os valores para a linha selecionada
            //int iLinhaFor = 0;
            //foreach (ItemNotaFiscalVO item in newlstItemNotaFiscal)
            //{
            //    if (iLinhaFor == linha)
            //    {
            //        //atribui aos campos da tela para alteração
            //        Response.Redirect("CadastraItemNotaNFe.aspx");
            //        //sai do for
            //        break;
            //    }
            //    iLinhaFor++;
            //}
        }
        else if (e.CommandName == "Excluir")
        {
            newlstItemNotaFiscal.RemoveAt(linha);

            grdProduto.DataSource = newlstItemNotaFiscal;
            grdProduto.DataBind();
            updProduto.Update();
            //atualiza lstItemNotaFiscal
            Session["lstItemNotaFiscal"] = newlstItemNotaFiscal.ToArray();
        }

    }
    private decimal? TotalGeral = 0;
    protected void grdProduto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ItemNotaFiscalVO identItemNotaFiscal = (ItemNotaFiscalVO)e.Row.DataItem;
            e.Row.Cells[1].Text = identItemNotaFiscal.Produto.Codigo;
            if (!string.IsNullOrEmpty(identItemNotaFiscal.OP))
                e.Row.Cells[2].Text = identItemNotaFiscal.OP.Trim();
            string pedido = string.Empty;
            if (!string.IsNullOrEmpty(identItemNotaFiscal.CodPedidoCliente))
                pedido = " - Ped.: " + identItemNotaFiscal.CodPedidoCliente.Trim();

            e.Row.Cells[3].Text = identItemNotaFiscal.Produto.Descricao +pedido;
            e.Row.Cells[4].Text = identItemNotaFiscal.Produto.NCM;
            e.Row.Cells[5].Text = identItemNotaFiscal.Icms.CodTipoTributacao;
            e.Row.Cells[6].Text = identItemNotaFiscal.Produto.Unidade.TipoUnidade;
            e.Row.Cells[7].Text = identItemNotaFiscal.Qtd.ToString();
            e.Row.Cells[8].Text = identItemNotaFiscal.Valor.ToString();
            TotalGeral += identItemNotaFiscal.TotalItem;
            e.Row.Cells[9].Text = identItemNotaFiscal.TotalItem.ToString();
            e.Row.Cells[10].Text = identItemNotaFiscal.ICMS.ToString();
            e.Row.Cells[11].Text = identItemNotaFiscal.IPI.ToString();
            e.Row.Cells[12].Text = identItemNotaFiscal.CalcIPI.ToString();

            #region Botão Editar
            ImageButton imgEditarFatura = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditarFatura.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            //imgEditarFatura.CommandArgument = identItemNotaFiscal.CodItemNotaFiscal.ToString();
            imgEditarFatura.CommandArgument = identItemNotaFiscal.Produto.CodProduto.ToString();
            imgEditarFatura.CommandName = "Editar";
            imgEditarFatura.Style.Add("cursor", "hand");
            imgEditarFatura.ToolTip = "Editar dados do Produto ["+identItemNotaFiscal.Produto.Descricao+"]";
            imgEditarFatura.Attributes.Add("onclick", "javascript:window.showModalDialog('" + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/NFe/CadastraItemNFe.aspx?CodProduto=" + identItemNotaFiscal.Produto.CodProduto.ToString() + "&CodItemNotaFiscal="+identItemNotaFiscal.CodItemNotaFiscal+"&AcaoProduto=Editar','','dialogWidth:800px;dialogHeight:600px,scroll:yes,center:yes,status:no')");
            #endregion

            #region Botão Excluir
            ImageButton imgExcluirFatura = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluirFatura.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            //imgExcluirFatura.CommandArgument = identItemNotaFiscal.CodItemNotaFiscal.ToString();
            imgExcluirFatura.CommandArgument = identItemNotaFiscal.Produto.CodProduto.ToString();
            imgExcluirFatura.CommandName = "Excluir";
            imgExcluirFatura.Attributes["onclick"] = "return confirm('Confirmar exclusão do Produto ["+identItemNotaFiscal.Produto.Descricao+"]?');";
            imgExcluirFatura.Style.Add("cursor", "hand");
            imgExcluirFatura.ToolTip = "Excluir Duplicata";
            #endregion

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "Total";
            e.Row.Cells[9].Text = "dos";
            e.Row.Cells[10].Text = "Produtos:";
            //e.Row.Cells[9].Attributes.Add("align", "left");
            e.Row.Cells[12].Attributes.Add("align", "right");
            e.Row.Cells[12].Text = TotalGeral.ToString();
        }

    }
        
    #endregion
    #region Métodos do Grid Fatura
    protected void grdFatura_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Método adicionado para atender o componente
    }

    protected void grdFatura_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //forma de pegar o index do datagrid
        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        int linha = row.RowIndex;
        //armazena em viewstate a linha selecionada para posterior atualização
        ViewState["LinhaSelecionadaDuplicata"] = linha;

        DuplicataVO[] lstDuplicata = (DuplicataVO[])ViewState["lstDuplicata"];
        List<DuplicataVO> newlstDuplicata = new List<DuplicataVO>(lstDuplicata);

        if (e.CommandName == "Editar")
        {
            hdfTipoAcaoFatura.Value = "Editar";

            //busca no vo os valores para a linha selecionada
            int iLinhaFor = 0;
            foreach (DuplicataVO item in newlstDuplicata)
            {
                if (iLinhaFor == linha)
                {
                    //atribui aos campos da tela para alteração
                    txtDias.Text = item.Dias.ToString();
                    //sai do for
                    break;
                }
                iLinhaFor++;
            }
        }
        else if (e.CommandName == "Excluir")
        {
            newlstDuplicata.RemoveAt(linha);

            grdFatura.DataSource = newlstDuplicata;
            grdFatura.DataBind();
            updFatura.Update();
            //atualiza viewstate
            ViewState["lstDuplicata"] = newlstDuplicata.ToArray();
        }

    }

    protected void grdFatura_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DuplicataVO identDuplicata = (DuplicataVO)e.Row.DataItem;
            e.Row.Cells[1].Text = identDuplicata.Dias.ToString();
            e.Row.Cells[2].Text = identDuplicata.Vencimento.ToString();
            e.Row.Cells[3].Text = identDuplicata.Numero;

            e.Row.Cells[4].Text = identDuplicata.Valor.ToString();

            #region Botão Editar
            ImageButton imgEditarFatura = (ImageButton)e.Row.FindControl("imgEditarFatura");
            imgEditarFatura.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            imgEditarFatura.CommandArgument = identDuplicata.CodDuplicata.ToString();
            imgEditarFatura.CommandName = "Editar";
            imgEditarFatura.Style.Add("cursor", "hand");
            imgEditarFatura.ToolTip = "Editar dados da Duplicata";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluirFatura = (ImageButton)e.Row.FindControl("imgExcluirFatura");
            imgExcluirFatura.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            imgExcluirFatura.CommandArgument = identDuplicata.CodDuplicata.ToString();
            imgExcluirFatura.CommandName = "Excluir";
            imgExcluirFatura.Attributes["onclick"] = "return confirm('Confirmar exclusão da Duplicata?');";
            imgExcluirFatura.Style.Add("cursor", "hand");
            imgExcluirFatura.ToolTip = "Excluir Duplicata";
            #endregion

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }

    }
    #endregion

    protected void ddlCFOP_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlCFOP_SelectedIndexChanged1(object sender, EventArgs e)
    {
    }

    protected void btnCarregarValoresCFOP_Click(object sender, EventArgs e)
    {
        CFOPVO identCFOP = new CFOPVO();
        identCFOP.CFOP = txtCFOP.Text;
        List<CFOPVO> lstCFOP = new CFOP().Listar(identCFOP);
        if(lstCFOP.Count > 0)
            DadosCFOP = lstCFOP[0];
    }

    protected void btnIncluirProduto_Click(object sender, EventArgs e)
    {

    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        var opcao = Request.QueryString["opcao"] ?? string.Empty;
        var valor = Request.QueryString["valor"] ?? string.Empty;
        var campo = Request.QueryString["campo"] ?? string.Empty;
        Response.Redirect("ListaNFe.aspx?valor="+valor+"&opcao="+opcao+"&campo="+campo);
    }


}
