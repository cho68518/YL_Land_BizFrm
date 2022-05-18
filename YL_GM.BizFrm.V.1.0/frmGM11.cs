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
    public partial class frmGM11 : FrmBase
    {
        frmGM11_Pop01 popup1;
        frmGM11_Pop02 popup2;
        frmGM11_Pop03 popup3;
        frmGM11_Pop04 popup4;
        frmGM11_Pop05 popup5;
        frmGM11_Pop06 popup6;
        public frmGM11()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmGM11";
            //폼명설정
            this.FrmName = "도넛라이프 일일보고";

        }

        private void frmGM11_Load(object sender, EventArgs e)
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
            
            //this.gridBand5.Columns["1"].Frozen = true;
            //this.bandedGridView1.Columns["to_day"].Frozen = true;

            efwGridControl1.MyGridView.BestFitColumns();
            bandedGridView1.Columns["vip_new_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["vip_new_cnt"].SummaryItem.FieldName = "vip_new_cnt";
            bandedGridView1.Columns["vip_new_cnt"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["vip_new_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["vip_new_amt"].SummaryItem.FieldName = "vip_new_amt";
            bandedGridView1.Columns["vip_new_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["vip_exten_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["vip_exten_cnt"].SummaryItem.FieldName = "vip_exten_cnt";
            bandedGridView1.Columns["vip_exten_cnt"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["vip_exten_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["vip_exten_amt"].SummaryItem.FieldName = "vip_exten_amt";
            bandedGridView1.Columns["vip_exten_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["biz_new_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_new_cnt"].SummaryItem.FieldName = "biz_new_cnt";
            bandedGridView1.Columns["biz_new_cnt"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["biz_new_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_new_amt"].SummaryItem.FieldName = "biz_new_amt";
            bandedGridView1.Columns["biz_new_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["biz_exten_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_exten_cnt"].SummaryItem.FieldName = "biz_exten_cnt";
            bandedGridView1.Columns["biz_exten_cnt"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["biz_exten_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_exten_amt"].SummaryItem.FieldName = "biz_exten_amt";
            bandedGridView1.Columns["biz_exten_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["gshop_new_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gshop_new_cnt"].SummaryItem.FieldName = "gshop_new_cnt";
            bandedGridView1.Columns["gshop_new_cnt"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["gshop_new_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gshop_new_amt"].SummaryItem.FieldName = "gshop_new_amt";
            bandedGridView1.Columns["gshop_new_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["gshop_exten_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gshop_exten_cnt"].SummaryItem.FieldName = "gshop_exten_cnt";
            bandedGridView1.Columns["gshop_exten_cnt"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["gshop_exten_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gshop_exten_amt"].SummaryItem.FieldName = "gshop_exten_amt";
            bandedGridView1.Columns["gshop_exten_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["new_gshop_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["new_gshop_cnt"].SummaryItem.FieldName = "new_gshop_cnt";
            bandedGridView1.Columns["new_gshop_cnt"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView1.Columns["goods_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["goods_qty"].SummaryItem.FieldName = "goods_qty";
            bandedGridView1.Columns["goods_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView1.Columns["goods_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["goods_amt"].SummaryItem.FieldName = "goods_amt";
            bandedGridView1.Columns["goods_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["goods_d_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["goods_d_amt"].SummaryItem.FieldName = "goods_d_amt";
            bandedGridView1.Columns["goods_d_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["goods_s_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["goods_s_amt"].SummaryItem.FieldName = "goods_s_amt";
            bandedGridView1.Columns["goods_s_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["doma_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["doma_qty"].SummaryItem.FieldName = "doma_qty";
            bandedGridView1.Columns["doma_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView1.Columns["doma_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["doma_amt"].SummaryItem.FieldName = "doma_amt";
            bandedGridView1.Columns["doma_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["vip_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["vip_qty"].SummaryItem.FieldName = "vip_qty";
            bandedGridView1.Columns["vip_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["vip_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["vip_amt"].SummaryItem.FieldName = "vip_amt";
            bandedGridView1.Columns["vip_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["biz_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_qty"].SummaryItem.FieldName = "biz_qty";
            bandedGridView1.Columns["biz_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["biz_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_amt"].SummaryItem.FieldName = "biz_amt";
            bandedGridView1.Columns["biz_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["md_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["md_qty"].SummaryItem.FieldName = "md_qty";
            bandedGridView1.Columns["md_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["md_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["md_amt"].SummaryItem.FieldName = "md_amt";
            bandedGridView1.Columns["md_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["gshop_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gshop_qty"].SummaryItem.FieldName = "gshop_qty";
            bandedGridView1.Columns["gshop_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["gshop_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gshop_amt"].SummaryItem.FieldName = "gshop_amt";
            bandedGridView1.Columns["gshop_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["gprod_gd_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gprod_gd_amt"].SummaryItem.FieldName = "gprod_gd_amt";
            bandedGridView1.Columns["gprod_gd_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["tot_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["tot_qty"].SummaryItem.FieldName = "tot_qty";
            bandedGridView1.Columns["tot_qty"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView1.Columns["tot_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["tot_amt"].SummaryItem.FieldName = "tot_amt";
            bandedGridView1.Columns["tot_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["new_gshop_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["new_gshop_cnt"].SummaryItem.FieldName = "new_gshop_cnt";
            bandedGridView1.Columns["new_gshop_cnt"].SummaryItem.DisplayFormat = "{0}";

            bandedGridView1.Columns["cafe_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["cafe_qty"].SummaryItem.FieldName = "cafe_qty";
            bandedGridView1.Columns["cafe_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["cafe_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["cafe_amt"].SummaryItem.FieldName = "cafe_amt";
            bandedGridView1.Columns["cafe_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["open_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["open_qty"].SummaryItem.FieldName = "open_qty";
            bandedGridView1.Columns["open_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["open_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["open_amt"].SummaryItem.FieldName = "open_amt";
            bandedGridView1.Columns["open_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["out_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["out_qty"].SummaryItem.FieldName = "out_qty";
            bandedGridView1.Columns["out_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["out_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["out_amt"].SummaryItem.FieldName = "out_amt";
            bandedGridView1.Columns["out_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["back_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["back_qty"].SummaryItem.FieldName = "back_qty";
            bandedGridView1.Columns["back_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["back_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["back_amt"].SummaryItem.FieldName = "back_amt";
            bandedGridView1.Columns["back_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["naver_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["naver_qty"].SummaryItem.FieldName = "naver_qty";
            bandedGridView1.Columns["naver_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["naver_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["naver_amt"].SummaryItem.FieldName = "naver_amt";
            bandedGridView1.Columns["naver_amt"].SummaryItem.DisplayFormat = "{0:c}"; 

            bandedGridView1.Columns["experience_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["experience_qty"].SummaryItem.FieldName = "experience_qty";
            bandedGridView1.Columns["experience_qty"].SummaryItem.DisplayFormat = "{0}";
            bandedGridView1.Columns["experience_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["experience_amt"].SummaryItem.FieldName = "experience_amt";
            bandedGridView1.Columns["experience_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["doma_gd_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["doma_gd_amt"].SummaryItem.FieldName = "doma_gd_amt";
            bandedGridView1.Columns["doma_gd_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["doma_d_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["doma_d_amt"].SummaryItem.FieldName = "doma_d_amt";
            bandedGridView1.Columns["doma_d_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["biz_gd_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_gd_amt"].SummaryItem.FieldName = "biz_gd_amt";
            bandedGridView1.Columns["biz_gd_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["biz_d_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_d_amt"].SummaryItem.FieldName = "biz_d_amt";
            bandedGridView1.Columns["biz_d_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["shop_gd_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["shop_gd_amt"].SummaryItem.FieldName = "shop_gd_amt";
            bandedGridView1.Columns["shop_gd_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["shop_d_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["shop_d_amt"].SummaryItem.FieldName = "shop_d_amt";
            bandedGridView1.Columns["shop_d_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["tot_gd"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["tot_gd"].SummaryItem.FieldName = "tot_gd";
            bandedGridView1.Columns["tot_gd"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["tot_d"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["tot_d"].SummaryItem.FieldName = "tot_d";
            bandedGridView1.Columns["tot_d"].SummaryItem.DisplayFormat = "{0:c}";
        }

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM11_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        private void BtnDispYes1_Click(object sender, EventArgs e)
        {

            popup1 = new frmGM11_Pop01();

            popup1.to_day = bandedGridView1.GetFocusedRowCellValue("pay_date").ToString();
            popup1.FormClosed += popup1_FormClosed;
            popup1.ShowDialog();
        }
        private void popup1_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup1.FormClosed -= popup1_FormClosed;

            popup1 = null;
        }


        private void BtnDispYes2_Click(object sender, EventArgs e)
        {

            popup2 = new frmGM11_Pop02();

            popup2.to_day = bandedGridView1.GetFocusedRowCellValue("pay_date").ToString();
            popup2.FormClosed += popup2_FormClosed;
            popup2.ShowDialog();
        }
        private void popup2_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup2.FormClosed -= popup2_FormClosed;

            popup2 = null;
        }

        private void BtnDispYes3_Click(object sender, EventArgs e)
        {

            popup3 = new frmGM11_Pop03();

            popup3.to_day = bandedGridView1.GetFocusedRowCellValue("pay_date").ToString();
            popup3.FormClosed += popup3_FormClosed;
            popup3.ShowDialog();
        }
        private void popup3_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup3.FormClosed -= popup3_FormClosed;

            popup3 = null;
        }

        private void BtnDispYes4_Click(object sender, EventArgs e)
        {

            popup4 = new frmGM11_Pop04();

            popup4.to_day = bandedGridView1.GetFocusedRowCellValue("pay_date").ToString();
            popup4.FormClosed += popup4_FormClosed;
            popup4.ShowDialog();
        }
        private void popup4_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup4.FormClosed -= popup4_FormClosed;

            popup4 = null;
        }

        private void BtnDispYes5_Click(object sender, EventArgs e)
        {

            popup5 = new frmGM11_Pop05();

            popup5.to_day = bandedGridView1.GetFocusedRowCellValue("pay_date").ToString();
            popup5.FormClosed += popup5_FormClosed;
            popup5.ShowDialog();
        }
        private void popup5_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup5.FormClosed -= popup5_FormClosed;

            popup5 = null;
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            popup6 = new frmGM11_Pop06();
            popup6.ShowDialog();
        }
    }
}
