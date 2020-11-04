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
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(178, 44);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(140, 23);
            this.efwSimpleButton1.TabIndex = 3;
            this.efwSimpleButton1.Text = "저장";
            this.efwSimpleButton1.Click += new System.EventHandler(this.efwSimpleButton1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(324, 44);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(140, 23);
            this.efwSimpleButton2.TabIndex = 5;
            this.efwSimpleButton2.Text = "조회";
            this.efwSimpleButton2.Click += new System.EventHandler(this.efwSimpleButton2_Click);
            // 
            // richEditControl1
            // 
            this.richEditControl1.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richEditControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.richEditControl1.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            this.richEditControl1.Location = new System.Drawing.Point(6, 89);
            this.richEditControl1.Name = "richEditControl1";
            this.richEditControl1.Options.Bookmarks.Visibility = DevExpress.XtraRichEdit.RichEditBookmarkVisibility.Hidden;
            this.richEditControl1.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richEditControl1.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richEditControl1.Size = new System.Drawing.Size(554, 563);
            this.richEditControl1.TabIndex = 6;
            this.richEditControl1.Text = "richEditControl1";
            this.richEditControl1.BeforeExport += new DevExpress.XtraRichEdit.BeforeExportEventHandler(this.richEditControl1_BeforeExport);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(789, 230);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(434, 368);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // frmTest100
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.richEditControl1);
            this.Controls.Add(this.efwSimpleButton2);
            this.Controls.Add(this.efwSimpleButton1);
            this.Name = "frmTest100";
            this.Size = new System.Drawing.Size(1250, 753);
            this.Controls.SetChildIndex(this.efwSimpleButton1, 0);
            this.Controls.SetChildIndex(this.efwSimpleButton2, 0);
            this.Controls.SetChildIndex(this.richEditControl1, 0);
            this.Controls.SetChildIndex(this.richTextBox1, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}