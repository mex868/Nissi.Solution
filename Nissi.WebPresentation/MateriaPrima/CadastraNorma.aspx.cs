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

    public partial class CadastraNorma : BasePage
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
                    mpeIncluirNorma.Show();
                }
            }
        }


        #region Propriedades
        public NormaVO DadosNorma
        {
            set
            {
                if (value.CodNorma > 0)
                    hdfCodNorma.Value = value.CodNorma.ToString();
                txtNorma.Text = value.Descricao;
                txtRevisao.Text = value.Revisao.ToString();
            }
            get
            {
                NormaVO identNorma = new NormaVO();
                identNorma.Descricao = txtNorma.Text;
                identNorma.Revisao = !string.IsNullOrEmpty(txtRevisao.Text) ? int.Parse(txtRevisao.Text) : 0;
                if (!string.IsNullOrEmpty(hdfCodNorma.Value))
                    identNorma.CodNorma = Convert.ToInt16(hdfCodNorma.Value);
                return identNorma;
            }
        }


        #endregion
        #region Métodos
        private void Pesquisar()
        {
            List<NormaVO> lNorma = new List<NormaVO>();
            lNorma = new Norma().Listar();
            if (lNorma.Count > 0)
            {
                grdListaResultado.DataSource = lNorma;
                grdListaResultado.DataBind();

            }
            else
            {
                grdListaResultado.DataSource = new List<NormaVO>();
                grdListaResultado.DataBind();
 
            }
        }

        private void LimparCampos()
        {
            hdfCodNorma.Value =
            txtNorma.Text = "";

        }
        #endregion

        #region Métodos da Grid

        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            NormaVO identNorma = new NormaVO();
            identNorma.CodNorma = Convert.ToInt16(e.CommandArgument);
            if (e.CommandName == "Excluir")
            {
                new Norma().Excluir(Convert.ToInt16(identNorma.CodNorma),UsuarioAtivo.CodFuncionario.Value);
                Pesquisar();
            }
            else if (e.CommandName == "Editar")
            {
                hdfTipoAcao.Value = "Editar";
                DadosNorma = new Norma().ListarPorCodigo(identNorma.CodNorma);
                Master.PosicionarFoco(txtNorma);
                mpeIncluirNorma.Show();
            }


        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                NormaVO tempNorma = (NormaVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempNorma.CodNorma.ToString();
                e.Row.Cells[2].Text = tempNorma.Descricao+"/"+tempNorma.Revisao;

                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempNorma.CodNorma.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Norma [" + tempNorma.Descricao.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
               // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempNorma.CodNorma.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Norma [" + tempNorma.Descricao.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir Cliente [" + tempNorma.Descricao.Trim() + "]";
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
                new Norma().Incluir(DadosNorma.Descricao, DadosNorma.Revisao,UsuarioAtivo.CodFuncionario.Value);

            }
            else
            {
                new Norma().Alterar(DadosNorma.CodNorma, DadosNorma.Descricao, DadosNorma.Revisao,UsuarioAtivo.CodFuncionario.Value);
            }

            if (hdfCadastroPopup.Value != "sim")
            {
                mpeIncluirNorma.Hide();
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
            Master.PosicionarFoco(txtNorma);
            mpeIncluirNorma.Show();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (hdfCadastroPopup.Value != "sim")
            {
                LimparCampos();
                mpeIncluirNorma.Hide();
            }
            else
                ExecutarScript(new StringBuilder("window.close()"));

        }
        #endregion
    }
