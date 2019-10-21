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

namespace YL_SCM.BizFrm
{
    public partial class frmSCM03_Pop01 : FrmPopUpBase
    {
        public Int32 pOrderNo { get; set; }

        public frmSCM03_Pop01()
        {
            InitializeComponent();
        }

        private void frmGM05_Pop01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            txtOrderNo.EditValue = pOrderNo;
            
        }
    }
}
