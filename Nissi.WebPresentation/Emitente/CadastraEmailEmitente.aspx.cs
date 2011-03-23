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


public partial class CadastraEmailEmitente : BasePage
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
                //este grid só salvará quando salvar o EmailEmitente inteiro
                txtCodigoPesq.Focus();
            }
        }
                
        #region Propriedades
        public EmailEmitenteVO DadosEmailEmitente
        {
            set {
                if(value.CodEmailEmitente > 0)
                hdfCodProduto.Value = value.CodEmailEmitente.ToString();
                txtEmail.Text = value.Email;
                ddlTipo.SelectedValue = ((int)value.Tipo).ToString();

            }
           get
            {
                EmailEmitenteVO identEmailEmitente = new EmailEmitenteVO();
                identEmailEmitente.Email = txtEmail.Text;
                identEmailEmitente.Tipo = (TypePedido)Convert.ToInt32(ddlTipo.SelectedValue);
                identEmailEmitente.CodEmailEmitente = !string.IsNullOrEmpty(hdfCodProduto.Value)?int.Parse(hdfCodProduto.Value):0;

                return identEmailEmitente;
            }
        }


        #endregion
        
        #region Métodos
        private void Pesquisar()
        {
            EmailEmitenteVO emailEmitenteVO = new EmailEmitenteVO();
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
            {
                emailEmitenteVO.CodEmailEmitente = Convert.ToInt32(hdfIdRazaoSocial.Value);
            }
            else
            {
                if ((hdfTipoAcao.Value == "Incluir" || hdfTipoAcao.Value == "Editar" || hdfTipoAcao.Value == "IncluirItem") && (!string.IsNullOrEmpty(hdfCodProduto.Value) || !string.IsNullOrEmpty(hdfCodigo.Value)))
                {
                    if (!string.IsNullOrEmpty(hdfCodProduto.Value))
                    {
                        emailEmitenteVO.CodEmailEmitente = Convert.ToInt32(hdfCodProduto.Value);
                    }
                    LimparCampos();
                }
                else
                {

                    if (!string.IsNullOrEmpty(txtCodigoPesq.Text))
                        emailEmitenteVO.CodEmailEmitente = int.Parse(txtCodigoPesq.Text);
                    emailEmitenteVO.Email = txtEmailPesq.Text;
                }
                
            }
            List<EmailEmitenteVO> listarPorEmailEmitente = new List<EmailEmitenteVO>();
            if (emailEmitenteVO.CodEmailEmitente != 0)
            {
                var email = EmailEmitente.ListarPorCodigo(emailEmitenteVO.CodEmailEmitente);
                if (email != null)
                listarPorEmailEmitente.Add(email);
            }
                
            if (!string.IsNullOrEmpty(emailEmitenteVO.Email))
                listarPorEmailEmitente = EmailEmitente.ListarPorEmailEmitente(emailEmitenteVO.Email);
            if (listarPorEmailEmitente != null && listarPorEmailEmitente.Count > 0)
            {
                grdListaResultado.DataSource = listarPorEmailEmitente;
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
            txtEmailPesq.Text = 
            txtCodigo.Text =
            hdfCodProduto.Value = 
            hdfIdRazaoSocial.Value =
            hdfCodigo.Value =
            ddlTipo.Text =
            txtEmail.Text = string.Empty;
        }

        

        private void CarregaCombo()
        {
            Geral.CarregarDDL(ref ddlTipo, ListarEnvair().ToArray(), "Key", "Value");
        }

        private Dictionary<int,string> ListarEnvair()
        {
            Dictionary<int,string> Enviar = new Dictionary<int, string>();
            Enviar.Add(0,"Compras");
            Enviar.Add(2,"Vendas");
            return Enviar;
        }
        #endregion
        
        #region Métodos da Grid

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmailEmitenteVO identProduto = new EmailEmitenteVO();
            identProduto.CodEmailEmitente = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Excluir")
            {
                EmailEmitente.Excluir(identProduto.CodEmailEmitente,UsuarioAtivo.CodFuncionario.Value);
                Pesquisar();
            }
            else if (e.CommandName == "Editar")
            {
                hdfTipoAcao.Value = "Editar";
                DadosEmailEmitente = EmailEmitente.ListarPorCodigo(identProduto.CodEmailEmitente);
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
                EmailEmitenteVO tempProduto = (EmailEmitenteVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempProduto.CodEmailEmitente.ToString();
                e.Row.Cells[2].Text = tempProduto.Email;
                string tipo = string.Empty;
                switch (tempProduto.Tipo)
                {
                    case TypePedido.Compra:
                        tipo = "Compras";
                        break;
                    case TypePedido.CompraInsumo:
                        tipo = "Compras";
                        break;
                    case TypePedido.Venda:
                        tipo = "Vendas";
                        break;
                    case TypePedido.VendaInsumo:
                        tipo = "Vendas";
                        break;
                }
                e.Row.Cells[3].Text = tipo;
                Random random = new Random();
                int num = random.Next(1000);
                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempProduto.CodEmailEmitente.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Produto Insumo [" + tempProduto.Email.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempProduto.CodEmailEmitente.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Produto Insumo [" + tempProduto.Email.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir EmailEmitente [" + tempProduto.Email.Trim() + "]";
                #endregion
                #region Botao Incluir Item
                if (hdfTipoAcao.Value == "IncluirItem")
                {
                    //verificar se na Session["lstItemNotaFiscal"] já existe o EmailEmitente desta linha,
                    //se existir, não será incluído novamente, portanto, não exibirá o botão Incluir Ítem
                    //if (!ProdutoJaIncluido(tempProduto.CodProduto))
                    //{
                        ImageButton imgIncluirItem = (ImageButton)e.Row.FindControl("imgIncluirItem");
                        imgIncluirItem.Visible = true;
                        imgIncluirItem.ImageUrl = caminhoAplicacao + @"Imagens\IncluirItem.png";
                        imgIncluirItem.CommandArgument = tempProduto.CodEmailEmitente.ToString();
                        imgIncluirItem.CommandName = "IncluirItem";
                        imgIncluirItem.Style.Add("cursor", "hand");
                        imgIncluirItem.ToolTip = "Incluir Item [" + tempProduto.Email.Trim() + "] na Nota Fiscal";
                        imgIncluirItem.Attributes.Add("onclick", "window.name='Produto Insumo';window.open('" + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/NFe/CadastraItemNFe.aspx?CodProduto=" + tempProduto.CodEmailEmitente.ToString() + "&CodItemNotaFiscal="+num+"','EmailEmitente');");
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
               hdfCodProduto.Value = EmailEmitente.Incluir(DadosEmailEmitente.Email, (int)DadosEmailEmitente.Tipo, UsuarioAtivo.CodFuncionario.Value).ToString();
            }
            else 
            {
                EmailEmitente.Alterar(DadosEmailEmitente.CodEmailEmitente,DadosEmailEmitente.Email, (int)DadosEmailEmitente.Tipo, UsuarioAtivo.CodFuncionario.Value);
            }

            Pesquisar();
            mpeIncluirProduto.Hide();
            LimparCampos();
        }
        
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            if (hdfTipoAcao.Value != "IncluirItem")
                hdfTipoAcao.Value = "Incluir";
            LimparCampos();
            mpeIncluirProduto.Show();
            Master.PosicionarFoco(txtEmail);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            mpeIncluirProduto.Hide();
        }

      
        #endregion

    }


