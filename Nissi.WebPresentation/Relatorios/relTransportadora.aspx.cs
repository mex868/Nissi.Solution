using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class RelTransportadora : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string inicio = Request.QueryString["strRazaoIni"].ToString();
            string fim = Request.QueryString["strRazaoFim"].ToString();

           // string Uf = Request.QueryString["UF"].ToString();

            odsTransportadora.SelectMethod = "GetData";
            odsTransportadora.SelectParameters.Clear();
            odsTransportadora.SelectParameters.Add("RazaoSocialIni", inicio);
            odsTransportadora.SelectParameters.Add("RazaoSocialFim", fim);
            odsTransportadora.DataBind();

            odsEmitente.SelectMethod = "GetData";
            odsEmitente.SelectParameters.Clear();
            odsEmitente.SelectParameters.Add("CodEmitente", "");
            odsEmitente.DataBind();
            ReportViewer1.DataBind();

        }
    }
}