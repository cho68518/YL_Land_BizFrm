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
    public partial class frmSCM01 : FrmBase
    {
        public frmSCM01()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "SCM01";
            //폼명설정
            this.FrmName = "판매자 등록";
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;


            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;

            //gridView1.Columns["o_purchase_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["o_purchase_cost"].SummaryItem.FieldName = "o_purchase_cost";
            //gridView1.Columns["o_purchase_cost"].SummaryItem.DisplayFormat = "{0:c}";

            this.efwGridControl1.BindControlSet(
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
            this.efwGridControl1.Click += efwGridControl1_Click;
            SetCmb();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            btnS_ZIPCODE.EditValue = "";
            if (dr != null && dr["s_zipcode"].ToString() != "")
            {
                this.btnS_ZIPCODE.EditValue2 = dr["s_zipcode"].ToString();
                this.btnS_ZIPCODE.Text = dr["s_zipcode"].ToString();
            }
        }
        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");

        }



        private void SetCmb()
        {
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

        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM01_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_agencyname", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQ_AGENCY_NAME.EditValue;
  

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

        private void BtnNEW_Click(object sender, EventArgs e)
        {
            NewMode();
        }

        private void TxtQ_AGENCY_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void BtnSAVE_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtS_COMPANY_NAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 거래처명을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtS_ID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtS_PASSWORD.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 비빌번호를 입력하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM01_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtS_IDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_work_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_type"].Value = "A";
                            cmd.Parameters["i_work_type"].Direction = ParameterDirection.Input;
                            
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


        public override void Delete()
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
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM01_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtS_IDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_work_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_type"].Value = "D";
                            cmd.Parameters["i_work_type"].Direction = ParameterDirection.Input;

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

        private void btnS_ZIPCODE_Click(object sender, EventArgs e)
        {
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = btnS_ZIPCODE, ParentAddr1 = txtS_ADDRESS, ParentAddr2 = txtS_ADDRESS1 };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
            
        }

    }
}

