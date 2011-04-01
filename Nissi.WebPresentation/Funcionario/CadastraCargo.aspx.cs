#region Using
using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Nissi.Model;
using Nissi.Business;
using System.Globalization;
using System.Collections.Generic;
#endregion

    public partial class CadastraCargo : BasePage
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
                    mpeIncluirCargo.Show();
                }
            }
        }


        #region Propriedades
        public CargoVO DadosCargo
        {
            set
            {
                if (value.CodCargo > 0)
                    hdfCodCargo.Value = value.CodCargo.ToString();
                txtCargo.Text = value.Nome;
            }
            get
            {
                CargoVO identCargo = new CargoVO();
                identCargo.Nome = txtCargo.Text;
                if (!string.IsNullOrEmpty(hdfCodCargo.Value))
                    identCargo.CodCargo = Convert.ToInt16(hdfCodCargo.Value);
                return identCargo;
            }
        }


        #endregion
        #region Métodos
        private void Pesquisar()
        {
            List<CargoVO> lCargo = new List<CargoVO>();
            lCargo = new Cargo().Listar(null);
            if (lCargo.Count > 0)
            {
                grdListaResultado.DataSource = lCargo;
                grdListaResultado.DataBind();

            }
            else
            {
                grdListaResultado.DataSource = new List<CargoVO>();
                grdListaResultado.DataBind();
 
            }
        }

        private void LimparCampos()
        {
            hdfCodCargo.Value =
            txtCargo.Text = "";

        }
        #endregion

        #region Métodos da Grid

        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CargoVO identCargo = new CargoVO();
            identCargo.CodCargo = Convert.ToInt16(e.CommandArgument);
            if (e.CommandName == "Excluir")
            {
                new Cargo().Excluir(Convert.ToInt16(identCargo.CodCargo),UsuarioAtivo.CodFuncionario.Value);
                Pesquisar();
            }
            else if (e.CommandName == "Editar")
            {
                hdfTipoAcao.Value = "Editar";
                DadosCargo = new Cargo().Listar(identCargo.CodCargo)[0];
                mpeIncluirCargo.Show();
            }


        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CargoVO tempCargo = (CargoVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempCargo.CodCargo.ToString();
                e.Row.Cells[2].Text = tempCargo.Nome;

                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempCargo.CodCargo.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Cargo [" + tempCargo.Nome.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
               // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempCargo.CodCargo.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Cargo [" + tempCargo.Nome.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir Cliente [" + tempCargo.Nome.Trim() + "]";
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
                new Cargo().Incluir(DadosCargo,UsuarioAtivo.CodFuncionario.Value);

            }
            else
            {
                new Cargo().Alterar(DadosCargo,UsuarioAtivo.CodFuncionario.Value);
            }

            if (hdfCadastroPopup.Value != "sim")
            {
                mpeIncluirCargo.Hide();
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
            mpeIncluirCargo.Show();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (hdfCadastroPopup.Value != "sim")
            {
                LimparCampos();
                mpeIncluirCargo.Hide();
            }
            else
                ExecutarScript(new StringBuilder("window.close()"));

        }
        #endregion
    }
