namespace YL_GM.BizFrm
{
    partial class frmGM11_Pop05
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
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo1 = new Easy.Framework.WinForm.Control.ServiceInfo();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGM11_Pop05));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo2 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo3 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.bthNew = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwLabel2 = new Easy.Framework.WinForm.Control.efwLabel();
            this.dt1T = new Easy.Framework.WinForm.Control.efwDateEdit();
            this.dt1F = new Easy.Framework.WinForm.Control.efwDateEdit();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt1T.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt1T.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt1F.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt1F.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.efwGridControl1.Location = new System.Drawing.Point(0, 68);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.efwGridControl1.Size = new System.Drawing.Size(954, 487);
            this.efwGridControl1.TabIndex = 49;
            this.efwGridControl1.TableName = "";
            serviceInfo3.InstanceName = "";
            serviceInfo3.IsUserIDAdd = true;
            serviceInfo3.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo3.ParamsInfo")));
            serviceInfo3.ProcName = "";
            serviceInfo3.UserParams = null;
            this.efwGridControl1.UpdateServiceInfo = serviceInfo3;
            this.efwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Red;
            this.gridView1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridView1.DetailHeight = 476;
            this.gridView1.GridControl = this.efwGridControl1;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", null, "(건수 : {0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "o_total_cost", null, "합계 : {0:c} )")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "주문일";
            this.gridColumn1.FieldName = "o_date";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "온라인몰";
            this.gridColumn2.FieldName = "o_company_nm";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "오픈마켓";
            this.gridColumn3.FieldName = "o_sub_company_nm";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "수량";
            this.gridColumn4.FieldName = "o_qty";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "금액";
            this.gridColumn5.FieldName = "o_amt";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "비고";
            this.gridColumn6.FieldName = "remark";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 458;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = "Y";
            this.repositoryItemCheckEdit1.ValueUnchecked = "N";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.ValueChecked = "Y";
            this.repositoryItemCheckEdit2.ValueUnchecked = "N";
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.bthNew);
            this.efwPanelControl1.Controls.Add(this.efwLabel2);
            this.efwPanelControl1.Controls.Add(this.dt1T);
            this.efwPanelControl1.Controls.Add(this.dt1F);
            this.efwPanelControl1.Controls.Add(this.efwLabel1);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(954, 68);
            this.efwPanelControl1.TabIndex = 48;
            // 
            // bthNew
            // 
            this.bthNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bthNew.ImageOptions.Image")));
            this.bthNew.IsMultiLang = false;
            this.bthNew.Location = new System.Drawing.Point(363, 12);
            this.bthNew.Name = "bthNew";
            this.bthNew.Size = new System.Drawing.Size(83, 43);
            this.bthNew.TabIndex = 0;
            this.bthNew.Text = "조회";
            this.bthNew.Click += new System.EventHandler(this.bthNew_Click);
            // 
            // efwLabel2
            // 
            this.efwLabel2.EraserGroup = null;
            this.efwLabel2.IsMultiLang = false;
            this.efwLabel2.Location = new System.Drawing.Point(178, 23);
            this.efwLabel2.Name = "efwLabel2";
            this.efwLabel2.Size = new System.Drawing.Size(9, 14);
            this.efwLabel2.TabIndex = 51;
            this.efwLabel2.Text = "~";
            // 
            // dt1T
            // 
            this.dt1T.EditValue = null;
            this.dt1T.EraserGroup = "CLR1";
            this.dt1T.Location = new System.Drawing.Point(194, 20);
            this.dt1T.Name = "dt1T";
            this.dt1T.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.dt1T.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dt1T.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt1T.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt1T.Size = new System.Drawing.Size(102, 20);
            this.dt1T.TabIndex = 2;
            // 
            // dt1F
            // 
            this.dt1F.EditValue = null;
            this.dt1F.EraserGroup = "CLR1";
            this.dt1F.Location = new System.Drawing.Point(68, 20);
            this.dt1F.Name = "dt1F";
            this.dt1F.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.dt1F.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dt1F.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt1F.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt1F.Size = new System.Drawing.Size(102, 20);
            this.dt1F.TabIndex = 1;
            // 
            // efwLabel1
            // 
            this.efwLabel1.EraserGroup = null;
            this.efwLabel1.IsMultiLang = false;
            this.efwLabel1.Location = new System.Drawing.Point(23, 24);
            this.efwLabel1.Name = "efwLabel1";
            this.efwLabel1.Size = new System.Drawing.Size(20, 14);
            this.efwLabel1.TabIndex = 47;
            this.efwLabel1.Text = "기간";
            // 
            // frmGM11_Pop05
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 555);
            this.Controls.Add(this.efwGridControl1);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmGM11_Pop05";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "온라인 주문 상세현황";
            this.Load += new System.EventHandler(this.frmGM11_Pop05_Load);
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.efwPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt1T.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt1T.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt1F.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt1F.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwSimpleButton bthNew;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel2;
        private Easy.Framework.WinForm.Control.efwDateEdit dt1T;
        private Easy.Framework.WinForm.Control.efwDateEdit dt1F;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}