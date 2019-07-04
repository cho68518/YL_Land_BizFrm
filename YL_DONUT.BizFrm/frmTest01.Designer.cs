namespace YL_DONUT.BizFrm
{
    partial class frmTest01
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest01));
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo2 = new Easy.Framework.WinForm.Control.ServiceInfo();
            Easy.Framework.WinForm.Control.ServiceInfo serviceInfo3 = new Easy.Framework.WinForm.Control.ServiceInfo();
            this.accordionControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.panel2 = new System.Windows.Forms.Panel();
            this.navBarControl2 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem3 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem4 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarSeparatorItem1 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.btnSet = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwTextEdit1 = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.txtAddr = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwMemoEdit1 = new Easy.Framework.WinForm.Control.efwMemoEdit();
            this.efwSimpleButton2 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwGridControl1 = new Easy.Framework.WinForm.Control.efwGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.efwSimpleButton3 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl2)).BeginInit();
            this.navBarControl2.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwTextEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwMemoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // accordionControl
            // 
            this.accordionControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accordionControl.Location = new System.Drawing.Point(3, 35);
            this.accordionControl.Name = "accordionControl";
            this.accordionControl.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl.Size = new System.Drawing.Size(297, 638);
            this.accordionControl.TabIndex = 2;
            this.accordionControl.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Element1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.efwSimpleButton3);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.efwGridControl1);
            this.panel2.Controls.Add(this.efwSimpleButton2);
            this.panel2.Controls.Add(this.efwMemoEdit1);
            this.panel2.Controls.Add(this.txtAddr);
            this.panel2.Controls.Add(this.navBarControl2);
            this.panel2.Controls.Add(this.navBarControl1);
            this.panel2.Controls.Add(this.btnSet);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.efwSimpleButton1);
            this.panel2.Controls.Add(this.efwTextEdit1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(300, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(858, 638);
            this.panel2.TabIndex = 3;
            // 
            // navBarControl2
            // 
            this.navBarControl2.ActiveGroup = this.navBarGroup1;
            this.navBarControl2.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.navBarControl2.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2});
            this.navBarControl2.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1,
            this.navBarItem2,
            this.navBarItem3,
            this.navBarItem4,
            this.navBarSeparatorItem1});
            this.navBarControl2.Location = new System.Drawing.Point(718, 0);
            this.navBarControl2.Name = "navBarControl2";
            this.navBarControl2.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl2.Size = new System.Drawing.Size(140, 638);
            this.navBarControl2.TabIndex = 6;
            this.navBarControl2.Text = "navBarControl2";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "navBarGroup1";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 30;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem1),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem2),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem3)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "navBarItem1";
            this.navBarItem1.Name = "navBarItem1";
            // 
            // navBarItem2
            // 
            this.navBarItem2.Caption = "navBarItem2";
            this.navBarItem2.Name = "navBarItem2";
            // 
            // navBarItem3
            // 
            this.navBarItem3.Caption = "navBarItem3";
            this.navBarItem3.Name = "navBarItem3";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.navBarGroupControlContainer1.Appearance.Options.UseBackColor = true;
            this.navBarGroupControlContainer1.Controls.Add(this.treeView1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(132, 76);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(132, 76);
            this.treeView1.TabIndex = 0;
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "navBarGroup2";
            this.navBarGroup2.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.GroupClientHeight = 80;
            this.navBarGroup2.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup2.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem4),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem1)});
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // navBarItem4
            // 
            this.navBarItem4.Caption = "navBarItem4";
            this.navBarItem4.Name = "navBarItem4";
            // 
            // navBarSeparatorItem1
            // 
            this.navBarSeparatorItem1.CanDrag = false;
            this.navBarSeparatorItem1.Enabled = false;
            this.navBarSeparatorItem1.Hint = null;
            this.navBarSeparatorItem1.Name = "navBarSeparatorItem1";
            // 
            // navBarControl1
            // 
            this.navBarControl1.Location = new System.Drawing.Point(548, 51);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(140, 300);
            this.navBarControl1.TabIndex = 5;
            this.navBarControl1.Text = "navBarControl2";
            // 
            // btnSet
            // 
            this.btnSet.IsMultiLang = false;
            this.btnSet.Location = new System.Drawing.Point(434, 21);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 4;
            this.btnSet.Text = "Set Menu1";
            this.btnSet.Click += new System.EventHandler(this.BtnSet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(31, 71);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(75, 23);
            this.efwSimpleButton1.TabIndex = 1;
            this.efwSimpleButton1.Text = "Report";
            this.efwSimpleButton1.Click += new System.EventHandler(this.EfwSimpleButton1_Click);
            // 
            // efwTextEdit1
            // 
            this.efwTextEdit1.EditValue2 = null;
            this.efwTextEdit1.Location = new System.Drawing.Point(6, 18);
            this.efwTextEdit1.Name = "efwTextEdit1";
            this.efwTextEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.efwTextEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.efwTextEdit1.RequireMessage = null;
            this.efwTextEdit1.Size = new System.Drawing.Size(100, 20);
            this.efwTextEdit1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "XtraReportsForXpf.SmallIcon.png");
            this.imageList1.Images.SetKeyName(1, "AddFile_16x16.png");
            this.imageList1.Images.SetKeyName(2, "Merge_16x16.png");
            this.imageList1.Images.SetKeyName(3, "ClearTableStyle_16x16.png");
            this.imageList1.Images.SetKeyName(4, "Apply_16x16.png");
            this.imageList1.Images.SetKeyName(5, "Up2_16x16.png");
            this.imageList1.Images.SetKeyName(6, "ClearFormatting_16x16.png");
            this.imageList1.Images.SetKeyName(7, "AddFile_16x16.png");
            // 
            // txtAddr
            // 
            this.txtAddr.EditValue = "불정로 6";
            this.txtAddr.EditValue2 = null;
            this.txtAddr.Location = new System.Drawing.Point(47, 242);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtAddr.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtAddr.RequireMessage = null;
            this.txtAddr.Size = new System.Drawing.Size(142, 20);
            this.txtAddr.TabIndex = 7;
            // 
            // efwMemoEdit1
            // 
            this.efwMemoEdit1.Location = new System.Drawing.Point(47, 268);
            this.efwMemoEdit1.Name = "efwMemoEdit1";
            this.efwMemoEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.efwMemoEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.efwMemoEdit1.Size = new System.Drawing.Size(250, 115);
            this.efwMemoEdit1.TabIndex = 8;
            // 
            // efwSimpleButton2
            // 
            this.efwSimpleButton2.IsMultiLang = false;
            this.efwSimpleButton2.Location = new System.Drawing.Point(209, 245);
            this.efwSimpleButton2.Name = "efwSimpleButton2";
            this.efwSimpleButton2.Size = new System.Drawing.Size(75, 23);
            this.efwSimpleButton2.TabIndex = 9;
            this.efwSimpleButton2.Text = "efwSimpleButton2";
            this.efwSimpleButton2.Click += new System.EventHandler(this.EfwSimpleButton2_Click);
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
            serviceInfo2.InstanceName = "";
            serviceInfo2.IsUserIDAdd = true;
            serviceInfo2.ParamsInfo = ((System.Collections.Generic.Dictionary<int, object>)(resources.GetObject("serviceInfo2.ParamsInfo")));
            serviceInfo2.ProcName = "";
            serviceInfo2.UserParams = ((System.Collections.Generic.List<object>)(resources.GetObject("serviceInfo2.UserParams")));
            this.efwGridControl1.InsertServiceInfo = serviceInfo2;
            this.efwGridControl1.IsAddExcelBtn = true;
            this.efwGridControl1.isAddPrintBtn = true;
            this.efwGridControl1.IsMultiLang = false;
            this.efwGridControl1.Location = new System.Drawing.Point(47, 389);
            this.efwGridControl1.MainView = this.gridView1;
            this.efwGridControl1.Name = "efwGridControl1";
            this.efwGridControl1.NowRowHandle = 0;
            this.efwGridControl1.PKColumns = ((System.Collections.ArrayList)(resources.GetObject("efwGridControl1.PKColumns")));
            this.efwGridControl1.PrevRowHandle = -2147483648;
            this.efwGridControl1.Size = new System.Drawing.Size(400, 200);
            this.efwGridControl1.TabIndex = 10;
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
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(475, 389);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 11;
            // 
            // efwSimpleButton3
            // 
            this.efwSimpleButton3.IsMultiLang = false;
            this.efwSimpleButton3.Location = new System.Drawing.Point(290, 245);
            this.efwSimpleButton3.Name = "efwSimpleButton3";
            this.efwSimpleButton3.Size = new System.Drawing.Size(75, 23);
            this.efwSimpleButton3.TabIndex = 12;
            this.efwSimpleButton3.Text = "efwSimpleButton3";
            this.efwSimpleButton3.Click += new System.EventHandler(this.EfwSimpleButton3_Click);
            // 
            // frmTest01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.accordionControl);
            this.Name = "frmTest01";
            this.Size = new System.Drawing.Size(1161, 673);
            this.Controls.SetChildIndex(this.accordionControl, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl2)).EndInit();
            this.navBarControl2.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwTextEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwMemoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private System.Windows.Forms.Panel panel2;
        private Easy.Framework.WinForm.Control.efwTextEdit efwTextEdit1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
        private System.Windows.Forms.Label label1;
        private Easy.Framework.WinForm.Control.efwSimpleButton btnSet;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem3;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem4;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton2;
        private Easy.Framework.WinForm.Control.efwMemoEdit efwMemoEdit1;
        private Easy.Framework.WinForm.Control.efwTextEdit txtAddr;
        private Easy.Framework.WinForm.Control.efwGridControl efwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton3;
    }
}