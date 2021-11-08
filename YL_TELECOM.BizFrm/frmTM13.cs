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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using YL_TELECOM.BizFrm.Dlg;

namespace YL_TELECOM.BizFrm
{
    public partial class frmTM13 : FrmBase
    {
        public frmTM13()
        {
            InitializeComponent();
            this.QCode = "TM13";
            //폼명설정
            this.FrmName = "월별원가 비용등록";
        }

        private void frmTM13_Load(object sender, EventArgs e)
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


            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            txtAcc_Date.EditValue = DateTime.Now;
            dtYear.EditValue = DateTime.Now;
            rbCustS.EditValue = "7";
            rbCustQ.EditValue = "7";
            cmbCompany.EditValue = "01";
            rbUnit.EditValue = "1";



            this.rbCustS.Visible = false;
            this.efwLabel1.Text = "일자";
            this.dtS_DATE.Visible = true;
            this.dtE_DATE.Visible = true;
            this.efwLabel2.Visible = true;
            this.dtYearMonth.Visible = false;
            this.btnExcelUpdate.Visible = true;
            this.rbUnit.Visible = false;
            this.dtYear.Visible = false;

            dtYearMonth.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtYearMonth.Properties.Mask.EditMask = "yyyy-MM";
            dtYearMonth.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtYearMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtYearMonth.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtYearMonth.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            this.efwGridControl2.BindControlSet(
               new ColumnControlSet("acc_code", txtAcc_Code)
             , new ColumnControlSet("middle_name", txtAcc_Name)
             , new ColumnControlSet("large_name", txtLarge_Name)
             );

            this.efwGridControl1.BindControlSet(
               new ColumnControlSet("acc_code", txtAcc_Code)
             , new ColumnControlSet("acc_code", txtAcc_Code)
             , new ColumnControlSet("middle_name", txtAcc_Name)
             , new ColumnControlSet("acc_date", txtAcc_Date)
             , new ColumnControlSet("large_name", txtLarge_Name)
             , new ColumnControlSet("in_amt", txtIn_Amt)
             , new ColumnControlSet("out_amt", txtOut_Amt)
             , new ColumnControlSet("summary", txtSummary)
             , new ColumnControlSet("remark", txtRemark)
             , new ColumnControlSet("idx", txtidx)
             );
            this.efwGridControl1.Click += efwGridControl1_Click;

            gridView6.OptionsView.ShowFooter = true;
            this.efwGridControl6.BindControlSet(
                      new ColumnControlSet("idx", txtCost_idx)
                    , new ColumnControlSet("major_code", cmbMajor_Code)
                    , new ColumnControlSet("large_code", cmbLarge_Code)
                    , new ColumnControlSet("middle_code", txtMiddle_Code)
                    , new ColumnControlSet("middle_name", txtMiddle_Name)
                    , new ColumnControlSet("small_code", cmbSmall_Code)
                    , new ColumnControlSet("sort", txtSort)
                    , new ColumnControlSet("use_yn", tbUse_YN)
                    , new ColumnControlSet("remark", txtCustRemark)
                      );

            this.efwGridControl6.Click += efwGridControl6_Click;



            tbUse_YN.EditValue = "Y";
            tbUse_YN.EditValue = "Y";

            SetCmb();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                this.txtAcc_Date.EditValue = dr["acc_date"].ToString();
                this.cmbCompany.EditValue = dr["company"].ToString();
            }
        }
        private void efwGridControl6_Click(object sender, EventArgs e)
        {
        }
        private void SetCmb()
        {

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00060'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbMajor_Code, codeArray);
            }
            cmbMajor_Code.EditValue = "7";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00061' ";

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

            cmbLarge_Code.EditValue = "01";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00062' ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbSmall_Code, codeArray);
            }

            cmbSmall_Code.EditValue = "01";
        }

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                //Open1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                //Open2();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                //Open3();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage10)
            {
                AccCode();
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            txtCost_idx.EditValue = "0";
            txtCustRemark.EditValue = "";
            txtMiddle_Code.EditValue = "";
            txtMiddle_Name.EditValue = "";
            txtCustRemark.EditValue = "";
            txtSort.EditValue = "0";
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM13_SAVE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtCost_idx.EditValue).ToString();
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Major_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_Major_Code"].Value = cmbMajor_Code.EditValue;
                            cmd.Parameters["i_Major_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Large_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_Large_Code"].Value = cmbLarge_Code.EditValue;
                            cmd.Parameters["i_Large_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Middle_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_Middle_Code"].Value = txtMiddle_Code.EditValue;
                            cmd.Parameters["i_Middle_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Middle_Name", MySqlDbType.VarChar));
                            cmd.Parameters["i_Middle_Name"].Value = txtMiddle_Name.EditValue;
                            cmd.Parameters["i_Middle_Name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Small_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_Small_Code"].Value = cmbSmall_Code.EditValue;
                            cmd.Parameters["i_Small_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Sort", MySqlDbType.Int32));
                            cmd.Parameters["i_Sort"].Value = Convert.ToInt32(txtSort.EditValue).ToString();
                            cmd.Parameters["i_Sort"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_use_yn", MySqlDbType.VarChar));
                            cmd.Parameters["i_use_yn"].Value = tbUse_YN.EditValue;
                            cmd.Parameters["i_use_yn"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_Remark"].Value = txtCustRemark.EditValue;
                            cmd.Parameters["i_Remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            txtCost_idx.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                AccCode();
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM13_DELETE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtCost_idx.EditValue).ToString();
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

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
                txtCost_idx.EditValue = "0";
                txtCustRemark.EditValue = "";
                txtMiddle_Code.EditValue = "";
                txtMiddle_Name.EditValue = "";
                txtCustRemark.EditValue = "";
                txtSort.EditValue = "0";

                AccCode();

            }
        }

        public void AccCode()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM13_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = rbCustS.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl6.DataBind(ds);
                            this.efwGridControl6.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
    }
}
