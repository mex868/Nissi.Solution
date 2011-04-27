using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Nissi.WebPresentation.App_Code
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class WebService : System.Web.Services.WebService
    {

        public WebService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public void UploadFileAjax()
        {
            HttpRequest request = this.Context.Request;

            HttpPostedFile file = request.Files["file"];
            //Save the file appropriately.
            //file.SaveAs(...);

            string msg = "File Name: " + file.FileName;
            msg += "<br />First Name: " + request["first-name"];
            msg += "<br />Country: " + request["country_Value"];

            var o = new { success = true, msg = msg };
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            HttpContext context = this.Context;
            HttpResponse response = context.Response;

            context.Response.ContentType = "text/html";

            string s = serializer.Serialize(o);
            byte[] b = response.ContentEncoding.GetBytes(s);
            response.AddHeader("Content-Length", b.Length.ToString());
            response.BinaryWrite(b);

            try
            {
                this.Context.Response.Flush();
                this.Context.Response.Close();
            }
            catch (Exception) { }
        }

    }
}

