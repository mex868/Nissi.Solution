﻿namespace Nissi.WinFormsApplication
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nFeNascionalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parâmetrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nFeNascionalToolStripMenuItem,
            this.parâmetrosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(694, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nFeNascionalToolStripMenuItem
            // 
            this.nFeNascionalToolStripMenuItem.Name = "nFeNascionalToolStripMenuItem";
            this.nFeNascionalToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.nFeNascionalToolStripMenuItem.Text = "NF-e Nacional";
            this.nFeNascionalToolStripMenuItem.Click += new System.EventHandler(this.nFeNascionalToolStripMenuItem_Click);
            // 
            // parâmetrosToolStripMenuItem
            // 
            this.parâmetrosToolStripMenuItem.Name = "parâmetrosToolStripMenuItem";
            this.parâmetrosToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.parâmetrosToolStripMenuItem.Text = "Parâmetros";
            this.parâmetrosToolStripMenuItem.Click += new System.EventHandler(this.parâmetrosToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 453);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NF-e Nacional - v 20100715";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nFeNascionalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parâmetrosToolStripMenuItem;
    }
}
