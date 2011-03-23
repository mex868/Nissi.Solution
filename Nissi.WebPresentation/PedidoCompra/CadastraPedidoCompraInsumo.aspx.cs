using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using Nissi.Model;
using Nissi.Business;
using Nissi.Util;
using System.Globalization;
using System.Web.Services;

public partial class CadastrarPedidoCompraInsumo : BasePage
{
    #region Propriedades
    private FornecedorVO DadosRazaoSocial
    {
        set {
            if (value.CodPessoa > 0)
            {
                value.Cep = PreencherCamposCEP(value.Cep);
                hdfCodigoFornecedor.Value = value.CodRef;
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
                txtContato.Text = value.Contato;
                txtEmail.Text = value.Email;
                txtSite.Text = value.Site;
            }
        }
        get
        {
            FornecedorVO FornecedorVO = new FornecedorVO();
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                FornecedorVO.CodPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
            return FornecedorVO;
        }
    }

    private PedidoCompraInsumoVO DadosPedidoCompraInsumo
    {
        set
        {
            hdfCodPedidoCompraInsumo.Value = value.CodPedidoCompraInsumo.ToString();
            txtPedidoCompra.Text = value.CodPedidoCompraInsumo.ToString().PadLeft(8,'0');
            if (value.DataEmissao != null)
            txtEmissao.Text = value.DataEmissao.ToString("dd/MM/yyyy");
            ddlCondicoesPgto.SelectedValue = value.FormaPgto.CodFormaPgto.ToString();
            ddlTipoRetirada.SelectedValue = value.TipoRetirada.ToString();
            if(value.FuncionarioComprador != null)
            {
                hdfCodComprador.Value = value.FuncionarioComprador.CodPessoa.ToString();
                txtComprador.Text = value.FuncionarioComprador.Nome;
            }
            if(value.FuncionarioAprovador != null)
            {
                hdfCodAprovador.Value = value.FuncionarioAprovador.CodPessoa.ToString();
                txtAprovador.Text = value.FuncionarioAprovador.Nome;
            }
            txtCondicoesFornecimento.Text = value.CondicaoFornecimento;
            txtObservacao.Text = value.Observacao;
            txtDataPrazoEntrega.Text = value.DataPrazoEntrega.ToString();
            DadosRazaoSocial = value.Fornecedor;
            grdProduto.DataSource = value.ItemPedidoCompraInsumoVo;
            grdProduto.DataBind();
        }
        get 
        {
            PedidoCompraInsumoVO identPedidoCompraInsumoVO = new PedidoCompraInsumoVO();
            ItemPedidoCompraInsumoVO[] lstItemPedidoCompraInsumoVO = (ItemPedidoCompraInsumoVO[])Session["lstItemPedidoCompraInsumo"];
            List<ItemPedidoCompraInsumoVO> newlstItemPedidoCompraInsumoVO = new List<ItemPedidoCompraInsumoVO>(lstItemPedidoCompraInsumoVO);
            if (!string.IsNullOrEmpty(hdfCodPedidoCompraInsumo.Value))
                identPedidoCompraInsumoVO.CodPedidoCompraInsumo = int.Parse(hdfCodPedidoCompraInsumo.Value);
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                identPedidoCompraInsumoVO.Fornecedor.CodPessoa = int.Parse(hdfIdRazaoSocial.Value);
            txtPedidoCompra.Text = identPedidoCompraInsumoVO.CodPedidoCompraInsumo.ToString().PadLeft(8, '0');
            if (!string.IsNullOrEmpty(txtEmissao.Text))
                identPedidoCompraInsumoVO.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
            if (!string.IsNullOrEmpty(txtDataPrazoEntrega.Text))
                identPedidoCompraInsumoVO.DataPrazoEntrega = Convert.ToDateTime(txtDataPrazoEntrega.Text);
            if (ddlCondicoesPgto.SelectedValue != null)
                identPedidoCompraInsumoVO.FormaPgto.CodFormaPgto = short.Parse(ddlCondicoesPgto.SelectedValue);
            identPedidoCompraInsumoVO.TipoRetirada = short.Parse(ddlTipoRetirada.SelectedValue);
            identPedidoCompraInsumoVO.FuncionarioComprador.CodPessoa = hdfCodComprador.Value != ""? int.Parse(hdfCodComprador.Value):0;
            identPedidoCompraInsumoVO.FuncionarioAprovador.CodPessoa = hdfCodAprovador.Value != ""? int.Parse(hdfCodAprovador.Value):0;
            identPedidoCompraInsumoVO.CondicaoFornecimento = txtCondicoesFornecimento.Text;
            identPedidoCompraInsumoVO.Observacao = txtObservacao.Text;
            identPedidoCompraInsumoVO.ItemPedidoCompraInsumoVo = newlstItemPedidoCompraInsumoVO;
            return identPedidoCompraInsumoVO; 
        }
    }
    #endregion

