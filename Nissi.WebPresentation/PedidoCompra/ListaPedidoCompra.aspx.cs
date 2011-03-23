using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Nissi.Business;
using Nissi.Model;
using Nissi.Util;
using BS = Nissi.Business;
using System.Globalization;
using ImageButton = System.Web.UI.WebControls.ImageButton;

namespace Nissi.WebPresentation.PedidoCompra
{
    public partial class ListaPedidoCompra : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["CodPedidoCompra"] != null || Request.QueryString["opcao"] != null)
                {
                    hdfCodPedidoCompra.Value = Request.QueryString["CodPedidoCompra"] ?? string.Empty;
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
                CarregarCombos();
                Master.PosicionarFoco(txtPedidoCompra);
            }
        }
        private void EscolherOpcao(string opcao)
        {
            string tvar = string.Empty;
            switch (hdfOpcao.Value)
            {
                case "PedidoCompra":
                    txtPedidoCompra.Text = hdfValor.Value;
                    rbPedidoCompra.Checked = true;
                    tvar = "1";
                    break;
                case "Bitola":
                    ddlBitola.SelectedValue = hdfValor.Value;
                    rbBitola.Checked = true;
                    tvar = "2";
                    break;
                case "RazaoSocial":
                    txtRazaoSocial.Text = hdfValor.Value;
                    rbRazaoSocial.Checked = true;
                    tvar = "3";
                    break;
                case "ClasseTipo":
                    ddlClasseTipo.SelectedValue = hdfValor.Value;
                    rbClasseTipo.Checked = true;
                    tvar = "5";
                    break;
                
            }
            ExecutarScript(new StringBuilder("TipoPesquisa("+tvar+")"));
        }
        protected void btnPesquisarExt_Click(object sender, DirectEventArgs e)
        {
            Pesquisar();
        }

        #region Pesquisar
        private void Pesquisar()
        {
            int codPessoa;
            int codPedidoCompra;
            var itemPedidoCompraVos = new List<ListItemPedidoCompraVO>();
            grdListaResultado.DataSource = null;
            grdListaResultado.DataBind();
            grdListaResultado1.Hidden = true;

            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value) && !hdfIdRazaoSocial.Equals("null"))
            {
                codPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
                itemPedidoCompraVos = new BS.PedidoCompra().ListarPorFornecedor(codPessoa);
            }
            else
            {
                if (rbPedidoCompra.Checked && !string.IsNullOrEmpty(txtPedidoCompra.Text))
                {
                    codPedidoCompra = int.Parse(txtPedidoCompra.Text);
                    itemPedidoCompraVos = new BS.PedidoCompra().ListarPorCodigo(codPedidoCompra);
                }

                if (rbBitola.Checked && !string.IsNullOrEmpty(ddlBitola.SelectedValue))
                {
                    int codBitola = int.Parse(ddlBitola.SelectedValue);
                    itemPedidoCompraVos = new BS.PedidoCompra().ListarPorBitola(codBitola);

                }
                if (!string.IsNullOrEmpty(hdfCodPedidoCompra.Value))
                {
                    codPedidoCompra = int.Parse(hdfCodPedidoCompra.Value);
                    itemPedidoCompraVos = new BS.PedidoCompra().ListarPorCodigo(codPedidoCompra);
                }
                if (rbClasseTipo.Checked && !string.IsNullOrEmpty(ddlClasseTipo.SelectedValue))
                {
                    int codClasseTipo = int.Parse(ddlClasseTipo.SelectedValue);
                    itemPedidoCompraVos = new BS.PedidoCompra().ListarClasseTipo(codClasseTipo);
                }
                if (rbPeriodo.Checked)
                {
                    DateTime dataInicio = DateTime.Parse(txtDataInicio.Text);
                    DateTime dataFim = DateTime.Parse(txtDataFim.Text);
                    itemPedidoCompraVos = new BS.PedidoCompra().ListarPorData(dataInicio, dataFim);
                }
            }

            if (itemPedidoCompraVos.Count > 0)
            {
                //grdListaResultado.DataSource = itemPedidoCompraVos;
                //grdListaResultado.DataBind();
                grdListaResultado1.Show();
                StoreListaResultado.DataSource = itemPedidoCompraVos;
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
                ItemPedidoCompraVO itemPedidoCompraVo = new ItemPedidoCompraVO();
                string[] args = e.CommandArgument.ToString().Split('|');
                itemPedidoCompraVo.PedidoCompraVo.CodPedidoCompra = int.Parse(args[0]);
                switch (e.CommandName)
                {
                        //Modulo de Edicao
                    case "Editar":
                        if (rbPedidoCompra.Checked && !string.IsNullOrEmpty(txtPedidoCompra.Text))
                        {
                            hdfValor.Value = txtPedidoCompra.Text;
                            hdfOpcao.Value = "PedidoCompra";
                        }
                        if (rbBitola.Checked && !string.IsNullOrEmpty(ddlBitola.SelectedValue))
                        {
                            hdfValor.Value = ddlBitola.SelectedValue;
                            hdfOpcao.Value = "Bitola";
                        }
                        if (rbRazaoSocial.Checked && !string.IsNullOrEmpty(txtRazaoSocial.Text))
                        {
                            hdfValor.Value = txtRazaoSocial.Text;
                            hdfOpcao.Value = "RazaoSocial";
                        }
                        if (rbClasseTipo.Checked && !string.IsNullOrEmpty(ddlClasseTipo.SelectedValue))
                        {
                            hdfValor.Value = ddlClasseTipo.SelectedValue;
                            hdfOpcao.Value = "ClasseTipo";
                        }
                        Response.Redirect("CadastraPedidoCompra.aspx?acao=Editar&CodPedidoCompra=" +
                                          itemPedidoCompraVo.PedidoCompraVo.CodPedidoCompra + "&tipo=" + args[1] +
                                          "&valor=" + hdfValor.Value + "&opcao=" + hdfOpcao.Value);
                        break;
                        //Modulo de Excluir
                    case "Excluir":
                        new Business.PedidoCompra().Excluir(itemPedidoCompraVo.PedidoCompraVo.CodPedidoCompra);
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
                var itemPedidoCompraVo = (ListItemPedidoCompraVO)e.Row.DataItem;
                e.Row.Cells[1].Text = itemPedidoCompraVo.OrdemCompra.ToString().PadLeft(8,'0');
                e.Row.Cells[2].Text = itemPedidoCompraVo.DataEmissao.ToString("dd/MM/yyyy");
                e.Row.Cells[3].Text = itemPedidoCompraVo.Fornecedor;
                e.Row.Cells[4].Text = itemPedidoCompraVo.Bitola.ToString();
                e.Row.Cells[5].Text = itemPedidoCompraVo.MateriaPrimaVo.Descricao;
                e.Row.Cells[6].Text = itemPedidoCompraVo.Preco.ToString();
                e.Row.Cells[7].Text = itemPedidoCompraVo.Ipi.ToString();
                e.Row.Cells[8].Text = itemPedidoCompraVo.Qtde.ToString();
                if (itemPedidoCompraVo.DataPrevista != null)
                    e.Row.Cells[9].Text = itemPedidoCompraVo.DataPrevista.Value.ToString("dd/MM/yyyy");
                e.Row.Cells[10].Text = itemPedidoCompraVo.QtdeEntregue.ToString();
                e.Row.Cells[10].Attributes.Add("align", "right");
                e.Row.Cells[11].Text = itemPedidoCompraVo.Saldo.ToString();
                e.Row.Cells[11].Attributes.Add("align", "right");
                switch (itemPedidoCompraVo.Situacao)
                {
                    case "Aberto":
                        e.Row.Cells[12].CssClass = "verde";
                        break;
                    case "Atrasado":
                        e.Row.Cells[12].CssClass = "vermelho";
                        break;
                    case "Entregue em Atraso":
                        e.Row.Cells[12].CssClass = "vermelho";
                        break;
                }
                e.Row.Cells[12].Text = itemPedidoCompraVo.Situacao;
                e.Row.Cells[12].Attributes.Add("align", "right");
                e.Row.Cells[13].CssClass = itemPedidoCompraVo.DiaEmAtraso < 0 ? "vermelho" : "verde";
                e.Row.Cells[13].Text = itemPedidoCompraVo.DiaEmAtraso.ToString();
                #region Botao Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = itemPedidoCompraVo.OrdemCompra+"|"+itemPedidoCompraVo.Tipo;
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Pedido de Compra [" + itemPedidoCompraVo.OrdemCompra.ToString().PadLeft(8, '0') + "] - Fornecedor [" + itemPedidoCompraVo.Fornecedor.Trim() + "]";
                #endregion
                #region Botao Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\excluir.png";
                imgExcluir.CommandArgument = itemPedidoCompraVo.OrdemCompra.ToString();
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar cancelamento do Pedido de Compra [" + itemPedidoCompraVo.OrdemCompra.ToString().PadLeft(8, '0') + "] - Fornecedor [" + itemPedidoCompraVo.Fornecedor.Trim() + "]?');";

                imgExcluir.CommandName = "Excluir";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Cancelar dados do Pedido de Compra [" + itemPedidoCompraVo.OrdemCompra.ToString().PadLeft(8, '0') + "] - Fornecedor [" + itemPedidoCompraVo.Fornecedor.Trim() + "]";
                #endregion
                #region Botao Imprimir
                ImageButton imgImprimir = (ImageButton)e.Row.FindControl("imgImprimir");
                imgImprimir.ImageUrl = caminhoAplicacao + @"Imagens\Imprimir.png";
                imgImprimir.ToolTip = "Emissão de Pedido de Compra [" + itemPedidoCompraVo.OrdemCompra.ToString().PadLeft(8, '0') + "]";
                imgImprimir.Attributes.Add("onclick", "ChamaPedidoCompra(" + itemPedidoCompraVo.OrdemCompra + ")");
                imgImprimir.CommandArgument = itemPedidoCompraVo.OrdemCompra.ToString();
                imgImprimir.CommandName = "Imprimir";
                #endregion
                #region Botao EnviarEmail
                ImageButton imgEnviarEmail = (ImageButton)e.Row.FindControl("imgEnviarEmail");
                imgEnviarEmail.ImageUrl = caminhoAplicacao + @"Imagens\Enviar.png";
                imgEnviarEmail.ToolTip = "Enviar  Pedido de Compra por E-mail [" + itemPedidoCompraVo.OrdemCompra.ToString().PadLeft(8, '0') + "]";
                imgEnviarEmail.Attributes.Add("onclick", "EnviarPedidoCompra(" + itemPedidoCompraVo.OrdemCompra + ","+itemPedidoCompraVo.CodPessoa+")");
                imgEnviarEmail.CommandArgument = itemPedidoCompraVo.OrdemCompra.ToString();
                imgEnviarEmail.CommandName = "Enviar";
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
            hdfCodPedidoCompra.Value = string.Empty;
            Pesquisar();
        }        
        #endregion

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastraPedidoCompra.aspx?acao=Incluir");
        }

        protected void rbCNPJ_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
        private void CarregarCombos()
        {
            Geral.CarregarDDL(ref ddlClasseTipo, new ClasseTipo().Listar().ToArray(), "CodClasseTipo", "Descricao");
            Geral.CarregarDDL(ref ddlBitola, new Bitola().Listar().ToArray(), "CodBitola", "Bitola");
        }

        protected void StoreListaResultado_RefreshData(object sender, Ext.Net.StoreRefreshDataEventArgs e)
        {
            Pesquisar();
        }        
    }
}
