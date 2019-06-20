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

namespace YL_MM.BizFrm.Dlg
{
    public partial class frmMemberInfo_Dup : FrmPopUpBase
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

        public string U_NAME
        {
            get;
            set;
        }

        #endregion

        public frmMemberInfo_Dup()
        {
            InitializeComponent();
        }

        private void FrmMemberInfo_Dup_Load(object sender, EventArgs e)
        {
            //setCmb();
            cmbQ1.EditValue = "0";

            Open1();
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

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM05_SELECT_02"
                , this.cmbQ1.EditValue
                , this.txtSearch.Text
                );

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

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Open1();
        }

        private void EfwGridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            this.U_NAME = row["u_name"].ToString();
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


        #endregion


    }
}
