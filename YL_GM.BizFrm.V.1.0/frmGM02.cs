#region "frmGM03 설명"
//===================================================================================================
//■Program Name  : frmGSHOP02
//■Description   : MD별 G멀티샵 등록현황
//■Author        : 송호철
//■Date          : 2019.07.03
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.07.03][송호철] Base
//[2] [2019.07.03][송호철] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

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
    public partial class frmGM02 : FrmBase
    {
        frmGM02_03_Pop01 popup;

        public frmGM02()
        {
            InitializeComponent();
            this.QCode = "GM02";
            //폼명설정
            this.FrmName = "MD별 G멀티샵 등록현황";
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

            dtS_DATE.EditValue = DateTime.Now;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;
            gridView2.OptionsView.ShowFooter = true;

            gridView1.Columns["month1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month1"].SummaryItem.FieldName = "month1";
            gridView1.Columns["month1"].SummaryItem.DisplayFormat = "{0}";


            gridView1.Columns["month2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month2"].SummaryItem.FieldName = "month2";
            gridView1.Columns["month2"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month3"].SummaryItem.FieldName = "month3";
            gridView1.Columns["month3"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month4"].SummaryItem.FieldName = "month4";
            gridView1.Columns["month4"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month5"].SummaryItem.FieldName = "month5";
            gridView1.Columns["month5"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month6"].SummaryItem.FieldName = "month6";
            gridView1.Columns["month6"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month7"].SummaryItem.FieldName = "month7";
            gridView1.Columns["month7"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month8"].SummaryItem.FieldName = "month8";
            gridView1.Columns["month8"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month9"].SummaryItem.FieldName = "month9";
            gridView1.Columns["month9"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month10"].SummaryItem.FieldName = "month10";
            gridView1.Columns["month10"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month11"].SummaryItem.FieldName = "month11";
            gridView1.Columns["month11"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month12"].SummaryItem.FieldName = "month12";
            gridView1.Columns["month12"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["total"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["total"].SummaryItem.FieldName = "total";
            gridView1.Columns["total"].SummaryItem.DisplayFormat = "{0}";

            gridView2.Columns["total"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["total"].SummaryItem.FieldName = "total";
            gridView2.Columns["total"].SummaryItem.DisplayFormat = "{0}";
            rbYrarType.EditValue = "1";


            Series series1 = new Series("series1", ViewType.Line);
            ((LineSeriesView)series1.View).LineStyle.Thickness = 1;

                    //((LineSeriesView)series1.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            //((LineSeriesView)series1.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
            //((LineSeriesView)series1.View).LineStyle.DashStyle = DashStyle.Dash;

        }


        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GM_GM02_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0,4);

                        cmd.Parameters.Add("i_mdt_type", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = "";

                       
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
                Open1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        public void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GM_GM02_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbYrarType.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }

                ChartCreat1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        public void ChartCreat1()
        {
            //차트 클리어
            for (int i = 0; i < chartControl2.Series.Count; i++)
                this.chartControl2.Series[i].Points.Clear();


            SeriesPoint sPont = null;

            //시리즈 포인트
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                sPont = new SeriesPoint(gridView2.GetRowCellValue(i, "u_nickname"), Convert.ToInt16(gridView2.GetRowCellValue(i, "total")));
                                this.chartControl2.Series["Series 1"].Points.Add(sPont);
            }
        }
        private void EfwGridControl1_DoubleClick(object sender, EventArgs e)
        {

            popup = new frmGM02_03_Pop01();


            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            popup.pU_ID = gridView1.GetFocusedRowCellValue("u_id").ToString();
            popup.pYEAR = dtS_DATE.EditValue3.Substring(0, 4);
            popup.pQTYPE = "GM02";

            popup.pQ1 = gridView1.GetFocusedRowCellValue("u_name").ToString();
            popup.pQ2 = gridView1.GetFocusedRowCellValue("u_nickname").ToString();

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }

        private void RbYrarType_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}
