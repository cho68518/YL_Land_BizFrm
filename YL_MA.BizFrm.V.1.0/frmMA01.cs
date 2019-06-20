#region "frmMA01 설명"
//===================================================================================================
//■Program Name  : frmMA01
//■Description   : 인사기본정보 관리
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using YL_MA.BizFrm.Dlg;

namespace YL_MA.BizFrm
{
    public partial class frmMA01 : FrmBase
    {
        public string pCoIdNo
        {
            get;
            set;
        }

        #region Fields

        bool _isNewMode;

        #endregion

        #region 생성자
        public frmMA01()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MA01";
            //폼명설정
            this.FrmName = "인사기본";
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

            ////그리드 컬럼에 체크박스 레포지토리아이템 추가
            //GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "USEYN");

            if (UserInfo.instance().ORG_CD != null && UserInfo.instance().ORG_CD.ToString() != "")
            {
                txtCOMPANYCD.EditValue = UserInfo.instance().ORG_CD;
                txtCOMPANYNAME.EditValue = UserInfo.instance().ORG_NM;
            }
            else
            {
                txtCOMPANYCD.EditValue = "YL01";
                txtCOMPANYNAME.EditValue = "(주)와이엘랜드";
            }

            setCmb();

            if (UserInfo.instance().BIZCD != null && UserInfo.instance().BIZCD.ToString() != "")
            {
                cmbBIZCD.EditValue = UserInfo.instance().BIZCD;
                //cmbBIZCDQ.EditValue = UserInfo.instance().BIZCD;
            }
            else
            {
                cmbBIZCD.EditValue = "01";
                //cmbBIZCDQ.EditValue = "01";
            }

            cmbBIZCDQ.EditValue = "";

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["BIZCD_NM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns["BIZCD_NM"].SummaryItem.FieldName = "BIZCD_NM";
            //gridView1.Columns["BIZCD_NM"].SummaryItem.DisplayFormat = "총인원: {0:n2}";
            gridView1.Columns["BIZCD_NM"].SummaryItem.DisplayFormat = "총인원: {0:0}";

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("COMPANYCD", txtCOMPANYCD)
                      , new ColumnControlSet("COIDNO", txtCOIDNO)
                      , new ColumnControlSet("NAME", txtNAME)
                      , new ColumnControlSet("CHINESENAME", txtCHINESENAME)
                      , new ColumnControlSet("ENGLISHNAME", txtENGLISHNAME)
                      , new ColumnControlSet("IDCARDNO", txtIDCARDNO)
                      , new ColumnControlSet("SEXTYPE", txtSEXTYPE)
                      , new ColumnControlSet("RESITYPE", txtRESITYPE)
                      , new ColumnControlSet("ISOCODE", txtISOCODE)
                      , new ColumnControlSet("VISANO", txtVISANO)
                      , new ColumnControlSet("MIDCARDNO", txtMIDCARDNO)
                      , new ColumnControlSet("UNIONORNOT", chkUNIONORNOT)
                      , new ColumnControlSet("FTAXTYPE", chkFTAXTYPE)
                      , new ColumnControlSet("MARRIAGEORNOT", chkMARRIAGEORNOT)
                      , new ColumnControlSet("RESTYN", chkRESTYN)
                      //, new ColumnControlSet("COMPANYNAME"   , txtCOMPANYNAME)
                      , new ColumnControlSet("BIZCD", cmbBIZCD)
                      //, new ColumnControlSet("DEPTNAME"      , txtDEPTNAME.EditValue2)
                      //, new ColumnControlSet("DUTYCODE"      , txtDUTYCODE)
                      , new ColumnControlSet("ENTERINGDATE", dtENTERINGDATE)
                      , new ColumnControlSet("RETIREMENTDATE", dtRETIREMENTDATE)
                      , new ColumnControlSet("MEMO", txtMEMO)
                      , new ColumnControlSet("OCCUPATIONKIND", cmbOCCUPATIONKIND)
                      , new ColumnControlSet("PAYMENTTYPE", cmbPAYMENTTYPE)
                      , new ColumnControlSet("ERP_USEYN", chkERP_USEYN)
                      //, new ColumnControlSet("LOGINID"       , txtLOGINID)
                      , new ColumnControlSet("USERID", txtUSERID)
                      //, new ColumnControlSet("PWD"           , txtPWD)
                      , new ColumnControlSet("COMPANYNAME", txtCOMPANYNAME)
                      , new ColumnControlSet("COIDNO_FILE_NM", lblFILE_GRP_ID)
                      , new ColumnControlSet("SAVE_COIDNO_FILE_NM", lblFILE_GRP_ID2)
                      , new ColumnControlSet("ZIPNO", txtZIPNO)
                      , new ColumnControlSet("ADDR1", txtADDR1)
                      , new ColumnControlSet("ADDR2", txtADDR2)
                      , new ColumnControlSet("HPNO", txtHPNO)
                      , new ColumnControlSet("EMAIL", txtEMAIL)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;

            this.efwGridControl2.BindControlSet(
                      new ColumnControlSet("ID", txtID2)
                      , new ColumnControlSet("COIDNO", txtCOIDNO)
                      , new ColumnControlSet("FRELATION", cmbFRELATION)
                      , new ColumnControlSet("FNATION", cmbFNATION)
                      , new ColumnControlSet("NAME", txtNAME2)
                      , new ColumnControlSet("IDCARDNO", txtIDCARDNO2)
                      , new ColumnControlSet("ACADEMY", cmbACADEMY)
                      , new ColumnControlSet("JOB", txtJOB)
                      , new ColumnControlSet("LIVINGTOGETHER", cmbLIVINGTOGETHER)
                      );
        }

