using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class RelClientes : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             string nomIni = Request.QueryString["RazaoIni"].ToString();
             string nomFim = Request.QueryString["RazaoFim"].ToString();
             string codIni = Request.QueryString["CodIni"].ToString();
             string codFim = Request.QueryString["CodFim"].ToString();
             string uf = Request.QueryString["UF"].ToString();

            ODSemitente.SelectMethod = "GetData";
            ODSemitente.SelectParameters.Clear();
            ODSemitente.SelectParameters.Add("CodEmitente", "");
            ODSemitente.DataBind();

            ODScliente.SelectMethod = "GetData";
            ODScliente.SelectParameters.Clear();
            ODScliente.SelectParameters.Add("CodPessoaIni", codIni);
            ODScliente.SelectParameters.Add("CodPessoaFim", codFim);
            ODScliente.SelectParameters.Add("RazaoSocialIni", nomIni);
            ODScliente.SelectParameters.Add("RazaoSocialFim", nomFim);
            ODScliente.SelectParameters.Add("UF", uf);
            ODScliente.DataBind();
            ReportViewer1.DataBind();
        }
    }
}