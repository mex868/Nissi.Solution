using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;
using Nissi.Util;
using BS = Nissi.Business;
using System.Globalization;

namespace Nissi.WebPresentation.EntradaEstoque
{
    public partial class ListaEntradaEstoque : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["CodEntradaEstoque"] != null || Request.QueryString["opcao"] != null)
                {
                    hdfCodEntradaEstoque.Value = Request.QueryString["CodEntradaEstoque"] ?? string.Empty;
                    hdfOpcao.Value = Request.QueryString["opcao"] ?? string.Empty;
                    if (!string.IsNullOrEmpty(hdfOpcao.Value))
                    {
                        hdfValor.Value = Request.QueryString["valor"] ?? string.Empty;
                        EscolherOpcao(hdfOpcao.Value);
                    }
                    Pesquisar();
                }

                else
                {
                    DateTime hoje = DateTime.Now.Date;
                    // Agora obtém o primeiro dia do mês corrente
                    DateTime primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month-1, 01);
                    // Adiciona 1 mês no primeiro dia do mês corrente, para obtermos 
                    // o primeiro dia do próximo mes
                    DateTime primeiroDiaDoProximoMes = primeiroDiaDoMes.AddMonths(2);
                    // Finalmente, diminuimos 1 dia do primeiro dia do próximo mês,
                    // e deixamos a classe DateTime calcular o último dia do mês anterior :)
                    DateTime ultimoDiaDoMes = primeiroDiaDoProximoMes.AddDays(-1);
                    txtDataInicio.Text = primeiroDiaDoMes.ToString("dd/MM/yyyy");
                    txtDataFim.Text = ultimoDiaDoMes.ToString("dd/MM/yyyy");
                    rbPeriodo.Checked = true;
                    Pesquisar();
                } 
                Master.PosicionarFoco(txtEntradaEstoque);
            }
        }
        private void EscolherOpcao(string opcao)
        {
            string tvar = string.Empty;
            switch (hdfOpcao.Value)
            {
                case "EntradaEstoque":
                    txtEntradaEstoque.Text = hdfValor.Value;
                    rbEntradaEstoque.Checked = true;
                    tvar = "1";
                    break;
                case "Lote":
                    txtLote.Text= hdfValor.Value;
                    rbLote.Checked = true;
                    tvar = "2";
                    break;
                case "RazaoSocial":
                    txtRazaoSocial.Text = hdfValor.Value;
                    rbRazaoSocial.Checked = true;
                    tvar = "3";
                    break;
                case "Corrida":
                    txtCorrida.Text = hdfValor.Value;
                    rbCorrida.Checked = true;
                    tvar = "4";
                    break;
                case "Certificado":
                    txtCertificado.Text = hdfValor.Value;
                    rbCertificado.Checked = true;
                    tvar = "5";
                    break;
            }
            ExecutarScript(new StringBuilder("TipoPesquisa("+tvar+")"));
        }

        #region Pesquisar
        private void Pesquisar()
        {
            int codPessoa;
            int codEntradaEstoque;
            List<ItemEntradaEstoqueVO> itemEntradaEstoqueVos = new List<ItemEntradaEstoqueVO>();
            grdListaResultado.DataSource = null;
            grdListaResultado.DataBind();

            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value) && !hdfIdRazaoSocial.Equals("null"))
            {
                codPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
                itemEntradaEstoqueVos = new BS.EntradaEstoque().ListarPorFornecedor(codPessoa);
            }
            else
            {
                if (rbEntradaEstoque.Checked && !string.IsNullOrEmpty(txtEntradaEstoque.Text))
                {
                    codEntradaEstoque = int.Parse(txtEntradaEstoque.Text);
                    itemEntradaEstoqueVos = new BS.EntradaEstoque().ListarPorCodigo(codEntradaEstoque);
                }

                if (rbLote.Checked && !string.IsNullOrEmpty(txtLote.Text))
                {
                    itemEntradaEstoqueVos = new BS.EntradaEstoque().ListarPorLote(txtLote.Text);
                }
                if (!string.IsNullOrEmpty(hdfCodEntradaEstoque.Value))
                {
                    codEntradaEstoque = int.Parse(hdfCodEntradaEstoque.Value);
                    itemEntradaEstoqueVos = new BS.EntradaEstoque().ListarPorCodigo(codEntradaEstoque);
                }
                if (rbCorrida.Checked && !string.IsNullOrEmpty(txtCorrida.Text))
                {
                    itemEntradaEstoqueVos = new BS.EntradaEstoque().ListarPorCorrida(txtCorrida.Text);
                }
                if (rbCertificado.Checked && !string.IsNullOrEmpty(txtCertificado.Text))
                {
                    itemEntradaEstoqueVos = new BS.EntradaEstoque().ListarPorCertificado(txtCertificado.Text);
                }
                if (rbPeriodo.Checked)
                {
                    DateTime dataInicio = DateTime.Parse(txtDataInicio.Text);
                    DateTime dataFim = DateTime.Parse(txtDataFim.Text);
                    itemEntradaEstoqueVos = new BS.EntradaEstoque().ListarPorData(dataInicio, dataFim);
                }
            }

            if (itemEntradaEstoqueVos.Count > 0)
            {
                //grdListaResultado.DataSource = itemEntradaEstoqueVos;
                //grdListaResultado.DataBind();
                grdListaResultado1.Show();
                StoreListaResultado.DataSource = itemEntradaEstoqueVos;
                StoreListaResultado.DataBind();
            }
            else
            {
                MensagemCliente("Não existem registros para o filtro informado.");
            }
            hdfIdRazaoSocial.Value = string.Empty;
        }

        #endregion

        #region Métodos do Grid
        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Page"))
            {
                ItemEntradaEstoqueVO itemEntradaEstoqueVo = new ItemEntradaEstoqueVO();
                string[] args = e.CommandArgument.ToString().Split('|');
                itemEntradaEstoqueVo.EntradaEstoqueVo.CodEntradaEstoque = int.Parse(args[0]);
                switch (e.CommandName)
                {
                        //Modulo de Edicao
                    case "Editar":
                        if (rbEntradaEstoque.Checked && !string.IsNullOrEmpty(txtEntradaEstoque.Text))
                        {
                            hdfValor.Value = txtEntradaEstoque.Text;
                            hdfOpcao.Value = "EntradaEstoque";
                        }
                        if (rbLote.Checked && !string.IsNullOrEmpty(txtLote.Text))
                        {
                            hdfValor.Value = txtLote.Text;
                            hdfOpcao.Value = "Lote";
                        }
                        if (rbRazaoSocial.Checked && !string.IsNullOrEmpty(txtRazaoSocial.Text))
                        {
                            hdfValor.Value = txtRazaoSocial.Text;
                            hdfOpcao.Value = "RazaoSocial";
                        }
                        if (rbCorrida.Checked && !string.IsNullOrEmpty(txtCorrida.Text))
                        {
                            hdfValor.Value = txtCorrida.Text;
                            hdfOpcao.Value = "Corrida";
                        }
                        if (rbCertificado.Checked && !string.IsNullOrEmpty(txtCertificado.Text))
                        {
                            hdfValor.Value = txtCertificado.Text;
                            hdfOpcao.Value = "Certificado";
                        }
                        Response.Redirect("CadastraEntradaEstoque.aspx?acao=Editar&CodEntradaEstoque=" +
                                          itemEntradaEstoqueVo.EntradaEstoqueVo.CodEntradaEstoque + "&tipo=" + args[1] +
                                          "&valor=" + hdfValor.Value + "&opcao=" + hdfOpcao.Value);
                        break;
                        //Modulo de Excluir
                    case "Excluir":
                        new Business.EntradaEstoque().Excluir(itemEntradaEstoqueVo.EntradaEstoqueVo.CodEntradaEstoque);
                        //Atualizar Lista
                        Pesquisar();
                        break;
                        //Modulo de Imprimir
                    case "Imprimir":
                        break;
                        //Modulo de Enviar
                    case "Enviar":
                        break;
                }
            }

        }
        
        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ItemEntradaEstoqueVO itemEntradaEstoqueVo = (ItemEntradaEstoqueVO)e.Row.DataItem;
                e.Row.Cells[1].Text = itemEntradaEstoqueVo.Lote.ToString();
                e.Row.Cells[2].Text = itemEntradaEstoqueVo.Corrida;
                e.Row.Cells[3].Text = itemEntradaEstoqueVo.Certificado;
                e.Row.Cells[4].Text = itemEntradaEstoqueVo.EntradaEstoqueVo.Fornecedor.RazaoSocial;
                e.Row.Cells[5].Text = itemEntradaEstoqueVo.MateriaPrimaVo.Descricao;
                e.Row.Cells[6].Text = itemEntradaEstoqueVo.Qtd.ToString();
                e.Row.Cells[7].Text = itemEntradaEstoqueVo.EntradaEstoqueVo.DataEntrada.ToString("dd/MM/yyyy");
                #region Botao Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = itemEntradaEstoqueVo.EntradaEstoqueVo.CodEntradaEstoque + "|" + itemEntradaEstoqueVo.EntradaEstoqueVo.Tipo;
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados da Nota Fiscal [" + itemEntradaEstoqueVo.EntradaEstoqueVo.CodEntradaEstoque.ToString().PadLeft(8, '0') + "] - Fornecedor [" + itemEntradaEstoqueVo.EntradaEstoqueVo.Fornecedor.RazaoSocial.Trim() + "]";
                #endregion
                #region Botao Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\excluir.png";
                imgExcluir.CommandArgument = itemEntradaEstoqueVo.EntradaEstoqueVo.CodEntradaEstoque.ToString();
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar cancelamento da Entrada de Estoque do Fornecedor [" + itemEntradaEstoqueVo.EntradaEstoqueVo.Fornecedor.RazaoSocial.Trim() + "]?');";
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Cancelar dados da Nota Fiscal [" + itemEntradaEstoqueVo.EntradaEstoqueVo.CodEntradaEstoque.ToString().PadLeft(8, '0') + "] - Fornecedor [" + itemEntradaEstoqueVo. EntradaEstoqueVo.Fornecedor.RazaoSocial.Trim() + "]";
                #endregion
                /*#region Botao Status
                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                imgStatus.Visible = true; 
                switch (itemEntradaEstoqueVo.CodEntradaEstoque.IndStatus)
                {
                    case "0":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\Enviar.png";
                        imgStatus.ToolTip = "Aguardando Envio";
                        break;
                    case "1":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\EntradaEstoqueOk.png";
                        imgStatus.ToolTip = "Autorizado o uso da CodEntradaEstoque-e";
                        break;
                    case "2":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\EntradaEstoqueFail.png";
                        imgStatus.ToolTip = "Erro no Schema";
                        e.Row.CssClass = "FundoLinha3";
                        break;
                    case "3":
                        imgStatus.ImageUrl = caminhoAplicacao + @"Imagens\EntradaEstoqueFail.png";
                        imgStatus.ToolTip = "Cancelado o uso da CodEntradaEstoque-e";
                        e.Row.CssClass = "FundoLinha3";
                        break;
                }
                //imgStatus.ToolTip = "Enviar dados da Nota Fiscal [" + identEntradaEstoque.CodEntradaEstoque.ToString() + "] - Fornecedor [" + identEntradaEstoque.Fornecedor.RazaoSocial.Trim() + "]";
                #endregion
                #region Botao Emitir duplicata
                ImageButton imgDuplicata = (ImageButton)e.Row.FindControl("imgDuplicata");
                imgDuplicata.ToolTip = "Emissão de Duplicata da nota fiscal [" + itemEntradaEstoqueVo.CodEntradaEstoque.ToString().PadLeft(8, '0') + "]";
                imgDuplicata.Attributes.Add("onclick", "ChamaDuplicata(" + itemEntradaEstoqueVo.CodEntradaEstoque.ToString() + ")");
                imgDuplicata.CommandArgument = itemEntradaEstoqueVo.CodEntradaEstoque.ToString();
                imgDuplicata.CommandName = "Duplicata";
                #endregion
                #region Botao Visualizar
                ImageButton imgVisualizarEntradaEstoque = (ImageButton)e.Row.FindControl("imgVisualizarEntradaEstoque");
                imgVisualizarEntradaEstoque.ImageUrl = caminhoAplicacao + @"Imagens\apps.png";
                imgVisualizarEntradaEstoque.ToolTip = "Visualizar nota fiscal [" + itemEntradaEstoqueVo.EntradaEstoque.ToString().PadLeft(8, '0') + "]";
                imgVisualizarEntradaEstoque.Attributes.Add("onclick", "ChamaVisualizarEntradaEstoque(" + itemEntradaEstoqueVo.CodEntradaEstoque + ")");
                imgVisualizarEntradaEstoque.CommandArgument = itemEntradaEstoqueVo.CodEntradaEstoque.ToString();
                imgVisualizarEntradaEstoque.CommandName = "Visualizar";
                #endregion
                 */
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
            hdfCodEntradaEstoque.Value = string.Empty;
            Pesquisar();
        }        
        #endregion

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastraEntradaEstoque.aspx?acao=Incluir");
        }

        protected void rbCNPJ_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void StoreListaResultado_RefreshData(object sender, Ext.Net.StoreRefreshDataEventArgs e)
        {
            Pesquisar();
        }
    
    }
}
