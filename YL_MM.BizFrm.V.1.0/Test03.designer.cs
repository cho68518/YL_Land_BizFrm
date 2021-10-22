namespace YL_MM.BizFrm
{
    partial class Test03
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Test03));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imgOrganList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(3, 35);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(200, 507);
            this.treeView1.TabIndex = 3;
            // 
            // imgOrganList
            // 
            this.imgOrganList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgOrganList.ImageStream")));
            this.imgOrganList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgOrganList.Images.SetKeyName(0, "DocumentMap_16x16.png");
            this.imgOrganList.Images.SetKeyName(1, "DocumentMap_16x16_Gray.png");
            this.imgOrganList.Images.SetKeyName(2, "DocumentMap_16x16_Gray.png");
            // 
            // Test03
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView1);
            this.Name = "Test03";
            this.Size = new System.Drawing.Size(1094, 542);
            this.Load += new System.EventHandler(this.Test03_Load);
            this.Controls.SetChildIndex(this.treeView1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imgOrganList;
    }
}