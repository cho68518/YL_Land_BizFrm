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
    public partial class frmGM11_Pop04 : FrmPopUpBase
    {
        public string to_day { get; set; }
        public frmGM11_Pop04()
        {
            InitializeComponent();
        }
        private void frmGM11_Pop04_Load(object sender, EventArgs e)
        {
            dt1F.EditValue = DateTime.Now.ToString(to_day);
            dt1T.EditValue = DateTime.Now.ToString(to_day);

            gridView1.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView1.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_donut_g_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_donut_g_cost"].SummaryItem.FieldName = "o_donut_g_cost";
            gridView1.Columns["o_donut_g_cost"].SummaryItem.DisplayFormat = "{0:c}";

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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM11_POP04_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dt1T.EditValue3;

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


        private void bthNew_Click(object sender, EventArgs e)
        {
            Open1();
        }



    }
}
