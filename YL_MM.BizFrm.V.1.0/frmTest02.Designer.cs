namespace YL_MM.BizFrm
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
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwTextEdit1 = new Easy.Framework.WinForm.Control.efwTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.efwTextEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(638, 147);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(75, 23);
            this.efwSimpleButton1.TabIndex = 2;
            this.efwSimpleButton1.Text = "efwSimpleButton1";
            this.efwSimpleButton1.Click += new System.EventHandler(this.EfwSimpleButton1_Click);
            // 
            // efwTextEdit1
            // 
            this.efwTextEdit1.EditValue2 = null;
            this.efwTextEdit1.Location = new System.Drawing.Point(91, 148);
            this.efwTextEdit1.Name = "efwTextEdit1";
            this.efwTextEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.efwTextEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.efwTextEdit1.RequireMessage = null;
            this.efwTextEdit1.Size = new System.Drawing.Size(541, 20);
            this.efwTextEdit1.TabIndex = 3;
            // 
            // frmTest02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.efwTextEdit1);
            this.Controls.Add(this.efwSimpleButton1);
            this.Name = "frmTest02";
            this.Size = new System.Drawing.Size(1040, 611);
            this.Controls.SetChildIndex(this.efwSimpleButton1, 0);
            this.Controls.SetChildIndex(this.efwTextEdit1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.efwTextEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
        private Easy.Framework.WinForm.Control.efwTextEdit efwTextEdit1;
    }
}