        #endregion

        public override void FrmShown()
        {
            base.FrmShown();

            fValueSet();

            cmbBIZCDQ.ItemIndex = 0;

            //if (!string.IsNullOrEmpty(this.pCoIdNo))
            //{

            //}
            Open1();
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

                CodeAgent.SetLegacyCode(cmbBIZCD, strQueruy);
                CodeAgent.SetLegacyCode(cmbBIZCDQ, strQueruy2);
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        #endregion


        #region 신규

        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
            Eraser.Clear(this, "CLR2");
            Eraser.Clear(this, "CLRA");

            fValueSet();
        }

        #endregion

        #region 조회

        private void Open1()
        {
            try
            {
                base.Search();

                if (efwRadioGroup1.SelectedIndex == -1)
                {
                    efwRadioGroup1.SelectedIndex = 0;
                }

                DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MA_MA01_SELECT_02"
                    , txtCOMPANYCD.EditValue
                    , cmbBIZCDQ.EditValue
                    , efwRadioGroup1.Properties.Items[efwRadioGroup1.SelectedIndex].Value.ToString()
                    , txtNAMEQ.EditValue
                    );

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        #endregion



        #region 이벤트

        private void txtDEPTNAME_Click(object sender, EventArgs e)
        {
            frmDeptInfo FrmInfo = new frmDeptInfo() { ParentBtn = txtDEPTNAME };
            FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
            FrmInfo.COMPANYNAME = txtCOMPANYNAME.EditValue.ToString();
            FrmInfo.ShowDialog();
        }

        private void txtDUTYCODE_Click(object sender, EventArgs e)
        {
            frmDutyInfo FrmInfo = new frmDutyInfo() { ParentBtn = txtDUTYCODE };
            FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
            FrmInfo.COMPANYNAME = txtCOMPANYNAME.EditValue.ToString();
            FrmInfo.ShowDialog();
        }

        private void btnOpen1_Click(object sender, EventArgs e)
        {
            Open1();
            NewMode();
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            if (txtCOMPANYCD.Text == string.Empty)
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사를 선택하세요 !!");
                return;
            }

            frmPopUpFileUpload frmFileUpload = new frmPopUpFileUpload() { /*폼 뷰잉전에 설정*/UploadKey = "YLEFW.TBL_BASIC.SAVE_COIDNO_FILE_NM" };
            picImg.DBKey = "YLEFW.TBL_BASIC.SAVE_COIDNO_FILE_NM";
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

            if (dr != null && dr["DEPTCODE"].ToString() != "")
            {
                this.txtDEPTNAME.EditValue2 = dr["DEPTCODE"].ToString();
                this.txtDEPTNAME.Text = dr["DEPTNAME"].ToString();
            }
            if (dr != null && dr["DUTYCODE"].ToString() != "")
            {
                this.txtDUTYCODE.EditValue2 = dr["DUTYCODE"].ToString();
                this.txtDUTYCODE.Text = dr["DUTYNAME"].ToString();
            }
            if (dr != null && dr["ZIPNO"].ToString() != "")
            {
                this.txtZIPNO.EditValue2 = dr["ZIPNO"].ToString();
                this.txtZIPNO.Text = dr["ZIPNO"].ToString();
            }

            picImg.Image = null;

            picImg.DBKey = "YLEFW.TBL_BASIC.SAVE_COIDNO_FILE_NM";
            //파일서버에서 이미지 가져오기
            if (dr["SAVE_COIDNO_FILE_NM"].ToString() != null)
                WebFileAgent.GetImageFile(picImg, dr["SAVE_COIDNO_FILE_NM"].ToString());

            //개인신상 조회
            OpenA();
            //가족사항 조회
            OpenB();

            _isNewMode = false;
        }

