namespace YL_RM.BizFrm
{
    partial class frmMain
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
            this.efwPnlBody = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.efwSimpleButton3 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.btnSI0102 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.efwPnlBody)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // efwPnlBody
            // 
            this.efwPnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efwPnlBody.Location = new System.Drawing.Point(123, 0);
            this.efwPnlBody.Name = "efwPnlBody";
            this.efwPnlBody.Size = new System.Drawing.Size(1257, 820);
            this.efwPnlBody.TabIndex = 9;
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton3);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton2);
            this.efwPanelControl1.Controls.Add(this.btnSI0102);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(123, 820);
            this.efwPanelControl1.TabIndex = 8;
            // 
            // efwSimpleButton3
            // 
            this.efwSimpleButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwSimpleButton3.IsMultiLang = false;
            this.efwSimpleButton3.Location = new System.Drawing.Point(2, 45);
            this.efwSimpleButton3.Name = "efwSimpleButton3";
            this.efwSimpleButton3.Size = new System.Drawing.Size(119, 43);
            this.efwSimpleButton3.TabIndex = 4;
            this.efwSimpleButton3.Text = "frmRM02";
            this.efwSimpleButton3.Click += new System.EventHandler(this.efwSimpleButton3_Click);
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(2, 787);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(119, 31);
            this.efwSimpleButton2.TabIndex = 3;
            this.efwSimpleButton2.Text = "frmTest";
            // 
            // btnSI0102
            // 
            this.btnSI0102.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSI0102.IsMultiLang = false;
            this.btnSI0102.Location = new System.Drawing.Point(2, 2);
            this.btnSI0102.Name = "btnSI0102";
            this.btnSI0102.Size = new System.Drawing.Size(119, 43);
            this.btnSI0102.TabIndex = 0;
            this.btnSI0102.Text = "frmRM01";
            this.btnSI0102.Click += new System.EventHandler(this.btnSI0102_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 820);
            this.Controls.Add(this.efwPnlBody);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmMain";
            this.Text = "frmMain";
            ((System.ComponentModel.ISupportInitialize)(this.efwPnlBody)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPnlBody;
        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton3;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnSI0102;
    }
}