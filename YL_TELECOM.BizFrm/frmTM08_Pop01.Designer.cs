﻿namespace YL_TELECOM.BizFrm
{
    partial class frmTM08_Pop01
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTM08_Pop01));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo1 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo2 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo3 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.efwPanelControl1 = new Easy.Framework.WinForm.Control.efwPanelControl();
            this.dtS_DATE = new Easy.Framework.WinForm.Control.efwDateEdit();
            this.efwLabel3 = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwLabel2 = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.lblCnt = new Easy.Framework.WinForm.Control.efwLabel();
            this.txtFileName = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.efwLabel4 = new Easy.Framework.WinForm.Control.efwLabel();
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).BeginInit();
            this.efwPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtS_DATE.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtS_DATE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // efwPanelControl1
            // 
            this.efwPanelControl1.Controls.Add(this.efwLabel4);
            this.efwPanelControl1.Controls.Add(this.dtS_DATE);
            this.efwPanelControl1.Controls.Add(this.efwLabel3);
            this.efwPanelControl1.Controls.Add(this.efwLabel2);
            this.efwPanelControl1.Controls.Add(this.efwLabel1);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton2);
            this.efwPanelControl1.Controls.Add(this.lblCnt);
            this.efwPanelControl1.Controls.Add(this.txtFileName);
            this.efwPanelControl1.Controls.Add(this.efwSimpleButton1);
            this.efwPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.efwPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.efwPanelControl1.Name = "efwPanelControl1";
            this.efwPanelControl1.Size = new System.Drawing.Size(582, 72);
            this.efwPanelControl1.TabIndex = 4;
            this.efwPanelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.efwPanelControl1_Paint);
            // 
            // dtS_DATE
            // 
            this.dtS_DATE.EditValue = new System.DateTime(2019, 7, 1, 0, 0, 0, 0);
            this.dtS_DATE.IsRequire = true;
            this.dtS_DATE.Location = new System.Drawing.Point(72, 16);
            this.dtS_DATE.MaximumSize = new System.Drawing.Size(121, 0);
            this.dtS_DATE.MinimumSize = new System.Drawing.Size(121, 0);
            this.dtS_DATE.Name = "dtS_DATE";
            this.dtS_DATE.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(226)))));
            this.dtS_DATE.Properties.Appearance.Options.UseBackColor = true;
            this.dtS_DATE.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.dtS_DATE.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dtS_DATE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtS_DATE.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtS_DATE.Properties.Mask.EditMask = "yyyy-MM";
            this.dtS_DATE.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtS_DATE.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.dtS_DATE.Size = new System.Drawing.Size(121, 20);
            this.dtS_DATE.TabIndex = 59;
            // 
            // efwLabel3
            // 
            this.efwLabel3.EraserGroup = null;
            this.efwLabel3.IsMultiLang = false;
            this.efwLabel3.Location = new System.Drawing.Point(12, 19);
            this.efwLabel3.Name = "efwLabel3";
            this.efwLabel3.Size = new System.Drawing.Size(40, 14);
            this.efwLabel3.TabIndex = 58;
            this.efwLabel3.Text = "재고년월";
            // 
            // efwLabel2
            // 
            this.efwLabel2.EraserGroup = null;
            this.efwLabel2.IsMultiLang = false;
            this.efwLabel2.Location = new System.Drawing.Point(514, 19);
            this.efwLabel2.Name = "efwLabel2";
            this.efwLabel2.Size = new System.Drawing.Size(20, 14);
            this.efwLabel2.TabIndex = 52;
            this.efwLabel2.Text = "건수";
            // 
            // efwLabel1
            // 
            this.efwLabel1.EraserGroup = null;
            this.efwLabel1.IsMultiLang = false;
            this.efwLabel1.Location = new System.Drawing.Point(491, 22);
            this.efwLabel1.Name = "efwLabel1";
            this.efwLabel1.Size = new System.Drawing.Size(0, 14);
            this.efwLabel1.TabIndex = 51;
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("efwSimpleButton2.ImageOptions.Image")));
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(393, 11);
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
            this.lblCnt.Location = new System.Drawing.Point(552, 16);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(10, 18);
            this.lblCnt.TabIndex = 49;
            this.lblCnt.Text = "0";
            // 
            // txtFileName
            // 
            this.txtFileName.EditValue2 = null;
            this.txtFileName.Location = new System.Drawing.Point(480, 16);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtFileName.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtFileName.RequireMessage = null;
            this.txtFileName.Size = new System.Drawing.Size(16, 20);
            this.txtFileName.TabIndex = 48;
            this.txtFileName.Visible = false;
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("efwSimpleButton1.ImageOptions.Image")));
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(225, 11);
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
            this.efwGridControl1.Location = new System.Drawing.Point(0, 72);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.Size = new System.Drawing.Size(582, 503);
            this.efwGridControl1.TabIndex = 5;
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
            // efwLabel4
            // 
            this.efwLabel4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.efwLabel4.Appearance.Options.UseForeColor = true;
            this.efwLabel4.EraserGroup = null;
            this.efwLabel4.IsMultiLang = false;
            this.efwLabel4.Location = new System.Drawing.Point(12, 46);
            this.efwLabel4.Name = "efwLabel4";
            this.efwLabel4.Size = new System.Drawing.Size(213, 14);
            this.efwLabel4.TabIndex = 60;
            this.efwLabel4.Text = "유심 SER_NO는 \'000000\' 를 입력해주세요.";
            // 
            // frmTM08_Pop01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 575);
            this.Controls.Add(this.efwGridControl1);
            this.Controls.Add(this.efwPanelControl1);
            this.Name = "frmTM08_Pop01";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "기초재고 EXCEL UPDATE";
            this.Load += new System.EventHandler(this.frmTM08_Pop01_Load);
            ((System.ComponentModel.ISupportInitialize)(this.efwPanelControl1)).EndInit();
            this.efwPanelControl1.ResumeLayout(false);
            this.efwPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtS_DATE.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtS_DATE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
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
        private Easy.Framework.WinForm.Control.efwDateEdit dtS_DATE;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel3;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel4;
    }
}