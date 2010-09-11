using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nissi.Model;

namespace Nissi.WinFormsApplication
{
    public partial class frmNfeParametros : Form
    {
        public frmNfeParametros()
        {
            InitializeComponent();
        }
        #region Propriedades
        public ParametroVO  DadosParametros
        {
            set{
                    //Parametros
                    modeloTextBox.Text = value.Modelo;
                    serieComboBox.SelectedValue = value.Serie;
                    ambienteComboBox.SelectedValue = value.Ambiente;
                    tipoDanfeComboBox.SelectedValue = value.TipoDanfe;
                    pathPrincipalTextBox.Text = value.PathPrincipal;
                    dataPacketTextBox.Text = value.DataPacket;
                    schemasTextBox.Text = value.Schemas;
                    dataPacketFormSegTextBox.Text = value.DataPacketFormSeg;
                    noSerieCertificadoTextBox.Text = value.NoSerieCertificado;
                    ativaTraceComboBox.SelectedValue = value.AtivaTrace;
                    verProcTextBox.Text = value.VerProc;
                    //Web Services
                    nFeRecepcaoTextBox.Text = value.NFeRecepcao;
                    nFeRetRecepcaoTextBox.Text = value.NFeRetRecepcao;
                    nFeCancelamentoTextBox.Text = value.NFeCancelamento;
                    nFeInutilizacaoTextBox.Text = value.NFeInutilizacao;
                    nFeConsultaProtocoloTextBox.Text = value.NFeConsultaProtocolo;
                    nFeStatusServicoTextBox.Text = value.NFeStatusServico;
                    totalizarCfopCheckBox.Checked = value.TotalizarCfop == "1"?true:false;
                }
            get{
                    ParametroVO identParametros = new ParametroVO();
                    //Parametros
                    identParametros.Modelo = modeloTextBox.Text;
                    identParametros.Serie = serieComboBox.SelectedItem.ToString();
                    identParametros.Ambiente =ambienteComboBox.SelectedValue.ToString();
                    identParametros.TipoDanfe = tipoDanfeComboBox.SelectedValue.ToString();
                    identParametros.PathPrincipal = pathPrincipalTextBox.Text;
                    identParametros.DataPacket = dataPacketTextBox.Text;
                    identParametros.Schemas = schemasTextBox.Text;
                    identParametros.DataPacketFormSeg = dataPacketFormSegTextBox.Text;
                    identParametros.NoSerieCertificado = noSerieCertificadoTextBox.Text;
                    identParametros.AtivaTrace = ativaTraceComboBox.SelectedItem.ToString();
                    identParametros.VerProc = verProcTextBox.Text;
                    identParametros.DanfeInfo = danfeInfoTextBox.Text;
                    //Web Services
                    identParametros.NFeRecepcao = nFeRecepcaoTextBox.Text;
                    identParametros.NFeRetRecepcao = nFeRetRecepcaoTextBox.Text;
                    identParametros.NFeCancelamento = nFeCancelamentoTextBox.Text;
                    identParametros.NFeInutilizacao = nFeInutilizacaoTextBox.Text;
                    identParametros.NFeConsultaProtocolo = nFeConsultaProtocoloTextBox.Text;
                    identParametros.NFeStatusServico = nFeStatusServicoTextBox.Text;
                    identParametros.TotalizarCfop = totalizarCfopCheckBox.Checked == true ? "1" : "0";
                    return identParametros;
                }
        }
        #endregion
        public enum modificar { incluir = 0, alterar = 1, excluir = 2 };
        public modificar status
        {
            get;
            set;
        }
        ServiceReference1.Service1Client ServiceClient = new ServiceReference1.Service1Client();
        private void frmNfeParametros_Load(object sender, EventArgs e)
        {
            ParametroVO[] lstParametro = ServiceClient.ListarParametro();
            ambienteComboBox.DataSource = Ambiente.GetListaAmbiente();
            ambienteComboBox.ValueMember = "Codigo";
            ambienteComboBox.DisplayMember = "Descricao";
            tipoDanfeComboBox.DataSource = TipoDanfe.GetListaTipoDanfe();
            tipoDanfeComboBox.ValueMember = "Codigo";
            tipoDanfeComboBox.DisplayMember = "Descricao";
            if (lstParametro.Count() != 0)
            {
                DadosParametros = lstParametro[0];
                status = modificar.alterar;
            }
            else
                status = modificar.incluir;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            switch (status)
            {
                case modificar.incluir:
                    ServiceClient.IncluirParametro(DadosParametros);
                    break;
                case modificar.alterar:
                    ServiceClient.AlterarParametro(DadosParametros);
                    break;
                case modificar.excluir:
                    ServiceClient.ExcluirParametro();
                    break;
            }
            this.Close();
        }


