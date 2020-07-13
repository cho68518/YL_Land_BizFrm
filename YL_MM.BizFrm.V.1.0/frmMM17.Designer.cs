namespace YL_MM.BizFrm
{
    partial class frmMM17
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMM17));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo2 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo3 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.efwLabel2 = new Easy.Framework.WinForm.Control.efwLabel();
            this.txtQuery = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.rbQType = new Easy.Framework.WinForm.Control.efwRadioGroup();
            this.dtSDATE = new Easy.Framework.WinForm.Control.efwDateEdit();
            this.efwLabel35 = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbQType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSDATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSDATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.efwLabel2);
            this.efwPanelControl1.Controls.Add(this.txtQuery);
            this.efwPanelControl1.Controls.Add(this.rbQType);
            this.efwPanelControl1.Controls.Add(this.dtSDATE);
            this.efwPanelControl1.Controls.Add(this.efwLabel35);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwPanelControl1.Location = new System.Drawing.Point(3, 35);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(992, 46);
            this.efwPanelControl1.TabIndex = 2;
            // 
            // efwLabel2
            // 
            this.efwLabel2.EraserGroup = null;
            this.efwLabel2.IsMultiLang = false;
            this.efwLabel2.Location = new System.Drawing.Point(225, 16);
            this.efwLabel2.Name = "efwLabel2";
            this.efwLabel2.Size = new System.Drawing.Size(44, 14);
            this.efwLabel2.TabIndex = 167;
            this.efwLabel2.Text = "검색 문구";
            // 
            // txtQuery
            // 
            this.txtQuery.EditValue2 = null;
            this.txtQuery.Location = new System.Drawing.Point(298, 13);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtQuery.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtQuery.RequireMessage = null;
            this.txtQuery.Size = new System.Drawing.Size(154, 20);
            this.txtQuery.TabIndex = 166;
            this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuery_KeyDown);
            // 
            // rbQType
            // 
            this.rbQType.IsMultiLang = false;
            this.rbQType.Location = new System.Drawing.Point(494, 11);
            this.rbQType.Name = "rbQType";
            this.rbQType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rbQType.Properties.Appearance.Options.UseBackColor = true;
            this.rbQType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.rbQType.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.rbQType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rbQType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("D", "도넛 라이프"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("T", "텔레콤  "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("A", "전체    ")});
            this.rbQType.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            this.rbQType.RequireMessage = null;
            this.rbQType.Size = new System.Drawing.Size(241, 24);
            this.rbQType.TabIndex = 164;
            // 
            // dtSDATE
            // 
            this.dtSDATE.EditValue = null;
            this.dtSDATE.Location = new System.Drawing.Point(96, 13);
            this.dtSDATE.Name = "dtSDATE";
            this.dtSDATE.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.dtSDATE.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dtSDATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtSDATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtSDATE.Size = new System.Drawing.Size(100, 20);
            this.dtSDATE.TabIndex = 163;
            // 
            // efwLabel35
            // 
            this.efwLabel35.EraserGroup = null;
            this.efwLabel35.IsMultiLang = false;
            this.efwLabel35.Location = new System.Drawing.Point(27, 16);
            this.efwLabel35.Name = "efwLabel35";
            this.efwLabel35.Size = new System.Drawing.Size(44, 14);
            this.efwLabel35.TabIndex = 162;
            this.efwLabel35.Text = "기간 년월";
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
            this.efwGridControl1.IsMultiLang = false;
            this.efwGridControl1.Location = new System.Drawing.Point(3, 81);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.Size = new System.Drawing.Size(992, 369);
            this.efwGridControl1.TabIndex = 3;
            this.efwGridControl1.TableName = "";
            serviceInfo3.InstanceName = "";
            serviceInfo3.IsUserIDAdd = true;
            serviceInfo3.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo3.ParamsInfo")));
            serviceInfo3.ProcName = "";
            serviceInfo3.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo3.UserParams")));
            this.efwGridControl1.UpdateServiceInfo = serviceInfo3;
            this.efwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.efwGridControl1.Click += new System.EventHandler(this.efwGridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gridView1.GridControl = this.efwGridControl1;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", null, "  ( 전송 건수:{0} )", "<Null>")});
            this.gridView1.Name = "gridView1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "전송일";
            this.gridColumn1.DisplayFormat.FormatString = "yyyy/M/d HH:mm:ss";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "insert_time";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 160;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "메세지구분";
            this.gridColumn2.FieldName = "msg_type";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "수신번호";
            this.gridColumn3.FieldName = "dstaddr";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 130;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "발신번호";
            this.gridColumn4.FieldName = "callback";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 98;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "결과";
            this.gridColumn5.FieldName = "stat";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "대체전송";
            this.gridColumn6.FieldName = "k_next_type";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "본문";
            this.gridColumn7.FieldName = "text";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 586;
            // 
            // frmMM17
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.efwGridControl1);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmMM17";
            this.Size = new System.Drawing.Size(998, 450);
            this.Load += new System.EventHandler(this.frmMM17_Load);
            this.Controls.SetChildIndex(this.efwPanelControl1, 0);
            this.Controls.SetChildIndex(this.efwGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.efwPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbQType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSDATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSDATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Easy.Framework.WinForm.Control.efwDateEdit dtSDATE;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel35;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private Easy.Framework.WinForm.Control.efwRadioGroup rbQType;
        private Easy.Framework.WinForm.Control.efwTextEdit txtQuery;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel2;
    }
}