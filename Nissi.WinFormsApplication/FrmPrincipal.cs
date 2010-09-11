using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Nissi.WinFormsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void parâmetrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void nFeNascionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNfeTransmite nfeTransmite = new frmNfeTransmite();
            nfeTransmite.ShowDialog();
        }

    }
}
