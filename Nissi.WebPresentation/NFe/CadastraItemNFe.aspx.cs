using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Model;
using Nissi.Business;

public partial class CadastraItemNFe : BasePage
{
    #region Propriedades
    public ItemNotaFiscalVO DadosItemNotaFiscalVO
    {
        set
        {
            txtDescricao.Text = value.Produto.Descricao;
            ddlUnidade.SelectedValue = value.Produto.Unidade.CodUnidade.ToString();
            ddlClassificacaoFiscal.SelectedValue = value.Produto.NCM;
            hdfCodItemNotaFiscal.Value = value.CodItemNotaFiscal.ToString();
            txtCodigo.Text = value.Codigo; //Código do produto
            ddlUnidade.SelectedValue = value.Unidade; //Unidade de medida
            txtQuantidade.Text = value.Qtd.ToString(); //Quantidade
            txtValorItem.Text = value.Valor.ToString(); //Preço de Venda
            txtDesconto.Text = value.Desconto.ToString(); //Desconto
            txtTotalItem.Text = value.TotalItem.ToString(); //Total do Ítem
            txtICMS.Text = value.ICMS.ToString(); //ICMS
            txtBaseICMS.Text = value.BaseICMS.ToString(); //Base ICMS
            txtValorICMS.Text = value.CalcIcms.ToString(); //Valor do ICMS
            txtIPI.Text = value.IPI.ToString(); //IPI
            txtValorIPI.Text = value.CalcIPI.ToString(); //Valor do IPI
            txtSituacaoTributaria.Text = value.Icms.CodTipoTributacao; //Situação Tributária
            tbxCodPedido.Text = value.CodPedidoCliente;
            tbxOP.Text = value.OP.ToString();
      
            if (value.CalcICMSSobIpi == true) //Calcular ICMS Sob IPI
                ckbCalcSobIpi.Checked = true;
            else
                ckbCalcSobIpi.Checked = false;

            //setar valores do icms
            ICMSGrid.DataSource = value.Produto.ICMS;
            ICMSGrid.DataBind();
            ViewState["lstICMS"] = value.Produto.ICMS.ToArray();
        }
        get
        {
            ItemNotaFiscalVO identItemNotaFiscalVO = new ItemNotaFiscalVO();
            identItemNotaFiscalVO.Produto.Descricao = txtDescricao.Text;
            identItemNotaFiscalVO.Produto.Unidade.CodUnidade = Convert.ToInt32(ddlUnidade.SelectedValue);
            //identItemNotaFiscalVO.Produto.NCM = ddlClassificacaoFiscal.SelectedValue;
            identItemNotaFiscalVO.Produto.Codigo = txtCodigo.Text;
            if (Request.QueryString["CodProduto"] != null)
                identItemNotaFiscalVO.Produto.CodProduto = Convert.ToInt32(Request.QueryString["CodProduto"]);
            if (Request.QueryString["CodItemNotaFiscal"] != null)
                identItemNotaFiscalVO.CodItemNotaFiscal = Convert.ToInt32(Request.QueryString["CodItemNotaFiscal"]);
            //setar valores do icms
            ICMSVO[] lstICMS = (ICMSVO[])ViewState["lstICMS"];
            List<ICMSVO> newlstICMS = new List<ICMSVO>(lstICMS);
            identItemNotaFiscalVO.Produto.ICMS = newlstICMS;
            foreach (ICMSVO tempICMS in lstICMS)
            {
                identItemNotaFiscalVO.Icms = tempICMS;
            }
            identItemNotaFiscalVO.Codigo = txtCodigo.Text; //Código do produto
            //Descrição do produto
            identItemNotaFiscalVO.Unidade = ddlUnidade.SelectedValue; //Unidade de medida
            identItemNotaFiscalVO.Produto.Unidade.TipoUnidade = ddlUnidade.Text; //Unidade de medida
            identItemNotaFiscalVO.Qtd = strToDecimal(txtQuantidade.Text); //Quantidade
            identItemNotaFiscalVO.Valor = strToDecimal(txtValorItem.Text); //Preço de Venda
            identItemNotaFiscalVO.Desconto = strToDecimal(txtDesconto.Text); //Desconto
            identItemNotaFiscalVO.ICMS = strToDecimal(txtICMS.Text); //ICMS
            identItemNotaFiscalVO.BaseICMS = strToDecimal(txtBaseICMS.Text); //Base ICMS
            identItemNotaFiscalVO.IPI = strToDecimal(txtIPI.Text); //IPI
            identItemNotaFiscalVO.Produto.NCM = ddlClassificacaoFiscal.SelectedValue; //Classificação Fiscal
            identItemNotaFiscalVO.CalcICMSSobIpi = (ckbCalcSobIpi.Checked ? true : false); //Calcular ICMS Sob IPI
            if (!string.IsNullOrEmpty(tbxCodPedido.Text))
                identItemNotaFiscalVO.CodPedidoCliente = tbxCodPedido.Text.Trim();
            identItemNotaFiscalVO.OP = tbxOP.Text.Trim();

            return identItemNotaFiscalVO;
        }
    }
    private decimal strToDecimal(string valor)
    {
        decimal dec = 0;
        if (!string.IsNullOrEmpty(valor))
            dec = Convert.ToDecimal(valor);
        return dec;
    }
    public ProdutoVO DadosProdutoVO
    {
        set
        {
            txtDescricao.Text = value.Descricao;
            txtCodigo.Text = value.Codigo;
            ddlUnidade.SelectedValue = value.Unidade.CodUnidade.ToString();
            ddlClassificacaoFiscal.SelectedValue = value.NCM;
            foreach (ICMSVO icms in value.ICMS)
                txtSituacaoTributaria.Text = "0" + icms.CodTipoTributacao;
            //setar valores do icms
            ICMSGrid.DataSource = value.ICMS;
            ICMSGrid.DataBind();
            ViewState["lstICMS"] = value.ICMS.ToArray();
        }
    }

