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
            bandedGridView1.Columns["vip_new_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["vip_new_amt"].SummaryItem.FieldName = "vip_new_amt";
            bandedGridView1.Columns["vip_new_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["vip_exten_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["vip_exten_amt"].SummaryItem.FieldName = "vip_exten_amt";
            bandedGridView1.Columns["vip_exten_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["biz_new_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_new_amt"].SummaryItem.FieldName = "biz_new_amt";
            bandedGridView1.Columns["biz_new_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["biz_exten_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_exten_amt"].SummaryItem.FieldName = "biz_exten_amt";
            bandedGridView1.Columns["biz_exten_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["gshop_new_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gshop_new_amt"].SummaryItem.FieldName = "gshop_new_amt";
            bandedGridView1.Columns["gshop_new_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["gshop_exten_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gshop_exten_amt"].SummaryItem.FieldName = "gshop_exten_amt";
            bandedGridView1.Columns["gshop_exten_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["goods_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["goods_amt"].SummaryItem.FieldName = "goods_amt";
            bandedGridView1.Columns["goods_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["goods_d_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["goods_d_amt"].SummaryItem.FieldName = "goods_d_amt";
            bandedGridView1.Columns["goods_d_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["goods_s_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["goods_s_amt"].SummaryItem.FieldName = "goods_s_amt";
            bandedGridView1.Columns["goods_s_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["doma_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["doma_amt"].SummaryItem.FieldName = "doma_amt";
            bandedGridView1.Columns["doma_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["vip_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["vip_amt"].SummaryItem.FieldName = "vip_amt";
            bandedGridView1.Columns["vip_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["biz_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["biz_amt"].SummaryItem.FieldName = "biz_amt";
            bandedGridView1.Columns["biz_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["md_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["md_amt"].SummaryItem.FieldName = "md_amt";
            bandedGridView1.Columns["md_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["gshop_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gshop_amt"].SummaryItem.FieldName = "gshop_amt";
            bandedGridView1.Columns["gshop_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["gprod_gd_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["gprod_gd_amt"].SummaryItem.FieldName = "gprod_gd_amt";
            bandedGridView1.Columns["gprod_gd_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["tot_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["tot_amt"].SummaryItem.FieldName = "tot_amt";
            bandedGridView1.Columns["tot_amt"].SummaryItem.DisplayFormat = "{0:c}";

            bandedGridView1.Columns["new_gshop_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            bandedGridView1.Columns["new_gshop_cnt"].SummaryItem.FieldName = "new_gshop_cnt";
            bandedGridView1.Columns["new_gshop_cnt"].SummaryItem.DisplayFormat = "{0}";
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

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
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

    }
}
