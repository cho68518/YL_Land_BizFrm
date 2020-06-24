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
    public partial class frmGSHOP12_Pop01 : FrmPopUpBase
    {
        public string pURL { get; set; }
        public Bitmap pPIC { get; set; }
        public frmGSHOP12_Pop01()
        {
            InitializeComponent();
        }

        private void frmGSHOP12_Pop01_Load(object sender, EventArgs e)
        {
            this.Text = "사진정보";

            picImg.LoadAsync(pURL);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }


    }
}
