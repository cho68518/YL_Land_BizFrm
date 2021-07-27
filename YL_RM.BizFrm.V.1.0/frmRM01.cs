using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Report;
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

namespace YL_RM.BizFrm
{
    public partial class frmRM01 : FrmBase
    {
        public frmRM01()
        {
            InitializeComponent();
            this.QCode = "RM01";
            //폼명설정
            this.FrmName = "발모 후기현황";
        }
        private void frmRM01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            gridView1.OptionsView.ShowFooter = true;
            
        }


    }
}
