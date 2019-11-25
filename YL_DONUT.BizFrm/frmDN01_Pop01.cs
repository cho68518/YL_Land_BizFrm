using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace YL_DONUT.BizFrm
{
    public partial class frmDN01_Pop01 : FrmPopUpBase
    {
        public int Id { get; set; }
        public frmDN01_Pop01()
        {
            InitializeComponent();
        }

        private void frmDN01_Pop01_Load(object sender, EventArgs e)
        {
            txtID.EditValue = Id;
        }
    }
}
