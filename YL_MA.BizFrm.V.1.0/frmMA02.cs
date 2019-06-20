#region "frmMA02 설명"
//===================================================================================================
//■Program Name  : frmMA02
//■Description   : 부서별인원현황
//■Author        : 조정현
//■Date          : 2019.04.25
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.04.18][조정현] Base
//[2] [2019.04.18][조정현] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_MA.BizFrm
{
    public partial class frmMA02 : FrmBase
    {
        #region Fields


        #endregion

        #region 생성자
        public frmMA02()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MA02";
            //폼명설정
            this.FrmName = "부서별인원현황";
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
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            repositoryItemPictureEdit1.NullText = " ";

            //Image pimage = YL_MA.BizFrm.Properties.Resources.PictureBox_16x16;

            ////그리드 컬럼에 체크박스 레포지토리아이템 추가
            //GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "USEYN");

            if (UserInfo.instance().ORG_CD != null && UserInfo.instance().ORG_CD.ToString() != "")
            {
                txtCOMPANYCD.EditValue = UserInfo.instance().ORG_CD;
                //txtCOMPANYNAME.EditValue = UserInfo.instance().ORG_NM;
            }
            else
            {
                txtCOMPANYCD.EditValue = "YL01";
                //txtCOMPANYNAME.EditValue = "(주)와이엘랜드";
            }

            setCmb();

            if (UserInfo.instance().BIZCD != null && UserInfo.instance().BIZCD.ToString() != "")
            {
                //cmbBIZCD.EditValue = UserInfo.instance().BIZCD;
                cmbBIZCDQ.EditValue = UserInfo.instance().BIZCD;
            }
            else
            {
                //cmbBIZCD.EditValue = "01";
                cmbBIZCDQ.EditValue = "01";
            }

            ////그리드로 클릭시 컨트롤 데이터 바인딩
            //this.efwGridControl1.BindControlSet(
            //          new ColumnControlSet("COMPANYCD", txtCOMPANYCD)
            //          , new ColumnControlSet("BIZCD", cmbBIZCD)
            //          //, new ColumnControlSet("BIZCD_NM", txtBIZCD_NM)
            //          , new ColumnControlSet("DEPTCODE", txtDEPTCODE)
            //          , new ColumnControlSet("DEPTNAME", txtDEPTNAME)
            //          , new ColumnControlSet("DEPTABBR", txtDEPTABBR)
            //          , new ColumnControlSet("STARTYM", txtSTARTYM)
            //          , new ColumnControlSet("ENDYM", txtENDYM)
            //          , new ColumnControlSet("USEYN", chkUSEYN)
            //          , new ColumnControlSet("MEMO", txtMEMO)
            //          , new ColumnControlSet("RNKORDER", txtRNKORDER)
            //          , new ColumnControlSet("REFCODE", txtREFCODE)
            //          , new ColumnControlSet("LEVEL", txtLEVEL)
            //          );

            //this.efwGridControl1.Click += efwGridControl1_Click;
            //chkUSEYN.Checked = true;

            //Open1();

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["MCNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["MCNT"].SummaryItem.FieldName = "MCNT";
            //gridView1.Columns["MCNT"].SummaryItem.DisplayFormat = "총인원: {0:n2}";
            gridView1.Columns["MCNT"].SummaryItem.DisplayFormat = "총인원: {0:0}";

            gridView2.OptionsView.ShowFooter = true;
            gridView2.Columns["EMAIL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView2.Columns["EMAIL"].SummaryItem.FieldName = "EMAIL";
            //gridView2.Columns["COIDNO"].SummaryItem.DisplayFormat = "총인원: {0:n2}";
            gridView2.Columns["EMAIL"].SummaryItem.DisplayFormat = "총인원: {0:0}";
        }

        #endregion

        public override void FrmShown()
        {
            base.FrmShown();

            //fValueSet();

            cmbBIZCDQ.ItemIndex = 0;
            cmbBIZCDQ.EditValue = "";

            Open3();
        }

        #region 콤보세팅

        private void setCmb()
        {
            try
            {
                //사업장콤보
                string strQueruy = @"SELECT
                                        BIZCD    DCODE
                                       ,BIZCD_NM DNAME
                                     FROM TBL_BIZCD
                                     WHERE COMPANYCD = '" + txtCOMPANYCD.EditValue + "' " +
                                       " ORDER BY BIZCD ASC";

                string strQueruy2 = @"  SELECT
                              T1.DCODE, T1.DNAME
                              FROM
                              (
	                             SELECT ''  DCODE, N'전체'  DNAME
	                             UNION ALL
	                             SELECT BIZCD    DCODE
                                       ,BIZCD_NM DNAME
                                 FROM TBL_BIZCD
	                             WHERE COMPANYCD = '" + txtCOMPANYCD.EditValue + "'" + @") T1
                               ORDER BY T1.DCODE ASC";

                //CodeAgent.SetLegacyCode(cmbBIZCD, strQueruy);
                CodeAgent.SetLegacyCode(cmbBIZCDQ, strQueruy2);
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        #endregion

        #region 조회

        private void Open1()
        {
            try
            {
                //gridView2.SelectAll();
                //gridView2.DeleteSelectedRows();

                efwGridControl2.DataSource = null;

                //base.Search();
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA02_SELECT_01"
                    , txtCOMPANYCD.EditValue
                    , cmbBIZCDQ.EditValue
                    );

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open3()
        {
            try
            {
                //base.Search();
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA02_SELECT_03"
                    , txtCOMPANYCD.EditValue
                    );

                efwGridControl2.DataBind(ds);
                this.efwGridControl2.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open2(string pBIZCD, string pDEPTCODE)
        {
            try
            {
                //base.Search();
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA02_SELECT_02"
                    , txtCOMPANYCD.EditValue
                    , pBIZCD
                    , pDEPTCODE
                    );

                efwGridControl2.DataBind(ds);
                this.efwGridControl2.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        #endregion

        #region 이벤트

        private void BtnOpen1_Click(object sender, EventArgs e)
        {
            Open1();

            if (cmbBIZCDQ.EditValue.ToString() == "")
            {
                Open3();
            }
        }

        private void GridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "MCNT")
            {
                if (Convert.ToInt32(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
            //else if (e.Column.FieldName == "PICYN")
            //{
            //    if (Convert.ToInt32(e.Value) == 0)
            //    {
            //        e.DisplayText = "";
            //    }

            //    e.Column..Value = YL_MA.BizFrm.Properties.Resources.PictureBox_16x16;
            //}
        }

        private void EfwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            Open2(dr["BIZCD"].ToString(), dr["DEPTCODE"].ToString());

            Eraser.Clear(this, "CLR1");
        }

        private void GridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            //if (e.IsGetData)
            //    e.Value = YL_MA.BizFrm.Properties.Resources.PictureBox_16x16;
            //string sval = string.Empty;

            //if (e.Column.FieldName == "PICYESNO")
            //{
            //    sval = e.Value.ToString();
            //}

            //if (e.Column.FieldName == "PICYN")
            //{
            //    if(sval == "Y")
            //    {
            //        e.Value = YL_MA.BizFrm.Properties.Resources.PictureBox_16x16;
            //    }
            //}

            if (e.IsGetData && e.Column.FieldName == "PICYN")
            {
                DataRow row = gridView2.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["PICYESNO"].ToString();

                if (retVal == "Y")
                    e.Value = YL_MA.BizFrm.Properties.Resources.PictureBox_16x16;
                else
                    e.Value = " ";
            }
        }

        private void EfwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);

            if (dr == null)
                return;

            if (dr != null && dr["COIDNO"].ToString() != "")
                this.txtCOIDNO.EditValue = dr["COIDNO"].ToString();
            if (dr != null && dr["NAME"].ToString() != "")
                this.txtNAME.EditValue = dr["NAME"].ToString();
            if (dr != null && dr["DEPTNAME"].ToString() != "")
                this.txtDEPTNAME.EditValue = dr["DEPTNAME"].ToString();
            if (dr != null && dr["DUTYNAME"].ToString() != "")
                this.txtDUTYNAME.EditValue = dr["DUTYNAME"].ToString();


            picImg.Image = null;

            picImg.DBKey = "YLEFW.TBL_BASIC.SAVE_COIDNO_FILE_NM";
            //파일서버에서 이미지 가져오기
            if (dr["SAVE_COIDNO_FILE_NM"].ToString() != null)
                WebFileAgent.GetImageFile(picImg, dr["SAVE_COIDNO_FILE_NM"].ToString());
        }

        private void CmbBIZCDQ_EditValueChanged(object sender, EventArgs e)
        {
            //Open1();

            //if (cmbBIZCDQ.EditValue.ToString() == "")
            //{
            //    Open3();
            //}
            BtnOpen1_Click(null, null);
        }


        #endregion

        private void GridView1_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.ForeColor = Color.Red;
        }

        private void GridView2_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.ForeColor = Color.Red;
        }

        private void BtnBasic_Click(object sender, EventArgs e)
        {
            frmMA01 frm1 = new frmMA01() { pCoIdNo = txtCOIDNO.EditValue.ToString() };
            frm1.Show();
        }
    }
}
