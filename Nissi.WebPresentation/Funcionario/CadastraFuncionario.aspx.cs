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

public partial class CadastraFuncionario : BasePage
{
    #region Dados do Funcionario
    private FuncionarioVO DadosFuncionario
    {
        get
        {
            FuncionarioVO identFuncionario = new FuncionarioVO();
            if (hdfTipoAcao.Value.Equals("Editar"))
            {
                identFuncionario.CodPessoa = Convert.ToInt32(hdfCodPessoa.Value);
                identFuncionario.CodFuncionario = Convert.ToInt32(hdfCodFuncionario.Value);
            }
            identFuncionario.CPF = txtCPF.Text.Replace(".", "").Replace("-", "");
            identFuncionario.Cep.CodCep = Endereco.CodCep.Replace("-", "");
            identFuncionario.Celular = txtCelular.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
            identFuncionario.Telefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ","").Trim();
            identFuncionario.Fax = txtFax.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ","").Trim();
            identFuncionario.Complemento = Endereco.Complemento;
            identFuncionario.Contato = txtContato.Text;
            identFuncionario.Email = txtEmail.Text;
            identFuncionario.RG = txtRG.Text;
            identFuncionario.NomeFantasia = txtNomeFantasia.Text;
            identFuncionario.Numero = Endereco.Numero;
            identFuncionario.Observacao = txtObservacao.Text;
            identFuncionario.RazaoSocial = txtRazaoSocial.Text;
            identFuncionario.Site = txtSite.Text;
            identFuncionario.Tipo = "E";
            identFuncionario.DataAdmissao = null;
            if (txtDataAdmissao.Text != "")
            {
                CultureInfo culture = new CultureInfo("pt-BR");
                identFuncionario.DataAdmissao = DateTime.ParseExact(txtDataAdmissao.Text, "dd/MM/yyyy", culture);
            }
            if (txtDataDemissao.Text != "")
            {
                CultureInfo culture = new CultureInfo("pt-BR");
                identFuncionario.DataDemissao = DateTime.ParseExact(txtDataDemissao.Text, "dd/MM/yyyy", culture);
            }
                identFuncionario.Departamento.CodDepartamento = short.Parse(ddlDepartamento.SelectedValue);
                identFuncionario.Cargo.CodCargo = short.Parse(ddlCargo.SelectedValue);
                identFuncionario.Login = txtLogin.Text;
                identFuncionario.Ativo = chkAtivo.Checked ? true : false;
                identFuncionario.AcessaSistema = chkAcessa.Checked ? true : false;
                identFuncionario.Banco = ucBanco.DadosBanco;
            return identFuncionario;
        }
    }
    #endregion

    #region LimparCampos
    private void limparCampos()
    {   
        Endereco.limpaCampos();
        ucBanco.LimparCampos();
        divLogin.Style.Add("display", "none");
        chkAcessa.Checked = false;
        hdfCodFuncionario.Value =
        txtCelular.Text      = 
        txtTelefone.Text     = 
        txtCPF.Text          = 
        txtContato.Text      = 
        txtEmail.Text        = 
        txtFax.Text          =
        txtRG.Text           = 
        txtNomeFantasia.Text = 
        txtObservacao.Text   = 
        txtRazaoSocial.Text  = 
        txtSite.Text         = 
        txtTelefone.Text     = 
        txtDataAdmissao.Text =
        txtLogin.Text        =
        txtDataDemissao.Text = string.Empty;


    }
    #endregion

