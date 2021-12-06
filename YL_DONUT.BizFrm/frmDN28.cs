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
namespace YL_DONUT.BizFrm
{
    public partial class frmDN28 : FrmBase
    {
        public frmDN28()
        {
            InitializeComponent();
            this.QCode = "DN28";
            //폼명설정
            this.FrmName = "창고 재고관리";
        }

        private void frmDN28_Load(object sender, EventArgs e)
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
            dtS_DATE.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dtE_DATE.EditValue = DateTime.Now;
            dtInput_Date_tab1.EditValue = DateTime.Now;
            dtOut_Date_tab2.EditValue = DateTime.Now;
            dtInput_Date.EditValue = DateTime.Now;
            dtOut_Date.EditValue = DateTime.Now;
            txtType.EditValue = "입고";
            rbInput_Type_tab1.EditValue = "2";
            rbG_Prod.EditValue = "1";

            dtYearMonth.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtYearMonth.Properties.Mask.EditMask = "yyyy-MM";
            dtYearMonth.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtYearMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtYearMonth.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtYearMonth.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            txtOut_Idx.EditValue = "0";
            txtInput_Idx.EditValue = "0";
            rbInput_Type.EditValue = "2";

            this.efwLabel1.Text = "년월";
            this.dtYearMonth.Visible = true;
            this.dtS_DATE.Visible = false;
            this.dtE_DATE.Visible = false;
            this.rbG_Prod.Visible = false;
            this.efwSimpleButton11.Visible = false;
            cmbIs_FactoryQ.EditValue = "002";
            gridView5.OptionsView.ShowFooter = true;

            bandedGridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
               new ColumnControlSet("p_code", txtInput_P_Code)
             , new ColumnControlSet("p_code", txtOut_P_Code)
             , new ColumnControlSet("p_name", txtInput_P_Name)
             , new ColumnControlSet("p_name", txtOut_P_Name)
             , new ColumnControlSet("type", txtType)
             , new ColumnControlSet("cust_price", txtInput_Price)
           );

            gridView1.OptionsView.ShowFooter = true;
            this.efwGridControl2.BindControlSet(
            new ColumnControlSet("p_code", txtP_Code_tab1)
            , new ColumnControlSet("p_name", txtP_Name_tab1)
            , new ColumnControlSet("last_price", txtPrice_tab1)
            );

            gridView2.OptionsView.ShowFooter = true;
            this.efwGridControl3.BindControlSet(
            new ColumnControlSet("p_code", txtP_Code_tab1)
            , new ColumnControlSet("p_name", txtP_Name_tab1)
            , new ColumnControlSet("idx", txtIdx_tab1)
            , new ColumnControlSet("qty", txtQty_tab1)
            , new ColumnControlSet("price", txtPrice_tab1)
            , new ColumnControlSet("amt", txtAmt_tab1)
            , new ColumnControlSet("remark", txtRemark_tab1)
            );

            gridView3.OptionsView.ShowFooter = true;
            this.efwGridControl4.BindControlSet(
            new ColumnControlSet("p_code", txtP_Code_tab2)
            , new ColumnControlSet("p_name", txtP_Name_tab2)
            //, new ColumnControlSet("idx", txtIdx_tab2)
            //, new ColumnControlSet("is_factory", cmbIs_Factory_tab2)
            //, new ColumnControlSet("out_type", cmbOut_Type_tab2)
            //, new ColumnControlSet("out_date", dtOut_Date_tab2)
            //, new ColumnControlSet("qty", txtQty_tab2)
            //, new ColumnControlSet("remark", txtRemark_tab2)
            );

            gridView4.OptionsView.ShowFooter = true;

            this.efwGridControl5.BindControlSet(
            new ColumnControlSet("p_code", txtP_Code_tab2)
            , new ColumnControlSet("p_name", txtP_Name_tab2)
            , new ColumnControlSet("idx", txtIdx_tab2)
            , new ColumnControlSet("qty", txtQty_tab2)
            , new ColumnControlSet("out_type", cmbOut_Type_tab2)
            , new ColumnControlSet("remark", txtRemark_tab2)
            );

            gridView4.OptionsView.ShowFooter = true;

            this.efwGridControl7.BindControlSet(
            new ColumnControlSet("p_code", txtP_CodeQ)
            );

