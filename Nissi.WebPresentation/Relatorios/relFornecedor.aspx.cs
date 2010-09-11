using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class relFornecedor : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string Inicio = Request.QueryString["Inicio"].ToString();
            string Fim = Request.QueryString["Fim"].ToString();

            string Uf = Request.QueryString["UF"].ToString();

            odsFornecedor.SelectMethod = "GetData";
            odsFornecedor.SelectParameters.Clear();
            odsFornecedor.SelectParameters.Add("CodPessoaIni", Inicio);
            odsFornecedor.SelectParameters.Add("CodPessoaFim", Fim);
            odsFornecedor.SelectParameters.Add("UF", Uf);
            odsFornecedor.DataBind();

            odsEmitente.SelectMethod = "GetData";
            odsEmitente.SelectParameters.Clear();
            odsEmitente.SelectParameters.Add("CodEmitente", "");
            odsEmitente.DataBind();
            ReportViewer1.DataBind();

        }
    }
}