    #region carregaDados
    private void carregaDados(FuncionarioVO identFuncionario)
    {
        hdfCodFuncionario.Value = identFuncionario.CodFuncionario.ToString();
        hdfCodPessoa.Value = identFuncionario.CodPessoa.ToString();
        txtCelular.Text = Mascara("TEL", identFuncionario.Celular);
        CEPVO identCep = new CEPVO();
        identCep.CodCep = identFuncionario.Cep.CodCep;
        identCep = new CEP().Listar(identCep);
        Endereco.preencheCep(identCep, new ArrayList());
        txtTelefone.Text = Mascara("TEL", identFuncionario.Telefone);
        txtCPF.Text = Mascara("CPF", identFuncionario.CPF);
        Endereco.Complemento = identFuncionario.Complemento;
        txtContato.Text = identFuncionario.Contato;
        txtEmail.Text = identFuncionario.Email;
        txtFax.Text = Mascara("TEL", identFuncionario.Fax);
        txtRG.Text = identFuncionario.RG;
        txtNomeFantasia.Text = identFuncionario.Apelido;
        Endereco.Numero = identFuncionario.Numero;
        txtObservacao.Text = identFuncionario.Observacao;
        txtRazaoSocial.Text = identFuncionario.Nome;
        txtSite.Text = identFuncionario.Site;
        DateTime? dttDataAdmissao;
        if (identFuncionario.DataAdmissao != null)
        {
            dttDataAdmissao = identFuncionario.DataAdmissao;
            txtDataAdmissao.Text = dttDataAdmissao.Value.Day.ToString() + "/" + dttDataAdmissao.Value.Month.ToString() + "/" + dttDataAdmissao.Value.Year.ToString();
        }
        DateTime? dttDataDemissao;
        if (identFuncionario.DataDemissao != null)
        {
            dttDataDemissao = identFuncionario.DataDemissao;
            txtDataDemissao.Text = dttDataDemissao.Value.Day.ToString() + "/" + dttDataDemissao.Value.Month.ToString() + "/" + dttDataDemissao.Value.Year.ToString();
        }
        ddlDepartamento.SelectedValue = identFuncionario.Departamento.CodDepartamento.ToString();
        ddlCargo.SelectedValue = identFuncionario.Cargo.CodCargo.ToString();
        if (identFuncionario.Ativo != null)
            chkAtivo.Checked =(bool) identFuncionario.Ativo;
       // ucBanco.DadosBanco = identFuncionario.ba
        if (!string.IsNullOrEmpty(identFuncionario.Login))
        {
            txtLogin.Text = identFuncionario.Login;
            chkAcessa.Checked = true;
            divLogin.Style.Add("display", "block");
        }
        ucBanco.DadosBanco = identFuncionario.Banco;

    }
    #endregion    

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            preencherDepartamento(null);
            preencherCargo(null);
            Master.PosicionarFoco(txtCPF);
        }
        if (chkAcessa.Checked)
            divLogin.Style.Add("display", "inline");
        else
            divLogin.Style.Add("display", "none");
    }
    #endregion

    #region btnVoltar_Click
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
    #endregion

    #region btnIncluir_Click
    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        limparCampos();
        hdfTipoAcao.Value = "Incluir";
        Master.PosicionarFoco(txtRazaoSocial);
        mpeIncluir.Show();
    }   
    #endregion

    #region Pesquisar
    private void Pesquisar()
    {
        FuncionarioVO identFuncionario = new FuncionarioVO();
        identFuncionario.RG = null;
        identFuncionario.Nome = null;
        identFuncionario.CPF = null;
        if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
        {
            identFuncionario.CodFuncionario = Convert.ToInt32(hdfIdRazaoSocial.Value);
        }
        else
        {
            if (!string.IsNullOrEmpty(hdfCodFuncionario.Value))
                identFuncionario.CodFuncionario = Convert.ToInt32(hdfCodFuncionario.Value);
            if (rbCpf.Checked)
                identFuncionario.CPF = txtCPFPesq.Text.Replace(".", "").Replace("-", "");
            else
                if (rbRg.Checked)
                    identFuncionario.RG = txtRGPesq.Text;
                else
                    if (rbNome.Checked)
                        identFuncionario.Nome = txtNome.Text;
        }
        
        List<FuncionarioVO> lFuncionario = new Funcionario().Listar(identFuncionario);
        if (lFuncionario.Count > 0)
        {
            grdListaResultado.DataSource = lFuncionario;
            grdListaResultado.DataBind();
        }
        else
            MensagemCliente("Não existe resultados para esta pesquisa.");
        hdfIdRazaoSocial.Value = string.Empty;
    }

    #endregion

    #region btnPesquisar_Click
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
    }
    #endregion

    #region btnCancelar_Click
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        hdfTipoAcao.Value = string.Empty;
        mpeIncluir.Hide();
    }    
    #endregion

    #region btnSalvar_Click
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (hdfTipoAcao.Value.Equals("Incluir"))
            hdfCodFuncionario.Value = new Funcionario().Incluir(DadosFuncionario, UsuarioAtivo.CodFuncionario.Value).ToString();
        else
            new Funcionario().Alterar(DadosFuncionario, UsuarioAtivo.CodFuncionario.Value);
        Pesquisar();
        limparCampos();
        hdfTipoAcao.Value = string.Empty;
        mpeIncluir.Hide();
    }    
    #endregion

    #region Métodos do Departamento
    #region DadosDepartamento
    private DepartamentoVO DadosDepartamento
    {
        get {
            DepartamentoVO identDepartamento = new DepartamentoVO();
            if (hdfTipoAcaoDepartamento.Value.Equals("Editar"))
                identDepartamento.CodDepartamento = short.Parse(hdfCodDepartamento.Value);
            identDepartamento.Nome = txtDepartamento.Text;
            return identDepartamento;
        }
    }
    #endregion
    #region preencherDepartamento
    private void preencherDepartamento(int? codDepartamento)
    {
        ddlDepartamento.DataSource = new Departamento().Listar(null);
        ddlDepartamento.DataTextField = "Nome";
        ddlDepartamento.DataValueField = "CodDepartamento";
        ddlDepartamento.DataBind();
        if (codDepartamento != null)
            ddlDepartamento.SelectedValue = codDepartamento.ToString(); ;
    }    
    #endregion
    #region imgDepartamento_Click
    protected void imgDepartamento_Click(object sender, ImageClickEventArgs e)
    {
        hdfTipoAcaoDepartamento.Value = "Incluir";
        mpeIncluiDepartamento.Show();
        Master.PosicionarFoco(txtDepartamento);
    }

    #endregion
    #region btnSalvarDepartamento_Click
    protected void btnSalvarDepartamento_Click(object sender, EventArgs e)
    {
        if (hdfTipoAcaoDepartamento.Value.Equals("Incluir"))
        {
            int codDepartamento = new Departamento().Incluir(DadosDepartamento, UsuarioAtivo.CodFuncionario.Value);
            preencherDepartamento(codDepartamento);
        }
        mpeIncluiDepartamento.Hide();
    }
    #endregion
    #region btnCancelarDepartamento_Click
    protected void btnCancelarDepartamento_Click(object sender, EventArgs e)
    {
        hdfTipoAcaoDepartamento.Value = string.Empty;
        mpeIncluiDepartamento.Hide();
    }
    
    #endregion
    #endregion

    #region Métodos do Cargo
    #region DadosCargo
    private CargoVO DadosCargo
    {
        get
        {
            CargoVO identCargo = new CargoVO();
            if (hdfTipoAcaoDepartamento.Value.Equals("Editar"))
                identCargo.CodCargo = short.Parse(hdfCodCargo.Value);
            identCargo.Nome = txtCargo.Text;
            return identCargo;
        }
    }
    #endregion
    #region preencherCargo
    private void preencherCargo(short? codCargo)
    {
        ddlCargo.DataSource = new Cargo().Listar(codCargo);
        ddlCargo.DataTextField = "Nome";
        ddlCargo.DataValueField = "CodCargo";
        ddlCargo.DataBind();
        if (codCargo != null)
            ddlCargo.SelectedValue = codCargo.ToString();
    }
    #endregion
    #region imgCargo_Click
    protected void imgCargo_Click(object sender, ImageClickEventArgs e)
    {
        hdfTipoAcaoCargo.Value = "Incluir";
        mpeIncluirCargo.Show();
        Master.PosicionarFoco(txtCargo);

    }    
    #endregion    
    #region btnSalvarCargo_Click
    protected void btnSalvarCargo_Click(object sender, EventArgs e)
    {
        if (hdfTipoAcaoCargo.Value.Equals("Incluir"))
        {
            short codCargo = new Cargo().Incluir(DadosCargo, UsuarioAtivo.CodFuncionario.Value);
            preencherCargo(codCargo);
        }
        mpeIncluirCargo.Hide();
    }    
    #endregion
    #region btnCancelarCargo_Click
    protected void btnCancelarCargo_Click(object sender, EventArgs e)
    {
        hdfTipoAcaoCargo.Value = string.Empty;
        mpeIncluirCargo.Hide();
    }    
    #endregion
    #endregion
    private List<PerfilAcessoVO> _listaAssociados;
    private List<PerfilAcessoVO> _listaAssociar;
    #region Métodos do Grid
    protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Page"))
        {
            FuncionarioVO identFuncionario = new FuncionarioVO();
            string[] codigos = e.CommandArgument.ToString().Split('|');
            identFuncionario.CodFuncionario = int.Parse(codigos[0]);

            //Módulo de exclusão
            if (e.CommandName == "Excluir")
            {
                //Excluir
                new Funcionario().Excluir(identFuncionario.CodFuncionario, "C");

                //Atualizar Lista
                Pesquisar();
            }
            else if (e.CommandName == "Editar") //Módulo de alteração
            {
                hdfTipoAcao.Value = "Editar";
                identFuncionario = new Funcionario().Listar(identFuncionario)[0];
                mpeIncluir.Show();

                //Alimentar campos para edição
                carregaDados(identFuncionario);
            }
            else if (e.CommandName == "Perfil")
            {
                hdfCodUsuario.Value = identFuncionario.CodFuncionario.ToString();
                lblUsuarioPerfil.Text = codigos[1];
                carregaListaNaoAssociados();
                carregaListaAssociados();
                if (lbxAssociar.Items.Count > 0)
                    lbxAssociar.SelectedIndex = 0;
                if (lbxAssociados.Items.Count > 0)
                    lbxAssociados.SelectedIndex = 0;
                ArmazenaValorSessao("listaAssociados", _listaAssociados);
                ArmazenaValorSessao("listaAssociar", _listaAssociar);
                mpePerfil.Show();
            }
            else if (e.CommandName == "Reiniciar")
            {
                FuncionarioVO identFunc = new FuncionarioVO();
                identFuncionario.CodFuncionario = Convert.ToInt32(e.CommandArgument);
                new Funcionario().ReiniciarSenha(identFuncionario);
                MensagemCliente("Senha reiniciada com sucesso.");
            }
        }
    }
    protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FuncionarioVO identFuncionario = (FuncionarioVO)e.Row.DataItem;

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            imgEditar.CommandArgument = identFuncionario.CodFuncionario + "|" + identFuncionario.Apelido;
            imgEditar.CommandName = "Editar";
            imgEditar.Style.Add("cursor", "hand");
            imgEditar.ToolTip = "Editar dados do Funcionário [" + identFuncionario.Nome.Trim() + "]";
            #endregion
            #region Botão Perfil
            ImageButton imgPerfil = (ImageButton)e.Row.FindControl("imgPerfil");
            imgPerfil.ImageUrl = caminhoAplicacao + @"Imagens\DatabasePermissionsMenu.png";
            imgPerfil.CommandArgument = identFuncionario.CodFuncionario+"|"+identFuncionario.Apelido;
            imgPerfil.CommandName = "Perfil";
            imgPerfil.Style.Add("cursor", "hand");
            imgPerfil.ToolTip = "Perfil do Funcionário [" + identFuncionario.Nome.Trim() + "]";
            #endregion
            #region Botão Reiniciar Senha
            ImageButton imgReiniciar = (ImageButton)e.Row.FindControl("imgReiniciar");
            imgReiniciar.CommandArgument = identFuncionario.CodFuncionario.ToString();
            imgReiniciar.CommandName = "Reiniciar";
            imgReiniciar.Attributes["onclick"] = "return confirm('Confirma a reicinialização da senha do Funcionário [" + identFuncionario.Nome.Trim() + "]?');";
            imgReiniciar.Style.Add("cursor", "hand");
            imgReiniciar.ToolTip = "Reiniciar Senha do Funcionário [" + identFuncionario.Nome.Trim() + "]";
            #endregion

            e.Row.Cells[1].Text = identFuncionario.Nome.ToString();
            e.Row.Cells[2].Text = identFuncionario.RG;
            e.Row.Cells[3].Text = Mascara("CPF", identFuncionario.CPF);
            e.Row.Cells[4].Text = identFuncionario.Departamento.Nome;
            e.Row.Cells[5].Text = identFuncionario.Cargo.Nome;

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
    }



    private void carregaListaNaoAssociados()
    {
        lbxAssociar.DataSource = Usuario.ListarPerfilNaoAssociado(int.Parse(hdfCodUsuario.Value));
        lbxAssociar.DataTextField = "NomPerfilAcesso";
        lbxAssociar.DataValueField = "CodPerfilAcesso";
        lbxAssociar.DataBind();
    }
    private void carregaListaAssociados()
    {
        lbxAssociados.DataSource = Usuario.ListarPerfilAssociado(int.Parse(hdfCodUsuario.Value));
        lbxAssociados.DataTextField = "NomPerfilAcesso";
        lbxAssociados.DataValueField = "CodPerfilAcesso";
        lbxAssociados.DataBind();
    }


    protected void btnAssociar_Click(object sender, EventArgs e)
    {
        ListItem item = lbxAssociar.SelectedItem;
        if (item != null)
        {
            adicionaLista(item);
        }
        Master.PosicionarFoco(lbxAssociar);
    }

    protected void btnRetirar_Click(object sender, EventArgs e)
    {
        ListItem item = lbxAssociados.SelectedItem;
        if (item != null)
        {
            removeLista(item);
        }
        if (lbxAssociados.Items.Count > 0)
        {
            Master.PosicionarFoco(lbxAssociados);
            lbxAssociados.SelectedIndex = 0;
        }
        else
        {            
            Master.PosicionarFoco(lbxAssociar);
        }
    }

    protected void btnAssociarTodos_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in lbxAssociar.Items)
        {
            adicionaLista(item);
        }
        lbxAssociar.Items.Clear();
        _listaAssociar.Clear();
        Master.PosicionarFoco(lbxAssociados);
    }

    protected void btnRetirarTodos_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in lbxAssociados.Items)
        {
            removeLista(item);
        }
        lbxAssociados.Items.Clear();
        _listaAssociados.Clear();
        Master.PosicionarFoco(lbxAssociar);
    }

    #region adicionaLista
    private void adicionaLista(ListItem item)
    {
        PerfilAcessoVO identUsuario = new PerfilAcessoVO();
        if (RecuperaValorSessao("listaAssociados") == null)
            _listaAssociados = new List<PerfilAcessoVO>();
        else
            _listaAssociados = (List<PerfilAcessoVO>)RecuperaValorSessao("listaAssociados");
        if (RecuperaValorSessao("listaAssociar") == null)
            _listaAssociar = new List<PerfilAcessoVO>();
        else
            _listaAssociar = (List<PerfilAcessoVO>)RecuperaValorSessao("listaAssociar");
        identUsuario.CodPerfilAcesso = short.Parse(item.Value);
        _listaAssociados.Add(identUsuario);
        _listaAssociar = Remove(_listaAssociar, identUsuario);
        lbxAssociados.Items.Add(item);
        lbxAssociar.Items.Remove(item);
        ArmazenaValorSessao("listaAssociados", _listaAssociados);
        ArmazenaValorSessao("listaAssociar", _listaAssociar);
    }
    #endregion

    #region removeLista
    private void removeLista(ListItem item)
    {
        PerfilAcessoVO identAcessoVO = new PerfilAcessoVO();
        if (RecuperaValorSessao("listaAssociados") == null)
            _listaAssociados = new List<PerfilAcessoVO>();
        else
            _listaAssociados = (List<PerfilAcessoVO>)RecuperaValorSessao("listaAssociados");
        if (RecuperaValorSessao("listaAssociar") == null)
            _listaAssociar = new List<PerfilAcessoVO>();
        else
            _listaAssociar = (List<PerfilAcessoVO>)RecuperaValorSessao("listaAssociar");
        identAcessoVO.CodPerfilAcesso = short.Parse(item.Value);
        identAcessoVO.UsuarioAlt = UsuarioAtivo.CodFuncionario.Value;
        _listaAssociar.Add(identAcessoVO);
        _listaAssociados = Remove(_listaAssociados, identAcessoVO);
        lbxAssociar.Items.Add(item);
        lbxAssociados.Items.Remove(item);
        ArmazenaValorSessao("listaAssociados", _listaAssociados);
        ArmazenaValorSessao("listaAssociar", _listaAssociar);
    }
    #endregion

    private List<PerfilAcessoVO> Remove(List<PerfilAcessoVO> listaPerfilAcessoVO, PerfilAcessoVO identPefilAcessoVO)
    {
        List<PerfilAcessoVO> novalista = new List<PerfilAcessoVO>();
        foreach (PerfilAcessoVO identPerfilAcessoTemp in listaPerfilAcessoVO)
        {
            if (identPefilAcessoVO.CodPerfilAcesso != identPerfilAcessoTemp.CodPerfilAcesso)
                novalista.Add(identPerfilAcessoTemp);
        }
        return novalista;
    }
    #endregion

    protected void btnSalvarPerfil_Click(object sender, EventArgs e)
    {
        _listaAssociados = (List<PerfilAcessoVO>)RecuperaValorSessao("listaAssociados");
        _listaAssociar = (List<PerfilAcessoVO>)RecuperaValorSessao("listaAssociar");
        if (_listaAssociar != null && _listaAssociar.Count > 0)
            Usuario.Excluir(int.Parse(hdfCodUsuario.Value), _listaAssociar);
        if (_listaAssociados != null && _listaAssociados.Count > 0)
            Usuario.Incluir(int.Parse(hdfCodUsuario.Value), _listaAssociados, UsuarioAtivo.CodFuncionario.Value);
        hdfCodUsuario.Value = string.Empty;
        DestroiValorSessao("listaAssociados");
        DestroiValorSessao("listaAssociar");
        mpePerfil.Hide();
    }

    protected void btnCancelarPerfil_Click(object sender, EventArgs e)
    {
        hdfCodUsuario.Value = string.Empty;
        DestroiValorSessao("listaAssociados");
        DestroiValorSessao("listaAssociar");
        mpePerfil.Hide();
    }

}