            bandedGridView2.OptionsView.ShowFooter = true;
            bandedGridView2.Columns["bef_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["bef_qty"].SummaryItem.FieldName = "bef_qty";
            bandedGridView2.Columns["bef_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView2.Columns["bef_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["bef_amt"].SummaryItem.FieldName = "bef_amt";
            bandedGridView2.Columns["bef_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView2.Columns["good_in_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["good_in_qty"].SummaryItem.FieldName = "good_in_qty";
            bandedGridView2.Columns["good_in_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView2.Columns["prod_in_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["prod_in_qty"].SummaryItem.FieldName = "prod_in_qty";
            bandedGridView2.Columns["prod_in_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView2.Columns["in_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["in_amt"].SummaryItem.FieldName = "in_amt";
            bandedGridView2.Columns["in_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView2.Columns["prod_out_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["prod_out_qty"].SummaryItem.FieldName = "prod_out_qty";
            bandedGridView2.Columns["prod_out_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView2.Columns["slae_out_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["slae_out_qty"].SummaryItem.FieldName = "slae_out_qty";
            bandedGridView2.Columns["slae_out_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView2.Columns["promotion_out_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["promotion_out_qty"].SummaryItem.FieldName = "promotion_out_qty";
            bandedGridView2.Columns["promotion_out_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView2.Columns["back_out_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["back_out_qty"].SummaryItem.FieldName = "back_out_qty";
            bandedGridView2.Columns["back_out_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView2.Columns["out_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["out_amt"].SummaryItem.FieldName = "out_amt";
            bandedGridView2.Columns["out_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView2.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            bandedGridView2.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView2.Columns["stock_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView2.Columns["stock_amt"].SummaryItem.FieldName = "stock_amt";
            bandedGridView2.Columns["stock_amt"].SummaryItem.DisplayFormat = "{0:c}";

            // 통합수불

            gridView9.OptionsView.ShowFooter = true;
            gridView9.Columns["bef_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView9.Columns["bef_qty"].SummaryItem.FieldName = "bef_qty";
            gridView9.Columns["bef_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView9.Columns["bef_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView9.Columns["bef_amt"].SummaryItem.FieldName = "bef_amt";
            gridView9.Columns["bef_amt"].SummaryItem.DisplayFormat = "{0:c}";

         
            gridView9.Columns["in_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView9.Columns["in_qty"].SummaryItem.FieldName = "in_qty";
            gridView9.Columns["in_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView9.Columns["in_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView9.Columns["in_amt"].SummaryItem.FieldName = "in_amt";
            gridView9.Columns["in_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView9.Columns["out_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView9.Columns["out_qty"].SummaryItem.FieldName = "out_qty";
            gridView9.Columns["out_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView9.Columns["out_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView9.Columns["out_amt"].SummaryItem.FieldName = "out_amt";
            gridView9.Columns["out_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView9.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView9.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView9.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView9.Columns["stock_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView9.Columns["stock_amt"].SummaryItem.FieldName = "stock_amt";
            gridView9.Columns["stock_amt"].SummaryItem.DisplayFormat = "{0:c}";


            SetCmb();
        }

        private void SetCmb()
        {


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00072' and use_yn = 'Y'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbIs_FactoryQ, codeArray);
                CodeAgent.MakeCodeControl(this.cmbInput_Factory, codeArray);
                CodeAgent.MakeCodeControl(this.cmbOut_Factory, codeArray);
                CodeAgent.MakeCodeControl(this.cmbIs_Factory_tab1, codeArray);
                CodeAgent.MakeCodeControl(this.cmbIs_Factory_tab2, codeArray);
            }
            cmbIs_FactoryQ.EditValue = "002";
            cmbInput_Factory.EditValue = "002";
            cmbOut_Factory.EditValue = "002";
            cmbIs_Factory_tab1.EditValue = "001";
            cmbIs_Factory_tab2.EditValue = "001";
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00074' and use_yn = 'Y' and code_id in ('1','3') ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbOut_Type, codeArray); 

            }
            cmbOut_Type.EditValue = "1";


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00074' and use_yn = 'Y'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbOut_Type_tab2, codeArray);
            }
            cmbOut_Type_tab2.EditValue = "1";

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
            cmbAgency_Idx.EditValue = "5";
            
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT concat(idx) as DCODE ,company_name as DNAME  FROM domabiz.tb_agency_info ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                 CodeAgent.MakeCodeControl(this.cmbAgency_tab1, codeArray);
            }
            cmbAgency_tab1.EditValue = "1";

        }

        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl3.GetSelectedRow(0);
            if (dr != null && dr["agency_idx"].ToString() != "")
            {
                this.cmbAgency_tab1.EditValue = Convert.ToChar(dr["agency_idx"]).ToString();
                this.dtInput_Date_tab1.EditValue = dr["input_date"].ToString();
                this.cmbIs_Factory_tab1.EditValue = dr["is_factory"].ToString();
                this.rbInput_Type_tab1.EditValue = dr["input_type"].ToString();
            }
        }

        private void efwGridControl5_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl5.GetSelectedRow(0);
            if (dr != null && dr["out_date"].ToString() != "")
            {
                this.dtOut_Date_tab2.EditValue = dr["out_date"].ToString();
                this.cmbIs_Factory_tab2.EditValue = dr["is_factory"].ToString();
            }
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["p_code"].ToString() != "")
            {
                txtIdx_tab1.EditValue = "0";
                txtRemark_tab1.EditValue = "";
                txtQty_tab1.EditValue = "0";
                txtPrice_tab1.EditValue = "0";
                txtAmt_tab1.EditValue = "0";
                cmbAgency_tab1.EditValue = "1";
            }
        }
        private void efwGridControl4_Click(object sender, EventArgs e)
        {
            txtIdx_tab2.EditValue = "0";
            txtRemark_tab2.EditValue = "";
            txtQty_tab2.EditValue = "0";
        }
        private void efwGridControl7_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl7.GetSelectedRow(0);
            if (dr != null && dr["p_code"].ToString() != "")
            {
                this.txtP_CodeQ.EditValue = dr["p_code"].ToString();
              Open7();
              Open8();
            }
        }

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage7)
            {
                Open10();
            }
            else if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
                
            }
            else if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage4)
            {
                Open4();
                Open9();
            }
            else if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage6)
            {
                Open5();
            }
            else if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage5)
            {
                Open6();
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0,6);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar,3);
                        cmd.Parameters[1].Value = cmbIs_FactoryQ.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.bandedGridView1.BestFitColumns();
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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 3);
                        cmd.Parameters[2].Value = cmbIs_FactoryQ.EditValue;

                        cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtP_NameQ.EditValue;

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
        private void Open4()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_06", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 3);
                        cmd.Parameters[2].Value = cmbIs_FactoryQ.EditValue;

                        cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtP_NameQ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
                            //this.efwGridControl5.MyGridView.BestFitColumns();
                            //gridView4.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open5()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_07", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 3);
                        cmd.Parameters[1].Value = cmbIs_FactoryQ.EditValue;

                        cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtProdName.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl6.DataBind(ds);
                         //   this.efwGridControl6.MyGridView.BestFitColumns();
                         //   gridView1.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open6()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_08", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 3);
                        cmd.Parameters[1].Value = cmbIs_FactoryQ.EditValue;

                        cmd.Parameters.Add("i_price_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = rbG_Prod.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl7.DataBind(ds);
                            //   this.efwGridControl6.MyGridView.BestFitColumns();
                            //   gridView1.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open7()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_09", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 3);
                        cmd.Parameters[1].Value = cmbIs_FactoryQ.EditValue;

                        cmd.Parameters.Add("i_p_code", MySqlDbType.Int32);
                        cmd.Parameters[2].Value = Convert.ToInt32(txtP_CodeQ.EditValue).ToString();

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl8.DataBind(ds);
                            //   this.efwGridControl6.MyGridView.BestFitColumns();
                            //   gridView1.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open8()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_10", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 3);
                        cmd.Parameters[1].Value = cmbIs_FactoryQ.EditValue;

                        cmd.Parameters.Add("i_p_code", MySqlDbType.Int32);
                        cmd.Parameters[2].Value = Convert.ToInt32(txtP_CodeQ.EditValue).ToString();

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl9.DataBind(ds);
                            //   this.efwGridControl6.MyGridView.BestFitColumns();
                            //   gridView1.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        private void Open9()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_11", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 3);
                        cmd.Parameters[2].Value = cmbIs_FactoryQ.EditValue;

                        cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtP_NameQ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl10.DataBind(ds);
                            this.efwGridControl10.MyGridView.BestFitColumns();
                            gridView6.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open10()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_12", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 6);
                        
                        cmd.Parameters.Add("i_price_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbG_Prod.EditValue;

                        cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtProdName.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl11.DataBind(ds);
                            this.efwGridControl11.MyGridView.BestFitColumns();
                            gridView9.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        #region
        private void bandedGridView1_Click(object sender, EventArgs e)
        {
            txtInput_Qty.EditValue = "0";
            txtInput_Price.EditValue = "0";
            txtInput_Amt.EditValue = "0";
            txtOut_Qty.EditValue = "0";

            var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            var rowIndex = gv.FocusedRowHandle;
            var columnIndex = gv.FocusedColumn.VisibleIndex;
            if (columnIndex == 4 ^ columnIndex == 5)
                txtChk.EditValue = "01";
            else if (columnIndex == 6 ^ columnIndex == 7)
                txtChk.EditValue = "02";
            else if (columnIndex == 8 ^ columnIndex == 9)
                txtChk.EditValue = "03";
            else if (columnIndex == 10 ^ columnIndex == 11)
                txtChk.EditValue = "04";
            else if (columnIndex == 12 ^ columnIndex == 13)
                txtChk.EditValue = "05";
            else if (columnIndex == 14 ^ columnIndex == 15)
                txtChk.EditValue = "06";
            else if (columnIndex == 16 ^ columnIndex == 17)
                txtChk.EditValue = "07";
            else if (columnIndex == 18 ^ columnIndex == 19)
                txtChk.EditValue = "08";
            else if (columnIndex == 20 ^ columnIndex == 21)
                txtChk.EditValue = "09";
            else if (columnIndex == 22 ^ columnIndex == 23)
                txtChk.EditValue = "10";
            else if (columnIndex == 24 ^ columnIndex == 25)
                txtChk.EditValue = "11";
            else if (columnIndex == 26 ^ columnIndex == 27)
                txtChk.EditValue = "12";
            else if (columnIndex == 28 ^ columnIndex == 29)
                txtChk.EditValue = "13";
            else if (columnIndex == 30 ^ columnIndex == 31)
                txtChk.EditValue = "14";
            else if (columnIndex == 32 ^ columnIndex == 33)
                txtChk.EditValue = "15";
            else if (columnIndex == 34 ^ columnIndex == 35)
                txtChk.EditValue = "16";
            else if (columnIndex == 36 ^ columnIndex == 37)
                txtChk.EditValue = "17";
            else if (columnIndex == 38 ^ columnIndex == 39)
                txtChk.EditValue = "18";
            else if (columnIndex == 40 ^ columnIndex == 41)
                txtChk.EditValue = "19";
            else if (columnIndex == 42 ^ columnIndex == 43)
                txtChk.EditValue = "20";
            else if (columnIndex == 44 ^ columnIndex == 45)
                txtChk.EditValue = "21";
            else if (columnIndex == 46 ^ columnIndex == 47)
                txtChk.EditValue = "22";
            else if (columnIndex == 48 ^ columnIndex == 49)
                txtChk.EditValue = "23";
            else if (columnIndex == 50 ^ columnIndex == 51)
                txtChk.EditValue = "24";
            else if (columnIndex == 52 ^ columnIndex == 53)
                txtChk.EditValue = "25";
            else if (columnIndex == 54 ^ columnIndex == 55)
                txtChk.EditValue = "26";
            else if (columnIndex == 56 ^ columnIndex == 57)
                txtChk.EditValue = "27";
            else if (columnIndex == 58 ^ columnIndex == 59)
                txtChk.EditValue = "28";
            else if (columnIndex == 60 ^ columnIndex == 61)
                txtChk.EditValue = "29";
            else if (columnIndex == 62 ^ columnIndex == 63)
                txtChk.EditValue = "30";
            else if (columnIndex == 64 ^ columnIndex == 65)
                txtChk.EditValue = "31";
            if (columnIndex > 3)
            {
                dtInput_Date.EditValue = dtYearMonth.EditValue3.Substring(0, 4) + "-" + dtYearMonth.EditValue3.Substring(4, 2) + "-" + txtChk.EditValue;
                dtOut_Date.EditValue = dtYearMonth.EditValue3.Substring(0, 4) + "-" + dtYearMonth.EditValue3.Substring(4, 2) + "-" + txtChk.EditValue;
            }

            if (columnIndex == 4  ^ columnIndex == 6  ^ columnIndex == 8  ^ columnIndex == 10 ^ columnIndex == 12 ^ columnIndex == 14 ^ columnIndex == 16 ^ columnIndex == 18 ^ columnIndex == 20 ^
                columnIndex == 22 ^ columnIndex == 24 ^ columnIndex == 26 ^ columnIndex == 28 ^ columnIndex == 30 ^ columnIndex == 32 ^ columnIndex == 34 ^ columnIndex == 36 ^ columnIndex == 38 ^
                columnIndex == 40 ^ columnIndex == 42 ^ columnIndex == 44 ^ columnIndex == 46 ^ columnIndex == 48 ^ columnIndex == 50 ^ columnIndex == 52 ^ columnIndex == 54 ^ columnIndex == 56 ^
                columnIndex == 58 ^ columnIndex == 60 ^ columnIndex == 62 ^ columnIndex == 64 )
                cmbOut_Type.EditValue = "1";
            else if (columnIndex == 5 ^ columnIndex == 7 ^ columnIndex == 9 ^ columnIndex == 11 ^ columnIndex == 13 ^ columnIndex == 15 ^ columnIndex == 17 ^ columnIndex == 19 ^ columnIndex == 21 ^
                     columnIndex == 23 ^ columnIndex == 25 ^ columnIndex == 27 ^ columnIndex == 29^ columnIndex == 31 ^ columnIndex == 33 ^ columnIndex == 35 ^ columnIndex == 37 ^ columnIndex == 29 ^
                     columnIndex == 41 ^ columnIndex == 43 ^ columnIndex == 45 ^ columnIndex == 47 ^ columnIndex == 49 ^ columnIndex == 51 ^ columnIndex == 53 ^ columnIndex == 55 ^ columnIndex == 57 ^
                     columnIndex == 59 ^ columnIndex == 61 ^ columnIndex == 63 ^ columnIndex == 65)
                cmbOut_Type.EditValue = "3";
            //if (txtType.EditValue.ToString() == "입고")
            //{
            //    this.btnIn_New.Enabled = true;
            //    this.btnOut_New.Enabled = false;
            //    this.btnIn_Save.Enabled = true;
            //    this.btnOut_Save.Enabled = false;
            //    this.btnIn_Delete.Enabled = true;
            //    this.btnOut_Delete.Enabled = false;
            //}
            //else
            //{
            //    this.btnIn_New.Enabled = false;
            //    this.btnOut_New.Enabled = true;
            //    this.btnIn_Save.Enabled = false;
            //    this.btnOut_Save.Enabled = true;
            //    this.btnIn_Delete.Enabled = false;
            //    this.btnOut_Delete.Enabled = true;
            //}
            Open2();
        }
        #endregion

        private void Open2()
        {
            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    if (txtType.EditValue.ToString() == "입고")
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new MySqlParameter("i_p_code", MySqlDbType.Int32));
                            cmd.Parameters["i_p_code"].Value = Convert.ToInt32(txtInput_P_Code.EditValue);
                            cmd.Parameters["i_p_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_input_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_input_date"].Value = dtInput_Date.EditValue;
                            cmd.Parameters["i_input_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_factory", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_factory"].Value = cmbInput_Factory.EditValue;
                            cmd.Parameters["i_is_factory"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_qty", MySqlDbType.Int32));
                            cmd.Parameters["o_qty"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_price", MySqlDbType.Int32));
                            cmd.Parameters["o_price"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_amt", MySqlDbType.Int32));
                            cmd.Parameters["o_amt"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_agency_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_agency_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_input_type", MySqlDbType.VarChar));
                            cmd.Parameters["o_input_type"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_remark", MySqlDbType.VarChar));
                            cmd.Parameters["o_remark"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            string sQty = cmd.Parameters["o_qty"].Value.ToString();
                            if (sQty == "" ^ sQty is null)
                                sQty = "0";
                            else
                                sQty = cmd.Parameters["o_qty"].Value.ToString();
                            txtInput_Qty.EditValue = sQty;

                            string sPrice = cmd.Parameters["o_price"].Value.ToString();
                            if (sPrice == "" ^ sPrice is null)
                                sPrice = "0";
                            else
                                sPrice = cmd.Parameters["o_price"].Value.ToString();
                            txtInput_Price.EditValue = sPrice;

                            string sAmt = cmd.Parameters["o_amt"].Value.ToString();
                            if (sAmt == "" ^ sAmt is null)
                                sAmt = "0";
                            else
                                sPrice = cmd.Parameters["o_amt"].Value.ToString();
                            txtInput_Amt.EditValue = sAmt;

                            cmbAgency_Idx.EditValue = cmd.Parameters["o_agency_idx"].Value.ToString();
                            if (cmbAgency_Idx.EditValue.ToString() == "" ^ cmbAgency_Idx.EditValue.ToString() == null)
                                cmbAgency_Idx.EditValue = "5";

                            rbInput_Type.EditValue = cmd.Parameters["o_input_type"].Value.ToString();
                            if (rbInput_Type.EditValue.ToString() == "" ^ rbInput_Type.EditValue.ToString() == null)
                                rbInput_Type.EditValue = "2";

                            txtRemark.EditValue = cmd.Parameters["o_remark"].Value.ToString();
                        }
                    }
                    else
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new MySqlParameter("i_p_code", MySqlDbType.Int32));
                            cmd.Parameters["i_p_code"].Value = Convert.ToInt32(txtInput_P_Code.EditValue);
                            cmd.Parameters["i_p_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_out_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_out_date"].Value = dtOut_Date.EditValue;
                            cmd.Parameters["i_out_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_factory", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_factory"].Value = cmbInput_Factory.EditValue;
                            cmd.Parameters["i_is_factory"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_out_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_out_type"].Value = cmbOut_Type.EditValue;
                            cmd.Parameters["i_out_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_qty", MySqlDbType.Int32));
                            cmd.Parameters["o_qty"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_remark", MySqlDbType.VarChar));
                            cmd.Parameters["o_remark"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            txtOut_Qty.EditValue = cmd.Parameters["o_qty"].Value.ToString();
                            txtOut_Remark.EditValue = cmd.Parameters["o_remark"].Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            //Open1();
        }


        private void btnIn_New_Click(object sender, EventArgs e)
        {
            rbInput_Type.EditValue = "2";
            txtInput_P_Name.EditValue = "";
            cmbInput_Factory.EditValue = "002";
            txtInput_P_Name.EditValue = "";
            txtInput_Qty.EditValue = "0";
            txtInput_Price.EditValue = "0";
            txtInput_Amt.EditValue = "0";
            txtInput_P_Code.EditValue = "";
            txtInput_Idx.EditValue = "0";

        }

        private void btnOut_New_Click(object sender, EventArgs e)
        {
            cmbOut_Type.EditValue = "1";
            txtOut_P_Name.EditValue = "";
            cmbOut_Factory.EditValue = "002";
            txtOut_P_Name.EditValue = "";
            txtOut_Qty.EditValue = "0";
            txtOut_P_Code.EditValue = "";
            txtOut_Idx.EditValue = "0";

        }

        private void btnIn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInput_P_Code.EditValue.ToString() == "" ^ txtInput_P_Code.EditValue.ToString() == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, "입고 등록 제품을 선택하세요!");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SAVE_01", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_input_date", MySqlDbType.DateTime);
                        cmd.Parameters[0].Value = dtInput_Date.EditValue;

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbInput_Factory.EditValue;

                        cmd.Parameters.Add("i_input_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = rbInput_Type.EditValue;

                        cmd.Parameters.Add("i_Agency_Idx", MySqlDbType.Int32);
                        cmd.Parameters[3].Value = Convert.ToInt32(cmbAgency_Idx.EditValue).ToString();

                        cmd.Parameters.Add("i_Input_p_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtInput_P_Code.EditValue;

                        string sQty;
                        if  (txtInput_Qty.EditValue.ToString() == "" ^ txtInput_Qty.EditValue.ToString() == null)
                              sQty = "0";
                        else
                            sQty = Convert.ToInt32(txtInput_Qty.EditValue).ToString();
                        cmd.Parameters.Add("i_qty", MySqlDbType.Int32, 11);
                        cmd.Parameters[5].Value = Convert.ToInt32(sQty).ToString();

                        string sPrice;
                        if (txtInput_Price.EditValue.ToString() == "" ^ txtInput_Price.EditValue.ToString() == null)
                            sPrice = "0";
                        else
                            sPrice = Convert.ToInt32(txtInput_Price.EditValue).ToString();
                        cmd.Parameters.Add("i_price", MySqlDbType.Int32, 11);
                        cmd.Parameters[6].Value = Convert.ToInt32(sPrice).ToString();

                        string sAmt;
                        if (txtInput_Amt.EditValue.ToString() == "" ^ txtInput_Amt.EditValue.ToString() == null)
                            sAmt = "0";
                        else
                            sAmt = Convert.ToInt32(txtInput_Amt.EditValue).ToString();
                        cmd.Parameters.Add("i_amt", MySqlDbType.Int32, 11);
                        cmd.Parameters[7].Value = Convert.ToInt32(sAmt).ToString();

                        cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 300);
                        cmd.Parameters[8].Value = txtRemark.EditValue;

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Search();
        }

        private void btnOut_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOut_P_Code.EditValue.ToString() == "" ^ txtOut_P_Code.EditValue.ToString() == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, "출고 등록 제품을 선택하세요!");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SAVE_02", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_out_date", MySqlDbType.DateTime);
                        cmd.Parameters[0].Value = dtOut_Date.EditValue;

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbOut_Factory.EditValue;

                        cmd.Parameters.Add("i_out_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbOut_Type.EditValue;

                        cmd.Parameters.Add("i_out_p_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtOut_P_Code.EditValue;

                        string sQty;
                        if (txtOut_Qty.EditValue.ToString() == "" ^ txtOut_Qty.EditValue.ToString() == null)
                            sQty = "0";
                        else
                            sQty = Convert.ToInt32(txtOut_Qty.EditValue).ToString();
                        cmd.Parameters.Add("i_qty", MySqlDbType.Int32, 11);
                        cmd.Parameters[4].Value = Convert.ToInt32(sQty).ToString();

                        cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 300);
                        cmd.Parameters[5].Value = txtOut_Remark.EditValue;

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Search();
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtProdName.EditValue;

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbIs_FactoryQ.EditValue;

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

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtP_Name_tab1.EditValue.ToString() == "" ^ txtP_Name_tab1.EditValue.ToString() == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, "입고 등록 제품을 선택하세요!");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SAVE_03", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_idx", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtIdx_tab1.EditValue).ToString();

                        cmd.Parameters.Add("i_input_date", MySqlDbType.DateTime);
                        cmd.Parameters[1].Value = Convert.ToDateTime(dtInput_Date_tab1.EditValue);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbIs_Factory_tab1.EditValue;

                        cmd.Parameters.Add("i_input_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = rbInput_Type_tab1.EditValue;

                        cmd.Parameters.Add("i_Agency_Idx", MySqlDbType.Int32);
                        cmd.Parameters[4].Value = Convert.ToInt32(cmbAgency_tab1.EditValue).ToString();

                        cmd.Parameters.Add("i_Input_p_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[5].Value = Convert.ToInt32(txtP_Code_tab1.EditValue).ToString();

                        string sQty;
                        if (txtQty_tab1.EditValue.ToString() == "" ^ txtQty_tab1.EditValue.ToString() == null)
                            sQty = "0";
                        else
                            sQty = Convert.ToInt32(txtQty_tab1.EditValue).ToString();
                        cmd.Parameters.Add("i_qty", MySqlDbType.Int32, 11);
                        cmd.Parameters[6].Value = Convert.ToInt32(sQty).ToString();

                        string sPrice;
                        if (txtPrice_tab1.EditValue.ToString() == "" ^ txtPrice_tab1.EditValue.ToString() == null)
                            sPrice = "0";
                        else
                            sPrice = Convert.ToInt32(txtPrice_tab1.EditValue).ToString();
                        cmd.Parameters.Add("i_price", MySqlDbType.Int32, 11);
                        cmd.Parameters[7].Value = Convert.ToInt32(sPrice).ToString();

                        string sAmt;
                        if (txtAmt_tab1.EditValue.ToString() == "" ^ txtAmt_tab1.EditValue.ToString() == null)
                            sAmt = "0";
                        else
                            sAmt = Convert.ToInt32(txtAmt_tab1.EditValue).ToString();
                        cmd.Parameters.Add("i_amt", MySqlDbType.Int32, 11);
                        cmd.Parameters[8].Value = Convert.ToInt32(sAmt).ToString();

                        cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 300);
                        cmd.Parameters[9].Value = txtRemark_tab1.EditValue;

                        cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                        cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        txtIdx_tab1.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                        con.Close();
                    }
                    Search();

                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            txtIdx_tab1.EditValue = "0";
            txtRemark_tab1.EditValue = "";
            txtQty_tab1.EditValue = "0";
            txtPrice_tab1.EditValue = "0";
            txtAmt_tab1.EditValue = "0";
            cmbAgency_tab1.EditValue = "1";
            dtInput_Date_tab1.EditValue = DateTime.Now;
        }



        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtProdName.EditValue;

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbIs_FactoryQ.EditValue;

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
            txtIdx_tab2.EditValue = "0";
            txtRemark_tab2.EditValue = "";
            txtQty_tab2.EditValue = "0";
            dtOut_Date_tab2.EditValue = DateTime.Now;
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtP_Name_tab2.EditValue.ToString() == "" ^ txtP_Name_tab2.EditValue.ToString() == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, "출고 등록 제품을 선택하세요!");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SAVE_04", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_idx", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtIdx_tab2.EditValue).ToString();

                        cmd.Parameters.Add("i_out_date", MySqlDbType.DateTime);
                        cmd.Parameters[1].Value = Convert.ToDateTime(dtOut_Date_tab2.EditValue);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbIs_Factory_tab2.EditValue;

                        cmd.Parameters.Add("i_out_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = cmbOut_Type_tab2.EditValue;

                        cmd.Parameters.Add("i_out_p_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtP_Code_tab2.EditValue;

                        string sQty;
                        if (txtQty_tab2.EditValue.ToString() == "" ^ txtQty_tab2.EditValue.ToString() == null)
                            sQty = "0";
                        else
                            sQty = Convert.ToInt32(txtQty_tab2.EditValue).ToString();
                        cmd.Parameters.Add("i_qty", MySqlDbType.Int32, 11);
                        cmd.Parameters[5].Value = Convert.ToInt32(sQty).ToString();

                        cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 300);
                        cmd.Parameters[6].Value = txtRemark_tab2.EditValue;

                        cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                        cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        txtIdx_tab2.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Search();
        }

        private void btnIn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInput_Qty.EditValue.ToString() == "0" ^ txtInput_Qty.EditValue.ToString() == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, "삭제할 제품을 선택하세요!");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_DELETE_01", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_input_date", MySqlDbType.DateTime);
                        cmd.Parameters[0].Value = dtInput_Date.EditValue;

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbInput_Factory.EditValue;

