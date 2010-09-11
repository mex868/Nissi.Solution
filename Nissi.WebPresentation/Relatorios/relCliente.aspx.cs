using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class relClientes : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string codIni = string.Empty;
            string codFim = string.Empty;
            string NomIni = string.Empty;
            string NomFim = string.Empty;
            string UF = string.Empty;

             NomIni = Request.QueryString["Inicio"].ToString();
             NomFim = Request.QueryString["Fim"].ToString();
             codIni = Request.QueryString["codIni"].ToString();
             codFim = Request.QueryString["codFim"].ToString();
             UF = Request.QueryString["UF"].ToString();

            ODSemitente.SelectMethod = "GetData";
            ODSemitente.SelectParameters.Clear();
            ODSemitente.SelectParameters.Add("CodEmitente", "");
            ODSemitente.DataBind();

            ODScliente.SelectMethod = "GetData";
            ODScliente.SelectParameters.Clear();
            ODScliente.SelectParameters.Add("CodPessoaIni", codIni);
            ODScliente.SelectParameters.Add("CodPessoaFim", codFim);
            ODScliente.SelectParameters.Add("RazaoSocialIni", NomIni);
            ODScliente.SelectParameters.Add("RazaoSocialFim", NomFim);
            ODScliente.SelectParameters.Add("UF", UF);
            ODScliente.DataBind();
            ReportViewer1.DataBind();
        }
    }
}