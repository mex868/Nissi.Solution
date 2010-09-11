#region Using
using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Nissi.Model;
using Nissi.Business;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
#endregion


    public partial class CadastroProduto : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["acao"] != null)
                    hdfTipoAcao.Value = Request.QueryString["acao"].ToString();
                if (Request.QueryString["codigo"] != null)
                    hdfCodigo.Value = Request.QueryString["codigo"].ToString();
                if (hdfTipoAcao.Value == "IncluirItem")
                {
                    this.Master.InibirTopo();
                    Pesquisar();
                }
                CarregaCombo();
                //criar ViewState para armazenar valores do grid de ICMS
                //este grid só salvará quando salvar o produto inteiro
                List<ICMSVO> lstICMS = new List<ICMSVO>();
                ViewState.Add("lstICMS", lstICMS.ToArray());

                txtCodigoPesq.Focus();
            }
        }
                
        #region Propriedades
        public ProdutoVO DadosProduto
        {
            set {
                if(value.CodProduto > 0)
                hdfCodProduto.Value = value.CodProduto.ToString();
                txtDescricao.Text = value.Descricao;
                txtCodigo.Text = value.Codigo;
                ddlUnidade.SelectedValue = value.Unidade.CodUnidade.ToString();
                ddlClassificacaoFiscal.SelectedValue = value.NCM;

                //setar valores do icms
                ICMSGrid.DataSource = value.ICMS;
                ICMSGrid.DataBind();
                ViewState["lstICMS"] = value.ICMS.ToArray();
            }
           get
            {
                ProdutoVO identProduto = new ProdutoVO();
                identProduto.Descricao = txtDescricao.Text;
                identProduto.Unidade.CodUnidade = Convert.ToInt32(ddlUnidade.SelectedValue);
                identProduto.NCM = ddlClassificacaoFiscal.SelectedValue;
                identProduto.Codigo = txtCodigo.Text;
                if (!string.IsNullOrEmpty(hdfCodProduto.Value))
                    identProduto.CodProduto = Convert.ToInt32(hdfCodProduto.Value);

                //setar valores do icms
                ICMSVO[] lstICMS = (ICMSVO[])ViewState["lstICMS"];
                List<ICMSVO> newlstICMS = new List<ICMSVO>(lstICMS);

                identProduto.ICMS = newlstICMS;

                return identProduto;
            }
        }


        #endregion
        
        #region Métodos
        private void Pesquisar()
        {
            ProdutoVO identProduto = new ProdutoVO();
            if ((hdfTipoAcao.Value == "Incluir" || hdfTipoAcao.Value == "Editar" || hdfTipoAcao.Value == "IncluirItem") && (!string.IsNullOrEmpty(hdfCodProduto.Value) || !string.IsNullOrEmpty(hdfCodigo.Value)))
            {
                if (!string.IsNullOrEmpty(hdfCodProduto.Value))
                {
                    identProduto.CodProduto = Convert.ToInt32(hdfCodProduto.Value);
                }
                if (!string.IsNullOrEmpty(hdfCodigo.Value))
                {
                    identProduto.Codigo = hdfCodigo.Value;
                }
                LimparCampos();
            }
            else
            {

                if (!string.IsNullOrEmpty(txtCodigoPesq.Text))
                    identProduto.Codigo = txtCodigoPesq.Text;
                identProduto.Descricao = txtDescricaoPesq.Text;
            }
            List<ProdutoVO> lProduto = new List<ProdutoVO>();
            lProduto = new Produto().Listar(identProduto);
            if (lProduto.Count > 0)
            {
                grdListaResultado.DataSource = lProduto;
                grdListaResultado.DataBind();
                grdListaResultado.Visible = true;
            }
            else
            {
                grdListaResultado.Visible = false;
            }
            LimparCampos();

        }

        private void LimparCampos()
        {
            txtCodigoPesq.Text =
            txtDescricaoPesq.Text = 
            txtCodigo.Text = 
            hdfCodProduto.Value = 
            hdfCodigo.Value =
            txtDescricao.Text = string.Empty;
            LimparCamposICMS();
        }

        private void LimparCamposICMS()
        {
            ddlTipoTributacao.SelectedIndex = 0;
            ddlOrigem.SelectedIndex = 0;
            ddlModalidadeBaseCalculo.SelectedIndex = 0;
            txtAliquota.Text = "";
            txtPercentualBaseCalculo.Text = "";
            ddlModalidadeBaseCalculoICMSST.SelectedIndex = 0;
            txtAliquotaST.Text = "";
            txtPercentualICMSST.Text = "";
            txtPercentualValorAdicionado.Text = "";
        }
        

        private void CarregaCombo()
        {
            ddlUnidade.DataSource = new Unidade().Listar(new UnidadeVO());
            ddlUnidade.DataTextField = "Descricao";
            ddlUnidade.DataValueField = "CodUnidade";
            ddlUnidade.DataBind();

            ddlClassificacaoFiscal.DataSource = new ClassificacaoFiscal().Listar(new ClassificacaoFiscalVO());
            ddlClassificacaoFiscal.DataTextField = "Letra";
            ddlClassificacaoFiscal.DataValueField = "Numero";
            ddlClassificacaoFiscal.DataBind();


            ddlTipoTributacao.DataSource = TipoTributacao.GetListaTipoTributacao();
            ddlTipoTributacao.DataTextField = "Descricao";
            ddlTipoTributacao.DataValueField = "Codigo";

            ddlTipoTributacao.DataBind();

            ddlModalidadeBaseCalculo.DataSource = ModalidadeBaseCalculoICMS.GetListaModalidadeBaseCalculoICMS();
            ddlModalidadeBaseCalculo.DataTextField = "Descricao";
            ddlModalidadeBaseCalculo.DataValueField = "Codigo";
            ddlModalidadeBaseCalculo.DataBind();

            ddlOrigem.DataSource = OrigemMercadoria.GetListaOrigemMercadoria();
            ddlOrigem.DataTextField = "Descricao";
            ddlOrigem.DataValueField = "Codigo";
            ddlOrigem.DataBind();

            ddlModalidadeBaseCalculoICMSST.DataSource = ModalidadeBaseCalculoICMSST.GetListaModalidadeBaseCalculoICMSST();
            ddlModalidadeBaseCalculoICMSST.DataTextField = "Descricao";
            ddlModalidadeBaseCalculoICMSST.DataValueField = "Codigo";
            ddlModalidadeBaseCalculoICMSST.DataBind();
        }

        /// <summary>
        /// Método para verificar se o produto já está na lista de produtos inseridos na emissão da Nota Fiscal (CadastraNFe.aspx)
        /// </summary>
        /// <param name="codProduto">Código do Produto</param>
        private bool ProdutoJaIncluido(int? codProduto)
        {
            if ((Session["lstItemNotaFiscal"] != null) && (codProduto != null))
            {
                ItemNotaFiscalVO[] lstItemNotaFiscal = (ItemNotaFiscalVO[])Session["lstItemNotaFiscal"];
                foreach (ItemNotaFiscalVO item in lstItemNotaFiscal)
                {
                    if (item.Produto.CodProduto == codProduto)
                        return true;
                }
                     
            }
            return false;
        }
        #endregion

        #region Métodos da Grid

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProdutoVO identProduto = new ProdutoVO();
            identProduto.CodProduto = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Excluir")
            {
                new Produto().Excluir(identProduto);
                Pesquisar();
            }
            else if (e.CommandName == "Editar")
            {
                hdfTipoAcao.Value = "Editar";
                DadosProduto = new Produto().Listar(identProduto)[0];
                mpeIncluirProduto.Show();
            }
            //else if (e.CommandName == "IncluirItem")
            //{
              //  Response.Redirect(@"\NFe\CadastraItemNFe.aspx?CodProduto=" + identProduto.CodProduto.ToString());
            //}
        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProdutoVO tempProduto = (ProdutoVO)e.Row.DataItem;

                if (!string.IsNullOrEmpty(tempProduto.Codigo))
                    e.Row.Cells[1].Text = tempProduto.Codigo.ToString();
                
                e.Row.Cells[2].Text = tempProduto.Descricao;
                e.Row.Cells[3].Text = tempProduto.Unidade.Descricao;

                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempProduto.CodProduto.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Produto [" + tempProduto.Descricao.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempProduto.CodProduto.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Produto [" + tempProduto.Descricao.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir Produto [" + tempProduto.Descricao.Trim() + "]";
                #endregion
                #region Botao Incluir Item
                if (hdfTipoAcao.Value == "IncluirItem")
                {
                    //verificar se na Session["lstItemNotaFiscal"] já existe o produto desta linha,
                    //se existir, não será incluído novamente, portanto, não exibirá o botão Incluir Ítem
                    if (!ProdutoJaIncluido(tempProduto.CodProduto))
                    {
                        ImageButton imgIncluirItem = (ImageButton)e.Row.FindControl("imgIncluirItem");
                        imgIncluirItem.Visible = true;
                        imgIncluirItem.ImageUrl = caminhoAplicacao + @"Imagens\IncluirItem.png";
                        imgIncluirItem.CommandArgument = tempProduto.CodProduto.ToString();
                        imgIncluirItem.CommandName = "IncluirItem";
                        imgIncluirItem.Style.Add("cursor", "hand");
                        imgIncluirItem.ToolTip = "Incluir Item [" + tempProduto.Descricao.Trim() + "] na Nota Fiscal";
                        imgIncluirItem.Attributes.Add("onclick", "window.name='Produto';window.open('" + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/NFe/CadastraItemNFe.aspx?CodProduto=" + tempProduto.CodProduto.ToString() + "','Produto');");
                    }
                }
                #endregion
                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";

            }
        }

        protected void ICMSGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ICMSVO tempICMS = (ICMSVO)e.Row.DataItem;
                var tributacao = TipoTributacao.GetListaTipoTributacao().Single(t => t.Codigo == tempICMS.CodTipoTributacao);
                e.Row.Cells[1].Text = tributacao.Descricao;
                var origem = OrigemMercadoria.GetListaOrigemMercadoria().Single(o => o.Codigo == tempICMS.CodOrigem);
                e.Row.Cells[2].Text = origem.Descricao;
                
                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempICMS.CodProduto.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do ICMS";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempICMS.CodProduto.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do ICMS?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir ICMS";
                #endregion

                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";
            }
        }

        protected void ICMSGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //forma de pegar o index do datagrid
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int linha = row.RowIndex;
            //armazena em viewstate a linha selecionada para posterior atualização
            ViewState["LinhaSelecionadaICMS"] = linha;

            ICMSVO[] lstICMS = (ICMSVO[])ViewState["lstICMS"];
            List<ICMSVO> newlstICMS = new List<ICMSVO>(lstICMS);

            if (e.CommandName == "Editar")
            {
                hdfTipoAcaoICMS.Value = "Editar";

                //busca no vo os valores para a linha selecionada
                int iLinhaFor = 0;
                foreach (ICMSVO item in newlstICMS)
                {
                    if (iLinhaFor == linha)
                    {
                        //atribui aos campos da tela para alteração
                        ddlTipoTributacao.SelectedValue = item.CodTipoTributacao;
                        ddlOrigem.SelectedValue = Convert.ToString(item.CodOrigem);
                        ddlModalidadeBaseCalculo.SelectedValue = Convert.ToString(item.CodBaseCalculo);
                        ddlModalidadeBaseCalculoICMSST.SelectedValue = Convert.ToString(item.CodBaseCalculoICMSST);
                        txtAliquota.Text = item.Aliquota.ToString().Trim().Equals("") ? "" : Convert.ToString(item.Aliquota);
                        txtPercentualBaseCalculo.Text = item.PercentualReducao.ToString().Trim().Equals("") ? "" : Convert.ToString(item.PercentualReducao);
                        txtAliquotaST.Text = item.AliquotaST.ToString().Trim().Equals("") ? "" : Convert.ToString(item.AliquotaST);
                        txtPercentualICMSST.Text = item.PercentualReducaoST.ToString().Trim().Equals("") ? "" : Convert.ToString(item.PercentualReducaoST);
                        txtPercentualValorAdicionado.Text = item.PercentualMargemST.ToString().Trim().Equals("") ? "" : Convert.ToString(item.PercentualMargemST);

                        //exibe panel com os controles de icms
                        divCadastroICMS.Visible = true;
                        upICMS.Update();

                        //sai do for
                        break;
                    }
                    iLinhaFor++;
                }
            }
            else if (e.CommandName == "Excluir")
            {
                newlstICMS.RemoveAt(linha);   
                
                ICMSGrid.DataSource = newlstICMS;
                ICMSGrid.DataBind();
                updICMS.Update();
                
                //atualiza viewstate
                ViewState["lstICMS"] = newlstICMS.ToArray();
            }
        }

        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void ICMSGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        #endregion

        #region Métodos dos Botões

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
        
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if ((hdfTipoAcao.Value == "Incluir" || hdfTipoAcao.Value == "IncluirItem") && (string.IsNullOrEmpty(hdfCodProduto.Value) || string.IsNullOrEmpty(hdfCodigo.Value)))
            {
               hdfCodProduto.Value = new Produto().Incluir(DadosProduto).ToString();
            }
            else 
            {
                new Produto().AlterarProduto(DadosProduto);
            }

            Pesquisar();
            mpeIncluirProduto.Hide();
            LimparCampos();
            divCadastroICMS.Visible = false;
        }

        protected void btnSalvarICMS_Click(object sender, EventArgs e)
        {
            //será incluido no grid de ICMS manualmente (não incluirá no banco ainda)
            //pois só deverá ser incluido no banco quando salvar o produto

            ICMSVO[] lstICMS = (ICMSVO[])ViewState["lstICMS"];
            List<ICMSVO> newlstICMS = new List<ICMSVO>(lstICMS);
            
            //se for edição de ICMS, atualizar o list
            if (hdfTipoAcaoICMS.Value.Equals("Incluir"))
            {
                /************************************************************************
                Se a ação for inclusão, simplesmente verifica se o ítem já foi cadastrado
                se já for, exibe mensagem e não inclui o ítem
                /***********************************************************************/
                ICMSVO result = newlstICMS.Find(
                delegate(ICMSVO bk)
                    {
                        return bk.CodTipoTributacao == ddlTipoTributacao.SelectedValue;
                    }
                );
                if (result != null)
                {

                    MensagemCliente("ICMS Já cadastrado!");
                    return;
                }
                /************************************************************************/
                /************************************************************************/
                

                //senão, incluir novo ítem no list
                ICMSVO lstICMSAux = new ICMSVO();
                lstICMSAux.CodTipoTributacao = ddlTipoTributacao.SelectedValue;
                lstICMSAux.CodOrigem = Convert.ToInt32(ddlOrigem.SelectedValue);
                lstICMSAux.CodBaseCalculo = Convert.ToInt32(ddlModalidadeBaseCalculo.SelectedValue);
                lstICMSAux.CodBaseCalculoICMSST = Convert.ToInt32(ddlModalidadeBaseCalculoICMSST.SelectedValue);

                if (!txtAliquota.Text.Trim().Equals(""))
                    lstICMSAux.Aliquota = Convert.ToDecimal(txtAliquota.Text.Trim());
                
                if (!txtPercentualBaseCalculo.Text.Trim().Equals(""))
                    lstICMSAux.PercentualReducao = Convert.ToDecimal(txtPercentualBaseCalculo.Text.Trim());

                if (!txtAliquotaST.Text.Trim().Equals(""))
                    lstICMSAux.AliquotaST = Convert.ToDecimal(txtAliquotaST.Text.Trim());

                if (!txtPercentualICMSST.Text.Trim().Equals(""))
                    lstICMSAux.PercentualReducaoST = Convert.ToDecimal(txtPercentualICMSST.Text.Trim());

                if (!txtPercentualValorAdicionado.Text.Trim().Equals(""))
                    lstICMSAux.PercentualMargemST = Convert.ToDecimal(txtPercentualValorAdicionado.Text.Trim());

                newlstICMS.Add(lstICMSAux);
            }
            else
            {
                int linha = Convert.ToInt32(ViewState["LinhaSelecionadaICMS"]);
                int iLinhaFor = 0;
                foreach (ICMSVO item in newlstICMS)
                {
                    if (iLinhaFor == linha)
                    {
                        /************************************************************************
                        Se a ação for alteração, verifica se o ítem já está cadastrado, se já estiver
                        será impedido, desde que não seja ele mesmo
                        /***********************************************************************/
                        int iLinhaSelecionada = 0;
                        foreach (ICMSVO item2 in newlstICMS)
                        {
                            if ((item2.CodTipoTributacao == ddlTipoTributacao.SelectedValue) &&
                                (iLinhaSelecionada != linha))
                            {
                                MensagemCliente("ICMS Já cadastrado!");
                                return;
                            }
                            iLinhaSelecionada++;
                        }
                        /************************************************************************
                        
                        /***********************************************************************/
                        
                        item.CodTipoTributacao = ddlTipoTributacao.SelectedValue;
                        item.CodOrigem = Convert.ToInt32(ddlOrigem.SelectedValue);
                        item.CodBaseCalculo = Convert.ToInt32(ddlModalidadeBaseCalculo.SelectedValue);
                        item.CodBaseCalculoICMSST = Convert.ToInt32(ddlModalidadeBaseCalculoICMSST.SelectedValue);

                        if (!txtAliquota.Text.Trim().Equals(""))
                            item.Aliquota = Convert.ToDecimal(txtAliquota.Text.Trim());

                        if (!txtPercentualBaseCalculo.Text.Trim().Equals(""))
                            item.PercentualReducao = Convert.ToDecimal(txtPercentualBaseCalculo.Text.Trim());

                        if (!txtAliquotaST.Text.Trim().Equals(""))
                            item.AliquotaST = Convert.ToDecimal(txtAliquotaST.Text.Trim());

                        if (!txtPercentualICMSST.Text.Trim().Equals(""))
                            item.PercentualReducaoST = Convert.ToDecimal(txtPercentualICMSST.Text.Trim());

                        if (!txtPercentualValorAdicionado.Text.Trim().Equals(""))
                            item.PercentualMargemST = Convert.ToDecimal(txtPercentualValorAdicionado.Text.Trim());

                        //sai do for
                        break;
                    }
                    iLinhaFor++;
                }
            }
                
            ICMSGrid.DataSource = newlstICMS;
            ICMSGrid.DataBind();
            updICMS.Update();

            
            //atualiza viewstate
            ViewState["lstICMS"] = newlstICMS.ToArray();

            LimparCamposICMS();
            divCadastroICMS.Visible = false;
        }
        
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            if (hdfTipoAcao.Value != "IncluirItem")
                hdfTipoAcao.Value = "Incluir";
            LimparCampos();
            mpeIncluirProduto.Show();
            Master.PosicionarFoco(txtDescricao);
            List<ICMSVO> newlstICMS = new List<ICMSVO>();
            ICMSGrid.DataSource = newlstICMS;
            ICMSGrid.DataBind();
            ViewState.Add("lstICMS", newlstICMS.ToArray());
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            mpeIncluirProduto.Hide();
            divCadastroICMS.Visible = false;

            List<ICMSVO> newlstICMS = new List<ICMSVO>();
            ICMSGrid.DataSource = newlstICMS;
            ICMSGrid.DataBind();
            ViewState.Add("lstICMS", newlstICMS.ToArray());
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaCombo();
        }

        protected void btnInserirICMS_Click(object sender, EventArgs e)
        {
            divCadastroICMS.Visible = true;
            hdfTipoAcaoICMS.Value = "Incluir";
            ddlTipoTributacao.SelectedValue = "41";
            ddlOrigem.SelectedValue = "0";

        }

        protected void btnCancelarICMS_Click(object sender, EventArgs e)
        {
            divCadastroICMS.Visible = false;
            LimparCampos();
        }
        
        #endregion

    }


