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

namespace YL_GM.BizFrm
{
    public partial class frmGM22 : FrmBase
    {
        frmGM22_Pop01 popup;
        public frmGM22()
        {
            InitializeComponent();
            this.QCode = "GM22";
            //폼명설정
            this.FrmName = "월별원가 비용등록";
        }

        private void frmGM22_Load(object sender, EventArgs e)
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
            rbCustQ.EditValue = "4";
            cmbCompany.EditValue = "01";
            cmbCompanyQ.EditValue = "01";
            rbUnit.EditValue = "1";



            this.rbCustS.Visible = false;
            this.efwLabel1.Text = "일자";
            this.dtS_DATE.Visible = true;
            this.dtE_DATE.Visible = true;
            this.efwLabel2.Visible = true;
            this.dtYearMonth.Visible = false;
            this.btnExcelUpdate.Visible = true;
            this.efwLabel46.Visible = true;
            this.cmbCompanyQ.Visible = true;
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
                    , new ColumnControlSet("tel_use_yn", rbtel_use_yn)
                    , new ColumnControlSet("remark", txtCustRemark)
                      );

            this.efwGridControl6.Click += efwGridControl6_Click;



            tbUse_YN.EditValue = "Y";
            tbUse_YN.EditValue = "Y";

            SetCmb();
            tot();
            tot1();
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
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00059' and use_yn = 'Y'  ";

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
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00056'  ";

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
            cmbMajor_Code.EditValue = "4";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00057' and code_id in ('01','02','03','98') ";

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
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00058' and code_id = '00' ";

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

