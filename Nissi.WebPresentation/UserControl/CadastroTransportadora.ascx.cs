#region using
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Nissi.Model;
using Nissi.Business;
using System.Globalization;
using System.Collections.Generic;

#endregion
namespace Nissi.WebPresentation.UserControl
{
    

    public partial class CadastroTransportadora : System.Web.UI.UserControl
    {
        public BasePage basepage = new BasePage();
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { }
        }
        #endregion

        #region Propriedades
        private int _tipoAcao;
        public int TipoAcao
        {
            get { return _tipoAcao; }
            set { _tipoAcao = value; }
        }

        public int _codPessoa;
        public int CodPessoa
        {
            get { return _codPessoa; }
            set { _codPessoa = value; }
        }

                public TransportadoraVO DadosTransportadora    
                {
        set
        {
            txtCelular.Text = value.Celular;
            txtCnpj.Text = basepage.FormatarCnpj(value.CNPJ);
            txtContato.Text = value.Contato;
            txtEmail.Text = value.Email;
            txtFax.Text = value.Fax;
            txtInscEst.Text = value.InscricaoEstadual;
            txtNomeFantasia.Text = value.NomeFantasia;
            txtObservacao.Text = value.Observacao;
            txtRazaoSocial.Text = value.RazaoSocial;
            txtSite.Text = value.Site;
            txtTelefone.Text = value.Telefone;
            Endereco1.CodCep = value.Cep.CodCep;
            Endereco1.CodCidade = value.Cep.Cidade.CodCidade.ToString();
            Endereco1.Complemento = value.Complemento;
            Endereco1.Numero = value.Numero;
        }
        get
        {
            TransportadoraVO identTransportadoraVO = new TransportadoraVO();
            identTransportadoraVO.Celular = txtCelular.Text;
            identTransportadoraVO.Cep.CodCep = Endereco1.CodCep.ToString();
            identTransportadoraVO.CNPJ = txtCnpj.Text;
            identTransportadoraVO.Cep.CodCep = Endereco1.CodCep;
            identTransportadoraVO.Cep.Cidade.CodCidade = Convert.ToInt32(Endereco1.CodCidade);
            identTransportadoraVO.Complemento = Endereco1.Complemento;
            identTransportadoraVO.Numero =   Endereco1.Numero;
            identTransportadoraVO.CNPJ = basepage.FormatarCnpj(txtCnpj.Text);
            identTransportadoraVO.Email = txtEmail.Text;
            identTransportadoraVO.Site = txtSite.Text;
            identTransportadoraVO.Telefone = txtTelefone.Text;
            identTransportadoraVO.RazaoSocial = txtRazaoSocial.Text;
            identTransportadoraVO.NomeFantasia = txtNomeFantasia.Text;

            return identTransportadoraVO;
                }
    }


        #endregion

        #region Métodos



        private void Pesquisar()
        {
            TransportadoraVO identTransportadora = new TransportadoraVO();
            identTransportadora.CodPessoa = CodPessoa > 0 ? CodPessoa : int.MinValue;
            List<TransportadoraVO> lTransportadora = new Transportadora().Listar(identTransportadora);

            if (lTransportadora.Count > 0)
            {
                grdListaResultado.DataSource = lTransportadora;
                grdListaResultado.DataBind();
            }
        }

        private void CadastraTransportadora()
        {
            try
            {
                new Transportadora().Incluir(DadosTransportadora, 1);
            }
            catch(Exception)
            {
                basepage.MensagemCliente("Erro no cadastro da Transportadora.");
            }
 
        }
        #endregion



        #region Métodos da Grid
        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TransportadoraVO tempTransportadora = (TransportadoraVO)e.Row.DataItem;
                //string strIndPesquisa = tempPesquisa.ProcessoApuratorio.CodProcesso.ToString();
                string strCodTranp = tempTransportadora.CodTransportadora.ToString();
                HyperLink lnkRazao = new HyperLink();
                lnkRazao = (HyperLink)e.Row.FindControl("lnkRazao");
                lnkRazao.Text = tempTransportadora.RazaoSocial;
                lnkRazao.ToolTip = "Clique aqui para visualizar os contatos da transportadora.";
                lnkRazao.Attributes.Add("onclick", "ChamaPopup('" + strCodTranp + "');");

                e.Row.Cells[2].Controls.Add(lnkRazao);
                e.Row.Cells[3].Text = tempTransportadora.NomeFantasia;
                e.Row.Cells[4].Text = basepage.FormatarCnpj(tempTransportadora.CNPJ);
                e.Row.Cells[5].Text = tempTransportadora.InscricaoEstadual;

                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";
            }
        }
        #endregion

        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
