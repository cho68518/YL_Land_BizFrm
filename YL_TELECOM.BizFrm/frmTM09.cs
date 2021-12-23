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
using DevExpress.XtraGrid.Views.Grid;

namespace YL_TELECOM.BizFrm
{
    public partial class frmTM09 : FrmBase
    {
        public frmTM09()
        {
            InitializeComponent();
            this.QCode = "TM14";
            //폼명설정
            this.FrmName = "월별원가 비용등록";
        }

        private void frmTM114_Load(object sender, EventArgs e)
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
            rbCustS.EditValue = "4";
            cmbCompany.EditValue = "01";
            rbUnit.EditValue = "2";

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

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["amt"].SummaryItem.FieldName = "amt";
            gridView1.Columns["amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView3.OptionsView.ShowFooter = true;
            DataTable table = new DataTable();
            this.efwGridControl3.DataSource = table;
            (this.efwGridControl3.MainView as GridView).RowCellStyle += gridView3_RowCellStyle;

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
             , new ColumnControlSet("amt", txtAmt)
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
            tot();
            //tot1();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                this.txtAcc_Date.EditValue = dr["acc_date"].ToString();
                this.cmbCompany.EditValue = dr["company"].ToString();
                this.txtAmt.EditValue = dr["amt"].ToString();
            }
        }
        private void efwGridControl6_Click(object sender, EventArgs e)
        {
        }
        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00078' and use_yn = 'Y'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCompany, codeArray);
                CodeAgent.MakeCodeControl(this.cmbCompanyQ, codeArray);
            }
            cmbCompany.EditValue = "01";
            cmbCompanyQ.EditValue = "01";
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00075'  ";

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
            cmbMajor_Code.EditValue = "1";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00076' ";

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
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00077' ";

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

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            txtidx.EditValue = "0";
            txtAmt.EditValue = "0";
            txtSummary.EditValue = "";
            txtRemark.EditValue = "";
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
        private void tot()
        {

            //gridView3.Columns["1_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["1_month"].SummaryItem.FieldName = "1_month";
            //gridView3.Columns["1_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["2_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["2_month"].SummaryItem.FieldName = "2_month";
            //gridView3.Columns["2_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["3_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["3_month"].SummaryItem.FieldName = "3_month";
            //gridView3.Columns["3_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["4_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["4_month"].SummaryItem.FieldName = "4_month";
            //gridView3.Columns["4_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["5_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["5_month"].SummaryItem.FieldName = "5_month";
            //gridView3.Columns["5_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["6_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["6_month"].SummaryItem.FieldName = "6_month";
            //gridView3.Columns["6_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["7_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["7_month"].SummaryItem.FieldName = "7_month";
            //gridView3.Columns["7_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["8_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["8_month"].SummaryItem.FieldName = "8_month";
            //gridView3.Columns["8_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["9_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["9_month"].SummaryItem.FieldName = "9_month";
            //gridView3.Columns["9_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["10_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["10_month"].SummaryItem.FieldName = "10_month";
            //gridView3.Columns["10_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["11_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["11_month"].SummaryItem.FieldName = "11_month";
            //gridView3.Columns["11_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["12_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["12_month"].SummaryItem.FieldName = "12_month";
            //gridView3.Columns["12_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["t_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["t_month"].SummaryItem.FieldName = "t_month";
            //gridView3.Columns["t_month"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView3.Columns["bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView3.Columns["bef"].SummaryItem.FieldName = "bef";
            //gridView3.Columns["bef"].SummaryItem.DisplayFormat = "{0:c}";
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
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage10)
            {
                AccCode();
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM14_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_company", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = cmbCompanyQ.EditValue;

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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM14_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 4);

                        cmd.Parameters.Add("i_company", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = cmbCompanyQ.EditValue;

                        cmd.Parameters.Add("i_Qtype", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = rbCustS.EditValue;

                        cmd.Parameters.Add("i_unit", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = rbUnit.EditValue;

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
        private void Open3()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM14_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 4);

                        cmd.Parameters.Add("i_company", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = cmbCompanyQ.EditValue;

                        cmd.Parameters.Add("i_Qtype", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = rbCustS.EditValue;

                        cmd.Parameters.Add("i_unit", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = rbUnit.EditValue;

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

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM14_SAVE_01", con))
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
                            //cmd.Parameters["i_Small_Code"].Value = cmbSmall_Code.EditValue;
                            cmd.Parameters["i_Small_Code"].Value = "01";
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

        public void AccCode()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM14_SELECT_01", con))
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

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM14_DELETE_01", con))
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

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAcc_Code.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "계정코드를 선택하세요!");
                return;
            }

            if (cmbCompany.EditValue.ToString() == "0")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회사구분을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM14_DELETE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue).ToString();
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
                Search();
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM14_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtAcc_NameQ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
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
            txtAcc_Code.EditValue = "";
            txtAcc_Name.EditValue = "";
            txtLarge_Name.EditValue = "";
            txtAmt.EditValue = "0";
            txtSummary.EditValue = "";
            txtRemark.EditValue = "";
            txtidx.EditValue = "0";
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAcc_Code.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "계정코드를 선택하세요!");
                return;
            }

            if (cmbCompany.EditValue.ToString() == "")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회사구분을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM14_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue).ToString();
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_company"].Value = cmbCompany.EditValue;
                            cmd.Parameters["i_company"].Direction = ParameterDirection.Input;

                            string sAcc_Date = string.Empty;
                            sAcc_Date = txtAcc_Date.EditValue.ToString();
                            sAcc_Date = sAcc_Date.Substring(0, 4) + "-" + sAcc_Date.Substring(5, 2) + "-" + sAcc_Date.Substring(8, 2) + " " + sAcc_Date.Substring(13, 8);

                            cmd.Parameters.Add(new MySqlParameter("i_acc_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_acc_date"].Value = Convert.ToDateTime(sAcc_Date);
                            cmd.Parameters["i_acc_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_acc_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_acc_code"].Value = txtAcc_Code.EditValue;
                            cmd.Parameters["i_acc_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_amt", MySqlDbType.Int32));
                            cmd.Parameters["i_amt"].Value = Convert.ToInt32(txtAmt.EditValue).ToString();
                            cmd.Parameters["i_amt"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_summary", MySqlDbType.VarChar));
                            cmd.Parameters["i_summary"].Value = txtSummary.EditValue;
                            cmd.Parameters["i_summary"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            txtidx.EditValue = cmd.Parameters["o_idx"].Value.ToString();
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

        private void efwGridControl3_Click(object sender, EventArgs e)
        {

        }

        private void gridView3_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null)
                return;

            if (e.RowHandle != view.FocusedRowHandle)
            {
                DevExpress.XtraGrid.Views.Grid.GridView View = sender as GridView;
                string err = View.GetRowCellDisplayText(e.RowHandle, View.Columns["large_name"]);
                if (err == "합계")//Cell의 값이 APPLE인 경우 Cell색 변경
                {
                    e.Appearance.BackColor = Color.Cornsilk;
                    e.Appearance.BackColor2 = Color.Cornsilk; //그라데이션 처리
                }
               else if (err == "총계")
                {
                    {
                        e.Appearance.BackColor = Color.Ivory;
                        e.Appearance.BackColor2 = Color.Ivory; //그라데이션 처리
                    }
                }
            }
        }
    }
}
