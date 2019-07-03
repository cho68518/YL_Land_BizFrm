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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest01));
            this.accordionControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.panel2 = new System.Windows.Forms.Panel();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.btnSet = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.efwTextEdit1 = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.navBarControl2 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem3 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem4 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarSeparatorItem1 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwTextEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl2)).BeginInit();
            this.navBarControl2.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
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
            // navBarControl1
            // 
            this.navBarControl1.Location = new System.Drawing.Point(670, 21);
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
            this.navBarControl2.Size = new System.Drawing.Size(140, 638);
            this.navBarControl2.TabIndex = 6;
            this.navBarControl2.Text = "navBarControl2";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "navBarGroup1";
            this.navBarGroup1.Expanded = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwTextEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl2)).EndInit();
            this.navBarControl2.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
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
    }
}