using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.EntradaEstoque
{
    public partial class GerarPDF : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(new DateTime(1970, 1, 1));
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            byte[] pdfByte;
            try
            {
                string sVarCache = Request["Variavel_Cache"];
                if ((sVarCache != "") && (Cache[sVarCache] != null))
                {
                    pdfByte = (byte[])Cache[sVarCache];
                }
                else
                {
                    pdfByte = (byte[])ViewState[sVarCache];
                }                                 
                Response.ContentType = "application/pdf";             
                Response.AddHeader("content-disposition", "attachment;filename=labtest.pdf");             
                Response.Buffer = true;             
                Response.Clear();
                Response.OutputStream.Write(pdfByte, 0, pdfByte.Length);             
                Response.OutputStream.Flush();             
                Response.End();
            }
            catch (Exception ex)
            {
                //Blocks.ExceptionManager.Publish(Ex);
            }
        }
    }
}