        private void btnPathPrincipal_Click(object sender, EventArgs e)
        {
            fdbParametros.Description = "Selecione uma pasta";
            fdbParametros.RootFolder = Environment.SpecialFolder.MyComputer;
            fdbParametros.ShowNewFolderButton = true;
            if (fdbParametros.ShowDialog() == DialogResult.OK)
            {
                pathPrincipalTextBox.Text = fdbParametros.SelectedPath;
            }
        }

        private void btnDataPacket_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\"; 
            ofd.Filter = "DataPacket (*..xtr)|*.txt|Todos Arquivos (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dataPacketTextBox.Text = ofd.FileName;
            }
        }

        private void btnSchemas_Click(object sender, EventArgs e)
        {
            fdbParametros.Description = "Selecione uma pasta";
            fdbParametros.RootFolder = Environment.SpecialFolder.MyComputer;
            fdbParametros.ShowNewFolderButton = true;
            if (fdbParametros.ShowDialog() == DialogResult.OK)
            {
                schemasTextBox.Text = fdbParametros.SelectedPath;
            }
        }

        private void btnDataPacketFormSeg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "DataPacketFormSeg (*..xtr)|*.txt|Todos Arquivos (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dataPacketFormSegTextBox.Text = ofd.FileName;
            }
        }

        private void ambienteComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ambienteComboBox.SelectedValue != null && ambienteComboBox.SelectedValue.ToString() == "1")
            {
                nFeRecepcaoTextBox.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nferecepcao2.asmx";
                nFeRetRecepcaoTextBox.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nferetrecepcao2.asmx";
                nFeCancelamentoTextBox.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfecancelamento2.asmx";
                nFeInutilizacaoTextBox.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeinutilizacao2.asmx";
                nFeConsultaProtocoloTextBox.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta2.asmx";
                nFeStatusServicoTextBox.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfestatusservico2.asmx";
            }
            else
            {
                nFeRecepcaoTextBox.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nferecepcao.asmx";
                nFeRetRecepcaoTextBox.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nferetrecepcao.asmx"; 
                nFeCancelamentoTextBox.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfecancelamento.asmx"; 
                nFeInutilizacaoTextBox.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfeinutilizacao.asmx"; 
                nFeConsultaProtocoloTextBox.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta.asmx"; 
                nFeStatusServicoTextBox.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfestatusservico.asmx"; 
            }
        }
    }

    public class Ambiente
    {
        public Ambiente(int codigoAmbiente, string descricaoAmbiente)
        {
            Codigo = codigoAmbiente;
            Descricao = descricaoAmbiente;
        }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        public static List<Ambiente> GetListaAmbiente()
        {
            List<Ambiente> listaAmbiente = new List<Ambiente>();
            listaAmbiente.Add(new Ambiente(1, "Produção(Oficial)"));
            listaAmbiente.Add(new Ambiente(2, "Homologação(Testes)"));
            return listaAmbiente;
        }
    }

    public class TipoDanfe
    {
        public TipoDanfe(int codigoTipoDanfe, string descricaoTipoDanfe)
        {
            Codigo = codigoTipoDanfe;
            Descricao = descricaoTipoDanfe;
        }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        public static List<TipoDanfe> GetListaTipoDanfe()
        {
            List<TipoDanfe> listaTipoDanfe = new List<TipoDanfe>();
            listaTipoDanfe.Add(new TipoDanfe(1, "Retrato"));
            listaTipoDanfe.Add(new TipoDanfe(2, "Paisagem"));
            return listaTipoDanfe;
        }
    }

}
