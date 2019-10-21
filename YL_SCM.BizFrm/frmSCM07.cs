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
    public partial class frmSCM07 : FrmBase
    {
        public frmSCM07()
        {
            InitializeComponent();
            this.QCode = " frmSCM07";
            //폼명설정
            this.FrmName = "출금 등록";
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            gridView1.OptionsView.ShowFooter = true;
            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("pay_type", cmbPayType)
            , new ColumnControlSet("out_date", dtOutDate)
            , new ColumnControlSet("store_code", cmbInSellers)
            , new ColumnControlSet("amt", txtAmt)
            , new ColumnControlSet("remark", txtRemark)
            , new ColumnControlSet("idx", txtIDX)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;

            cmbSellers.EditValue = "N";
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            dtOutDate.EditValue = DateTime.Now;
            SetCmb();
        }


        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            //if (dr != null && dr["doramd_type"].ToString() != "")
            //{
            //    this.cbDORAMD_TYPE.EditValue = dr["doramd_type"].ToString();
            //    //this.cbDORAMD_TYPE.Text = Convert.ToInt16(dr["doramd_type"]).ToString();
            //    this.cbDORAMD_TYPE.Text = dr["doramd_type"].ToString();
            //}
            //Open3();
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

                CodeAgent.MakeCodeControl(this.cmbInSellers, codeArray);
            }
            cmbInSellers.EditValue = "1";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00013' order by code_id ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbPayType, codeArray);
            }
            cmbPayType.EditValue = "1";


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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM07_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_store_code", MySqlDbType.Int32);
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
                        //    this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAmt.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 금액을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtRemark.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 적요를 입력하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM07_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_store_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_store_code"].Value = cmbInSellers.EditValue;
                            cmd.Parameters["i_store_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pay_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_pay_type"].Value = cmbPayType.EditValue;
                            cmd.Parameters["i_pay_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_out_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_out_date"].Value = dtOutDate.EditValue;
                            cmd.Parameters["i_out_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_amt", MySqlDbType.Int32));
                            cmd.Parameters["i_amt"].Value = Convert.ToInt32(txtAmt.EditValue);
                            cmd.Parameters["i_amt"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("o_Idx", MySqlDbType.Int32));
                            cmd.Parameters["o_Idx"].Direction = ParameterDirection.Output;


                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            txtIDX.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                            

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
            Search();
        }

    }
}
