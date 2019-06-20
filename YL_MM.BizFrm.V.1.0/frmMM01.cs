#region "frmMM01 설명"
//===================================================================================================
//■Program Name  : frmMM01
//■Description   : 회사정보 관리 (사업장코드 포함)
//■Author        : 조정현
//■Date          : 2019.04.18
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

using Easy.Framework.Common;
using Easy.Framework.Common.PopUp;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_MM.BizFrm
{
    public partial class frmMM01 : FrmBase
    {
        #region Fields
        
        #endregion

        #region 생성자
        public frmMM01()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MM01";
            //폼명설정
            this.FrmName = "회사정보관리";

            //UserInfo.instance().ORG_CD;
        }
        #endregion

        #region FrmLoadEvent()
        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = false;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            if (UserInfo.instance().ORG_CD != null && UserInfo.instance().ORG_CD.ToString() != "")
                txtCOMPANYCD.EditValue = UserInfo.instance().ORG_CD;
            else
                txtCOMPANYCD.EditValue = "YL01";

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("BIZCD"       , txtBIZCD)
                      , new ColumnControlSet("BIZCD_NM"  , txtBIZCD_NM)
                      , new ColumnControlSet("REGISTERNO", txtREGISTERNO2)
                      , new ColumnControlSet("PREGIDENT" , txtPREGIDENT2)
                      , new ColumnControlSet("WORKTYPE"  , txtWORKTYPE2)
                      , new ColumnControlSet("WORKJONG"  , txtWORKJONG2)
                      , new ColumnControlSet("ZIPNO"     , txtZIPNO2)
                      , new ColumnControlSet("ADDR"      , txtADDR2)
                      , new ColumnControlSet("TEL"       , txtTEL2)
                      , new ColumnControlSet("FAX"       , txtFAX2)
                      , new ColumnControlSet("MEMO"      , txtMEMO2)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;

            Open1();
            Open2();
        }

        #endregion

        #region 조회

        private void btnOpen1_Click(object sender, EventArgs e)
        {
            //회사정보조회
            Open1();
        }

        private void btnOpen2_Click(object sender, EventArgs e)
        {
            //사업장정보조회
            Open2();
        }

        private void Open1()
        {
            //회사정보조회
            picImg.Image = null;

            try
            {
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM01_SELECT_01", txtCOMPANYCD.EditValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCOMPANYCD.EditValue   = ds.Tables[0].Rows[0]["COMPANYCD"].ToString();
                    txtCOMPANYNAME.EditValue = ds.Tables[0].Rows[0]["COMPANYNAME"].ToString();
                    txtCOMPANYENG.EditValue  = ds.Tables[0].Rows[0]["COMPANYENG"].ToString();
                    txtZIPNO.EditValue       = ds.Tables[0].Rows[0]["ZIPNO"].ToString();
                    txtADDR.EditValue        = ds.Tables[0].Rows[0]["ADDR"].ToString();
                    txtTEL.EditValue         = ds.Tables[0].Rows[0]["TEL"].ToString();
                    txtFAX.EditValue         = ds.Tables[0].Rows[0]["FAX"].ToString();
                    txtURL.EditValue         = ds.Tables[0].Rows[0]["URL"].ToString();
                    txtMEMO.EditValue        = ds.Tables[0].Rows[0]["MEMO"].ToString();
                    txtREGISTERNO.EditValue  = ds.Tables[0].Rows[0]["REGISTERNO"].ToString();
                    txtPREGIDENT.EditValue   = ds.Tables[0].Rows[0]["PREGIDENT"].ToString();
                    txtWORKTYPE.EditValue    = ds.Tables[0].Rows[0]["WORKTYPE"].ToString();
                    txtWORKJONG.EditValue    = ds.Tables[0].Rows[0]["WORKJONG"].ToString();
                    lblFILE_GRP_ID.Text      = ds.Tables[0].Rows[0]["LOGO_FILE_NM"].ToString();
                    lblFILE_GRP_ID2.Text     = ds.Tables[0].Rows[0]["SAVE_LOGO_FILE_NM"].ToString();

                    picImg.DBKey = "YLEFW.TBL_COMPANY.SAVE_LOGO_FILE_NM";
                    //파일서버에서 이미지 가져오기
                    if (ds.Tables[0].Rows[0]["SAVE_LOGO_FILE_NM"].ToString() != null)
                        WebFileAgent.GetImageFile(picImg, ds.Tables[0].Rows[0]["SAVE_LOGO_FILE_NM"].ToString());
                }
                else
                {
                    MessageAgent.MessageShow(MessageType.Error, "회사정보가 없습니다!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open2()
        {
            //사업장정보조회
            try
            {
                //base.Search();
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM01_SELECT_02"
                , this.txtCOMPANYCD.Text);

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        #endregion

        #region 저장

        private void btnSave1_Click(object sender, EventArgs e)
        {
            //회사정보 저장
            if (txtCOMPANYCD.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사코드가 없습니다!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                if (ValidationAgentEx.IsRequireCheck(this.layoutControl1.Controls, "R1"))
                {
                    try
                    {
                        int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MM_MM01_SAVE_01"
                                                         , UserInfo.instance().UserId
                                                        , this.txtCOMPANYCD.EditValue
                                                        , this.txtCOMPANYNAME.EditValue
                                                        , this.txtCOMPANYENG.EditValue
                                                        , this.txtZIPNO.EditValue
                                                        , this.txtADDR.EditValue
                                                        , this.txtTEL.EditValue
                                                        , this.txtFAX.EditValue
                                                        , this.txtURL.EditValue
                                                        , this.txtMEMO.EditValue
                                                        , this.txtREGISTERNO.EditValue
                                                        , this.txtPREGIDENT.EditValue
                                                        , this.txtWORKTYPE.EditValue
                                                        , this.txtWORKJONG.EditValue
                                                        , this.lblFILE_GRP_ID.Text
                                                        , this.lblFILE_GRP_ID2.Text
                                                         );

                        if (retVal > 0)
                        {
                            Open1();
                            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    }
                }
            }
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            //사업장정보저장
            if (txtCOMPANYCD.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사코드가 없습니다!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                if (ValidationAgentEx.IsRequireCheck(this.layoutControl1.Controls, "R2"))
                {
                    try
                    {
                        int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MM_MM01_SAVE_02"
                                                         , UserInfo.instance().UserId
                                                         , this.txtCOMPANYCD.EditValue
                                                         , this.txtBIZCD.EditValue
                                                         , this.txtBIZCD_NM.EditValue
                                                         , this.txtREGISTERNO2.EditValue
                                                         , this.txtPREGIDENT2.EditValue
                                                         , this.txtWORKTYPE2.EditValue
                                                         , this.txtWORKJONG2.EditValue
                                                         , this.txtADDR2.EditValue
                                                         , this.txtZIPNO2.EditValue
                                                         , this.txtTEL2.EditValue
                                                         , this.txtFAX2.EditValue
                                                         , this.txtMEMO2.EditValue
                                                         );

                        if (retVal > 0)
                        {
                            Open2();
                            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    }
                }
            }
        }

        #endregion

        #region 이벤트

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            if (txtCOMPANYCD.Text == string.Empty)
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사를 선택하세요 !!");
                return;
            }

            frmPopUpFileUpload frmFileUpload = new frmPopUpFileUpload() { /*폼 뷰잉전에 설정*/UploadKey = "YLEFW.TBL_COMPANY.SAVE_LOGO_FILE_NM" };
            picImg.DBKey = "YLEFW.TBL_COMPANY.SAVE_LOGO_FILE_NM";
            //팝업창 자동 종료
            frmFileUpload.IsAutoClose = true;

            frmFileUpload.efwControl = this.picImg;

            frmFileUpload.ShowDialog();

            //예외처리 파일업로드 창을 강제 종료시 파일명이 넘어오지 않는다.
            if (frmFileUpload.FileName != string.Empty)
                lblFILE_GRP_ID.Text = frmFileUpload.FileName;

            if (frmFileUpload.SaveFileName != string.Empty)
                lblFILE_GRP_ID2.Text = frmFileUpload.SaveFileName;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            //if (dr != null && dr["FLR"].ToString() != "0" && dr["FLR"].ToString() != "")
            //    this.xtraTabPage3.PageEnabled = true;
            //else
            //    this.xtraTabPage3.PageEnabled = false;


        }



        #endregion

        private void BtnNew_Click(object sender, EventArgs e)
        {
            Eraser.Clear(this, "CLR2");
        }
    }
}
