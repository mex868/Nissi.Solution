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
using System.Text;

namespace Nissi.WebPresentation
{
    public partial class Login : BasePage
    {
        #region Variáveis Globais
        private string _timeOut = ConfigurationManager.AppSettings["Nissi.Timeout"].ToString();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.InibirTopo();

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["EfetuarLogoff"] != null)
                {
                    ExecutarScript(new StringBuilder("window.close()"));
                }


                // this.Master.PosicionarFoco(tbxNomeUsuario);

                //if (Request.QueryString["EfetuarLogoff"] != null)
                //{
                  //  Response.Cookies.Remove(UsuarioAtivo.CodFuncionario.ToString());
                   // UsuarioAtivo = null;
                //}
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            FuncionarioVO identUsuario = new FuncionarioVO();

            string loginUsuario = tbxNomeUsuario.Text;
            string modoDesenvolvedor = string.Empty;

            #region Validar Acesso 'Modo Desenvolvedor'
            if ((tbxNomeUsuario.Text.Length > 2) &&
                (tbxNomeUsuario.Text.Substring(0, 2).Equals("@@")))
            {
                loginUsuario = tbxNomeUsuario.Text.Substring(2, tbxNomeUsuario.Text.Length - 2);
                if (string.IsNullOrEmpty(modoDesenvolvedor)) modoDesenvolvedor = "yes";
            }
            else
                modoDesenvolvedor = "no";
            #endregion

            //Autenticar conexão de Usuário no Banco
            string mensagem = new Funcionario().Autenticar(loginUsuario, tbxSenha.Text, out identUsuario);

            //Falha ao autenticar
            if (!string.IsNullOrEmpty(mensagem))
                MensagemCliente(mensagem);
            else
            {
                #region Registrar usuário na aplicação
                //Registrar login no Asp.Net
                FormsAuthenticationTicket ticket = new
                        FormsAuthenticationTicket(
                        1,
                        identUsuario.CodFuncionario.ToString(),
                        DateTime.Now,
                        DateTime.Now.AddMinutes(int.Parse(_timeOut)),
                        false,
                        identUsuario.Login);

                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                //Salvar cookie do Usuário
                HttpCookie ticketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(ticketCookie);

                //Armazenar Usuário na Sessão
                UsuarioAtivo = identUsuario;

                //Validar Acesso 'Modo Desenvolvedor'
                if (modoDesenvolvedor.Equals("yes"))
                {
                    UsuarioAtivo.ModoDesenvolvedor = true;
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    UsuarioAtivo.ModoDesenvolvedor = false;
                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "tamanhoTela", "CommonAbrirTelaCheia(null, null, 'Default.aspx');", true);
                }
                #endregion
            }

        }
    }
}
