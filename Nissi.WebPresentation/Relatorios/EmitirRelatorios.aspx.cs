using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;
using System.Text;


namespace Nissi.WebPresentation.Relatorios
{
    public partial class EmitirRelatorios : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarCombos();
            }

        }

        private void CarregarCombos()
        {

                //Carrega a combo com o uf
                ddlUF.DataSource = new UnidadeFederacao().Listar();
                ddlUF.DataTextField = "CodUF";
                ddlUF.DataValueField = "CodUF";
                ddlUF.DataBind();
                ddlUF.Items.Insert(0, new ListItem("Todos", ""));

                ddlRegiao.Items.Add(new ListItem("Notas Fiscais no Estado", "1"));
                ddlRegiao.Items.Add(new ListItem("Notas Fiscais Fora do Estado", "2"));
                ddlRegiao.DataBind();

            
          
        }

        protected void ddlRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableVarios.Style.Add("display", "none");
            tableNotaFiscal.Style.Add("display", "none");
            tdCod1.Style.Add("display", "none");
            tdCod2.Style.Add("display", "none");
            tdCod3.Style.Add("display", "none");
            tdCod4.Style.Add("display", "none");
            tdRazao1.Style.Add("display", "none");
            tdRazao2.Style.Add("display", "none");
            tdRazao3.Style.Add("display", "none");
            tdRazao4.Style.Add("display", "none");
            tableVarios.Style.Add("display", "none");
            tableNotaFiscal.Style.Add("display", "none");
            tbRadioCliente.Style.Add("display", "none");
            tableProduto.Style.Add("display", "none");
            tdUf1.Style.Add("display", "none");
            tdUf2.Style.Add("display", "none");
            if (ddlRelatorio.SelectedIndex > 0)
            {
                if (ddlRelatorio.SelectedValue != "2")
                {
                    tableVarios.Style.Add("display", "block");

                    if (ddlRelatorio.SelectedValue == "1")
                    {
                        tdUf1.Style.Add("display", "block");
                        tdUf2.Style.Add("display", "block");
                        tbRadioCliente.Style.Add("display", "block");
                        if (rbtCod.Checked)
                        {
                            tdCod1.Style.Add("display", "block");
                            tdCod2.Style.Add("display", "block");
                            tdCod3.Style.Add("display", "block");
                            tdCod4.Style.Add("display", "block");
                        }                        

                        else
                        {
                            tdRazao1.Style.Add("display", "block");
                            tdRazao2.Style.Add("display", "block");
                            tdRazao3.Style.Add("display", "block");
                            tdRazao4.Style.Add("display", "block");
                        }
                    }
                    else if (ddlRelatorio.SelectedValue == "5")
                    {
                        tableProduto.Style.Add("display", "block");
                    }
                    else
                    {
                        tdRazao1.Style.Add("display", "block");
                        tdRazao2.Style.Add("display", "block");
                        tdRazao3.Style.Add("display", "block");
                        tdRazao4.Style.Add("display", "block");


                    }
                }
                else
                {
                    tableNotaFiscal.Style.Add("display", "block");
                }
            }
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            if (ddlRelatorio.SelectedIndex > 0)
            {
                string strUrl = "";
                StringBuilder parametro = new StringBuilder();

                switch(ddlRelatorio.SelectedValue)
                {
                    case "1"://Clientes
                        parametro.Append("RazaoIni!"+txtInicio.Text);
                        parametro.Append("|RazaoFim!" + txtFim.Text);
                        parametro.Append("|CodIni!" + txtCodIni.Text);
                        parametro.Append("|CodFim!" + txtCodFim.Text);
                        parametro.Append("|UF!" + ddlUF.SelectedValue);
                    strUrl = "relCliente.aspx?";
                    break;
                    case "2":// NotaFiscal

                    parametro.Append("DtIni!" + tbxPeriodoInicial.Text);
                    parametro.Append("|DtFim!" + tbxPeriodoFinal.Text);
                    parametro.Append("|Tipo!" + ddlRegiao.SelectedValue);
                    strUrl = "relNotaFiscal.aspx?";

                        break;

                    case "3"://fornecedor
                         parametro.Append("strRazaoIni!"+txtInicio.Text);
                        parametro.Append("|strRazaoFim!" + txtFim.Text);
                        strUrl = "relFornecedor.aspx?";
                        break;
                    case "4"://transportadora
                        parametro.Append("strRazaoIni!"+txtInicio.Text);
                        parametro.Append("|strRazaoFim!" + txtFim.Text);
                        strUrl="relTransportadora.aspx?";
                        break;

                    case "5"://Produto
                        parametro.Append("Codigo!"+txtCodigo.Text);
                        parametro.Append("|Descricao!" + txtDescricao.Text);
                        parametro.Append("|DtIni!" + tbxPeriodoIni.Text);
                        parametro.Append("|DtFim!" + tbxFinal.Text);
                        strUrl = "relProduto.aspx?";
                        break;

                }
                

                    ExecutarScript(new StringBuilder("AbrirRelatorio('" + strUrl + "','" +parametro.ToString()+ "');"));
            }

        }

    }
}