        private void TxtZIPNO_Click(object sender, EventArgs e)
        {
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = txtZIPNO, ParentAddr1 = txtADDR1, ParentAddr2 = txtADDR2 };
            FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
            FrmInfo.COMPANYNAME = txtCOMPANYNAME.EditValue.ToString();
            FrmInfo.ShowDialog();
            txtADDR2.Focus();
        }




        #region 아이디 중복검사

        private void btnDupChk_Click(object sender, EventArgs e)
        {
            ////ERP사용자 중복검사
            //if (string.IsNullOrEmpty(this.txtCOMPANYCD.Text))
            //{
            //    MessageAgent.MessageShow(MessageType.Warning, "회사코드가 없습니다!");
            //    txtCOMPANYCD.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.cmbBIZCD.Text))
            //{
            //    MessageAgent.MessageShow(MessageType.Warning, "사업장을 선택하세요!");
            //    cmbBIZCD.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.txtLOGINID.Text))
            //{
            //    MessageAgent.MessageShow(MessageType.Warning, "로그인ID를 입력하세요!");
            //    txtLOGINID.Focus();
            //    return;
            //}

            //string strQuery = string.Format(@"SELECT COUNT(*) AS ID_CNT FROM TBL_BASIC WHERE COMPANYCD='{0}' AND BIZCD='{1}' AND LOGINID='{2}'"
            //    , this.txtCOMPANYCD.EditValue, this.cmbBIZCD.EditValue, this.txtLOGINID.EditValue);

            //DataSet ds = ServiceAgent.ExecuteDataSetStr("CONIS_IBS", strQuery);

            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
            //{
            //    int retVal = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_CNT"]);

            //    if (retVal > 0)
            //    {
            //        MessageAgent.MessageShow(MessageType.Warning, "이미 존재하는 ID 입니다.");
            //        _isIDChk = false;
            //    }
            //    else
            //    {
            //        MessageAgent.MessageShow(MessageType.Warning, "사용할수 있는ID 입니다.");
            //        _isIDChk = true;
            //    }
            //}
        }

        #endregion

        #endregion

        #region 기타메서드

        private void chkTab()
        {
            if (txtCOIDNO.EditValue == null || txtCOIDNO.EditValue.ToString() == "")
            {
                xtraTabControl1.Enabled = false;
            }
            else
            {
                xtraTabControl1.Enabled = true;
            }
        }

