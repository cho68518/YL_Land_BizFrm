namespace YL_MM.BizFrm
{
    partial class frmMM12_Pop01
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
            this.efwLabel11 = new Easy.Framework.WinForm.Control.efwLabel();
            this.txtSearch = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.cmbQ1 = new Easy.Framework.WinForm.Control.efwLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQ1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.efwLabel11);
            this.efwPanelControl1.Controls.Add(this.txtSearch);
            this.efwPanelControl1.Controls.Add(this.cmbQ1);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(800, 45);
            this.efwPanelControl1.TabIndex = 0;
            // 
            // efwLabel11
            // 
            this.efwLabel11.EraserGroup = null;
            this.efwLabel11.IsMultiLang = false;
            this.efwLabel11.Location = new System.Drawing.Point(26, 15);
            this.efwLabel11.Name = "efwLabel11";
            this.efwLabel11.Size = new System.Drawing.Size(40, 14);
            this.efwLabel11.TabIndex = 72;
            this.efwLabel11.Text = "회원검색";
            // 
            // txtSearch
            // 
            this.txtSearch.EditValue2 = null;
            this.txtSearch.Location = new System.Drawing.Point(276, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtSearch.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSearch.RequireMessage = null;
            this.txtSearch.Size = new System.Drawing.Size(224, 20);
            this.txtSearch.TabIndex = 71;
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
            this.cmbQ1.Location = new System.Drawing.Point(123, 12);
            this.cmbQ1.MasterCode = "E03.MEMBERQ_GBN1";
            this.cmbQ1.Name = "cmbQ1";
            this.cmbQ1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(226)))));
            this.cmbQ1.Properties.Appearance.Options.UseBackColor = true;
            this.cmbQ1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQ1.Size = new System.Drawing.Size(147, 20);
            this.cmbQ1.TabIndex = 70;
            // 
            // frmMM12_Pop01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmMM12_Pop01";
            this.Text = "결재 현황";
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.efwPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQ1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel11;
        private Easy.Framework.WinForm.Control.efwTextEdit txtSearch;
        private Easy.Framework.WinForm.Control.efwLookUpEdit cmbQ1;
    }
}