using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nissi.Model;
using System.IO;
using System.Threading;
using Nissi.Business;


namespace Nissi.WinFormsApplication
{
    public partial class frmNfeTransmite : Form
    {
        public frmNfeTransmite()
        {
            InitializeComponent();
        }

        class DGVColumnHeader : DataGridViewColumnHeaderCell
        {
            private Rectangle CheckBoxRegion;
            private bool checkAll = false;

            protected override void Paint(Graphics graphics,
                Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                DataGridViewElementStates dataGridViewElementState,
                object value, object formattedValue, string errorText,
                DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
            {

                base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value,
                    formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

                graphics.FillRectangle(new SolidBrush(cellStyle.BackColor), cellBounds);

                CheckBoxRegion = new Rectangle(
                    cellBounds.Location.X + 1,
                    cellBounds.Location.Y + 2,
                    25, cellBounds.Size.Height - 4);


                if (this.checkAll)
                    ControlPaint.DrawCheckBox(graphics, CheckBoxRegion, ButtonState.Checked);
                else
                    ControlPaint.DrawCheckBox(graphics, CheckBoxRegion, ButtonState.Normal);

                Rectangle normalRegion =
                    new Rectangle(
                    cellBounds.Location.X + 1 + 25,
                    cellBounds.Location.Y,
                    cellBounds.Size.Width - 26,
                    cellBounds.Size.Height);

                graphics.DrawString(value.ToString(), cellStyle.Font, new SolidBrush(cellStyle.ForeColor), normalRegion);
            }

            protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
            {
                //Convert the CheckBoxRegion 
                Rectangle rec = new Rectangle(new Point(0, 0), this.CheckBoxRegion.Size);
                this.checkAll = !this.checkAll;
                if (rec.Contains(e.Location))
                {
                    this.DataGridView.Invalidate();
                }
                base.OnMouseClick(e);
            }

            public bool CheckAll
            {
                get { return this.checkAll; }
                set { this.checkAll = value; }
            }
        }
        DGVColumnHeader dgvColumnHeader;
        private void MontarColunas(DataGridView dgvSource, List<NotaFiscalVO> identNotaFiscal)
        {
            dgvNotaFiscal.Columns.Clear();
            //initialize DGVColumnHeader object
             dgvColumnHeader = new DGVColumnHeader();

            //Criando coluna para seleção dos dados;
            DataGridViewCheckBoxColumn colChk = new DataGridViewCheckBoxColumn();
            colChk.DataPropertyName = "Selecionar";
            colChk.HeaderText = "Sel.";
            colChk.Width = 30;
            
            //Criando Coluna da Chave NF-e
            DataGridViewTextBoxColumn colChaveNFe = new DataGridViewTextBoxColumn();
            colChaveNFe.DataPropertyName = "ChaveNFe";
            colChaveNFe.HeaderText = "Chave NF-e";
            colChaveNFe.Width = 280;

            //Criando Coluna do Número da Nota Fiscal
            DataGridViewTextBoxColumn colNF = new DataGridViewTextBoxColumn();
            colNF.DataPropertyName = "NF";
            colNF.HeaderText = "Nota Fiscal";
            colNF.Width = 100;

            //Criando Coluna da Série da Nota Fiscal
            DataGridViewTextBoxColumn colSerie = new DataGridViewTextBoxColumn();
            colSerie.DataPropertyName = "Serie";
            colSerie.HeaderText = "Série";
            colSerie.Width = 45;

            //Criando Coluna de Data de Emissão
            DataGridViewTextBoxColumn colDataEmissao = new DataGridViewTextBoxColumn();
            colDataEmissao.DataPropertyName = "DataEmissao";
            colDataEmissao.HeaderText = "Data Emissão";

            //Criando Coluna do Destinátario
            DataGridViewTextBoxColumn colDestinatario = new DataGridViewTextBoxColumn();
            colDestinatario.DataPropertyName = "RazaoSocial";
            colDestinatario.HeaderText = "Destinatário";
            colDestinatario.Width = 250;

            //Criando Coluna de Status NF-e
            DataGridViewTextBoxColumn colStatusNfe = new DataGridViewTextBoxColumn();
            colStatusNfe.DataPropertyName = "IndStatus";
            colStatusNfe.Name = "IndStatus";
            colStatusNfe.HeaderText = "Status NF-e";
            colStatusNfe.Width = 150;

            //dgvSource.Controls.Add(checkboxHeader);
            //Adicionando ao Grid
            dgvSource.Columns.AddRange(new DataGridViewColumn[] { colChk, colChaveNFe,colNF, colSerie, colDataEmissao, colDestinatario, colStatusNfe });
            dgvSource.Columns[0].HeaderCell = dgvColumnHeader;
            

            dgvSource.DataSource = identNotaFiscal;
            for (int i = 0; i < dgvSource.Columns.Count; i++)
            {
                if (dgvSource.Columns[i].DataPropertyName != "Selecionar" && dgvSource.Columns[i].DataPropertyName != "ChaveNFe" && dgvSource.Columns[i].DataPropertyName != "Serie" && dgvSource.Columns[i].DataPropertyName != "NF" && dgvSource.Columns[i].DataPropertyName != "DataEmissao" && dgvSource.Columns[i].DataPropertyName != "RazaoSocial" && dgvSource.Columns[i].DataPropertyName != "IndStatus")
                dgvSource.Columns[i].Visible = false;
            }
        }

