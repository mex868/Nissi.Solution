using System;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;
using System.Linq;


    public partial class EnviarEmailEmitente : BasePage
    {
        public EnviarEmailEmitente()
        {
            caminhoAplicacao = Server.MapPath(@"~\Relatorios\");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.InibirTopo(); 
            var emailEmitente = EmailEmitente.ListarPorTipo(0);
            int codCliente = int.Parse(Request.QueryString["CodPessoa"]);
            var email = Cliente.PegarEmail(codCliente);
            emailEmitente.Add(new EmailEmitenteVO()
                                  {
                                      Email = email
                                  });
            foreach (var emailEmitenteVO in emailEmitente)
            {
                if (emailEmitenteVO.Email != "")
                {
                    ckbListEmail.Items.Add(new ListItem(emailEmitenteVO.Email
                    ,emailEmitenteVO.CodEmailEmitente.ToString(),true));
                }
            }
            btnEnviar.Attributes.Add("onclick", "window.close();");
            
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            var identFornecedor = new FornecedorVO();
            identFornecedor.CodPessoa = int.Parse(Request.QueryString["CodPessoa"]);
            int codigo = int.Parse(Request.QueryString["CodPedidoCompra"]);
            identFornecedor = new Fornecedor().Listar(identFornecedor).FirstOrDefault();
            for (int i = 0; i < ckbListEmail.Items.Count-1;i++ )
            {
                Nissi.Util.SendEmail.enviarEmailAnexo("smtp.nissimetal.com.br", ckbListEmail.Items[i].Text, caminhoAplicacao + codigo.ToString().PadLeft(8, '0') + ".PDF", "Pedido de Compra", getBody(codigo, identFornecedor.RazaoSocial, identFornecedor.CNPJ));
            }
        }
        public StringBuilder getBody(int codigo, string razaoSocial, string cNpj)
        {
            StringBuilder sbBody = new StringBuilder();
            //Adiciona estrutura HTML do E-Mail  
            sbBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbBody.Append("<head><title>Pedido de Compra</title>");
            sbBody.Append("<style type='text/css'>body {margin-left: 0px;margin-top: 0px;margin-right: 0px;margin-bottom: 0px;background-color: #E1E0F2;}");
            sbBody.Append("<div style=1margin:20 0 0 1001>");
            sbBody.Append("<div style='border:1px solid black; width:50%; padding:0 0 0 10'>");
            sbBody.Append("body,td,th {font-family: Verdana, Geneva, sans-serif;font-size: 12px;}</style></head><body>");
            sbBody.Append("Está mesangem se refere ao Pedido de Compra número: <b>[" + codigo.ToString().PadLeft(8,'0') + "] que está em anexo</b><br /><br />");
            sbBody.Append("<strong><h3>.::Emitida Para:</h3></strong><br />");
            sbBody.Append("<hr style='width:100%; padding-right:10px'>");
            sbBody.Append("<b>Razão Social:</b><br />");
            //Adiciona texto digitado no Razão Social
            sbBody.Append("[" + razaoSocial + "]");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>CNPJ:</b><br />");
            //Adiciona texto digitado no CNPJ  
            sbBody.Append("[" + cNpj + "]");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b></b><br />");
            //Adiciona texto digitado no TextBox txtAssunto  
            sbBody.Append("Este e-mail foi enviado pelo Sistema de Pedido de Compra da NISSI METALURGICA");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Powered By:</b><br />");
            //Adiciona texto digitado no TextBox txtMensagem  
            sbBody.Append("CloudHolding -  http://www.cloudholding.com.br");
            sbBody.Append("<br /><br />");
            sbBody.Append("</div>");
            sbBody.Append("</div>");
            sbBody.Append("<br /></body></html>");
            return sbBody;
        }

    }
