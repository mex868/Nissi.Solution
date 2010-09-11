using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Model;
using Nissi.Business;
using Nissi.WebPresentation.WSCEPServices1;
using System.Collections;




namespace Nissi.WebPresentation.UserControl
{
    public partial class Endereco : System.Web.UI.UserControl
    {
        private BasePage basepage = new BasePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                preencherUF();

            }
            else 
            {
                HabitarCampos(false);

            }
 
        }
        public string CodCep
        {
            get
            {
                string codCep = null;
                if (ViewState["codCep"] != null)
                    codCep = ViewState["codCep"].ToString();
                if (codCep != null)
                    return codCep;

                return string.Empty;
            }
            set
            {
                ViewState["codCep"] = value;
            }
        }
        public string CodCidade
        {
            get
            {
                string codCidade = ViewState["codCidade"] as string;
                if (codCidade != null)
                    return codCidade;

                return string.Empty;
            }
            set
            {
                ViewState["codCidade"] = value;
            }
        }
        public string CodUF
        {
            get
            {
                string CodUF = ViewState["codUF"] as string;
                if (CodUF != null)
                    return CodUF;
                return string.Empty;
            }
            set
            {
                ViewState["codUF"] = value;
            }
        }
        public string Logradouro
        {
            get { return txtEndereco.Text; }
            set { txtEndereco.Text = value; }
        }
        public string Complemento
        {
            get { return txtComplemento.Text; }
            set { txtComplemento.Text = value; }
        }
        public string Numero
        {
            get { return txtNumero.Text; }
            set { txtNumero.Text = value; }
 
        }

        #region Carrega Combos
        private void preencherUF()
        {
            ddlUF.DataSource = new UnidadeFederacao().Listar();
            ddlUF.DataTextField = "NomUF";
            ddlUF.DataValueField = "CodUF";
            ddlUF.DataBind();
        }

        private void PreencherTipoLogradouro()
        {
            ddlTipoLogradouro.DataSource = new TipoLogradouro().Listar();
            ddlTipoLogradouro.DataTextField = "NomTipoLogradouro";
            ddlTipoLogradouro.DataValueField = "CodNomTipoLogradouro";
            ddlTipoLogradouro.DataBind();
        }
        #endregion
        public void limpaCampos()
        {
            txtCep.Text         = 
            txtComplemento.Text = 
            txtNumero.Text      =
            txtEndereco.Text    =
            txtBairro.Text      = 
            txtNumero.Text      = 
            txtComplemento.Text = string.Empty;
        }
        #region Métodos Auxiliares
        private void HabitarCampos(bool habilitar)
        {
            string tipo = habilitar ? "inline" : "none";
           tdLabel.Style.Add("display", tipo);
            tdBotaoIncluirCEP.Style.Add("display", tipo);
            tdDLLTipo.Style.Add("display", tipo);
            tdTipo.Style.Add("display", tipo);
            txtCep.Enabled = !habilitar;
            if (habilitar)
            {
                lblInfornacao.Text = "**CEP não encontrado, Favor incluir o endereço e clicar no botão INCLUIR.**";
                PreencherTipoLogradouro();
            }
        }
