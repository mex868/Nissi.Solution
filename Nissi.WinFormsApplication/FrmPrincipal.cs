using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using nfec;
using Nissi.Model;
using Nissi.Business;

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
        nfec.Parametro parametroNfe = new nfec.Parametro();
        private void Form1_Load(object sender, EventArgs e)
        {
            List<ParametroVO> lstParametro = new Nissi.Business.Parametro().Listar();
            foreach (ParametroVO identParametro in lstParametro)
            {
                parametroNfe.Ambiente = identParametro.Ambiente;
                parametroNfe.AtivaTrace = identParametro.AtivaTrace;
                parametroNfe.DanfeInfo = identParametro.DanfeInfo;
                parametroNfe.DanfeLogo = identParametro.DanfeLogo;
                parametroNfe.DataPacket = identParametro.DataPacket;
                parametroNfe.DataPacketFormSeg = identParametro.DataPacketFormSeg;
                parametroNfe.Modelo = identParametro.Modelo;
                parametroNfe.NFeCancelamento = identParametro.NFeCancelamento;
                parametroNfe.NFeConsultaProtocolo = identParametro.NFeConsultaProtocolo;
                parametroNfe.NFeInutilizacao = identParametro.NFeInutilizacao;
                parametroNfe.NFeRecepcao = identParametro.NFeRecepcao;
                parametroNfe.NFeRetRecepcao = identParametro.NFeRetRecepcao;
                parametroNfe.NFeStatusServico = identParametro.NFeStatusServico;
                parametroNfe.NoSerieCertificado = identParametro.NoSerieCertificado;
                parametroNfe.PathPrincipal = identParametro.PathPrincipal;
                parametroNfe.Schemas = identParametro.Schemas;
                parametroNfe.Serie = identParametro.Serie;
                parametroNfe.TipoDanfe = identParametro.TipoDanfe;
                parametroNfe.TotalizarCfop = identParametro.TotalizarCfop;
                parametroNfe.VerProc = identParametro.VerProc;
                parametroNfe.CNPJ = identParametro.CNPJ;
                parametroNfe.UnidadeFederada = identParametro.UnidadeFederada;
            }
            //lblAmbiente.Text = parametroNfe.Ambiente == "1" ? "Produção(Oficial)" : "Homologação(Testes)";
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

    }
}
