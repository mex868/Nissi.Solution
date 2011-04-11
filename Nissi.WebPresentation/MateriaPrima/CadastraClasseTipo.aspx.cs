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

    public partial class CadastraClasseTipo : BasePage
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
                    mpeIncluirClasseTipo.Show();
                }
            }
        }


        #region Propriedades
        public ClasseTipoVO DadosClasseTipo
        {
            set
            {
                if (value.CodClasseTipo > 0)
                    hdfCodClasseTipo.Value = value.CodClasseTipo.ToString();
                txtClasseTipo.Text = value.Descricao;
            }
            get
            {
                ClasseTipoVO identClasseTipo = new ClasseTipoVO();
                identClasseTipo.Descricao = txtClasseTipo.Text;
                if (!string.IsNullOrEmpty(hdfCodClasseTipo.Value))
                    identClasseTipo.CodClasseTipo = Convert.ToInt32(hdfCodClasseTipo.Value);
                return identClasseTipo;
            }
        }


        #endregion
        #region Métodos
        private void Pesquisar()
        {
            List<ClasseTipoVO> lClasseTipo = new List<ClasseTipoVO>();
            lClasseTipo = new ClasseTipo().Listar();
            if (lClasseTipo.Count > 0)
            {
                grdListaResultado.DataSource = lClasseTipo;
                grdListaResultado.DataBind();

            }
            else
            {
                grdListaResultado.DataSource = new List<ClasseTipoVO>();
                grdListaResultado.DataBind();
 
            }
        }

        private void LimparCampos()
        {
            hdfCodClasseTipo.Value =
            txtClasseTipo.Text = "";

        }
        #endregion

        #region Métodos da Grid

        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ClasseTipoVO identClasseTipo = new ClasseTipoVO();
            identClasseTipo.CodClasseTipo = Convert.ToInt16(e.CommandArgument);
            if (e.CommandName == "Excluir")
            {
                new ClasseTipo().Excluir(Convert.ToInt16(identClasseTipo.CodClasseTipo),UsuarioAtivo.CodFuncionario.Value);
                Pesquisar();
            }
            else if (e.CommandName == "Editar")
            {
                hdfTipoAcao.Value = "Editar";
                DadosClasseTipo = new ClasseTipo().ListarPorCodigo(identClasseTipo.CodClasseTipo.Value);
                Master.PosicionarFoco(txtClasseTipo);
                mpeIncluirClasseTipo.Show();
            }


        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ClasseTipoVO tempClasseTipo = (ClasseTipoVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempClasseTipo.CodClasseTipo.ToString();
                e.Row.Cells[2].Text = tempClasseTipo.Descricao;

                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempClasseTipo.CodClasseTipo.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do ClasseTipo [" + tempClasseTipo.Descricao.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
               // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempClasseTipo.CodClasseTipo.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do ClasseTipo [" + tempClasseTipo.Descricao.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir Cliente [" + tempClasseTipo.Descricao.Trim() + "]";
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
                new ClasseTipo().Incluir(DadosClasseTipo.Descricao,UsuarioAtivo.CodFuncionario.Value);

            }
            else
            {
                new ClasseTipo().Alterar(DadosClasseTipo.CodClasseTipo.Value, DadosClasseTipo.Descricao ,UsuarioAtivo.CodFuncionario.Value);
            }

            if (hdfCadastroPopup.Value != "sim")
            {
                mpeIncluirClasseTipo.Hide();
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
            Master.PosicionarFoco(txtClasseTipo);
            mpeIncluirClasseTipo.Show();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (hdfCadastroPopup.Value != "sim")
            {
                LimparCampos();
                mpeIncluirClasseTipo.Hide();
            }
            else
                ExecutarScript(new StringBuilder("window.close()"));

        }
        #endregion
    }
