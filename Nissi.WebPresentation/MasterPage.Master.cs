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
                Session.Timeout = 300;                
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
            var mUsuarioAtivo = (FuncionarioVO)Session["UsuarioAtivo"];

            //Caso encontre o Usuário conectado
            if (mUsuarioAtivo != null)
            {
                var basePage = new BasePage();
                lblUsuario.Text = mUsuarioAtivo.Apelido;
                lblData.Text = DateTime.Now.ToString();
                lblVersion.Text = VersionInfo;
                string xmlMenu = string.Empty;
                if (Session["xmlMenu"] != null)
                    xmlMenu = (string)Session["xmlMenu"];
                else
                {
                    if (mUsuarioAtivo.CodFuncionario != null)
                        xmlMenu = Business.Usuario.ListarMenuUsuario(mUsuarioAtivo.CodFuncionario.Value);
                    //Armazenar XML do Menu na Sessão
                    Session.Add("xmlMenu", xmlMenu);
                }
                var xmldoc = new XmlDocument();
                xmldoc.LoadXml(xmlMenu);
                //Carregar Menu conforme Perfil de Acesso);
                menuPrincipal.DataSource = xmldoc;
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
                menuPrincipal.zIndex = 9999;
                menuPrincipal.DataBind();
            }
        }
        protected string VersionInfo{
            get{
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
               }
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
