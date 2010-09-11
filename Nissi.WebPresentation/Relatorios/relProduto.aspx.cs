using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class relProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dataIni = DateTime.MinValue;
            DateTime dataFim;
            string strDataIni = string.Empty;
            string strDataFim = string.Empty;
            string codigo = Request.QueryString["Codigo"]==""?null: Request.QueryString["Codigo"];
            string descricao = Request.QueryString["Descricao"] == "" ? null : Request.QueryString["Descricao"];

            if (!string.IsNullOrEmpty(Request.QueryString["Inicio"].ToString()))
            {
                dataIni = Convert.ToDateTime(Request.QueryString["Inicio"].ToString());
                dataFim = Convert.ToDateTime(Request.QueryString["Fim"].ToString());
                strDataIni = dataIni.ToString("yyyy-MM-dd 00:00:00.000");
                strDataFim = dataFim.ToString("yyyy-MM-dd 00:00:00.000"); 
            }
            ODSemitente.SelectMethod = "GetData";
            ODSemitente.SelectParameters.Clear();
            ODSemitente.SelectParameters.Add("CodEmitente", "");
            ODSemitente.DataBind();

            ODSproduto.SelectMethod = "GetData";
            ODSproduto.SelectParameters.Clear();
            ODSproduto.SelectParameters.Add("Codigo",codigo);
            ODSproduto.SelectParameters.Add("Descricao",descricao);
            ODSproduto.SelectParameters.Add("DataEmissaoIni",strDataIni);
            ODSproduto.SelectParameters.Add("DataEmissaoFim",strDataFim);
            ODSproduto.DataBind();

            ReportViewer1.DataBind();

        }
    }
}