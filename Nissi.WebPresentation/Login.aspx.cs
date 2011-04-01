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
                Master.PosicionarFoco(tbxNomeUsuario);

                if (Session["xmlMenu"] != null)
                    Session.Remove("xmlMenu");

                if (Session["userTemp"] != null)
                    Session.Remove("userTemp");

                if (Request.QueryString["EfetuarLogoff"] != null)
                {
                    Response.Cookies.Remove(UsuarioAtivo.CodFuncionario.ToString());
                    UsuarioAtivo = null;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            AutenticarUsuario();
        }

        protected void btnGrava_Click(object sender, EventArgs e)
        {
            Gravar();
        }

        private void Gravar()
        {
            var identUsuario = (UsuarioVO)RecuperaValorSessao("userTemp");

            if (identUsuario != null)
            {
                    //Alterando a senha
                    identUsuario.Funcionario.Senha = Usuario.CriptografarSenha(tbxNovaSenha.Text);
                    Usuario.AlterarSenha(identUsuario);

                    //Destruindo a sessão
                    DestroiValorSessao("userTemp");

                    mpeNovaSenha.Hide();

                    //Preencher campos de Login
                    tbxNomeUsuario.Text = identUsuario.Funcionario.Login;
                    hdnNovaSenha.Value = tbxNovaSenha.Text;
                    
                    //Retornar a tela anterior e exibe combo de Departamento
                    this.AutenticarUsuario();
                    //btnLogin_Click(sender, e);
            }
            else
                MensagemCliente("Erro ao recuperar dados do Usuário. Clique em [Voltar] e tente novamente!");
 
        }

        private void AutenticarUsuario()
        {
            UsuarioVO identUsuario;

            var loginUsuario = tbxNomeUsuario.Text;
            var modoDesenvolvedor = string.Empty;

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
            //Caso seja primeiro acesso, retorno a senha informada na ModalPopup
            if (!string.IsNullOrEmpty(hdnNovaSenha.Value))
                tbxSenha.Text = hdnNovaSenha.Value;
            //Autenticar conexão de Usuário no Banco
            var mensagem = Usuario.Autenticar(loginUsuario, tbxSenha.Text, out identUsuario);
            if (mensagem == "primeiroacesso")
            {
                ArmazenaValorSessao("userTemp", identUsuario);
                //Montar tela para informe de Nova Senha
                mpeNovaSenha.Show();

                //Foco no campo Nova Senha
                Master.PosicionarFoco(tbxNovaSenha);
            }
            else
            {
                //Falha ao autenticar
                if (!string.IsNullOrEmpty(mensagem))
                    MensagemCliente(mensagem);
                else
                {
                    #region Registrar usuário na aplicação
                    //Registrar login no Asp.Net
                    var ticket = new
                            FormsAuthenticationTicket(
                            1,
                            identUsuario.Funcionario.CodFuncionario.ToString(),
                            DateTime.Now,
                            DateTime.Now.AddMinutes(int.Parse(_timeOut)),
                            false,
                            identUsuario.Funcionario.Login);

                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    //Salvar cookie do Usuário
                    var ticketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(ticketCookie);

                    //Armazenar Usuário na Sessão
                    UsuarioAtivo = identUsuario.Funcionario;

                    //Validar Acesso 'Modo Desenvolvedor'
                    if (modoDesenvolvedor.Equals("yes"))
                    {
                        UsuarioAtivo.ModoDesenvolvedor = true;
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        UsuarioAtivo.ModoDesenvolvedor = false;
                        Response.Redirect("Default.aspx");
                    }
                    #endregion
                }
            }
        }
    }
}
