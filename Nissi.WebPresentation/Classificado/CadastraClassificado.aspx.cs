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

namespace Nissi.WebPresentation.Classificado
{
    public partial class CadastraClassificado : BasePage
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LimparCampos();
                Master.PosicionarFoco(txtLetraPesq);
                if (Request.QueryString["popup"] != null && Request.QueryString["popup"].ToString() == "sim")
                {
                    //ArmazenaValorSessao("TipoAcao", "Incluir");
                    hdfTipoAcao.Value = "Incluir";
                    hdfCadastroPopup.Value = "sim";
                    this.Master.InibirTopo();
                    Master.PosicionarFoco(txtLetra);
                    mpeTransIncluir.Show();
                }
            }
        }
        #endregion

        #region Propriedades
        public ClassificacaoFiscalVO DadosClassificacaoFiscal
        {
            set
            {
                hdfCodClassificacaoFiscal.Value = value.CodClassificacaoFiscal.ToString();
                txtNumero.Text = value.Numero;
                txtLetra.Text = value.Letra;
                

            }
            get
            {
                ClassificacaoFiscalVO identClassificacaoFiscalVO = new ClassificacaoFiscalVO();
                identClassificacaoFiscalVO.CodClassificacaoFiscal = hdfCodClassificacaoFiscal.Value != "" ? Convert.ToInt32(hdfCodClassificacaoFiscal.Value.Replace(".", "").Replace("-", "").Replace("/", "")) : int.MinValue;
                identClassificacaoFiscalVO.Letra =txtLetra.Text ;
                identClassificacaoFiscalVO.Numero = txtNumero.Text;
                identClassificacaoFiscalVO.UsuarioAlt = 1;
                identClassificacaoFiscalVO.UsuarioInc = 1;
                return identClassificacaoFiscalVO;
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
            Master.PosicionarFoco(txtLetra);
            mpeTransIncluir.Show();
        }
        #endregion

        #region btnCancelar_Click
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
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
               hdfCodClassificacaoFiscal.Value = new ClassificacaoFiscal().Incluir(DadosClassificacaoFiscal).ToString();
            }
            else
            { 
                new ClassificacaoFiscal().Alterar(DadosClassificacaoFiscal); 
            }
            if (hdfCadastroPopup.Value != "sim")
            {
                mpeTransIncluir.Hide();
                Pesquisar();
                LimparCampos();
            }
            else
            {
                ExecutarScript(new StringBuilder("window.close()"));
            }
        }
        #endregion

        #endregion

        #region Métodos

        private void LimparCampos()
        {
            txtLetra.Text =
             txtNumero.Text =
             hdfCodClassificacaoFiscal.Value = "";

            DestroiValorSessao("TipoAcao");
            mpeTransIncluir.Hide();
        }

        private void Pesquisar()
        {
            ClassificacaoFiscalVO identClassificacaoFiscal = new ClassificacaoFiscalVO();
            if (!string.IsNullOrEmpty(hdfCodClassificacaoFiscal.Value) || !string.IsNullOrEmpty(txtNumeroPesq .Text)|| !string.IsNullOrEmpty(txtLetraPesq.Text))
            {
                if (!string.IsNullOrEmpty(hdfCodClassificacaoFiscal.Value))
                    identClassificacaoFiscal.CodClassificacaoFiscal = Convert.ToInt32(hdfCodClassificacaoFiscal.Value);

                if (!string.IsNullOrEmpty(txtNumeroPesq.Text))
                    identClassificacaoFiscal.Numero = txtNumeroPesq.Text;
                if (!string.IsNullOrEmpty(txtLetraPesq.Text))
                    identClassificacaoFiscal.Letra = txtLetraPesq.Text;
            }
            else
                MensagemCliente("Favor informar pelo um filtro para pesquisa");

            List<ClassificacaoFiscalVO> lClassificacaoFiscal = new ClassificacaoFiscal().Listar(identClassificacaoFiscal);

            if (lClassificacaoFiscal.Count > 0)
            {
                grdListaResultado.DataSource = lClassificacaoFiscal;
                grdListaResultado.DataBind();
            }
            else
            {
                grdListaResultado.DataSource = new List<ClassificacaoFiscalVO>();
                grdListaResultado.DataBind();
                MensagemCliente("Não Existem ClassificacaoFiscais Cadastradas");
            }
        }

        #endregion

        #region Métodos da Grid
        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ClassificacaoFiscalVO identClassificacaoFiscal = new ClassificacaoFiscalVO();

            identClassificacaoFiscal.CodClassificacaoFiscal = int.Parse(e.CommandArgument.ToString());

            //Módulo de exclusão
            if (e.CommandName == "Excluir")
            {
                //Excluir
                new ClassificacaoFiscal().Excluir(identClassificacaoFiscal);

                //Atualizar Lista
                Pesquisar();
            }
            else if (e.CommandName == "Editar")  //Módulo de alteração
            {
                //ArmazenaValorSessao("TipoAcao", "Editar");
                hdfTipoAcao.Value = "Editar";

                DadosClassificacaoFiscal = new ClassificacaoFiscal().Listar(identClassificacaoFiscal)[0];

                //Alimentar campos para edição
                upCadastro.Update();
                mpeTransIncluir.Show();
            }

        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ClassificacaoFiscalVO tempClassificacaoFiscal = (ClassificacaoFiscalVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempClassificacaoFiscal.Letra;
                e.Row.Cells[2].Text = tempClassificacaoFiscal.Numero;

                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempClassificacaoFiscal.CodClassificacaoFiscal.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados da ClassificacaoFiscal [" + tempClassificacaoFiscal.Numero.Trim() + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempClassificacaoFiscal.CodClassificacaoFiscal.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão da Classificação fiscal [" + tempClassificacaoFiscal.Numero.Trim() + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir ClassificacaoFiscal [" + tempClassificacaoFiscal.Numero.Trim() + "]";
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


