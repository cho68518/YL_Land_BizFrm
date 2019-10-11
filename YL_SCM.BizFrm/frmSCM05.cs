using DevExpress.XtraEditors.Repository;
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
using YL_SCM.BizFrm.Dlg;
namespace YL_SCM.BizFrm
{
    public partial class frmSCM05 : FrmBase
    {
        public frmSCM05()
        {
            InitializeComponent();
            this.QCode = " frmSCM05";
            //폼명설정
            this.FrmName = "기간별 출고마감현황";


        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView1.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["c_delivery_price"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["c_delivery_price"].SummaryItem.FieldName = "c_delivery_price";
            gridView1.Columns["c_delivery_price"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["c_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["c_amt"].SummaryItem.FieldName = "c_amt";
            gridView1.Columns["c_amt"].SummaryItem.DisplayFormat = "{0:c}";
            cmbSellers.EditValue = "N";

            SetCmb();
        }
        private void SetCmb()
        {
            // 공급자구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(s_idx,'') as DCODE ,s_company_name as DNAME  FROM domaadmin.tb_sellers_info where s_status = 'Y' order by s_company_name ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbSellers, codeArray);
            }
            cmbSellers.EditValue = "1";
        }
        public override void Search()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM05_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(cmbSellers.EditValue);

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[2].Value = dtE_DATE.EditValue3;



                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }

                txtOutQty.EditValue = Convert.ToInt32(gridView1.Columns["p_num"].SummaryItem.SummaryValue);
                txtOutAmt.EditValue = Convert.ToInt32(gridView1.Columns["o_total_cost"].SummaryItem.SummaryValue);
                txtDeliveryPrice.EditValue = Convert.ToInt32(gridView1.Columns["c_delivery_price"].SummaryItem.SummaryValue);
                txtConfirmAmt.EditValue = Convert.ToInt32(gridView1.Columns["c_amt"].SummaryItem.SummaryValue);

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        public override void Save()
        {
            try
            {

                var saveResult = new SaveTableResultInfo() { IsError = true };

                var dt = efwGridControl1.GetChangeDataWithRowState;
                var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][StatusColumn].ToString() == "U")
                    {
                        //Console.WriteLine("------------------------------------------------------------");
                        //Console.WriteLine("[U] " + dt.Rows[i]["o_code"].ToString());
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM05_SAVE_01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_id", MySqlDbType.Int32, 50);
                                cmd.Parameters[0].Value = Convert.ToInt32(dt.Rows[i]["id"]).ToString();

                                cmd.Parameters.Add("i_c_delivery_price", MySqlDbType.Int32, 11);
                                cmd.Parameters[1].Value = Convert.ToInt32(dt.Rows[i]["c_delivery_price"]).ToString();


                                cmd.Parameters.Add("i_c_price", MySqlDbType.Int32, 11);
                                cmd.Parameters[2].Value = Convert.ToInt32(dt.Rows[i]["c_price"]).ToString();

                                cmd.Parameters.Add("i_c_amt", MySqlDbType.Int32, 11);
                                cmd.Parameters[3].Value = Convert.ToInt32(dt.Rows[i]["c_amt"]).ToString();


                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
