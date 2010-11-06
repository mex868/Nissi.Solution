using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class RelProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dataIni = DateTime.MinValue;
            DateTime dataFim = DateTime.MinValue;
            if (!string.IsNullOrEmpty(Request.QueryString["DtIni"].ToString()))
            {
                dataIni = Convert.ToDateTime(Request.QueryString["DtIni"].ToString());
                dataFim = Convert.ToDateTime(Request.QueryString["DtFim"].ToString());

            }
            string codigo = null;
            string descricao = null;
            if (!string.IsNullOrEmpty(Request.QueryString["Codigo"]))
                codigo = Request.QueryString["Codigo"].ToString();

            if (!string.IsNullOrEmpty(Request.QueryString["Descricao"]))
                descricao = Request.QueryString["Descricao"].ToString();


            ODSemitente.SelectMethod = "GetData";
            ODSemitente.SelectParameters.Clear();
            ODSemitente.SelectParameters.Add("CodEmitente", "");
            ODSemitente.DataBind();

            ODSproduto.SelectMethod = "GetData";
            ODSproduto.SelectParameters.Clear();
            ODSproduto.SelectParameters.Add("DataEmissaoIni", dataIni.ToString("yyyy-MM-dd 00:00:00.000"));
            ODSproduto.SelectParameters.Add("DataEmissaoFim", dataFim.ToString("yyyy-MM-dd 00:00:00.000"));
            ODSproduto.SelectParameters.Add("Codigo",codigo);
            ODSproduto.SelectParameters.Add("Descricao",descricao);
            ODSproduto.DataBind();

            ReportViewer1.DataBind();

        }
    }
}