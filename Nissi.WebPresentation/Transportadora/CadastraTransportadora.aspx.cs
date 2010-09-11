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

public partial class CadastraTransportadora :BasePage
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            LimparCampos();
            if (Request.QueryString["popup"] != null && Request.QueryString["popup"].ToString()=="sim")
            {
                //ArmazenaValorSessao("TipoAcao", "Incluir");
                hdfTipoAcao.Value = "Incluir";
                divConsulta.Style.Add("display","none");
                hdfCadastroPopup.Value = "sim";
                this.Master.InibirTopo();
                mpeTransIncluir.Show();
            }
        }
    }
    #endregion

    #region Propriedades
    public TransportadoraVO DadosTransportadora
        {
            set
            {
                hdfCodTransportadora.Value = value.CodTransportadora.ToString();
                hdfCodPessoa.Value = value.CodPessoa.ToString();
                txtCelular.Text = Mascara("TEL", value.Celular);
                CEPVO identCep = new CEPVO();
                identCep.CodCep = value.Cep.CodCep;
                identCep = new CEP().Listar(identCep);
                Endereco1.preencheCep(identCep,new ArrayList());
                txtTelefone.Text = Mascara("TEL", value.Telefone);
                txtCNPJ.Text = Mascara("CNPJ", value.CNPJ);
                Endereco1.Complemento = value.Complemento;
                txtContato.Text = value.Contato;
                txtEmail.Text = value.Email;
                txtFax.Text = Mascara("TEL",value.Fax);
                txtInscricaoEstadual.Text = value.InscricaoEstadual;
                txtNomeFantasia.Text = value.NomeFantasia;
                Endereco1.Numero = value.Numero;
                txtObservacao.Text = value.Observacao;
                txtRazaoSocial.Text = value.RazaoSocial;
                txtSite.Text = value.Site;
                txtContato.Text = value.Contato;
            }
            get
            {
                TransportadoraVO identTransportadoraVO = new TransportadoraVO();
                identTransportadoraVO.CodTransportadora = hdfCodTransportadora.Value != "" ? Convert.ToInt32(hdfCodTransportadora.Value.Replace(",", "")) : int.MinValue;
                identTransportadoraVO.CodPessoa = hdfCodPessoa.Value != "" ? Convert.ToInt32(hdfCodPessoa.Value.Replace(",", "")) : int.MinValue;
                identTransportadoraVO.Celular = txtCelular.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                identTransportadoraVO.Cep.CodCep = Endereco1.CodCep.ToString().Replace("-", "");
                identTransportadoraVO.Cep.Cidade.CodCidade = Convert.ToInt32(Endereco1.CodCidade);
                identTransportadoraVO.Complemento = Endereco1.Complemento;
                identTransportadoraVO.Numero = Endereco1.Numero;
                identTransportadoraVO.CNPJ = txtCNPJ.Text.Replace(".", "").Replace("-", "").Replace("/", "");
                identTransportadoraVO.InscricaoEstadual = txtInscricaoEstadual.Text;
                identTransportadoraVO.Email = txtEmail.Text;
                identTransportadoraVO.Site = txtSite.Text;
                identTransportadoraVO.Telefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                identTransportadoraVO.Fax = txtFax.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                identTransportadoraVO.RazaoSocial = txtRazaoSocial.Text;
                identTransportadoraVO.NomeFantasia = txtNomeFantasia.Text;
                identTransportadoraVO.Contato = txtContato.Text;
                return identTransportadoraVO;
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
    }
    #endregion

    #region btnCancelar_Click
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (hdfCadastroPopup.Value != "sim")
        {
            mpeTransIncluir.Hide();
        }
        else
        {
            ExecutarScript(new StringBuilder("window.close()"));
        }    
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
           hdfCodTransportadora.Value =  new Transportadora().Incluir(DadosTransportadora, 1).ToString(); }
        else
        { new Transportadora().Alterar(DadosTransportadora, 1); }

        if (hdfCadastroPopup.Value != "sim")
        {
            Pesquisar();
            LimparCampos();
            mpeTransIncluir.Hide();
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
        txtFax.Text =
        txtCNPJ.Text =
        txtSite.Text =
        txtEmail.Text =
        txtContato.Text =
        txtContato.Text =
        txtInscricaoEstadual.Text =
        txtTelefone.Text =
        txtTelefone.Text = 
        hdfCodPessoa.Value =
        txtObservacao.Text =
        txtRazaoSocial.Text =
        txtNomeFantasia.Text =
        hdfCodTransportadora.Value = "";
        Endereco1.limpaCampos();
        DestroiValorSessao("TipoAcao");
        mpeTransIncluir.Hide();
    }

    private void Pesquisar()
    {
        TransportadoraVO identTransportadora = new TransportadoraVO();
        if (!string.IsNullOrEmpty(hdfCodTransportadora.Value)) 
            identTransportadora.CodTransportadora = Convert.ToInt32(hdfCodTransportadora.Value);
        if (rbCNPJ.Checked)
            identTransportadora.CNPJ = txtCNPJ.Text;

        if (rbNomeFantasia.Checked)
            identTransportadora.NomeFantasia = txtNomeFantasiaPesq.Text;

        if (rbRazaoSocial.Checked)
            identTransportadora.RazaoSocial = txtRazao.Text;

         List<TransportadoraVO> lTransportadora = new Transportadora().Listar(identTransportadora);

         if (lTransportadora.Count > 0)
         {
             grdListaResultado.DataSource = lTransportadora;
             grdListaResultado.DataBind();
         }
         else
         {
             MensagemCliente("Não Existem Transportadoras Cadastradas");
         }
    }



    #endregion

    #region Métodos da Grid
    protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        TransportadoraVO identTransportadora = new TransportadoraVO();

        identTransportadora.CodTransportadora = int.Parse(e.CommandArgument.ToString());

        //Módulo de exclusão
        if (e.CommandName == "Excluir")
        {
            //Excluir
            new Transportadora().Excluir(identTransportadora.CodTransportadora);

            //Atualizar Lista
            Pesquisar();
        }
        else if (e.CommandName == "Editar")  //Módulo de alteração
        {
            //ArmazenaValorSessao("TipoAcao", "Editar");
            hdfTipoAcao.Value = "Editar";

            DadosTransportadora = new Transportadora().Listar(identTransportadora)[0];

            //Alimentar campos para edição
            upCadastro.Update();
            mpeTransIncluir.Show();
        }

    }

    protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TransportadoraVO tempTransportadora =(TransportadoraVO) e.Row.DataItem;

            e.Row.Cells[1].Text = tempTransportadora.RazaoSocial;
            e.Row.Cells[2].Text = tempTransportadora.NomeFantasia;
            e.Row.Cells[3].Text = Mascara("CNPJ", tempTransportadora.CNPJ);
            e.Row.Cells[4].Text = tempTransportadora.InscricaoEstadual;
            e.Row.Cells[5].Text = Mascara("TEL",tempTransportadora.Telefone);

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            imgEditar.CommandArgument = tempTransportadora.CodTransportadora.ToString();
            imgEditar.CommandName = "Editar";
            imgEditar.Style.Add("cursor", "hand");
            imgEditar.ToolTip = "Editar dados da Transportadora [" + tempTransportadora.RazaoSocial.Trim() + "]";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            imgExcluir.CommandArgument = tempTransportadora.CodPessoa.ToString();
            imgExcluir.CommandName = "Excluir";
            imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Cliente [" + tempTransportadora.RazaoSocial.Trim() + "]?');";
            imgExcluir.Style.Add("cursor", "hand");
            imgExcluir.ToolTip = "Excluir Cliente [" + tempTransportadora.RazaoSocial.Trim() + "]";
            #endregion

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";

        
        }

    }

    #endregion
}

