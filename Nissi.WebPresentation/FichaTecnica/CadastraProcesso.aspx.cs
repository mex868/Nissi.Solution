#region Using

using System;
using System.Text;
using System.Web.UI.WebControls;
using Nissi.Model;
using Nissi.Business;
using System.Collections.Generic;

#endregion

namespace Nissi.WebPresentation.FichaTecnica
{
    public partial class CadastraProcesso : BasePage
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
                    mpeIncluirProcesso.Show();
                }
            }
        }


        #region Propriedades
        public ProcessoVO DadosProcesso
        {
            set
            {
                if (value.CodProcesso > 0)
                    hdfCodProcesso.Value = value.CodProcesso.ToString();
                txtProcesso.Text = value.Descricao;
            }
            get
            {
                ProcessoVO identProcesso = new ProcessoVO();
                identProcesso.Descricao = txtProcesso.Text;
                if (!string.IsNullOrEmpty(hdfCodProcesso.Value))
                    identProcesso.CodProcesso = Convert.ToInt32(hdfCodProcesso.Value);
                return identProcesso;
            }
        }


        #endregion
        #region Métodos
        private void Pesquisar()
        {
            List<ProcessoVO> lProcesso = new List<ProcessoVO>();
            lProcesso = Processo.Listar();
            if (lProcesso.Count > 0)
            {
                grdListaResultado.DataSource = lProcesso;
                grdListaResultado.DataBind();

            }
            else
            {
                grdListaResultado.DataSource = new List<ProcessoVO>();
                grdListaResultado.DataBind();
            }
        }

        private void LimparCampos()
        {
            hdfCodProcesso.Value =
                txtProcesso.Text = "";

        }
        #endregion

        #region Métodos da Grid

        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProcessoVO identProcesso = new ProcessoVO();
            identProcesso.CodProcesso = Convert.ToInt16(e.CommandArgument);
            if (e.CommandName == "Excluir")
            {
                Processo.Excluir(Convert.ToInt16(identProcesso.CodProcesso), UsuarioAtivo.CodFuncionario.Value);
                Pesquisar();
            }
            else if (e.CommandName == "Editar")
            {
                hdfTipoAcao.Value = "Editar";
                DadosProcesso = Processo.ListarPorCodigo(identProcesso.CodProcesso);
                Master.PosicionarFoco(txtProcesso);
                mpeIncluirProcesso.Show();
            }


        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProcessoVO tempProcesso = (ProcessoVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempProcesso.CodProcesso.ToString();
                e.Row.Cells[2].Text = tempProcesso.Descricao;

                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempProcesso.CodProcesso.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Processo [" + tempProcesso.Descricao.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempProcesso.CodProcesso.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Processo [" + tempProcesso.Descricao.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir Cliente [" + tempProcesso.Descricao.Trim() + "]";
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
                Processo.Incluir(DadosProcesso.Descricao,UsuarioAtivo.CodFuncionario.Value);

            }
            else
            {
                Processo.Alterar(DadosProcesso.CodProcesso, DadosProcesso.Descricao ,UsuarioAtivo.CodFuncionario.Value);
            }

            if (hdfCadastroPopup.Value != "sim")
            {
                mpeIncluirProcesso.Hide();
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
            Master.PosicionarFoco(txtProcesso);
            mpeIncluirProcesso.Show();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (hdfCadastroPopup.Value != "sim")
            {
                LimparCampos();
                mpeIncluirProcesso.Hide();
            }
            else
                ExecutarScript(new StringBuilder("window.close()"));

        }
        #endregion
    }
}