        private void dgvNotaFiscal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            NotaFiscalVO identNotaFiscal = (NotaFiscalVO)dgvNotaFiscal.Rows[e.RowIndex].DataBoundItem;
            if (this.dgvNotaFiscal.Columns[e.ColumnIndex].DataPropertyName == "NF")
            {
                e.Value = identNotaFiscal.NF.ToString().PadLeft(8,'0');
            }
            if (this.dgvNotaFiscal.Columns[e.ColumnIndex].DataPropertyName == "ChaveNFe")
            {
                e.Value = identNotaFiscal.NFe.ChaveNFE;
            }
            if (this.dgvNotaFiscal.Columns[e.ColumnIndex].DataPropertyName == "DataEmissao")
            {
                ShortFormDateFormat(e);
            }
            if (this.dgvNotaFiscal.Columns[e.ColumnIndex].DataPropertyName == "RazaoSocial")
            {
                e.Value = identNotaFiscal.Cliente.RazaoSocial;
            }
            if (this.dgvNotaFiscal.Columns[e.ColumnIndex].DataPropertyName == "IndStatus")
            {
                switch(identNotaFiscal.NFe.IndStatus)
                {
                    case "0":
                        e.Value = "Aguardando Envio";
                        break;
                    case "1":
                        e.Value = "Autorizado o uso da NF-e";
                        break;
                    case "2":
                        e.Value = "Erro no Schema";
                        break;
                    case "3":
                        e.Value = "Cancelado o uso da NF-e";
                        break;
                    default: e.Value = "Aguardando Envio";
                        break;
                }
            }
        }

        //Even though the date internaly stores the year as YYYY, using formatting, the
        //UI can have the format in YY.  
        private static void ShortFormDateFormat(DataGridViewCellFormattingEventArgs formatting)
        {
            if (formatting.Value != null)
            {
                try
                {
                    System.Text.StringBuilder dateString = new System.Text.StringBuilder();
                    DateTime theDate = DateTime.Parse(formatting.Value.ToString());

                    dateString.Append(theDate.Day);
                    dateString.Append("/");
                    dateString.Append(theDate.Month);
                    dateString.Append("/");
                    dateString.Append(theDate.Year.ToString());
                    formatting.Value = dateString.ToString();
                    formatting.FormattingApplied = true;
                }
                catch (FormatException)
                {
                    // Set to false in case there are other handlers interested trying to
                    // format this DataGridViewCellFormattingEventArgs instance.
                    formatting.FormattingApplied = false;
                }
            }
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvNotaFiscal.RowCount; i++)
            {
                dgvNotaFiscal[0, i].Value = ((CheckBox)dgvNotaFiscal.Controls.Find("checkboxHeader", true)[0]).Checked;
            }
            dgvNotaFiscal.EndEdit();
        }