    #endregion

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Master.InibirTopo();
            CarregaCombo();
            Master.PosicionarFoco(txtQuantidade);
            if (Request.QueryString["CodProduto"] != null)
            {
                //Se AcaoProduto=Editar, significa que a página veio da CadastraNFe e os
                //dados do produto serão editados
                if ((Request.QueryString["AcaoProduto"] != null) &&
                    (Request.QueryString["AcaoProduto"].Equals("Editar")) &&
                    (Session["lstItemNotaFiscal"] != null))
                {
                    ItemNotaFiscalVO[] lstItemNotaFiscal = (ItemNotaFiscalVO[])Session["lstItemNotaFiscal"];
                    var Nota = lstItemNotaFiscal.Single(t => t.Produto.CodProduto == Convert.ToInt32(Request.QueryString["CodProduto"]) && t.CodItemNotaFiscal == Convert.ToInt32(Request.QueryString["CodItemNotaFiscal"]));
                    DadosItemNotaFiscalVO = Nota;
                }
                else
                {
                    Random random = new Random();
                    int num = random.Next(1000);

                    ProdutoVO identProduto = new ProdutoVO();
                    hdfCodItemNotaFiscal.Value = num.ToString();
                    tbxOP.Text = hdfCodItemNotaFiscal.Value;
                    identProduto.CodProduto = Convert.ToInt32(Request.QueryString["CodProduto"]);
                    DadosProdutoVO = new Produto().Listar(identProduto)[0];
                }

                Session.Remove("ItemNF");
            }
        }

    }

    protected void ICMSGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void ICMSGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ICMSVO tempICMS = (ICMSVO)e.Row.DataItem;
            var tributacao = TipoTributacao.GetListaTipoTributacao().SingleOrDefault(t => t.Codigo == tempICMS.CodTipoTributacao);
            if (tributacao != null)
                e.Row.Cells[0].Text = tributacao.Descricao;
            var origem = OrigemMercadoria.GetListaOrigemMercadoria().SingleOrDefault(o => o.Codigo == tempICMS.CodOrigem);
            if (origem != null)    
                e.Row.Cells[1].Text = origem.Descricao;
        }
    }

    #endregion

    #region Métodos Privados
    private void CarregaCombo()
    {
        ddlUnidade.DataSource = new Unidade().Listar(new UnidadeVO());
        ddlUnidade.DataTextField = "TipoUnidade";
        ddlUnidade.DataValueField = "CodUnidade";
        ddlUnidade.DataBind();

        ddlClassificacaoFiscal.DataSource = new ClassificacaoFiscal().Listar(new ClassificacaoFiscalVO());
        ddlClassificacaoFiscal.DataTextField = "Letra";
        ddlClassificacaoFiscal.DataValueField = "Numero";
        ddlClassificacaoFiscal.DataBind();
    }

    #endregion

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        //armazena valores na session para usar na página de cadastro de nf
        Session.Add("ItemNF", DadosItemNotaFiscalVO);

        if (Request.QueryString["AcaoProduto"] != null)
        {
            Session.Add("AcaoProduto", Request.QueryString["AcaoProduto"]);
        }
        else
        {
            Session.Add("AcaoProduto", "Incluir");
        }

        ExecutarScript(new StringBuilder("window.close();"));
    }

    protected void btnAtualizar_Click(object sender, EventArgs e)
    {

    }
}