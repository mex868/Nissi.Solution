using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nissi.WebPresentation
{
    /// <summary>
    /// Summary description for GerarPDF1
    /// </summary>
    public class GerarPDF1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetExpires(new DateTime(1970, 1, 1));
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            byte[] pdfByte;
            try
            {

                string sVarCache = context.Request["Variavel_Cache"];
                if ((sVarCache != "") && (context.Cache[sVarCache] != null))
                {
                    pdfByte = (byte[])context.Cache[sVarCache];
                    context.Cache.Remove(sVarCache);
                    // Cache[sVarCache] = null;
                }
                else
                {

                    pdfByte = new byte[0];
                }

                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("content-disposition", "attachment;filename=labtest.pdf");
                context.Response.Buffer = true;
                context.Response.Clear();
                context.Response.OutputStream.Write(pdfByte, 0, pdfByte.Length);
                context.Response.OutputStream.Flush();
                context.Response.End();
            }
            catch (Exception ex)
            {
                //Blocks.ExceptionManager.Publish(Ex);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}