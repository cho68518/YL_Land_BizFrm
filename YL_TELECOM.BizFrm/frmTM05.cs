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

namespace YL_TELECOM.BizFrm
{
    public partial class frmTM05 : FrmBase
    {
        public frmTM05()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmTM05";
            //폼명설정
            this.FrmName = "영업일지 등록";
        }

        private void FrmLoadEvent(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //efwLabel1.Font = new Font(efwLabel1.Font, FontStyle.Bold);
            this.efwLabel1.Font = new System.Drawing.Font("맑은고딕", 26, System.Drawing.FontStyle.Bold);
            //this.efwLabel1.Font = new Font(this.efwLabel1.Font, FontStyle.Bold);
        }





    }
}
