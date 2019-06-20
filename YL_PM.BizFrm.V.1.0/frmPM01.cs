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
using YL_PM.BizFrm;
using YL_PM.BizFrm.Dlg;

namespace YL_PM.BizFrm
{
    public partial class frmPM01 : FrmBase
    {
        bool _isNewMode;
        public frmPM01()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "PM01";
            //폼명설정
            this.FrmName = "매입거래처";
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
                cmbBIZCDQ.EditValue = UserInfo.instance().BIZCD;
                            }
            else
            {
                cmbBIZCDQ.EditValue = "01";
                cmbLARGETYPE.EditValue = "01";
            }

            gridView1.OptionsView.ShowFooter = true;

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("COMPANYCD", txtCOMPANYCD)
                      , new ColumnControlSet("BIZCD", cmbBIZCD)
                      , new ColumnControlSet("PURCHASECODE", txtPURCHASECODE)
                      , new ColumnControlSet("PURCHASENAME", txtPURCHASENAME)
                      , new ColumnControlSet("REGISTERNO", txtREGISTERNO)
                      , new ColumnControlSet("LARGETYPE", cmbLARGETYPE)
                      , new ColumnControlSet("INDATE", dtINDATE)
                      , new ColumnControlSet("DEALSTOPDATE", dtDEALSTOPDATE)
                      , new ColumnControlSet("DEALSTOPREASON", txtDEALSTOPREASON)
                      , new ColumnControlSet("HANDPHONE", txtHANDPHONE)
                      , new ColumnControlSet("EMAILADD", txtEMAILADD)
                      , new ColumnControlSet("PERSONNAME", txtPERSONNAME)
                      , new ColumnControlSet("SERVICETYPE", txtSERVICETYPE)
                      , new ColumnControlSet("SERVICENAME", txtSERVICENAME)
                      , new ColumnControlSet("CHARGENAME", txtCHARGENAME)
                      , new ColumnControlSet("TELNO", txtTELNO)
                      , new ColumnControlSet("FAXNO", txtFAXNO)
                      , new ColumnControlSet("POSTNO", txtPOSTNO)
                      , new ColumnControlSet("ADDRESS1", txtADDRESS1)
                      , new ColumnControlSet("ADDRESS2", txtADDRESS2)
                      , new ColumnControlSet("REMARK", txtREMARK)
                      , new ColumnControlSet("CUSTID", txtCUSTID)
                      , new ColumnControlSet("CUSTPASS", txtCUSTPASS)
                      , new ColumnControlSet("ACCHOLDER", txtACCHOLDER)
                      , new ColumnControlSet("BANKCODE", txtBANKCODE)
                      , new ColumnControlSet("SALENO", txtSALENO)
                      , new ColumnControlSet("ACCNO", txtACCNO)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;

