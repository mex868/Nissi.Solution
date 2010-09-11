using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class relNotaFiscal : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dataIni= DateTime.MinValue;
            DateTime dataFim;
            string TipoPesq="" ;
            string strDataIni = "";
            string strDataFim = "";
            if (!string.IsNullOrEmpty(Request.QueryString["Inicio"].ToString()))
            {
                dataIni = Convert.ToDateTime(Request.QueryString["Inicio"].ToString());
                dataFim = Convert.ToDateTime(Request.QueryString["Fim"].ToString());
                strDataIni = dataIni.ToString("yyyy-MM-dd 00:00:00.000");
                strDataFim = dataFim.ToString("yyyy-MM-dd 00:00:00.000"); 
            }
            TipoPesq = Request.QueryString["UF"].ToString();
            ObjEmitente.SelectMethod = "GetData";
            ObjEmitente.SelectParameters.Clear();
            ObjEmitente.SelectParameters.Add("CodEmitente", "");
            ObjEmitente.DataBind();

            SouceNotaFiscal.SelectMethod = "GetData";
            SouceNotaFiscal.SelectParameters.Clear();
            SouceNotaFiscal.SelectParameters.Add("DataEmissaoIni",strDataIni);
            SouceNotaFiscal.SelectParameters.Add("DataEmissaoFim", strDataFim);
            SouceNotaFiscal.SelectParameters.Add("UF",TipoPesq);
            SouceNotaFiscal.DataBind();

            rwNota.DataBind();

        }

        protected void SouceNotaFiscal_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }
    }
}