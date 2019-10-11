namespace YL_DONUT.BizFrm
{
    partial class frmDN20_Pop01
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
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo4 = new Easy.Framework.WinForm.Control.ServiceInfo();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDN20_Pop01));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo5 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo6 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtFileName = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.efwLabel2 = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.lblCnt = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // efwGridControl1
            // 
            this.efwGridControl1.BindSet = null;
            this.efwGridControl1.DBName = "";
            serviceInfo4.InstanceName = "";
            serviceInfo4.IsUserIDAdd = true;
            serviceInfo4.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo4.ParamsInfo")));
            serviceInfo4.ProcName = "";
            serviceInfo4.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo4.UserParams")));
            this.efwGridControl1.DeleteServiceInfo = serviceInfo4;
            this.efwGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            serviceInfo5.InstanceName = "";
            serviceInfo5.IsUserIDAdd = true;
            serviceInfo5.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo5.ParamsInfo")));
            serviceInfo5.ProcName = "";
            serviceInfo5.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo5.UserParams")));
            this.efwGridControl1.InsertServiceInfo = serviceInfo5;
            this.efwGridControl1.IsAddExcelBtn = true;
            this.efwGridControl1.isAddPrintBtn = true;
            this.efwGridControl1.IsMultiLang = false;
            this.efwGridControl1.Location = new System.Drawing.Point(0, 48);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.Size = new System.Drawing.Size(460, 402);
            this.efwGridControl1.TabIndex = 4;
            this.efwGridControl1.TableName = "";
            serviceInfo6.InstanceName = "";
            serviceInfo6.IsUserIDAdd = true;
            serviceInfo6.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo6.ParamsInfo")));
            serviceInfo6.ProcName = "";
            serviceInfo6.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo6.UserParams")));
            this.efwGridControl1.UpdateServiceInfo = serviceInfo6;
            this.efwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.efwGridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // txtFileName
            // 
            this.txtFileName.EditValue2 = null;
            this.txtFileName.Location = new System.Drawing.Point(11, 15);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtFileName.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtFileName.RequireMessage = null;
            this.txtFileName.Size = new System.Drawing.Size(16, 20);
            this.txtFileName.TabIndex = 48;
            this.txtFileName.Visible = false;
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.efwLabel2);
            this.efwPanelControl1.Controls.Add(this.efwLabel1);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton2);
            this.efwPanelControl1.Controls.Add(this.lblCnt);
            this.efwPanelControl1.Controls.Add(this.txtFileName);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton1);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(460, 48);
            this.efwPanelControl1.TabIndex = 3;
            // 
            // efwLabel2
            // 
            this.efwLabel2.EraserGroup = null;
            this.efwLabel2.IsMultiLang = false;
            this.efwLabel2.Location = new System.Drawing.Point(76, 19);
            this.efwLabel2.Name = "efwLabel2";
            this.efwLabel2.Size = new System.Drawing.Size(20, 14);
            this.efwLabel2.TabIndex = 52;
            this.efwLabel2.Text = "건수";
            // 
            // efwLabel1
            // 
            this.efwLabel1.EraserGroup = null;
            this.efwLabel1.IsMultiLang = false;
            this.efwLabel1.Location = new System.Drawing.Point(53, 22);
            this.efwLabel1.Name = "efwLabel1";
            this.efwLabel1.Size = new System.Drawing.Size(0, 14);
            this.efwLabel1.TabIndex = 51;
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("efwSimpleButton2.ImageOptions.Image")));
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(367, 11);
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
            this.lblCnt.Location = new System.Drawing.Point(114, 16);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(10, 18);
            this.lblCnt.TabIndex = 49;
            this.lblCnt.Text = "0";
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("efwSimpleButton1.ImageOptions.Image")));
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(187, 11);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(163, 28);
            this.efwSimpleButton1.TabIndex = 0;
            this.efwSimpleButton1.Text = "EXCEL DATA 가져오기";
            this.efwSimpleButton1.Click += new System.EventHandler(this.efwSimpleButton1_Click);
            // 
            // frmDN20_Pop01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 450);
            this.Controls.Add(this.efwGridControl1);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmDN20_Pop01";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "운송장번호 EXCEL UPDATE";
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.efwPanelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Easy.Framework.WinForm.Control.efwTextEdit txtFileName;
        private Easy.Framework.WinForm.Control.efwPanelControl efwPanelControl1;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel2;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
        private Easy.Framework.WinForm.Control.efwLabel lblCnt;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}