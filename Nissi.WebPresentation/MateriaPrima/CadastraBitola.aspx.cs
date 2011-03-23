#region Using

using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Model;
using Nissi.Business;
using System.Collections.Generic;

#endregion

    namespace Nissi.WebPresentation.MateriaPrima
    {
        public partial class CadastraBitola : BasePage
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
                       // ((Page) this).Master.InibirTopo();
                        mpeIncluirBitola.Show();
                    }
                }
            }


            #region Propriedades
            public BitolaVO DadosBitola
            {
                set
                {
                    if (value.CodBitola > 0)
                        hdfCodBitola.Value = value.CodBitola.ToString();
                    txtBitola.Text = value.Bitola.ToString();
                }
                get
                {
                    BitolaVO identBitola = new BitolaVO();
                    identBitola.Bitola = !string.IsNullOrEmpty(txtBitola.Text)?decimal.Parse(txtBitola.Text):0;
                    if (!string.IsNullOrEmpty(hdfCodBitola.Value))
                        identBitola.CodBitola = Convert.ToInt16(hdfCodBitola.Value);
                    return identBitola;
                }
            }


            #endregion
            #region Métodos
            private void Pesquisar()
            {
                List<BitolaVO> lBitola = new List<BitolaVO>();
                lBitola = new Bitola().Listar();
                if (lBitola.Count > 0)
                {
                    grdListaResultado.DataSource = lBitola;
                    grdListaResultado.DataBind();

                }
                else
                {
                    grdListaResultado.DataSource = new List<BitolaVO>();
                    grdListaResultado.DataBind();
 
                }
            }

            private void LimparCampos()
            {
                hdfCodBitola.Value =
                    txtBitola.Text = "";

            }
            #endregion

            #region Métodos da Grid

            protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {

            }

            protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                BitolaVO identBitola = new BitolaVO();
                identBitola.CodBitola = Convert.ToInt16(e.CommandArgument);
                if (e.CommandName == "Excluir")
                {
                    new Bitola().Excluir(identBitola.CodBitola,UsuarioAtivo.CodFuncionario.Value);
                    Pesquisar();
                }
                else if (e.CommandName == "Editar")
                {
                    hdfTipoAcao.Value = "Editar";
                    DadosBitola = new Bitola().ListarPorCodigo(identBitola.CodBitola);
                    //((Page) this).Master.PosicionarFoco(txtBitola);
                    mpeIncluirBitola.Show();
                }


            }

            protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    BitolaVO tempBitola = (BitolaVO)e.Row.DataItem;

                    e.Row.Cells[1].Text = tempBitola.CodBitola.ToString();
                    e.Row.Cells[2].Text = tempBitola.Bitola.ToString();

                    #region Botão Editar
                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                    imgEditar.CommandArgument = tempBitola.CodBitola.ToString();
                    imgEditar.CommandName = "Editar";
                    imgEditar.Style.Add("cursor", "hand");
                    imgEditar.ToolTip = "Editar dados do Bitola [" + tempBitola.Bitola.ToString() + "]";
                    #endregion

                    #region Botão Excluir
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                    // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                    imgExcluir.CommandArgument = tempBitola.CodBitola.ToString();
                    imgExcluir.CommandName = "Excluir";
                    imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Bitola [" + tempBitola.Bitola + "]?');";
                    imgExcluir.Style.Add("cursor", "hand");
                    imgExcluir.ToolTip = "Excluir Bitola [" + tempBitola.Bitola + "]";
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
                    new Bitola().Incluir(DadosBitola.Bitola,UsuarioAtivo.CodFuncionario.Value);

                }
                else
                {
                    new Bitola().Alterar(DadosBitola.CodBitola, DadosBitola.Bitola,UsuarioAtivo.CodFuncionario.Value);
                }

                if (hdfCadastroPopup.Value != "sim")
                {
                    mpeIncluirBitola.Hide();
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
                //((Page) this).Master.PosicionarFoco(txtBitola);
                mpeIncluirBitola.Show();
            }

            protected void btnCancelar_Click(object sender, EventArgs e)
            {
                if (hdfCadastroPopup.Value != "sim")
                {
                    LimparCampos();
                    mpeIncluirBitola.Hide();
                }
                else
                    ExecutarScript(new StringBuilder("window.close()"));

            }
            #endregion
        }
    }
