using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            this.pnl2.CustomConfig.Add(new Ext.Net.ConfigItem("submitUrl", this.ResolveUrl("~/WebService.asmx/UploadFileAjax"), Ext.Net.ParameterMode.Value));
        }

        #region Propriedades
        //private FornecedorVO DadosRazaoSocial
        //{
        //    set
        //    {
        //        if (value.CodPessoa > 0)
        //        {
        //            hdfCodigoFornecedor.Value = value.CodRef;
        //            hdfIdRazaoSocial.Value = value.CodPessoa.ToString();
        //            txtRazaoSocial.Text = value.RazaoSocial;
        //            txtCEP.Text = value.Cep.CodCep;
        //            txtCNPJ.Text = value.CNPJ;
        //            txtInscricaoEstatual.Text = value.InscricaoEstadual;
        //            txtEndereco.Text = value.Cep.NomEndereco +
        //                                                (value.Numero.Trim().Equals("") ? "" : ", " + value.Numero) +
        //                                                (value.Complemento.Trim().Equals("") ? "" : " - " + value.Complemento);
        //            txtBairro.Text = value.Cep.Bairro.NomBairro;
        //            txtMunicipio.Text = value.Cep.Cidade.NomCidade;
        //            txtUF.Text = value.Cep.Cidade.UF.CodUF;
        //            txtFoneFax.Text = value.Telefone + (value.Fax.Trim().Equals("") ? " " : " / " + value.Fax);
        //            txtContato.Text = value.Contato;
        //            txtEmail.Text = value.Email;
        //            txtSite.Text = value.Site;
        //        }
        //    }
        //}
        private EntradaEstoqueVO DadosEntradaEstoque
        {
            set
            {
                txtPedidoCompra.Text = value.PedidoCompra.CodPedidoCompra.ToString().PadLeft(8, '0');
                txtNotaFiscalItem.Text = value.NotaFiscal.ToString().PadLeft(8, '0');
                if (value.DataEntrada != null)
                    txtData.Text = value.DataEntrada.ToString("dd/MM/yyyy");
                if (value.DataNotaFiscal != null)
                    txtDataNotaFiscalItem.Text = value.DataNotaFiscal.ToString("dd/MM/yyyy");
                //DadosRazaoSocial = value.Fornecedor;
                //grdProduto.DataSource = value.Itens;
                //grdProduto.DataBind();
            }
            get
            {
                EntradaEstoqueVO identEntradaEstoqueVo = new EntradaEstoqueVO();
                //if (!string.IsNullOrEmpty(hdfCodEntradaEstoque.Value))
                //    identEntradaEstoqueVo.CodEntradaEstoque = int.Parse(hdfCodEntradaEstoque.Value);
                identEntradaEstoqueVo.PedidoCompra.CodPedidoCompra = int.Parse(hdfCodPedidoCompraItem.Text);
                if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                    identEntradaEstoqueVo.Fornecedor.CodPessoa = int.Parse(hdfIdRazaoSocial.Value);
                //txtEntradaEstoque.Text = identEntradaEstoqueVo.CodEntradaEstoque.ToString().PadLeft(8, '0');
                if (!string.IsNullOrEmpty(txtData.Text))
                    identEntradaEstoqueVo.DataEntrada = Convert.ToDateTime(txtData.Text);
                if (!string.IsNullOrEmpty(txtDataNotaFiscalItem.Text))
                    identEntradaEstoqueVo.DataNotaFiscal = Convert.ToDateTime(txtDataNotaFiscalItem.Text);
                if (!string.IsNullOrEmpty(txtNotaFiscalItem.Text))
                    identEntradaEstoqueVo.NotaFiscal = txtNotaFiscalItem.Text;
                identEntradaEstoqueVo.Itens = itemEntradaEstoqueVOs;
                return identEntradaEstoqueVo;
            }
        }

        private List<ItemEntradaEstoqueVO> itemEntradaEstoqueVOs
        {
            get
            {
                //será incluido no grid de ICMS manualmente (não incluirá no banco ainda)
                //pois só deverá ser incluido no banco quando salvar o produto

                //armazena em viewstate a linha selecionada para posterior atualização
                ItemEntradaEstoqueVO[] lstItemEntradaEstoque = (ItemEntradaEstoqueVO[])Session["lstItemEntradaEstoque"];
                List<ItemEntradaEstoqueVO> newlstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>(lstItemEntradaEstoque);
                int codMateriaPrima = int.Parse(hdfCodMateriaPrima.Text);
                int codBitola = int.Parse(hdfCodBitola.Text);
                decimal qtde = !string.IsNullOrEmpty(txtQtde.Text) ? decimal.Parse(txtQtde.Text) : 0;
                decimal valor = !string.IsNullOrEmpty(txtValorUnit.Text) ? decimal.Parse(txtValorUnit.Text) : 0;
                string norma = txtNorma.Text;
                decimal bitola = !string.IsNullOrEmpty(txtBitola.Text) ? decimal.Parse(txtBitola.Text) : 0;
                string unidade = ddlUnidade.SelectedItem.Text;
                string especificacao = txtEspecificacao.Text;
                decimal Ipi = !string.IsNullOrEmpty(txtIPI.Text) ? decimal.Parse(txtIPI.Text) : 0;
                //se for edição de ICMS, atualizar o list
                if (hdfTipoAcaoItem.Value.Equals("Incluir"))
                {
                    /************************************************************************
                    Se a ação for inclusão, simplesmente verifica se o ítem já foi cadastrado
                    se já for, exibe mensagem e não inclui o ítem
                    /***********************************************************************/


                    //senão, incluir novo ítem no list
                    ItemEntradaEstoqueVO lstItemEntradaEstoqueAux = new ItemEntradaEstoqueVO();
                    lstItemEntradaEstoqueAux.Lote = int.Parse(txtLote.Text);
                    lstItemEntradaEstoqueAux.Certificado = txtCertificado.Text;
                    lstItemEntradaEstoqueAux.Corrida = txtCorrida.Text;
                    lstItemEntradaEstoqueAux.MateriaPrimaVo.CodMateriaPrima = codMateriaPrima;
                    lstItemEntradaEstoqueAux.MateriaPrimaVo.NormaVo.Descricao = norma;
                    lstItemEntradaEstoqueAux.BitolaVo.CodBitola = codBitola;
                    lstItemEntradaEstoqueAux.BitolaVo.Bitola = bitola;
                    lstItemEntradaEstoqueAux.Qtd = qtde;
                    lstItemEntradaEstoqueAux.UnidadeVo.TipoUnidade = unidade;
                    lstItemEntradaEstoqueAux.Especificacao = especificacao;
                    lstItemEntradaEstoqueAux.Ipi = Ipi;
                    lstItemEntradaEstoqueAux.Valor = valor;
                    lstItemEntradaEstoqueAux.C = !string.IsNullOrEmpty(txtC.Text) ? decimal.Parse(txtC.Text) : 0;
                    lstItemEntradaEstoqueAux.Si = !string.IsNullOrEmpty(txtSi.Text) ? decimal.Parse(txtSi.Text) : 0;
                    lstItemEntradaEstoqueAux.Mn = !string.IsNullOrEmpty(txtMn.Text) ? decimal.Parse(txtMn.Text) : 0;
                    lstItemEntradaEstoqueAux.P = !string.IsNullOrEmpty(txtP.Text) ? decimal.Parse(txtP.Text) : 0;
                    lstItemEntradaEstoqueAux.S = !string.IsNullOrEmpty(txtS.Text) ? decimal.Parse(txtS.Text) : 0;
                    lstItemEntradaEstoqueAux.Cr = !string.IsNullOrEmpty(txtCr.Text) ? decimal.Parse(txtCr.Text) : 0;
                    lstItemEntradaEstoqueAux.Ni = !string.IsNullOrEmpty(txtNi.Text) ? decimal.Parse(txtNi.Text) : 0;
                    lstItemEntradaEstoqueAux.Mo = !string.IsNullOrEmpty(txtMo.Text) ? decimal.Parse(txtMo.Text) : 0;
                    lstItemEntradaEstoqueAux.Cu = !string.IsNullOrEmpty(txtCu.Text) ? decimal.Parse(txtCu.Text) : 0;
                    lstItemEntradaEstoqueAux.Ti = !string.IsNullOrEmpty(txtTi.Text) ? decimal.Parse(txtTi.Text) : 0;
                    lstItemEntradaEstoqueAux.N2 = !string.IsNullOrEmpty(txtN2.Text) ? decimal.Parse(txtN2.Text) : 0;
                    lstItemEntradaEstoqueAux.Co = !string.IsNullOrEmpty(txtCo.Text) ? decimal.Parse(txtCo.Text) : 0;
                    lstItemEntradaEstoqueAux.Al = !string.IsNullOrEmpty(txtAl.Text) ? decimal.Parse(txtAl.Text) : 0;
                    lstItemEntradaEstoqueAux.Resistencia = !string.IsNullOrEmpty(txtResistenciaTracao.Text) ? decimal.Parse(txtResistenciaTracao.Text) : 0;
                    lstItemEntradaEstoqueAux.Dureza = !string.IsNullOrEmpty(txtDureza.Text) ? decimal.Parse(txtDureza.Text) : 0;
                    lstItemEntradaEstoqueAux.Nota = !string.IsNullOrEmpty(txtNota.Text) ? decimal.Parse(txtNota.Text) : 0;
                    lstItemEntradaEstoqueAux.CertificadoScanneado = (byte[])ViewState[key_Pdf];

                    newlstItemEntradaEstoque.Add(lstItemEntradaEstoqueAux);
                }
                else
                {
                    /************************************************************************
                    Atualiza o item do grid        
                    /***********************************************************************/
                    var item =
                        newlstItemEntradaEstoque.Where(r => r.MateriaPrimaVo.CodMateriaPrima == codMateriaPrima && r.BitolaVo.CodBitola == codBitola).Select(r => r).
                            FirstOrDefault();
                    item.MateriaPrimaVo.CodMateriaPrima = codMateriaPrima;
                    item.MateriaPrimaVo.NormaVo.Descricao = norma;
                    item.BitolaVo.CodBitola = codBitola;
                    item.BitolaVo.Bitola = bitola;
                    item.Lote = int.Parse(txtLote.Text);
                    item.Certificado = txtCertificado.Text;
                    item.Corrida = txtCorrida.Text;
                    item.Qtd = qtde;
                    item.UnidadeVo.TipoUnidade = unidade;
                    item.Especificacao = especificacao;
                    item.Ipi = Ipi;
                    item.Valor = valor;
                    item.C = !string.IsNullOrEmpty(txtC.Text) ? decimal.Parse(txtC.Text) : 0;
                    item.Si = !string.IsNullOrEmpty(txtSi.Text) ? decimal.Parse(txtSi.Text) : 0;
                    item.Mn = !string.IsNullOrEmpty(txtMn.Text) ? decimal.Parse(txtMn.Text) : 0;
                    item.P = !string.IsNullOrEmpty(txtP.Text) ? decimal.Parse(txtP.Text) : 0;
                    item.S = !string.IsNullOrEmpty(txtS.Text) ? decimal.Parse(txtS.Text) : 0;
                    item.Cr = !string.IsNullOrEmpty(txtCr.Text) ? decimal.Parse(txtCr.Text) : 0;
                    item.Ni = !string.IsNullOrEmpty(txtNi.Text) ? decimal.Parse(txtNi.Text) : 0;
                    item.Mo = !string.IsNullOrEmpty(txtMo.Text) ? decimal.Parse(txtMo.Text) : 0;
                    item.Cu = !string.IsNullOrEmpty(txtCu.Text) ? decimal.Parse(txtCu.Text) : 0;
                    item.Ti = !string.IsNullOrEmpty(txtTi.Text) ? decimal.Parse(txtTi.Text) : 0;
                    item.N2 = !string.IsNullOrEmpty(txtN2.Text) ? decimal.Parse(txtN2.Text) : 0;
                    item.Co = !string.IsNullOrEmpty(txtCo.Text) ? decimal.Parse(txtCo.Text) : 0;
                    item.Al = !string.IsNullOrEmpty(txtAl.Text) ? decimal.Parse(txtAl.Text) : 0;
                    item.Resistencia = !string.IsNullOrEmpty(txtResistenciaTracao.Text) ? decimal.Parse(txtResistenciaTracao.Text) : 0;
                    item.Dureza = !string.IsNullOrEmpty(txtDureza.Text) ? decimal.Parse(txtDureza.Text) : 0;
                    item.Nota = !string.IsNullOrEmpty(txtNota.Text) ? decimal.Parse(txtNota.Text) : 0;
                    item.CertificadoScanneado = (byte[])ViewState[key_Pdf];

                    //sai do for
                }
                return newlstItemEntradaEstoque;
                
                //mpeIncluirItem.Hide();               
            }
        }
        #endregion
        protected void Cell_Click(object sender, DirectEventArgs e)
        {
            RowSelectionModel sm = this.grdListaResultado1.SelectionModel.Primary as RowSelectionModel;
            if (sm != null) sm.BackColor = Color.Red;
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
            Geral.CarregarSelectBox(ref ddlUnidade, new Unidade().Listar(new UnidadeVO()).ToArray(),"CodUnidade","TipoUnidade");
        }

        protected void StoreListaResultado_RefreshData(object sender, Ext.Net.StoreRefreshDataEventArgs e)
        {
            Pesquisar();
        }

        protected void btnIncluirItem_Click(object sender, EventArgs e)
        {
            CreateEntrada(TypePedido.Compra);
            LimparCamposItemEntradaEstoque();
        }

        private int CreateEntrada(TypePedido typePedido)
        {
            int codEntradaEstoque = 0;
            switch (typePedido)
            {
                case TypePedido.Compra:
                    codEntradaEstoque = new BS.EntradaEstoque().Incluir(DadosEntradaEstoque.Fornecedor.CodPessoa,
                                                                 DadosEntradaEstoque.PedidoCompra.CodPedidoCompra,
                                                                 DadosEntradaEstoque.DataNotaFiscal,
                                                                 DadosEntradaEstoque.DataEntrada,
                                                                 DadosEntradaEstoque.NotaFiscal,
                                                                 UsuarioAtivo.CodFuncionario.Value,
                                                                 DadosEntradaEstoque.Itens, typePedido);
                    break;
                //case TypePedido.CompraInsumo:
                //    codEntradaEstoque = new EntradaEstoque().Incluir(DadosEntradaEstoqueInsumo.Fornecedor.CodPessoa,
                //                                                 DadosEntradaEstoqueInsumo.PedidoCompra.CodPedidoCompra,
                //                                                 DadosEntradaEstoqueInsumo.DataNotaFiscal,
                //                                                 DadosEntradaEstoqueInsumo.DataEntrada,
                //                                                 DadosEntradaEstoqueInsumo.NotaFiscal,
                //                                                 UsuarioAtivo.CodFuncionario.Value,
                //                                                 DadosEntradaEstoqueInsumo.Itens, typePedido);
                //    break;
            }
            return codEntradaEstoque;
        }

        private const string key_Pdf = "PDF";
        protected void btnCarregarImagem_Click(object sender, EventArgs e)
        {
            //btnCarregarImagem.Attributes.Add("onclick", "return ValidaArquivoImagem();");
            if ((upFileUp.PostedFile == null) || (upFileUp.PostedFile.ContentLength == 0)
                || !upFileUp.PostedFile.ContentType.Contains("pdf"))
            {
                //mpeIncluirItem.Show();
                MensagemCliente(updBotoes, "Informe um arquivo de pdf válido");
            }
            else
            {
               //mpeIncluirItem.Show();
                Stream Input = upFileUp.PostedFile.InputStream;
                // Inicializa o buffer			
                byte[] certificadoPdf = new byte[Input.Length];
                // Lê a imagem do arquivo          			 
                Input.Read(certificadoPdf, 0, Convert.ToInt32(Input.Length));
                // Joga no ViewState
                ViewState[key_Pdf] = certificadoPdf;
                lkbArquivoPdf.Text = "(Arquivo carregado)";
                string sVarCache = "PDF";
                Cache[sVarCache] = certificadoPdf;
            }
        }
        protected void lkbArquivoPdf_Click(object sender, EventArgs e)
        {
            Response.Redirect("GerarPDF.aspx?Variavel_Cache=PDF");
        }

        private void LimparCamposItemEntradaEstoque()
        {
            hdfTipoAcaoItem.Value = "Incluir";
            hdfCodMateriaPrima.Value =
            hdfCodBitola.Value =
            txtLote.Text =
            txtCertificado.Text =
            txtCorrida.Text =
            txtBitola.Text =
            txtQtdePedidoCompra.Text =
            txtQtde.Text =
            txtValorUnit.Text =
            txtC.Text =
            txtSi.Text =
            txtMn.Text =
            txtP.Text =
            txtS.Text =
            txtCr.Text =
            txtNi.Text =
            txtMo.Text =
            txtCu.Text =
            txtTi.Text =
            txtN2.Text =
            txtCo.Text =
            txtAl.Text =
            txtResistenciaTracao.Text =
            txtDureza.Text =
            txtNorma.Text =
            txtBitola.Text =
            hdfCodMateriaPrima.Text =
            hdfCodBitola.Text =
            ddlUnidade.Text =
            txtIPI.Text =
            txtNota.Text = string.Empty;
            lkbArquivoPdf.Text = "(Nenhum arquivo carregado)";
            Cache[key_Pdf] = "";
            ViewState.Clear();
            Session.Remove("ComposicaoMateriaPrima");
            Session.Remove("ResistenciaTracao");
        }

        protected void ddlBitola_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string mensagem = string.Empty;
            //decimal value = 0;
            //if (!string.IsNullOrEmpty(ddlBitola.SelectedValue))
            //{
            //    if (Session["ComposicaoMateriaPrima"] != null)
            //    {
            //        value = decimal.Parse(ddlBitola.SelectedItem.Text);
            //        //armazena em viewstate a linha selecionada para posterior atualização
            //        ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
            //        List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
            //        bool verificado = false;
            //        foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
            //        {
            //            if (0 != newlstComposicaoMateriaPrimaVo.BitolaMaxima && value < newlstComposicaoMateriaPrimaVo.BitolaMinima)
            //            {
            //                if (verificado == false)
            //                {
            //                    mensagem = newlstComposicaoMateriaPrimaVo.BitolaMinima + " - " + newlstComposicaoMateriaPrimaVo.BitolaMaxima;
            //                    verificado = true;
            //                }
            //            }
            //            else
            //            {
            //                if (value > newlstComposicaoMateriaPrimaVo.BitolaMaxima && verificado == false)
            //                {
            //                    mensagem = newlstComposicaoMateriaPrimaVo.BitolaMinima + " - " + newlstComposicaoMateriaPrimaVo.BitolaMaxima;
            //                }
            //                else
            //                {
            //                    verificado = true;
            //                    mensagem = string.Empty;
            //                }
            //            }
            //        }
            //        if (!string.IsNullOrEmpty(mensagem))
            //        {
            //            spanBi.Attributes.Add("title", mensagem);
            //            spanBi.Style.Add("display", "inline");
            //            ddlBitola.BackColor = Color.Yellow;
            //        }
            //        else
            //        {
            //            spanBi.Style.Add("display", "none");
            //            ddlBitola.BackColor = Color.White;
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(ddlMateriaPrima.SelectedValue))
            //    {
            //        var resistenciaTracao = new ResistenciaTracaoVO();
            //        if (!string.IsNullOrEmpty(ddlMateriaPrima.SelectedValue))
            //            resistenciaTracao =
            //                new BS.MateriaPrima().ListarResistenciaTracao(int.Parse(ddlMateriaPrima.SelectedValue),
            //                                                           int.Parse(ddlBitola.SelectedValue));
            //        var lstResistenciaTracao = new List<ResistenciaTracaoVO>();
            //        lstResistenciaTracao.Add(resistenciaTracao);
            //        Session.Add("ResistenciaTracao", lstResistenciaTracao.ToArray());
            //    }
            //}
        }

        protected void txtAl_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {
                        if (0 != newlstComposicaoMateriaPrimaVo.AlMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.AlMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.AlMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.AlMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.AlMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.AlMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.AlMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanAl.Attributes.Add("title", mensagem);
                        spanAl.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanAl.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtC.Focus();
        }


        protected void txtC_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {
                        if (0 != newlstComposicaoMateriaPrimaVo.CMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.CMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.CMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.CMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.CMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.CMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.CMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanC.Attributes.Add("title", mensagem);
                        spanC.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanC.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtSi.Focus();
        }

        protected void txtSi_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {
                        if (0 != newlstComposicaoMateriaPrimaVo.SiMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.SiMinimo)
                            {
                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.SiMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.SiMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.SiMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.SiMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.SiMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanSi.Attributes.Add("title", mensagem);
                        spanSi.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanSi.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtMn.Focus();
        }

        protected void txtMn_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {

                        if (0 != newlstComposicaoMateriaPrimaVo.MnMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.MnMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.MnMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.MnMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.MnMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.MnMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.MnMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanMn.Attributes.Add("title", mensagem);
                        spanMn.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanMn.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtP.Focus();
        }

        protected void txtP_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {
                        if (0 != newlstComposicaoMateriaPrimaVo.PMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.PMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.PMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.PMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.PMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.PMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.PMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanP.Attributes.Add("title", mensagem);
                        spanP.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanP.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtS.Focus();
        }

        protected void txtS_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {

                        if (0 != newlstComposicaoMateriaPrimaVo.SMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.SMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.SMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.SMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.SMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.SMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.SMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanS.Attributes.Add("title", mensagem);
                        spanS.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanS.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtCr.Focus();
        }

        protected void txtCr_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {
                        if (0 != newlstComposicaoMateriaPrimaVo.CrMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.CrMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.CrMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.CrMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.CrMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.CrMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.CrMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanCr.Attributes.Add("title", mensagem);
                        spanCr.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanCr.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtNi.Focus();
        }

        protected void txtNi_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {

                        if (0 != newlstComposicaoMateriaPrimaVo.NiMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.NiMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.NiMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.NiMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.NiMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.NiMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.NiMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanNi.Attributes.Add("title", mensagem);
                        spanNi.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanNi.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtMo.Focus();
        }

        protected void txtMo_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {

                        if (0 != newlstComposicaoMateriaPrimaVo.MoMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.MoMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.MoMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.MoMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.MoMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.MoMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.MoMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanMo.Attributes.Add("title", mensagem);
                        spanMo.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanMo.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtCu.Focus();
        }

        protected void txtCu_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {
                        if (0 != newlstComposicaoMateriaPrimaVo.CuMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.CuMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.CuMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.CuMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.CuMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.CuMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.CuMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanCu.Attributes.Add("title", mensagem);
                        spanCu.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanCu.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtTi.Focus();
        }

        protected void txtTi_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {
                        if (0 != newlstComposicaoMateriaPrimaVo.TiMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.TiMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.TiMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.TiMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.CMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.TiMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.TiMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanTi.Attributes.Add("title", mensagem);
                        spanTi.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanTi.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtN2.Focus();
        }

        protected void txtN2_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {
                        if (0 != newlstComposicaoMateriaPrimaVo.N2Maximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.N2Minimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.N2Minimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.N2Maximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.CMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.N2Minimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.N2Maximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanN2.Attributes.Add("title", mensagem);
                        spanN2.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanN2.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtCo.Focus();
        }

        protected void txtCo_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ComposicaoMateriaPrima"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                    List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                    bool verificado = false;
                    foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                    {
                        if (0 != newlstComposicaoMateriaPrimaVo.CoMaximo)
                        {
                            if (value < newlstComposicaoMateriaPrimaVo.CoMinimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.CoMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.CoMaximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstComposicaoMateriaPrimaVo.CoMaximo && verificado == false)
                                {
                                    mensagem = newlstComposicaoMateriaPrimaVo.CoMinimo + " - " +
                                               newlstComposicaoMateriaPrimaVo.CoMaximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanCo.Attributes.Add("title", mensagem);
                        spanCo.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanCo.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtResistenciaTracao.Focus();
        }

        protected void txtResistenciaTracao_TextChanged(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            decimal value = 0;
            if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                if (Session["ResistenciaTracao"] != null)
                {
                    value = decimal.Parse(((TextBox)sender).Text);
                    //armazena em viewstate a linha selecionada para posterior atualização
                    ResistenciaTracaoVO[] lstResistenciaTracao = (ResistenciaTracaoVO[])Session["ResistenciaTracao"];
                    List<ResistenciaTracaoVO> newlstResistenciaTracaoVos = new List<ResistenciaTracaoVO>(lstResistenciaTracao);
                    bool verificado = false;
                    foreach (var newlstresistenciaTracaoVo in newlstResistenciaTracaoVos)
                    {
                        if (0 != newlstresistenciaTracaoVo.Maximo)
                        {
                            if (value < newlstresistenciaTracaoVo.Minimo)
                            {

                                if (verificado == false)
                                {
                                    mensagem = newlstresistenciaTracaoVo.Minimo + " - " +
                                               newlstresistenciaTracaoVo.Maximo;
                                    verificado = true;
                                }
                            }
                            else
                            {
                                if (value > newlstresistenciaTracaoVo.Maximo && verificado == false)
                                {
                                    mensagem = newlstresistenciaTracaoVo.Minimo + " - " +
                                               newlstresistenciaTracaoVo.Maximo;
                                }
                                else
                                {
                                    verificado = true;
                                    mensagem = string.Empty;
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        spanRt.Attributes.Add("title", mensagem);
                        spanRt.Style.Add("display", "inline");
                        ((TextBox)sender).BackColor = Color.Yellow;
                    }
                    else
                    {
                        spanRt.Style.Add("display", "none");
                        ((TextBox)sender).BackColor = Color.White;
                    }
                }
            }
            txtDureza.Focus();
        }

        protected void ddlMateriaPrima_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var lstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>();
            //if (!string.IsNullOrEmpty(ddlMateriaPrima.SelectedValue))
            //    lstComposicaoMateriaPrima = new BS.MateriaPrima().ListarComposicaoMateriaPrima(int.Parse(ddlMateriaPrima.SelectedValue));
            //Session.Add("ComposicaoMateriaPrima", lstComposicaoMateriaPrima.ToArray());
        }

        protected void btnLimparImagem_Click(object sender, EventArgs e)
        {
            lkbArquivoPdf.Text = "(Nenhum arquivo carregado)";
            ViewState.Clear();
        }

        private void ShowFormInfo(FormPanel form)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Submitted fields: ");

            foreach (string key in Request.Form.Keys)
            {
                sb.Append(key).Append(", ");
            }

            sb.Append("<br/>Submitted files: ").Append(Request.Files.Count);

            sb.Append("<br/>Form items: <br/>");
            foreach (Field field in form.Items)
            {
                sb.Append(field.ClientID).Append("=").Append(field.Value.ToString()).Append("<br/>");
            }

            X.Msg.Alert("Submit", sb.ToString()).Show();
        }

        protected void Submit1(object sender, DirectEventArgs e)
        {
            ShowFormInfo(pnl2);
        }
    }
}
