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
    public partial class frmPM05 : FrmBase
    {
     //   MySQLConn sconn = new MySQLConn(ConstantLib.BasicConn_Dev);
     //   MySQLConn sconn = new MySQLConn(ConstantLib.BasicConn_Real);
        bool _isNewMode;
        DataSet _dsDeptInfo = null;
        DataSet _dsDeptInfo2 = null;

        public ImageList imgOrganList { get; private set; }

        public frmPM05()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "PM05";
            //폼명설정
            this.FrmName = "주문 현황";
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

            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            //gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "총주문금액: {0:c}";
            gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";


            gridView1.Columns["o_donut_d_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_donut_d_cost"].SummaryItem.FieldName = "o_donut_d_cost";
            gridView1.Columns["o_donut_d_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_donut_m_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_donut_m_cost"].SummaryItem.FieldName = "o_donut_m_cost";
            gridView1.Columns["o_donut_m_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_donut_c_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_donut_c_cost"].SummaryItem.FieldName = "o_donut_c_cost";
            gridView1.Columns["o_donut_c_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_delivery_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_delivery_cost"].SummaryItem.FieldName = "o_delivery_cost";
            gridView1.Columns["o_delivery_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_purchase_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_purchase_cost"].SummaryItem.FieldName = "o_purchase_cost";
            gridView1.Columns["o_purchase_cost"].SummaryItem.DisplayFormat = "{0:c}";

            //this.efwGridControl1.BindControlSet(
            //new ColumnControlSet("id", txtID)
            //);
            //this.efwGridControl1.Click += efwGridControl1_Click;
            rbP_SHOW_TYPE.EditValue = "T";
            setCmb();
        }

        private void setCmb()
        {
            try
            {
                Dictionary<string, string> myRecord;
                int i = 0;

                //대분류
                string strQueruy = @"SELECT
                                        CODE    DCODE
                                       ,CODE_NM DNAME
                                     FROM dbo.ETCCODE
                                     WHERE GRP_CODE = 'DELIVERYTYPE' " +
                                       " ORDER BY RANKORDER";
                CodeAgent.SetLegacyCode(cmbO_TYPE, strQueruy);
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }


        }

        #region 신규

        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
        }

        #endregion


        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
               // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_PM_PM05_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtP_NAME.EditValue;

                        if (rbP_SHOW_TYPE.EditValue.ToString() != "Y" && rbP_SHOW_TYPE.EditValue.ToString() != "N")
                            sP_SHOW_TYPE = null;
                        else
                            sP_SHOW_TYPE = rbP_SHOW_TYPE.EditValue.ToString();

                        // efwRadioGroup1.Properties.Items[efwRadioGroup1.SelectedIndex].Value.ToString()
                        cmd.Parameters.Add("i_is_order", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = sP_SHOW_TYPE;

                        cmd.Parameters.Add("i_o_receive_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtO_RECEIVE_NAME.EditValue;

                        cmd.Parameters.Add("i_u_nickname", MySqlDbType.VarChar, 50);
                        cmd.Parameters[5].Value = txtU_NICKNAME.EditValue;

                        cmd.Parameters.Add("i_o_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = cmbO_TYPE.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        #endregion

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            _isNewMode = false;
        }

    }
}
