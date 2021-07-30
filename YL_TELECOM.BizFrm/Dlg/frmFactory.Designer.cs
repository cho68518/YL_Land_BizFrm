
namespace YL_TELECOM.BizFrm.Dlg
{
    partial class frmFactory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFactory));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo1 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo2 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo3 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.rbUse_yn = new Easy.Framework.WinForm.Control.efwRadioGroup();
            this.efwLabel2 = new Easy.Framework.WinForm.Control.efwLabel();
            this.txt_search = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbUse_yn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_search.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.simpleButton2);
            this.efwPanelControl1.Controls.Add(this.rbUse_yn);
            this.efwPanelControl1.Controls.Add(this.efwLabel2);
            this.efwPanelControl1.Controls.Add(this.txt_search);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(446, 72);
            this.efwPanelControl1.TabIndex = 5;
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(304, 16);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(118, 39);
            this.simpleButton2.TabIndex = 93;
            this.simpleButton2.Text = "창고 목록";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // rbUse_yn
            // 
            this.rbUse_yn.IsMultiLang = false;
            this.rbUse_yn.Location = new System.Drawing.Point(13, 43);
            this.rbUse_yn.Name = "rbUse_yn";
            this.rbUse_yn.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rbUse_yn.Properties.Appearance.Options.UseBackColor = true;
            this.rbUse_yn.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.rbUse_yn.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.rbUse_yn.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rbUse_yn.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("T", "전체"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Y", "운영"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("N", "거래중지")});
            this.rbUse_yn.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            this.rbUse_yn.RequireMessage = null;
            this.rbUse_yn.Size = new System.Drawing.Size(208, 23);
            this.rbUse_yn.TabIndex = 87;
            // 
            // efwLabel2
            // 
            this.efwLabel2.EraserGroup = null;
            this.efwLabel2.IsMultiLang = false;
            this.efwLabel2.Location = new System.Drawing.Point(12, 16);
            this.efwLabel2.Name = "efwLabel2";
            this.efwLabel2.Size = new System.Drawing.Size(30, 14);
            this.efwLabel2.TabIndex = 3;
            this.efwLabel2.Text = "창고명";
            // 
            // txt_search
            // 
            this.txt_search.EditValue2 = null;
            this.txt_search.Location = new System.Drawing.Point(65, 13);
            this.txt_search.Name = "txt_search";
            this.txt_search.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txt_search.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txt_search.RequireMessage = null;
            this.txt_search.Size = new System.Drawing.Size(156, 20);
            this.txt_search.TabIndex = 2;
            // 
            // efwGridControl1
            // 
            this.efwGridControl1.BindSet = null;
            this.efwGridControl1.DBName = "";
            serviceInfo1.InstanceName = "";
            serviceInfo1.IsUserIDAdd = true;
            serviceInfo1.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo1.ParamsInfo")));
            serviceInfo1.ProcName = "";
            serviceInfo1.UserParams = null;
            this.efwGridControl1.DeleteServiceInfo = serviceInfo1;
            this.efwGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            serviceInfo2.InstanceName = "";
            serviceInfo2.IsUserIDAdd = true;
            serviceInfo2.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo2.ParamsInfo")));
            serviceInfo2.ProcName = "";
            serviceInfo2.UserParams = null;
            this.efwGridControl1.InsertServiceInfo = serviceInfo2;
            this.efwGridControl1.IsAddExcelBtn = true;
            this.efwGridControl1.isAddPrintBtn = true;
            this.efwGridControl1.IsMultiLang = false;
            this.efwGridControl1.Location = new System.Drawing.Point(0, 72);
            this.efwGridControl1.MainView = this.gridView2;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.efwGridControl1.Size = new System.Drawing.Size(446, 431);
            this.efwGridControl1.TabIndex = 84;
            this.efwGridControl1.TableName = "";
            this.efwGridControl1.TabStop = false;
            serviceInfo3.InstanceName = "";
            serviceInfo3.IsUserIDAdd = true;
            serviceInfo3.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo3.ParamsInfo")));
            serviceInfo3.ProcName = "";
            serviceInfo3.UserParams = null;
            this.efwGridControl1.UpdateServiceInfo = serviceInfo3;
            this.efwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.efwGridControl1.DoubleClick += new System.EventHandler(this.efwGridControl1_DoubleClick);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView2.GridControl = this.efwGridControl1;
            this.gridView2.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", null, "건수 : {0}")});
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowFooter = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "코드";
            this.gridColumn1.FieldName = "idx_fac";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 46;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "창고명";
            this.gridColumn2.FieldName = "u_name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 211;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "등록일";
            this.gridColumn3.FieldName = "insert_date";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 80;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "거래유무";
            this.gridColumn4.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn4.FieldName = "use_yn";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 53;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = "Y";
            this.repositoryItemCheckEdit1.ValueUnchecked = "N";
            // 
            // frmFactory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 503);
            this.Controls.Add(this.efwGridControl1);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmFactory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "창고 목록";
            this.Load += new System.EventHandler(this.frmFactory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.efwPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbUse_yn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_search.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwRadioGroup rbUse_yn;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel2;
        private Easy.Framework.WinForm.Control.efwTextEdit txt_search;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}