            cmbSmall_Code.EditValue = "00";
        }

        private void tot()
        {
            gridView3.OptionsView.ShowFooter = true;
            gridView3.Columns["ylland"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["ylland"].SummaryItem.FieldName = "ylland";
            gridView3.Columns["ylland"].SummaryItem.DisplayFormat = "{0:c}";

            gridView3.Columns["telecom"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["telecom"].SummaryItem.FieldName = "telecom";
            gridView3.Columns["telecom"].SummaryItem.DisplayFormat = "{0:c}";

            gridView3.Columns["gnys"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["gnys"].SummaryItem.FieldName = "gnys";
            gridView3.Columns["gnys"].SummaryItem.DisplayFormat = "{0:c}";

            gridView3.Columns["yeoyou_in"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["yeoyou_in"].SummaryItem.FieldName = "yeoyou_in";
            gridView3.Columns["yeoyou_in"].SummaryItem.DisplayFormat = "{0:c}";

            gridView3.Columns["y_communication"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["y_communication"].SummaryItem.FieldName = "y_communication";
            gridView3.Columns["y_communication"].SummaryItem.DisplayFormat = "{0:c}";

            gridView3.Columns["yn_kim"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["yn_kim"].SummaryItem.FieldName = "yn_kim";
            gridView3.Columns["yn_kim"].SummaryItem.DisplayFormat = "{0:c}";

            gridView3.Columns["tot"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["tot"].SummaryItem.FieldName = "tot";
            gridView3.Columns["tot"].SummaryItem.DisplayFormat = "{0:c}";
        }


        private void tot1()
        {
            advBandedGridView1.OptionsView.ShowFooter = true;

            advBandedGridView1.Columns["1_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["1_bef"].SummaryItem.FieldName = "1_bef";
            advBandedGridView1.Columns["1_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["1_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["1_month"].SummaryItem.FieldName = "1_month";
            advBandedGridView1.Columns["1_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["1_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["1_rate"].SummaryItem.FieldName = "1_rate";
            //advBandedGridView1.Columns["1_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["2_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["2_bef"].SummaryItem.FieldName = "2_bef";
            advBandedGridView1.Columns["2_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["2_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["2_month"].SummaryItem.FieldName = "2_month";
            advBandedGridView1.Columns["2_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["2_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["2_rate"].SummaryItem.FieldName = "2_rate";
            //advBandedGridView1.Columns["2_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["3_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["3_bef"].SummaryItem.FieldName = "3_bef";
            advBandedGridView1.Columns["3_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["3_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["3_month"].SummaryItem.FieldName = "3_month";
            advBandedGridView1.Columns["3_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["3_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["3_rate"].SummaryItem.FieldName = "3_rate";
            //advBandedGridView1.Columns["3_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["4_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["4_bef"].SummaryItem.FieldName = "4_bef";
            advBandedGridView1.Columns["4_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["4_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["4_month"].SummaryItem.FieldName = "4_month";
            advBandedGridView1.Columns["4_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["4_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["4_rate"].SummaryItem.FieldName = "4_rate";
            //advBandedGridView1.Columns["4_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["5_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["5_bef"].SummaryItem.FieldName = "5_bef";
            advBandedGridView1.Columns["5_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["5_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["5_month"].SummaryItem.FieldName = "5_month";
            advBandedGridView1.Columns["5_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["5_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["5_rate"].SummaryItem.FieldName = "5_rate";
            //advBandedGridView1.Columns["5_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["6_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["6_bef"].SummaryItem.FieldName = "6_bef";
            advBandedGridView1.Columns["6_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["6_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["6_month"].SummaryItem.FieldName = "6_month";
            advBandedGridView1.Columns["6_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["6_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["6_rate"].SummaryItem.FieldName = "6_rate";
            //advBandedGridView1.Columns["6_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["7_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["7_bef"].SummaryItem.FieldName = "7_bef";
            advBandedGridView1.Columns["7_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["7_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["7_month"].SummaryItem.FieldName = "7_month";
            advBandedGridView1.Columns["7_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["7_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["7_rate"].SummaryItem.FieldName = "7_rate";
            //advBandedGridView1.Columns["7_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["8_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["8_bef"].SummaryItem.FieldName = "8_bef";
            advBandedGridView1.Columns["8_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["8_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["8_month"].SummaryItem.FieldName = "8_month";
            advBandedGridView1.Columns["8_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["8_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["8_rate"].SummaryItem.FieldName = "8_rate";
            //advBandedGridView1.Columns["8_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["9_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["9_bef"].SummaryItem.FieldName = "9_bef";
            advBandedGridView1.Columns["9_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["9_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["9_month"].SummaryItem.FieldName = "9_month";
            advBandedGridView1.Columns["9_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["9_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["9_rate"].SummaryItem.FieldName = "9_rate";
            //advBandedGridView1.Columns["9_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["10_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["10_bef"].SummaryItem.FieldName = "10_bef";
            advBandedGridView1.Columns["10_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["10_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["10_month"].SummaryItem.FieldName = "10_month";
            advBandedGridView1.Columns["10_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["10_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["10_rate"].SummaryItem.FieldName = "10_rate";
            //advBandedGridView1.Columns["10_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["11_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["11_bef"].SummaryItem.FieldName = "11_bef";
            advBandedGridView1.Columns["11_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["11_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["11_month"].SummaryItem.FieldName = "11_month";
            advBandedGridView1.Columns["11_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["11_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["11_rate"].SummaryItem.FieldName = "11_rate";
            //advBandedGridView1.Columns["11_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["12_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["12_bef"].SummaryItem.FieldName = "12_bef";
            advBandedGridView1.Columns["12_bef"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["12_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["12_month"].SummaryItem.FieldName = "12_month";
            advBandedGridView1.Columns["12_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["12_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["12_rate"].SummaryItem.FieldName = "12_rate";
            //advBandedGridView1.Columns["12_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["t_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["t_bef"].SummaryItem.FieldName = "t_bef";
            advBandedGridView1.Columns["t_bef"].SummaryItem.DisplayFormat = "합계: {0:c}";

            advBandedGridView1.Columns["t_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["t_month"].SummaryItem.FieldName = "t_month";
            advBandedGridView1.Columns["t_month"].SummaryItem.DisplayFormat = "{0:c}";

            //advBandedGridView1.Columns["t_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["t_rate"].SummaryItem.FieldName = "t_rate";
            //advBandedGridView1.Columns["t_rate"].SummaryItem.DisplayFormat = "{0}";
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
            if (cmbCompanyQ.EditValue.ToString() == "")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회사구분을 선택하세요!");
                return;
            }
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM22_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM22_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year_month", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_major_code", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = rbCustS.EditValue;

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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM22_SELECT_04", con))
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

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {

            if (MessageAgent.MessageShow(MessageType.Confirm, "원가 항목을 SETTING 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM22_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Year_Month", MySqlDbType.VarChar));
                            cmd.Parameters["i_Year_Month"].Value = dtS_DATE.EditValue3.Substring(0,6);
                            cmd.Parameters["i_Year_Month"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Search();
            }
        }

        private void btnExcelUpdate_Click(object sender, EventArgs e)
        {
            popup = new frmGM22_Pop01();
            popup.ShowDialog();
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {

            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM22_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtAcc_NameQ.EditValue;

                        cmd.Parameters.Add("i_major_code", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = rbCustQ.EditValue;

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
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM22_SAVE_01", con))
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

                            cmd.Parameters.Add(new MySqlParameter("i_in_amt", MySqlDbType.Int32));
                            cmd.Parameters["i_in_amt"].Value = Convert.ToInt32(txtIn_Amt.EditValue).ToString();
                            cmd.Parameters["i_in_amt"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_out_amt", MySqlDbType.Int32));
                            cmd.Parameters["i_out_amt"].Value = Convert.ToInt32(txtOut_Amt.EditValue).ToString();
                            cmd.Parameters["i_out_amt"].Direction = ParameterDirection.Input;

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
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM22_DELETE_01", con))
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

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            txtAcc_Code.EditValue = "";
            txtAcc_Name.EditValue = "";
            txtLarge_Name.EditValue = "";
            txtIn_Amt.EditValue = "0";
            txtOut_Amt.EditValue = "0";
            txtSummary.EditValue = "";
            txtRemark.EditValue = "";
            txtidx.EditValue = "0";
        }

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.efwLabel1.Visible = true;
                this.efwLabel1.Text = "일자";
                this.dtS_DATE.Visible = true;
                this.dtE_DATE.Visible = true;
                this.efwLabel2.Visible = true;
                this.dtYearMonth.Visible = false;
                this.efwLabel46.Visible = true;
                this.cmbCompanyQ.Visible = true;
                this.btnExcelUpdate.Visible = true;
                this.dtYear.Visible = false;
                this.rbUnit.Visible = false;
                this.rbCustS.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                this.efwLabel1.Visible = true;
                this.efwLabel1.Text = "년월";
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                this.efwLabel2.Visible = false;
                this.dtYearMonth.Visible = true;
                this.btnExcelUpdate.Visible = false;
                this.efwLabel46.Visible = false;
                this.cmbCompanyQ.Visible = false;
                this.dtYear.Visible = false;
                this.rbUnit.Visible = false;
                this.rbCustS.Visible = true;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                this.efwLabel1.Visible = true;
                this.efwLabel1.Text = "연도";
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                this.efwLabel2.Visible = false;
                this.dtYearMonth.Visible = false;
                this.dtYear.Visible = true;
                this.rbUnit.Visible = true;
                this.btnExcelUpdate.Visible = false;
                this.efwLabel46.Visible = true;
                this.cmbCompanyQ.Visible = true;
                this.rbCustS.Visible = true;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage10)
            {
                this.efwLabel1.Visible = false;
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                this.efwLabel2.Visible = false;
                this.dtYearMonth.Visible = false;
                this.dtYear.Visible = false;
                this.rbUnit.Visible = false;
                this.btnExcelUpdate.Visible = false;
                this.efwLabel46.Visible = false;
                this.cmbCompanyQ.Visible = false;
                this.rbCustS.Visible = true;
            }
        }

        private void cmbCompany_EditValueChanged(object sender, EventArgs e)
        {

            cmbCompanyQ.EditValue = cmbCompany.EditValue;

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
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SAVE_03", con))
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


                            cmd.Parameters.Add(new MySqlParameter("i_tel_use_yn", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel_use_yn"].Value = rbtel_use_yn.EditValue;
                            cmd.Parameters["i_tel_use_yn"].Direction = ParameterDirection.Input;

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
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_DELETE_02", con))
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SELECT_05", con))
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

        private void cmbMajor_Code_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbMajor_Code.EditValue.ToString() == "4")
            {
                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00057'  and code_memo = '1'  ";

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

                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00058' ";

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

                cmbLarge_Code.EditValue = "01";
                cmbSmall_Code.EditValue = "00";
            }
            else
            {
                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00057' and code_memo = '2'  ";

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

                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00058' and code_id <> '00' ";

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
                cmbLarge_Code.EditValue = "04";
                cmbSmall_Code.EditValue = "01";
            }
        }
    }
}
