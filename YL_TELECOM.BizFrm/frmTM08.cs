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
    public partial class frmTM08 : FrmBase
    {
        frmTM08_Pop01 popup1;
        frmTM08_Pop02 popup2;
        frmTM08_Pop03 popup3;
        frmTM08_Pop04 popup4;
        public frmTM08()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "TM08";
            //폼명설정
            this.FrmName = "기초재고 등록";
        }

        private void frmTM08_Load(object sender, EventArgs e)
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
            this.IsExcel = true;

            dtYear_Month.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtYear_Month.Properties.Mask.EditMask = "yyyy-MM";
            dtYear_Month.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtYear_Month.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtYear_Month.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtYear_Month.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            btnFactory.EditValue = "4099";
            txtFactory_NM.EditValue = "여유텔레콤(본사창고)";
            rbType.EditValue = "1";

            this.efwLabel1.Text = "년월";
            this.dtYear_Month.Visible = true;
            this.dtS_DATE.Visible = false;
            this.dtE_DATE.Visible = false;
            efwLabel2.Visible = false;
            btnExcelUpdate.Visible = true;
            efwSimpleButton2.Visible = true;
            efwLabel5.Visible = false;
            btnFactory.Visible = false;
            txtFactory_NM.Visible = false;
            rbType.Visible = false;
            efwSimpleButton11.Visible = false;

            //tab1
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["qty"].SummaryItem.FieldName = "qty";
            gridView1.Columns["qty"].SummaryItem.DisplayFormat = "수량: {0}";

            gridView1.Columns["amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["amt"].SummaryItem.FieldName = "amt";
            gridView1.Columns["amt"].SummaryItem.DisplayFormat = "금액: {0:c}";

            this.efwGridControl1.BindControlSet(
            new ColumnControlSet("ser_no", txtser_no)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;

            //tab2
            gridView2.OptionsView.ShowFooter = true;
            gridView2.Columns["qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["qty"].SummaryItem.FieldName = "qty";
            gridView2.Columns["qty"].SummaryItem.DisplayFormat = "수량: {0}";
            gridView2.Columns["amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["amt"].SummaryItem.FieldName = "amt";
            gridView2.Columns["amt"].SummaryItem.DisplayFormat = "금액: {0:c}";
            this.efwGridControl2.BindControlSet(
               new ColumnControlSet("idx", txtidx)
            );
            this.efwGridControl2.Click += efwGridControl2_Click;

            //tab3
            gridView3.OptionsView.ShowFooter = true;
            gridView3.Columns["qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["qty"].SummaryItem.FieldName = "qty";
            gridView3.Columns["qty"].SummaryItem.DisplayFormat = "수량: {0}";

            this.efwGridControl3.BindControlSet(
            new ColumnControlSet("ser_no", txtser_no)
            );
            this.efwGridControl3.Click += efwGridControl3_Click;

            //tab5
            gridView5.OptionsView.ShowFooter = true;
            gridView5.Columns["bef_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["bef_qty"].SummaryItem.FieldName = "bef_qty";
            gridView5.Columns["bef_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView5.Columns["bef_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["bef_amt"].SummaryItem.FieldName = "bef_amt";
            gridView5.Columns["bef_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView5.Columns["in_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["in_qty"].SummaryItem.FieldName = "in_qty";
            gridView5.Columns["in_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView5.Columns["in_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["in_amt"].SummaryItem.FieldName = "in_amt";
            gridView5.Columns["in_amt"].SummaryItem.DisplayFormat = "{0:c}";
            gridView5.Columns["out_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["out_qty"].SummaryItem.FieldName = "out_qty";
            gridView5.Columns["out_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView5.Columns["out_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["out_amt"].SummaryItem.FieldName = "out_amt";
            gridView5.Columns["out_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView5.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView5.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView5.Columns["stock_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["stock_amt"].SummaryItem.FieldName = "stock_amt";
            gridView5.Columns["stock_amt"].SummaryItem.DisplayFormat = "{0:c}";

            //tab6
            gridView6.OptionsView.ShowFooter = true;
            gridView6.Columns["bef_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["bef_qty"].SummaryItem.FieldName = "bef_qty";
            gridView6.Columns["bef_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView6.Columns["bef_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["bef_amt"].SummaryItem.FieldName = "bef_amt";
            gridView6.Columns["bef_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView6.Columns["in_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["in_qty"].SummaryItem.FieldName = "in_qty";
            gridView6.Columns["in_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView6.Columns["in_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["in_amt"].SummaryItem.FieldName = "in_amt";
            gridView6.Columns["in_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView6.Columns["out_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["out_qty"].SummaryItem.FieldName = "out_qty";
            gridView6.Columns["out_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView6.Columns["out_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["out_amt"].SummaryItem.FieldName = "out_amt";
            gridView6.Columns["out_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView6.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView6.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView6.Columns["stock_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["stock_amt"].SummaryItem.FieldName = "stock_amt";
            gridView6.Columns["stock_amt"].SummaryItem.DisplayFormat = "{0:c}";
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["ser_no"].ToString() != "")
            {
                this.txtser_no.EditValue = dr["ser_no"].ToString();
                this.txtm_code.EditValue = dr["m_code"].ToString();
            }
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                this.txtidx.EditValue = dr["idx"].ToString();
            }
        }

        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl3.GetSelectedRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                this.txtidx.EditValue = dr["idx"].ToString();
            }
        }

        private void efwGridControl5_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl5.GetSelectedRow(0);

            if (dr != null && dr["ser_no"].ToString() != "")
            {
                this.txtser_no.EditValue = dr["ser_no"].ToString();
            }

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
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open4();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                Open5();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage6)
            {
                Open6();
            }
        }

        public override void Save()
        {
            try
            {

                var saveResult = new SaveTableResultInfo() { IsError = true };

                var dt = efwGridControl4.GetChangeDataWithRowState;
                var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][StatusColumn].ToString() == "U")
                    {
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SAVE_06", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_entr_no", MySqlDbType.VarChar, 20);
                                cmd.Parameters[0].Value = dt.Rows[i]["a_entr_no"].ToString();

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
        // 엑셀 버튼
        private void btnExcelUpdate_Click(object sender, EventArgs e)
        {   // 기초
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                popup1 = new frmTM08_Pop01();
                popup1.ShowDialog();
            }
            // 입고
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                popup2 = new frmTM08_Pop02();
                popup2.ShowDialog();
            }
            // 출고
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                popup3 = new frmTM08_Pop03();
                popup3.ShowDialog();
            }
        }

    
        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.efwLabel1.Text = "년월";
                this.dtYear_Month.Visible = true;
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                efwLabel2.Visible = false;
                btnExcelUpdate.Visible = true;
                efwSimpleButton2.Visible = true;
                efwLabel5.Visible = false;
                btnFactory.Visible = false;
                txtFactory_NM.Visible = false;
                rbType.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2 ^ efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                this.efwLabel1.Text = "일자";
                this.dtYear_Month.Visible = false;
                this.dtS_DATE.Visible = true;
                this.dtE_DATE.Visible = true;
                efwLabel2.Visible = true;
                btnExcelUpdate.Visible = true;
                efwSimpleButton2.Visible = false;
                efwLabel5.Visible = false;
                btnFactory.Visible = false;
                txtFactory_NM.Visible = false;
                rbType.Visible = false;
                efwSimpleButton11.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                this.efwLabel1.Text = "일자";
                this.dtYear_Month.Visible = false;
                this.dtS_DATE.Visible = true;
                this.dtE_DATE.Visible = true;
                efwLabel2.Visible = true;
                btnExcelUpdate.Visible = false;
                efwSimpleButton2.Visible = false;
                efwLabel5.Visible = false;
                btnFactory.Visible = false;
                txtFactory_NM.Visible = false;
                rbType.Visible = false;
                efwSimpleButton11.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5 ^ efwXtraTabControl1.SelectedTabPage == this.xtraTabPage6)
            {
                this.efwLabel1.Text = "년월";
                this.dtYear_Month.Visible = true;
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                efwLabel2.Visible = false;
                btnExcelUpdate.Visible = false;
                efwSimpleButton2.Visible = false;
                efwLabel5.Visible = true;
                btnFactory.Visible = true;
                txtFactory_NM.Visible = true;
                rbType.Visible = true;
                efwSimpleButton11.Visible = true;
            }
        }
        // 기초
        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);
                        
                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtSearch.EditValue;


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
        // 입고
        private void Open2()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;


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
        // 출고
        private void Open3()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        // 현행화 출고
        private void Open4()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SELECT_08", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_cust_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;

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
        // 재고현황 (개별)
        private void Open5()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_factory", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = btnFactory.EditValue;

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;

                        cmd.Parameters.Add("i_Type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbType.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
                            this.efwGridControl5.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        // 재고현황 (통합)
        private void Open6()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_factory", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = btnFactory.EditValue;

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;

                        cmd.Parameters.Add("i_Type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbType.EditValue;

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

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SAVE_02", con))
                    {

                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_YearMonth", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "수량 금액을 기말 재고로 적용 하였습니다.");
            Search();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            this.txtser_no.EditValue = dr["ser_no"].ToString();
            this.txtm_code.EditValue = dr["m_code"].ToString();
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_m_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_m_code"].Value = txtm_code.EditValue.ToString();
                            cmd.Parameters["i_m_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ser_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_ser_no"].Value = txtser_no.EditValue.ToString();
                            cmd.Parameters["i_ser_no"].Direction = ParameterDirection.Input;

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
                txtser_no.EditValue = "";
                txtm_code.EditValue = "";
                Search();
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            this.txtidx.EditValue = dr["idx"].ToString();

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_DELETE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue);
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
                txtidx.EditValue = "";
                Search();
            }
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl3.GetSelectedRow(0);
            this.txtidx.EditValue = dr["idx"].ToString();

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_DELETE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue);
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
                txtidx.EditValue = "";
                Search();
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            popup4 = new frmTM08_Pop04();


            popup4.m_code = gridView5.GetFocusedRowCellValue("m_code").ToString();
            popup4.ser_no = gridView5.GetFocusedRowCellValue("ser_no").ToString();

            popup4.FormClosed += popup_FormClosed;
            popup4.ShowDialog();
        }

        private void efwSimpleButton2_Click_1(object sender, EventArgs e)
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SAVE_02", con))
                    {

                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_YearMonth", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "수량 금액을 기말 재고로 적용 하였습니다.");
            Search();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup4.FormClosed -= popup_FormClosed;

            popup4 = null;
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(gridView1.GetFocusedDisplayText());
            e.Handled = true;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void efwGridControl2_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(gridView2.GetFocusedDisplayText());
            e.Handled = true;
        }

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(gridView3.GetFocusedDisplayText());
            e.Handled = true;
        }

        private void gridView5_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(gridView5.GetFocusedDisplayText());
            e.Handled = true;
        }

        private void btnFactory_Click(object sender, EventArgs e)
        {
            frmFactory FrmInfo = new frmFactory() { Factory = btnFactory, Factory_NM = txtFactory_NM };
            FrmInfo.ShowDialog();
        }
        // 평균단가 산정 이월처리
        private void efwSimpleButton11_Click(object sender, EventArgs e)
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM08_SAVE_07", con))
                    {

                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_YearMonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "이월처리가 완료 되었습니다.");
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(gridView1.GetFocusedDisplayText());
            e.Handled = true;
        }

        private void advBandedGridView4_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(advBandedGridView4.GetFocusedDisplayText());
            e.Handled = true;
        }

        private void gridView6_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(gridView6.GetFocusedDisplayText());
            e.Handled = true;
        }


    }
}
