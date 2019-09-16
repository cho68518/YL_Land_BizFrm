namespace YL_SCM.BizFrm
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
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.btnSCM01 = new Easy.Framework.WinForm.Control.efwSimpleButton();
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
            this.efwPnlBody.Size = new System.Drawing.Size(1109, 688);
            this.efwPnlBody.TabIndex = 15;
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton2);
            this.efwPanelControl1.Controls.Add(this.btnSCM01);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(123, 688);
            this.efwPanelControl1.TabIndex = 14;
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(2, 655);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(119, 31);
            this.efwSimpleButton2.TabIndex = 3;
            this.efwSimpleButton2.Text = "frmTest";
            // 
            // btnSCM01
            // 
            this.btnSCM01.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSCM01.IsMultiLang = false;
            this.btnSCM01.Location = new System.Drawing.Point(2, 2);
            this.btnSCM01.Name = "btnSCM01";
            this.btnSCM01.Size = new System.Drawing.Size(119, 35);
            this.btnSCM01.TabIndex = 0;
            this.btnSCM01.Text = "frmSCM01";
            this.btnSCM01.Click += new System.EventHandler(this.BtnSCM01_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 688);
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
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnSCM01;
    }
}