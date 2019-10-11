#region "frmGM03 설명"
//===================================================================================================
//■Program Name  : frmGSHOP03
//■Description   : 지역별 G멀티샵 등록현황
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
    public partial class frmGM03 : FrmBase
    {
        frmGM02_03_Pop01 popup;
        public frmGM03()
        {
            InitializeComponent();
            this.QCode = "GM03";
            //폼명설정
            this.FrmName = "지역별 G멀티샵 등록현황";
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
            rbYearType.EditValue = "1";
        }


        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);




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
                Open1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        private void EfwGridControl1_DoubleClick(object sender, EventArgs e)
        {

            popup = new frmGM02_03_Pop01();


            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);


            popup.pYEAR = dtS_DATE.EditValue3.Substring(0, 4);
            popup.pQTYPE = "GM03";
            popup.pSIDO = gridView1.GetFocusedRowCellValue("sido_co").ToString();
            popup.pGUGUN = gridView1.GetFocusedRowCellValue("gugun_co").ToString();

            popup.pQ1 = gridView1.GetFocusedRowCellValue("sido").ToString();
            popup.pQ2 = gridView1.GetFocusedRowCellValue("gugun").ToString();

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }

        private void Open1()
        {
            try
            {
                //string sP_SHOW_TYPE = string.Empty;
                //decimal n1 = 0; decimal n2 = 0;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM03_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);


                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbYearType.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow[] rows = dt.Select();
                            // 지역별 COUNT
                                efwArea1.Text = String.Format("{0:#,##0}", rows[0]["Area1"]);
                                efwArea2.Text = String.Format("{0:#,##0}", rows[0]["Area2"]);
                                efwArea3.Text = String.Format("{0:#,##0}", rows[0]["Area3"]);
                                efwArea4.Text = String.Format("{0:#,##0}", rows[0]["Area4"]);
                                efwArea5.Text = String.Format("{0:#,##0}", rows[0]["Area5"]);
                                efwArea6.Text = String.Format("{0:#,##0}", rows[0]["Area6"]);
                                efwArea7.Text = String.Format("{0:#,##0}", rows[0]["Area7"]);
                                efwArea8.Text = String.Format("{0:#,##0}", rows[0]["Area8"]);
                                efwArea9.Text = String.Format("{0:#,##0}", rows[0]["Area9"]);
                                efwArea10.Text = String.Format("{0:#,##0}", rows[0]["Area10"]);
                                efwArea11.Text = String.Format("{0:#,##0}", rows[0]["Area11"]);
                                efwArea12.Text = String.Format("{0:#,##0}", rows[0]["Area12"]);
                                efwArea13.Text = String.Format("{0:#,##0}", rows[0]["Area13"]);
                                efwArea14.Text = String.Format("{0:#,##0}", rows[0]["Area14"]);
                                efwArea15.Text = String.Format("{0:#,##0}", rows[0]["Area15"]);
                                efwArea16.Text = String.Format("{0:#,##0}", rows[0]["Area16"]);
                                efwArea17.Text = String.Format("{0:#,##0}", rows[0]["Area17"]);
                                lbTot.Text = String.Format("{0:#,##0}", rows[0]["Tot"]);
                            }
                        }
                    }
                    con.Close();
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
            for (int i = 0; i < chartControl1.Series.Count; i++)
                this.chartControl1.Series[i].Points.Clear();

            SeriesPoint sPont = null;

            for (int i = 0; i < 17; i++)
            {
                if (i == 0)
                {
                    sPont = new SeriesPoint("서울", Convert.ToInt32(efwArea1.Text.Replace(",", "")));
                }
                else if (i == 1)
                {
                    sPont = new SeriesPoint("부산", Convert.ToInt32(efwArea2.Text.Replace(",", "")));
                }
                else if (i == 2)
                {
                    sPont = new SeriesPoint("인천", Convert.ToInt32(efwArea3.Text.Replace(",", "")));
                }
                else if (i == 3)
                {
                    sPont = new SeriesPoint("광주", Convert.ToInt32(efwArea4.Text.Replace(",", "")));
                }
                else if (i == 4)
                {
                    sPont = new SeriesPoint("대구", Convert.ToInt32(efwArea5.Text.Replace(",", "")));
                }
                else if (i == 5)
                {
                    sPont = new SeriesPoint("대전", Convert.ToInt32(efwArea6.Text.Replace(",", "")));
                }
                else if (i == 6)
                {
                    sPont = new SeriesPoint("울산", Convert.ToInt32(efwArea7.Text.Replace(",", "")));
                }
                else if (i == 7)
                {
                    sPont = new SeriesPoint("세종", Convert.ToInt32(efwArea8.Text.Replace(",", "")));
                }
                else if (i == 8)
                {
                    sPont = new SeriesPoint("제주", Convert.ToInt32(efwArea9.Text.Replace(",", "")));
                }
                else if (i == 9)
                {
                    sPont = new SeriesPoint("경기", Convert.ToInt32(efwArea10.Text.Replace(",", "")));
                }
                else if (i == 10)
                {
                    sPont = new SeriesPoint("강원", Convert.ToInt32(efwArea11.Text.Replace(",", "")));
                }
                else if (i == 11)
                {
                    sPont = new SeriesPoint("경북", Convert.ToInt32(efwArea12.Text.Replace(",", "")));
                }
                else if (i == 12)
                {
                    sPont = new SeriesPoint("경남", Convert.ToInt32(efwArea13.Text.Replace(",", "")));
                }
                else if (i == 13)
                {
                    sPont = new SeriesPoint("전북", Convert.ToInt32(efwArea14.Text.Replace(",", "")));
                }
                else if (i == 14)
                {
                    sPont = new SeriesPoint("전남", Convert.ToInt32(efwArea15.Text.Replace(",", "")));
                }
                else if (i == 15)
                {
                    sPont = new SeriesPoint("충북", Convert.ToInt32(efwArea16.Text.Replace(",", "")));
                }
                else if (i == 16)
                {
                    sPont = new SeriesPoint("충남", Convert.ToInt32(efwArea17.Text.Replace(",", "")));
                }
                this.chartControl1.Series["Series 1"].Points.Add(sPont);
            }


        }

        private void RbYearType_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}
