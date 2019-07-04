namespace YL_GSHOP.BizFrm
{
    partial class frmGSHOP08_Pop02
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
            this.picImg = new Easy.Framework.WinForm.Control.efwPictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.picImg.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // picImg
            // 
            this.picImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picImg.Location = new System.Drawing.Point(5, 5);
            this.picImg.Name = "picImg";
            this.picImg.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picImg.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picImg.Size = new System.Drawing.Size(822, 558);
            this.picImg.TabIndex = 2;
            // 
            // frmGSHOP08_Pop02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 568);
            this.Controls.Add(this.picImg);
            this.Name = "frmGSHOP08_Pop02";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frmGSHOP08_Pop02";
            this.Load += new System.EventHandler(this.FrmGSHOP08_Pop02_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImg.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPictureEdit picImg;
    }
}