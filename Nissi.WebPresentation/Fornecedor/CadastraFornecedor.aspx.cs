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

public partial class CadastraFornecedor : BasePage
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LimparCampos();
            CarregaComboTipoFornecimento();
        }
    }
    #endregion

    #region Propriedades
    public FornecedorVO DadosFornecedor
    {
        set
        {
            hdfCodFornecedor.Value = value.CodFornecedor.ToString();
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
            txtFax.Text = Mascara("TEL", value.Fax);
            txtInscricaoEstadual.Text = value.InscricaoEstadual;
            txtNomeFantasia.Text = value.NomeFantasia;
            Endereco1.Numero = value.Numero;
            txtObservacao.Text = value.Observacao;
            txtRazaoSocial.Text = value.RazaoSocial;
            txtSite.Text = value.Site;
            txtContato.Text = value.Contato;
            ucBanco.DadosBanco = value.Banco;
            ddlTipoFornecimento.SelectedValue = value.TipoFornecimento.CodTipoFornecimento.ToString();

        }
        get
        {
            FornecedorVO identFornecedorVO = new FornecedorVO();
            identFornecedorVO.CodFornecedor = hdfCodFornecedor.Value != "" ? Convert.ToInt32(hdfCodFornecedor.Value.Replace(".", "").Replace("-", "").Replace("/", "")) : int.MinValue;
            identFornecedorVO.CodPessoa = hdfCodPessoa.Value != "" ? Convert.ToInt32(hdfCodPessoa.Value.Replace(",", "")) : int.MinValue;
            identFornecedorVO.Celular = txtCelular.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
            identFornecedorVO.Cep.CodCep = Endereco1.CodCep.ToString().Replace("-", "");
            identFornecedorVO.Cep.Cidade.CodCidade = Convert.ToInt32(Endereco1.CodCidade);
            identFornecedorVO.Complemento = Endereco1.Complemento;
            identFornecedorVO.Numero = Endereco1.Numero;
            identFornecedorVO.CNPJ = txtCNPJ.Text.Replace(".", "").Replace("-", "").Replace("/", "");
            identFornecedorVO.Email = txtEmail.Text;
            identFornecedorVO.Site = txtSite.Text;
            identFornecedorVO.Telefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
            identFornecedorVO.Fax = txtFax.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
            identFornecedorVO.RazaoSocial = txtRazaoSocial.Text;
            identFornecedorVO.NomeFantasia = txtNomeFantasia.Text;
            identFornecedorVO.InscricaoEstadual = txtInscricaoEstadual.Text;
            identFornecedorVO.Observacao = txtObservacao.Text;
            identFornecedorVO.Contato = txtContato.Text;
            if (!string.IsNullOrEmpty(ddlTipoFornecimento.SelectedValue))
                identFornecedorVO.TipoFornecimento.CodTipoFornecimento = Convert.ToInt16(ddlTipoFornecimento.SelectedValue); 
            identFornecedorVO.Banco = ucBanco.DadosBanco;

            return identFornecedorVO;
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
            /*hdfCodFornecedor.Value =*/
            new Fornecedor().Incluir(DadosFornecedor, 1);
        }
        else
        { new Fornecedor().Alterar(DadosFornecedor, 1); }

        Pesquisar();
        LimparCampos();
        mpeTransIncluir.Hide();

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
        txtCelular.Text  =
        hdfCodPessoa.Value =
        txtObservacao.Text =
        txtRazaoSocial.Text =
        txtNomeFantasia.Text =
        hdfCodFornecedor.Value = "";
        Endereco1.limpaCampos();
        DestroiValorSessao("TipoAcao");
        mpeTransIncluir.Hide();
        ddlTipoFornecimento.SelectedIndex = 0;
        ucBanco.LimparCampos();
    }

    private void Pesquisar()
    {
        FornecedorVO identFornecedor = new FornecedorVO();
        if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
        {
            identFornecedor.CodPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
        }
        else
        {
            if (!string.IsNullOrEmpty(hdfCodFornecedor.Value))
                identFornecedor.CodFornecedor = Convert.ToInt32(hdfCodFornecedor.Value);
            if (rbCNPJ.Checked)
                identFornecedor.CNPJ = txtCNPJPesq.Text.Replace(".", "").Replace("-", "").Replace("/", ""); ;

            if (rbNomeFantasia.Checked)
                identFornecedor.NomeFantasia = txtNomeFantasiaPesq.Text;

            if (rbRazaoSocial.Checked)
                identFornecedor.RazaoSocial = txtRazao.Text;
        }
        List<FornecedorVO> lFornecedor = new Fornecedor().Listar(identFornecedor);

        if (lFornecedor.Count > 0)
        {
            grdListaResultado.DataSource = lFornecedor;
            grdListaResultado.DataBind();
        }
        else
        {
            MensagemCliente("Não Existem Fornecedors Cadastradas");
        }
        hdfIdRazaoSocial.Value = string.Empty;
    }

    private void CarregaComboTipoFornecimento()
    {
        ddlTipoFornecimento.DataSource = new TipoFornecimento().Listar(null);
        ddlTipoFornecimento.DataTextField = "Descricao";
        ddlTipoFornecimento.DataValueField = "CodTipoFornecimento";
        ddlTipoFornecimento.DataBind();
        ddlTipoFornecimento.Items.Insert(0, new ListItem());
    }




    #endregion

    #region Métodos da Grid
    protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        FornecedorVO identFornecedor = new FornecedorVO();

        identFornecedor.CodFornecedor = int.Parse(e.CommandArgument.ToString());

        //Módulo de exclusão
        if (e.CommandName == "Excluir")
        {
            //Excluir
            new Fornecedor().Excluir(identFornecedor.CodFornecedor);

            //Atualizar Lista
            Pesquisar();
        }
        else if (e.CommandName == "Editar")  //Módulo de alteração
        {
            //ArmazenaValorSessao("TipoAcao", "Editar");
            hdfTipoAcao.Value = "Editar";

            DadosFornecedor = new Fornecedor().Listar(identFornecedor)[0];

            //Alimentar campos para edição
            upCadastro.Update();
            mpeTransIncluir.Show();
        }

    }

    protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FornecedorVO tempFornecedor = (FornecedorVO)e.Row.DataItem;

            e.Row.Cells[1].Text = tempFornecedor.RazaoSocial;
            e.Row.Cells[2].Text = tempFornecedor.NomeFantasia;
            e.Row.Cells[3].Text = Mascara("CNPJ", tempFornecedor.CNPJ);
            e.Row.Cells[4].Text = tempFornecedor.InscricaoEstadual;
            e.Row.Cells[5].Text = Mascara("TEL", tempFornecedor.Telefone);

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            imgEditar.CommandArgument = tempFornecedor.CodFornecedor.ToString();
            imgEditar.CommandName = "Editar";
            imgEditar.Style.Add("cursor", "hand");
            imgEditar.ToolTip = "Editar dados da Fornecedor [" + tempFornecedor.RazaoSocial.Trim() + "]";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            imgExcluir.CommandArgument = tempFornecedor.CodPessoa.ToString();
            imgExcluir.CommandName = "Excluir";
            imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Cliente [" + tempFornecedor.RazaoSocial.Trim() + "]?');";
            imgExcluir.Style.Add("cursor", "hand");
            imgExcluir.ToolTip = "Excluir Cliente [" + tempFornecedor.RazaoSocial.Trim() + "]";
            #endregion

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";


        }

    }

    #endregion
}

