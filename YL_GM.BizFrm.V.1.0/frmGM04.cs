#region "frmGM04 설명"
//===================================================================================================
//■Program Name  : frmGM04
//■Description   : 월별매출현황
//■Author        : 송호철
//■Date          : 2019.07.22
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.07.22][송호철] Base
//[2] [2019.07.22][송호철] 
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
    public partial class frmGM04 : FrmBase
    {
        frmGM04_Pop01 popup;
        public frmGM04()
        {
            InitializeComponent();
            this.QCode = "GM04";
            //폼명설정
            this.FrmName = "월별 매출현황";
        }

        public override void FrmLoadEvent()
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
            dfDate.EditValue = DateTime.Now;
            rbq_type.EditValue = "1";
            rbAmt_type.EditValue = "1";

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




            advBandedGridView1.Columns["date1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date1"].SummaryItem.FieldName = "date1";
            advBandedGridView1.Columns["date1"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["date2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date2"].SummaryItem.FieldName = "date2";
            advBandedGridView1.Columns["date2"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date3"].SummaryItem.FieldName = "date3";
            advBandedGridView1.Columns["date3"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date4"].SummaryItem.FieldName = "date4";
            advBandedGridView1.Columns["date4"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date5"].SummaryItem.FieldName = "date5";
            advBandedGridView1.Columns["date5"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date6"].SummaryItem.FieldName = "date6";
            advBandedGridView1.Columns["date6"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date7"].SummaryItem.FieldName = "date7";
            advBandedGridView1.Columns["date7"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date8"].SummaryItem.FieldName = "date8";
            advBandedGridView1.Columns["date8"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date9"].SummaryItem.FieldName = "date9";
            advBandedGridView1.Columns["date9"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date10"].SummaryItem.FieldName = "date10";
            advBandedGridView1.Columns["date10"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date11"].SummaryItem.FieldName = "date11";
            advBandedGridView1.Columns["date11"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date12"].SummaryItem.FieldName = "date12";
            advBandedGridView1.Columns["date12"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["date_tot"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["date_tot"].SummaryItem.FieldName = "date_tot";
            advBandedGridView1.Columns["date_tot"].SummaryItem.DisplayFormat = "{0:c}";

        }

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    if (rbq_type.EditValue == "1")
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM04_SELECT_01", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 4);
                            cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);

                            cmd.Parameters.Add("i_date", MySqlDbType.VarChar, 2);
                            cmd.Parameters[1].Value = dfDate.EditValue3.Substring(6, 2);

                            cmd.Parameters.Add("i_amt_type", MySqlDbType.VarChar, 1);
                            cmd.Parameters[2].Value = rbAmt_type.EditValue;

                            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                            {
                                DataTable ds = new DataTable();
                                sda.Fill(ds);
                                efwGridControl1.DataBind(ds);
                                //    this.efwGridControl1.MyGridView.BestFitColumns();

                            }
                        }
                    }
                    else
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM04_SELECT_03", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 4);
                            cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);

                            cmd.Parameters.Add("i_date", MySqlDbType.VarChar, 2);
                            cmd.Parameters[1].Value = dfDate.EditValue3.Substring(6, 2);

                            cmd.Parameters.Add("i_amt_type", MySqlDbType.VarChar, 1);
                            cmd.Parameters[2].Value = rbAmt_type.EditValue;

                            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                            {
                                DataTable ds = new DataTable();
                                sda.Fill(ds);
                                efwGridControl1.DataBind(ds);
                                //    this.efwGridControl1.MyGridView.BestFitColumns();

                            }
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
            for (int i = 0; i < chartControl1.Series.Count; i++)
                this.chartControl1.Series[i].Points.Clear();

            SeriesPoint sPont = null;

            //시리즈 포인트
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    sPont = new SeriesPoint("1", Convert.ToInt32(advBandedGridView1.Columns["month1"].SummaryItem.SummaryValue));
                }
                else if (i == 1)
                {
                    sPont = new SeriesPoint("2", Convert.ToInt32(advBandedGridView1.Columns["month2"].SummaryItem.SummaryValue));
                }
                else if (i == 2)
                {
                    sPont = new SeriesPoint("3", Convert.ToInt32(advBandedGridView1.Columns["month3"].SummaryItem.SummaryValue));
                }
                else if (i == 3)
                {
                    sPont = new SeriesPoint("4", Convert.ToInt32(advBandedGridView1.Columns["month4"].SummaryItem.SummaryValue));
                }
                else if (i == 4)
                {
                    sPont = new SeriesPoint("5", Convert.ToInt32(advBandedGridView1.Columns["month5"].SummaryItem.SummaryValue));
                }
                else if (i == 5)
                {
                    sPont = new SeriesPoint("6", Convert.ToInt32(advBandedGridView1.Columns["month6"].SummaryItem.SummaryValue));
                }
                else if (i == 6)
                {
                    sPont = new SeriesPoint("7", Convert.ToInt32(advBandedGridView1.Columns["month7"].SummaryItem.SummaryValue));
                }
                else if (i == 7)
                {
                    sPont = new SeriesPoint("8", Convert.ToInt32(advBandedGridView1.Columns["month8"].SummaryItem.SummaryValue));
                }
                else if (i == 8)
                {
                    sPont = new SeriesPoint("9", Convert.ToInt32(advBandedGridView1.Columns["month9"].SummaryItem.SummaryValue));
                }
                else if (i == 9)
                {
                    sPont = new SeriesPoint("10", Convert.ToInt32(advBandedGridView1.Columns["month10"].SummaryItem.SummaryValue));
                }
                else if (i == 10)
                {
                    sPont = new SeriesPoint("11", Convert.ToInt32(advBandedGridView1.Columns["month11"].SummaryItem.SummaryValue));
                }
                else if (i == 11)
                {
                    sPont = new SeriesPoint("12", Convert.ToInt32(advBandedGridView1.Columns["month12"].SummaryItem.SummaryValue));
                }
                this.chartControl1.Series["Series 1"].Points.Add(sPont);
            }

        }


        private void BtnDispYes_Click(object sender, EventArgs e)
        {

            popup = new frmGM04_Pop01();

            popup.pYEAR = dtS_DATE.EditValue3.Substring(0, 4);
            popup.pCT_CODE = advBandedGridView1.GetFocusedRowCellValue("p_ct_cd").ToString();
            popup.pCT_NM = advBandedGridView1.GetFocusedRowCellValue("p_ct_nm").ToString();
            popup.pLevel = advBandedGridView1.GetFocusedRowCellValue("level").ToString();

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }


        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }
    }

}

