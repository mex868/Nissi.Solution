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
            string inicio = Request.QueryString["RazaoIni"].ToString();
            string fim = Request.QueryString["RazaoFim"].ToString();

           // string Uf = Request.QueryString["UF"].ToString();

            odsTransportadora.SelectMethod = "GetData";
            odsTransportadora.SelectParameters.Clear();
            odsTransportadora.SelectParameters.Add("CodPessoaIni", inicio);
            odsTransportadora.SelectParameters.Add("CodPessoaFim", fim);
            odsTransportadora.DataBind();

            odsEmitente.SelectMethod = "GetData";
            odsEmitente.SelectParameters.Clear();
            odsEmitente.SelectParameters.Add("CodEmitente", "");
            odsEmitente.DataBind();
            ReportViewer1.DataBind();

        }
    }
}