#endregion


        #region Localizar
        protected void lkbLocalizar_Click(object sender, EventArgs e)
        {
            HabitarCampos(false);
            CEPVO identCEP = new CEPVO();
            string strCEP = txtCep.Text.Replace("-", "");
            identCEP.CodCep = strCEP;
            identCEP.NomEndereco = new Fonetica().Gerar(txtEndereco.Text);
            ArrayList strArray = new ArrayList();

            if (!string.IsNullOrEmpty(CodCidade))
                identCEP.Cidade.CodCidade = Convert.ToInt32(CodCidade);

            if (!string.IsNullOrEmpty(txtCep.Text))
            {
                identCEP = new CEP().Listar(identCEP);

                //Caso não encontre o CEP informado procura no WebServices
                if (string.IsNullOrEmpty(identCEP.NomEndereco))
                {
                    strArray = PesquisaCepWebServices(strCEP);
                    if (strArray.Count > 0)
                    {
                        preencheCep(new CEPVO(), strArray);

                    }
                    else
                    {
                        HabitarCampos(true);
                    }
                }
                else
                {
                    preencheCep(identCEP, strArray);
                }
            }
        }

        private ArrayList PesquisaCepWebServices(string strCEP)
        {
            WSCEPServices1.Service ws = new Nissi.WebPresentation.WSCEPServices1.Service();
            WSCEPServices1.Logradouro[] ruas = ws.getLogradouro(strCEP);
            ArrayList strArray = new ArrayList();

            if (ruas != null)
            {
                if (ruas.Length > 0)
                { 
                    WSCEPServices1.Logradouro rua = ruas[0];
                    strArray.Add(rua.Endereco);
                    strArray.Add(rua.CEP.Substring(0, 5) + "-" + rua.CEP.Substring(5, 3));
                    strArray.Add(rua.Bairro);
                    strArray.Add(rua.UF);
                    strArray.Add(rua.Cidade);
                }
            }
            return strArray;
        }

        #endregion
        #region preencheCep
        public void preencheCep(CEPVO identCEP,ArrayList strArray)
        {

            if (!string.IsNullOrEmpty(identCEP.CodCep))
            {
                txtCep.Text             = identCEP.CodCep.Substring(0, 5) + "-" + identCEP.CodCep.Substring(5, 3);
                this.CodCep             = identCEP.CodCep;
                this.CodCidade          = identCEP.Cidade.CodCidade.ToString();
                this.CodUF              = identCEP.Cidade.UF.CodUF;
                txtEndereco.Text        = identCEP.NomEndereco;
                txtBairro.Text          = identCEP.Bairro.NomBairro;
                ddlUF.SelectedValue = identCEP.Cidade.UF.CodUF;
                cddCidade.SelectedValue = identCEP.Cidade.CodCidade.ToString();
            }
            else if (strArray.Count > 0)
            {
                txtEndereco.Text        = strArray[0].ToString();
                txtCep.Text             = strArray[1].ToString();
                txtBairro.Text          = strArray[2].ToString();
                ddlUF.SelectedItem.Text = strArray[3].ToString();
                cddCidade.SelectedValue = strArray[4].ToString();
                this.CodCep             = strArray[1].ToString();
                this.CodCidade          = cddCidade.SelectedValue;
                this.CodUF = ddlUF.SelectedValue;

            }


        }
        #endregion
        #region Métodos do Grid Cep
        protected void grdListaCep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void grdListaCep_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Selecionar")
            {
                string[] dados = e.CommandArgument.ToString().Split('|');
                CEPVO identCEP = new CEPVO();
                identCEP.CodCep = dados[0].ToString();
                identCEP.NomEndereco = dados[1].ToString();
                identCEP.Bairro.NomBairro = dados[2].ToString();
                identCEP.Cidade.CodCidade = Convert.ToInt32(dados[3].ToString());
                identCEP.Cidade.NomCidade = dados[4].ToString();
                identCEP.Cidade.UF.CodUF = dados[5].ToString();
                preencheCep(identCEP, new ArrayList()); 
            }
        }
        protected void grdListaCep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CEPVO identCEP = (CEPVO)e.Row.DataItem;
                #region Selecionar
                LinkButton link = (LinkButton)e.Row.FindControl("lkbCep");
                link.CommandName = "Selecionar";
                link.CommandArgument = identCEP.CodCep + "|" + identCEP.NomEndereco + "|" + identCEP.Bairro.NomBairro + "|" + identCEP.Cidade.CodCidade + "|" + identCEP.Cidade.NomCidade + "|" + identCEP.Cidade.UF.CodUF;
                link.Text = identCEP.CodCep;
                #endregion
                e.Row.Cells[1].Text = identCEP.NomEndereco;
                e.Row.Cells[2].Text = identCEP.Bairro.NomBairro;
                e.Row.Cells[3].Text = identCEP.Cidade.NomCidade;
                e.Row.Cells[5].Text = identCEP.Cidade.UF.CodUF;

                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";
            }
        }

        #endregion

        protected void btnIncluirCep_Click(object sender, EventArgs e)
        {

            CEPVO identCEP = new CEPVO();
            identCEP.NomEndereco = txtEndereco.Text;
            identCEP.Bairro.NomBairro = txtBairro.Text;
            identCEP.Bairro.CodBairro = Convert.ToInt32(cddCidade.SelectedValue.Substring(0, 4));
            identCEP.Cidade.CodCidade = Convert.ToInt32(ddlCidade.SelectedValue);
            identCEP.Cidade.UF.CodUF = ddlUF.SelectedValue;
            identCEP.TipoLogradouro.NomTipoLogradouro = ddlTipoLogradouro.SelectedItem.Text;
            identCEP.CodCep = txtCep.Text.Replace("-", "");
            new CEP().Incluir(identCEP);
            preencheCep(identCEP,new ArrayList());
            HabitarCampos(false);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            HabitarCampos(false);
        }
    }
}