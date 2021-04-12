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
    public partial class frmGM12_Pop01 : FrmPopUpBase
    {
        public string md_u_id { get; set; }
        public string u_nickname { get; set; }
        public DateTime year { get; set; }
        public string prod_type { get; set; }
        public frmGM12_Pop01() 
        {
            InitializeComponent();
        }

        private void frmGM12_Pop01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            txtmd_u_id.Text = md_u_id;
            txtu_nickanme.EditValue = u_nickname;
            dtS_DATE.EditValue = year;
            txtprod_type.EditValue = prod_type;


            gridView1.OptionsView.ShowFooter = true;
            //gridView1.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["p_num"].SummaryItem.FieldName = "p_num";
            //gridView1.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            //gridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            //gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView1.Columns["use_money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["use_money"].SummaryItem.FieldName = "use_money";
            //gridView1.Columns["use_money"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView1.Columns["o_delivery_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["o_delivery_cost"].SummaryItem.FieldName = "o_delivery_cost";
            //gridView1.Columns["o_delivery_cost"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView1.Columns["o_pay_type"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["o_pay_type"].SummaryItem.FieldName = "o_pay_type";
            //gridView1.Columns["o_pay_type"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView1.Columns["totamt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["totamt"].SummaryItem.FieldName = "totamt";
            //gridView1.Columns["totamt"].SummaryItem.DisplayFormat = "{0:c}";
            Open1();
        }
        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM12_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 4);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);

                        cmd.Parameters.Add("i_md_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtmd_u_id.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //  this.efwGridControl1.MyGridView.BestFitColumns();

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
