namespace Nissi.WinFormsApplication
{
    partial class frmNFeCancelar
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
            this.txtNFe = new System.Windows.Forms.TextBox();
            this.txtProtocolo = new System.Windows.Forms.TextBox();
            this.txtJustificativa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblContar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNFe
            // 
            this.txtNFe.Location = new System.Drawing.Point(102, 38);
            this.txtNFe.Name = "txtNFe";
            this.txtNFe.ReadOnly = true;
            this.txtNFe.Size = new System.Drawing.Size(290, 21);
            this.txtNFe.TabIndex = 2;
            // 
            // txtProtocolo
            // 
            this.txtProtocolo.Location = new System.Drawing.Point(102, 76);
            this.txtProtocolo.Name = "txtProtocolo";
            this.txtProtocolo.ReadOnly = true;
            this.txtProtocolo.Size = new System.Drawing.Size(290, 21);
            this.txtProtocolo.TabIndex = 1;
            // 
            // txtJustificativa
            // 
            this.txtJustificativa.Location = new System.Drawing.Point(102, 108);
            this.txtJustificativa.Name = "txtJustificativa";
            this.txtJustificativa.Size = new System.Drawing.Size(520, 21);
            this.txtJustificativa.TabIndex = 0;
            this.txtJustificativa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJustificativa_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "NF-e:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Protocolo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Justificativa:";
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(20, 139);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(98, 23);
            this.btnSair.TabIndex = 6;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(524, 139);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 23);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar NF-e";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblContar
            // 
            this.lblContar.AutoSize = true;
            this.lblContar.Location = new System.Drawing.Point(628, 112);
            this.lblContar.Name = "lblContar";
            this.lblContar.Size = new System.Drawing.Size(0, 13);
            this.lblContar.TabIndex = 8;
            // 
            // frmNFeCancelar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 180);
            this.Controls.Add(this.lblContar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJustificativa);
            this.Controls.Add(this.txtProtocolo);
            this.Controls.Add(this.txtNFe);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmNFeCancelar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancelamento de NF-e";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNFe;
        private System.Windows.Forms.TextBox txtProtocolo;
        private System.Windows.Forms.TextBox txtJustificativa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblContar;
    }
}