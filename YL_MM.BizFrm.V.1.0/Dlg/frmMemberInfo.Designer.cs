﻿namespace YL_MM.BizFrm.Dlg
{
    partial class frmMemberInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMemberInfo));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo1 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo2 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo3 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbQ1 = new Easy.Framework.WinForm.Control.efwLookUpEdit();
            this.btnSearch1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.txtSearch = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwLabel2 = new Easy.Framework.WinForm.Control.efwLabel();
            this.lblMType = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            this.btnClose = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwGroupControl1 = new Easy.Framework.WinForm.Control.efwGroupControl();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQ1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGroupControl1)).BeginInit();
            this.efwGroupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbQ1);
            this.panel1.Controls.Add(this.btnSearch1);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.efwLabel2);
            this.panel1.Controls.Add(this.lblMType);
            this.panel1.Controls.Add(this.efwLabel1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 65);
            this.panel1.TabIndex = 3;
            // 
            // cmbQ1
            // 
            childHierarchy1.CodeCtrl = null;
            childHierarchy1.DbName = null;
            childHierarchy1.SpName = null;
            this.cmbQ1.ChildHierarchyInfo = childHierarchy1;
            hierarchy1.DbName = null;
            hierarchy1.SpName = null;
            this.cmbQ1.HierarchyInfo = hierarchy1;
            this.cmbQ1.IsMultiLang = false;
            this.cmbQ1.Location = new System.Drawing.Point(66, 10);
            this.cmbQ1.MasterCode = "E03.MEMBERQ_GBN1";
            this.cmbQ1.Name = "cmbQ1";
            this.cmbQ1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQ1.Size = new System.Drawing.Size(100, 20);
            this.cmbQ1.TabIndex = 9;
            // 
            // btnSearch1
            // 
            this.btnSearch1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch1.ButtonType = Easy.Framework.Util.BtnType.Search;
            this.btnSearch1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch1.ImageOptions.Image")));
            this.btnSearch1.IsMultiLang = false;
            this.btnSearch1.Location = new System.Drawing.Point(527, 9);
            this.btnSearch1.Name = "btnSearch1";
            this.btnSearch1.Size = new System.Drawing.Size(75, 23);
            this.btnSearch1.TabIndex = 8;
            this.btnSearch1.Text = "조회";
            this.btnSearch1.Click += new System.EventHandler(this.BtnSearch1_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.EditValue2 = null;
            this.txtSearch.Location = new System.Drawing.Point(171, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtSearch.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSearch.RequireMessage = null;
            this.txtSearch.Size = new System.Drawing.Size(206, 20);
            this.txtSearch.TabIndex = 7;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // efwLabel2
            // 
            this.efwLabel2.EraserGroup = null;
            this.efwLabel2.IsMultiLang = false;
            this.efwLabel2.Location = new System.Drawing.Point(31, 13);
            this.efwLabel2.Name = "efwLabel2";
            this.efwLabel2.Size = new System.Drawing.Size(20, 14);
            this.efwLabel2.TabIndex = 5;
            this.efwLabel2.Text = "검색";
            // 
            // lblMType
            // 
            this.lblMType.EraserGroup = null;
            this.lblMType.IsMultiLang = false;
            this.lblMType.Location = new System.Drawing.Point(282, 40);
            this.lblMType.Name = "lblMType";
            this.lblMType.Size = new System.Drawing.Size(56, 14);
            this.lblMType.TabIndex = 4;
            this.lblMType.Text = "efwLabel2";
            this.lblMType.Visible = false;
            // 
            // efwLabel1
            // 
            this.efwLabel1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.efwLabel1.Appearance.Options.UseForeColor = true;
            this.efwLabel1.EraserGroup = null;
            this.efwLabel1.IsMultiLang = false;
            this.efwLabel1.Location = new System.Drawing.Point(68, 44);
            this.efwLabel1.Name = "efwLabel1";
            this.efwLabel1.Size = new System.Drawing.Size(168, 14);
            this.efwLabel1.TabIndex = 3;
            this.efwLabel1.Text = "※ 회원을 터블클릭하여 선택하세요!";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ButtonType = Easy.Framework.Util.BtnType.Close;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.IsMultiLang = false;
            this.btnClose.Location = new System.Drawing.Point(607, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "취소";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // efwGroupControl1
            // 
            this.efwGroupControl1.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("efwGroupControl1.CaptionImageOptions.Image")));
            this.efwGroupControl1.Controls.Add(this.efwGridControl1);
            this.efwGroupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efwGroupControl1.IsMultiLang = false;
            this.efwGroupControl1.Location = new System.Drawing.Point(5, 70);
            this.efwGroupControl1.Name = "efwGroupControl1";
            this.efwGroupControl1.Size = new System.Drawing.Size(696, 469);
            this.efwGroupControl1.TabIndex = 4;
            this.efwGroupControl1.Text = "회원목록";
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
            this.efwGridControl1.IsEditable = false;
            this.efwGridControl1.IsMultiLang = false;
            this.efwGridControl1.Location = new System.Drawing.Point(2, 23);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.Size = new System.Drawing.Size(692, 444);
            this.efwGridControl1.TabIndex = 5;
            this.efwGridControl1.TableName = "";
            serviceInfo3.InstanceName = "";
            serviceInfo3.IsUserIDAdd = true;
            serviceInfo3.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo3.ParamsInfo")));
            serviceInfo3.ProcName = "";
            serviceInfo3.UserParams = null;
            this.efwGridControl1.UpdateServiceInfo = serviceInfo3;
            this.efwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.efwGridControl1.DoubleClick += new System.EventHandler(this.EfwGridControl1_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.efwGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "회원구분";
            this.gridColumn1.FieldName = "u_chef_level";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "이름";
            this.gridColumn5.FieldName = "u_name";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "아이디";
            this.gridColumn2.FieldName = "user_id";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "닉네임";
            this.gridColumn6.FieldName = "u_nickname";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "idx";
            this.gridColumn3.FieldName = "idx";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "u_id";
            this.gridColumn4.FieldName = "u_id";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // frmMemberInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 544);
            this.Controls.Add(this.efwGroupControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmMemberInfo";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "회원정보";
            this.Load += new System.EventHandler(this.FrmMemberInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQ1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGroupControl1)).EndInit();
            this.efwGroupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnClose;
        private Easy.Framework.WinForm.Control.efwGroupControl efwGroupControl1;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel1;
        private Easy.Framework.WinForm.Control.efwLabel lblMType;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel2;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnSearch1;
        private Easy.Framework.WinForm.Control.efwTextEdit txtSearch;
        private Easy.Framework.WinForm.Control.efwLookUpEdit cmbQ1;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}