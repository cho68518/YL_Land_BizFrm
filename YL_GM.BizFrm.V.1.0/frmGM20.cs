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
    public partial class frmGM20 : FrmBase
    {
        public frmGM20()
        {
            InitializeComponent();
            this.QCode = "GM20";
            //폼명설정
            this.FrmName = "제품별 회원등급별 매출현황";
        }

        private void frmGM20_Load(object sender, EventArgs e)
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
            
            dtS_DATE.EditValue = DateTime.Now.ToString("yyyy") + "-01-01";
            dtE_DATE.EditValue = DateTime.Now;
            advBandedGridView1.OptionsView.ShowFooter = true;
            advBandedGridView1.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["p_num"].SummaryItem.FieldName = "p_num";
            advBandedGridView1.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["g_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["g_amt"].SummaryItem.FieldName = "g_amt";
            advBandedGridView1.Columns["g_amt"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["p_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["p_amt"].SummaryItem.FieldName = "p_amt";
            advBandedGridView1.Columns["p_amt"].SummaryItem.DisplayFormat = "{0}";

            //
            advBandedGridView1.Columns["gshop_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["gshop_qty"].SummaryItem.FieldName = "gshop_qty";
            advBandedGridView1.Columns["gshop_qty"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["gshop_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["gshop_amt"].SummaryItem.FieldName = "gshop_amt";
            advBandedGridView1.Columns["gshop_amt"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["gshop_gd"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["gshop_gd"].SummaryItem.FieldName = "gshop_gd";
            advBandedGridView1.Columns["gshop_gd"].SummaryItem.DisplayFormat = "{0}";
            //
            advBandedGridView1.Columns["md_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["md_qty"].SummaryItem.FieldName = "md_qty";
            advBandedGridView1.Columns["md_qty"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["md_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["md_amt"].SummaryItem.FieldName = "md_amt";
            advBandedGridView1.Columns["md_amt"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["md_gd"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["md_gd"].SummaryItem.FieldName = "md_gd";
            advBandedGridView1.Columns["md_gd"].SummaryItem.DisplayFormat = "{0}";
            //
            advBandedGridView1.Columns["biz_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["biz_qty"].SummaryItem.FieldName = "biz_qty";
            advBandedGridView1.Columns["biz_qty"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["biz_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["biz_amt"].SummaryItem.FieldName = "biz_amt";
            advBandedGridView1.Columns["biz_amt"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["biz_gd"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["biz_gd"].SummaryItem.FieldName = "biz_gd";
            advBandedGridView1.Columns["biz_gd"].SummaryItem.DisplayFormat = "{0}";
            //
            advBandedGridView1.Columns["vip_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["vip_qty"].SummaryItem.FieldName = "vip_qty";
            advBandedGridView1.Columns["vip_qty"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["vip_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["vip_amt"].SummaryItem.FieldName = "vip_amt";
            advBandedGridView1.Columns["vip_amt"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["vip_gd"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["vip_gd"].SummaryItem.FieldName = "vip_gd";
            advBandedGridView1.Columns["vip_gd"].SummaryItem.DisplayFormat = "{0}";
            //
            advBandedGridView1.Columns["doma_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["doma_qty"].SummaryItem.FieldName = "doma_qty";
            advBandedGridView1.Columns["doma_qty"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["doma_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["doma_amt"].SummaryItem.FieldName = "doma_amt";
            advBandedGridView1.Columns["doma_amt"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["doma_gd"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["doma_gd"].SummaryItem.FieldName = "doma_gd";
            advBandedGridView1.Columns["doma_gd"].SummaryItem.DisplayFormat = "{0}";
        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM20_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //this.efwGridControl1.MyGridView.BestFitColumns();

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
                    sPont = new SeriesPoint("수량 합계", Convert.ToInt32(advBandedGridView1.Columns["p_num"].SummaryItem.SummaryValue));
                }
                if (i == 1)
                {
                    sPont = new SeriesPoint("금액 합계", Convert.ToInt32(advBandedGridView1.Columns["p_amt"].SummaryItem.SummaryValue));
                }
                else if (i == 2)
                {
                    sPont = new SeriesPoint("G멀티샵 수량", Convert.ToInt32(advBandedGridView1.Columns["gshop_qty"].SummaryItem.SummaryValue));
                }
                else if (i == 3)
                {
                    sPont = new SeriesPoint("G멀티샵 금액", Convert.ToInt32(advBandedGridView1.Columns["gshop_amt"].SummaryItem.SummaryValue));
                }
                else if (i == 4)
                {
                    sPont = new SeriesPoint("MD 수량", Convert.ToInt32(advBandedGridView1.Columns["md_qty"].SummaryItem.SummaryValue));
                }
                else if (i == 5)
                {
                    sPont = new SeriesPoint("MD 금액", Convert.ToInt32(advBandedGridView1.Columns["md_amt"].SummaryItem.SummaryValue));
                }
                else if (i == 6)
                {
                    sPont = new SeriesPoint("BIZ 수량", Convert.ToInt32(advBandedGridView1.Columns["biz_qty"].SummaryItem.SummaryValue));
                }
                else if (i == 7)
                {
                    sPont = new SeriesPoint("BIZ 금액", Convert.ToInt32(advBandedGridView1.Columns["biz_amt"].SummaryItem.SummaryValue));
                }
                else if (i == 8)
                {
                    sPont = new SeriesPoint("VIP 수량", Convert.ToInt32(advBandedGridView1.Columns["vip_qty"].SummaryItem.SummaryValue));
                }
                else if (i == 9)
                {
                    sPont = new SeriesPoint("VIP 금액", Convert.ToInt32(advBandedGridView1.Columns["vip_amt"].SummaryItem.SummaryValue));
                }
                else if (i == 10)
                {
                    sPont = new SeriesPoint("DOMA 수량", Convert.ToInt32(advBandedGridView1.Columns["doma_qty"].SummaryItem.SummaryValue));
                }
                else if (i == 11)
                {
                    sPont = new SeriesPoint("DOMA 금액", Convert.ToInt32(advBandedGridView1.Columns["doma_amt"].SummaryItem.SummaryValue));
                }

                this.chartControl1.Series["Series 1"].Points.Add(sPont);
            }

        }

    }
}
