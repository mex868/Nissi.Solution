using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Nissi.Util;

namespace Nissi.WebPresentation
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            string file = ((System.Web.HttpApplication)(sender)).Request.CurrentExecutionFilePath;
            Server.ClearError();

            if (ex != null && ex.InnerException != null)
            {
                ex = ex.InnerException;

                // Tratamento para evitar o problema de super-log causado por URLs
                // absolutas geradas com til no meio do caminho
                if (ex.Message != "Arquivo inexistente." &&
                    ex.Source.IndexOf("ProcessRequestInternal(System.Web.HttpContext)") < 0)
                {
                    //Session.Add("Excecao", ex);
                    SendEmail.enviarEmailAnexo("smtp.nissimetal.com.br", "mex868@gmail.com;", "Erro do Sistema",
                                               new StringBuilder(ex.Message + " - Arquivo: " + file));
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}