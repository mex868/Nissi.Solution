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

    public partial class CadastroUnidade :BasePage
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
                    mpeIncluirUnidade.Show();
                }
            }
        }

  
        #region Propriedades
        public UnidadeVO DadosUnidade
        {
            set {
                if(value.CodUnidade > 0)
                hdfCodUnidade.Value = value.CodUnidade.ToString();
                txtDescricao.Text = value.Descricao;
                txtUnidade.Text = value.TipoUnidade;
            }
           get
            {
                UnidadeVO identUnidade = new UnidadeVO();
                identUnidade.Descricao = txtDescricao.Text;
                identUnidade.TipoUnidade = txtUnidade.Text;
                if (!string.IsNullOrEmpty(hdfCodUnidade.Value))
                    identUnidade.CodUnidade = Convert.ToInt32(hdfCodUnidade.Value);
                return identUnidade;
            }
        }


        #endregion

        #region Métodos
        private void Pesquisar()
        {
            List<UnidadeVO> lUnidade = new List<UnidadeVO>();
            lUnidade = new Unidade().Listar(new UnidadeVO());
            if (lUnidade.Count > 0)
            {
                grdListaResultado.DataSource = lUnidade;
                grdListaResultado.DataBind();

            }
        }

        private void LimparCampos()
        {
            hdfCodUnidade.Value =
            txtDescricao.Text =
            txtUnidade.Text = "";
            
        }
        #endregion

        #region Métodos da Grid

        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UnidadeVO identUnidade = new UnidadeVO();
            identUnidade.CodUnidade = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Excluir")
            {
                new Unidade().Excluir(identUnidade);
                Pesquisar();
            }
            else if (e.CommandName == "Editar")
            {
                hdfTipoAcao.Value = "Editar";
                DadosUnidade = new Unidade().Listar(identUnidade)[0];
                mpeIncluirUnidade.Show();
            }


        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UnidadeVO tempUnidade = (UnidadeVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempUnidade.CodUnidade.ToString();
                e.Row.Cells[2].Text = tempUnidade.Descricao;
                e.Row.Cells[3].Text = tempUnidade.TipoUnidade;

                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempUnidade.CodUnidade.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Unidade [" + tempUnidade.Descricao.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempUnidade.CodUnidade.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Unidade [" + tempUnidade.Descricao.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir Cliente [" + tempUnidade.Descricao.Trim() + "]";
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
               new Unidade().Incluir(DadosUnidade);

            }
            else 
            {
                new Unidade().Alterar(DadosUnidade);
            }

            if (hdfCadastroPopup.Value != "sim")
            {
                mpeIncluirUnidade.Hide();
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
            mpeIncluirUnidade.Show(); 
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (hdfCadastroPopup.Value != "sim")
            {
                LimparCampos();
                mpeIncluirUnidade.Hide();
            }
            else 
                ExecutarScript(new StringBuilder("window.close()"));

        }
        #endregion
    }
