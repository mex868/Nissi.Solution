using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class RelFornecedor : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string inicio = Request.QueryString["RazaoIni"].ToString();
            string fim = Request.QueryString["RazaoFim"].ToString();

           // string Uf = Request.QueryString["UF"].ToString();

            odsFornecedor.SelectMethod = "GetData";
            odsFornecedor.SelectParameters.Clear();
            odsFornecedor.SelectParameters.Add("CodPessoaIni", inicio);
            odsFornecedor.SelectParameters.Add("CodPessoaFim", fim);
           // odsFornecedor.SelectParameters.Add("UF", Uf);
            odsFornecedor.DataBind();

            odsEmitente.SelectMethod = "GetData";
            odsEmitente.SelectParameters.Clear();
            odsEmitente.SelectParameters.Add("CodEmitente", "");
            odsEmitente.DataBind();
            ReportViewer1.DataBind();

        }
    }
}