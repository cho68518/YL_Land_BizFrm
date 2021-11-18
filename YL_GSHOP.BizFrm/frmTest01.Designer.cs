
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest01));
            this.imgOrganList = new System.Windows.Forms.ImageList(this.components);
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
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
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(86, 79);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(75, 78);
            this.efwSimpleButton1.TabIndex = 7;
            this.efwSimpleButton1.Text = "Map";
            this.efwSimpleButton1.Click += new System.EventHandler(this.efwSimpleButton1_Click);
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(86, 179);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(75, 78);
            this.efwSimpleButton2.TabIndex = 8;
            this.efwSimpleButton2.Text = "Map";
            this.efwSimpleButton2.Click += new System.EventHandler(this.efwSimpleButton2_Click);
            // 
            // frmTest01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.efwSimpleButton2);
            this.Controls.Add(this.efwSimpleButton1);
            this.Name = "frmTest01";
            this.Size = new System.Drawing.Size(1178, 704);
            this.Load += new System.EventHandler(this.frmTest01_Load);
            this.Controls.SetChildIndex(this.efwSimpleButton1, 0);
            this.Controls.SetChildIndex(this.efwSimpleButton2, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imgOrganList;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
    }
}