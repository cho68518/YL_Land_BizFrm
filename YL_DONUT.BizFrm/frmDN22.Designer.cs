namespace YL_DONUT.BizFrm
{
    partial class frmDN22
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDN22));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo2 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo3 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.rbLevelQ = new Easy.Framework.WinForm.Control.efwRadioGroup();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.efwGroupControl1 = new Easy.Framework.WinForm.Control.efwGroupControl();
            this.efwLabel5 = new Easy.Framework.WinForm.Control.efwLabel();
            this.txtStory_id = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwLabel4 = new Easy.Framework.WinForm.Control.efwLabel();
            this.txtVisit_count = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.btnSave = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.btnDelete = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.rbIs_use = new Easy.Framework.WinForm.Control.efwRadioGroup();
            this.txtContents = new Easy.Framework.WinForm.Control.efwMemoEdit();
            this.txtSubject = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwLabel3 = new Easy.Framework.WinForm.Control.efwLabel();
            this.dtReg_date = new Easy.Framework.WinForm.Control.efwDateEdit();
            this.efwLabel2 = new Easy.Framework.WinForm.Control.efwLabel();
            this.rbLevel = new Easy.Framework.WinForm.Control.efwRadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbLevelQ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGroupControl1)).BeginInit();
            this.efwGroupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStory_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisit_count.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbIs_use.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContents.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtReg_date.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtReg_date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbLevel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.rbLevelQ);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwPanelControl1.Location = new System.Drawing.Point(3, 35);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(1040, 52);
            this.efwPanelControl1.TabIndex = 2;
            // 
            // rbLevelQ
            // 
            this.rbLevelQ.IsMultiLang = false;
            this.rbLevelQ.Location = new System.Drawing.Point(23, 15);
            this.rbLevelQ.Name = "rbLevelQ";
            this.rbLevelQ.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rbLevelQ.Properties.Appearance.Options.UseBackColor = true;
            this.rbLevelQ.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.rbLevelQ.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.rbLevelQ.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("215", "도마공지"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("238", "G 메니저 공지"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("400", "G 멀티샵 공지")});
            this.rbLevelQ.RequireMessage = null;
            this.rbLevelQ.Size = new System.Drawing.Size(322, 24);
            this.rbLevelQ.TabIndex = 0;
            this.rbLevelQ.SelectedIndexChanged += new System.EventHandler(this.rbLevelQ_SelectedIndexChanged);
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
            this.efwGridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            serviceInfo2.InstanceName = "";
            serviceInfo2.IsUserIDAdd = true;
            serviceInfo2.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo2.ParamsInfo")));
            serviceInfo2.ProcName = "";
            serviceInfo2.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo2.UserParams")));
            this.efwGridControl1.InsertServiceInfo = serviceInfo2;
            this.efwGridControl1.IsAddExcelBtn = true;
            this.efwGridControl1.isAddPrintBtn = true;
            this.efwGridControl1.IsMultiLang = false;
            this.efwGridControl1.Location = new System.Drawing.Point(3, 87);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.Size = new System.Drawing.Size(1040, 425);
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
            this.gridColumn7,
            this.gridColumn8});
            this.gridView1.GridControl = this.efwGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "category_no";
            this.gridColumn1.FieldName = "category_no";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Width = 146;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "story_id";
            this.gridColumn2.FieldName = "story_id";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Width = 146;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "등록일";
            this.gridColumn3.FieldName = "reg_date";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 69;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "노출유무";
            this.gridColumn4.FieldName = "is_use_nm";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 58;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "제목";
            this.gridColumn5.FieldName = "subject";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 192;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "공지 내용";
            this.gridColumn6.FieldName = "contents";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 648;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "방문건수";
            this.gridColumn7.FieldName = "visit_count";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 55;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "노출유무";
            this.gridColumn8.FieldName = "is_use";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(3, 512);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(1040, 5);
            this.splitterControl1.TabIndex = 4;
            this.splitterControl1.TabStop = false;
            // 
            // efwGroupControl1
            // 
            this.efwGroupControl1.Controls.Add(this.efwLabel5);
            this.efwGroupControl1.Controls.Add(this.txtStory_id);
            this.efwGroupControl1.Controls.Add(this.efwLabel4);
            this.efwGroupControl1.Controls.Add(this.txtVisit_count);
            this.efwGroupControl1.Controls.Add(this.btnSave);
            this.efwGroupControl1.Controls.Add(this.btnDelete);
            this.efwGroupControl1.Controls.Add(this.rbIs_use);
            this.efwGroupControl1.Controls.Add(this.txtContents);
            this.efwGroupControl1.Controls.Add(this.txtSubject);
            this.efwGroupControl1.Controls.Add(this.efwLabel3);
            this.efwGroupControl1.Controls.Add(this.dtReg_date);
            this.efwGroupControl1.Controls.Add(this.efwLabel2);
            this.efwGroupControl1.Controls.Add(this.rbLevel);
            this.efwGroupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efwGroupControl1.IsMultiLang = false;
            this.efwGroupControl1.Location = new System.Drawing.Point(3, 517);
            this.efwGroupControl1.Name = "efwGroupControl1";
            this.efwGroupControl1.Size = new System.Drawing.Size(1040, 100);
            this.efwGroupControl1.TabIndex = 5;
            this.efwGroupControl1.Text = "공지 저장";
            // 
            // efwLabel5
            // 
            this.efwLabel5.EraserGroup = null;
            this.efwLabel5.IsMultiLang = false;
            this.efwLabel5.Location = new System.Drawing.Point(207, 162);
            this.efwLabel5.Name = "efwLabel5";
            this.efwLabel5.Size = new System.Drawing.Size(46, 14);
            this.efwLabel5.TabIndex = 68;
            this.efwLabel5.Text = "스토리 ID";
            this.efwLabel5.Visible = false;
            // 
            // txtStory_id
            // 
            this.txtStory_id.EditValue = "";
            this.txtStory_id.EditValue2 = null;
            this.txtStory_id.EraserGroup = "CLR1";
            this.txtStory_id.Location = new System.Drawing.Point(270, 159);
            this.txtStory_id.Name = "txtStory_id";
            this.txtStory_id.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtStory_id.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtStory_id.RequireMessage = null;
            this.txtStory_id.Size = new System.Drawing.Size(71, 20);
            this.txtStory_id.TabIndex = 67;
            this.txtStory_id.Visible = false;
            // 
            // efwLabel4
            // 
            this.efwLabel4.EraserGroup = null;
            this.efwLabel4.IsMultiLang = false;
            this.efwLabel4.Location = new System.Drawing.Point(27, 134);
            this.efwLabel4.Name = "efwLabel4";
            this.efwLabel4.Size = new System.Drawing.Size(40, 14);
            this.efwLabel4.TabIndex = 66;
            this.efwLabel4.Text = "방문건수";
            // 
            // txtVisit_count
            // 
            this.txtVisit_count.EditValue2 = null;
            this.txtVisit_count.Enabled = false;
            this.txtVisit_count.EraserGroup = "CLR1";
            this.txtVisit_count.Location = new System.Drawing.Point(85, 131);
            this.txtVisit_count.Name = "txtVisit_count";
            this.txtVisit_count.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtVisit_count.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtVisit_count.RequireMessage = null;
            this.txtVisit_count.Size = new System.Drawing.Size(71, 20);
            this.txtVisit_count.TabIndex = 65;
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.IsMultiLang = false;
            this.btnSave.Location = new System.Drawing.Point(270, 130);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 63;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.IsMultiLang = false;
            this.btnDelete.Location = new System.Drawing.Point(189, 130);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 62;
            this.btnDelete.Text = "삭제";
            this.btnDelete.Visible = false;
            // 
            // rbIs_use
            // 
            this.rbIs_use.IsMultiLang = false;
            this.rbIs_use.Location = new System.Drawing.Point(217, 72);
            this.rbIs_use.Name = "rbIs_use";
            this.rbIs_use.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rbIs_use.Properties.Appearance.Options.UseBackColor = true;
            this.rbIs_use.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.rbIs_use.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.rbIs_use.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Y", "노출"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("N", "미노출")});
            this.rbIs_use.RequireMessage = null;
            this.rbIs_use.Size = new System.Drawing.Size(128, 24);
            this.rbIs_use.TabIndex = 61;
            // 
            // txtContents
            // 
            this.txtContents.ByteLength = 200;
            this.txtContents.EraserGroup = "CLR1";
            this.txtContents.Location = new System.Drawing.Point(382, 24);
            this.txtContents.Name = "txtContents";
            this.txtContents.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtContents.Properties.Appearance.Options.UseFont = true;
            this.txtContents.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtContents.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtContents.Size = new System.Drawing.Size(599, 198);
            this.txtContents.TabIndex = 60;
            // 
            // txtSubject
            // 
            this.txtSubject.EditValue2 = null;
            this.txtSubject.EraserGroup = "CLR1";
            this.txtSubject.Location = new System.Drawing.Point(85, 103);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtSubject.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSubject.RequireMessage = null;
            this.txtSubject.Size = new System.Drawing.Size(260, 20);
            this.txtSubject.TabIndex = 8;
            // 
            // efwLabel3
            // 
            this.efwLabel3.EraserGroup = null;
            this.efwLabel3.IsMultiLang = false;
            this.efwLabel3.Location = new System.Drawing.Point(27, 106);
            this.efwLabel3.Name = "efwLabel3";
            this.efwLabel3.Size = new System.Drawing.Size(20, 14);
            this.efwLabel3.TabIndex = 7;
            this.efwLabel3.Text = "제목";
            // 
            // dtReg_date
            // 
            this.dtReg_date.EditValue = new System.DateTime(2019, 6, 7, 0, 0, 0, 0);
            this.dtReg_date.IsRequire = true;
            this.dtReg_date.Location = new System.Drawing.Point(85, 74);
            this.dtReg_date.Name = "dtReg_date";
            this.dtReg_date.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(226)))));
            this.dtReg_date.Properties.Appearance.Options.UseBackColor = true;
            this.dtReg_date.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.dtReg_date.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dtReg_date.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtReg_date.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtReg_date.Size = new System.Drawing.Size(114, 20);
            this.dtReg_date.TabIndex = 6;
            // 
            // efwLabel2
            // 
            this.efwLabel2.EraserGroup = null;
            this.efwLabel2.IsMultiLang = false;
            this.efwLabel2.Location = new System.Drawing.Point(27, 77);
            this.efwLabel2.Name = "efwLabel2";
            this.efwLabel2.Size = new System.Drawing.Size(30, 14);
            this.efwLabel2.TabIndex = 2;
            this.efwLabel2.Text = "작성일";
            // 
            // rbLevel
            // 
            this.rbLevel.IsMultiLang = false;
            this.rbLevel.Location = new System.Drawing.Point(23, 36);
            this.rbLevel.Name = "rbLevel";
            this.rbLevel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rbLevel.Properties.Appearance.Options.UseBackColor = true;
            this.rbLevel.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.rbLevel.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.rbLevel.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("215", "도마공지"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("238", "G 메니저 공지"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("400", "G 멀티샵 공지")});
            this.rbLevel.RequireMessage = null;
            this.rbLevel.Size = new System.Drawing.Size(322, 24);
            this.rbLevel.TabIndex = 1;
            // 
            // frmDN22
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.efwGroupControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.efwGridControl1);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmDN22";
            this.Size = new System.Drawing.Size(1046, 617);
            this.Load += new System.EventHandler(this.frmDN22_Load);
            this.Controls.SetChildIndex(this.efwPanelControl1, 0);
            this.Controls.SetChildIndex(this.efwGridControl1, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            this.Controls.SetChildIndex(this.efwGroupControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbLevelQ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGroupControl1)).EndInit();
            this.efwGroupControl1.ResumeLayout(false);
            this.efwGroupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStory_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVisit_count.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbIs_use.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContents.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtReg_date.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtReg_date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbLevel.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwRadioGroup rbLevelQ;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private Easy.Framework.WinForm.Control.efwGroupControl efwGroupControl1;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel2;
        private Easy.Framework.WinForm.Control.efwRadioGroup rbLevel;
        private Easy.Framework.WinForm.Control.efwDateEdit dtReg_date;
        private Easy.Framework.WinForm.Control.efwTextEdit txtSubject;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel3;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnSave;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnDelete;
        private Easy.Framework.WinForm.Control.efwRadioGroup rbIs_use;
        private Easy.Framework.WinForm.Control.efwMemoEdit txtContents;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel5;
        private Easy.Framework.WinForm.Control.efwTextEdit txtStory_id;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel4;
        private Easy.Framework.WinForm.Control.efwTextEdit txtVisit_count;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}