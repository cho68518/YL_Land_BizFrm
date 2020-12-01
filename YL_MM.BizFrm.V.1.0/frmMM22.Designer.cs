namespace YL_MM.BizFrm
{
    partial class frmMM22
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
            Easy.Framework.WinForm.Control.ChildHierarchy childHierarchy2 = new Easy.Framework.WinForm.Control.ChildHierarchy();
            Easy.Framework.WinForm.Control.Hierarchy hierarchy2 = new Easy.Framework.WinForm.Control.Hierarchy();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            this.cmbQ1 = new Easy.Framework.WinForm.Control.efwLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQ1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.efwLabel1);
            this.efwPanelControl1.Controls.Add(this.cmbQ1);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwPanelControl1.Location = new System.Drawing.Point(3, 35);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(1146, 50);
            this.efwPanelControl1.TabIndex = 4;
            // 
            // efwLabel1
            // 
            this.efwLabel1.EraserGroup = null;
            this.efwLabel1.IsMultiLang = false;
            this.efwLabel1.Location = new System.Drawing.Point(23, 18);
            this.efwLabel1.Name = "efwLabel1";
            this.efwLabel1.Size = new System.Drawing.Size(30, 14);
            this.efwLabel1.TabIndex = 27;
            this.efwLabel1.Text = "작업일";
            // 
            // cmbQ1
            // 
            childHierarchy2.CodeCtrl = null;
            childHierarchy2.DbName = null;
            childHierarchy2.SpName = null;
            this.cmbQ1.ChildHierarchyInfo = childHierarchy2;
            hierarchy2.DbName = null;
            hierarchy2.SpName = null;
            this.cmbQ1.HierarchyInfo = hierarchy2;
            this.cmbQ1.IsMultiLang = false;
            this.cmbQ1.Location = new System.Drawing.Point(96, 15);
            this.cmbQ1.MasterCode = "E03.MEMBERQ_GBN4";
            this.cmbQ1.Name = "cmbQ1";
            this.cmbQ1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(226)))));
            this.cmbQ1.Properties.Appearance.Options.UseBackColor = true;
            this.cmbQ1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQ1.Size = new System.Drawing.Size(147, 20);
            this.cmbQ1.TabIndex = 26;
            // 
            // frmMM22
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmMM22";
            this.Size = new System.Drawing.Size(1152, 562);
            this.Controls.SetChildIndex(this.efwPanelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.efwPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQ1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel1;
        private Easy.Framework.WinForm.Control.efwLookUpEdit cmbQ1;
    }
}