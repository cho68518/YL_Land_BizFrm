namespace YL_TELECOM.BizFrm.Dlg
{
    partial class frmZipNoInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZipNoInfo));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo4 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo5 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo6 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.txtSch = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            this.txtCOMPANYCD = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.btnClose = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCOMPANYCD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtSch);
            this.panel1.Controls.Add(this.efwLabel1);
            this.panel1.Controls.Add(this.txtCOMPANYCD);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 49);
            this.panel1.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ButtonType = Easy.Framework.Util.BtnType.Search;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.IsMultiLang = false;
            this.btnSearch.Location = new System.Drawing.Point(300, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 27);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "검색";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSch
            // 
            this.txtSch.EditValue2 = null;
            this.txtSch.Location = new System.Drawing.Point(86, 13);
            this.txtSch.Name = "txtSch";
            this.txtSch.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtSch.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSch.RequireMessage = null;
            this.txtSch.Size = new System.Drawing.Size(164, 20);
            this.txtSch.TabIndex = 5;
            this.txtSch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSch_KeyDown);
            // 
            // efwLabel1
            // 
            this.efwLabel1.EraserGroup = null;
            this.efwLabel1.IsMultiLang = false;
            this.efwLabel1.Location = new System.Drawing.Point(44, 16);
            this.efwLabel1.Name = "efwLabel1";
            this.efwLabel1.Size = new System.Drawing.Size(30, 14);
            this.efwLabel1.TabIndex = 4;
            this.efwLabel1.Text = "검색어";
            // 
            // txtCOMPANYCD
            // 
            this.txtCOMPANYCD.EditValue2 = null;
            this.txtCOMPANYCD.Enabled = false;
            this.txtCOMPANYCD.Location = new System.Drawing.Point(254, 14);
            this.txtCOMPANYCD.Name = "txtCOMPANYCD";
            this.txtCOMPANYCD.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtCOMPANYCD.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCOMPANYCD.RequireMessage = null;
            this.txtCOMPANYCD.Size = new System.Drawing.Size(17, 20);
            this.txtCOMPANYCD.TabIndex = 3;
            this.txtCOMPANYCD.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ButtonType = Easy.Framework.Util.BtnType.Close;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.IsMultiLang = false;
            this.btnClose.Location = new System.Drawing.Point(380, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "취소";
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
            this.efwGridControl1.Location = new System.Drawing.Point(0, 49);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2});
            this.efwGridControl1.Size = new System.Drawing.Size(466, 476);
            this.efwGridControl1.TabIndex = 9;
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
            this.gridColumn2});
            this.gridView1.DetailHeight = 408;
            this.gridView1.GridControl = this.efwGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "우편번호";
            this.gridColumn1.FieldName = "zipNo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "주소";
            this.gridColumn2.FieldName = "roadAddr";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
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
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // frmZipNoInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 525);
            this.Controls.Add(this.efwGridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmZipNoInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "주소검색";
            this.Load += new System.EventHandler(this.frmZipNoInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCOMPANYCD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnSearch;
        private Easy.Framework.WinForm.Control.efwTextEdit txtSch;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel1;
        private Easy.Framework.WinForm.Control.efwTextEdit txtCOMPANYCD;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnClose;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
    }
}