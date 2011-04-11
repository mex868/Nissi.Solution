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

            ODSCliente.SelectMethod = "GetData";
            ODSCliente.SelectParameters.Clear();
            ODSCliente.SelectParameters.Add("CodPessoaIni", codIni);
            ODSCliente.SelectParameters.Add("CodPessoaFim", codFim);
            ODSCliente.SelectParameters.Add("CodRefIni", nomIni);
            ODSCliente.SelectParameters.Add("CodRefFim", nomFim);
            ODSCliente.SelectParameters.Add("UF", uf);
            ODSCliente.DataBind();
            ReportViewer1.DataBind();
        }
    }
}