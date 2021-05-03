namespace YL_RM.BizFrm.Dlg
{
    partial class frmGMember_info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGMember_info));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo4 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo5 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo6 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.txtSearch_Name = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwLabel2 = new Easy.Framework.WinForm.Control.efwLabel();
            this.lblMType = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            this.btnClose = new Easy.Framework.WinForm.Control.efwSimpleButton();
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
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch_Name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch1);
            this.panel1.Controls.Add(this.txtSearch_Name);
            this.panel1.Controls.Add(this.efwLabel2);
            this.panel1.Controls.Add(this.lblMType);
            this.panel1.Controls.Add(this.efwLabel1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 84);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnSearch1
            // 
            this.btnSearch1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch1.ButtonType = Easy.Framework.Util.BtnType.Search;
            this.btnSearch1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch1.ImageOptions.Image")));
            this.btnSearch1.IsMultiLang = false;
            this.btnSearch1.Location = new System.Drawing.Point(618, 12);
            this.btnSearch1.Name = "btnSearch1";
            this.btnSearch1.Size = new System.Drawing.Size(75, 27);
            this.btnSearch1.TabIndex = 8;
            this.btnSearch1.Text = "조회";
            this.btnSearch1.ToolTip = "조회";
            this.btnSearch1.Click += new System.EventHandler(this.btnSearch1_Click);
            // 
            // txtSearch_Name
            // 
            this.txtSearch_Name.EditValue2 = null;
            this.txtSearch_Name.Location = new System.Drawing.Point(89, 15);
            this.txtSearch_Name.Name = "txtSearch_Name";
            this.txtSearch_Name.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtSearch_Name.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSearch_Name.RequireMessage = null;
            this.txtSearch_Name.Size = new System.Drawing.Size(206, 20);
            this.txtSearch_Name.TabIndex = 7;
            this.txtSearch_Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_Name_KeyDown_1);
            // 
            // efwLabel2
            // 
            this.efwLabel2.EraserGroup = null;
            this.efwLabel2.IsMultiLang = false;
            this.efwLabel2.Location = new System.Drawing.Point(31, 19);
            this.efwLabel2.Name = "efwLabel2";
            this.efwLabel2.Size = new System.Drawing.Size(30, 14);
            this.efwLabel2.TabIndex = 5;
            this.efwLabel2.Text = "검색명";
            // 
            // lblMType
            // 
            this.lblMType.EraserGroup = null;
            this.lblMType.IsMultiLang = false;
            this.lblMType.Location = new System.Drawing.Point(228, 45);
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
            this.efwLabel1.Location = new System.Drawing.Point(31, 45);
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
            this.btnClose.Location = new System.Drawing.Point(698, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "취소";
            this.btnClose.ToolTip = "취소";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // efwGridControl1
            // 
            this.efwGridControl1.BindSet = null;
            this.efwGridControl1.DBName = "";
            serviceInfo4.InstanceName = "";
            serviceInfo4.IsUserIDAdd = true;
            serviceInfo4.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo4.ParamsInfo")));
            serviceInfo4.ProcName = "";
            serviceInfo4.UserParams = null;
            this.efwGridControl1.DeleteServiceInfo = serviceInfo4;
            this.efwGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            serviceInfo5.InstanceName = "";
            serviceInfo5.IsUserIDAdd = true;
            serviceInfo5.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo5.ParamsInfo")));
            serviceInfo5.ProcName = "";
            serviceInfo5.UserParams = null;
            this.efwGridControl1.InsertServiceInfo = serviceInfo5;
            this.efwGridControl1.IsAddExcelBtn = true;
            this.efwGridControl1.isAddPrintBtn = true;
            this.efwGridControl1.IsEditable = false;
            this.efwGridControl1.IsMultiLang = false;
            this.efwGridControl1.Location = new System.Drawing.Point(0, 84);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoEdit1});
            this.efwGridControl1.Size = new System.Drawing.Size(800, 441);
            this.efwGridControl1.TabIndex = 12;
            this.efwGridControl1.TableName = "";
            serviceInfo6.InstanceName = "";
            serviceInfo6.IsUserIDAdd = true;
            serviceInfo6.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo6.ParamsInfo")));
            serviceInfo6.ProcName = "";
            serviceInfo6.UserParams = null;
            this.efwGridControl1.UpdateServiceInfo = serviceInfo6;
            this.efwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.efwGridControl1.DoubleClick += new System.EventHandler(this.efwGridControl1_DoubleClick);
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
            this.gridView1.DetailHeight = 408;
            this.gridView1.GridControl = this.efwGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "idx";
            this.gridColumn1.FieldName = "idx";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "u_id";
            this.gridColumn2.FieldName = "u_id";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "이름";
            this.gridColumn3.FieldName = "u_name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "닉네임";
            this.gridColumn4.FieldName = "u_nickname";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "로그인ID";
            this.gridColumn5.FieldName = "login_id";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "상호명";
            this.gridColumn6.FieldName = "gshop_name";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "전화번호";
            this.gridColumn7.FieldName = "tel_no";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "주소";
            this.gridColumn8.FieldName = "road_addr";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // frmGMember_info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.efwGridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmGMember_info";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "회원검색";
            this.Load += new System.EventHandler(this.frmMember_info_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch_Name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnSearch1;
        private Easy.Framework.WinForm.Control.efwTextEdit txtSearch_Name;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel2;
        private Easy.Framework.WinForm.Control.efwLabel lblMType;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel1;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnClose;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}