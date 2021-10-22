namespace YL_DONUT.BizFrm
{
    partial class frmDN24_Pop01
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDN24_Pop01));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo1 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo2 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo3 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo4 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo5 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo6 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbO_Sub_Company = new Easy.Framework.WinForm.Control.efwLookUpEdit();
            this.efwLabel2 = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.lblCnt = new Easy.Framework.WinForm.Control.efwLabel();
            this.txtFileName = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.efwXtraTabControl1 = new Easy.Framework.WinForm.Control.efwXtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.efwGridControl2 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbO_Sub_Company.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwXtraTabControl1)).BeginInit();
            this.efwXtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.label1);
            this.efwPanelControl1.Controls.Add(this.cmbO_Sub_Company);
            this.efwPanelControl1.Controls.Add(this.efwLabel2);
            this.efwPanelControl1.Controls.Add(this.efwLabel1);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton2);
            this.efwPanelControl1.Controls.Add(this.lblCnt);
            this.efwPanelControl1.Controls.Add(this.txtFileName);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton1);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(1199, 56);
            this.efwPanelControl1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 14);
            this.label1.TabIndex = 98;
            this.label1.Text = "온라인 몰";
            // 
            // cmbO_Sub_Company
            // 
            childHierarchy1.CodeCtrl = null;
            childHierarchy1.DbName = null;
            childHierarchy1.SpName = null;
            this.cmbO_Sub_Company.ChildHierarchyInfo = childHierarchy1;
            hierarchy1.DbName = null;
            hierarchy1.SpName = null;
            this.cmbO_Sub_Company.HierarchyInfo = hierarchy1;
            this.cmbO_Sub_Company.IsMultiLang = false;
            this.cmbO_Sub_Company.Location = new System.Drawing.Point(174, 19);
            this.cmbO_Sub_Company.MasterCode = "E03.PAY_GBN";
            this.cmbO_Sub_Company.Name = "cmbO_Sub_Company";
            this.cmbO_Sub_Company.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbO_Sub_Company.Size = new System.Drawing.Size(175, 20);
            this.cmbO_Sub_Company.TabIndex = 97;
            this.cmbO_Sub_Company.EditValueChanged += new System.EventHandler(this.cmbO_Sub_Company_EditValueChanged);
            // 
            // efwLabel2
            // 
            this.efwLabel2.EraserGroup = null;
            this.efwLabel2.IsMultiLang = false;
            this.efwLabel2.Location = new System.Drawing.Point(18, 22);
            this.efwLabel2.Name = "efwLabel2";
            this.efwLabel2.Size = new System.Drawing.Size(20, 14);
            this.efwLabel2.TabIndex = 52;
            this.efwLabel2.Text = "건수";
            // 
            // efwLabel1
            // 
            this.efwLabel1.EraserGroup = null;
            this.efwLabel1.IsMultiLang = false;
            this.efwLabel1.Location = new System.Drawing.Point(559, 22);
            this.efwLabel1.Name = "efwLabel1";
            this.efwLabel1.Size = new System.Drawing.Size(0, 14);
            this.efwLabel1.TabIndex = 51;
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("efwSimpleButton2.ImageOptions.Image")));
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(541, 15);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(82, 28);
            this.efwSimpleButton2.TabIndex = 50;
            this.efwSimpleButton2.Text = "저장";
            this.efwSimpleButton2.Click += new System.EventHandler(this.efwSimpleButton2_Click);
            // 
            // lblCnt
            // 
            this.lblCnt.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblCnt.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblCnt.Appearance.Options.UseFont = true;
            this.lblCnt.Appearance.Options.UseForeColor = true;
            this.lblCnt.EraserGroup = null;
            this.lblCnt.IsMultiLang = false;
            this.lblCnt.Location = new System.Drawing.Point(56, 20);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(10, 18);
            this.lblCnt.TabIndex = 49;
            this.lblCnt.Text = "0";
            // 
            // txtFileName
            // 
            this.txtFileName.EditValue2 = null;
            this.txtFileName.Location = new System.Drawing.Point(644, 19);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtFileName.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtFileName.RequireMessage = null;
            this.txtFileName.Size = new System.Drawing.Size(78, 20);
            this.txtFileName.TabIndex = 48;
            this.txtFileName.Visible = false;
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("efwSimpleButton1.ImageOptions.Image")));
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(369, 15);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(151, 28);
            this.efwSimpleButton1.TabIndex = 0;
            this.efwSimpleButton1.Text = "EXCEL DATA 가져오기";
            this.efwSimpleButton1.Click += new System.EventHandler(this.efwSimpleButton1_Click);
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
            this.efwGridControl1.Location = new System.Drawing.Point(5, 5);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.Size = new System.Drawing.Size(1183, 460);
            this.efwGridControl1.TabIndex = 6;
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
            this.gridView1.GridControl = this.efwGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // efwXtraTabControl1
            // 
            this.efwXtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efwXtraTabControl1.IsMultiLang = false;
            this.efwXtraTabControl1.Location = new System.Drawing.Point(0, 56);
            this.efwXtraTabControl1.Name = "efwXtraTabControl1";
            this.efwXtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.efwXtraTabControl1.Size = new System.Drawing.Size(1199, 501);
            this.efwXtraTabControl1.TabIndex = 45;
            this.efwXtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.efwGridControl1);
            this.xtraTabPage1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage1.ImageOptions.Image")));
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.xtraTabPage1.Size = new System.Drawing.Size(1193, 470);
            this.xtraTabPage1.Text = "쿠팡        ";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.efwGridControl2);
            this.xtraTabPage2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage2.ImageOptions.Image")));
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.xtraTabPage2.Size = new System.Drawing.Size(1193, 470);
            this.xtraTabPage2.Text = "네이버스마트스토어   ";
            // 
            // efwGridControl2
            // 
            this.efwGridControl2.BindSet = null;
            this.efwGridControl2.DBName = "";
            serviceInfo4.InstanceName = "";
            serviceInfo4.IsUserIDAdd = true;
            serviceInfo4.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo4.ParamsInfo")));
            serviceInfo4.ProcName = "";
            serviceInfo4.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo4.UserParams")));
            this.efwGridControl2.DeleteServiceInfo = serviceInfo4;
            this.efwGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            serviceInfo5.InstanceName = "";
            serviceInfo5.IsUserIDAdd = true;
            serviceInfo5.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo5.ParamsInfo")));
            serviceInfo5.ProcName = "";
            serviceInfo5.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo5.UserParams")));
            this.efwGridControl2.InsertServiceInfo = serviceInfo5;
            this.efwGridControl2.IsAddExcelBtn = true;
            this.efwGridControl2.isAddPrintBtn = true;
            this.efwGridControl2.IsMultiLang = false;
            this.efwGridControl2.Location = new System.Drawing.Point(5, 5);
            this.efwGridControl2.MainView = this.gridView2;
            this.efwGridControl2.Name = "efwGridControl2";
            this.efwGridControl2.NowRowHandle = 0;
            this.efwGridControl2.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl2.PKColumns")));
            this.efwGridControl2.PrevRowHandle = -2147483648;
            this.efwGridControl2.Size = new System.Drawing.Size(1183, 460);
            this.efwGridControl2.TabIndex = 7;
            this.efwGridControl2.TableName = "";
            serviceInfo6.InstanceName = "";
            serviceInfo6.IsUserIDAdd = true;
            serviceInfo6.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo6.ParamsInfo")));
            serviceInfo6.ProcName = "";
            serviceInfo6.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo6.UserParams")));
            this.efwGridControl2.UpdateServiceInfo = serviceInfo6;
            this.efwGridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.efwGridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // frmDN24_Pop01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 557);
            this.Controls.Add(this.efwXtraTabControl1);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmDN24_Pop01";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "온라인몰 EXCEL UPLOAD";
            this.Load += new System.EventHandler(this.frmDN24_Pop01_Load);
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.efwPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbO_Sub_Company.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwXtraTabControl1)).EndInit();
            this.efwXtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel2;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
        private Easy.Framework.WinForm.Control.efwLabel lblCnt;
        private Easy.Framework.WinForm.Control.efwTextEdit txtFileName;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private Easy.Framework.WinForm.Control.efwLookUpEdit cmbO_Sub_Company;
        private Easy.Framework.WinForm.Control.efwXtraTabControl efwXtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}