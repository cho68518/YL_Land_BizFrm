
namespace YL_GSHOP.BizFrm
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
            this.efwSimpleButton1 = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.efwDateEdit1 = new Easy.Framework.WinForm.Control.efwDateEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.efwDateEdit2 = new Easy.Framework.WinForm.Control.efwDateEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.efwTextEdit1 = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.efwLabel1 = new Easy.Framework.WinForm.Control.efwLabel();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.efwListBoxControl1 = new Easy.Framework.WinForm.Control.efwListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwDateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwDateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwDateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwDateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwTextEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwListBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // efwSimpleButton1
            // 
            this.efwSimpleButton1.IsMultiLang = false;
            this.efwSimpleButton1.Location = new System.Drawing.Point(67, 72);
            this.efwSimpleButton1.Name = "efwSimpleButton1";
            this.efwSimpleButton1.Size = new System.Drawing.Size(105, 36);
            this.efwSimpleButton1.TabIndex = 2;
            this.efwSimpleButton1.Text = "쿠폰발행";
            this.efwSimpleButton1.Click += new System.EventHandler(this.efwSimpleButton1_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.efwLabel1);
            this.layoutControl1.Controls.Add(this.efwTextEdit1);
            this.layoutControl1.Controls.Add(this.efwDateEdit2);
            this.layoutControl1.Controls.Add(this.efwDateEdit1);
            this.layoutControl1.Location = new System.Drawing.Point(67, 128);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(550, 189);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(550, 189);
            this.Root.TextVisible = false;
            // 
            // efwDateEdit1
            // 
            this.efwDateEdit1.EditValue = null;
            this.efwDateEdit1.Location = new System.Drawing.Point(110, 12);
            this.efwDateEdit1.Name = "efwDateEdit1";
            this.efwDateEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.efwDateEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.efwDateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.efwDateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.efwDateEdit1.Size = new System.Drawing.Size(147, 20);
            this.efwDateEdit1.StyleController = this.layoutControl1;
            this.efwDateEdit1.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.efwDateEdit1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(249, 24);
            this.layoutControlItem1.Text = "쿠폰유효기간 시작일";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(94, 14);
            // 
            // efwDateEdit2
            // 
            this.efwDateEdit2.EditValue = null;
            this.efwDateEdit2.Location = new System.Drawing.Point(359, 12);
            this.efwDateEdit2.Name = "efwDateEdit2";
            this.efwDateEdit2.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.efwDateEdit2.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.efwDateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.efwDateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.efwDateEdit2.Size = new System.Drawing.Size(179, 20);
            this.efwDateEdit2.StyleController = this.layoutControl1;
            this.efwDateEdit2.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.efwDateEdit2;
            this.layoutControlItem2.Location = new System.Drawing.Point(249, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(281, 24);
            this.layoutControlItem2.Text = "쿠폰유효기간 종료일";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(94, 14);
            // 
            // efwTextEdit1
            // 
            this.efwTextEdit1.EditValue2 = null;
            this.efwTextEdit1.Location = new System.Drawing.Point(110, 36);
            this.efwTextEdit1.Name = "efwTextEdit1";
            this.efwTextEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.efwTextEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.efwTextEdit1.RequireMessage = null;
            this.efwTextEdit1.Size = new System.Drawing.Size(147, 20);
            this.efwTextEdit1.StyleController = this.layoutControl1;
            this.efwTextEdit1.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.efwTextEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(249, 24);
            this.layoutControlItem3.Text = "쿠폰 발행 매수";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(94, 14);
            // 
            // efwLabel1
            // 
            this.efwLabel1.EraserGroup = null;
            this.efwLabel1.IsMultiLang = false;
            this.efwLabel1.Location = new System.Drawing.Point(261, 36);
            this.efwLabel1.Name = "efwLabel1";
            this.efwLabel1.Size = new System.Drawing.Size(10, 14);
            this.efwLabel1.StyleController = this.layoutControl1;
            this.efwLabel1.TabIndex = 7;
            this.efwLabel1.Text = "매";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.efwLabel1;
            this.layoutControlItem4.Location = new System.Drawing.Point(249, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(14, 24);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 48);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(530, 121);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(263, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(267, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // efwListBoxControl1
            // 
            this.efwListBoxControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.efwListBoxControl1.IsMultiLang = false;
            this.efwListBoxControl1.Location = new System.Drawing.Point(623, 41);
            this.efwListBoxControl1.Name = "efwListBoxControl1";
            this.efwListBoxControl1.Size = new System.Drawing.Size(354, 660);
            this.efwListBoxControl1.TabIndex = 4;
            // 
            // frmTest01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.efwListBoxControl1);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.efwSimpleButton1);
            this.Name = "frmTest01";
            this.Size = new System.Drawing.Size(1178, 704);
            this.Controls.SetChildIndex(this.efwSimpleButton1, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.efwListBoxControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwDateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwDateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwDateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwDateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwTextEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efwListBoxControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwSimpleButton efwSimpleButton1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel1;
        private Easy.Framework.WinForm.Control.efwTextEdit efwTextEdit1;
        private Easy.Framework.WinForm.Control.efwDateEdit efwDateEdit2;
        private Easy.Framework.WinForm.Control.efwDateEdit efwDateEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private Easy.Framework.WinForm.Control.efwListBoxControl efwListBoxControl1;
    }
}