            fValueSet();
            Open1();
        }

        #endregion
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
                CodeAgent.SetLegacyCode(cmbBIZCD, strQueruy);

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

                CodeAgent.SetLegacyCode(cmbBIZCDQ, strQueruy2);


                //대분류
                string strQueruy3 = @"SELECT
                                        CODE    DCODE
                                       ,CODE_NM DNAME
                                     FROM ETCCODE
                                    WHERE GRP_CODE = 'PURLARGETYPE' 
                                    ORDER BY GRP_CODE ASC";
                CodeAgent.SetLegacyCode(cmbLARGETYPE, strQueruy3);

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
            
            fValueSet();
            Open1();
        }

        #endregion

        private void fValueSet()
        {
            string syymm = DateTime.Now.ToString("yyyyMM");



            // 거래처코드 자동 생성
            //string strQuery = string.Format(@"SELECT COUNT(*) AS ID_CNT FROM TBL_BASIC WHERE COMPANYCD='{0}' AND BIZCD='{1}' AND LOGINID='{2}'"
            string strQuery = string.Format(@"SELECT ISNULL(dbo.LPAD(MAX(PURCHASECODE) + 1, 6, '0'), '000001') AS SEQ  FROM dbo.TBL_PURCHASEMASTER ");

            DataSet ds = ServiceAgent.ExecuteDataSetStr("CONIS_IBS", strQuery);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                txtPURCHASECODE.EditValue = ds.Tables[0].Rows[0]["SEQ"];
            }
            cmbBIZCDQ.EditValue = "";
            txtPURCHASENAME.Focus();
            _isNewMode = true;
        }


        #region 조회


        private void Open1()
        {
            try
            {
                base.Search();

                DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_PM_PM01_SELECT_01"
                    , txtCOMPANYCD.EditValue
                    , cmbBIZCDQ.EditValue
                    , txtNAMEQ.EditValue
                    );

                efwGridControl1.DataBind(ds);
             //   this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            Open1();
        }
        #endregion

        #region 저장

        public override void Save()
        {
            
            //기본정보 저장
            if (txtPURCHASECODE.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, " 거래처 코드가 없습니다!");
                return;
            }

            if (txtPURCHASENAME.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, " 거래처명이 없습니다!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                if (ValidationAgentEx.IsRequireCheck(this.layoutControl2.Controls, "P1"))
                {
                     try
                     {
                      //DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_PM_PM01_SAVE_01"
                      int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_PM_PM01_SAVE_01"
                                                                 , UserInfo.instance().UserId
                                                                 , this.txtCOMPANYCD.EditValue
                                                                 , this.cmbBIZCD.EditValue
                                                                 , this.txtPURCHASECODE.EditValue
                                                                 , this.txtPURCHASENAME.EditValue
                                                                 , this.txtREGISTERNO.EditValue
                                                                 , this.cmbLARGETYPE.EditValue
                                                                 , this.dtINDATE.EditValue3
                                                                 , this.dtDEALSTOPDATE.EditValue3
                                                                 , this.txtDEALSTOPREASON.EditValue
                                                                 , this.txtHANDPHONE.EditValue
                                                                 , this.txtEMAILADD.EditValue
                                                                 , this.txtPERSONNAME.EditValue
                                                                 , this.txtSERVICETYPE.EditValue
                                                                 , this.txtSERVICENAME.EditValue
                                                                 , this.txtCHARGENAME.EditValue
                                                                 , this.txtTELNO.EditValue
                                                                 , this.txtFAXNO.EditValue
                                                                 , this.txtPOSTNO.EditValue
                                                                 , this.txtADDRESS1.EditValue
                                                                 , this.txtADDRESS2.EditValue
                                                                 , this.txtCUSTID.EditValue
                                                                 , this.txtCUSTPASS.EditValue
                                                                 , this.txtACCHOLDER.EditValue
                                                                 , this.txtBANKCODE.EditValue2
                                                                 , this.txtSALENO.EditValue
                                                                 , this.txtACCNO.EditValue
                                                                );

                          if (retVal > 0)
                          //if (ds.Tables.Count > 0)
                          {
                              //this.txtPURCHASECODE.EditValue = ds.Tables[0].Rows[0]["NEW_ID"];
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

            if (string.IsNullOrEmpty(this.txtPURCHASENAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "삭제할 거래처명이 없습니다!");
                return;
            }

            DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?");

            if (drt == DialogResult.OK)
            {
                try
                {
                    int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_PM_PM01_DELETE_01"
                                                        , UserInfo.instance().UserId
                                                        , this.txtCOMPANYCD.EditValue
                                                        , this.txtPURCHASECODE.EditValue
                                                        );
                    if (retVal > 0)
                    //    if (ds.Tables.Count > 0)
                    {
                        MessageAgent.MessageShow(MessageType.Informational, "삭제되었습니다.");
                        Open1();
                        NewMode();
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        #endregion
        #region 이벤트



        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["POSTNO"].ToString() != "")
            {
                this.txtPOSTNO.EditValue2 = dr["POSTNO"].ToString();
                this.txtPOSTNO.Text = dr["POSTNO"].ToString();
            }

            if (dr != null && dr["BANKCODE"].ToString() != "")
            {
                this.txtBANKCODE.EditValue2 = dr["BANKCODE"].ToString();
                this.txtBANKCODE.Text = dr["BANKNAME"].ToString();
            }
            _isNewMode = false;
        }

        #endregion

        private void TxtPOSTNO_Click(object sender, EventArgs e)
        {
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = txtPOSTNO, ParentAddr1 = txtADDRESS1, ParentAddr2 = txtADDRESS2 };
            FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
            FrmInfo.COMPANYNAME = txtCOMPANYCD.EditValue.ToString();
            FrmInfo.ShowDialog();
            txtADDRESS2.Focus();
        }

        private void txtBANKCODE_Click(object sender, EventArgs e)
        {
            frmBankInfo FrmInfo = new frmBankInfo() { ParentBtn = txtBANKCODE };
            FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
            FrmInfo.COMPANYNAME = txtCOMPANYNAME.EditValue.ToString();
            FrmInfo.ShowDialog();
        }

        private void TxtNAMEQ_EditValueChanged(object sender, EventArgs e)
        {
            Open1();
        }
    }
}