        private void fValueSet()
        {
            string syymm = DateTime.Now.ToString("yyyyMM");



            txtISOCODE.EditValue = "KR";         // 국가
            cmbOCCUPATIONKIND.EditValue = "01";  // 직종구분
            cmbPAYMENTTYPE.EditValue = "01";     // 급여지급형태
            txtRESITYPE.EditValue = "1";         // 거주구분
            //efwRadioGroup1.EditValue = "01";

            //txtLOGINID.Enabled = false;
            txtUSERID.Enabled = false;
            //btnDupChk.Enabled = false;

            //사원번호 자동생성
            //string strQuery = string.Format(@"SELECT COUNT(*) AS ID_CNT FROM TBL_BASIC WHERE COMPANYCD='{0}' AND BIZCD='{1}' AND LOGINID='{2}'"
            string strQuery = string.Format(@"SELECT ISNULL(dbo.LPAD(MAX(SUBSTRING(COIDNO, 7, 4)) + 1, 4, '0'), '0001') AS SEQ  FROM dbo.TBL_BASIC WHERE SUBSTRING(COIDNO, 1, 6)='{0}'"
                 , syymm);

            DataSet ds = ServiceAgent.ExecuteDataSetStr("CONIS_IBS", strQuery);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                txtCOIDNO.EditValue = syymm + ds.Tables[0].Rows[0]["SEQ"];
            }

