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

                ddlRegiao.Items.Add(new ListItem("Todos",""));
                ddlRegiao.Items.Add(new ListItem("Notas Fiscais no Estado","SP"));
                ddlRegiao.Items.Add(new ListItem("Notas Fiscais Fora do Estado","3"));
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
            tdUf1.Style.Add("display", "none");
            tdUf2.Style.Add("display", "none");
            tableProduto.Style.Add("display", "none");
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
                    else
                        if (ddlRelatorio.SelectedValue == "5")
                            tableProduto.Style.Add("display", "block");
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
                string tipo =ddlRelatorio.SelectedValue ;
                string regiao=string.Empty;
                string strInicio = "";
                string strFim = "";
                string codIni = "";
                string codFim = "";


                switch (tipo)
                {
                    case "1":
                        if (rbtCod.Checked)
                        {
                            codIni = txtCodIni.Text;
                            codFim = txtCodFim.Text;
                            regiao = ddlRegiao.SelectedValue;
                            strInicio = tbxPeriodoInicial.Text;
                            strFim = tbxPeriodoFinal.Text;
                        }
                        break;
                    case "2":
                            regiao = ddlRegiao.SelectedValue;
                            strInicio = tbxPeriodoInicial.Text;
                            strFim = tbxPeriodoFinal.Text;
                        break;
                    case "5":

                        if (rbCodigo.Checked)
                        {
                            regiao = txtCodigoDescricao.Text;
                            codIni = string.Empty;
                        }
                        else
                        {
                            codIni = txtCodigoDescricao.Text;
                            regiao = string.Empty;
                        }
                        strInicio = tbxDataIni.Text;
                        strFim = tbxDataFim.Text;
                        break;
                    default:
                            regiao = ddlUF.SelectedValue;
                            strInicio = txtInicio.Text;
                            strFim = txtFim.Text;
                        break;

                }
                    ExecutarScript(new StringBuilder("AbrirRelatorio('" + strInicio + "','" + strFim + "','" + regiao + "','"+tipo+"','"+codIni+"','"+codFim+"');"));
            }

        }


    }
}