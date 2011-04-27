using System;
using System.Web;
using System.IO;

namespace Nissi.WebPresentation.EntradaEstoque
{


    public class FileUploadHandler: IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //Uploaded File Deletion
                if (context.Request.QueryString.Count > 0)
                {
                    NissiSession.ArquivoPdf = new byte[0];
                }
                //File Upload
                else
                {
                    Stream Input = context.Request.Files[0].InputStream;
                    // Inicializa o buffer			
                    byte[] certificadoPdf = new byte[Input.Length];
                    // Lê a imagem do arquivo          			 
                    Input.Read(certificadoPdf, 0, Convert.ToInt32(Input.Length));
                    // Joga no ViewState
                    NissiSession.ArquivoPdf = certificadoPdf;
                }
            }
            catch
            {

            }
        }

        #endregion
    }
}
