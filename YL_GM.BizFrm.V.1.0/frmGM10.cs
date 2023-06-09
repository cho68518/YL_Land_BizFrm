using DevExpress.XtraCharts;
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

namespace YL_GM.BizFrm
{
    public partial class frmGM10: FrmBase
    {
        public frmGM10()
        {
            InitializeComponent();
            this.QCode = "GM10";
            //폼명설정
            this.FrmName = "멀티샵별 매출 현황";
        }

        private void frmGM10_Load(object sender, EventArgs e)
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

            dtS_DATE.EditValue = DateTime.Now;

            dtSTART_DATE.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dtEND_DATE.EditValue = DateTime.Now;
            this.dtSTART_DATE.Visible = false;
            this.dtEND_DATE.Visible = false;
            this.efwLabel7.Visible = false;
            this.rbQ_type.Visible = false;
            this.rbShow_Level.Visible = false;
            this.txtProd_Name.Visible = false;
            this.efwLabel2.Visible = false;

            rbShop_Type.EditValue = "3";
            rbProd_Type.EditValue = "1";
            rbQtyOrAmt.EditValue = "1";
            rbQ_type.EditValue = "2";
            rbShow_Level.EditValue = "9";
            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            advBandedGridView1.OptionsView.ShowFooter = true;
            // 헤더 TATLE 추가
            //gridView1.ViewCaption = "1월"; gridView1.OptionsView.ShowViewCaption = true; Font dFont = gridView1.Appearance.ViewCaption.Font; gridView1.Appearance.ViewCaption.Font = new Font(dFont.FontFamily, dFont.Size + 20);

