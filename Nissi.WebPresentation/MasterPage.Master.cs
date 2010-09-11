using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Nissi.Model;
using System.Xml;
using System.Text;


namespace Nissi.WebPresentation
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "[Nissi] - Módulo de Nota Fiscal";

            if (!Page.IsPostBack)
            {
                MapearAplicacaoUsuario();
            }
        }



        #region scmPrincipal_AsyncPostBackError
        protected void scmPrincipal_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            scmPrincipal.AsyncPostBackErrorMessage = e.Exception.Message;
            throw e.Exception;
        }
        #endregion

        #region Métodos auxiliares

        private void MapearAplicacaoUsuario()
        {
            //Instância do Usuário conectado
            //FuncionarioVO mUsuarioAtivo = new FuncionarioVO();
            //mUsuarioAtivo = (FuncionarioVO)Session["UsuarioAtivo"];

            //Caso encontre o Usuário conectado
            //if (mUsuarioAtivo != null)
            //{
                //BasePage basePage = new BasePage();
               // lblUsuario.Text = mUsuarioAtivo.Login;

                //Carregar Menu conforme Perfil de Acesso
                menuPrincipal.DataSource = Server.MapPath("~/menu.xml");
                menuPrincipal.Layout = skmMenu.MenuLayout.Horizontal;
                menuPrincipal.DefaultCssClass = "Menu";
                menuPrincipal.SubMenuCssClass = "Menu";
                menuPrincipal.SelectedMenuItemStyle.CssClass = "MenuItemSelecionado";

                //Definir permissão de Acesso via Menu
                /*foreach (PerfilAcessoVO identPerfil in mUsuarioAtivo.Perfils)
                {
                    switch (identPerfil.CodPerfilAcesso)
                    {
                        case BasePage.perfilAdmForum:
                            menuPrincipal.UserRoles.Add("perfilAdmForum");
                            break;
                        case BasePage.perfilAdmSis:
                            menuPrincipal.UserRoles.Add("perfilAdmSis");
                            break;
                        case BasePage.perfilSTI:
                            menuPrincipal.UserRoles.Add("perfilSTI");
                            break;
                    }
                }
                */
                menuPrincipal.zIndex = 1000;
                menuPrincipal.DataBind();
            //}
        }

        public void PosicionarFoco(Control controle)
        {
            scmPrincipal.SetFocus(controle);
        }

        public void InibirMenu()
        {
            menuPrincipal.Visible = false;
            Page.ClientScript.RegisterStartupScript(typeof(string), "Topo", " <script> InibirMenu(); </script> ");
        }

        public void InibirTopo()
        {
            menuPrincipal.Visible = false;
            Page.ClientScript.RegisterStartupScript(typeof(string), "Topo", "<script>InibirTopo();</script>");
        }

        #endregion

    }

}
