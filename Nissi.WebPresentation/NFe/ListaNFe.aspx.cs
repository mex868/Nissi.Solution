using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Model;
using Nissi.Business;

namespace Nissi.WebPresentation.NFe
{
    public partial class ListaNFe : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["CodNF"] != null)
                {
                    hdfCodNF.Value = Request.QueryString["CodNF"].ToString();
                    Pesquisar();
                }
                Master.PosicionarFoco(txtNF);
            }
        }
        #region Pesquisar
        private void Pesquisar()
        {
            NotaFiscalVO identNFe = new NotaFiscalVO();
            if (rbNF.Checked && !string.IsNullOrEmpty(txtNF.Text))
                identNFe.NF = int.Parse(txtNF.Text);
            if (rbDataEmissao.Checked && !string.IsNullOrEmpty(txtDataEmissao.Text))
                identNFe.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);
            if (!string.IsNullOrEmpty(hdfCodNF.Value))
                identNFe.CodNF = int.Parse(hdfCodNF.Value);
            if (rbRazaoSocial.Checked && !string.IsNullOrEmpty(txtRazaoSocial.Text))
                identNFe.Cliente.RazaoSocial = txtRazaoSocial.Text;
            if (rbNaoEnviadas.Checked)
                identNFe.Status = 0;

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
                    Response.Redirect("CadastraNFe.aspx?acao=Editar&CodNF=" + identNFe.CodNF.ToString());
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
                imgEditar.ToolTip = "Editar dados da Nota Fiscal [" + identNFe.NF.ToString() + "] - Cliente [" + identNFe.Cliente.RazaoSocial.Trim() + "]";
                #endregion
                #region Botao Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\excluir.png";
                imgExcluir.CommandArgument = identNFe.CodNF.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Cancelar dados da Nota Fiscal [" + identNFe.NF.ToString() + "] - Cliente [" + identNFe.Cliente.RazaoSocial.Trim() + "]";
                #endregion
                #region Botao Status
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                imgStatus.Visible = true; 
                switch (identNFe.Status)
                {
                    case 0:
                        imgStatus.ImageUrl = "";
                        imgStatus.Visible = false;
                        break;
                    case 1:
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\NFeOk.png";
                        break;
                    case 2:
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\NFeFail.png";
                    break;
                }
                //imgStatus.ToolTip = "Enviar dados da Nota Fiscal [" + identNFe.NF.ToString() + "] - Cliente [" + identNFe.Cliente.RazaoSocial.Trim() + "]";
                #endregion
                #region Botao Emitir duplicata
                ImageButton imgDuplicata = (ImageButton)e.Row.FindControl("imgDuplicata");
                imgDuplicata.ToolTip = "Emissão de Duplicata da nota fiscal [" + identNFe.CodNF + "]";
                imgDuplicata.Attributes.Add("onclick", "ChamaDuplicata(" + identNFe.CodNF + ")");
                imgDuplicata.CommandArgument = identNFe.CodNF.ToString();
                imgDuplicata.CommandName = "Duplicata";
                #endregion
                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";
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

    }
}
