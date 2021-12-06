using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Report;
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
using YL_DONUT.BizFrm.Dlg;

namespace YL_DONUT.BizFrm
{
    public partial class frmDN27 : FrmBase
    {
        public frmDN27()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "DN27";
            //폼명설정
            this.FrmName = "마스터관리";
        }

        private void frmDN27_Load(object sender, EventArgs e)
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
            dtCreate_Date.EditValue = DateTime.Now;

            this.efwGridControl1.BindControlSet(
               new ColumnControlSet("p_code", txtP_Code)
             , new ColumnControlSet("large_code", cmbLarge_Code)
             , new ColumnControlSet("p_name", txtP_Name)
             , new ColumnControlSet("p_size", txtP_Size)
             , new ColumnControlSet("is_factory", cmbIs_Factory)
             , new ColumnControlSet("last_price", txtLast_Price)
             , new ColumnControlSet("cust_price", txtCust_Price)
             , new ColumnControlSet("agency_idx", cmbAgency_Idx)
             , new ColumnControlSet("min_order_qty", txtMin_Order_Qty)
             , new ColumnControlSet("save_qty", txtSave_Qty)
             , new ColumnControlSet("packing_qty", txtPacking_Qty)
             , new ColumnControlSet("remark", txtRemark)
             , new ColumnControlSet("create_date", dtCreate_Date)
             );
            this.efwGridControl1.Click += efwGridControl1_Click;

            this.efwGridControl2.BindControlSet(
               new ColumnControlSet("s_idx", txtS_IDX)
             , new ColumnControlSet("s_division", cmbS_DIVISION)
             , new ColumnControlSet("s_u_id", txtS_U_ID)
             , new ColumnControlSet("s_id", txtS_ID)
             , new ColumnControlSet("s_nickname", txtS_NICKNAME)
             , new ColumnControlSet("s_password", txtS_PASSWORD)
             , new ColumnControlSet("s_company_name", txtS_COMPANY_NAME)
             , new ColumnControlSet("s_boss_name", txtS_BOSS_NAME)
             , new ColumnControlSet("s_business_num", txtS_BUSINESS_NUM)
             , new ColumnControlSet("s_corporate_num", txtS_CORPORATE_NUM)
             , new ColumnControlSet("s_sales_num", txtS_SALES_NUM)
             , new ColumnControlSet("s_business_condition", txtS_BUSINESS_CONDITION)
             , new ColumnControlSet("s_business_type", txtS_BUSINESS_TYPE)
             , new ColumnControlSet("s_status", cmbS_STATUS)
             , new ColumnControlSet("s_tax_type", cmbS_TAX_TYPE)
             , new ColumnControlSet("s_depositor", txtS_DEPOSITOR)
             , new ColumnControlSet("s_bank", cmbS_BANK)
             , new ColumnControlSet("s_account", txtS_ACCOUNT)
             , new ColumnControlSet("s_respon_name", txtS_RESPON_NAME)
             , new ColumnControlSet("s_tel", txtS_TEL)
             , new ColumnControlSet("s_phone", txtS_PHONE)
             , new ColumnControlSet("s_fax", txtS_FAX)
             , new ColumnControlSet("s_zipcode", btnS_ZIPCODE)
             , new ColumnControlSet("s_address", txtS_ADDRESS)
             );
            this.efwGridControl2.Click += efwGridControl2_Click;


            this.efwGridControl4.BindControlSet(
              new ColumnControlSet("option_id", txtOption_ID)
            , new ColumnControlSet("product_name", txtSet_Name)
            , new ColumnControlSet("option_name", txtSet_Option)
            );


            this.efwGridControl3.BindControlSet(
              new ColumnControlSet("p_code", txtEA_Code)
            , new ColumnControlSet("p_name", txtEA_Name)
            );


            this.efwGridControl5.BindControlSet(
              new ColumnControlSet("option_id", txtOption_ID)
            , new ColumnControlSet("set_name", txtSet_Name)
            , new ColumnControlSet("option_name", txtSet_Option)
            , new ColumnControlSet("ea_code", txtEA_Code)
            , new ColumnControlSet("ea_name", txtEA_Name)
            , new ColumnControlSet("qty", txtQty)
            , new ColumnControlSet("remark", txtSet_Remark)
            );

            txtS_IDX.EditValue = "0";
            txtP_Code.EditValue = "0";
            rbShowType.EditValue = "Y";

