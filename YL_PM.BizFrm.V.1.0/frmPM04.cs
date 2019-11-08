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
namespace YL_PM.BizFrm
{
    public partial class frmPM04 : FrmBase
    {
        MySQLConn sconn = new MySQLConn(ConstantLib.BasicConn_Dev);
        bool _isNewMode;
        DataSet _dsDeptInfo = null;
        DataSet _dsDeptInfo2 = null;

        public ImageList imgOrganList { get; private set; }
        public frmPM04()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "PM04";
            //폼명설정
            this.FrmName = "상품마스터 등록";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            //if (UserInfo.instance().ORG_CD != null && UserInfo.instance().ORG_CD.ToString() != "")
            //    txtCOMPANYCD.EditValue = UserInfo.instance().ORG_CD;
            //else
            //    txtCOMPANYCD.EditValue = "YL01";

            //setCmb();

            //if (UserInfo.instance().BIZCD != null && UserInfo.instance().BIZCD.ToString() != "")
            //    cmbBIZCD.EditValue = UserInfo.instance().BIZCD;
            //else
            //    cmbBIZCD.EditValue = "01";


            //gridView1.OptionsView.ShowFooter = true;

            ////그리드로 클릭시 컨트롤 데이터 바인딩
            //this.efwGridControl1.BindControlSet(
            //          new ColumnControlSet("COMPANYCD", txtCOMPANYCD)
            //        , new ColumnControlSet("BIZCD", cmbBIZCD)
            //        , new ColumnControlSet("LARGE_CD", txtLARGE_CD)
            //        , new ColumnControlSet("LARGE_NM", txtLARGE_NM)
            //        );

            //this.efwGridControl1.Click += efwGridControl1_Click;

            ////CreTree();


            //Open1();
        }



        //private void setCmb()
        //{
        //    try
        //    {
        //        //사업장콤보
        //        string strQueruy = @"SELECT
        //                                BIZCD    DCODE
        //                               ,BIZCD_NM DNAME
        //                             FROM TBL_BIZCD
        //                             WHERE COMPANYCD = '" + txtCOMPANYCD.EditValue + "' " +
        //                               " ORDER BY BIZCD ASC";
        //        CodeAgent.SetLegacyCode(cmbBIZCD, strQueruy);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
        //    }
        //}

        #endregion

    }
}
