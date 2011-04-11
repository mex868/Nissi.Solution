#region Using

using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Model;
using System.Collections.Generic;
using System.Linq;

#endregion

    namespace Nissi.WebPresentation.PrazoEntrega
    {
        public partial class CadastraPrazoEntrega : BasePage
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
                        //((Page) this).Master.InibirTopo();
                        mpeIncluirPrazoEntrega.Show();
                    }
                }
            }


            #region Propriedades
            public PrazoEntregaVO DadosPrazoEntregaVO
            {
                set
                {
                    if (value.CodPrazoEntrega > 0)
                        hdfCodPrazoEntrega.Value = value.CodPrazoEntrega.ToString();
                    txtDescricao.Text = value.Descricao;
                    txtIntervalo.Text = value.Dias.ToString();
                }
                get
                {
                    PrazoEntregaVO identPrazoEntregaVOVo = new PrazoEntregaVO();
                    identPrazoEntregaVOVo.Descricao = txtDescricao.Text;
                    if (!string.IsNullOrEmpty(txtIntervalo.Text))
                        identPrazoEntregaVOVo.Dias = short.Parse(txtIntervalo.Text);
                    if (!string.IsNullOrEmpty(hdfCodPrazoEntrega.Value))
                        identPrazoEntregaVOVo.CodPrazoEntrega = Convert.ToInt16((string) hdfCodPrazoEntrega.Value);
                    return identPrazoEntregaVOVo;
                }
            }


            #endregion
            #region Métodos
            private void Pesquisar()
            {
                List<PrazoEntregaVO> lPrazoEntregaVO = new List<PrazoEntregaVO>();
                lPrazoEntregaVO = new Business.PrazoEntrega().Listar();
                if (lPrazoEntregaVO.Count > 0)
                {
                    grdListaResultado.DataSource = lPrazoEntregaVO;
                    grdListaResultado.DataBind();

                }
                else
                {
                    grdListaResultado.DataSource = new List<PrazoEntregaVO>();
                    grdListaResultado.DataBind();
 
                }
            }

            private void LimparCampos()
            {
                hdfCodPrazoEntrega.Value =
                    txtDescricao.Text =
                    txtIntervalo.Text = string.Empty;

            }
            #endregion

            #region Métodos da Grid

            protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {

            }

            protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                PrazoEntregaVO identPrazoEntregaVO = new PrazoEntregaVO();
                identPrazoEntregaVO.CodPrazoEntrega = Convert.ToInt16(e.CommandArgument);
                if (e.CommandName == "Excluir")
                {
                    new Business.PrazoEntrega().Excluir(identPrazoEntregaVO.CodPrazoEntrega);
                    Pesquisar();
                }
                else if (e.CommandName == "Editar")
                {
                    hdfTipoAcao.Value = "Editar";
                    DadosPrazoEntregaVO = new Business.PrazoEntrega().ListarPorCodigo(identPrazoEntregaVO.CodPrazoEntrega);
                    Master.PosicionarFoco(txtDescricao);
                    mpeIncluirPrazoEntrega.Show();
                }
            }

            protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    PrazoEntregaVO tempBitola = (PrazoEntregaVO)e.Row.DataItem;

                    e.Row.Cells[1].Text = tempBitola.CodPrazoEntrega.ToString();
                    e.Row.Cells[2].Text = tempBitola.Descricao.ToString();
                    e.Row.Cells[3].Text = tempBitola.Dias.ToString();
                    #region Botão Editar
                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                    imgEditar.CommandArgument = tempBitola.CodPrazoEntrega.ToString();
                    imgEditar.CommandName = "Editar";
                    imgEditar.Style.Add("cursor", "hand");
                    imgEditar.ToolTip = "Editar dados do Forma de Pagamento [" + tempBitola.Descricao.ToString() + "]";
                    #endregion

                    #region Botão Excluir
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                    // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                    imgExcluir.CommandArgument = tempBitola.CodPrazoEntrega.ToString();
                    imgExcluir.CommandName = "Excluir";
                    imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Forma de Pagamento [" + tempBitola.Descricao + "]?');";
                    imgExcluir.Style.Add("cursor", "hand");
                    imgExcluir.ToolTip = "Excluir Forma de Pagamento [" + tempBitola.Descricao + "]";
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
                    new Business.PrazoEntrega().Incluir(DadosPrazoEntregaVO.Descricao, DadosPrazoEntregaVO.Dias, UsuarioAtivo.CodFuncionario.Value);
                }
                else
                {
                    new Business.PrazoEntrega().Alterar(DadosPrazoEntregaVO.CodPrazoEntrega, DadosPrazoEntregaVO.Descricao, DadosPrazoEntregaVO.Dias, UsuarioAtivo.CodFuncionario.Value);
                }

                if (hdfCadastroPopup.Value != "sim")
                {
                    mpeIncluirPrazoEntrega.Hide();
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
                mpeIncluirPrazoEntrega.Show();
            }

            protected void btnCancelar_Click(object sender, EventArgs e)
            {
                if (hdfCadastroPopup.Value != "sim")
                {
                    LimparCampos();
                    mpeIncluirPrazoEntrega.Hide();
                }
                else
                    ExecutarScript(new StringBuilder("window.close()"));

            }
            #endregion
        }
    }