            SetCmb();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["p_code"].ToString() != "")
            {
                this.txtP_Code.EditValue = dr["p_code"].ToString();
                this.cmbLarge_Code.EditValue = dr["large_code"].ToString();
                this.cmbAgency_Idx.EditValue = dr["agency_idx"].ToString();
            }
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            btnS_ZIPCODE.EditValue = "";
            if (dr != null && dr["s_zipcode"].ToString() != "")
            {
                this.btnS_ZIPCODE.EditValue2 = dr["s_zipcode"].ToString();
                this.btnS_ZIPCODE.Text = dr["s_zipcode"].ToString();
            }
        }

        private void efwGridControl4_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl4.GetSelectedRow(0);
            if (dr != null && dr["option_id"].ToString() != "")
            {
                this.txtOption_ID.EditValue = dr["option_id"].ToString();
                txtEA_Code.EditValue = "";
                txtEA_Name.EditValue = "";
                txtQty.EditValue = "1";
                txtSet_Remark.EditValue = "";
                Open3();
            }
        }

        private void efwGridControl5_Click(object sender, EventArgs e)
        {

        }

        private void SetCmb()
        {


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00070' and use_yn = 'Y'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbLarge_Code, codeArray);

            }
            cmbLarge_Code.EditValue = "1";

            // 매입거래처
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT idx as DCODE ,company_name as DNAME  FROM domabiz.tb_agency_info ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbAgency_Idx, codeArray);
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00071'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbIs_Factory, codeArray);
            }
            cmbLarge_Code.EditValue = "1";

            // 공급자구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00007' ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbS_DIVISION, codeArray);
            }
            // 상태
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00009' ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbS_STATUS, codeArray);
            }

            // 과세유형
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00008' ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbS_TAX_TYPE, codeArray);
            }

            // 은행
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00006' ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbS_BANK, codeArray);
            }
            cmbS_DIVISION.EditValue = "C";
            cmbS_STATUS.EditValue = "Y";
            cmbS_TAX_TYPE.EditValue = "1";
            cmbS_BANK.EditValue = "1";
        }
        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();
            }
        }
        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

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

        private void Open2()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_agencyname", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
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


        private void Open3()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_set_code", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtOption_ID.EditValue).ToString();


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
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

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            SetCmb();
            base.NewMode();
            Eraser.Clear(this, "CLR1");
            Eraser.Clear(this, "CLR2");
            txtS_IDX.EditValue = "0";
            txtP_Code.EditValue = "0";
            cmbS_DIVISION.EditValue = "C";
            cmbS_STATUS.EditValue = "Y";
            cmbS_TAX_TYPE.EditValue = "1";
            cmbS_BANK.EditValue = "1";
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtP_Name.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "제품명을 입력하세요!");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_p_code", MySqlDbType.Int32));
                            cmd.Parameters["i_p_code"].Value = Convert.ToInt32(txtP_Code.EditValue).ToString();
                            cmd.Parameters["i_p_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_large_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_large_code"].Value = cmbLarge_Code.EditValue;
                            cmd.Parameters["i_large_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_name"].Value = txtP_Name.EditValue; ;
                            cmd.Parameters["i_p_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_size", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_size"].Value = txtP_Size.EditValue;
                            cmd.Parameters["i_p_size"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_factory", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_factory"].Value = cmbIs_Factory.EditValue;
                            cmd.Parameters["i_is_factory"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_last_price", MySqlDbType.Int32));
                            cmd.Parameters["i_last_price"].Value = Convert.ToInt32(txtLast_Price.EditValue).ToString();
                            cmd.Parameters["i_last_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_cust_price", MySqlDbType.Int32));
                            cmd.Parameters["i_cust_price"].Value = Convert.ToInt32(txtCust_Price.EditValue).ToString();
                            cmd.Parameters["i_cust_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_agency_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_agency_idx"].Value = Convert.ToInt32(cmbAgency_Idx.EditValue).ToString();
                            cmd.Parameters["i_agency_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_min_order_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_min_order_qty"].Value = Convert.ToInt32(txtMin_Order_Qty.EditValue).ToString();
                            cmd.Parameters["i_min_order_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_save_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_save_qty"].Value = Convert.ToInt32(txtSave_Qty.EditValue).ToString();
                            cmd.Parameters["i_save_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_packing_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_packing_qty"].Value = Convert.ToInt32(txtPacking_Qty.EditValue).ToString();
                            cmd.Parameters["i_packing_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_p_code", MySqlDbType.Int32));
                            cmd.Parameters["o_p_code"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            txtP_Code.EditValue = cmd.Parameters["o_p_code"].Value.ToString();
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
            }
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtP_Code.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "삭제할 제품을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_p_code", MySqlDbType.Int32));
                            cmd.Parameters["i_p_code"].Value = Convert.ToInt32(txtP_Code.EditValue).ToString();
                            cmd.Parameters["i_p_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
            }
        }

        private void BtnNEW_Click(object sender, EventArgs e)
        {
            NewMode();
        }

        private void btnSAVE_Click(object sender, EventArgs e)
        {


            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_work_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_type"].Value = "A";
                            cmd.Parameters["i_work_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtS_IDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_division", MySqlDbType.VarChar));
                            cmd.Parameters["i_division"].Value = cmbS_DIVISION.EditValue;
                            cmd.Parameters["i_division"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtS_U_ID.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_id"].Value = txtS_ID.EditValue;
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_nickname", MySqlDbType.VarChar));
                            cmd.Parameters["i_nickname"].Value = txtS_NICKNAME.EditValue;
                            cmd.Parameters["i_nickname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_password", MySqlDbType.VarChar));
                            cmd.Parameters["i_password"].Value = txtS_PASSWORD.EditValue;
                            cmd.Parameters["i_password"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_company_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_company_name"].Value = txtS_COMPANY_NAME.EditValue;
                            cmd.Parameters["i_company_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_boss_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_boss_name"].Value = txtS_BOSS_NAME.EditValue;
                            cmd.Parameters["i_boss_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_business_num", MySqlDbType.VarChar));
                            cmd.Parameters["i_business_num"].Value = txtS_BUSINESS_NUM.EditValue;
                            cmd.Parameters["i_business_num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_corporate_num", MySqlDbType.VarChar));
                            cmd.Parameters["i_corporate_num"].Value = txtS_CORPORATE_NUM.EditValue;
                            cmd.Parameters["i_corporate_num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sales_num", MySqlDbType.VarChar));
                            cmd.Parameters["i_sales_num"].Value = txtS_SALES_NUM.EditValue;
                            cmd.Parameters["i_sales_num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_business_condition", MySqlDbType.VarChar));
                            cmd.Parameters["i_business_condition"].Value = txtS_BUSINESS_CONDITION.EditValue;
                            cmd.Parameters["i_business_condition"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_business_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_business_type"].Value = txtS_BUSINESS_TYPE.EditValue;
                            cmd.Parameters["i_business_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_status", MySqlDbType.VarChar));
                            cmd.Parameters["i_status"].Value = cmbS_STATUS.EditValue;
                            cmd.Parameters["i_status"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tax_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_tax_type"].Value = cmbS_TAX_TYPE.EditValue;
                            cmd.Parameters["i_tax_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_depositor", MySqlDbType.VarChar));
                            cmd.Parameters["i_depositor"].Value = txtS_DEPOSITOR.EditValue;
                            cmd.Parameters["i_depositor"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_bank", MySqlDbType.VarChar));
                            cmd.Parameters["i_bank"].Value = cmbS_BANK.EditValue;
                            cmd.Parameters["i_bank"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_account", MySqlDbType.VarChar));
                            cmd.Parameters["i_account"].Value = txtS_ACCOUNT.EditValue;
                            cmd.Parameters["i_account"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_respon_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_respon_name"].Value = txtS_RESPON_NAME.EditValue;
                            cmd.Parameters["i_respon_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel"].Value = txtS_TEL.EditValue;
                            cmd.Parameters["i_tel"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_phone", MySqlDbType.VarChar));
                            cmd.Parameters["i_phone"].Value = txtS_PHONE.EditValue;
                            cmd.Parameters["i_phone"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_fax", MySqlDbType.VarChar));
                            cmd.Parameters["i_fax"].Value = txtS_FAX.EditValue;
                            cmd.Parameters["i_fax"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_zipcode", MySqlDbType.VarChar));
                            cmd.Parameters["i_zipcode"].Value = btnS_ZIPCODE.EditValue;
                            cmd.Parameters["i_zipcode"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_address", MySqlDbType.VarChar));
                            cmd.Parameters["i_address"].Value = txtS_ADDRESS.EditValue;
                            cmd.Parameters["i_address"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            txtS_IDX.EditValue = cmd.Parameters["o_idx"].Value.ToString();


                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();

            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtS_IDX.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 삭제할 거래처명을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_work_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_type"].Value = "D";
                            cmd.Parameters["i_work_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtS_IDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_division", MySqlDbType.VarChar));
                            cmd.Parameters["i_division"].Value = cmbS_DIVISION.EditValue;
                            cmd.Parameters["i_division"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtS_U_ID.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_id"].Value = txtS_ID.EditValue;
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_nickname", MySqlDbType.VarChar));
                            cmd.Parameters["i_nickname"].Value = txtS_NICKNAME.EditValue;
                            cmd.Parameters["i_nickname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_password", MySqlDbType.VarChar));
                            cmd.Parameters["i_password"].Value = txtS_PASSWORD.EditValue;
                            cmd.Parameters["i_password"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_company_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_company_name"].Value = txtS_COMPANY_NAME.EditValue;
                            cmd.Parameters["i_company_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_boss_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_boss_name"].Value = txtS_BOSS_NAME.EditValue;
                            cmd.Parameters["i_boss_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_business_num", MySqlDbType.VarChar));
                            cmd.Parameters["i_business_num"].Value = txtS_BUSINESS_NUM.EditValue;
                            cmd.Parameters["i_business_num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_corporate_num", MySqlDbType.VarChar));
                            cmd.Parameters["i_corporate_num"].Value = txtS_CORPORATE_NUM.EditValue;
                            cmd.Parameters["i_corporate_num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sales_num", MySqlDbType.VarChar));
                            cmd.Parameters["i_sales_num"].Value = txtS_SALES_NUM.EditValue;
                            cmd.Parameters["i_sales_num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_business_condition", MySqlDbType.VarChar));
                            cmd.Parameters["i_business_condition"].Value = txtS_BUSINESS_CONDITION.EditValue;
                            cmd.Parameters["i_business_condition"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_business_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_business_type"].Value = txtS_BUSINESS_TYPE.EditValue;
                            cmd.Parameters["i_business_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_status", MySqlDbType.VarChar));
                            cmd.Parameters["i_status"].Value = cmbS_STATUS.EditValue;
                            cmd.Parameters["i_status"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tax_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_tax_type"].Value = cmbS_TAX_TYPE.EditValue;
                            cmd.Parameters["i_tax_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_depositor", MySqlDbType.VarChar));
                            cmd.Parameters["i_depositor"].Value = txtS_DEPOSITOR.EditValue;
                            cmd.Parameters["i_depositor"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_bank", MySqlDbType.VarChar));
                            cmd.Parameters["i_bank"].Value = cmbS_BANK.EditValue;
                            cmd.Parameters["i_bank"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_account", MySqlDbType.VarChar));
                            cmd.Parameters["i_account"].Value = txtS_ACCOUNT.EditValue;
                            cmd.Parameters["i_account"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_respon_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_respon_name"].Value = txtS_RESPON_NAME.EditValue;
                            cmd.Parameters["i_respon_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel"].Value = txtS_TEL.EditValue;
                            cmd.Parameters["i_tel"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_phone", MySqlDbType.VarChar));
                            cmd.Parameters["i_phone"].Value = txtS_PHONE.EditValue;
                            cmd.Parameters["i_phone"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_fax", MySqlDbType.VarChar));
                            cmd.Parameters["i_fax"].Value = txtS_FAX.EditValue;
                            cmd.Parameters["i_fax"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_zipcode", MySqlDbType.VarChar));
                            cmd.Parameters["i_zipcode"].Value = btnS_ZIPCODE.EditValue;
                            cmd.Parameters["i_zipcode"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_address", MySqlDbType.VarChar));
                            cmd.Parameters["i_address"].Value = txtS_ADDRESS.EditValue;
                            cmd.Parameters["i_address"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            txtS_IDX.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
            }
        }

        private void btnS_ZIPCODE_Click(object sender, EventArgs e)
        {
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = btnS_ZIPCODE, ParentAddr1 = txtS_ADDRESS, ParentAddr2 = txtS_ADDRESS1 };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtProdName.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_ProdName", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtProdName.EditValue;


                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbShowType.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            this.efwGridControl4.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SAVE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_work_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_type"].Value = "A";
                            cmd.Parameters["i_work_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_set_code", MySqlDbType.Int32));
                            cmd.Parameters["i_set_code"].Value = Convert.ToInt32(txtOption_ID.EditValue).ToString();
                            cmd.Parameters["i_set_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_code", MySqlDbType.Int32));
                            cmd.Parameters["i_p_code"].Value = Convert.ToInt32(txtEA_Code.EditValue).ToString();
                            cmd.Parameters["i_p_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_qty"].Value = Convert.ToInt32(txtQty.EditValue).ToString();
                            cmd.Parameters["i_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtSet_Remark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open3();
            }
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SAVE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_work_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_type"].Value = "D";
                            cmd.Parameters["i_work_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_set_code", MySqlDbType.Int32));
                            cmd.Parameters["i_set_code"].Value = Convert.ToInt32(txtOption_ID.EditValue).ToString();
                            cmd.Parameters["i_set_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_code", MySqlDbType.Int32));
                            cmd.Parameters["i_p_code"].Value = Convert.ToInt32(txtEA_Code.EditValue).ToString();
                            cmd.Parameters["i_p_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_qty"].Value = Convert.ToInt32(txtQty.EditValue).ToString();
                            cmd.Parameters["i_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtSet_Remark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open3();
            }
        }

    }
}
