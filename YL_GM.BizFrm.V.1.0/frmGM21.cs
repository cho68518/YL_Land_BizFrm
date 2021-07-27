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
    public partial class frmGM21 : FrmBase
    {
        public frmGM21()
        {
            InitializeComponent();
            this.QCode = "GM21";
            //폼명설정
            this.FrmName = "전년대비 매출 증감현황";
        }

        private void frmGM21_Load(object sender, EventArgs e)
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
            rbq_type.EditValue = "2";


            advBandedGridView1.OptionsView.ShowFooter = true;

            advBandedGridView1.Columns["1_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["1_bef"].SummaryItem.FieldName = "1_bef";
            advBandedGridView1.Columns["1_bef"].SummaryItem.DisplayFormat = "{0}"; 

            advBandedGridView1.Columns["1_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["1_month"].SummaryItem.FieldName = "1_month";
            advBandedGridView1.Columns["1_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["1_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["1_rate"].SummaryItem.FieldName = "1_rate";
            //advBandedGridView1.Columns["1_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["2_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["2_bef"].SummaryItem.FieldName = "2_bef";
            advBandedGridView1.Columns["2_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["2_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["2_month"].SummaryItem.FieldName = "2_month";
            advBandedGridView1.Columns["2_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["2_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["2_rate"].SummaryItem.FieldName = "2_rate";
            //advBandedGridView1.Columns["2_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["3_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["3_bef"].SummaryItem.FieldName = "3_bef";
            advBandedGridView1.Columns["3_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["3_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["3_month"].SummaryItem.FieldName = "3_month";
            advBandedGridView1.Columns["3_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["3_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["3_rate"].SummaryItem.FieldName = "3_rate";
            //advBandedGridView1.Columns["3_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["4_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["4_bef"].SummaryItem.FieldName = "4_bef";
            advBandedGridView1.Columns["4_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["4_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["4_month"].SummaryItem.FieldName = "4_month";
            advBandedGridView1.Columns["4_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["4_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["4_rate"].SummaryItem.FieldName = "4_rate";
            //advBandedGridView1.Columns["4_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["5_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["5_bef"].SummaryItem.FieldName = "5_bef";
            advBandedGridView1.Columns["5_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["5_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["5_month"].SummaryItem.FieldName = "5_month";
            advBandedGridView1.Columns["5_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["5_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["5_rate"].SummaryItem.FieldName = "5_rate";
            //advBandedGridView1.Columns["5_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["6_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["6_bef"].SummaryItem.FieldName = "6_bef";
            advBandedGridView1.Columns["6_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["6_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["6_month"].SummaryItem.FieldName = "6_month";
            advBandedGridView1.Columns["6_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["6_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["6_rate"].SummaryItem.FieldName = "6_rate";
            //advBandedGridView1.Columns["6_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["7_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["7_bef"].SummaryItem.FieldName = "7_bef";
            advBandedGridView1.Columns["7_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["7_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["7_month"].SummaryItem.FieldName = "7_month";
            advBandedGridView1.Columns["7_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["7_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["7_rate"].SummaryItem.FieldName = "7_rate";
            //advBandedGridView1.Columns["7_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["8_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["8_bef"].SummaryItem.FieldName = "8_bef";
            advBandedGridView1.Columns["8_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["8_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["8_month"].SummaryItem.FieldName = "8_month";
            advBandedGridView1.Columns["8_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["8_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["8_rate"].SummaryItem.FieldName = "8_rate";
            //advBandedGridView1.Columns["8_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["9_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["9_bef"].SummaryItem.FieldName = "9_bef";
            advBandedGridView1.Columns["9_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["9_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["9_month"].SummaryItem.FieldName = "9_month";
            advBandedGridView1.Columns["9_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["9_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["9_rate"].SummaryItem.FieldName = "9_rate";
            //advBandedGridView1.Columns["9_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["10_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["10_bef"].SummaryItem.FieldName = "10_bef";
            advBandedGridView1.Columns["10_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["10_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["10_month"].SummaryItem.FieldName = "10_month";
            advBandedGridView1.Columns["10_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["10_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["10_rate"].SummaryItem.FieldName = "10_rate";
            //advBandedGridView1.Columns["10_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["11_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["11_bef"].SummaryItem.FieldName = "11_bef";
            advBandedGridView1.Columns["11_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["11_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["11_month"].SummaryItem.FieldName = "11_month";
            advBandedGridView1.Columns["11_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["11_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["11_rate"].SummaryItem.FieldName = "11_rate";
            //advBandedGridView1.Columns["11_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["12_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["12_bef"].SummaryItem.FieldName = "12_bef";
            advBandedGridView1.Columns["12_bef"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["12_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["12_month"].SummaryItem.FieldName = "12_month";
            advBandedGridView1.Columns["12_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["12_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["12_rate"].SummaryItem.FieldName = "12_rate";
            //advBandedGridView1.Columns["12_rate"].SummaryItem.DisplayFormat = "{0}";

            advBandedGridView1.Columns["t_bef"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["t_bef"].SummaryItem.FieldName = "t_bef";
            advBandedGridView1.Columns["t_bef"].SummaryItem.DisplayFormat = "합계: {0}";

            advBandedGridView1.Columns["t_month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["t_month"].SummaryItem.FieldName = "t_month";
            advBandedGridView1.Columns["t_month"].SummaryItem.DisplayFormat = "{0}";

            //advBandedGridView1.Columns["t_rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //advBandedGridView1.Columns["t_rate"].SummaryItem.FieldName = "t_rate";
            //advBandedGridView1.Columns["t_rate"].SummaryItem.DisplayFormat = "{0}";
        }

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM21_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);

                        cmd.Parameters.Add("i_Qtype", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbq_type.EditValue;

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

    }
}
