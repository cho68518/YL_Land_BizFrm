using DevExpress.XtraEditors.Repository;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_MM.BizFrm.Dlg;
using DevExpress.XtraGrid.Columns;

namespace YL_MM.BizFrm
{
    public partial class frmMM15 : FrmBase
    {
        public frmMM15()
        {
            InitializeComponent();
            this.QCode = "MM15";
            //폼명설정
            this.FrmName = "상품 할인율 관리";
        }

        private void efwPanelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void efwTextEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void efwTextEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void efwRadioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