    #region Eventos
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            CarregarCombos();
            //cria a referência a variável que vai ser armazenada na Session
            List<ItemPedidoCompraInsumoVO> lstItemPedidoCompraInsumo = new List<ItemPedidoCompraInsumoVO>();
            //cria referência que vai resgatar o valor da Session
            PedidoCompraInsumoVO identPedidoCompra = new PedidoCompraInsumoVO();
            hdfTipoAcao.Value = Request.QueryString["acao"] ?? "";
            if (hdfTipoAcao.Value.Equals("Incluir"))
            {
                txtEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnVoltar.Enabled = false;
                txtCondicoesFornecimento.Text = "ENVIAR CERTIFICADO DE MAT. PRIMA";
                txtObservacao.Text = "NÃO ACEITAREMOS MATERIAL EM VÁRIOS ROLOS, POIS TEMOS MUITA PERDA";
            }
            else
            if (hdfTipoAcao.Value.Equals("Editar"))
            {
                identPedidoCompra.CodPedidoCompraInsumo = Convert.ToInt32(Request.QueryString["CodPedidoCompraInsumo"]);
                identPedidoCompra = new PedidoCompra().ListarTudoInsumo(identPedidoCompra.CodPedidoCompraInsumo);
                DadosPedidoCompraInsumo = identPedidoCompra;
                lstItemPedidoCompraInsumo = identPedidoCompra.ItemPedidoCompraInsumoVo;
                btnVoltar.Enabled = true;
                btnSalvar.Enabled = true;
            }

