using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;

namespace Nissi.WebPresentation
{
    public partial class GeraImagem : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            Response.Cache.SetExpires(new DateTime(1970, 1, 1));
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            byte[] imagem;
            try
            {

                string sVarCache = Request["Variavel_Cache"];
                if ((sVarCache != "") && (Cache[sVarCache] != null))
                {
                    imagem = (byte[])Cache[sVarCache];
                    Cache.Remove(sVarCache);
                    // Cache[sVarCache] = null;
                }
                else
                {
                    EmitenteVO tempEmitente = new EmitenteVO();
                    EmitenteVO identEmitente = new EmitenteVO();
                    identEmitente.CodEmitente = Convert.ToInt32(Session["CodEmitente"]);
                    tempEmitente = new Emitente().Listar(identEmitente)[0];

                    imagem = (byte[])tempEmitente.Image;
                }

                Response.OutputStream.Write(imagem, 0, imagem.Length);

                this.ContentType = "image/jpeg";
                Response.AddHeader("content-disposition", "inline; filename=assinatura.jpg");
                Response.AddHeader("Content-Length", imagem.Length.ToString());
            }
            catch (Exception ex)
            {
                //Blocks.ExceptionManager.Publish(Ex);
            }
        }
    }
}
