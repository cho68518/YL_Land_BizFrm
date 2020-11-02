namespace YL_DONUT.BizFrm
{
    partial class frmTest100
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
            this.efwMemoEdit1 = new Easy.Framework.WinForm.Control.efwMemoEdit();
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.efwPictureEdit1 = new Easy.Framework.WinForm.Control.efwPictureEdit();
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.efwMemoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // efwMemoEdit1
            // 
            this.efwMemoEdit1.Location = new System.Drawing.Point(74, 119);
            this.efwMemoEdit1.Name = "efwMemoEdit1";
            this.efwMemoEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.efwMemoEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.efwMemoEdit1.Size = new System.Drawing.Size(299, 237);
            this.efwMemoEdit1.TabIndex = 2;
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(436, 120);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(140, 23);
            this.efwSimpleButton1.TabIndex = 3;
            this.efwSimpleButton1.Text = "efwSimpleButton1";
            this.efwSimpleButton1.Click += new System.EventHandler(this.efwSimpleButton1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // efwPictureEdit1
            // 
            this.efwPictureEdit1.Location = new System.Drawing.Point(436, 168);
            this.efwPictureEdit1.Name = "efwPictureEdit1";
            this.efwPictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.efwPictureEdit1.Size = new System.Drawing.Size(251, 188);
            this.efwPictureEdit1.TabIndex = 4;
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(721, 117);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(140, 23);
            this.efwSimpleButton2.TabIndex = 5;
            this.efwSimpleButton2.Text = "efwSimpleButton2";
            this.efwSimpleButton2.Click += new System.EventHandler(this.efwSimpleButton2_Click);
            // 
            // accordionControl1
            // 
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accordionControl1.Location = new System.Drawing.Point(113, 381);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.Size = new System.Drawing.Size(630, 235);
            this.accordionControl1.TabIndex = 6;
            this.accordionControl1.Text = "accordionControl1";
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Element1";
            // 
            // frmTest100
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.efwSimpleButton2);
            this.Controls.Add(this.efwPictureEdit1);
            this.Controls.Add(this.efwSimpleButton1);
            this.Controls.Add(this.efwMemoEdit1);
            this.Name = "frmTest100";
            this.Size = new System.Drawing.Size(1128, 646);
            this.Controls.SetChildIndex(this.efwMemoEdit1, 0);
            this.Controls.SetChildIndex(this.efwSimpleButton1, 0);
            this.Controls.SetChildIndex(this.efwPictureEdit1, 0);
            this.Controls.SetChildIndex(this.efwSimpleButton2, 0);
            this.Controls.SetChildIndex(this.accordionControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.efwMemoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwMemoEdit efwMemoEdit1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Easy.Framework.WinForm.Control.efwPictureEdit efwPictureEdit1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
    }
}