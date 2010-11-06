namespace Nissi.WinFormsApplication
{
    partial class frmNfeTransmite
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gpNotasFiscais = new System.Windows.Forms.GroupBox();
            this.lblAmbiente = new System.Windows.Forms.Label();
            this.dgvNotaFiscal = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblProcesso = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.ckbData = new System.Windows.Forms.CheckBox();
            this.btnFiltro = new System.Windows.Forms.Button();
            this.rbErro = new System.Windows.Forms.RadioButton();
            this.rbCancelada = new System.Windows.Forms.RadioButton();
            this.rbTransmitida = new System.Windows.Forms.RadioButton();
            this.rbNaoEnviada = new System.Windows.Forms.RadioButton();
            this.btnEnviarEmail = new System.Windows.Forms.Button();
            this.btnDistribuicao = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.gpNotasFiscais.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotaFiscal)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpNotasFiscais
            // 
            this.gpNotasFiscais.Controls.Add(this.lblAmbiente);
            this.gpNotasFiscais.Controls.Add(this.dgvNotaFiscal);
            this.gpNotasFiscais.Location = new System.Drawing.Point(1, 80);
            this.gpNotasFiscais.Name = "gpNotasFiscais";
            this.gpNotasFiscais.Size = new System.Drawing.Size(931, 239);
            this.gpNotasFiscais.TabIndex = 0;
            this.gpNotasFiscais.TabStop = false;
            this.gpNotasFiscais.Text = "Notas Fiscais";
            this.gpNotasFiscais.Enter += new System.EventHandler(this.gpNotasFiscais_Enter);
            // 
            // lblAmbiente
            // 
            this.lblAmbiente.AutoSize = true;
            this.lblAmbiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmbiente.ForeColor = System.Drawing.Color.Red;
            this.lblAmbiente.Location = new System.Drawing.Point(6, 216);
            this.lblAmbiente.Name = "lblAmbiente";
            this.lblAmbiente.Size = new System.Drawing.Size(85, 20);
            this.lblAmbiente.TabIndex = 2;
            this.lblAmbiente.Text = "Ambiente";
            this.lblAmbiente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvNotaFiscal
            // 
            this.dgvNotaFiscal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotaFiscal.Location = new System.Drawing.Point(6, 19);
            this.dgvNotaFiscal.Name = "dgvNotaFiscal";
            this.dgvNotaFiscal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNotaFiscal.Size = new System.Drawing.Size(918, 194);
            this.dgvNotaFiscal.TabIndex = 0;
            this.dgvNotaFiscal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNotaFiscal_CellClick);
            this.dgvNotaFiscal.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNotaFiscal_CellFormatting);
            this.dgvNotaFiscal.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvNotaFiscal_ColumnHeaderMouseClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(2, 333);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(931, 23);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Imprimir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(209, 362);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Status";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(313, 362);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Inutilizar NF-e";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblProcesso
            // 
            this.lblProcesso.AutoSize = true;
            this.lblProcesso.Location = new System.Drawing.Point(6, 317);
            this.lblProcesso.Name = "lblProcesso";
            this.lblProcesso.Size = new System.Drawing.Size(0, 13);
            this.lblProcesso.TabIndex = 10;
            // 
            // btnSair
            // 
            this.btnSair.Image = global::Nissi.WinFormsApplication.Properties.Resources.Cancel;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(1, 362);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(98, 23);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = global::Nissi.WinFormsApplication.Properties.Resources.NextComment;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(836, 362);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(96, 23);
            this.btnSalvar.TabIndex = 5;
            this.btnSalvar.Text = "Transmitir";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(417, 362);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 23);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar NF-e";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpData);
            this.groupBox1.Controls.Add(this.ckbData);
            this.groupBox1.Controls.Add(this.btnFiltro);
            this.groupBox1.Controls.Add(this.rbErro);
            this.groupBox1.Controls.Add(this.rbCancelada);
            this.groupBox1.Controls.Add(this.rbTransmitida);
            this.groupBox1.Controls.Add(this.rbNaoEnviada);
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(931, 72);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opções de Filtro";
            // 
            // dtpData
            // 
            this.dtpData.Enabled = false;
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpData.Location = new System.Drawing.Point(83, 42);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(141, 20);
            this.dtpData.TabIndex = 6;
            // 
            // ckbData
            // 
            this.ckbData.AutoSize = true;
            this.ckbData.Location = new System.Drawing.Point(10, 44);
            this.ckbData.Name = "ckbData";
            this.ckbData.Size = new System.Drawing.Size(67, 17);
            this.ckbData.TabIndex = 5;
            this.ckbData.Text = "por Data";
            this.ckbData.UseVisualStyleBackColor = true;
            this.ckbData.CheckedChanged += new System.EventHandler(this.ckbData_CheckedChanged);
            // 
            // btnFiltro
            // 
            this.btnFiltro.Location = new System.Drawing.Point(568, 20);
            this.btnFiltro.Name = "btnFiltro";
            this.btnFiltro.Size = new System.Drawing.Size(95, 23);
            this.btnFiltro.TabIndex = 4;
            this.btnFiltro.Text = "Filtrar";
            this.btnFiltro.UseVisualStyleBackColor = true;
            this.btnFiltro.Click += new System.EventHandler(this.btnFiltro_Click);
            // 
            // rbErro
            // 
            this.rbErro.AutoSize = true;
            this.rbErro.Location = new System.Drawing.Point(455, 20);
            this.rbErro.Name = "rbErro";
            this.rbErro.Size = new System.Drawing.Size(84, 17);
            this.rbErro.TabIndex = 3;
            this.rbErro.Text = "Erro no XML";
            this.rbErro.UseVisualStyleBackColor = true;
            // 
            // rbCancelada
            // 
            this.rbCancelada.AutoSize = true;
            this.rbCancelada.Location = new System.Drawing.Point(329, 20);
            this.rbCancelada.Name = "rbCancelada";
            this.rbCancelada.Size = new System.Drawing.Size(81, 17);
            this.rbCancelada.TabIndex = 2;
            this.rbCancelada.Text = "Canceladas";
            this.rbCancelada.UseVisualStyleBackColor = true;
            // 
            // rbTransmitida
            // 
            this.rbTransmitida.AutoSize = true;
            this.rbTransmitida.Location = new System.Drawing.Point(172, 19);
            this.rbTransmitida.Name = "rbTransmitida";
            this.rbTransmitida.Size = new System.Drawing.Size(84, 17);
            this.rbTransmitida.TabIndex = 1;
            this.rbTransmitida.Text = "Transmitidas";
            this.rbTransmitida.UseVisualStyleBackColor = true;
            // 
            // rbNaoEnviada
            // 
            this.rbNaoEnviada.AutoSize = true;
            this.rbNaoEnviada.Checked = true;
            this.rbNaoEnviada.Location = new System.Drawing.Point(10, 20);
            this.rbNaoEnviada.Name = "rbNaoEnviada";
            this.rbNaoEnviada.Size = new System.Drawing.Size(113, 17);
            this.rbNaoEnviada.TabIndex = 0;
            this.rbNaoEnviada.TabStop = true;
            this.rbNaoEnviada.Text = "Aguardando Envio";
            this.rbNaoEnviada.UseVisualStyleBackColor = true;
            // 
            // btnEnviarEmail
            // 
            this.btnEnviarEmail.Location = new System.Drawing.Point(518, 362);
            this.btnEnviarEmail.Name = "btnEnviarEmail";
            this.btnEnviarEmail.Size = new System.Drawing.Size(95, 23);
            this.btnEnviarEmail.TabIndex = 13;
            this.btnEnviarEmail.Text = "Enviar Email";
            this.btnEnviarEmail.UseVisualStyleBackColor = true;
            this.btnEnviarEmail.Click += new System.EventHandler(this.btnEnviarEmail_Click);
            // 
            // btnDistribuicao
            // 
            this.btnDistribuicao.Location = new System.Drawing.Point(619, 362);
            this.btnDistribuicao.Name = "btnDistribuicao";
            this.btnDistribuicao.Size = new System.Drawing.Size(103, 23);
            this.btnDistribuicao.TabIndex = 14;
            this.btnDistribuicao.Text = "Gerar Distribuição";
            this.btnDistribuicao.UseVisualStyleBackColor = true;
            this.btnDistribuicao.Click += new System.EventHandler(this.btnDistribuicao_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(746, 358);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frmNfeTransmite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 393);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnDistribuicao);
            this.Controls.Add(this.btnEnviarEmail);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblProcesso);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.gpNotasFiscais);
            this.Name = "frmNfeTransmite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sistema de Transmissão NF-e";
            this.Load += new System.EventHandler(this.frmNfeTransmite_Load);
            this.gpNotasFiscais.ResumeLayout(false);
            this.gpNotasFiscais.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotaFiscal)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpNotasFiscais;
        private System.Windows.Forms.DataGridView dgvNotaFiscal;
        private System.Windows.Forms.Label lblAmbiente;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblProcesso;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFiltro;
        private System.Windows.Forms.RadioButton rbErro;
        private System.Windows.Forms.RadioButton rbCancelada;
        private System.Windows.Forms.RadioButton rbTransmitida;
        private System.Windows.Forms.RadioButton rbNaoEnviada;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.CheckBox ckbData;
        private System.Windows.Forms.Button btnEnviarEmail;
        private System.Windows.Forms.Button btnDistribuicao;
        private System.Windows.Forms.Button button4;
    }
}