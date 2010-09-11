using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Model;
using Nissi.Business;
using System.Configuration;


public partial class ListaPerfilAcesso : BasePage
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (rbTodos.Checked)
            {
                txbPesquisa.BackColor = System.Drawing.Color.WhiteSmoke;
                txbPesquisa.Enabled = false;
            }
            else
            {
                txbPesquisa.BackColor = System.Drawing.Color.White;
                txbPesquisa.Enabled = true;
                Master.PosicionarFoco(txbPesquisa);
            }
        }
    }
   
    #endregion

    #region DadosPerfilAcesso
    private PerfilAcessoVO DadosPerfilAcesso
    {
        get
        {
            PerfilAcessoVO identPerfilAcesso = new PerfilAcessoVO();
            if (hdfTipoAcao.Value.Equals("Editar"))
                identPerfilAcesso.CodPerfilAcesso = short.Parse(hdfCodigo.Value);
            identPerfilAcesso.DescPerfilAcesso = txbDescricao.Text;
            identPerfilAcesso.NomPerfilAcesso = txbNome.Text;
            identPerfilAcesso.Ativo = ckbIncluir.Checked;
            return identPerfilAcesso;
        }
    }        
    #endregion

    #region btnIncluir_Click
    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        hdfTipoAcao.Value = "Incluir";
        txbNome.Text = string.Empty;
        txbDescricao.Text = string.Empty;
        ckbIncluir.Checked = true;
        mpeInclui.Show();
        Master.PosicionarFoco(txbNome);
    }
    #endregion

    #region btnVoltar_Click
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
    #endregion

    #region Pesquisar
    private void Pesquisar()
    {
        PerfilAcessoVO identPerfilAcesso = new PerfilAcessoVO();
        identPerfilAcesso.CodPerfilAcesso = null;
        identPerfilAcesso.NomPerfilAcesso = rbNome.Checked ? txbPesquisa.Text : null;
        identPerfilAcesso.DescPerfilAcesso = rbDescricao.Checked ? txbPesquisa.Text : null;
        grdListaResultado.DataSource = new Nissi.Business.PerfilAcesso().Listar(identPerfilAcesso);
        grdListaResultado.DataBind();
    }
    
    #endregion

    #region btnPesquisar_Click
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
    }
   
    #endregion

    #region Métodos do Grid
    protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        PerfilAcessoVO identPerfilAcesso = new PerfilAcessoVO();
        identPerfilAcesso.CodPerfilAcesso = short.Parse(e.CommandArgument.ToString());

        //Módulo de exclusão
        if (e.CommandName == "Excluir")
        {
            //Excluir
            new PerfilAcesso().Excluir(identPerfilAcesso.CodPerfilAcesso, 1);

            //Atualizar Lista
            Pesquisar();
        }
        else if (e.CommandName == "Editar")  //Módulo de alteração
        {
            hdfTipoAcao.Value = "Editar";
            identPerfilAcesso = new PerfilAcesso().Listar(identPerfilAcesso)[0];
            mpeInclui.Show();

            //Alimentar campos para edição
            txbNome.Text = identPerfilAcesso.NomPerfilAcesso.ToString();
            txbDescricao.Text = identPerfilAcesso.DescPerfilAcesso.ToString();
            ckbIncluir.Checked = identPerfilAcesso.Ativo.Value;
            hdfCodigo.Value = identPerfilAcesso.CodPerfilAcesso.ToString();
            this.Master.PosicionarFoco(txbNome);
        }
    }

    protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PerfilAcessoVO identPerfilAcesso = (PerfilAcessoVO)e.Row.DataItem;

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            imgEditar.CommandArgument = identPerfilAcesso.CodPerfilAcesso.ToString();
            imgEditar.CommandName = "Editar";
            imgEditar.Style.Add("cursor", "hand");
            imgEditar.ToolTip = "Editar dados do Perfil [" + identPerfilAcesso.NomPerfilAcesso.Trim() + "]";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            imgExcluir.CommandArgument = identPerfilAcesso.CodPerfilAcesso.ToString();
            imgExcluir.CommandName = "Excluir";
            imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Perfil [" + identPerfilAcesso.NomPerfilAcesso.Trim() + "]?');";
            imgExcluir.Style.Add("cursor", "hand");
            imgExcluir.ToolTip = "Excluir Perfil [" + identPerfilAcesso.NomPerfilAcesso.Trim() + "]";
            #endregion

            e.Row.Cells[1].Text = identPerfilAcesso.NomPerfilAcesso.ToString();
            e.Row.Cells[2].Text = identPerfilAcesso.DescPerfilAcesso.ToString();
            e.Row.Cells[3].Text = identPerfilAcesso.Ativo == true ? "Sim" : "Não";

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }

    }
   
    #endregion
    #region btnCancelar_Click
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        hdfTipoAcao.Value = string.Empty;
        Pesquisar();
        mpeInclui.Hide();
    }
    
    #endregion
    #region btnSalvar_Click
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (hdfTipoAcao.Value.Equals("Incluir"))
        {
            //new NissiMenu().Alterar(DadosMenu, UsuarioAtivo.CodFuncionario);
            new PerfilAcesso().Incluir(DadosPerfilAcesso, 1);
        }
        else
        {
            new PerfilAcesso().Alterar(DadosPerfilAcesso, 1);
        }
        Pesquisar();
        mpeInclui.Hide();
    }
    #endregion
}
