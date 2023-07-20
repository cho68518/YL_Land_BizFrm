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

    public partial class frmGM12 : FrmBase
    {
        frmGM12_Pop01 popup1;
        public frmGM12()
        {
            InitializeComponent();
            this.QCode = "GM12";
            //폼명설정
            this.FrmName = "MD별 멀티샵 실적현황";
        }

        private void frmGM12_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            dtS_DATE.EditValue = DateTime.Now;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            advBandedGridView1.OptionsView.ShowFooter = true;
            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;



            rbis_biz.EditValue = "0";
            
            dtStart_Date.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dtEnd_Date.EditValue = DateTime.Now;
            rbCom.EditValue = "1";

            this.efwLabel1.Text = "년도";
            this.dtS_DATE.Visible = true;
            this.dtStart_Date.Visible = false;
            this.dtEnd_Date.Visible = false;
            this.rbis_biz.Visible = true;
            this.efwLabel2.Visible = false;

            advBandedGridView1.Columns["input1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input1"].SummaryItem.FieldName = "input1";
            advBandedGridView1.Columns["input1"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["qty1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty1"].SummaryItem.FieldName = "qty1";
            advBandedGridView1.Columns["qty1"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt1"].SummaryItem.FieldName = "amt1";
            advBandedGridView1.Columns["amt1"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_1"].SummaryItem.FieldName = "amt_1";
            advBandedGridView1.Columns["amt_1"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot1"].SummaryItem.FieldName = "tot1";
            advBandedGridView1.Columns["tot1"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["input2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input2"].SummaryItem.FieldName = "input2";
            advBandedGridView1.Columns["input2"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["qty2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty2"].SummaryItem.FieldName = "qty2";
            advBandedGridView1.Columns["qty2"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt2"].SummaryItem.FieldName = "amt2";
            advBandedGridView1.Columns["amt2"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_2"].SummaryItem.FieldName = "amt_2";
            advBandedGridView1.Columns["amt_2"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot2"].SummaryItem.FieldName = "tot2";
            advBandedGridView1.Columns["tot2"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["input3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input3"].SummaryItem.FieldName = "input3";
            advBandedGridView1.Columns["input3"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["qty3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty3"].SummaryItem.FieldName = "qty3";
            advBandedGridView1.Columns["qty3"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt3"].SummaryItem.FieldName = "amt3";
            advBandedGridView1.Columns["amt3"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_3"].SummaryItem.FieldName = "amt_3";
            advBandedGridView1.Columns["amt_3"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot3"].SummaryItem.FieldName = "tot3";
            advBandedGridView1.Columns["tot3"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["input4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input4"].SummaryItem.FieldName = "input4";
            advBandedGridView1.Columns["input4"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["qty4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty4"].SummaryItem.FieldName = "qty4";
            advBandedGridView1.Columns["qty4"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt4"].SummaryItem.FieldName = "amt4";
            advBandedGridView1.Columns["amt4"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_4"].SummaryItem.FieldName = "amt_4";
            advBandedGridView1.Columns["amt_4"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot4"].SummaryItem.FieldName = "tot4";
            advBandedGridView1.Columns["tot4"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["input5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input5"].SummaryItem.FieldName = "input5";
            advBandedGridView1.Columns["input5"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["qty5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty5"].SummaryItem.FieldName = "qty5";
            advBandedGridView1.Columns["qty5"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt5"].SummaryItem.FieldName = "amt5";
            advBandedGridView1.Columns["amt5"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_5"].SummaryItem.FieldName = "amt_5";
            advBandedGridView1.Columns["amt_5"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot5"].SummaryItem.FieldName = "tot5";
            advBandedGridView1.Columns["tot5"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["input6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input6"].SummaryItem.FieldName = "input6";
            advBandedGridView1.Columns["input6"].SummaryItem.DisplayFormat = "{0}";


            advBandedGridView1.Columns["qty6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty6"].SummaryItem.FieldName = "qty6";
            advBandedGridView1.Columns["qty6"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt6"].SummaryItem.FieldName = "amt6";
            advBandedGridView1.Columns["amt6"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_6"].SummaryItem.FieldName = "amt_6";
            advBandedGridView1.Columns["amt_6"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot6"].SummaryItem.FieldName = "tot6";
            advBandedGridView1.Columns["tot6"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["input7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input7"].SummaryItem.FieldName = "input7";
            advBandedGridView1.Columns["input7"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["qty7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty7"].SummaryItem.FieldName = "qty7";
            advBandedGridView1.Columns["qty7"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt7"].SummaryItem.FieldName = "amt7";
            advBandedGridView1.Columns["amt7"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["amt_7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_7"].SummaryItem.FieldName = "amt_7";
            advBandedGridView1.Columns["amt_7"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot7"].SummaryItem.FieldName = "tot7";
            advBandedGridView1.Columns["tot7"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["input8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input8"].SummaryItem.FieldName = "input8";
            advBandedGridView1.Columns["input8"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["qty8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty8"].SummaryItem.FieldName = "qty8";
            advBandedGridView1.Columns["qty8"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt8"].SummaryItem.FieldName = "amt8";
            advBandedGridView1.Columns["amt8"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_8"].SummaryItem.FieldName = "amt_8";
            advBandedGridView1.Columns["amt_8"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot8"].SummaryItem.FieldName = "tot8";
            advBandedGridView1.Columns["tot8"].SummaryItem.DisplayFormat = "{0:c}";



            advBandedGridView1.Columns["input9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input9"].SummaryItem.FieldName = "input9";
            advBandedGridView1.Columns["input9"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["qty9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty9"].SummaryItem.FieldName = "qty9";
            advBandedGridView1.Columns["qty9"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt9"].SummaryItem.FieldName = "amt9";
            advBandedGridView1.Columns["amt9"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_9"].SummaryItem.FieldName = "amt_9";
            advBandedGridView1.Columns["amt_9"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot9"].SummaryItem.FieldName = "tot9";
            advBandedGridView1.Columns["tot9"].SummaryItem.DisplayFormat = "{0:c}";



            advBandedGridView1.Columns["input10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input10"].SummaryItem.FieldName = "input10";
            advBandedGridView1.Columns["input10"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["qty10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty10"].SummaryItem.FieldName = "qty10";
            advBandedGridView1.Columns["qty10"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt10"].SummaryItem.FieldName = "amt10";
            advBandedGridView1.Columns["amt10"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_10"].SummaryItem.FieldName = "amt_10";
            advBandedGridView1.Columns["amt_10"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot10"].SummaryItem.FieldName = "tot10";
            advBandedGridView1.Columns["tot10"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["input11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input11"].SummaryItem.FieldName = "input11";
            advBandedGridView1.Columns["input11"].SummaryItem.DisplayFormat = "{0}";


            advBandedGridView1.Columns["qty11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty11"].SummaryItem.FieldName = "qty11";
            advBandedGridView1.Columns["qty11"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt11"].SummaryItem.FieldName = "amt11";
            advBandedGridView1.Columns["amt11"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_11"].SummaryItem.FieldName = "amt_11";
            advBandedGridView1.Columns["amt_11"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot11"].SummaryItem.FieldName = "tot11";
            advBandedGridView1.Columns["tot11"].SummaryItem.DisplayFormat = "{0:c}";




            advBandedGridView1.Columns["input12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input12"].SummaryItem.FieldName = "input12";
            advBandedGridView1.Columns["input12"].SummaryItem.DisplayFormat = "{0}";


            advBandedGridView1.Columns["qty12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["qty12"].SummaryItem.FieldName = "qty12";
            advBandedGridView1.Columns["qty12"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["amt12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt12"].SummaryItem.FieldName = "amt12";
            advBandedGridView1.Columns["amt12"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["amt_12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amt_12"].SummaryItem.FieldName = "amt_11";
            advBandedGridView1.Columns["amt_12"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["tot12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["tot12"].SummaryItem.FieldName = "tot12";
            advBandedGridView1.Columns["tot12"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["input12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["input12"].SummaryItem.FieldName = "input12";
            advBandedGridView1.Columns["input12"].SummaryItem.DisplayFormat = "{0}";


            advBandedGridView1.Columns["total1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["total1"].SummaryItem.FieldName = "total1";
            advBandedGridView1.Columns["total1"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["total2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["total2"].SummaryItem.FieldName = "total2";
            advBandedGridView1.Columns["total2"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["total3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["total3"].SummaryItem.FieldName = "total3";
            advBandedGridView1.Columns["total3"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["total4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["total4"].SummaryItem.FieldName = "total4";
            advBandedGridView1.Columns["total4"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["totamt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["totamt"].SummaryItem.FieldName = "totamt";
            advBandedGridView1.Columns["totamt"].SummaryItem.DisplayFormat = "{0:c}";

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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM12_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 4);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);

                        cmd.Parameters.Add("i_is_biz", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbis_biz.EditValue;

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
            }
        }

        private void Open2()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM12_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_start_date", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = dtStart_Date.EditValue.ToString().Substring(0, 10);

                        cmd.Parameters.Add("i_end_date", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = dtEnd_Date.EditValue.ToString().Substring(0, 10);

                        cmd.Parameters.Add("i_com", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = rbCom.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            //this.efwGridControl2.MyGridView.BestFitColumns();

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


        private void btnDetail1_Click(object sender, EventArgs e)
        {
            popup1 = new frmGM12_Pop01();

            popup1.md_u_id = advBandedGridView1.GetFocusedRowCellValue("registrant_uid").ToString();
            popup1.u_nickname = advBandedGridView1.GetFocusedRowCellValue("u_nickname").ToString();
            popup1.year = Convert.ToDateTime(dtS_DATE.EditValue);
            popup1.is_biz = rbis_biz.EditValue.ToString();
            popup1.FormClosed += popup1_FormClosed;
            popup1.ShowDialog();
        }
        private void popup1_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup1.FormClosed -= popup1_FormClosed;

            popup1 = null;
        }

        private void rbis_biz_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.efwLabel1.Text = "년도";
                this.dtS_DATE.Visible = true;
                this.dtStart_Date.Visible = false;
                this.dtEnd_Date.Visible = false;
                this.rbis_biz.Visible = true;
                this.efwLabel2.Visible = false;
            }

            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                this.efwLabel1.Text = "일자";
                this.dtS_DATE.Visible = false;
                this.dtStart_Date.Visible = true;
                this.dtEnd_Date.Visible = true;
                this.rbis_biz.Visible = false;
                this.efwLabel2.Visible = true;
            }
        }
    }
}
