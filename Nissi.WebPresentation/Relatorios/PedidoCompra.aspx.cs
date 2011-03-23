using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Nissi.WebPresentation.DataSetTableAdapters;

namespace Nissi.WebPresentation.Relatorios
{
    public partial class PedidoCompra : System.Web.UI.Page
    {
        protected string caminhoAplicacao;

        public PedidoCompra()
        {
            caminhoAplicacao = Server.MapPath(@"~\Relatorios\");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string codigo = null;
            if (!string.IsNullOrEmpty(Request.QueryString["CodPedidoCompra"]))
                codigo = Request.QueryString["CodPedidoCompra"];
            var tipo = Request.QueryString["tipo"];
            CreateReport(tipo,codigo);
        }
        protected void CreateReport(string tipo, string codigo)
        {
            if (tipo == "imprimir")
            {
                ODSEmitente.SelectMethod = "GetData";
                ODSEmitente.SelectParameters.Clear();
                ODSEmitente.SelectParameters.Add("CodEmitente", "");
                ODSEmitente.DataBind();
                ODSPedidoCompra.SelectMethod = "GetData";
                ODSPedidoCompra.SelectParameters.Clear();
                ODSPedidoCompra.SelectParameters.Add("CodPedidoCompra", codigo);
                ODSPedidoCompra.DataBind();
                ReportViewer1.DataBind();                
            }
            else
            {
               FileInfo fileInfo = new FileInfo(caminhoAplicacao + codigo.ToString().PadLeft(8, '0') + ".PDF");
               if (fileInfo.Exists)
                   fileInfo.Delete();
                var dsEmitente = new pr_selecionar_emitenteTableAdapter();
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetEmitente",
                                                                               dsEmitente.GetData(null).ToList()));
                var dsPedidoCompra = new pr_relatorio_pedidocompraTableAdapter();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetPedidoCompra",
                                                                               dsPedidoCompra.GetData(
                                                                                   int.Parse(codigo)).ToList()));
                const string deviceInfo = "<DeviceInfo>" +
                                          " <OutputFormat>PDF</OutputFormat>" +
                                          " <PageWidth>8.5in</PageWidth>" +
                                          " <PageHeight>10.5in</PageHeight>" +
                                          " <MarginTop>0.05in</MarginTop>" +
                                          " <MarginLeft>0.05in</MarginLeft>" +
                                          " <MarginRight>0.05in</MarginRight>" +
                                          " <MarginBottom>0.05in</MarginBottom>" +
                                          "</DeviceInfo>";

                var bytes = new byte[0];
                if (ReportViewer1.LocalReport.IsReadyForRendering)
                {
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding,
                                                             out extension, out streamids, out warnings);
                }
                var fs = new FileStream(caminhoAplicacao + codigo.PadLeft(8, '0') + ".PDF", FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                
                var codPessoa = Request.QueryString["CodPessoa"];
                Response.Redirect("../Emitente/EnviarEmailEmitente.aspx?CodPedidoCompra=" + codigo + "&CodPessoa=" + codPessoa);
            }

        }
    }
    
}