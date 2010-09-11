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

    public partial class CadastraDepartamento : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Pesquisar();
            }
        }


        #region Propriedades
        public DepartamentoVO DadosDepartamento
        {
            set
            {
                if (value.CodDepartamento > 0)
                    hdfCodDepartamento.Value = value.CodDepartamento.ToString();
                txtDepartamento.Text = value.Nome;
            }
            get
            {
                DepartamentoVO identDepartamento = new DepartamentoVO();
                identDepartamento.Nome = txtDepartamento.Text;
                if (!string.IsNullOrEmpty(hdfCodDepartamento.Value))
                    identDepartamento.CodDepartamento = Convert.ToInt16(hdfCodDepartamento.Value);
                return identDepartamento;
            }
        }


        #endregion
        #region Métodos
        private void Pesquisar()
        {
            List<DepartamentoVO> lDepartamento = new List<DepartamentoVO>();
            lDepartamento = new Departamento().Listar(null);
            if (lDepartamento.Count > 0)
            {
                grdListaResultado.DataSource = lDepartamento;
                grdListaResultado.DataBind();

            }
            else 
            {
                grdListaResultado.DataSource = new List<DepartamentoVO>();
                grdListaResultado.DataBind();
            }
        }

        private void LimparCampos()
        {
            hdfCodDepartamento.Value =
            txtDepartamento.Text = "";

        }
        #endregion

        #region Métodos da Grid

        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DepartamentoVO identDepartamento = new DepartamentoVO();
            identDepartamento.CodDepartamento = Convert.ToInt16(e.CommandArgument);
            if (e.CommandName == "Excluir")
            {
                new Departamento().Excluir(Convert.ToInt16(identDepartamento.CodDepartamento),1);
                Pesquisar();
            }
            else if (e.CommandName == "Editar")
            {
                hdfTipoAcao.Value = "Editar";
                DadosDepartamento = new Departamento().Listar(identDepartamento.CodDepartamento)[0];
                mpeIncluirDepartamento.Show();
            }


        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DepartamentoVO tempDepartamento = (DepartamentoVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempDepartamento.CodDepartamento.ToString();
                e.Row.Cells[2].Text = tempDepartamento.Nome;

                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempDepartamento.CodDepartamento.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Departamento [" + tempDepartamento.Nome.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
               // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempDepartamento.CodDepartamento.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Departamento [" + tempDepartamento.Nome.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir Cliente [" + tempDepartamento.Nome.Trim() + "]";
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
                new Departamento().Incluir(DadosDepartamento,1);

            }
            else
            {
                new Departamento().Alterar(DadosDepartamento,1);
            }

            if (hdfCadastroPopup.Value != "sim")
            {
                mpeIncluirDepartamento.Hide();
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
            mpeIncluirDepartamento.Show();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (hdfCadastroPopup.Value != "sim")
            {
                LimparCampos();
                mpeIncluirDepartamento.Hide();
            }
            else
                ExecutarScript(new StringBuilder("window.close()"));

        }
        #endregion
    }
