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
using DevExpress.XtraGrid.Columns;
using YL_TELECOM.BizFrm.Dlg;


namespace YL_TELECOM.BizFrm
{
    public partial class frmTM09 : FrmBase
    {
        public frmTM09()
        {
            InitializeComponent();
            this.QCode = "TM09";
            //폼명설정
            this.FrmName = "상품현황";
        }
    }
}
