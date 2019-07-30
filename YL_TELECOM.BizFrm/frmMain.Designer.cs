namespace YL_TELECOM.BizFrm
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
            this.btnTM01 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.btnTM02 = new Easy.Framework.WinForm.Control.efwSimpleButton();
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
            this.efwPnlBody.Size = new System.Drawing.Size(1275, 808);
            this.efwPnlBody.TabIndex = 13;
            // 
            // btnTM01
            // 
            this.btnTM01.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTM01.IsMultiLang = false;
            this.btnTM01.Location = new System.Drawing.Point(2, 2);
            this.btnTM01.Name = "btnTM01";
            this.btnTM01.Size = new System.Drawing.Size(119, 35);
            this.btnTM01.TabIndex = 0;
            this.btnTM01.Text = "frmTM01";
            this.btnTM01.Click += new System.EventHandler(this.BtnTM01_Click);
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.btnTM02);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton2);
            this.efwPanelControl1.Controls.Add(this.btnTM01);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(123, 808);
            this.efwPanelControl1.TabIndex = 12;
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(2, 775);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(119, 31);
            this.efwSimpleButton2.TabIndex = 3;
            this.efwSimpleButton2.Text = "frmTest";
            // 
            // btnTM02
            // 
            this.btnTM02.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTM02.IsMultiLang = false;
            this.btnTM02.Location = new System.Drawing.Point(2, 37);
            this.btnTM02.Name = "btnTM02";
            this.btnTM02.Size = new System.Drawing.Size(119, 35);
            this.btnTM02.TabIndex = 4;
            this.btnTM02.Text = "frmTM02";
            this.btnTM02.Click += new System.EventHandler(this.BtnTM02_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1398, 808);
            this.Controls.Add(this.efwPnlBody);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            ((System.ComponentModel.ISupportInitialize)(this.efwPnlBody)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPnlBody;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnTM01;
        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnTM02;
    }
}