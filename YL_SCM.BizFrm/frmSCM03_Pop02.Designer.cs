namespace YL_SCM.BizFrm
{
    partial class frmSCM03_Pop02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSCM03_Pop02));
            this.btnSendSMS = new Easy.Framework.WinForm.Control.efwSimpleButton();
            this.txtMSG = new Easy.Framework.WinForm.Control.efwMemoEdit();
            this.efwLabel5 = new Easy.Framework.WinForm.Control.efwLabel();
            this.txtOrderNo = new Easy.Framework.WinForm.Control.efwTextEdit();
            this.efwLabel4 = new Easy.Framework.WinForm.Control.efwLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtMSG.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSendSMS.ImageOptions.Image")));
            this.btnSendSMS.IsMultiLang = false;
            this.btnSendSMS.Location = new System.Drawing.Point(71, 159);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(120, 26);
            this.btnSendSMS.TabIndex = 31;
            this.btnSendSMS.Text = "SMS 문자전송";
            // 
            // txtMSG
            // 
            this.txtMSG.ByteLength = 200;
            this.txtMSG.Location = new System.Drawing.Point(12, 65);
            this.txtMSG.Name = "txtMSG";
            this.txtMSG.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtMSG.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtMSG.Size = new System.Drawing.Size(238, 88);
            this.txtMSG.TabIndex = 27;
            // 
            // efwLabel5
            // 
            this.efwLabel5.EraserGroup = null;
            this.efwLabel5.IsMultiLang = false;
            this.efwLabel5.Location = new System.Drawing.Point(12, 45);
            this.efwLabel5.Name = "efwLabel5";
            this.efwLabel5.Size = new System.Drawing.Size(88, 14);
            this.efwLabel5.TabIndex = 30;
            this.efwLabel5.Text = "전송할 메세지 내용";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.EditValue2 = null;
            this.txtOrderNo.Location = new System.Drawing.Point(61, 13);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Ivory;
            this.txtOrderNo.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtOrderNo.Properties.ReadOnly = true;
            this.txtOrderNo.RequireMessage = null;
            this.txtOrderNo.Size = new System.Drawing.Size(91, 20);
            this.txtOrderNo.TabIndex = 29;
            // 
            // efwLabel4
            // 
            this.efwLabel4.EraserGroup = null;
            this.efwLabel4.IsMultiLang = false;
            this.efwLabel4.Location = new System.Drawing.Point(12, 16);
            this.efwLabel4.Name = "efwLabel4";
            this.efwLabel4.Size = new System.Drawing.Size(40, 14);
            this.efwLabel4.TabIndex = 28;
            this.efwLabel4.Text = "주문번호";
            // 
            // frmSCM03_Pop02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 197);
            this.Controls.Add(this.btnSendSMS);
            this.Controls.Add(this.txtMSG);
            this.Controls.Add(this.efwLabel5);
            this.Controls.Add(this.txtOrderNo);
            this.Controls.Add(this.efwLabel4);
            this.Name = "frmSCM03_Pop02";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "주의~!";
            ((System.ComponentModel.ISupportInitialize)(this.txtMSG.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Easy.Framework.WinForm.Control.efwSimpleButton btnSendSMS;
        public Easy.Framework.WinForm.Control.efwMemoEdit txtMSG;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel5;
        private Easy.Framework.WinForm.Control.efwTextEdit txtOrderNo;
        private Easy.Framework.WinForm.Control.efwLabel efwLabel4;
    }
}