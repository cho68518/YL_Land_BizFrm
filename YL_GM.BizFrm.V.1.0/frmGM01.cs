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

namespace YL_GM.BizFrm
{
    public partial class frmGM01 : FrmBase
    {
        #region Fields

        //bool _is_dupchk = false;

        #endregion

        #region 생성자

        public frmGM01()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "GM01";
            //폼명설정
            this.FrmName = "회원정보현황";

            //gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);
        }

        #endregion

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //if (UserInfo.instance().ORG_CD != null && UserInfo.instance().ORG_CD.ToString() != "")
            //{
            //    txtCOMPANYCD.EditValue = UserInfo.instance().ORG_CD;
            //}
            //else
            //{
            //    txtCOMPANYCD.EditValue = "YL01";
            //}

            //if (UserInfo.instance().ORG_NM != null && UserInfo.instance().ORG_NM.ToString() != "")
            //{
            //    txtCOMPANYNAME.EditValue = UserInfo.instance().ORG_NM;
            //}
            //else
            //{
            //    txtCOMPANYNAME.EditValue = "(주)와이엘랜드";
            //}

            ////그리드 컬럼에 체크박스 레포지토리아이템 추가
            //GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_doramd");
            //GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_biz");

            //gridView1.OptionsView.ShowFooter = true;
            //gridView1.Columns["reg_date"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //gridView1.Columns["reg_date"].SummaryItem.FieldName = "reg_date";
            ////gridView1.Columns["MCNT"].SummaryItem.DisplayFormat = "총인원: {0:n2}";
            //gridView1.Columns["reg_date"].SummaryItem.DisplayFormat = "총인원: {0:0}";

            ////그리드로 클릭시 컨트롤 데이터 바인딩
            //this.efwGridControl1.BindControlSet(
            //            new ColumnControlSet("u_name", txtU_NAME)
            //          , new ColumnControlSet("user_id", txtUSER_ID)
            //          , new ColumnControlSet("u_id", txtU_ID)
            //          , new ColumnControlSet("u_nickname", txtU_NICKNAME)
            //          , new ColumnControlSet("u_gender", txtU_GENDER)
            //          , new ColumnControlSet("u_birthday", txtBIRTH)
            //          , new ColumnControlSet("u_email", txtU_EMAIL)
            //          , new ColumnControlSet("u_cell_num", txtU_CELL_NUM)
            //          , new ColumnControlSet("reg_date", txtREG_DATE)
            //          , new ColumnControlSet("login_date", txtLOGIN_DATE)
            //          , new ColumnControlSet("u_zip", txtU_ZIP)
            //          , new ColumnControlSet("u_addr", txtU_ADDR)
            //          , new ColumnControlSet("u_addr_detail", txtU_ADDR_DETAIL)
            //          , new ColumnControlSet("u_chef_level_cd", cmbU_CHEF_LEVEL)
            //          , new ColumnControlSet("is_al_friend_yn", chkIS_AL_FRIEND)
            //          , new ColumnControlSet("is_stock_friend_yn", chkIS_STOCK_FRIEND)
            //          , new ColumnControlSet("idx", txtIDX)
            //          , new ColumnControlSet("is_doramd", chkIS_DORAMD)
            //          , new ColumnControlSet("is_biz", chkIS_BIZ)
            //          , new ColumnControlSet("DM_Money", txtD)
            //          , new ColumnControlSet("TD_Money", txtTD)
            //          , new ColumnControlSet("AD_Money", txtAD)
            //          , new ColumnControlSet("GD_Money", txtGD)
            //          , new ColumnControlSet("CD_Money", txtCD)
            //          , new ColumnControlSet("VIP_Money", txtCOUPON)
            //          );

            //this.efwGridControl1.Click += efwGridControl1_Click;
        }

        #endregion



    }
}