            //btnIncluirProduto.Attributes.Add("onclick", "ChamaPopup();");
            //criar Session para armazenar valores do grid dos Itens da Nota Fiscal
            //este grid só salvará quando salvar a Nota Fiscal inteira
            Session.Add("lstItemPedidoCompraInsumo", lstItemPedidoCompraInsumo.ToArray());
            //criar ViewState para armazenar valores do grid das Duplicatas
            //este grid só salvará quando salvar a Nota Fiscal inteira
            Master.PosicionarFoco(txtEmissao);
        }

        ExecutarScript(new StringBuilder("OcultarBotaoCarregarValores();"));        
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (grdProduto.Rows.Count > 0)
        {
            Response.Redirect("ListaPedidoCompra.aspx?CodPedidoCompraInsumo=");
        }
        else
            MensagemCliente(updDados, "Não foi associado nenhuma matéria prima ao pedido de compra!");
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimparCampos();
        Response.Redirect("ListaPedidoCompra.aspx");
    }

    #endregion

    #region Métodos Privados
    //Todo: Está função poderia ser feita no próprio FornecedorData.cs
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

    private void LimparCampos()
    {
        Session.Remove("ItemPedidoCompraInsumo");
        Session.Remove("AcaoProduto");
    }

    private void CarregarCombos()
    {
        Geral.CarregarDDL(ref ddlCondicoesPgto, new FormaPgto().Listar().ToArray(), "CodFormaPgto", "Descricao");
        Geral.CarregarDDL(ref ddlProdutoInsumo, ProdutoInsumo.Listar().ToArray(),"CodProdutoInsumo","Descricao");
        Geral.CarregarDDL(ref ddlUnidadeInsumo, new Unidade().Listar(new UnidadeVO()).ToArray(), "CodUnidade", "TipoUnidade");
    }
    #endregion

    
    #region Métodos do Grid Produto
    protected void grdProduto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdProduto_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }


    protected void grdProduto_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
        
    #endregion
    
    
    protected void btnIncluirProduto_Click(object sender, EventArgs e)
    {
        
    }

    private void LimparCamposItemPedidoCompraInsumo()
    {
        hdfTipoAcaoItemPedidoCompraInsumo.Value = "Incluir";

            txtQtdInsumo.Text =
            ddlProdutoInsumo.Text =
            ddlUnidadeInsumo.Text =
            txtIPIInsumo.Text =
            txtValorInsumo.Text = string.Empty;

    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        var opcao = Request.QueryString["opcao"] ?? string.Empty;
        var valor = Request.QueryString["valor"] ?? string.Empty;
        var campo = Request.QueryString["campo"] ?? string.Empty;
        Response.Redirect("ListaPedidoCompra.aspx?valor="+valor+"&opcao="+opcao+"&campo="+campo);
    }

    protected void btnCarregarValores_Click(object sender, EventArgs e)
    {
        FornecedorVO identFornecedor = new FornecedorVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identFornecedor.CodPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
        DadosRazaoSocial = new Fornecedor().Listar(identFornecedor).First();
        ExecutarScript(updItens, new StringBuilder("showItens();"));
    }

    protected void btnSalvarItem_Click(object sender, EventArgs e)
    {
        //será incluido no grid de ICMS manualmente (não incluirá no banco ainda)
        //pois só deverá ser incluido no banco quando salvar o produto

        //armazena em viewstate a linha selecionada para posterior atualização
        ItemPedidoCompraInsumoVO[] lstItemPedidoCompraInsumo = (ItemPedidoCompraInsumoVO[])Session["lstItemPedidoCompraInsumo"];
        List<ItemPedidoCompraInsumoVO> newlstItemPedidoCompraInsumo = new List<ItemPedidoCompraInsumoVO>(lstItemPedidoCompraInsumo);
        int codProdutoInsumo = 0;
        try
        {

                codProdutoInsumo = int.Parse(ddlProdutoInsumo.SelectedValue);                

        }
        catch (Exception)
        {
            MensagemCliente(updItens, "Escolha um valor na lista para a Matéria Prima!");
            return;
        }

        int codUnidade = 0;
        try
        {
            codUnidade = int.Parse(ddlUnidadeInsumo.SelectedValue);
        }
        catch (Exception)
        {
            MensagemCliente(updItens, "Escolha um valor na lista para a Unidade!");
            return;
        }
        
        decimal qtde = !string.IsNullOrEmpty(txtQtdInsumo.Text) ? decimal.Parse(txtQtdInsumo.Text) : 0;
        decimal valor = !string.IsNullOrEmpty(txtValorInsumo.Text) ? decimal.Parse(txtValorInsumo.Text) : 0;
        string descricao = ddlProdutoInsumo.SelectedItem.Text;
        string unidade = ddlUnidadeInsumo.SelectedItem.Text;
        decimal Ipi = !string.IsNullOrEmpty(txtIPIInsumo.Text) ? decimal.Parse(txtIPIInsumo.Text) : 0;

        //se for edição de ICMS, atualizar o list
        if (hdfTipoAcaoItemPedidoCompraInsumo.Value.Equals("Incluir"))
        {
            /************************************************************************
            Se a ação for inclusão, simplesmente verifica se o ítem já foi cadastrado
            se já for, exibe mensagem e não inclui o ítem
            /***********************************************************************/
            ItemPedidoCompraInsumoVO result = newlstItemPedidoCompraInsumo.Find(
            delegate(ItemPedidoCompraInsumoVO bk)
            {
                return bk.ProdutoInsumoVo.CodProdutoInsumo == codProdutoInsumo;
            }
            );
            if (result != null)
            {

                MensagemCliente(updItens, "Material já cadastrada!");
                return;
            }
            /************************************************************************/
            /************************************************************************/

            //senão, incluir novo ítem no list
            ItemPedidoCompraInsumoVO lstItemPedidoCompraInsumoAux = new ItemPedidoCompraInsumoVO();

            lstItemPedidoCompraInsumoAux.ProdutoInsumoVo.CodProdutoInsumo = codProdutoInsumo;
            lstItemPedidoCompraInsumoAux.ProdutoInsumoVo.Descricao = descricao;
            lstItemPedidoCompraInsumoAux.UnidadeVo.CodUnidade = codUnidade;
            lstItemPedidoCompraInsumoAux.UnidadeVo.TipoUnidade = unidade;
            lstItemPedidoCompraInsumoAux.Ipi = Ipi;
            lstItemPedidoCompraInsumoAux.Qtd = qtde;
            lstItemPedidoCompraInsumoAux.Valor = valor;

            newlstItemPedidoCompraInsumo.Add(lstItemPedidoCompraInsumoAux);
        }
        else
        {
            /************************************************************************
            Se a ação for alteração, verifica se o ítem já está cadastrado, se já estiver
            será impedido, desde que não seja ele mesmo
            /***********************************************************************/
            int linha = Convert.ToInt32(ViewState["LinhaSelecionadaItemPedidoCompraInsumo"]);
            if (linha != codProdutoInsumo)
            {
                var item1 =
                    newlstItemPedidoCompraInsumo.Where(r => r.ProdutoInsumoVo.CodProdutoInsumo == codProdutoInsumo).Select(r => r).
                        FirstOrDefault();
                if (item1 != null)
                {
                    MensagemCliente(updItens, "Material já cadastrada!");
                    return;
                }
            }

            /************************************************************************
            Atualiza o item do grid        
            /***********************************************************************/
            var item =
                newlstItemPedidoCompraInsumo.Where(r => r.ProdutoInsumoVo.CodProdutoInsumo == linha).Select(r => r).
                    FirstOrDefault();
            item.ProdutoInsumoVo.CodProdutoInsumo = codProdutoInsumo;
            item.ProdutoInsumoVo.Descricao = descricao;
            item.UnidadeVo.CodUnidade = codUnidade;
            item.UnidadeVo.TipoUnidade = unidade;
            item.Ipi = Ipi;
            item.Qtd = qtde;
            item.Valor = valor;
            //sai do for
        }
        grdProduto.DataSource = newlstItemPedidoCompraInsumo;
        grdProduto.DataBind();
        //atualiza viewstate
        Session["lstItemPedidoCompraInsumo"] = newlstItemPedidoCompraInsumo.ToArray();
        LimparCamposItemPedidoCompraInsumo();
        Master.PosicionarFoco(ddlProdutoInsumo);
        ExecutarScript(updItens, new StringBuilder("showItens();"));
    }


    protected void ddlBitola_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
