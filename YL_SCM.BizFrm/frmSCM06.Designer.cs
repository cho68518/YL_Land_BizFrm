namespace YL_SCM.BizFrm
{
    partial class frmSCM06
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
            Easy.Framework.WinForm.Control.ChildHierarchy childHierarchy1 = new Easy.Framework.WinForm.Control.ChildHierarchy();
            Easy.Framework.WinForm.Control.Hierarchy hierarchy1 = new Easy.Framework.WinForm.Control.Hierarchy();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo1 = new Easy.Framework.WinForm.Control.ServiceInfo();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSCM06));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo2 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo3 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.ewfPanel = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.efwLabel4 = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwLabel3 = new Easy.Framework.WinForm.Control.efwLabel();
            this.dtE_DATE = new Easy.Framework.WinForm.Control.efwDateEdit();
            this.dtS_DATE = new Easy.Framework.WinForm.Control.efwDateEdit();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            this.cmbSellers = new Easy.Framework.WinForm.Control.efwLookUpEdit();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtConfirmAmt = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwLabel6 = new Easy.Framework.WinForm.Control.efwLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ewfPanel)).BeginInit();
            this.ewfPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtE_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtE_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtS_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtS_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSellers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmAmt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ewfPanel
            // 
            this.ewfPanel.Controls.Add(this.txtConfirmAmt);
            this.ewfPanel.Controls.Add(this.efwLabel6);
            this.ewfPanel.Controls.Add(this.efwLabel4);
            this.ewfPanel.Controls.Add(this.efwLabel3);
            this.ewfPanel.Controls.Add(this.dtE_DATE);
            this.ewfPanel.Controls.Add(this.dtS_DATE);
            this.ewfPanel.Controls.Add(this.efwLabel1);
            this.ewfPanel.Controls.Add(this.cmbSellers);
            this.ewfPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ewfPanel.Location = new System.Drawing.Point(3, 35);
            this.ewfPanel.Name = "ewfPanel";
            this.ewfPanel.Size = new System.Drawing.Size(1043, 50);
            this.ewfPanel.TabIndex = 6;
            // 
            // efwLabel4
            // 
            this.efwLabel4.EraserGroup = null;
            this.efwLabel4.IsMultiLang = false;
            this.efwLabel4.Location = new System.Drawing.Point(403, 17);
            this.efwLabel4.Name = "efwLabel4";
            this.efwLabel4.Size = new System.Drawing.Size(9, 14);
            this.efwLabel4.TabIndex = 12;
            this.efwLabel4.Text = "~";
            // 
            // efwLabel3
            // 
            this.efwLabel3.EraserGroup = null;
            this.efwLabel3.IsMultiLang = false;
            this.efwLabel3.Location = new System.Drawing.Point(255, 17);
            this.efwLabel3.Name = "efwLabel3";
            this.efwLabel3.Size = new System.Drawing.Size(30, 14);
            this.efwLabel3.TabIndex = 11;
            this.efwLabel3.Text = "출고일";
            // 
            // dtE_DATE
            // 
            this.dtE_DATE.EditValue = new System.DateTime(2019, 10, 10, 0, 0, 0, 0);
            this.dtE_DATE.IsRequire = true;
            this.dtE_DATE.Location = new System.Drawing.Point(418, 14);
            this.dtE_DATE.Name = "dtE_DATE";
            this.dtE_DATE.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(226)))));
            this.dtE_DATE.Properties.Appearance.Options.UseBackColor = true;
            this.dtE_DATE.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.dtE_DATE.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dtE_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtE_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtE_DATE.Size = new System.Drawing.Size(99, 20);
            this.dtE_DATE.TabIndex = 10;
            // 
            // dtS_DATE
            // 
            this.dtS_DATE.EditValue = new System.DateTime(2019, 10, 10, 0, 0, 0, 0);
            this.dtS_DATE.IsRequire = true;
            this.dtS_DATE.Location = new System.Drawing.Point(300, 14);
            this.dtS_DATE.Name = "dtS_DATE";
            this.dtS_DATE.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(226)))));
            this.dtS_DATE.Properties.Appearance.Options.UseBackColor = true;
            this.dtS_DATE.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.dtS_DATE.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dtS_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtS_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtS_DATE.Size = new System.Drawing.Size(99, 20);
            this.dtS_DATE.TabIndex = 9;
            // 
            // efwLabel1
            // 
            this.efwLabel1.EraserGroup = null;
            this.efwLabel1.IsMultiLang = false;
            this.efwLabel1.Location = new System.Drawing.Point(22, 17);
            this.efwLabel1.Name = "efwLabel1";
            this.efwLabel1.Size = new System.Drawing.Size(30, 14);
            this.efwLabel1.TabIndex = 5;
            this.efwLabel1.Text = "회사명";
            // 
            // cmbSellers
            // 
            childHierarchy1.CodeCtrl = null;
            childHierarchy1.DbName = null;
            childHierarchy1.SpName = null;
            this.cmbSellers.ChildHierarchyInfo = childHierarchy1;
            this.cmbSellers.EraserGroup = "CLR1";
            hierarchy1.DbName = null;
            hierarchy1.SpName = null;
            this.cmbSellers.HierarchyInfo = hierarchy1;
            this.cmbSellers.IsMultiLang = false;
            this.cmbSellers.Location = new System.Drawing.Point(58, 14);
            this.cmbSellers.Name = "cmbSellers";
            this.cmbSellers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSellers.Size = new System.Drawing.Size(168, 20);
            this.cmbSellers.TabIndex = 4;
            // 
            // efwGridControl1
            // 
            this.efwGridControl1.BindSet = null;
            this.efwGridControl1.DBName = "";
            serviceInfo1.InstanceName = "";
            serviceInfo1.IsUserIDAdd = true;
            serviceInfo1.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo1.ParamsInfo")));
            serviceInfo1.ProcName = "";
            serviceInfo1.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo1.UserParams")));
            this.efwGridControl1.DeleteServiceInfo = serviceInfo1;
            this.efwGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            serviceInfo2.InstanceName = "";
            serviceInfo2.IsUserIDAdd = true;
            serviceInfo2.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo2.ParamsInfo")));
            serviceInfo2.ProcName = "";
            serviceInfo2.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo2.UserParams")));
            this.efwGridControl1.InsertServiceInfo = serviceInfo2;
            this.efwGridControl1.IsAddExcelBtn = true;
            this.efwGridControl1.isAddPrintBtn = true;
            this.efwGridControl1.IsEditable = false;
            this.efwGridControl1.IsMultiLang = false;
            this.efwGridControl1.Location = new System.Drawing.Point(3, 85);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.Size = new System.Drawing.Size(1043, 440);
            this.efwGridControl1.TabIndex = 7;
            this.efwGridControl1.TableName = "";
            serviceInfo3.InstanceName = "";
            serviceInfo3.IsUserIDAdd = true;
            serviceInfo3.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo3.ParamsInfo")));
            serviceInfo3.ProcName = "";
            serviceInfo3.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo3.UserParams")));
            this.efwGridControl1.UpdateServiceInfo = serviceInfo3;
            this.efwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.gridView1.GridControl = this.efwGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Fast;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "출고일";
            this.gridColumn18.FieldName = "o_delivery_start_date";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 0;
            this.gridColumn18.Width = 90;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "입금일";
            this.gridColumn19.FieldName = "o_deposit_confirm_date";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 1;
            this.gridColumn19.Width = 90;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "주문번호";
            this.gridColumn1.FieldName = "o_code";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 130;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "주문자명";
            this.gridColumn2.FieldName = "u_name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 100;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "제품명";
            this.gridColumn5.FieldName = "product_name";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 160;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "옵션명";
            this.gridColumn3.FieldName = "option_name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 5;
            this.gridColumn3.Width = 250;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "주문상태";
            this.gridColumn4.FieldName = "o_type";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "주문일";
            this.gridColumn6.FieldName = "o_date";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "수량";
            this.gridColumn7.DisplayFormat.FormatString = "###,##,##0";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "p_num";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 8;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "배송비";
            this.gridColumn8.DisplayFormat.FormatString = "###,##,##0";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "c_delivery_price";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 9;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "확정단가";
            this.gridColumn9.DisplayFormat.FormatString = "###,##,##0";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "c_price";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 10;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "확정금액";
            this.gridColumn10.DisplayFormat.FormatString = "###,##,##0";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "c_amt";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 11;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "총금액";
            this.gridColumn11.DisplayFormat.FormatString = "###,##,##0";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "tot_amt";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 12;
            // 
            // txtConfirmAmt
            // 
            this.txtConfirmAmt.EditValue2 = null;
            this.txtConfirmAmt.Location = new System.Drawing.Point(824, 15);
            this.txtConfirmAmt.Name = "txtConfirmAmt";
            this.txtConfirmAmt.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.txtConfirmAmt.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtConfirmAmt.Properties.Appearance.Options.UseBackColor = true;
            this.txtConfirmAmt.Properties.Appearance.Options.UseForeColor = true;
            this.txtConfirmAmt.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtConfirmAmt.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtConfirmAmt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtConfirmAmt.Properties.DisplayFormat.FormatString = "###,###,##0";
            this.txtConfirmAmt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtConfirmAmt.Properties.EditFormat.FormatString = "###,###,##0";
            this.txtConfirmAmt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtConfirmAmt.Properties.ReadOnly = true;
            this.txtConfirmAmt.RequireMessage = null;
            this.txtConfirmAmt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtConfirmAmt.Size = new System.Drawing.Size(110, 18);
            this.txtConfirmAmt.TabIndex = 22;
            // 
            // efwLabel6
            // 
            this.efwLabel6.EraserGroup = null;
            this.efwLabel6.IsMultiLang = false;
            this.efwLabel6.Location = new System.Drawing.Point(753, 17);
            this.efwLabel6.Name = "efwLabel6";
            this.efwLabel6.Size = new System.Drawing.Size(54, 14);
            this.efwLabel6.TabIndex = 21;
            this.efwLabel6.Text = "미지급 금액";
            // 
            // frmSCM06
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.efwGridControl1);
            this.Controls.Add(this.ewfPanel);
            this.Name = "frmSCM06";
            this.Size = new System.Drawing.Size(1049, 525);
            this.Controls.SetChildIndex(this.ewfPanel, 0);
            this.Controls.SetChildIndex(this.efwGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ewfPanel)).EndInit();
            this.ewfPanel.ResumeLayout(false);
            this.ewfPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtE_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtE_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtS_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtS_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSellers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmAmt.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl ewfPanel;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel4;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel3;
        private Easy.Framework.WinForm.Control.efwDateEdit dtE_DATE;
        private Easy.Framework.WinForm.Control.efwDateEdit dtS_DATE;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel1;
        private Easy.Framework.WinForm.Control.efwLookUpEdit cmbSellers;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private Easy.Framework.WinForm.Control.efwTextEdit txtConfirmAmt;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel6;
    }
}