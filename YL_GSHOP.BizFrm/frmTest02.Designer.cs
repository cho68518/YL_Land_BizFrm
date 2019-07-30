namespace YL_GSHOP.BizFrm
{
    partial class frmTest02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest02));
            this.efwGroupControl2 = new Easy.Framework.WinForm.Control.efwGroupControl();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.efwGroupControl2)).BeginInit();
            this.efwGroupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // efwGroupControl2
            // 
            this.efwGroupControl2.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("efwGroupControl2.CaptionImageOptions.Image")));
            this.efwGroupControl2.Controls.Add(this.webBrowser1);
            this.efwGroupControl2.IsMultiLang = false;
            this.efwGroupControl2.Location = new System.Drawing.Point(207, 62);
            this.efwGroupControl2.Name = "efwGroupControl2";
            this.efwGroupControl2.Size = new System.Drawing.Size(687, 630);
            this.efwGroupControl2.TabIndex = 5;
            this.efwGroupControl2.Text = "위치정보";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(2, 23);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(683, 605);
            this.webBrowser1.TabIndex = 3;
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(126, 62);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(75, 78);
            this.efwSimpleButton1.TabIndex = 6;
            this.efwSimpleButton1.Text = "Map";
            this.efwSimpleButton1.Click += new System.EventHandler(this.EfwSimpleButton1_Click);
            // 
            // frmTest02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.efwSimpleButton1);
            this.Controls.Add(this.efwGroupControl2);
            this.Name = "frmTest02";
            this.Size = new System.Drawing.Size(1252, 753);
            this.Controls.SetChildIndex(this.efwGroupControl2, 0);
            this.Controls.SetChildIndex(this.efwSimpleButton1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.efwGroupControl2)).EndInit();
            this.efwGroupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwGroupControl efwGroupControl2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
    }
}