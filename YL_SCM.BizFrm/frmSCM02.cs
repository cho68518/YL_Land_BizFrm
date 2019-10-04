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
    public partial class frmSCM02 : FrmBase
    {
        public frmSCM02()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmSCM01";
            //폼명설정
            this.FrmName = "품목별 재고등록";

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
            rbShowType.EditValue = "Y";
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM02_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(cmbSellers.EditValue);

                        cmd.Parameters.Add("i_ProdName", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtProdName.EditValue;

                        if (rbShowType.EditValue.ToString() != "Y" && rbShowType.EditValue.ToString() != "N")
                            sShow_Type = null;
                        else
                            sShow_Type = rbShowType.EditValue.ToString();

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = sShow_Type;
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
        public override void Save()
        {
            try
            {

                var saveResult = new SaveTableResultInfo() { IsError = true };

                var dt = efwGridControl1.GetChangeDataWithRowState;
                var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                string sBunch = string.Empty;
                string sChkDate = string.Empty;
                DateTime sNowDate;
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][StatusColumn].ToString() == "U")
                    {
                        //Console.WriteLine("------------------------------------------------------------");
                        //Console.WriteLine("[U] " + dt.Rows[i]["o_code"].ToString());
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM02_SAVE_01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_id", MySqlDbType.Int32, 10);
                                cmd.Parameters[0].Value = Convert.ToInt32(dt.Rows[i]["id"]).ToString();

                                cmd.Parameters.Add("i_option_id", MySqlDbType.Int32, 11);
                                cmd.Parameters[1].Value = Convert.ToInt32(dt.Rows[i]["option_id"]).ToString();


                                if (dt.Rows[i]["p_bunch_delivery"].ToString() != "Y" && dt.Rows[i]["p_bunch_delivery"].ToString() != "N")
                                    sBunch = "N";
                                else
                                    sBunch = dt.Rows[i]["p_bunch_delivery"].ToString();


                                cmd.Parameters.Add("i_p_bunch_delivery", MySqlDbType.VarChar, 1);
                                cmd.Parameters[2].Value = sBunch;

                                cmd.Parameters.Add("i_p_stock_num", MySqlDbType.Int32, 10);
                                cmd.Parameters[3].Value = Convert.ToInt32(dt.Rows[i]["p_stock_num"]).ToString();

                                cmd.Parameters.Add("i_p_input_due_date", MySqlDbType.VarChar, 50);
                                cmd.Parameters[4].Value = dt.Rows[i]["p_input_due_date"].ToString();

                                                               
                                cmd.Parameters.Add("i_p_input_due_qty", MySqlDbType.Int32, 10);
                                cmd.Parameters[5].Value = Convert.ToInt32(dt.Rows[i]["p_input_due_qty"]).ToString();

                                // this.dtE_DATE.EditValue = new System.DateTime(2019, 5, 30, 13, 53, 11, 0);

                                sChkDate = dt.Rows[i]["p_plan_date"].ToString();

                                if (sChkDate == "")
                                    sNowDate = Convert.ToDateTime(new System.DateTime(2018, 12, 31, 12, 01, 01, 0));
                                else
                                    sNowDate = Convert.ToDateTime(dt.Rows[i]["p_plan_date"].ToString());

                                cmd.Parameters.Add("i_p_plan_date", MySqlDbType.DateTime);
                                cmd.Parameters[6].Value = sNowDate;


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
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
        }
        private void txtProdName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }



    }
}
