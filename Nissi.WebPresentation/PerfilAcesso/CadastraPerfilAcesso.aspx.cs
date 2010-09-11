#region Usings
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Text;
#endregion

public partial class Administracao_PerfilAcesso_CadastroPerfilAcesso : BasePage
{
  /*  #region Variáveis Globais
    protected string caminhoAplicacao = ConfigurationManager.AppSettings["CGA.SIDAP.CaminhoAplicacao"].ToString();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Modo Inclusão
            hdnCodPerfilAlteracao.Value = "0";

            CarregarListaFuncionalidade();
            CarregarGridPerfil();

            //Excluir lista antiga de funcionalidades do sistema
            if (Session["listaFuncionalidade"] != null)
                DestroiValorSessao("listaFuncionalidade");

            ExecutarScript(new StringBuilder(" PosicionarFoco(); "));
        }
    }
    #endregion

    #region Carregar Lista de Funcionalidades
    private void CarregarListaFuncionalidade()
    {
        //Caso seja inclusão
        if (hdnCodPerfilAlteracao.Value == "0")
        {
            //Listagem de todas as funcionalidades, use de sessão para evitar muitas chamadas ao banco
            List<FuncionalidadeSistemaVO> listaFuncionalidade;
            if (Session["listaFuncionalidade"] == null)
            {
                listaFuncionalidade = new Funcionalidade().Listar(UsuarioAtivo.NaturezaProcesso);
                ArmazenaValorSessao("listaFuncionalidade", listaFuncionalidade);
            }
            else
                listaFuncionalidade = (List<FuncionalidadeSistemaVO>)RecuperaValorSessao("listaFuncionalidade");

            //Populando todas as funcionalidades
            if (listaFuncionalidade.Count > 0)
            {
                lbxAssociar.DataSource = listaFuncionalidade;
                lbxAssociar.DataValueField = "CodFuncSistema";
                lbxAssociar.DataTextField = "NomFuncSistema";
                lbxAssociar.DataBind();
            }

            //Zerando as funcionalidades associadas
            lbxAssociados.Items.Clear();
        }
        else // Caso seja alteração
        {
            PerfilAcessoVO identPerfil = new PerfilAcessoVO();
            identPerfil.CodPerfilAcesso = short.Parse(hdnCodPerfilAlteracao.Value);

            //Listagem de Funcionalidades do Perfil
            List<FuncionalidadeSistemaVO> listaFuncionalidadePerfil = new Funcionalidade().ListarPorPerfil(identPerfil);

            //Listagem de todas as funcionalidades (exceto as do Perfil)
            List<FuncionalidadeSistemaVO> listaFuncionalidade = new Funcionalidade().ListarExcetoPerfilFuncionalidade(UsuarioAtivo.NaturezaProcesso,
                                                                                                                        listaFuncionalidadePerfil);

            //Populando todas as funcionalidades
            lbxAssociar.DataSource = listaFuncionalidade;
            lbxAssociar.DataValueField = "CodFuncSistema";
            lbxAssociar.DataTextField = "NomFuncSistema";
            lbxAssociar.DataBind();

            //Populando funcionalidades do perfil
            lbxAssociados.DataSource = listaFuncionalidadePerfil;
            lbxAssociados.DataValueField = "CodFuncSistema";
            lbxAssociados.DataTextField = "NomFuncSistema";
            lbxAssociados.DataBind();
        }
    }
    #endregion

    #region Eventos do RDCGrid
    protected void grdFPerfil_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Necessário para funcionamento da paginação, vide observação do componente
    }

    protected void grdFPerfil_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PerfilAcessoVO identPerfil = (PerfilAcessoVO)e.Row.DataItem;

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.gif";
            imgEditar.CommandArgument = identPerfil.CodPerfilAcesso.ToString();
            imgEditar.CommandName = "Editar";
            imgEditar.Style.Add("cursor", "hand");
            imgEditar.ToolTip = "Editar dados do Perfil [" + identPerfil.NomPerfilAcesso.Trim() + "]";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            imgExcluir.CommandArgument = identPerfil.CodPerfilAcesso.ToString();
            imgExcluir.CommandName = "Excluir";
            imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Perfil [" + identPerfil.NomPerfilAcesso.Trim() + "]?');";
            imgExcluir.Style.Add("cursor", "hand");
            imgExcluir.ToolTip = "Excluir Perfil [" + identPerfil.NomPerfilAcesso.Trim() + "]";
            #endregion

            e.Row.Cells[1].Text = identPerfil.NomPerfilAcesso;
            e.Row.Cells[2].Text = identPerfil.DescPerfilAcesso;

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
    }

    private void CarregarGridPerfil()
    {
        List<PerfilAcessoVO> listaPerfil = new PerfilAcesso().ListaPerfilAcesso(UsuarioAtivo);

        if (listaPerfil.Count > 0)
        {
            grdFPerfil.DataSource = listaPerfil;
            grdFPerfil.DataBind();
        }
    }

    protected void grdFPerfil_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        PerfilAcessoVO identPerfil = new PerfilAcessoVO();
        identPerfil.CodPerfilAcesso = short.Parse(e.CommandArgument.ToString());

        //Módulo de exclusão
        if (e.CommandName == "Excluir")
        {
            string mensagem = new PerfilAcesso().ExcluirPerfilAcesso(identPerfil, UsuarioAtivo);

            if (!string.IsNullOrEmpty(mensagem))
                MensagemCliente(mensagem);
            else
                CarregarGridPerfil();
        }
        else if (e.CommandName == "Editar")  //Módulo de alteração
        {
            grdFPerfil.Enabled = false;
            btnCancelaEdicao.Visible = true;

            //Retornar dados do perfil selecionado
            identPerfil.Natureza = UsuarioAtivo.NaturezaProcesso;
            identPerfil = new PerfilAcesso().ListarPerfilAcessoPorCodigo(identPerfil);

            //Carregar Campos
            tbxNomePerfil.Text = identPerfil.NomPerfilAcesso;
            tbxDescricao.Text = identPerfil.DescPerfilAcesso;

            //Carregar Funcionalidades
            hdnCodPerfilAlteracao.Value = identPerfil.CodPerfilAcesso.ToString();
            CarregarListaFuncionalidade();

            ExecutarScript(new StringBuilder(" PosicionarFoco(); "));
        }
    }
    #endregion

    #region Eventos dos Botões
    protected void btnGrava_Click(object sender, EventArgs e)
    {
        PerfilAcessoVO identPerfil = new PerfilAcessoVO();

        //Nome do Perfil
        identPerfil.NomPerfilAcesso = tbxNomePerfil.Text;

        //Descrição
        identPerfil.DescPerfilAcesso = tbxDescricao.Text;

        //String Xml das Funcionalidades selecionadas
        string xmlLista = new Funcionalidade().GerarXmlListaFuncionalidade(hdnListaFuncionalidade.Value.Split('|'));

        //Caso seja inclusão
        if (hdnCodPerfilAlteracao.Value == "0")
            identPerfil.CodPerfilAcesso = new PerfilAcesso().IncluirPerfilAcesso(identPerfil, UsuarioAtivo);
        else // Caso seja alteração
        {
            identPerfil.CodPerfilAcesso = short.Parse(hdnCodPerfilAlteracao.Value);
            new PerfilAcesso().AlterarPerfilAcesso(identPerfil, UsuarioAtivo);
            btnCancelaEdicao.Visible = false;
            grdFPerfil.Enabled = true;
        }

        //Associar funcionalidades ao perfil
        new PerfilAcesso().AssociarPerfilFuncionalidade(identPerfil.CodPerfilAcesso, xmlLista, UsuarioAtivo);

        //Limpar string XML de funcionalidades
        hdnListaFuncionalidade.Value = string.Empty;

        LimparCampos();

        //Atualizar lista de perfis
        CarregarGridPerfil();

        //Carregar funcionalidades
        CarregarListaFuncionalidade();

        ExecutarScript(new StringBuilder(" PosicionarFoco(); "));
    }

    protected void btnSai_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Default.aspx");
    }

    protected void btnCancelaEdicao_Click(object sender, EventArgs e)
    {
        LimparCampos();
        grdFPerfil.Enabled = true;
        btnCancelaEdicao.Visible = false;
        CarregarListaFuncionalidade();
        CarregarGridPerfil();
    }
    #endregion

    #region Funções Auxiliares
    private void LimparCampos()
    {
        hdnCodPerfilAlteracao.Value = "0";

        tbxNomePerfil.Text =
        tbxDescricao.Text = string.Empty;

        lbxAssociar.Items.Clear();
        lbxAssociados.Items.Clear();
    }
    #endregion*/
}
