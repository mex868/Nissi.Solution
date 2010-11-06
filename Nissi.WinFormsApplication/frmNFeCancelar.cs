using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nissi.Model;
using Nissi.Business;

namespace Nissi.WinFormsApplication
{
    public partial class frmNFeCancelar : Form
    {
        public frmNFeCancelar()
        {
            InitializeComponent();
        }

        NfeVO identNFe = new NfeVO();
        nfec.Parametro parametroNfe = new nfec.Parametro();
        Nfe ServiceClient = new Nfe();
        public frmNFeCancelar(NfeVO tempNFe, nfec.Parametro tempParametro) 
        {
            InitializeComponent();
            identNFe = tempNFe;
            parametroNfe = tempParametro;
            txtNFe.Text = identNFe.ChaveNFE;
            txtProtocolo.Text = identNFe.NumProtocolo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJustificativa.Text) && (txtJustificativa.TextLength > 14))
            {
                NotaFiscalControler controler = new NotaFiscalControler();
                string retorno = controler.CancelamentoNFe(identNFe.ChaveNFE, identNFe.NumProtocolo, txtJustificativa.Text, parametroNfe);
                if (retorno.Contains("#"))
                {
                    string[] retornos = retorno.Split('#');
                    retorno = retornos[1];
                    identNFe.NumProtocolo = retorno;
                    identNFe.IndStatus = "3";
                    ServiceClient.GravarStatusNFe(identNFe);
                    MessageBox.Show("Cancelado com sucesso Nº protocolo: "+retorno);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(retorno);
                }
            }
            else
            {
                MessageBox.Show("Justificativa tem que ter no mínimo 15 caracteres!");
            }
        }

        private void txtJustificativa_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblContar.Text = txtJustificativa.TextLength.ToString(); 
        }
    }
}
