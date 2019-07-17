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

namespace YL_MM.BizFrm.Dlg
{
    public partial class frmMemberInfo : FrmPopUpBase
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

        public string MTYPE
        {
            get;
            set;
        }

        public string USER_ID
        {
            get;
            set;
        }

        public string U_NAME
        {
            get;
            set;

        }
        public string U_NICKNAME
        {
            get;
            set;
        }

        public string U_ID
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

        #region Fields


        #endregion

        public frmMemberInfo()
        {
            InitializeComponent();

        }

        private void FrmMemberInfo_Load(object sender, EventArgs e)
        {
            this.Text = " 회원정보 (" + COMPANYNAME + ")";

            //txtCOMPANYNAME.Text = COMPANYNAME;
            gridView1.IndicatorWidth = 24;

            setCmb();
            cmbQ1.EditValue = "0";

            if (MTYPE != "0")
            {
                Open1();
            }
        }

        #region 조회

        private void setCmb()
        {
            try
            {
                //사업장콤보
                string strQueruy = @"SELECT
                                        CODE    DCODE
                                       ,CODE_NM DNAME
                                     FROM ETCCODE
                                     WHERE SYS_CODE = '03' AND GRP_CODE='MEMBERQ_GBN1' AND CODE <> 'X'" +
                                       " ORDER BY RANKORDER ASC";
                CodeAgent.SetLegacyCode(cmbQ1, strQueruy);
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open1()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM05_SELECT_03"
                                                        , this.COMPANYCD
                                                        , this.cmbQ1.EditValue
                                                        , this.txtSearch.Text
                                                        , this.MTYPE);

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region 이벤트

        private void EfwGridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            //this.ParentBtn.Text = row["u_name"].ToString();
            //this.ParentBtn.EditValue2 = row["user_id"].ToString();

            this.USER_ID = row["user_id"].ToString();
            this.U_NAME  = row["u_name"].ToString();
            this.U_NICKNAME = row["u_nickname"].ToString();
            this.U_ID = row["u_id"].ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void BtnSearch1_Click(object sender, EventArgs e)
        {
            Open1();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Open1();
        }

        private void GridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = e.RowHandle.ToString();
        }

        #endregion


    }
}