            txtNAME.Focus();
            _isNewMode = true;
        }




        #endregion

        #region 인사기본

        #region 저장

        public override void Save()
        {
            //base.Save();

            //if (chkERP_USEYN.Checked == true && string.IsNullOrEmpty(this.txtUSERID.Text))
            //{
            //    //처음으로 ERP사용자로 등록하는경우
            //    //if (_isNewMode && !_isIDChk)
            //    if (!_isIDChk)
            //    {
            //        MessageAgent.MessageShow(MessageType.Warning, "로그인ID 중복검사를 하시길 바랍니다.");
            //        return;
            //    }

            //}

            //기본정보 저장
            if (txtCOMPANYCD.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사코드가 없습니다!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                if (ValidationAgentEx.IsRequireCheck(this.layoutControl2.Controls, "R1"))
                {
                    if (ValidationAgentEx.IsRequireCheck(this.layoutControl3.Controls, "R1"))
                    {
                        if (ValidationAgentEx.IsRequireCheck(this.layoutControl4.Controls, "R1"))
                        {
                            try
                            {
                                DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MA_MA01_SAVE_02"
                                //int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MA_MA01_SAVE_02"
                                                                 , UserInfo.instance().UserId
                                                                , this.txtCOMPANYCD.EditValue
                                                                , this.txtCOIDNO.EditValue
                                                                , this.txtNAME.EditValue
                                                                , this.txtCHINESENAME.EditValue
                                                                , this.txtENGLISHNAME.EditValue
                                                                , this.txtIDCARDNO.EditValue
                                                                , this.txtSEXTYPE.EditValue
                                                                , this.txtRESITYPE.EditValue
                                                                , this.txtISOCODE.EditValue
                                                                , this.txtVISANO.EditValue
                                                                , this.txtMIDCARDNO.EditValue
                                                                , this.chkUNIONORNOT.Checked == true ? "Y" : "N"
                                                                , this.chkFTAXTYPE.Checked == true ? "Y" : "N"
                                                                , this.chkMARRIAGEORNOT.Checked == true ? "Y" : "N"
                                                                , this.chkRESTYN.Checked == true ? "Y" : "N"
                                                                , this.cmbBIZCD.EditValue
                                                                , this.txtDEPTNAME.EditValue2
                                                                , this.txtDUTYCODE.EditValue2
                                                                , this.dtENTERINGDATE.EditValue3
                                                                , this.dtRETIREMENTDATE.EditValue3
                                                                , this.txtMEMO.EditValue
                                                                , this.cmbOCCUPATIONKIND.EditValue
                                                                , this.cmbPAYMENTTYPE.EditValue
                                                                , this.chkERP_USEYN.Checked == true ? "Y" : "N"
                                                                //, this.txtLOGINID.EditValue
                                                                , this.txtUSERID.EditValue
                                                                //, this.txtPWD.EditValue
                                                                , this.lblFILE_GRP_ID.Text
                                                                , this.lblFILE_GRP_ID2.Text
                                                                , this.txtZIPNO.EditValue
                                                                , this.txtADDR1.EditValue
                                                                , this.txtADDR2.EditValue
                                                                , this.txtHPNO.EditValue
                                                                , this.txtEMAIL.EditValue
                                                                );

                                //if (retVal > 0)
                                if (ds.Tables.Count > 0)
                                {
                                    this.txtUSERID.EditValue = ds.Tables[0].Rows[0]["NEW_ID"];
                                    MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                                    Open1();

                                    //NewMode();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region 삭제

        public override void Delete()
        {
            base.Delete();

            if (string.IsNullOrEmpty(this.txtCOMPANYCD.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사코드가 없습니다!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtNAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "삭제할 사원이 없습니다!");
                return;
            }

            DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?");

            if (drt == DialogResult.OK)
            {
                try
                {
                    DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MA_MA01_DELETE_02"
                                                        , UserInfo.instance().UserId
                                                        , this.txtCOMPANYCD.EditValue
                                                        , this.cmbBIZCD.EditValue
                                                        , this.txtCOIDNO.EditValue
                                                        , this.txtUSERID.EditValue
                                                        );

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["ERRC"].ToString() == "ERR")
                        {
                            MessageAgent.MessageShow(MessageType.Error, ds.Tables[0].Rows[0]["ERRM"].ToString());
                        }
                        else
                        {
                            MessageAgent.MessageShow(MessageType.Informational, "삭제되었습니다.");
                            Open1();
                            NewMode();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        #endregion

        #endregion

        #region 개인신상

        #region 조회

        private void OpenA()
        {
            try
            {
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA01_SELECT_03"
                    , txtCOMPANYCD.EditValue
                    , cmbBIZCD.EditValue
                    , txtCOIDNO.EditValue
                    );

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtBIRTHDATE.EditValue = ds.Tables[0].Rows[0]["BIRTHDATE"].ToString();
                    txtMARRIEDDATE.EditValue = ds.Tables[0].Rows[0]["MARRIEDDATE"].ToString();
                    cmbRESIDENTTYPE.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();
                    cmbOWNERKIND.EditValue = ds.Tables[0].Rows[0]["OWNERKIND"].ToString();
                    txtCARNO.EditValue = ds.Tables[0].Rows[0]["CARNO"].ToString();
                    txtRELIGION.EditValue = ds.Tables[0].Rows[0]["RELIGION"].ToString();
                    txtLEFTSIGHT.EditValue = ds.Tables[0].Rows[0]["LEFTSIGHT"].ToString();
                    txtRIGHTSIGHT.EditValue = ds.Tables[0].Rows[0]["RIGHTSIGHT"].ToString();
                    cmbBLOODTYPE.EditValue = ds.Tables[0].Rows[0]["BLOODTYPE"].ToString();
                    txtHEIGHT.EditValue = ds.Tables[0].Rows[0]["HEIGHT"].ToString();
                    txtWEIGHT.EditValue = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                    txtHOUSEHOLDER.EditValue = ds.Tables[0].Rows[0]["HOUSEHOLDER"].ToString();
                    txtRELATION.EditValue = ds.Tables[0].Rows[0]["RELATION"].ToString();
                    txtHOBBY.EditValue = ds.Tables[0].Rows[0]["HOBBY"].ToString();
                    txtSPECIALABILITY.EditValue = ds.Tables[0].Rows[0]["SPECIALABILITY"].ToString();
                }
                else
                {
                    Eraser.Clear(this, "CLR2");
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        #endregion

        #region 저장

        private void BtnSave1_Click(object sender, EventArgs e)
        {
            //개인신상 저장
            if (_isNewMode)
            {
                MessageAgent.MessageShow(MessageType.Warning, "사원을 선택후 처리하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MA_MA01_SAVE_03"
                                                     , UserInfo.instance().UserId
                                                     , this.txtCOMPANYCD.EditValue
                                                     , this.cmbBIZCD.EditValue
                                                     , this.txtCOIDNO.EditValue
                                                     , this.txtBIRTHDATE.EditValue3
                                                     , this.txtMARRIEDDATE.EditValue3
                                                     , this.cmbRESIDENTTYPE.EditValue
                                                     , this.cmbOWNERKIND.EditValue
                                                     , this.txtCARNO.EditValue
                                                     , this.txtRELIGION.EditValue
                                                     , this.txtLEFTSIGHT.EditValue
                                                     , this.txtRIGHTSIGHT.EditValue
                                                     , this.cmbBLOODTYPE.EditValue
                                                     , this.txtHEIGHT.EditValue
                                                     , this.txtWEIGHT.EditValue
                                                     , this.txtHOUSEHOLDER.EditValue
                                                     , this.txtRELATION.EditValue
                                                     , this.txtHOBBY.EditValue
                                                     , this.txtSPECIALABILITY.EditValue
                                                     );
                    if (retVal > 0)
                    {
                        OpenA();
                        MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        #endregion

        #endregion

        #region 부양가족

        #region 신규

        private void BtnNew2_Click(object sender, EventArgs e)
        {
            //신규
            Eraser.Clear(this, "CLRA");
        }

        #endregion

        #region 조회

        private void OpenB()
        {
            try
            {
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA01_SELECT_04"
                   , txtCOMPANYCD.EditValue
                   , cmbBIZCD.EditValue
                   , txtCOIDNO.EditValue
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

        #region 저장

        private void BtnSave2_Click(object sender, EventArgs e)
        {
            //부양가족 저장
            if (_isNewMode)
            {
                MessageAgent.MessageShow(MessageType.Warning, "사원을 선택후 처리하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MA_MA01_SAVE_04"
                                                     , UserInfo.instance().UserId
                                                     , this.txtCOMPANYCD.EditValue
                                                     , this.cmbBIZCD.EditValue
                                                     , this.txtCOIDNO.EditValue
                                                     , Convert.ToInt32(this.txtID2.EditValue)
                                                     , this.cmbFRELATION.EditValue
                                                     , this.cmbFNATION.EditValue
                                                     , this.txtNAME2.EditValue
                                                     , this.txtIDCARDNO2.EditValue
                                                     , this.cmbACADEMY.EditValue
                                                     , this.txtJOB.EditValue
                                                     , this.cmbLIVINGTOGETHER.EditValue
                                                     );
                    if (retVal > 0)
                    {
                        OpenB();
                        MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        #endregion

        #region 삭제

        private void BtnDel2_Click(object sender, EventArgs e)
        {
            //부양가족 삭제
            if (txtID2.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "사원을 선택후 처리하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtID2.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "처리할 데이타가 선택되지 않았습니다!");
                cmbFRELATION.Focus();
                return;
            }

            DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?");

            if (drt == DialogResult.OK)
            {
                try
                {
                    DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MA_MA01_DELETE_03"
                                             , UserInfo.instance().UserId
                                             , Convert.ToInt32(this.txtID2.EditValue)
                                             );

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["ERRC"].ToString() == "ERR")
                        {
                            MessageAgent.MessageShow(MessageType.Error, ds.Tables[0].Rows[0]["ERRM"].ToString());
                        }
                        else
                        {
                            MessageAgent.MessageShow(MessageType.Informational, "삭제되었습니다.");
                            Eraser.Clear(this, "CLRA"); ;
                            OpenB();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }








        }



        #endregion

        #endregion

        private void GridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.ForeColor = Color.Red;
        }

        private void CmbBIZCDQ_EditValueChanged(object sender, EventArgs e)
        {
            btnOpen1_Click(null, null);
        }
    }
}
