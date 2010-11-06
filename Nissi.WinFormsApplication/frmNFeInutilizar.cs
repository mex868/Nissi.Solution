using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nissi.WinFormsApplication
{
    public partial class frmNFeInutilizar : Form
    {
        public frmNFeInutilizar()
        {
            InitializeComponent();
        }
        
        nfec.Parametro parametroNfe = new nfec.Parametro();
        public frmNFeInutilizar(nfec.Parametro tempParametro) 
        {
            InitializeComponent();
            parametroNfe = tempParametro;
        }

        private void btnInutilizar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.AppStarting; ;

            btnInutilizar.Enabled = false;

            nfec.nfecsharp nfe = new nfec.nfecsharp();
            string retorno = nfe.NfeInutilizacao(
                tbxAno.Text,
                tbxNumInicial.Text,
                tbxNumFinal.Text,
                tbxJustificativa.Text, parametroNfe);
            MessageBox.Show("Inutilizado com sucesso Nº protocolo: " + retorno);
            btnInutilizar.Enabled = true;
            Cursor = Cursors.Default;
            this.Close();
        }

        private void tbxAno_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxAno.Text))
            {
                erroValidation.SetError(this.tbxAno, "Digite um Ano");
                e.Cancel = true;
            }
            else
            {
                erroValidation.SetError(this.tbxAno, "");
            }
        }

        private void tbxNumInicial_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxNumInicial.Text))
            {
                erroValidation.SetError(this.tbxNumInicial, "Digite o Número Inicial");
                e.Cancel = true;
            }
            else
            {
                erroValidation.SetError(this.tbxNumInicial, "");
            }

        }

        private void tbxNumFinal_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxNumFinal.Text))
            {
                erroValidation.SetError(this.tbxNumFinal, "Digite o Número Inicial");
                e.Cancel = true;
            }
            else
            {
                erroValidation.SetError(this.tbxNumFinal, "");
            }
        }

        private void tbxJustificativa_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxJustificativa.Text) && (tbxJustificativa.MaxLength > 14))
            {
                erroValidation.SetError(this.tbxJustificativa, "Justificativa tem que ter no mínimo 15 caracteres!");
                e.Cancel = true;
            }
            else
            {
                erroValidation.SetError(this.tbxNumFinal, "");
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
