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
        ServiceReference1.Service1Client ServiceClient = new ServiceReference1.Service1Client();
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

            btnInutilizar.Enabled = true;
            Cursor = Cursors.Default;	
        }
    }
}
