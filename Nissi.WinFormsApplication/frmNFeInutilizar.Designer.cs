namespace Nissi.WinFormsApplication
{
    partial class frmNFeInutilizar
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
            this.components = new System.ComponentModel.Container();
            this.lblAno = new System.Windows.Forms.Label();
            this.lblNumInicial = new System.Windows.Forms.Label();
            this.lblNumFinal = new System.Windows.Forms.Label();
            this.lblJustificativa = new System.Windows.Forms.Label();
            this.tbxAno = new System.Windows.Forms.TextBox();
            this.tbxNumInicial = new System.Windows.Forms.TextBox();
            this.tbxNumFinal = new System.Windows.Forms.TextBox();
            this.tbxJustificativa = new System.Windows.Forms.TextBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnInutilizar = new System.Windows.Forms.Button();
            this.erroValidation = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.erroValidation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAno
            // 
            this.lblAno.AutoSize = true;
            this.lblAno.Location = new System.Drawing.Point(12, 9);
            this.lblAno.Name = "lblAno";
            this.lblAno.Size = new System.Drawing.Size(29, 13);
            this.lblAno.TabIndex = 0;
            this.lblAno.Text = "Ano:";
            // 
            // lblNumInicial
            // 
            this.lblNumInicial.AutoSize = true;
            this.lblNumInicial.Location = new System.Drawing.Point(12, 38);
            this.lblNumInicial.Name = "lblNumInicial";
            this.lblNumInicial.Size = new System.Drawing.Size(77, 13);
            this.lblNumInicial.TabIndex = 1;
            this.lblNumInicial.Text = "Número Inicial:";
            // 
            // lblNumFinal
            // 
            this.lblNumFinal.AutoSize = true;
            this.lblNumFinal.Location = new System.Drawing.Point(12, 70);
            this.lblNumFinal.Name = "lblNumFinal";
            this.lblNumFinal.Size = new System.Drawing.Size(72, 13);
            this.lblNumFinal.TabIndex = 2;
            this.lblNumFinal.Text = "Número Final:";
            // 
            // lblJustificativa
            // 
            this.lblJustificativa.AutoSize = true;
            this.lblJustificativa.Location = new System.Drawing.Point(12, 99);
            this.lblJustificativa.Name = "lblJustificativa";
            this.lblJustificativa.Size = new System.Drawing.Size(65, 13);
            this.lblJustificativa.TabIndex = 3;
            this.lblJustificativa.Text = "Justificativa:";
            // 
            // tbxAno
            // 
            this.tbxAno.Location = new System.Drawing.Point(95, 9);
            this.tbxAno.MaxLength = 2;
            this.tbxAno.Name = "tbxAno";
            this.tbxAno.Size = new System.Drawing.Size(100, 20);
            this.tbxAno.TabIndex = 4;
            this.tbxAno.Validating += new System.ComponentModel.CancelEventHandler(this.tbxAno_Validating);
            // 
            // tbxNumInicial
            // 
            this.tbxNumInicial.Location = new System.Drawing.Point(95, 38);
            this.tbxNumInicial.Name = "tbxNumInicial";
            this.tbxNumInicial.Size = new System.Drawing.Size(100, 20);
            this.tbxNumInicial.TabIndex = 5;
            this.tbxNumInicial.Validating += new System.ComponentModel.CancelEventHandler(this.tbxNumInicial_Validating);
            // 
            // tbxNumFinal
            // 
            this.tbxNumFinal.Location = new System.Drawing.Point(95, 67);
            this.tbxNumFinal.Name = "tbxNumFinal";
            this.tbxNumFinal.Size = new System.Drawing.Size(100, 20);
            this.tbxNumFinal.TabIndex = 6;
            this.tbxNumFinal.Validating += new System.ComponentModel.CancelEventHandler(this.tbxNumFinal_Validating);
            // 
            // tbxJustificativa
            // 
            this.tbxJustificativa.Location = new System.Drawing.Point(15, 116);
            this.tbxJustificativa.Multiline = true;
            this.tbxJustificativa.Name = "tbxJustificativa";
            this.tbxJustificativa.Size = new System.Drawing.Size(519, 73);
            this.tbxJustificativa.TabIndex = 7;
            this.tbxJustificativa.Validating += new System.ComponentModel.CancelEventHandler(this.tbxJustificativa_Validating);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(15, 210);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 8;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnInutilizar
            // 
            this.btnInutilizar.Location = new System.Drawing.Point(450, 210);
            this.btnInutilizar.Name = "btnInutilizar";
            this.btnInutilizar.Size = new System.Drawing.Size(84, 23);
            this.btnInutilizar.TabIndex = 9;
            this.btnInutilizar.Text = "Inutilizar NF-e";
            this.btnInutilizar.UseVisualStyleBackColor = true;
            this.btnInutilizar.Click += new System.EventHandler(this.btnInutilizar_Click);
            // 
            // erroValidation
            // 
            this.erroValidation.ContainerControl = this;
            // 
            // frmNFeInutilizar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 241);
            this.Controls.Add(this.btnInutilizar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.tbxJustificativa);
            this.Controls.Add(this.tbxNumFinal);
            this.Controls.Add(this.tbxNumInicial);
            this.Controls.Add(this.tbxAno);
            this.Controls.Add(this.lblJustificativa);
            this.Controls.Add(this.lblNumFinal);
            this.Controls.Add(this.lblNumInicial);
            this.Controls.Add(this.lblAno);
            this.Name = "frmNFeInutilizar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNFeInutilizar";
            ((System.ComponentModel.ISupportInitialize)(this.erroValidation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAno;
        private System.Windows.Forms.Label lblNumInicial;
        private System.Windows.Forms.Label lblNumFinal;
        private System.Windows.Forms.Label lblJustificativa;
        private System.Windows.Forms.TextBox tbxAno;
        private System.Windows.Forms.TextBox tbxNumInicial;
        private System.Windows.Forms.TextBox tbxNumFinal;
        private System.Windows.Forms.TextBox tbxJustificativa;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnInutilizar;
        private System.Windows.Forms.ErrorProvider erroValidation;
    }
}