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
    public partial class Cadastrar_CFOP : BasePage
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
        public CFOPVO DadosCFOP
        {
            set
            {
                hdfCodCFOP.Value = value.CodCFOP.ToString();
                txtDescricao.Text = value.NaturezaOperacao;
                txtCodigo.Text = value.CFOP;
                

            }
            get
            {
                CFOPVO identCFOPVO = new CFOPVO();
                identCFOPVO.CodCFOP = hdfCodCFOP.Value != "" ? Convert.ToInt32(hdfCodCFOP.Value.Replace(".", "").Replace("-", "").Replace("/", "")) : int.MinValue;
                identCFOPVO.CFOP =txtCodigo.Text ;
                identCFOPVO.NaturezaOperacao = txtDescricao.Text;
                identCFOPVO.UsuarioAlt = 1;
                identCFOPVO.UsuarioInc = 1;
                return identCFOPVO;
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
            Master.PosicionarFoco(txtCodigo);
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
                
               hdfCodCFOP.Value = new CFOP().Incluir(DadosCFOP,1).ToString();
            }
            else
            { new CFOP().Alterar(DadosCFOP); }

            Pesquisar();
            mpeTransIncluir.Hide();

        }
        #endregion

        #endregion

        #region Métodos

        private void LimparCampos()
        {
            txtCodigo.Text =
             txtDescricao.Text =
             hdfCodCFOP.Value = "";

            DestroiValorSessao("TipoAcao");
            mpeTransIncluir.Hide();
        }

        private void Pesquisar()
        {
            CFOPVO identCFOP = new CFOPVO();
            if (!string.IsNullOrEmpty(hdfCodCFOP.Value) || !string.IsNullOrEmpty(txtDescPesq .Text)|| !string.IsNullOrEmpty(txtCodigoPesq.Text))
            {
                if (!string.IsNullOrEmpty(hdfCodCFOP.Value))
                    identCFOP.CodCFOP = Convert.ToInt32(hdfCodCFOP.Value);

                if (!string.IsNullOrEmpty(txtDescPesq.Text))
                    identCFOP.NaturezaOperacao = txtDescPesq.Text;
                if (!string.IsNullOrEmpty(txtCodigoPesq.Text))
                    identCFOP.CFOP = txtCodigoPesq.Text;
            }
            else
                MensagemCliente("Favor informar pelo um filtro para pesquisa");

            List<CFOPVO> lCFOP = new CFOP().Listar(identCFOP);

            if (lCFOP.Count > 0)
            {
                grdListaResultado.DataSource = lCFOP;
                grdListaResultado.DataBind();
            }
            else
            {
                MensagemCliente("Não Existem CFOPs Cadastradas");
            }
        }

        #endregion

        #region Métodos da Grid
        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CFOPVO identCFOP = new CFOPVO();

            identCFOP.CodCFOP = int.Parse(e.CommandArgument.ToString());

            //Módulo de exclusão
            if (e.CommandName == "Excluir")
            {
                //Excluir
                new CFOP().Excluir((int)identCFOP.CodCFOP);

                //Atualizar Lista
                Pesquisar();
            }
            else if (e.CommandName == "Editar")  //Módulo de alteração
            {
                //ArmazenaValorSessao("TipoAcao", "Editar");
                hdfTipoAcao.Value = "Editar";

                DadosCFOP = new CFOP().Listar(identCFOP)[0];

                //Alimentar campos para edição
                upCadastro.Update();
                mpeTransIncluir.Show();
            }

        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CFOPVO tempCFOP = (CFOPVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempCFOP.CFOP;
                e.Row.Cells[2].Text = tempCFOP.NaturezaOperacao;

                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempCFOP.CodCFOP.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados da CFOP [" + tempCFOP.CFOP.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempCFOP.CodCFOP.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Cliente [" + tempCFOP.CFOP.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir CFOP [" + tempCFOP.CFOP.Trim() + "]";
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


