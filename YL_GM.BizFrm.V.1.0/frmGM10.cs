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
            rbShop_Type.EditValue = "";
            rbProd_Type.EditValue = "1";
            rbQtyOrAmt.EditValue = "1";
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












        }


        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

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
            }
        }

    }
}
