using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Model;

namespace Nissi.WebPresentation.Relatorios.NotaFiscal
{
    public partial class RelNotaFiscalPrincipal : BasePage
    {
        const string StrXml = @"C:\nfe-app\nfe\arquivos\procNFe35100705533093000133550010000000021000000025-procNFe.xml";
        protected void Page_Load(object sender, EventArgs e)
        {
            var ds = new DataSet();
            ds.ReadXml(StrXml);

            ReportViewer1.DataBind();
        }

        protected void ObjectDataSourceNFE_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }
    }
}