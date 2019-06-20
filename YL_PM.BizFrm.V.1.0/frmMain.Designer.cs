namespace YL_PM.BizFrm
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
            this.btnTest02 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.btnTest01 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwSimpleButton3 = new Easy.Framework.WinForm.Control.efwSimpleButton();
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
            this.efwPnlBody.Size = new System.Drawing.Size(1197, 767);
            this.efwPnlBody.TabIndex = 9;
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton3);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton2);
            this.efwPanelControl1.Controls.Add(this.btnTest02);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton1);
            this.efwPanelControl1.Controls.Add(this.btnTest01);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(123, 767);
            this.efwPanelControl1.TabIndex = 8;
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(2, 76);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(119, 37);
            this.efwSimpleButton2.TabIndex = 4;
            this.efwSimpleButton2.Text = "frmPM03";
            this.efwSimpleButton2.Click += new System.EventHandler(this.EfwSimpleButton2_Click);
            // 
            // btnTest02
            // 
            this.btnTest02.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTest02.IsMultiLang = false;
            this.btnTest02.Location = new System.Drawing.Point(2, 39);
            this.btnTest02.Name = "btnTest02";
            this.btnTest02.Size = new System.Drawing.Size(119, 37);
            this.btnTest02.TabIndex = 3;
            this.btnTest02.Text = "frmPM02";
            this.btnTest02.Click += new System.EventHandler(this.BtnTest02_Click_1);
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(2, 738);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(119, 27);
            this.efwSimpleButton1.TabIndex = 2;
            this.efwSimpleButton1.Text = "frmTest4";
            // 
            // btnTest01
            // 
            this.btnTest01.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTest01.IsMultiLang = false;
            this.btnTest01.Location = new System.Drawing.Point(2, 2);
            this.btnTest01.Name = "btnTest01";
            this.btnTest01.Size = new System.Drawing.Size(119, 37);
            this.btnTest01.TabIndex = 0;
            this.btnTest01.Text = "frmPM01";
            this.btnTest01.Click += new System.EventHandler(this.BtnTest01_Click_1);
            // 
            // efwSimpleButton3
            // 
            this.efwSimpleButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwSimpleButton3.IsMultiLang = false;
            this.efwSimpleButton3.Location = new System.Drawing.Point(2, 113);
            this.efwSimpleButton3.Name = "efwSimpleButton3";
            this.efwSimpleButton3.Size = new System.Drawing.Size(119, 37);
            this.efwSimpleButton3.TabIndex = 5;
            this.efwSimpleButton3.Text = "frmPM04";
            this.efwSimpleButton3.Click += new System.EventHandler(this.EfwSimpleButton3_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 767);
            this.Controls.Add(this.efwPnlBody);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmMain";
            this.Text = "frmMain - YL_PM.BizFrm.V.1.0 (자재구매관리)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.efwPnlBody)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPnlBody;
        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnTest01;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnTest02;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton3;
    }
}