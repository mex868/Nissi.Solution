using System;
using System.Web.UI.WebControls;
using Nissi.Model;

namespace Nissi.WebPresentation.UserControl
{
    public partial class Banco : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaComboTipoConta();
                CarregaComboNumBanco();
            }
        }
        #region Propriedades

        public BancoVO DadosBanco
        {
            set {
                txtAgencia.Text = value.Agencia.ToString();
                if (value.NumConta != null)
                    txtConta.Text = value.NumConta.ToString();
                if (value.CodBanco != null)
                {
                    ddlBanco.SelectedValue = value.CodBanco.ToString();
                    ddlNumBanco.SelectedValue = value.CodBanco.ToString();
                }
                ddlTipoConta.SelectedValue =Convert.ToString(value.TipoConta ? 1 : 0);
            }
            get
            {
                BancoVO identBanco = new BancoVO();
                if (!string.IsNullOrEmpty(ddlTipoConta.SelectedValue))
                {
                    if (ddlTipoConta.SelectedValue == "0")
                        identBanco.TipoConta = false;
                    else
                        identBanco.TipoConta = true;
                }

                if (!string.IsNullOrEmpty(ddlBanco.SelectedValue))
                    identBanco.CodBanco = Convert.ToInt32(ddlBanco.SelectedValue);

                identBanco.NumConta = txtConta.Text;
                if (!string.IsNullOrEmpty(txtAgencia.Text))
                    identBanco.Agencia = Convert.ToInt32(txtAgencia.Text);

                return identBanco;
            }
        }

        #endregion

        #region Carregar Combos
        private void CarregaComboTipoConta()
        {
            ddlBanco.DataSource = new Nissi.Business.Banco().Listar();
            ddlBanco.DataTextField = "Banco";
            ddlBanco.DataValueField = "CodBanco";
            ddlBanco.DataBind();
            ddlBanco.Items.Insert(0, new ListItem());
        }
        private void CarregaComboNumBanco()
        {
            ddlNumBanco.DataSource = new Nissi.Business.Banco().Listar();
            ddlNumBanco.DataTextField = "CodCompensacao";
            ddlNumBanco.DataValueField = "CodBanco";
            ddlNumBanco.DataBind();
            ddlNumBanco.Items.Insert(0, new ListItem());
        }
        public void LimparCampos()
        {
            txtConta.Text              =
            txtAgencia.Text            ="";
            ddlBanco.SelectedIndex     = 
            ddlNumBanco.SelectedIndex  =
            ddlTipoConta.SelectedIndex = 0;

        }

        #endregion

        protected void ddlNumBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBanco.SelectedValue = ddlNumBanco.SelectedValue;
        }

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlNumBanco.SelectedValue = ddlBanco.SelectedValue;
        }
    }
}