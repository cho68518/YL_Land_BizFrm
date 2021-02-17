namespace YL_DONUT.BizFrm
{
    partial class frmDN20_Pop02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDN20_Pop02));
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.SuspendLayout();
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("efwSimpleButton2.ImageOptions.Image")));
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(418, 36);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(82, 28);
            this.efwSimpleButton2.TabIndex = 51;
            this.efwSimpleButton2.Text = "발행";
            this.efwSimpleButton2.Click += new System.EventHandler(this.efwSimpleButton2_Click);
            // 
            // frmDN20_Pop02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 525);
            this.Controls.Add(this.efwSimpleButton2);
            this.Name = "frmDN20_Pop02";
            this.Text = "가입코드 발행";
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
    }
}