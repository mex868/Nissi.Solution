using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;
using System.Globalization;

namespace Nissi.WebPresentation.Produto
{
    public partial class ListaProduto : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string codigo = null;
            string descricao = null;
            DateTime? dataini = Convert.ToDateTime(tbxDataIni.Text);
            DateTime? datafim = Convert.ToDateTime(tbxDataFim.Text);
            if (rbCodigo.Checked)
            {
                codigo = txtCodigoDescricao.Text;
            }
            else
                descricao = txtCodigoDescricao.Text;

            List<ProdutoNFVO> lstProdutoNF= new ProdutoNF().Lista(codigo, descricao, dataini, datafim);
            if (lstProdutoNF.Count > 0)
            {
                grdListaResultado.DataSource = lstProdutoNF;
                grdListaResultado.DataBind();
            }
            else
                MensagemCliente("Não existem registros para o filtro informado.");
        }

        #region Métodos do Grid
        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProdutoNFVO identProdutoNF = new ProdutoNFVO();
            identProdutoNF.NF = int.Parse(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                //Modulo de Edicao
                case "Editar":
                    //Response.Redirect("CadastraNFe.aspx?acao=Editar&CodNF=" + identNFe.CodNF.ToString());
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
        private decimal TotalGeral = 0;
        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProdutoNFVO identProdutoNF = (ProdutoNFVO)e.Row.DataItem;
                e   .Row.Cells[1].Text = identProdutoNF.NF.ToString().PadLeft(8,'0');
                e.Row.Cells[2].Text = identProdutoNF.DataEmissao.Value.ToString("dd/MM/yyyy");
                e.Row.Cells[3].Text = identProdutoNF.Codigo;
                e.Row.Cells[4].Text = identProdutoNF.Descricao;
                e.Row.Cells[5].Text = identProdutoNF.Qtd.ToString();
                CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");
                string specifier = "C";
                e.Row.Cells[6].Text = identProdutoNF.Valor.Value.ToString(specifier, culture);
                e.Row.Cells[7].Text = identProdutoNF.TotalItem.Value.ToString(specifier, culture);
                TotalGeral += identProdutoNF.TotalItem.Value;
                #region Botao Emitir duplicata
                ImageButton imgVisualizar = (ImageButton)e.Row.FindControl("imgVisualizar");
                imgVisualizar.ImageUrl = caminhoAplicacao + @"Imagens\btn-SolicitacaoDocumentos.gif";
                imgVisualizar.ToolTip = "Emissão de Duplicata da nota fiscal [" + identProdutoNF.NF + "]";
                //imgDuplicata.Attributes.Add("onclick", "ChamaDuplicata(" + identNFe.CodNF + ")");
                imgVisualizar.CommandArgument = identProdutoNF.NF.ToString();
                imgVisualizar.CommandName = "Duplicata";
                #endregion
                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";
                
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "Total de Vendas";
                e.Row.Cells[6].Attributes.Add("align", "left");
                e.Row.Cells[7].Attributes.Add("align", "right");
                e.Row.Cells[7].Text = TotalGeral.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
            }
        }
        #endregion
    }
}