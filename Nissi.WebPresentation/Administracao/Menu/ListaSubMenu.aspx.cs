using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;


public partial class ListaSubMenu : BasePage
{
    private List<RolesVO> listaAssociados;
    private List<RolesVO> listaAssociar;
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SubMenuVO identSubMenu = new SubMenuVO();
            hdfCodMenu.Value = Request.QueryString["CodMenu"].ToString();
            identSubMenu.CodMenu = short.Parse(hdfCodMenu.Value);
            identSubMenu.CodSubMenu = null;
            preencherGrid(identSubMenu);
            if (Session["listaAssociar"] != null)
                DestroiValorSessao("listaAssociar");
            if (Session["listaAssociados"] != null)
                DestroiValorSessao("listaAssociados");
        }
    }
    #endregion
    #region preencherGrid
    private void preencherGrid(SubMenuVO identSubMenu)
    {
        grdListaResultado.DataSource = new SubMenu().Listar(identSubMenu);
        grdListaResultado.DataBind();

    }
    #endregion
    #region DadosSubMenu
    private SubMenuVO DadosSubMenu
    {
        get
        {
            SubMenuVO identSubMenu = new SubMenuVO();
            if (hdfTipoAcao.Value.Equals("Editar"))
                identSubMenu.CodSubMenu = short.Parse(hdfCodSubMenu.Value);
            identSubMenu.CodMenu = short.Parse(hdfCodMenu.Value);
            identSubMenu.Text = txtIncluirMenu.Text;
            identSubMenu.Url = txtUrl.Text;
            identSubMenu.Ordem = short.Parse(txtOrdem.Text);
            identSubMenu.Resolveurl = ckbResolveurl.Checked;
            identSubMenu.Ativo = ckbIncluir.Checked;
            return identSubMenu;
        }
    }
    #endregion

    #region Métodos do Grid
    protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Page"))
        {
            SubMenuVO identSubMenu = new SubMenuVO();
            string[] codigos = e.CommandArgument.ToString().Split('|');
            identSubMenu.CodSubMenu = short.Parse(codigos[0]);
            identSubMenu.CodMenu = short.Parse(hdfCodMenu.Value);
            //Módulo de exclusão
            if (e.CommandName == "Excluir")
            {
                //Excluir
                new SubMenu().Excluir(identSubMenu.CodSubMenu, UsuarioAtivo.CodFuncionario.Value);

                //Atualizar Lista
                preencherGrid(identSubMenu);
            }
            else if (e.CommandName == "Editar") //Módulo de alteração
            {
                hdfTipoAcao.Value = "Editar";
                identSubMenu = new SubMenu().Listar(identSubMenu)[0];
                mpeInclui.Show();

                //Alimentar campos para edição
                txtIncluirMenu.Text = identSubMenu.Text;
                txtUrl.Text = identSubMenu.Url;
                txtOrdem.Text = identSubMenu.Ordem.ToString();
                ckbResolveurl.Checked = identSubMenu.Resolveurl.Value;
                ckbIncluir.Checked = identSubMenu.Ativo.Value;
                hdfCodSubMenu.Value = identSubMenu.CodSubMenu.ToString();
                this.Master.PosicionarFoco(txtIncluirMenu);
            }
            else if (e.CommandName == "Roles") //Módulo de alteração
            {
                hdfCodSubMenuRoles.Value = identSubMenu.CodSubMenu.ToString();
                lblMenu.Text = codigos[1];
                carregaListaNaoAssociados();
                carregaListaAssociados();
                mpeRoles.Show();
            }
        }

    }

    protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SubMenuVO identSubMenu = (SubMenuVO)e.Row.DataItem;

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            imgEditar.CommandArgument = identSubMenu.CodSubMenu.ToString();
            imgEditar.CommandName = "Editar";
            imgEditar.Style.Add("cursor", "hand");
            imgEditar.ToolTip = "Editar dados do SubMenu [" + identSubMenu.Text.Trim() + "]";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            imgExcluir.CommandArgument = identSubMenu.CodSubMenu.ToString();
            imgExcluir.CommandName = "Excluir";
            imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do SubMenu [" + identSubMenu.Text.Trim() + "]?');";
            imgExcluir.Style.Add("cursor", "hand");
            imgExcluir.ToolTip = "Excluir SubMenu [" + identSubMenu.Text.Trim() + "]";
            #endregion

            #region Botão Roles
            ImageButton imgSubMenu = (ImageButton)e.Row.FindControl("imgRoles");
            imgSubMenu.ImageUrl = caminhoAplicacao + @"Imagens\DatabasePermissionsMenu.png";
            imgSubMenu.CommandArgument = identSubMenu.CodSubMenu + "|" + identSubMenu.Text.Trim();
            imgSubMenu.CommandName = "Roles";
            imgSubMenu.Style.Add("cursor", "hand");
            imgSubMenu.ToolTip = "Cadastrar Roles para o SubMenu [" + identSubMenu.Text.Trim() + "]";
            #endregion

            e.Row.Cells[1].Text = identSubMenu.Text;
            e.Row.Cells[2].Text = identSubMenu.Url;
            e.Row.Cells[3].Text = identSubMenu.Ordem.ToString();
            e.Row.Cells[4].Text = identSubMenu.Resolveurl == true ? "Sim" : "Não";
            e.Row.Cells[5].Text = identSubMenu.Ativo == true ? "Sim" : "Não";

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
    }
    #endregion
    #region btnIncluir_Click
    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        txtOrdem.Text = new SubMenu().ListarOrdem(new SubMenuVO(){CodMenu = short.Parse(hdfCodMenu.Value)}).ToString();
        hdfTipoAcao.Value = "Incluir";
        txtIncluirMenu.Text = string.Empty;
        txtUrl.Text = string.Empty;
        ckbResolveurl.Checked = true;
        ckbIncluir.Checked = true;
        mpeInclui.Show();
        Master.PosicionarFoco(txtIncluirMenu);
    }
   
    #endregion
    #region btnVoltar_Click
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListaMenu.aspx");
    }
    #endregion
    #region btnCancelar_Click
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        hdfCodSubMenu.Value = string.Empty;
        mpeInclui.Hide();
    }
    #endregion
    #region btnSalvar_Click
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (hdfTipoAcao.Value.Equals("Incluir"))
        {
            //new NissiMenu().Alterar(DadosMenu, UsuarioAtivo.CodFuncionario);
            new SubMenu().Incluir(DadosSubMenu, UsuarioAtivo.CodFuncionario.Value);
        }
        else
        {
            new SubMenu().Alterar(DadosSubMenu, UsuarioAtivo.CodFuncionario.Value);
        }
        SubMenuVO identSubMenu = new SubMenuVO();
        identSubMenu.CodMenu = short.Parse(hdfCodMenu.Value);
        identSubMenu.CodSubMenu = null;
        preencherGrid(identSubMenu);
        mpeInclui.Hide();
    }    
    #endregion
    #region Métodos de cadastro de Roles
    private void carregaListaNaoAssociados()
    {
        lbxAssociar.DataSource = new Roles().ListarPerfilNaoAssociado(short.Parse(hdfCodSubMenuRoles.Value));
        lbxAssociar.DataTextField = "NomPerfilAcesso";
        lbxAssociar.DataValueField = "CodPerfilAcesso";
        lbxAssociar.DataBind();
    }

    private void carregaListaAssociados()
    {
        List<PerfilAcessoVO> listaPerfilAcesso = new Roles().ListarPerfilAssociado(short.Parse(hdfCodSubMenuRoles.Value));
        lbxAssociados.DataSource = listaPerfilAcesso;
        lbxAssociados.DataTextField = "NomPerfilAcesso";
        lbxAssociados.DataValueField = "CodPerfilAcesso";
        lbxAssociados.DataBind();
    }
    #region btnAssociar_Click
    protected void btnAssociar_Click(object sender, EventArgs e)
    {
        ListItem item = lbxAssociar.SelectedItem;
        if (item != null)
        {
            adicionaLista(item);
        }
    }
    #endregion
    #region btnRetirar_Click
    protected void btnRetirar_Click(object sender, EventArgs e)
    {
        ListItem item = lbxAssociados.SelectedItem;
        if (item != null)
        {
            removeLista(item);
        }
    }
    #endregion
    #region btnAssociarTodos_Click
    protected void btnAssociarTodos_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in lbxAssociar.Items)
        {
            adicionaLista(item);
        }
        lbxAssociar.Items.Clear();
        listaAssociar.Clear();
    }
    #endregion
    #region btnRetirarTodos_Click
    protected void btnRetirarTodos_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in lbxAssociados.Items)
        {
            removeLista(item);
        }
        lbxAssociados.Items.Clear();
        listaAssociados.Clear();
    }
    #endregion
    #region btnCancelarRoles_Click
    protected void btnCancelarRoles_Click(object sender, EventArgs e)
    {
        hdfCodSubMenuRoles.Value = string.Empty;
        mpeRoles.Hide();
    }
    #endregion
    #region adicionaLista
    private void adicionaLista(ListItem item)
    {
        RolesVO identRoles = new RolesVO();
        if (RecuperaValorSessao("listaAssociados") == null)
            listaAssociados = new List<RolesVO>();
        else
            listaAssociados = (List<RolesVO>)RecuperaValorSessao("listaAssociados");
        if (RecuperaValorSessao("listaAssociar") == null)
            listaAssociar = new List<RolesVO>();
        else
            listaAssociar = (List<RolesVO>)RecuperaValorSessao("listaAssociar");
        identRoles.PerfilAcesso.CodPerfilAcesso = short.Parse(item.Value);
        identRoles.CodSubMenu = short.Parse(hdfCodSubMenuRoles.Value);
        listaAssociados.Add(identRoles);
        listaAssociar = Remove(listaAssociar, identRoles);
        lbxAssociados.Items.Add(item);
        lbxAssociar.Items.Remove(item);
        ArmazenaValorSessao("listaAssociados", listaAssociados);
        ArmazenaValorSessao("listaAssociar", listaAssociar);
    }
    
    #endregion
    #region removeLista
    private void removeLista(ListItem item)
    {
        RolesVO identRoles = new RolesVO();
        if (RecuperaValorSessao("listaAssociados") == null)
            listaAssociados = new List<RolesVO>();
        else
            listaAssociados = (List<RolesVO>)RecuperaValorSessao("listaAssociados");
        if (RecuperaValorSessao("listaAssociar") == null)
            listaAssociar = new List<RolesVO>();
        else
            listaAssociar = (List<RolesVO>)RecuperaValorSessao("listaAssociar");
        identRoles.PerfilAcesso.CodPerfilAcesso = short.Parse(item.Value);
        identRoles.CodSubMenu = short.Parse(hdfCodSubMenuRoles.Value);
        identRoles.UsuarioAlt = UsuarioAtivo.CodFuncionario.Value;
        listaAssociar.Add(identRoles);
        listaAssociados = Remove(listaAssociados, identRoles);
        lbxAssociar.Items.Add(item);
        lbxAssociados.Items.Remove(item);
        ArmazenaValorSessao("listaAssociados", listaAssociados);
        ArmazenaValorSessao("listaAssociar", listaAssociar);
    }
    #endregion
    private List<RolesVO> Remove(List<RolesVO> listaRolesVO, RolesVO identRoles)
    {
        List<RolesVO> novalista = new List<RolesVO>();
        foreach( RolesVO identRolesTemp in listaRolesVO)
        {
            if (identRoles.PerfilAcesso.CodPerfilAcesso != identRolesTemp.PerfilAcesso.CodPerfilAcesso && identRolesTemp.CodRoles == null )
                novalista.Add(identRolesTemp);
        }
        return novalista;
    }
    #region btnSalvarRoles_Click
    protected void btnSalvarRoles_Click(object sender, EventArgs e)
    {
         listaAssociados = (List<RolesVO>)RecuperaValorSessao("listaAssociados");
         listaAssociar = (List<RolesVO>)RecuperaValorSessao("listaAssociar");
        if (listaAssociar.Count > 0)
            new Roles().Excluir(listaAssociar);
        if (listaAssociados.Count > 0)
            new Roles().Incluir(listaAssociados, UsuarioAtivo.CodFuncionario.Value);
        hdfCodSubMenuRoles.Value = string.Empty;
        mpeRoles.Hide();
    }
    #endregion

    protected void lbxAssociar_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    #endregion

}

