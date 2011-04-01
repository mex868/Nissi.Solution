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
using Nissi.Util;

#endregion


public partial class CadastroProdutoInsumo : BasePage
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
                txtCodigoPesq.Focus();
                if (Request.QueryString["popup"] != null && Request.QueryString["popup"].ToString() == "sim")
                {
                    //ArmazenaValorSessao("TipoAcao", "Incluir");
                    hdfTipoAcao.Value = "Incluir";
                    hdfCadastroPopup.Value = "sim";
                    this.Master.InibirTopo();
                    mpeIncluirProduto.Show();
                    Master.PosicionarFoco(txtDescricao);
                    txtDescricao.Focus();
                }
                //criar ViewState para armazenar valores do grid de ICMS
                //este grid só salvará quando salvar o ProdutoInsumo inteiro
                List<ICMSVO> lstICMS = new List<ICMSVO>();
                ViewState.Add("lstICMS", lstICMS.ToArray());

                
            }
        }
                
        #region Propriedades
        public ProdutoInsumoVO DadosProduto
        {
            set {
                if(value.CodProdutoInsumo > 0)
                hdfCodProduto.Value = value.CodProdutoInsumo.ToString();
                txtDescricao.Text = value.Descricao;
                ddlUnidade.SelectedValue = value.Unidade.CodUnidade.ToString();
                txtValor.Text = value.Valor.ToString();

            }
           get
            {
                ProdutoInsumoVO identProduto = new ProdutoInsumoVO();
                identProduto.Descricao = txtDescricao.Text;
                identProduto.Unidade.CodUnidade = Convert.ToInt32(ddlUnidade.SelectedValue);
                identProduto.Valor = !string.IsNullOrEmpty(txtValor.Text)?decimal.Parse(txtValor.Text):0;
                identProduto.CodProdutoInsumo = !string.IsNullOrEmpty(hdfCodProduto.Value)?int.Parse(hdfCodProduto.Value):0;

                return identProduto;
            }
        }


        #endregion
        
        #region Métodos
        private void Pesquisar()
        {
            ProdutoInsumoVO identProduto = new ProdutoInsumoVO();
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
            {
                identProduto.CodProdutoInsumo = Convert.ToInt32(hdfIdRazaoSocial.Value);
            }
            else
            {
                if ((hdfTipoAcao.Value == "Incluir" || hdfTipoAcao.Value == "Editar" || hdfTipoAcao.Value == "IncluirItem") && (!string.IsNullOrEmpty(hdfCodProduto.Value) || !string.IsNullOrEmpty(hdfCodigo.Value)))
                {
                    if (!string.IsNullOrEmpty(hdfCodProduto.Value))
                    {
                        identProduto.CodProdutoInsumo = Convert.ToInt32(hdfCodProduto.Value);
                    }
                    LimparCampos();
                }
                else
                {

                    if (!string.IsNullOrEmpty(txtCodigoPesq.Text))
                        identProduto.CodProdutoInsumo = int.Parse(txtCodigoPesq.Text);
                    identProduto.Descricao = txtDescricaoPesq.Text;
                }
                
            }
            List<ProdutoInsumoVO> lProduto = new List<ProdutoInsumoVO>();
            if (identProduto.CodProdutoInsumo != 0)
            {
                lProduto.Add(ProdutoInsumo.ListarPorCodigo(identProduto.CodProdutoInsumo));
            }
                
            if (!string.IsNullOrEmpty(identProduto.Descricao))
                lProduto = ProdutoInsumo.ListarPorDescricao(identProduto.Descricao);
            if (lProduto.Count > 0)
            {
                grdListaResultado.DataSource = lProduto;
                grdListaResultado.DataBind();
                grdListaResultado.Visible = true;
            }
            else
            {
                grdListaResultado.Visible = false;
                MensagemCliente("Não existem registros para o filtro informado.");
            }
            LimparCampos();

        }

        private void LimparCampos()
        {
            txtCodigoPesq.Text =
            txtDescricaoPesq.Text = 
            txtCodigo.Text =
            txtValor.Text =
            hdfCodProduto.Value = 
            hdfIdRazaoSocial.Value =
            hdfCodigo.Value =
            ddlUnidade.Text =
            txtDescricao.Text = string.Empty;
        }

        

        private void CarregaCombo()
        {
            Geral.CarregarDDL(ref ddlUnidade, new Unidade().Listar(new UnidadeVO()).ToArray(), "CodUnidade", "TipoUnidade");
        }

        #endregion

        #region Métodos da Grid

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProdutoInsumoVO identProduto = new ProdutoInsumoVO();
            identProduto.CodProdutoInsumo = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Excluir")
            {
                ProdutoInsumo.Excluir(identProduto.CodProdutoInsumo);
                Pesquisar();
            }
            else if (e.CommandName == "Editar")
            {
                hdfTipoAcao.Value = "Editar";
                DadosProduto = ProdutoInsumo.ListarPorCodigo(identProduto.CodProdutoInsumo);
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
                ProdutoInsumoVO tempProduto = (ProdutoInsumoVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempProduto.CodProdutoInsumo.ToString();
                e.Row.Cells[2].Text = tempProduto.Descricao;
                e.Row.Cells[3].Text = tempProduto.Unidade.Descricao;
                e.Row.Cells[4].Text = tempProduto.Valor.ToString();
                Random random = new Random();
                int num = random.Next(1000);
                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempProduto.CodProdutoInsumo.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Produto Insumo [" + tempProduto.Descricao.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempProduto.CodProdutoInsumo.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Produto Insumo [" + tempProduto.Descricao.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir ProdutoInsumo [" + tempProduto.Descricao.Trim() + "]";
                #endregion
                #region Botao Incluir Item
                if (hdfTipoAcao.Value == "IncluirItem")
                {
                    //verificar se na Session["lstItemNotaFiscal"] já existe o ProdutoInsumo desta linha,
                    //se existir, não será incluído novamente, portanto, não exibirá o botão Incluir Ítem
                    //if (!ProdutoJaIncluido(tempProduto.CodProduto))
                    //{
                        ImageButton imgIncluirItem = (ImageButton)e.Row.FindControl("imgIncluirItem");
                        imgIncluirItem.Visible = true;
                        imgIncluirItem.ImageUrl = caminhoAplicacao + @"Imagens\IncluirItem.png";
                        imgIncluirItem.CommandArgument = tempProduto.CodProdutoInsumo.ToString();
                        imgIncluirItem.CommandName = "IncluirItem";
                        imgIncluirItem.Style.Add("cursor", "hand");
                        imgIncluirItem.ToolTip = "Incluir Item [" + tempProduto.Descricao.Trim() + "] na Nota Fiscal";
                        imgIncluirItem.Attributes.Add("onclick", "window.name='Produto Insumo';window.open('" + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/NFe/CadastraItemNFe.aspx?CodProduto=" + tempProduto.CodProdutoInsumo.ToString() + "&CodItemNotaFiscal="+num+"','ProdutoInsumo');");
                    //}
                }
                #endregion
                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";

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
               hdfCodProduto.Value = ProdutoInsumo.Incluir(DadosProduto.Descricao, DadosProduto.Unidade.CodUnidade, DadosProduto.Valor).ToString();
            }
            else 
            {
                ProdutoInsumo.Alterar(DadosProduto.CodProdutoInsumo,DadosProduto.Descricao, DadosProduto.Unidade.CodUnidade, DadosProduto.Valor);
            }

            if (hdfCadastroPopup.Value != "sim")
            {
                mpeIncluirProduto.Hide();
                LimparCampos();
                Pesquisar();
                updGrid.Update();
            }
            else
            {
                ExecutarScript(new StringBuilder("window.close()"));
            }
        }
        
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            if (hdfTipoAcao.Value != "IncluirItem")
                hdfTipoAcao.Value = "Incluir";
            LimparCampos();
            mpeIncluirProduto.Show();
            Master.PosicionarFoco(txtDescricao);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            mpeIncluirProduto.Hide();
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaCombo();
        }

        
        #endregion

    }


