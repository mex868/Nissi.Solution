#region Using
using System;
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


public partial class CadastraCliente : BasePage
{
    #region Dados do Cliente
    private ClienteVO DadosCliente
    {
        get
        {
            ClienteVO identCliente = new ClienteVO();
            identCliente.IndPessoaTipo = ddlTipoPessoa.SelectedValue == "1" ? true : false;
            if (hdfTipoAcao.Value.Equals("Editar"))
                identCliente.CodPessoa = Convert.ToInt32(hdfCodCliente.Value);
            identCliente.CodRef = txtCodigo.Text;
            identCliente.Cep.CodCep = Endereco.CodCep.Replace("-", "");
            identCliente.Celular = txtCelular.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
            identCliente.Telefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
            identCliente.Fax = txtFax.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
            if (ddlTipoPessoa.SelectedValue == "1")
                identCliente.CNPJ = txtCNPJ.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
            else
                identCliente.CNPJ = txtCPFCadastro.Text.Replace(".", "").Replace("-", "").Trim();

            identCliente.Complemento = Endereco.Complemento;
            identCliente.Contato = txtContato.Text;
            identCliente.Email = txtEmail.Text;
            identCliente.InscricaoEstadual = txtInscricaoEstadual.Text;
            identCliente.NomeFantasia = txtNomeFantasia.Text;
            identCliente.Numero = Endereco.Numero;
            identCliente.Observacao = txtObservacao.Text;
            identCliente.RazaoSocial = txtRazaoSocial.Text;
            identCliente.Site = txtSite.Text;
            identCliente.Tipo = "C";
            identCliente.Funcionario.CodFuncionario = null;
            if (ddlFuncionario.SelectedIndex != 0)
                identCliente.Funcionario.CodFuncionario = Convert.ToInt32(ddlFuncionario.SelectedValue);
            identCliente.EmailNFE = txtEmailNFE.Text;
            if (!string.IsNullOrEmpty(txtCepCobranca.Text)) 
                identCliente.CepCobranca = txtCepCobranca.Text.Replace("-","");
            identCliente.EnderecoCobranca = txtEnderecoCobranca.Text;
            return identCliente;
        }
    }
    #endregion

