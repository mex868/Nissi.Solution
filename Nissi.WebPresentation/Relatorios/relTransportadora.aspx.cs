using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class relTransportadora : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Inicio = Request.QueryString["Inicio"].ToString();
            string Fim = Request.QueryString["Fim"].ToString();

            string Uf = Request.QueryString["UF"].ToString();

            odsTransportadora.SelectMethod = "GetData";
            odsTransportadora.SelectParameters.Clear();
            odsTransportadora.SelectParameters.Add("CodPessoaIni", Inicio);
            odsTransportadora.SelectParameters.Add("CodPessoaFim", Fim);
            odsTransportadora.SelectParameters.Add("UF", Uf);
            odsTransportadora.DataBind();

            odsEmitente.SelectMethod = "GetData";
            odsEmitente.SelectParameters.Clear();
            odsEmitente.SelectParameters.Add("CodEmitente", "");
            odsEmitente.DataBind();
            ReportViewer1.DataBind();

        }
    }
}