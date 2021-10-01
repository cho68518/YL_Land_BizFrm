
namespace YL_GSHOP.BizFrm
{
    partial class frmTest01
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest01));
            this.imgOrganList = new System.Windows.Forms.ImageList();
            this.SuspendLayout();
            // 
            // imgOrganList
            // 
            this.imgOrganList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgOrganList.ImageStream")));
            this.imgOrganList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgOrganList.Images.SetKeyName(0, "DocumentMap_16x16.png");
            this.imgOrganList.Images.SetKeyName(1, "DocumentMap_16x16_Gray.png");
            this.imgOrganList.Images.SetKeyName(2, "DocumentMap_16x16_Gray.png");
            // 
            // frmTest01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "frmTest01";
            this.Size = new System.Drawing.Size(1178, 704);
            this.Load += new System.EventHandler(this.frmTest01_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imgOrganList;
    }
}