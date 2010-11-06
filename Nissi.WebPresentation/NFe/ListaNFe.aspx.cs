using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Model;
using Nissi.Business;
using System.Globalization;

namespace Nissi.WebPresentation.NFe
{
    public partial class ListaNFe : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["CodNF"] != null || Request.QueryString["opcao"] != null)
                {
                    hdfCodNF.Value = Request.QueryString["CodNF"] ?? string.Empty;
                    hdfOpcao.Value = Request.QueryString["opcao"] ?? string.Empty;
                    if (!string.IsNullOrEmpty(hdfOpcao.Value))
                    {
                        hdfValor.Value = Request.QueryString["valor"] ?? string.Empty;
                        EscolherOpcao(hdfOpcao.Value);
                    }
                    Pesquisar();
                }
                Master.PosicionarFoco(txtNF);
            }
        }
        private void EscolherOpcao(string opcao)
        {
            string tvar = string.Empty;
            switch (hdfOpcao.Value)
            {
                case "NF":
                    txtNF.Text = hdfValor.Value;
                    rbNF.Checked = true;
                    tvar = "1";
                    break;
                case "DataEmissao":
                    txtDataEmissao.Text = hdfValor.Value;
                    rbDataEmissao.Checked = true;
                    tvar = "2";
                    break;
                case "RazaoSocial":
                    txtRazaoSocial.Text = hdfValor.Value;
                    rbRazaoSocial.Checked = true;
                    tvar = "3";
                    break;
                case "CodigoCliente":
                    txtCodigoCliente.Text = hdfValor.Value;
                    rbCodigoCliente.Checked = true;
                    tvar = "5";
                    break;
                case "Produto":
                    txtCodigoDescricao.Text = hdfValor.Value;
                    rbProduto.Checked = true;
                    string campo = Request.QueryString["campo"] ?? string.Empty;
                    if (campo.Equals("0"))
                        rbCodigo.Checked = true;
                    else
                        rbOP.Checked = true;
                        tbxDataIni.Text = Session["dataini"].ToString();
                        tbxDataFim.Text = Session["datafim"].ToString();
                    tvar = "4";
                    break;
            }
            ExecutarScript(new StringBuilder("TipoPesquisa("+tvar+")"));
        }

        #region Pesquisar
        private void Pesquisar()
        {
            grdListaProduto.DataSource = null;
            grdListaProduto.DataBind();
            grdListaResultado.DataSource = null;
            grdListaResultado.DataBind();
            if (rbProduto.Checked)
            {
                string codigo = null;
                string Op = null;
                DateTime? dataini = Convert.ToDateTime(tbxDataIni.Text);
                DateTime? datafim = Convert.ToDateTime(tbxDataFim.Text);
                if (rbCodigo.Checked)
                {
                    codigo = txtCodigoDescricao.Text;
                }
                else
                    Op = txtCodigoDescricao.Text;

                List<ProdutoNFVO> lstProdutoNF = new ProdutoNF().Lista(codigo, Op, dataini, datafim);
                if (lstProdutoNF.Count > 0)
                {
                    grdListaProduto.DataSource = lstProdutoNF;
                    grdListaProduto.DataBind();
                }
                else
                    MensagemCliente("Não existem registros para o filtro informado.");
            }
            else
            {
                NotaFiscalVO identNFe = new NotaFiscalVO();
                if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                {
                    identNFe.Cliente.CodPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
                }
                else
                {
                    if (rbNF.Checked && !string.IsNullOrEmpty(txtNF.Text))
                        identNFe.NF = int.Parse(txtNF.Text);
                    if (rbDataEmissao.Checked && !string.IsNullOrEmpty(txtDataEmissao.Text))
                        identNFe.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);
                    if (!string.IsNullOrEmpty(hdfCodNF.Value))
                        identNFe.CodNF = int.Parse(hdfCodNF.Value);
                    if (rbRazaoSocial.Checked && !string.IsNullOrEmpty(txtRazaoSocial.Text))
                        identNFe.Cliente.RazaoSocial = txtRazaoSocial.Text;
                    if (rbCodigoCliente.Checked && !string.IsNullOrEmpty(txtCodigoCliente.Text))
                        identNFe.Cliente.CodRef = txtCodigoCliente.Text;
                }
                List<NotaFiscalVO> lNotaFiscal = new NotaFiscal().Listar(identNFe);
                if (lNotaFiscal.Count > 0)
                {
                    grdListaResultado.DataSource = lNotaFiscal;
                    grdListaResultado.DataBind();
                }
                else
                {
                    MensagemCliente("Não existem registros para o filtro informado.");
                }
                hdfIdRazaoSocial.Value = string.Empty;
            }
        }
        #endregion

        #region Métodos do Grid
        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            NotaFiscalVO identNFe = new NotaFiscalVO();
            identNFe.CodNF = int.Parse(e.CommandArgument.ToString());
            switch(e.CommandName)
            {
                //Modulo de Edicao
                case "Editar":
                if (rbNF.Checked && !string.IsNullOrEmpty(txtNF.Text))
                {
                    hdfValor.Value = txtNF.Text;
                    hdfOpcao.Value = "NF";
                }
                if (rbDataEmissao.Checked && !string.IsNullOrEmpty(txtDataEmissao.Text))
                {
                    hdfValor.Value = txtDataEmissao.Text;
                    hdfOpcao.Value = "DataEmissao";
                }
                if (rbRazaoSocial.Checked && !string.IsNullOrEmpty(txtRazaoSocial.Text))
                {
                    hdfValor.Value = txtRazaoSocial.Text;
                    hdfOpcao.Value = "RazaoSocial";
                }
                if (rbCodigoCliente.Checked && !string.IsNullOrEmpty(txtCodigoCliente.Text))
                {
                    hdfValor.Value = txtCodigoCliente.Text;
                    hdfOpcao.Value = "CodigoCliente";
                }
                Response.Redirect("CadastraNFe.aspx?acao=Editar&CodNF=" + identNFe.CodNF+"&valor="+hdfValor.Value+"&opcao="+hdfOpcao.Value);
                break;
                //Modulo de Excluir
                case "Excluir":
                    break;
                //Modulo de Imprimir
                case "Imprimir":
                    break;
                //Modulo de Enviar
                case "Enviar":
                    break;
            }
        }
        
        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NotaFiscalVO identNFe = (NotaFiscalVO)e.Row.DataItem;
                e.Row.Cells[1].Text = identNFe.Serie;
                e.Row.Cells[2].Text = identNFe.NF.ToString().PadLeft(8, '0'); ;
                e.Row.Cells[3].Text = identNFe.DataEmissao.Value.ToString("dd/MM/yyyy");
                e.Row.Cells[4].Text = identNFe.Cliente.RazaoSocial;
                e.Row.Cells[5].Text = identNFe.Cliente.NomeFantasia;
                e.Row.Cells[6].Text = identNFe.NFe.ChaveNFE;

                #region Botao Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = identNFe.CodNF.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados da Nota Fiscal [" + identNFe.NF.ToString().PadLeft(8, '0') + "] - Cliente [" + identNFe.Cliente.RazaoSocial.Trim() + "]";
                #endregion
                #region Botao Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\excluir.png";
                imgExcluir.CommandArgument = identNFe.CodNF.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Cancelar dados da Nota Fiscal [" + identNFe.NF.ToString().PadLeft(8, '0') + "] - Cliente [" + identNFe.Cliente.RazaoSocial.Trim() + "]";
                #endregion
                #region Botao Status
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                imgStatus.Visible = true; 
                switch (identNFe.NFe.IndStatus)
                {
                    case "0":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\Enviar.png";
                        imgStatus.ToolTip = "Aguardando Envio";
                        break;
                    case "1":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\NFeOk.png";
                        imgStatus.ToolTip = "Autorizado o uso da NF-e";
                        break;
                    case "2":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\NFeFail.png";
                        imgStatus.ToolTip = "Erro no Schema";
                        e.Row.CssClass = "FundoLinha3";
                        break;
                    case "3":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\NFeFail.png";
                        imgStatus.ToolTip = "Cancelado o uso da NF-e";
                        e.Row.CssClass = "FundoLinha3";
                        break;
                }
                //imgStatus.ToolTip = "Enviar dados da Nota Fiscal [" + identNFe.NF.ToString() + "] - Cliente [" + identNFe.Cliente.RazaoSocial.Trim() + "]";
                #endregion
                #region Botao Emitir duplicata
                ImageButton imgDuplicata = (ImageButton)e.Row.FindControl("imgDuplicata");
                imgDuplicata.ToolTip = "Emissão de Duplicata da nota fiscal [" + identNFe.NF.ToString().PadLeft(8, '0') + "]";
                imgDuplicata.Attributes.Add("onclick", "ChamaDuplicata(" + identNFe.CodNF.ToString() + ")");
                imgDuplicata.CommandArgument = identNFe.CodNF.ToString();
                imgDuplicata.CommandName = "Duplicata";
                #endregion
                if (identNFe.NFe.IndStatus != "2" && identNFe.NFe.IndStatus != "3")
                {
                    if (e.Row.RowState == DataControlRowState.Normal)
                        e.Row.CssClass = "FundoLinha1";
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                        e.Row.CssClass = "FundoLinha2";
                }
            }
        }
        #endregion
        #region btnPesquisar_Click
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            hdfCodNF.Value = string.Empty;
            Pesquisar();
        }        
        #endregion

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastraNFe.aspx?acao=Incluir");
        }

        protected void rbCNPJ_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
        #region Métodos do Grid Produto
        protected void grdListaProduto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaProduto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProdutoNFVO identProdutoNF = new ProdutoNFVO();
            identProdutoNF.CodNF = int.Parse(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                //Modulo de Edicao
                case "Editar":
                    string campo = "0";
                    if (rbProduto.Checked && !string.IsNullOrEmpty(txtCodigoDescricao.Text))
                    {
                        hdfValor.Value = txtCodigoDescricao.Text;
                        hdfOpcao.Value = "Produto";
                        if (rbOP.Checked)
                            campo = "1";
                        Session["dataini"] = tbxDataIni.Text;
                        Session["datafim"] = tbxDataFim.Text;
                    }
                    Response.Redirect("CadastraNFe.aspx?acao=Editar&CodNF=" + identProdutoNF.CodNF + "&valor=" + hdfValor.Value + "&opcao=" + hdfOpcao.Value+"&campo="+campo);
                    break;
                //Modulo de Excluir
                case "Excluir":
                    break;
                //Modulo de Imprimir
                case "Imprimir":
                    break;
                //Modulo de Enviar
                case "Visualizar":
                    MensagemCliente("Função disponivel em breve!");
                    break;
            }

        }
        private decimal TotalGeral = 0;
        protected void grdListaProduto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProdutoNFVO identProdutoNF = (ProdutoNFVO)e.Row.DataItem;
                e.Row.Cells[1].Text = identProdutoNF.NF.ToString().PadLeft(8, '0');
                if (!string.IsNullOrEmpty(identProdutoNF.OP))
                    e.Row.Cells[2].Text = identProdutoNF.OP.Trim();
                if (identProdutoNF.DataEmissao != null)
                    e.Row.Cells[3].Text = identProdutoNF.DataEmissao.Value.ToString("dd/MM/yyyy");
                e.Row.Cells[4].Text = identProdutoNF.Codigo;
                string pedido = string.Empty;
                if (!string.IsNullOrEmpty(identProdutoNF.CodPedidoCliente))
                    pedido = " - Ped.: " + identProdutoNF.CodPedidoCliente.Trim();
                e.Row.Cells[5].Text = identProdutoNF.Descricao+pedido;
                e.Row.Cells[6].Text = identProdutoNF.Qtd.ToString();
                e.Row.Cells[7].Text = identProdutoNF.Valor.ToString();
                e.Row.Cells[8].Text = identProdutoNF.TotalItem.ToString();
                if (identProdutoNF.TotalItem != null) 
                    TotalGeral += identProdutoNF.TotalItem.Value;
                #region Botao Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditarProduto");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = identProdutoNF.CodNF.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados da Nota Fiscal [" + identProdutoNF.NF.ToString().PadLeft(8, '0') + "] - Produto [" + identProdutoNF.Descricao.Trim() + "]";
                #endregion
                #region Botao Visualizar
                ImageButton imgVisualizar = (ImageButton)e.Row.FindControl("imgVisualizar");
                imgVisualizar.ImageUrl = caminhoAplicacao + @"Imagens\apps.png";
                imgVisualizar.ToolTip = "Visualizar nota fiscal [" + identProdutoNF.NF.ToString().PadLeft(8, '0') + "]";
                //imgDuplicata.Attributes.Add("onclick", "ChamaDuplicata(" + identNFe.CodNF + ")");
                imgVisualizar.CommandArgument = identProdutoNF.NF.ToString();
                imgVisualizar.CommandName = "Visualizar";
                #endregion
                #region Botao Status
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatusProduto");
                imgStatus.Visible = true;
                switch (identProdutoNF.IndStatus)
                {
                    case "0":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\Enviar.png";
                        imgStatus.ToolTip = "Aguardando Envio";
                        break;
                    case "1":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\NFeOk.png";
                        imgStatus.ToolTip = "Autorizado o uso da NF-e";
                        break;
                    case "2":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\NFeFail.png";
                        imgStatus.ToolTip = "Erro no Schema";
                        e.Row.CssClass = "FundoLinha3";
                        break;
                    case "3":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\NFeFail.png";
                        imgStatus.ToolTip = "Cancelado o uso da NF-e";
                        e.Row.CssClass = "FundoLinha3";
                        break;
                }
                #endregion
                if (identProdutoNF.IndStatus != "2" && identProdutoNF.IndStatus != "3")
                {
                    if (e.Row.RowState == DataControlRowState.Normal)
                        e.Row.CssClass = "FundoLinha1";
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                        e.Row.CssClass = "FundoLinha2";
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text = "Total de Vendas:";
                e.Row.Cells[7].Attributes.Add("align", "left");
                e.Row.Cells[8].Attributes.Add("align", "right");
                e.Row.Cells[8].Text = TotalGeral.ToString();
            }
        }
        #endregion


    }
}