                        cmd.Parameters.Add("i_Input_p_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtInput_P_Code.EditValue;

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Search();
        }

        private void btnOut_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOut_Qty.EditValue.ToString() == "0" ^ txtOut_Qty.EditValue.ToString() == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, "삭제할 제품을 선택하세요!");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_DELETE_02", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_out_date", MySqlDbType.DateTime);
                        cmd.Parameters[0].Value = dtOut_Date.EditValue;

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbOut_Factory.EditValue;

                        cmd.Parameters.Add("i_out_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbOut_Type.EditValue;

                        cmd.Parameters.Add("i_out_p_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtOut_P_Code.EditValue;

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Search();
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIdx_tab1.EditValue.ToString() == "0" ^ txtIdx_tab1.EditValue.ToString() == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, "삭제할 입고번호를 선택하세요!");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_DELETE_03", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_idx", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtIdx_tab1.EditValue).ToString();

                        cmd.Parameters.Add("i_input_date", MySqlDbType.DateTime);
                        cmd.Parameters[1].Value = Convert.ToDateTime(dtInput_Date_tab1.EditValue);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbIs_Factory_tab1.EditValue;

                        cmd.Parameters.Add("i_Input_p_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = Convert.ToInt32(txtP_Code_tab1.EditValue).ToString();

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "삭제 되었습니다.");
            txtIdx_tab1.EditValue = "0";
            txtRemark_tab1.EditValue = "";
            txtQty_tab1.EditValue = "0";
            txtPrice_tab1.EditValue = "0";
            txtAmt_tab1.EditValue = "0";
            cmbAgency_tab1.EditValue = "1";
            txtP_Name_tab1.EditValue = "";
            dtInput_Date_tab1.EditValue = DateTime.Now;
            Search();
        }
        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIdx_tab2.EditValue.ToString() == "0" ^ txtIdx_tab2.EditValue.ToString() == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, "삭제할 출고번호를 선택하세요!");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_DELETE_04", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_idx", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtIdx_tab2.EditValue).ToString();

                        cmd.Parameters.Add("i_out_date", MySqlDbType.DateTime);
                        cmd.Parameters[1].Value = Convert.ToDateTime(dtOut_Date_tab2.EditValue);

                        cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbIs_Factory_tab2.EditValue;

                        cmd.Parameters.Add("i_out_p_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtP_Code_tab2.EditValue;


                        cmd.ExecuteNonQuery();
                        con.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            txtIdx_tab2.EditValue = "0";
            txtRemark_tab2.EditValue = "";
            txtQty_tab2.EditValue = "0";
            txtP_Name_tab2.EditValue = "";
            dtOut_Date_tab2.EditValue = DateTime.Now;
            Search();
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {

        }
        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.efwLabel1.Text = "년월";
                this.dtYearMonth.Visible = true;
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                cmbIs_FactoryQ.EditValue = "002";
                this.efwSimpleButton11.Visible = false;
                this.rbG_Prod.Visible = false;
                this.cmbIs_FactoryQ.Visible = true;
                this.efwLabel46.Visible = true;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                this.efwLabel1.Text = "일자";
                this.dtYearMonth.Visible = false;
                this.dtS_DATE.Visible = true;
                this.dtE_DATE.Visible = true;
                cmbIs_FactoryQ.EditValue = "001";
                this.efwSimpleButton11.Visible = false;
                this.rbG_Prod.Visible = false;
                this.cmbIs_FactoryQ.Visible = true;
                this.efwLabel46.Visible = true;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage7)
            {
                this.efwLabel1.Text = "년월";
                this.dtYearMonth.Visible = true;
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                this.efwSimpleButton11.Visible = false;
                this.rbG_Prod.Visible = true;
                this.txtP_NameQ.Visible = true;
                this.efwLabel4.Visible = true;
                this.cmbIs_FactoryQ.Visible = false;
                this.efwLabel46.Visible = false;
            }
        }

        private void efwXtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage5 ^ efwXtraTabControl2.SelectedTabPage == this.xtraTabPage6)
            {
                this.efwLabel1.Text = "년월";
                this.dtYearMonth.Visible = true;
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                cmbIs_FactoryQ.EditValue = "001";
                this.efwSimpleButton11.Visible = true;
                this.rbG_Prod.Visible = true;
                this.efwLabel46.Visible = true;
                this.cmbIs_FactoryQ.Visible = true;
            }
            else if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage3 ^ efwXtraTabControl2.SelectedTabPage == this.xtraTabPage4 )
            {
                this.efwLabel1.Text = "일자";
                this.dtYearMonth.Visible = false;
                this.dtS_DATE.Visible = true;
                this.dtE_DATE.Visible = true;
                cmbIs_FactoryQ.EditValue = "001";
                this.rbG_Prod.Visible = false;
                this.efwSimpleButton11.Visible = false;
                this.efwSimpleButton11.Visible = false;
                this.efwLabel46.Visible = true;
                this.cmbIs_FactoryQ.Visible = true;
            }
        }

        private void efwSimpleButton9_Click_1(object sender, EventArgs e)
        {
            Open5();
        }

        private void efwSimpleButton10_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView5.DataRowCount; i++)
                    {
                        if (gridView5.GetRowCellValue(i, gridView5.Columns[2]).ToString().Length >= 1)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SAVE_05", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                                cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 6);

                                cmd.Parameters.Add("i_is_factory", MySqlDbType.VarChar, 3);
                                cmd.Parameters[1].Value = cmbIs_FactoryQ.EditValue.ToString();

                                cmd.Parameters.Add("i_p_code", MySqlDbType.Int32, 11);
                                cmd.Parameters[2].Value = Convert.ToInt32(gridView5.GetRowCellValue(i, gridView5.Columns[1]).ToString());

                                string sQty;
                                if (Convert.ToInt32(gridView5.GetRowCellValue(i, gridView5.Columns[3])).ToString() == "" ^ Convert.ToInt32(gridView5.GetRowCellValue(i, gridView5.Columns[3])).ToString() == null )
                                    sQty = "0";
                                else
                                    sQty = Convert.ToInt32(gridView5.GetRowCellValue(i, gridView5.Columns[3])).ToString();
                                cmd.Parameters.Add("i_qty", MySqlDbType.Int32, 11);
                                cmd.Parameters[3].Value = Convert.ToInt32(sQty);

                                string sPrice;
                                if (gridView5.GetRowCellValue(i, gridView5.Columns[4]).ToString() == "" ^ gridView5.GetRowCellValue(i, gridView5.Columns[4]).ToString() == null)
                                    sPrice = "0";
                                else
                                    sPrice = gridView5.GetRowCellValue(i, gridView5.Columns[4]).ToString();
                                cmd.Parameters.Add("i_price", MySqlDbType.Int32, 11);
                                cmd.Parameters[4].Value = Convert.ToInt32(sPrice);

                                string sAmt;
                                if (gridView5.GetRowCellValue(i, gridView5.Columns[5]).ToString() == "" ^ gridView5.GetRowCellValue(i, gridView5.Columns[5]).ToString() == null)
                                    sAmt = "0";
                                else
                                    sAmt = gridView5.GetRowCellValue(i, gridView5.Columns[5]).ToString();
                                cmd.Parameters.Add("i_amt", MySqlDbType.Int32, 11);
                                cmd.Parameters[5].Value = Convert.ToInt32(sAmt);


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

        private void efwSimpleButton11_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "이월처리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SAVE_06", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                            cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 6);

                            cmd.ExecuteNonQuery();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "이월처리가 완료되었습니다.");
            }
        }


        private void bandedGridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ////var gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            ////var rowIndex = gv.FocusedRowHandle;
            ////var columnIndex = gv.FocusedColumn.VisibleIndex;
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null)
                return;

            if (e.RowHandle >= 0)
            {
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void txtPrice_tab1_EditValueChanged(object sender, EventArgs e)
        {
            txtAmt_tab1.EditValue = Convert.ToInt32(txtQty_tab1.EditValue) * Convert.ToInt32(txtPrice_tab1.EditValue);
        }

        private void txtInput_Price_EditValueChanged(object sender, EventArgs e)
        {
            txtInput_Amt.EditValue = Convert.ToInt32(txtInput_Qty.EditValue) * Convert.ToInt32(txtInput_Price.EditValue);
        }


        private void efwSimpleButton12_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "기초 재고를 수불에 적용 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN28_SAVE_07", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                            cmd.Parameters[0].Value = dtYearMonth.EditValue3.Substring(0, 6);

                            cmd.ExecuteNonQuery();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "이월처리가 완료되었습니다.");
            }
        }


        private void txtQty_tab1_EditValueChanged(object sender, EventArgs e)
        {
            txtAmt_tab1.EditValue = Convert.ToInt32(txtQty_tab1.EditValue) * Convert.ToInt32(txtPrice_tab1.EditValue);
        }

        private void txtInput_Qty_EditValueChanged(object sender, EventArgs e)
        {
            txtInput_Amt.EditValue = Convert.ToInt32(txtInput_Qty.EditValue) * Convert.ToInt32(txtInput_Price.EditValue);
        }
    }
}