    #region carregaDados
    private void carregaDados(ClienteVO identCliente)
    {
        //hdfCodCliente.Value = identCliente.CodPessoa.ToString();
        txtCodigo.Text = identCliente.CodRef.ToString();
        txtCelular.Text = Mascara("TEL", identCliente.Celular);
        Endereco.preencheCep(identCliente.Cep,new ArrayList());
        txtTelefone.Text = Mascara("TEL", identCliente.Telefone);
        if (ddlTipoPessoa.SelectedValue =="1")
            txtCNPJ.Text = Mascara("CNPJ", identCliente.CNPJ);
        else
            txtCPFCadastro.Text = Mascara("CPF", identCliente.CNPJ);
        Endereco.Complemento = identCliente.Complemento;
        txtContato.Text = identCliente.Contato;
        txtEmail.Text = identCliente.Email;
        txtFax.Text = Mascara("TEL", identCliente.Fax);
        txtInscricaoEstadual.Text = identCliente.InscricaoEstadual;
        txtNomeFantasia.Text = identCliente.NomeFantasia;
        Endereco.Numero = identCliente.Numero;
        txtObservacao.Text = identCliente.Observacao;
        txtRazaoSocial.Text = identCliente.RazaoSocial;
        txtSite.Text = identCliente.Site;
        txtContato.Text = identCliente.Contato;
        ddlFuncionario.SelectedValue = identCliente.Funcionario.CodFuncionario.ToString();
        txtEmailNFE.Text = identCliente.EmailNFE;
        txtCepCobranca.Text = identCliente.CepCobranca;
        txtEnderecoCobranca.Text = identCliente.EnderecoCobranca;
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            limparCampos();
            preencherFuncionario();
        }
    }
    #endregion

    #region LimparCampos
    private void limparCampos()
    {
        hdfCodCliente.Value = "0";
        txtCodigo.Text =
        txtFax.Text =
        txtCNPJ.Text =
        txtEmail.Text =
        txtSite.Text =
        txtContato.Text =
        txtCelular.Text =
        txtTelefone.Text =
        hdfTipoAcao.Value =
        txtObservacao.Text =
        txtRazaoSocial.Text =
        txtCPFCadastro.Text =
        txtNomeFantasia.Text =
        hdnListaTransportadora.Value =
        txtCepCobranca.Text =
        txtEnderecoCobranca.Text =
        txtInscricaoEstadual.Text = string.Empty;
        lbxAssociar.Items.Clear();
        lbxAssociados.Items.Clear();
        Endereco.limpaCampos();
        ddlFuncionario.SelectedIndex = 0;
        DestroiValorSessao("lTransportadora");
    }
    #endregion
    
    #region preencherFuncionario
    private void preencherFuncionario()
    {
        FuncionarioVO identFuncionario = new FuncionarioVO();
        ddlFuncionario.DataSource = new Funcionario().Listar();
        ddlFuncionario.DataTextField = "RazaoSocial";
        ddlFuncionario.DataValueField = "CodFuncionario";
        ddlFuncionario.DataBind();
        ddlFuncionario.Items.Insert(0, new ListItem("",""));
    }
    #endregion

    #region Evento dos Botões

    #region btnPesquisar_Click
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
    }   
    #endregion

    #region btnIncluir_Click
    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        limparCampos();
        hdfTipoAcao.Value = "Incluir";
        divCNPJCadastro.Style.Add("display", "none");
        divCPFCadastro.Style.Add("display", "none");
        if (ddlTipoPessoa.SelectedValue == "2")
        {
            tdCNPJ.InnerText = "CPF";
            tdInscricaoEstadual.InnerText = "RG";
            tdRazaoSocial.InnerText = "Nome";
            divCPFCadastro.Style.Add("display", "block");
        }
        else
        {
            tdCNPJ.InnerText = "C.N.P.J";
            tdInscricaoEstadual.InnerText = "Inscrição Estadual";
            tdRazaoSocial.InnerText = "Razão Social";
            tdNomeFantasia.InnerText = "Nome Fantasia";
            divCNPJCadastro.Style.Add("display", "block");
        }
        CarregarListaTransportadora();
        Master.PosicionarFoco(txtCodigo);
        mpeIncluir.Show();
    }
    #endregion

    #region btnCancelar_Click
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        hdfCodCliente.Value = string.Empty;
        mpeIncluir.Hide();
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
        //String Xml das Funcionalidades selecionadas
        string xmlLista = new Transportadora().GerarXmlListaTransportadora(hdnListaTransportadora.Value.Split('|'));
        if (hdfTipoAcao.Value == "Incluir") 
        {
            hdfCodCliente.Value = new Cliente().Incluir(DadosCliente,xmlLista,1).ToString();
        }
        else
        { new Cliente().Alterar(DadosCliente, xmlLista, 1); }

        Pesquisar();
        hdfCodCliente.Value = string.Empty;
        mpeIncluir.Hide();
    }
    #endregion

    #region btnAtualizarLista_Click
    protected void btnAtualizar_Click(object sender, EventArgs e)
    {
        DestroiValorSessao("lTransportadora");
        CarregarListaTransportadora();
    }

    protected void ddlTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdListaResultado.DataSource = new List<Cliente>();
        grdListaResultado.DataBind();
        updListaResultado.Update();
    }

    #endregion


    #endregion

    #region Pesquisar
    private void Pesquisar()
    {
        ClienteVO identCliente = new ClienteVO();
        identCliente.IndPessoaTipo = ddlTipoPessoa.SelectedValue == "1"? true: false;
        if (!string.IsNullOrEmpty(hdfCodCliente.Value))
            identCliente.CodPessoa = Convert.ToInt32(hdfCodCliente.Value);
        if (rbCodigo.Checked)
            identCliente.CodRef = txtCodigoPesq.Text;
        if (rbCNPJ.Checked)
        {
            if (ddlTipoPessoa.SelectedValue == "1")
                identCliente.CNPJ = txtCNPJPesq.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
            if (ddlTipoPessoa.SelectedValue == "2")
                identCliente.CNPJ = txtCPFPesq.Text.Replace(".", "").Replace("-", "").Trim();
        }
        if (rbNomeFantasia.Checked)
               identCliente.NomeFantasia = txtNomeFantasiaPesq.Text;
        if (rbRazaoSocial.Checked)
                identCliente.RazaoSocial = txtRazao.Text;

        List<ClienteVO>lCliente = new Cliente().Listar(identCliente);
        if (lCliente.Count > 0)
        {
            grdListaResultado.DataSource = lCliente;
            grdListaResultado.DataBind();
            updListaResultado.Update();
        }
        else
        {
            limparCampos();
            MensagemCliente("Não existem registros para o filtro informado.");
        }
    }
    #endregion

    #region Carregar Lista de Transportadoras
    private void CarregarListaTransportadora()
    {
        //caso seja incusão
        if (hdfTipoAcao.Value == "Incluir")
        {
            //Listagem de todas as funcionalidades, use de sessão para evitar muitas chamadas ao banco
            List<TransportadoraVO> lTransportadora;
            if (Session["lTransportadora"] == null)
            {
                lTransportadora = new Transportadora().Listar();
                ArmazenaValorSessao("lTransportadora", lTransportadora);
            }
            else
                lTransportadora = (List<TransportadoraVO>)RecuperaValorSessao("lTransportadora");

            //Populando todas as localidades
            if (lTransportadora.Count > 0)
            {
                lbxAssociar.DataSource = lTransportadora;
                lbxAssociar.DataValueField = "CodTransportadora";
                lbxAssociar.DataTextField = "RazaoSocial";
                lbxAssociar.DataBind();
            }

            //Zerando as localidade associadas
            lbxAssociados.Items.Clear();
        }
        else if (hdfTipoAcao.Value == "Editar")//caso seja alteração
        {
            int CodPessoa = int.Parse(hdfCodCliente.Value);
            List<TransportadoraVO> listaTransportadoraCliente = new List<TransportadoraVO>();

            //Listagem das localidades da sala
            if (Session["listaTransportadoraCliente"] == null)
            {
                listaTransportadoraCliente = new Cliente().ListarPorCliente(CodPessoa);
            }
            else
            {
                listaTransportadoraCliente = (List<TransportadoraVO>)RecuperaValorSessao("listaTransportadoraCliente");
            }

            //Listagem de todas as Localidades (exceto as da Sala)
            List<TransportadoraVO> listaTransportadoraExcetoDoCliente = new Transportadora().ListarExcetoTransportadoraCliente(listaTransportadoraCliente);

            //Populando todas as localidades
            lbxAssociar.DataSource = listaTransportadoraExcetoDoCliente;
            lbxAssociar.DataValueField = "CodTransportadora";
            lbxAssociar.DataTextField = "RazaoSocial";
            lbxAssociar.DataBind();

            //Populando Localidades da Sala
            lbxAssociados.DataSource = listaTransportadoraCliente;
            lbxAssociados.DataValueField = "CodTransportadora";
            lbxAssociados.DataTextField = "RazaoSocial";
            lbxAssociados.DataBind();
        }
    }
    #endregion
 
    #region Métodos do Grid
    protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ClienteVO identCliente = new ClienteVO();
        identCliente.CodPessoa = int.Parse(e.CommandArgument.ToString());
        identCliente.IndPessoaTipo = ddlTipoPessoa.SelectedValue == "1" ? true : false;

        //Módulo de exclusão
        if (e.CommandName == "Excluir")
        {
            //Excluir
            new Cliente().Excluir(identCliente.CodPessoa);

            //Atualizar Lista
            Pesquisar();
           updListaResultado.Update();
        }
        else if (e.CommandName == "Editar")  //Módulo de alteração
        {
           // ArmazenaValorSessao("TipoAcao","Editar") ;
            hdfTipoAcao.Value = "Editar";
            hdfCodCliente.Value = identCliente.CodPessoa.ToString();
            identCliente = new Cliente().Listar(identCliente)[0];
            lblTitulo.Text = "Alteração de Cliente";
            divCNPJCadastro.Style.Add("display", "none");
            divCPFCadastro.Style.Add("display", "none");
            if (ddlTipoPessoa.SelectedValue == "2")
            {
                tdCNPJ.InnerText = "CPF";
                tdInscricaoEstadual.InnerText = "RG";
                tdRazaoSocial.InnerText = "Nome";
                divCPFCadastro.Style.Add("display", "block");
            }
            else
            {
                tdCNPJ.InnerText = "C.N.P.J";
                tdInscricaoEstadual.InnerText = "Inscrição Estadual";
                tdRazaoSocial.InnerText = "Razão Social";
                tdNomeFantasia.InnerText = "Nome Fantasia";
                divCNPJCadastro.Style.Add("display", "block");
            }

            //Alimentar campos para edição
            carregaDados(identCliente);
            CarregarListaTransportadora();
            mpeIncluir.Show();
        }
    }

    protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ClienteVO identCliente = (ClienteVO)e.Row.DataItem;

            if (ddlTipoPessoa.SelectedValue == "1")
            {
                grdListaResultado.HeaderRow.Cells[4].Visible = true;
                e.Row.Cells[4].Visible = true;
                grdListaResultado.HeaderRow.Cells[1].Text = "Razão Social";
                grdListaResultado.HeaderRow.Cells[2].Text = "Nome Fantasia";
                grdListaResultado.HeaderRow.Cells[3].Text = "CNPJ";
                e.Row.Cells[3].Text = Mascara("CNPJ", identCliente.CNPJ);
            }
            else 
            {
                grdListaResultado.HeaderRow.Cells[1].Text = "Nome";
                grdListaResultado.HeaderRow.Cells[3].Text = "CPF";
                grdListaResultado.HeaderRow.Cells[4].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[3].Text = Mascara("CPF", identCliente.CNPJ);
            }
                e.Row.Cells[1].Text = identCliente.RazaoSocial.ToString();
                e.Row.Cells[2].Text = identCliente.NomeFantasia;
                e.Row.Cells[4].Text = identCliente.InscricaoEstadual;

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            imgEditar.CommandArgument = identCliente.CodPessoa.ToString();
            imgEditar.CommandName = "Editar";
            imgEditar.Style.Add("cursor", "hand");
            imgEditar.ToolTip = "Editar dados do Cliente [" + identCliente.RazaoSocial.Trim() + "]";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            imgExcluir.CommandArgument = identCliente.CodPessoa.ToString();
            imgExcluir.CommandName = "Excluir";
            imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Cliente [" + identCliente.RazaoSocial.Trim() + "]?');";
            imgExcluir.Style.Add("cursor", "hand");
            imgExcluir.ToolTip = "Excluir Cliente [" + identCliente.RazaoSocial.Trim() + "]";
            #endregion

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
    }
    #endregion


}
