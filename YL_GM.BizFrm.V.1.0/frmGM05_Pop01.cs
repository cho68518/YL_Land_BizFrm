using Easy.Framework.Common;
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
using YL_GM.BizFrm.Dlg;

namespace YL_GM.BizFrm
{
    public partial class frmGM05_Pop01 : FrmPopUpBase
    {
        public string pYEAR { get; set; }
        public string pCOM_NM { get; set; }

        public frmGM05_Pop01()
        {
            InitializeComponent();
        }

        private void frmGM05_Pop01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            txtYEAR.EditValue = pYEAR;
            txtCT_NAME.EditValue = pCOM_NM;

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView1.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["o_inputamt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_inputamt"].SummaryItem.FieldName = "o_inputamt";
            gridView1.Columns["o_inputamt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_delivery_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_delivery_cost"].SummaryItem.FieldName = "o_delivery_cost";
            gridView1.Columns["o_delivery_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_total"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_total"].SummaryItem.FieldName = "o_total";
            gridView1.Columns["o_total"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["pp_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["pp_amt"].SummaryItem.FieldName = "pp_amt";
            gridView1.Columns["pp_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["margin"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["margin"].SummaryItem.FieldName = "margin";
            gridView1.Columns["margin"].SummaryItem.DisplayFormat = "{0:c}";

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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM05_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 4);
                        cmd.Parameters[0].Value = txtYEAR.EditValue;

                        cmd.Parameters.Add("i_ct_nm", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtCT_NAME.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //  this.efwGridControl1.MyGridView.BestFitColumns();
                            0
                        }
                    }
                }
                double nInputAmt = 0;
                double nSaleAmt = 0;
                double nMargin = 0;
                double nRate = 0;
                
                nInputAmt = Convert.ToInt32(gridView1.Columns["o_inputamt"].SummaryItem.SummaryValue);
                nSaleAmt = Convert.ToInt32(gridView1.Columns["pp_amt"].SummaryItem.SummaryValue);
                nMargin = nSaleAmt - nInputAmt;
                nRate = ((nSaleAmt - nInputAmt) / nSaleAmt) * 100;
                nRate = Math.Round(nRate, 2);

                txtInputAmt.EditValue = Convert.ToInt32(gridView1.Columns["o_inputamt"].SummaryItem.SummaryValue);
                txtSaleAmt.EditValue = Convert.ToInt32(gridView1.Columns["pp_amt"].SummaryItem.SummaryValue);
                txtMargin.EditValue = nMargin;
                txtRate.EditValue = nRate;

            }


            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

    }
}
