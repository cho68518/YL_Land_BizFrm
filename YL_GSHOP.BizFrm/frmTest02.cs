using Easy.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_GSHOP.BizFrm
{
    public partial class frmTest02 : FrmBase
    {
        public frmTest02()
        {
            InitializeComponent();
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = false;
            this.IsSearch = false;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            
            this.webBrowser1.Navigate("http://dotoc3.eyeoyou.com/maps/maptest10.asp?p1=298112.2669620839&p2=545594.0997949387");
        }
    }
}
