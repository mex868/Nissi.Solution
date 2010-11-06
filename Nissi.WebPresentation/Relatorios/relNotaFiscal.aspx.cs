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
            DateTime dataFim=DateTime.MinValue;
            string TipoPesq=null;

            if (!string.IsNullOrEmpty(Request.QueryString["DtIni"].ToString()))
            {
                dataIni = Convert.ToDateTime(Request.QueryString["DtIni"].ToString());
                dataFim = Convert.ToDateTime(Request.QueryString["DtFim"].ToString());

            }
            if (!string.IsNullOrEmpty(Request.QueryString["Tipo"].ToString()))
            {
                TipoPesq = Request.QueryString["Tipo"].ToString();
            }

            //DataSet Emitente
            ObjEmitente.SelectMethod = "GetData";
            ObjEmitente.SelectParameters.Clear();
            ObjEmitente.SelectParameters.Add("CodEmitente", "");
            ObjEmitente.DataBind();

            SouceNotaFiscal.SelectMethod = "GetData";
            SouceNotaFiscal.SelectParameters.Clear();
            SouceNotaFiscal.SelectParameters.Add("DataEmissaoIni",dataIni.ToString("yyyy-MM-dd 00:00:00.000"));
            SouceNotaFiscal.SelectParameters.Add("DataEmissaoFim", dataFim.ToString("yyyy-MM-dd 00:00:00.000"));
            SouceNotaFiscal.SelectParameters.Add("TIPO", TipoPesq);
            SouceNotaFiscal.DataBind();


            rwNota.DataBind();

        }

        protected void SouceNotaFiscal_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }
    }
}