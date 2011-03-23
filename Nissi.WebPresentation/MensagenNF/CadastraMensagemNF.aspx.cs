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

namespace Nissi.WebPresentation
{
    public partial class cadastrarMensagemNF : BasePage
    {

       #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LimparCampos();
                Master.PosicionarFoco(txtCodigoPesq);
            }
        }

        #endregion

        #region Propriedades
        public MensagemNFVO DadosMensagemNF
        {
            set
            {
                hdfCodMensagemNF.Value = value.CodMensagemNF.ToString();
                txtDescricao.Text = value.Descricao;

            }
            get
            {
                MensagemNFVO identMensagemNFVO = new MensagemNFVO();
                identMensagemNFVO.CodMensagemNF = hdfCodMensagemNF.Value != "" ? Convert.ToInt32(hdfCodMensagemNF.Value.Replace(".", "").Replace("-", "").Replace("/", "")) : int.MinValue;
                identMensagemNFVO.Descricao = txtDescricao.Text;
                return identMensagemNFVO;
            }
        }
        #endregion

        #region Eventos

        #region btnPesquisar_Click
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
            tblConsulta.Style.Add("class", "fundotabela");
        }
        #endregion

        #region btnIncluir_Click
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            hdfTipoAcao.Value = "Incluir";
            LimparCampos();
            mpeTransIncluir.Show();
            Master.PosicionarFoco(txtDescricao);
        }
        #endregion

        #region btnCancelar_Click
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mpeTransIncluir.Hide();
        }
        #endregion

        #region btnVoltar_Click
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
        #endregion

        #region btnSalvar_Click
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (hdfTipoAcao.Value == "Incluir")
            {
             
               hdfCodMensagemNF.Value = new MensagemNF().Incluir(DadosMensagemNF,UsuarioAtivo.CodFuncionario.Value).ToString();
            }
            else
            { new MensagemNF().Alterar(DadosMensagemNF,UsuarioAtivo.CodFuncionario.Value); }

            Pesquisar();
            mpeTransIncluir.Hide();

        }
        #endregion

        #endregion

        #region Métodos

        private void LimparCampos()
        {
            txtDescricao.Text =
             hdfCodMensagemNF.Value = "";

            DestroiValorSessao("TipoAcao");
            mpeTransIncluir.Hide();
        }

        private void Pesquisar()
        {
            MensagemNFVO identMensagemNF = new MensagemNFVO();
            if (!string.IsNullOrEmpty(hdfCodMensagemNF.Value))
                identMensagemNF.CodMensagemNF = Convert.ToInt32(hdfCodMensagemNF.Value);

            if (!string.IsNullOrEmpty(txtCodigoPesq.Text))
                identMensagemNF.Descricao = txtCodigoPesq.Text;
               
            List<MensagemNFVO> lMensagemNF = new MensagemNF().Listar(identMensagemNF);

            if (lMensagemNF.Count > 0)
            {
                grdListaResultado.DataSource = lMensagemNF;
                grdListaResultado.DataBind();
            }
            else
            {
                MensagemCliente("Não Existem MensagemNFs Cadastradas");
                grdListaResultado.DataSource = new List<MensagemNFVO>();
                grdListaResultado.DataBind();
            }
        }

        #endregion

        #region Métodos da Grid
        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MensagemNFVO identMensagemNF = new MensagemNFVO();

            identMensagemNF.CodMensagemNF = int.Parse(e.CommandArgument.ToString());

            //Módulo de exclusão
            if (e.CommandName == "Excluir")
            {
                //Excluir
                new MensagemNF().Excluir((int)identMensagemNF.CodMensagemNF,UsuarioAtivo.CodFuncionario.Value);

                //Atualizar Lista
                Pesquisar();
            }
            else if (e.CommandName == "Editar")  //Módulo de alteração
            {
                //ArmazenaValorSessao("TipoAcao", "Editar");
                hdfTipoAcao.Value = "Editar";

                DadosMensagemNF = new MensagemNF().Listar(identMensagemNF)[0];

                //Alimentar campos para edição
                upCadastro.Update();
                mpeTransIncluir.Show();
            }

        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MensagemNFVO tempMensagemNF = (MensagemNFVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempMensagemNF.Descricao;


                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempMensagemNF.CodMensagemNF.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados da MensagemNF [" + tempMensagemNF.Descricao.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempMensagemNF.CodMensagemNF.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Cliente [" + tempMensagemNF.Descricao.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir MensagemNF [" + tempMensagemNF.Descricao.Trim() + "]";
                #endregion

                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";

            }
        }
        #endregion
        
    }
}






