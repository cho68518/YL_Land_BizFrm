using Easy.Framework.Common;
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

namespace YL_MA.BizFrm
{
    public partial class frmDutyInfo : DevExpress.XtraEditors.XtraForm
    {
        #region Propertys

        public string COMPANYCD
        {
            get;
            set;
        }

        public string COMPANYNAME
        {
            get;
            set;
        }

        public efwButtonEdit ParentBtn
        {
            get;
            set;
        }

        #endregion

        public frmDutyInfo()
        {
            InitializeComponent();
        }

        private void frmDutyInfo_Load(object sender, EventArgs e)
        {
            txtCOMPANYCD.EditValue = COMPANYCD;
            txtCOMPANYNAME.Text = COMPANYNAME;

            setCmb();

            if (UserInfo.instance().BIZCD != null && UserInfo.instance().BIZCD.ToString() != "")
                cmbBIZCD.EditValue = UserInfo.instance().BIZCD;
            else
                cmbBIZCD.EditValue = "01";

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("COMPANYCD"  , txtCOMPANYCD)
                      , new ColumnControlSet("RANKORDER", txtRANKORDER)
                      , new ColumnControlSet("DUTYCODE" , txtDUTYCODE)
                      , new ColumnControlSet("DUTYNAME" , txtDUTYNAME)
                      , new ColumnControlSet("COMMENT"  , txtMEMO)
                      );

            Open1();
        }

        #region 조회

        private void setCmb()
        {
            try
            {
                //사업장콤보
                string strQueruy = @"SELECT
                                        BIZCD    DCODE
                                       ,BIZCD_NM DNAME
                                     FROM TBL_BIZCD
                                     WHERE COMPANYCD = '" + COMPANYCD + "' " +
                                       " ORDER BY BIZCD ASC";
                CodeAgent.SetLegacyCode(cmbBIZCD, strQueruy);
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open1()
        {
            //직책정보조회
            try
            {
                //base.Search();
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA01_SELECT_01", COMPANYCD);

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

        private void brnSave_Click(object sender, EventArgs e)
        {
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
                        int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MA_MA01_SAVE_01"
                                                         , UserInfo.instance().UserId
                                                        , this.txtCOMPANYCD.EditValue
                                                        , this.cmbBIZCD.EditValue
                                                        , this.txtDUTYCODE.EditValue
                                                        , this.txtDUTYNAME.EditValue
                                                        , Convert.ToInt16(this.txtRANKORDER.EditValue)
                                                        , this.txtMEMO.EditValue
                                                        );

                        if (retVal > 0)
                        {
                            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                            Open1();
                            btnNew_Click(null, null);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCOMPANYCD.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사코드가 없습니다!");
                return;
            }

            if (cmbBIZCD.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "사업장코드가 없습니다!");
                return;
            }

            DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?");

            if (drt == DialogResult.OK)
            {
                try
                {
                    int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MA_MA01_DELETE_01"
                                                        , UserInfo.instance().UserId
                                                        , this.txtCOMPANYCD.EditValue
                                                        , this.cmbBIZCD.EditValue
                                                        , this.txtDUTYCODE.EditValue
                                                        );

                    if (retVal > 0)
                    {
                        MessageAgent.MessageShow(MessageType.Informational, "삭제되었습니다.");
                        Open1();
                        btnNew_Click(null, null);
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

        private void btnSel_Click(object sender, EventArgs e)
        {
            if (txtDUTYCODE.EditValue != null && txtDUTYCODE.EditValue.ToString() != "")
            {
                //DataRow dr = this.efwGridControl3.GetSelectedRow(0);
                //this.ParentBtn.Text = dr["CAPTION"].ToString();
                //this.ParentBtn.EditValue2 = dr["ID"];

                this.ParentBtn.Text = txtDUTYNAME.EditValue.ToString();
                this.ParentBtn.EditValue2 = txtDUTYCODE.EditValue.ToString();

                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Eraser.Clear(this.Controls, "CLR1");
        }




        #endregion

      
    }
}
