#region Using

using System;
using System.Text;
using System.Web.UI.WebControls;
using Nissi.Model;
using System.Collections.Generic;

#endregion

    namespace Nissi.WebPresentation.FormaPgto
    {
        public partial class CadastraFormaPgto : BasePage
        {
            protected void Page_Load(object sender, EventArgs e)
            {

                if (!IsPostBack)
                {
                    Pesquisar();
                    updGrid.Update();
                    if (Request.QueryString["popup"] != null && Request.QueryString["popup"].ToString() == "sim")
                    {
                        //ArmazenaValorSessao("TipoAcao", "Incluir");
                        hdfTipoAcao.Value = "Incluir";
                        hdfCadastroPopup.Value = "sim";
                        this.Master.InibirTopo();
                        mpeIncluirFormaPgto.Show();
                    }
                }
            }


            #region Propriedades
            public FormaPgtoVO DadosFormaPgto
            {
                set
                {
                    if (value.CodFormaPgto > 0)
                        hdfCodFormaPgto.Value = value.CodFormaPgto.ToString();
                    txtDescricao.Text = value.Descricao;
                    txtIntervalo.Text = value.Intervalo.ToString();
                    txtParcelas.Text = value.Parcelas.ToString();
                }
                get
                {
                    FormaPgtoVO identFormaPgtoVo = new FormaPgtoVO();
                    identFormaPgtoVo.Descricao = txtDescricao.Text;
                    if (!string.IsNullOrEmpty(txtParcelas.Text))
                        identFormaPgtoVo.Parcelas = short.Parse(txtParcelas.Text);
                    if (!string.IsNullOrEmpty(txtIntervalo.Text))
                        identFormaPgtoVo.Intervalo = short.Parse(txtIntervalo.Text);
                    if (!string.IsNullOrEmpty(hdfCodFormaPgto.Value))
                        identFormaPgtoVo.CodFormaPgto = Convert.ToInt16((string)hdfCodFormaPgto.Value);
                    return identFormaPgtoVo;
                }
            }


            #endregion
            #region Métodos
            private void Pesquisar()
            {
                List<FormaPgtoVO> lFormaPgto = new List<FormaPgtoVO>();
                lFormaPgto = new Business.FormaPgto().Listar();
                if (lFormaPgto.Count > 0)
                {
                    grdListaResultado.DataSource = lFormaPgto;
                    grdListaResultado.DataBind();

                }
                else
                {
                    grdListaResultado.DataSource = new List<FormaPgtoVO>();
                    grdListaResultado.DataBind();
 
                }
            }

            private void LimparCampos()
            {
                hdfCodFormaPgto.Value =
                    txtDescricao.Text =
                    txtParcelas.Text =
                    txtIntervalo.Text = string.Empty;

            }
            #endregion

            #region Métodos da Grid

            protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {

            }

            protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                FormaPgtoVO identFormaPgto = new FormaPgtoVO();
                identFormaPgto.CodFormaPgto = Convert.ToInt16(e.CommandArgument);
                if (e.CommandName == "Excluir")
                {
                    new Business.FormaPgto().Excluir(identFormaPgto.CodFormaPgto,UsuarioAtivo.CodFuncionario.Value);
                    Pesquisar();
                }
                else if (e.CommandName == "Editar")
                {
                    hdfTipoAcao.Value = "Editar";
                    DadosFormaPgto = new Business.FormaPgto().ListarPorCodigo(identFormaPgto.CodFormaPgto)[0];
                    Master.PosicionarFoco(txtDescricao);
                    mpeIncluirFormaPgto.Show();
                }


            }

            protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    FormaPgtoVO tempFormaPgto = (FormaPgtoVO)e.Row.DataItem;

                    e.Row.Cells[1].Text = tempFormaPgto.CodFormaPgto.ToString();
                    e.Row.Cells[2].Text = tempFormaPgto.Descricao.ToString();
                    e.Row.Cells[3].Text = tempFormaPgto.Parcelas.ToString();
                    e.Row.Cells[4].Text = tempFormaPgto.Intervalo.ToString();

                    #region Botão Editar
                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                    imgEditar.CommandArgument = tempFormaPgto.CodFormaPgto.ToString();
                    imgEditar.CommandName = "Editar";
                    imgEditar.Style.Add("cursor", "hand");
                    imgEditar.ToolTip = "Editar dados do Forma de Pagamento [" + tempFormaPgto.Descricao.ToString() + "]";
                    #endregion

                    #region Botão Excluir
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                    // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                    imgExcluir.CommandArgument = tempFormaPgto.CodFormaPgto.ToString();
                    imgExcluir.CommandName = "Excluir";
                    imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Forma de Pagamento [" + tempFormaPgto.Descricao + "]?');";
                    imgExcluir.Style.Add("cursor", "hand");
                    imgExcluir.ToolTip = "Excluir Forma de Pagamento [" + tempFormaPgto.Descricao + "]";
                    #endregion

                    if (e.Row.RowState == DataControlRowState.Normal)
                        e.Row.CssClass = "FundoLinha1";
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                        e.Row.CssClass = "FundoLinha2";

                }
            }
            #endregion

            #region Métodos dos Botões


            protected void btnVoltar_Click(object sender, EventArgs e)
            {
                Response.Redirect("../Default.aspx");
            }
            protected void btnSalvar_Click(object sender, EventArgs e)
            {
                if (hdfTipoAcao.Value == "Incluir")
                {
                    new Business.FormaPgto().Incluir(DadosFormaPgto,UsuarioAtivo.CodFuncionario.Value);

                }
                else
                {
                    new Business.FormaPgto().Alterar(DadosFormaPgto, UsuarioAtivo.CodFuncionario.Value);
                }

                if (hdfCadastroPopup.Value != "sim")
                {
                    mpeIncluirFormaPgto.Hide();
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
                hdfTipoAcao.Value = "Incluir";
                LimparCampos();
                Master.PosicionarFoco(txtDescricao);
                mpeIncluirFormaPgto.Show();
            }

            protected void btnCancelar_Click(object sender, EventArgs e)
            {
                if (hdfCadastroPopup.Value != "sim")
                {
                    LimparCampos();
                    mpeIncluirFormaPgto.Hide();
                }
                else
                    ExecutarScript(new StringBuilder("window.close()"));

            }
            #endregion
        }
    }
