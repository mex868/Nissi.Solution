using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;
using System.Configuration;
using System.Drawing;

public partial class ListaMenu : BasePage
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (rbTodos.Checked)
            {
                txbPesquisa.BackColor = Color.WhiteSmoke;
                txbPesquisa.Enabled = false;
            }
            else
            {
                txbPesquisa.BackColor = Color.White;
                txbPesquisa.Enabled = true;
                Master.PosicionarFoco(txbPesquisa);
            }
        }
    }
    
    #endregion
    #region DadosMenu
    private MenuVO DadosMenu
    {
        get
        {
            MenuVO identMenu = new MenuVO();
            if (hdfTipoAcao.Value.Equals("Editar"))
                identMenu.CodMenu = short.Parse(hdfCodMenu.Value);
            identMenu.Text = txtIncluirMenu.Text;
            identMenu.Ativo = ckbIncluirMenu.Checked;
            return identMenu;
        }
    }
   
    #endregion
    #region btnIncluir_Click
    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        hdfTipoAcao.Value = "Incluir";
        hdfCodMenu.Value = string.Empty;
        txtIncluirMenu.Text = string.Empty;
        ckbIncluirMenu.Checked = true;
        mpeIncluiMenu.Show();
        Master.PosicionarFoco(txtIncluirMenu);
    }
   
    #endregion
    #region btnCancelar_Click
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        hdfTipoAcao.Value = string.Empty;
        mpeIncluiMenu.Hide();
    }
    
    #endregion
    #region Pesquisar
    private void Pesquisar()
    {
        MenuVO identMenu = new MenuVO();
        identMenu.Text = rbTodos.Checked ? txbPesquisa.Text : null;
        grdListaResultado.DataSource = new MenuAcesso().Listar(identMenu);
        grdListaResultado.DataBind();
    }
    
    #endregion
    #region btnPesquisar_Click
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
    }
    
    #endregion
    #region btnVoltar_Click
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Default.aspx");
    }
   
    #endregion
    #region btnSalvar_Click
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (hdfTipoAcao.Value.Equals("Incluir"))
        {
            //new NissiMenu().Alterar(DadosMenu, UsuarioAtivo.CodFuncionario);
            new MenuAcesso().Incluir(DadosMenu, 1);
        }
        else
        {
            new MenuAcesso().Alterar(DadosMenu, 1);
        }
        Pesquisar();
        mpeIncluiMenu.Hide();
    }
    #endregion
    #region Métodos do Grid
    protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        MenuVO identMenu = new MenuVO();
        identMenu.CodMenu = short.Parse(e.CommandArgument.ToString());

        //Módulo de exclusão
        if (e.CommandName == "Excluir")
        {
            //Excluir
            new MenuAcesso().Excluir(identMenu.CodMenu, 1);

            //Atualizar Lista
            Pesquisar();
        }
        else if (e.CommandName == "Editar")  //Módulo de alteração
        {
            hdfTipoAcao.Value = "Editar";
            identMenu = new MenuAcesso().Listar(identMenu)[0];
            mpeIncluiMenu.Show();

            //Alimentar campos para edição
            txtIncluirMenu.Text = identMenu.Text.ToString();
            ckbIncluirMenu.Checked = identMenu.Ativo.Value;
            hdfCodMenu.Value = identMenu.CodMenu.ToString();
            this.Master.PosicionarFoco(txtIncluirMenu);
        }
        else if (e.CommandName == "SubMenu")  //Módulo de alteração
        {
            Response.Redirect("ListaSubMenu.aspx?CodMenu=" + identMenu.CodMenu.ToString());
        }
    }
    protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MenuVO identMenu = (MenuVO)e.Row.DataItem;

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            imgEditar.CommandArgument = identMenu.CodMenu.ToString();
            imgEditar.CommandName = "Editar";
            imgEditar.Style.Add("cursor", "hand");
            imgEditar.ToolTip = "Editar dados do Menu [" + identMenu.Text.Trim() + "]";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            imgExcluir.CommandArgument = identMenu.CodMenu.ToString();
            imgExcluir.CommandName = "Excluir";
            imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Menu [" + identMenu.Text.Trim() + "]?');";
            imgExcluir.Style.Add("cursor", "hand");
            imgExcluir.ToolTip = "Excluir Menu [" + identMenu.Text.Trim() + "]";
            #endregion

            #region Botão SubMenu
            ImageButton imgSubMenu = (ImageButton)e.Row.FindControl("imgSubMenu");
            imgSubMenu.ImageUrl = caminhoAplicacao + @"Imagens\AdpDiagramRelationships.png";
            imgSubMenu.CommandArgument = identMenu.CodMenu.ToString();
            imgSubMenu.CommandName = "SubMenu";
            imgSubMenu.Style.Add("cursor", "hand");
            imgSubMenu.ToolTip = "Cadastrar Item para o Menu [" + identMenu.Text.Trim() + "]";
            #endregion

            e.Row.Cells[1].Text = identMenu.Text.ToString();
            e.Row.Cells[2].Text = identMenu.Ativo == true ? "Sim" : "Não";

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
    }
    #endregion


}
