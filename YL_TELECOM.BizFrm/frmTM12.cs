using DevExpress.XtraEditors.Repository;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using YL_TELECOM.BizFrm.Dlg;
namespace YL_TELECOM.BizFrm
{
    public partial class frmTM12 : FrmBase
    {
        //frmTM12_Pop01 popup;
        public frmTM12()
        {
            InitializeComponent();
            this.QCode = "TM12";
            //폼명설정
            this.FrmName = "대리점별 예치금현황";
        }

        private void frmTM12_Load(object sender, EventArgs e)
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
            this.IsExcel = true;
            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["amount"].SummaryItem.FieldName = "amount";
            gridView1.Columns["amount"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["detail_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["detail_amt"].SummaryItem.FieldName = "detail_amt";
            gridView1.Columns["detail_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["chr_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["chr_amt"].SummaryItem.FieldName = "chr_amt";
            gridView1.Columns["chr_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView2.OptionsView.ShowFooter = true;

            gridView2.Columns["amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["amount"].SummaryItem.FieldName = "amount";
            gridView2.Columns["amount"].SummaryItem.DisplayFormat = "{0:c}";
        }

 

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["agent_id"].ToString() != "")
            {
                this.txtAgent_Id.EditValue = dr["agent_id"].ToString();
                this.txtAgent_NM.EditValue = dr["u_name"].ToString();
            }
            Open1();
        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM12_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtSearch.EditValue;

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
        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM12_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtAgent_Id.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }




        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(gridView1.GetFocusedDisplayText());
            e.Handled = true;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
