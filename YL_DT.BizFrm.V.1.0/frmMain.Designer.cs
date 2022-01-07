namespace YL_DT.BizFrm
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
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.btnDT01 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwPnlBody = new Easy.Framework.WinForm.Control.efwPanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.efwPnlBody)).BeginInit();
            this.SuspendLayout();
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.btnDT01);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(123, 718);
            this.efwPanelControl1.TabIndex = 15;
            // 
            // btnDT01
            // 
            this.btnDT01.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDT01.IsMultiLang = false;
            this.btnDT01.Location = new System.Drawing.Point(2, 2);
            this.btnDT01.Name = "btnDT01";
            this.btnDT01.Size = new System.Drawing.Size(119, 35);
            this.btnDT01.TabIndex = 0;
            this.btnDT01.Text = "frmDT01";
            this.btnDT01.Click += new System.EventHandler(this.btnDT01_Click);
            // 
            // efwPnlBody
            // 
            this.efwPnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efwPnlBody.Location = new System.Drawing.Point(123, 0);
            this.efwPnlBody.Name = "efwPnlBody";
            this.efwPnlBody.Size = new System.Drawing.Size(1019, 718);
            this.efwPnlBody.TabIndex = 16;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 718);
            this.Controls.Add(this.efwPnlBody);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.efwPnlBody)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnDT01;
        private Easy.Framework.WinForm.Control.efwPanelControl efwPnlBody;
    }
}