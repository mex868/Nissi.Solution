#region Using

using System;
using System.Text;
using System.Web.UI.WebControls;
using Nissi.Model;
using Nissi.Business;
using System.Collections.Generic;
using Nissi.Util;

#endregion

    namespace Nissi.WebPresentation.FichaTecnica
    {
        public partial class CadastraCaracteristica : BasePage
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
                        mpeIncluirCaracteristica.Show();
                    }
                }
            }


            #region Propriedades
            public CaracteristicaVO DadosCaracteristica
            {
                set
                {
                    if (value.CodCaracteristica > 0)
                        hdfCodCaracteristica.Value = value.CodCaracteristica.ToString();
                    txtCaracteristica.Text = value.Descricao;
                    ddlTipo.SelectedValue = value.Tipo.ToString();
                }
                get
                {
                    CaracteristicaVO identCaracteristica = new CaracteristicaVO();
                    identCaracteristica.Descricao = txtCaracteristica.Text;
                    if (!string.IsNullOrEmpty(hdfCodCaracteristica.Value))
                        identCaracteristica.CodCaracteristica = Convert.ToInt16(hdfCodCaracteristica.Value);
                    if (!string.IsNullOrEmpty(ddlTipo.SelectedValue))
                        identCaracteristica.Tipo = short.Parse(ddlTipo.SelectedValue);
                    return identCaracteristica;
                }
            }


            #endregion
            #region Métodos
            private void Pesquisar()
            {
                List<CaracteristicaVO> lCaracteristica = new List<CaracteristicaVO>();
                lCaracteristica = Caracteristica.Listar();
                if (lCaracteristica.Count > 0)
                {
                    grdListaResultado.DataSource = lCaracteristica;
                    grdListaResultado.DataBind();

                }
                else
                {
                    grdListaResultado.DataSource = new List<CaracteristicaVO>();
                    grdListaResultado.DataBind();
 
                }
            }
            private void CarregarCombo()
            {
                Geral.CarregarDDL(ref ddlTipo, Caracteristica.ListarTipoCaracteristica().ToArray(),"Codigo","Descricao");
            }
            private void LimparCampos()
            {
                hdfCodCaracteristica.Value =
                    txtCaracteristica.Text = "";

            }
            #endregion

            #region Métodos da Grid

            protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {

            }

            protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                CaracteristicaVO identCaracteristica = new CaracteristicaVO();
                identCaracteristica.CodCaracteristica = Convert.ToInt16(e.CommandArgument);
                if (e.CommandName == "Excluir")
                {
                    Caracteristica.Excluir(identCaracteristica.CodCaracteristica,UsuarioAtivo.CodFuncionario.Value);
                    Pesquisar();
                }
                else if (e.CommandName == "Editar")
                {
                    hdfTipoAcao.Value = "Editar";
                    DadosCaracteristica =  Caracteristica.ListarPorCodigo(identCaracteristica.CodCaracteristica);
                    Master.PosicionarFoco(txtCaracteristica);
                    mpeIncluirCaracteristica.Show();
                }


            }

            protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CaracteristicaVO tempCaracteristica = (CaracteristicaVO)e.Row.DataItem;

                    e.Row.Cells[1].Text = tempCaracteristica.CodCaracteristica.ToString();
                    e.Row.Cells[2].Text = tempCaracteristica.Descricao;
                    if (tempCaracteristica.Tipo != null)
                        e.Row.Cells[3].Text = Caracteristica.ListarTipoCaracteristicaDescricao(tempCaracteristica.Tipo.Value);
                    #region Botão Editar
                    ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                    //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                    imgEditar.CommandArgument = tempCaracteristica.CodCaracteristica.ToString();
                    imgEditar.CommandName = "Editar";
                    imgEditar.Style.Add("cursor", "hand");
                    imgEditar.ToolTip = "Editar dados do Característica [" + tempCaracteristica.Descricao.ToString() + "]";
                    #endregion

                    #region Botão Excluir
                    ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                    // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                    imgExcluir.CommandArgument = tempCaracteristica.CodCaracteristica.ToString();
                    imgExcluir.CommandName = "Excluir";
                    imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Característica [" + tempCaracteristica.Descricao + "]?');";
                    imgExcluir.Style.Add("cursor", "hand");
                    imgExcluir.ToolTip = "Excluir Característica [" + tempCaracteristica.Descricao + "]";
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
                    Caracteristica.Incluir(DadosCaracteristica.Descricao,DadosCaracteristica.Tipo, UsuarioAtivo.CodFuncionario.Value);

                }
                else
                {
                    Caracteristica.Alterar(DadosCaracteristica.CodCaracteristica, DadosCaracteristica.Descricao,DadosCaracteristica.Tipo, UsuarioAtivo.CodFuncionario.Value);
                }

                if (hdfCadastroPopup.Value != "sim")
                {
                    mpeIncluirCaracteristica.Hide();
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
                CarregarCombo();
                Master.PosicionarFoco(txtCaracteristica);
                mpeIncluirCaracteristica.Show();
            }

            protected void btnCancelar_Click(object sender, EventArgs e)
            {
                if (hdfCadastroPopup.Value != "sim")
                {
                    LimparCampos();
                    mpeIncluirCaracteristica.Hide();
                }
                else
                    ExecutarScript(new StringBuilder("window.close()"));

            }
            #endregion
        }
    }
