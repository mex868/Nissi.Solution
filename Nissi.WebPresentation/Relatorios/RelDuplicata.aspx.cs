using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class RelDuplicata : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CodUf = Request.QueryString["CodNF"].ToString();
            ObjectDataSource1.InsertMethod = "GetData";
            ObjectDataSource1.SelectParameters.Clear();
            ObjectDataSource1.SelectParameters.Add("CodNF", CodUf);
            ObjectDataSource1.DataBind();

           /*ObjectDataSource3.InsertMethod = "GetData";
            ObjectDataSource3.SelectParameters.Clear();
            ObjectDataSource3.SelectParameters.Add("CodEmitente", "");
            ObjectDataSource3.DataBind();
            ReportViewer1.DataBind();*/

        }
    }
}