        private void ckbMarcarTodos_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= dgvNotaFiscal.RowCount - 1; i++)
            {
                dgvNotaFiscal[0, i].Value = !(bool)dgvNotaFiscal[0, i].Value; 
            }
        }

        private void gpNotasFiscais_Enter(object sender, EventArgs e)
        {

        }
        nfec.Parametro parametroNfe = new nfec.Parametro();
        Nfe ServiceClient = new Nfe();
        private void frmNfeTransmite_Load(object sender, EventArgs e)
        {
            btnFiltro_Click(sender, e);           
            List<ParametroVO> lstParametro = new Parametro().Listar();
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
            lblAmbiente.Text = parametroNfe.Ambiente == "1" ? "Produção(Oficial)" : "Homologação(Testes)";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            
            NotaFiscalControler controler = new NotaFiscalControler();
            //Seta o progressbar para o valor inicial
            progressBar1.Minimum = 0;
            //Seta o progressbar para o valor máximo
            progressBar1.Maximum = 100;
            //Setando quantidade por passo
            progressBar1.Step = 5;
            progressBar1.PerformStep();
            lblProcesso.Text = "Iniciando processos...";
            this.Update();
            progressBar1.PerformStep();
            lblProcesso.Text = "Verificando WebServices...";
            this.Update();
            //Verifica se o serviço está em operação
            try
            {
                string operacao = controler.StatusServicoNFe(parametroNfe);
                if (operacao == "Serviço em Operação")
                {
                    List<NfeVO> lstNfe = new List<NfeVO>();
                    StringBuilder sbArquivoAssinado = new StringBuilder();
                    bool selecaoNFe = false;
                    for (int i = 0; i < dgvNotaFiscal.RowCount; i++)
                    {
                        NotaFiscalVO identNotaFiscal = (NotaFiscalVO)dgvNotaFiscal.Rows[i].DataBoundItem;
                        //Verifica quais notas fiscais foram selecionadas para gerar e assinar o xml
                        if (dgvNotaFiscal[0, i].Value != null && (bool)dgvNotaFiscal[0, i].Value == true)
                        {
                            progressBar1.Visible = true;
                            progressBar1.PerformStep();
                            lblProcesso.Text = "Gerando arquivos XML...";
                            this.Update();
                            string mensagem = controler.GerarNFe(identNotaFiscal, parametroNfe);
                            if (mensagem.Contains("NFe"))
                            {
                                NfeVO identNFe = new NfeVO();
                                identNFe.ChaveNFE = mensagem.Substring(3, 44);
                                identNFe.CodNF = identNotaFiscal.CodNF;
                                identNFe.IndAmbiente = parametroNfe.Ambiente == "1" ? true : false;
                                identNFe.IndImpressao = false;
                                identNFe.UsuarioInc = 1;
                                identNFe.NF = identNotaFiscal.NF;
                                identNFe.Serie = identNotaFiscal.Serie;
                                identNFe.RazaoSocial = identNotaFiscal.Cliente.RazaoSocial;
                                identNFe.CNPJ = identNotaFiscal.Cliente.CNPJ;
                                identNFe.EnviarEmail = identNotaFiscal.Cliente.EmailNFE;
                                lstNfe.Add(identNFe);
                            }
                            selecaoNFe = true;
                        }
                    }
                    if (selecaoNFe)
                    {
                        //Após a geração e assinatura do xml segue o processo de válidação do arquivo
                        //os arquivos que forem validos, receberam um número de lote
                        string caminho = parametroNfe.PathPrincipal + @"\nfe\arquivos\assinado\";
                        string arquivo = ((parametroNfe.PathPrincipal == string.Empty) ? Application.StartupPath : parametroNfe.PathPrincipal) +
                        "\\nfe\\retornos\\retValidacao.txt";
                        foreach (NfeVO tempNfe in lstNfe)
                        {
                            string chNFe = "NFe" + tempNfe.ChaveNFE;
                            progressBar1.PerformStep();
                            lblProcesso.Text = "Válidando arquivos...";
                            this.Update();
                            string caminhoAssinado = caminho + chNFe + "-assinado.xml";
                            //Verifica a validade do xml
                            if (!new NotaFiscalControler().ValidarNFe(caminhoAssinado, parametroNfe, "nfe_v2.00.xsd"))
                            {
                                StringBuilder sb = new StringBuilder();
                                if (File.Exists(arquivo))
                                {
                                    StreamReader rd = new StreamReader(arquivo);
                                    while (rd.Peek() >= 0)
                                        sb.Append(rd.ReadLine() + "\r\n");
                                    rd.Close();
                                    File.Delete(arquivo);
                                }
                                controler.enviarEmailAnexo("smtp.nissimetal.com.br", "mex868@gmail.com;", caminhoAssinado, "Falha de Válidação", sb);
                                progressBar1.Visible = false;
                                return;
                            }
                            //Se for valido armazena no lstNfe
                            else
                            {
                                progressBar1.PerformStep();
                                lblProcesso.Text = "Arquivo Válido:" + caminhoAssinado;
                                this.Update();
                                sbArquivoAssinado.Append(caminhoAssinado + ";");
                            }

                        }
                        //Gerando arquivo de Lote
                        LoteVO identLote = new LoteVO();
                        identLote.Nfe = lstNfe;
                        int codLote = ServiceClient.GerarLote(identLote);
                        progressBar1.PerformStep();
                        lblProcesso.Text = "Gerando arquivo de Lote...";
                        this.Update();
                        string refStatus = controler.GerarLote(sbArquivoAssinado.ToString(), parametroNfe, codLote);
                        string recibo = string.Empty;

                        progressBar1.PerformStep();
                        lblProcesso.Text = "Enviando arquivo de Lote...";
                        this.Update();
                        //Envia o lote
                        recibo = controler.RecepcaoNFe(refStatus, parametroNfe);
                        NfeVO identNFe = new NfeVO();
                        identNFe.CodNumLote = codLote;
                        if (!string.IsNullOrEmpty(recibo))
                        {
                            string[] recibos = recibo.Split('#');
                            
                            recibo = recibos[1];
                        }
                        else
                        {
                            MessageBox.Show("Falha ao enviar o arquivo de lote!");
                            return;

                        }
                        identNFe.NumRecibo = recibo;
                        ServiceClient.GravarReciboNFe(identNFe);
                        Thread.Sleep(3000);
                        //Faz consulta do lote processado
                        string retorno = controler.RetRecepcaoNfe(recibo, parametroNfe);
                        //Se autorizado gera o arquivo de distribuição.
                        if (retorno.Contains("Autorizado o uso da NF-e"))
                        {
                            progressBar1.PerformStep();
                            lblProcesso.Text = retorno;
                            this.Update();
                            foreach (NfeVO identNfe in lstNfe)
                            {
                                progressBar1.PerformStep();
                                lblProcesso.Text = "Consultando protocolo NF-e...";
                                this.Update();
                                string protocolo = controler.ConsultaNFe(identNfe.ChaveNFE, parametroNfe);
                                if (!string.IsNullOrEmpty(protocolo))
                                {
                                    string[] protocolos = protocolo.Split('#');
                                    protocolo = protocolos[1];
                                }
                                else
                                {
                                    MessageBox.Show("Falha no retorno do número de protocolo!");
                                    return;
                                }
                                controler.DistribuicaoNFe(identNfe.ChaveNFE, recibo, codLote.ToString(), parametroNfe);
                                identNfe.CodNumLote = codLote;
                                identNfe.IndStatus = "1";
                                identNfe.NumRecibo = recibo;
                                identNfe.NumProtocolo = protocolo;
                                progressBar1.PerformStep();
                                lblProcesso.Text = "Gravando número do protocolo: " + protocolo;
                                this.Update();
                                //Grava o status de Autorizado o uso da NF-e
                                ServiceClient.GravarStatusNFe(identNfe);
                                alteraStatusGrid(identNfe);
                                progressBar1.PerformStep();
                                lblProcesso.Text = "Enviando Email do Arquivo XML...";
                                this.Update();

                                //Cria objeto string builder  
                                StringBuilder sbBody = new NotaFiscalControler().getBody(identNfe.Serie, identNfe.NF, identNfe.RazaoSocial, identNfe.CNPJ, identNfe.ChaveNFE, identNfe.NumProtocolo); string caminhoDistribuicao = parametroNfe.PathPrincipal + @"\nfe\arquivos\procNFe\" +
       identNfe.ChaveNFE + "-procNFe.xml";
                                string caminhoPDF = parametroNfe.PathPrincipal + @"\nfe\arquivos\" + identNfe.ChaveNFE + ".pdf";
                                controler.enviarEmailAnexo("smtp.nissimetal.com.br", "nfe@nissimetal.com.br;" + identNfe.EnviarEmail, caminhoDistribuicao, " NFe Nacional", sbBody);
                                progressBar1.Step = 100;
                                progressBar1.PerformStep();
                                lblProcesso.Text = "Imprimindo Danfe...";
                                this.Update();
                                Thread.Sleep(5000);
                                controler.NFeDanfe(caminhoDistribuicao + "|",
                                 caminhoPDF + "|",
                                Convert.ToInt32(parametroNfe.Ambiente),
                                3,
                                false,
                                parametroNfe.PathPrincipal + "|",
                                parametroNfe.TotalizarCfop + "|",
                                parametroNfe.DataPacketFormSeg + "|",
                                parametroNfe.TipoDanfe + "|",
                                parametroNfe.DanfeLogo + "|",
                                parametroNfe.DanfeInfo + "|",
                                parametroNfe.DataPacket + "|"
                                );
                            }

                        }
                        else
                        {
                            controler.enviarEmailAnexo("smtp.nissimetal.com.br", "mex868@gmail.com;", "Falha de Válidação", new StringBuilder(retorno));
                            MessageBox.Show(retorno);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecione pelo menos uma NF para enviar!");
                    }
                    lblProcesso.Text = string.Empty;
                    progressBar1.Visible = false;
                }
                else
                {
                    MessageBox.Show("Serviço não está em operação tente novamente mais tarde!");
                }

            }
            catch (Exception ex)
            {
                controler.enviarEmailAnexo("smtp.nissimetal.com.br", "mex868@gmail.com;", " NFe Nacional - Erro", new StringBuilder(ex.Message));
                MessageBox.Show("Um erro foi encontrado no sistema, um email foi enviado para analise! \nDesulpe-nos pelo incoveniente!");
            }
        }

        private void dgvNotaFiscal_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i < dgvNotaFiscal.Rows.Count; i++)
                {
                    NotaFiscalVO identNotaFiscal = (NotaFiscalVO)this.dgvNotaFiscal.Rows[i].DataBoundItem;
                    if (identNotaFiscal.NFe.IndStatus != "1")
                    {
                        dgvNotaFiscal.Rows[i].Cells[0].Value = dgvColumnHeader.CheckAll;
                    }
                }
            }

        }

        private void dgvNotaFiscal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex != -1)
                {
                    NotaFiscalVO identNotaFiscal = (NotaFiscalVO)this.dgvNotaFiscal.Rows[e.RowIndex].DataBoundItem;
                    if (identNotaFiscal.NFe.IndStatus != "1")
                    {
                        if (dgvColumnHeader.CheckAll == true)
                        {
                            //Escalate Editmode
                            this.dgvNotaFiscal.EndEdit();
                            string re_value = this.dgvNotaFiscal.Rows[e.RowIndex].Cells[0].EditedFormattedValue.ToString();
                            this.dgvNotaFiscal.Rows[e.RowIndex].Cells[0].Value = "true";
                        }
                    }
                    else
                    {
                        this.dgvNotaFiscal.EndEdit();
                    }
                }
            }
        }
        private void alteraStatusGrid(NfeVO identNFe)
        {
            List<NotaFiscalVO> lstNotaFiscal = new List<NotaFiscalVO>();
            for (int i = 0; i < dgvNotaFiscal.Rows.Count; i++)
            {
                NotaFiscalVO identNotaFiscal = (NotaFiscalVO)dgvNotaFiscal.Rows[i].DataBoundItem;
                if (identNotaFiscal.CodNF == identNFe.CodNF)
                {
                    identNotaFiscal.NFe.IndStatus = "1";
                    identNotaFiscal.NFe.ChaveNFE = identNFe.ChaveNFE;
                    identNotaFiscal.NFe.NumProtocolo = identNFe.NumProtocolo;

                }
               lstNotaFiscal.Add(identNotaFiscal);
            }
            dgvNotaFiscal.DataSource = lstNotaFiscal;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NotaFiscalVO identNotaFiscal = (NotaFiscalVO)dgvNotaFiscal.CurrentRow.DataBoundItem;
            new NotaFiscalControler().NFeDanfe(parametroNfe.PathPrincipal + @"\nfe\arquivos\procNFe\" +
            identNotaFiscal.NFe.ChaveNFE + "-procNFe.xml" + "|",
            parametroNfe.PathPrincipal + @"\nfe\arquivos\" + identNotaFiscal.NFe.ChaveNFE + ".pdf" + "|",
            Convert.ToInt32(parametroNfe.Ambiente),
            3,
            false,
            parametroNfe.PathPrincipal + "|",
            parametroNfe.TotalizarCfop + "|",
            parametroNfe.DataPacketFormSeg + "|",
            parametroNfe.TipoDanfe + "|",
            parametroNfe.DanfeLogo + "|",
            parametroNfe.DanfeInfo + "|",
            parametroNfe.DataPacket + "|"
            );
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           string status =  new nfec.nfecsharp().NfeStatusServico(parametroNfe);
           MessageBox.Show(status); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmNFeInutilizar inutilizar = new frmNFeInutilizar(parametroNfe);
            inutilizar.Show();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            NotaFiscalVO identNotaFiscal = (NotaFiscalVO)dgvNotaFiscal.CurrentRow.DataBoundItem;
            if (identNotaFiscal.NFe.IndStatus == "1")
            {
                NfeVO identNFe = identNotaFiscal.NFe;
                frmNFeCancelar cancelar = new frmNFeCancelar(identNFe, parametroNfe);
                cancelar.Show();
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            NotaFiscalVO identNotaFiscal = new NotaFiscalVO();
            if (ckbData.Checked)
                identNotaFiscal.DataEmissao = dtpData.Value;
            if (rbNaoEnviada.Checked)
            {
                //Notas Fiscais não enviadas
                identNotaFiscal.NFe.IndStatus = "0";
            }
            else
                if (rbTransmitida.Checked)
                {
                    //Notas Fiscais Transmitidas
                    identNotaFiscal.NFe.IndStatus = "1";
                }
                else
                    if (rbCancelada.Checked)
                    {
                        //Notas Fiscais Canceladas
                        identNotaFiscal.NFe.IndStatus = "3";
                    }
                    else
                    {
                        //Notas Fiscais com erro no XML
                        identNotaFiscal.NFe.IndStatus = "2";
                    }
            MontarColunas(dgvNotaFiscal, new NotaFiscal().ListarTudo(identNotaFiscal));
        }

        private void ckbData_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbData.Checked)
            {
                dtpData.Enabled = true;
            }
            else
            {
                dtpData.Enabled = false;
            }
        }

        private void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            NotaFiscalVO identNotaFiscal = (NotaFiscalVO)dgvNotaFiscal.CurrentRow.DataBoundItem;
            if (!string.IsNullOrEmpty(identNotaFiscal.NFe.ChaveNFE))
            {
                //Cria objeto string builder  
                StringBuilder sbBody = new NotaFiscalControler().getBody(identNotaFiscal.Serie, identNotaFiscal.NF, identNotaFiscal.Cliente.RazaoSocial, identNotaFiscal.Cliente.CNPJ, identNotaFiscal.NFe.ChaveNFE, identNotaFiscal.NFe.NumProtocolo);
                string caminhoDistribuicao = parametroNfe.PathPrincipal + @"\nfe\arquivos\procNFe\" +
    identNotaFiscal.NFe.ChaveNFE + "-procNFe.xml";
                new NotaFiscalControler().enviarEmailAnexo("smtp.nissimetal.com.br", "nfe@nissimetal.com.br;" + identNotaFiscal.Cliente.EmailNFE, caminhoDistribuicao, " NFe Nacional", sbBody);
                MessageBox.Show("Email Enviado com sucesso!");
            }
        }

        private void btnDistribuicao_Click(object sender, EventArgs e)
        {
            NotaFiscalVO identNotaFiscal = (NotaFiscalVO)dgvNotaFiscal.CurrentRow.DataBoundItem;
            if (identNotaFiscal.NFe.CodNumLote > 0)
            {
            string retorno = new NotaFiscalControler().DistribuicaoNFe(identNotaFiscal.NFe.ChaveNFE, identNotaFiscal.NFe.NumRecibo, identNotaFiscal.NFe.CodNumLote.ToString(), parametroNfe);
            MessageBox.Show(retorno);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NotaFiscalVO identNotaFiscal = (NotaFiscalVO)dgvNotaFiscal.CurrentRow.DataBoundItem;
            new NotaFiscalControler().NFeDanfe(parametroNfe.PathPrincipal + @"\nfe\arquivos\procNFe\" +
            identNotaFiscal.NFe.ChaveNFE + "-procNFe.xml" + "|",
            parametroNfe.PathPrincipal + @"\nfe\arquivos\pdf\" + identNotaFiscal.NFe.ChaveNFE + ".pdf" + "|",
            Convert.ToInt32(parametroNfe.Ambiente),
            2,
            false,
            parametroNfe.PathPrincipal + "|",
            parametroNfe.TotalizarCfop + "|",
            parametroNfe.DataPacketFormSeg + "|",
            parametroNfe.TipoDanfe + "|",
            parametroNfe.DanfeLogo + "|",
            parametroNfe.DanfeInfo + "|",
            parametroNfe.DataPacket + "|"
            );
        }
    }
}
