namespace YL_MM.BizFrm
{
    partial class frmMM03_Pop02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMM03_Pop02));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.picImg = new Easy.Framework.WinForm.Control.efwPictureEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImg.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 45);
            this.panel1.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("굴림", 9F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ButtonType = Easy.Framework.Util.BtnType.Close;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.IsMultiLang = false;
            this.btnCancel.Location = new System.Drawing.Point(690, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "닫기";
            this.btnCancel.ToolTip = "닫기";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // picImg
            // 
            this.picImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picImg.Location = new System.Drawing.Point(0, 45);
            this.picImg.Name = "picImg";
            this.picImg.Properties.AllowScrollOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
            this.picImg.Properties.AllowScrollViaMouseDrag = true;
            this.picImg.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
            this.picImg.Properties.ErrorImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("picImg.Properties.ErrorImageOptions.Image")));
            this.picImg.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picImg.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picImg.Properties.ZoomingOperationMode = DevExpress.XtraEditors.Repository.ZoomingOperationMode.MouseWheel;
            this.picImg.Size = new System.Drawing.Size(800, 480);
            this.picImg.TabIndex = 4;
            // 
            // frmMM03_Pop02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.picImg);
            this.Controls.Add(this.panel1);
            this.Name = "frmMM03_Pop02";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMM03_Pop02_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImg.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnCancel;
        private Easy.Framework.WinForm.Control.efwPictureEdit picImg;
    }
}