            advBandedGridView1.Columns["month1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month1"].SummaryItem.FieldName = "month1";
            advBandedGridView1.Columns["month1"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["month2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month2"].SummaryItem.FieldName = "month2";
            advBandedGridView1.Columns["month2"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month3"].SummaryItem.FieldName = "month3";
            advBandedGridView1.Columns["month3"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month4"].SummaryItem.FieldName = "month4";
            advBandedGridView1.Columns["month4"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month5"].SummaryItem.FieldName = "month5";
            advBandedGridView1.Columns["month5"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month6"].SummaryItem.FieldName = "month6";
            advBandedGridView1.Columns["month6"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month7"].SummaryItem.FieldName = "month7";
            advBandedGridView1.Columns["month7"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month8"].SummaryItem.FieldName = "month8";
            advBandedGridView1.Columns["month8"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month9"].SummaryItem.FieldName = "month9";
            advBandedGridView1.Columns["month9"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month10"].SummaryItem.FieldName = "month10";
            advBandedGridView1.Columns["month10"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month11"].SummaryItem.FieldName = "month11";
            advBandedGridView1.Columns["month11"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["month12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["month12"].SummaryItem.FieldName = "month12";
            advBandedGridView1.Columns["month12"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot"].SummaryItem.FieldName = "tot";
            advBandedGridView1.Columns["tot"].SummaryItem.DisplayFormat = "{0:c}";




            // 도넛 사용금액
            advBandedGridView1.Columns["d_month1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month1"].SummaryItem.FieldName = "d_month1";
            advBandedGridView1.Columns["d_month1"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["d_month2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month2"].SummaryItem.FieldName = "d_month2";
            advBandedGridView1.Columns["d_month2"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month3"].SummaryItem.FieldName = "d_month3";
            advBandedGridView1.Columns["d_month3"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month4"].SummaryItem.FieldName = "d_month4";
            advBandedGridView1.Columns["d_month4"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month5"].SummaryItem.FieldName = "d_month5";
            advBandedGridView1.Columns["d_month5"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month6"].SummaryItem.FieldName = "d_month6";
            advBandedGridView1.Columns["d_month6"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month7"].SummaryItem.FieldName = "d_month7";
            advBandedGridView1.Columns["d_month7"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month8"].SummaryItem.FieldName = "d_month8";
            advBandedGridView1.Columns["d_month8"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month9"].SummaryItem.FieldName = "d_month9";
            advBandedGridView1.Columns["d_month9"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month10"].SummaryItem.FieldName = "d_month10";
            advBandedGridView1.Columns["d_month10"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month11"].SummaryItem.FieldName = "d_month11";
            advBandedGridView1.Columns["d_month11"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_month12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_month12"].SummaryItem.FieldName = "d_month12";
            advBandedGridView1.Columns["d_month12"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["d_tot"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["d_tot"].SummaryItem.FieldName = "d_tot";
            advBandedGridView1.Columns["d_tot"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["chramt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["chramt"].SummaryItem.FieldName = "chramt";
            advBandedGridView1.Columns["chramt"].SummaryItem.DisplayFormat = "{0:c}";



            gridView3.Columns["order_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["order_cnt"].SummaryItem.FieldName = "order_cnt";
            gridView3.Columns["order_cnt"].SummaryItem.DisplayFormat = "{0}";


            gridView3.Columns["order_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["order_amt"].SummaryItem.FieldName = "order_amt";
            gridView3.Columns["order_amt"].SummaryItem.DisplayFormat = "{0:c}";


            gridView3.Columns["cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["cnt"].SummaryItem.FieldName = "cnt";
            gridView3.Columns["cnt"].SummaryItem.DisplayFormat = "{0}";


            // ----------------------------------------------------------------
            gridView1.Columns["order_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["order_amt"].SummaryItem.FieldName = "order_amt";
            gridView1.Columns["order_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month1"].SummaryItem.FieldName = "month1";
            gridView1.Columns["month1"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month2"].SummaryItem.FieldName = "month2";
            gridView1.Columns["month2"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month3"].SummaryItem.FieldName = "month3";
            gridView1.Columns["month3"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month4"].SummaryItem.FieldName = "month4";
            gridView1.Columns["month4"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month5"].SummaryItem.FieldName = "month5";
            gridView1.Columns["month5"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month6"].SummaryItem.FieldName = "month6";
            gridView1.Columns["month6"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month7"].SummaryItem.FieldName = "month7";
            gridView1.Columns["month7"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month8"].SummaryItem.FieldName = "month8";
            gridView1.Columns["month8"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month9"].SummaryItem.FieldName = "month9";
            gridView1.Columns["month9"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month10"].SummaryItem.FieldName = "month10";
            gridView1.Columns["month10"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month11"].SummaryItem.FieldName = "month11";
            gridView1.Columns["month11"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["month12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month12"].SummaryItem.FieldName = "month12";
            gridView1.Columns["month12"].SummaryItem.DisplayFormat = "{0:c}";




            advBandedGridView2.OptionsView.ShowFooter = true;


            advBandedGridView2.Columns["order_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView2.Columns["order_amt"].SummaryItem.FieldName = "order_amt";
            advBandedGridView2.Columns["order_amt"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView2.Columns["member_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView2.Columns["member_amt"].SummaryItem.FieldName = "member_amt";
            advBandedGridView2.Columns["member_amt"].SummaryItem.DisplayFormat = "{0:c}";



            this.efwGridControl3.BindControlSet(
                new ColumnControlSet("u_id", txtu_id)
                );
        }

        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl3.GetSelectedRow(0);

            if (dr != null && dr["u_id"].ToString() != "")
            {
                this.txtu_id.EditValue = dr["u_id"].ToString();

                if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage3)
                {
                    Open3();
                }
                else if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage4)
                {
                    Open4();
                }
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

            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                Open6();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage6)
            {
                Open8();
            }
        }

        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;
                Cursor.Current = Cursors.WaitCursor;
                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM10_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);


                        cmd.Parameters.Add("i_shop_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbShop_Type.EditValue;

                        cmd.Parameters.Add("i_prod_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = rbProd_Type.EditValue;

                        cmd.Parameters.Add("i_QtyOrAmt", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbQtyOrAmt.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
                //ChartCreat1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void Open2()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM10_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();

                        }
                    }
                }
                //ChartCreat1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }


        private void Open3()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM10_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_md_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtu_id.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            this.efwGridControl4.MyGridView.BestFitColumns();

                        }
                    }
                }
                //ChartCreat1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }


        private void Open4()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM10_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);

                        cmd.Parameters.Add("i_md_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtu_id.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
                //ChartCreat1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void Open6()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM10_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtSTART_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtEND_DATE.EditValue3;



                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
                            this.efwGridControl5.MyGridView.BestFitColumns();

                        }
                    }
                }
                //ChartCreat1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void Open7()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM10_SELECT_06", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtSTART_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtEND_DATE.EditValue3;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtu_id.EditValue;
                        
                        cmd.Parameters.Add("i_q_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbQ_type.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl6.DataBind(ds);
                            this.efwGridControl6.MyGridView.BestFitColumns();

                        }
                    }
                }
                //ChartCreat1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void Open8()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM10_SELECT_07", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtSTART_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtEND_DATE.EditValue3;

                        cmd.Parameters.Add("i_show_level", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = rbShow_Level.EditValue;

                        cmd.Parameters.Add("i_prod_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtProd_Name.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl7.DataBind(ds);
                            this.efwGridControl7.MyGridView.BestFitColumns();

                        }
                    }
                }
                //ChartCreat1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {


            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.efwLabel1.Text = "년도";
                
                this.dtS_DATE.Visible = true; 
                this.dtEND_DATE.Visible = false;
                this.dtSTART_DATE.Visible = false;
                this.rbProd_Type.Visible = true;
                this.rbQtyOrAmt.Visible = true;
                this.efwLabel7.Visible = false;
                this.rbQ_type.Visible = false;
                this.rbShow_Level.Visible = false;
                this.txtProd_Name.Visible = false;
                this.efwLabel2.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                this.efwLabel1.Text = "년도";
                this.dtS_DATE.Visible = true;
                this.dtEND_DATE.Visible = false;
                this.dtSTART_DATE.Visible = false;
                this.rbProd_Type.Visible = true;
                this.rbQtyOrAmt.Visible = true;
                this.efwLabel7.Visible = false;
                this.rbQ_type.Visible = false;
                this.rbShow_Level.Visible = false;
                this.txtProd_Name.Visible = false;
                this.efwLabel2.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                this.efwLabel1.Text = "입금일";
                this.dtS_DATE.Visible = false;
                this.dtEND_DATE.Visible = true;
                this.dtSTART_DATE.Visible = true;
                this.rbProd_Type.Visible = false;
                this.rbQtyOrAmt.Visible = false;
                this.efwLabel7.Visible = true;
                this.rbQ_type.Visible = true;
                this.rbShow_Level.Visible = false;
                this.txtProd_Name.Visible = false;
                this.efwLabel2.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage6)
            {
                this.efwLabel1.Text = "입금일";
                this.dtS_DATE.Visible = false;
                this.dtEND_DATE.Visible = true;
                this.dtSTART_DATE.Visible = true;
                this.rbProd_Type.Visible = false;
                this.rbQtyOrAmt.Visible = false;
                this.efwLabel7.Visible = true;
                this.rbQ_type.Visible = true;
                this.rbShow_Level.Visible = true;
                this.txtProd_Name.Visible = true;
                this.efwLabel2.Visible = true;
            } 

            // Search();
        }

        private void efwXtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
            }
            else if (efwXtraTabControl2.SelectedTabPage == this.xtraTabPage4)
            {
                Open4();
            }
        }

        private void efwGridControl5_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl5.GetSelectedRow(0);
            if (dr != null && dr["u_id"].ToString() != "")
            {
                this.txtu_id.EditValue = dr["u_id"].ToString();
                Open7();
            }
        }

        private void txtProd_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
