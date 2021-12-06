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
            this.FrmName = "상품 수불 현황";
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
            gridView1.Columns["bef_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["bef_qty"].SummaryItem.FieldName = "bef_qty";
            gridView1.Columns["bef_qty"].SummaryItem.DisplayFormat = "전월수량:{0}";

            gridView1.Columns["bef_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["bef_amt"].SummaryItem.FieldName = "bef_amt";
            gridView1.Columns["bef_amt"].SummaryItem.DisplayFormat = "전월금액: {0:c}";

            gridView1.Columns["in_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["in_qty"].SummaryItem.FieldName = "in_qty";
            gridView1.Columns["in_qty"].SummaryItem.DisplayFormat = "입고수량: {0}";

            gridView1.Columns["in_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["in_amt"].SummaryItem.FieldName = "in_amt";
            gridView1.Columns["in_amt"].SummaryItem.DisplayFormat = "입고금액: {0:c}";

            gridView1.Columns["out_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["out_qty"].SummaryItem.FieldName = "out_qty";
            gridView1.Columns["out_qty"].SummaryItem.DisplayFormat = "출고수량: {0}";

            gridView1.Columns["out_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["out_amt"].SummaryItem.FieldName = "out_amt";
            gridView1.Columns["out_amt"].SummaryItem.DisplayFormat = "출고금액: {0:c}";

            gridView1.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView1.Columns["stock_qty"].SummaryItem.DisplayFormat = "재고수량: {0}";

            gridView1.Columns["stock_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["stock_amt"].SummaryItem.FieldName = "stock_amt";
            gridView1.Columns["stock_amt"].SummaryItem.DisplayFormat = "재고 금액: {0:c}";

            dtS_DATE.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtS_DATE.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            btnFactory.EditValue = "4099";
            txtFactory_NM.EditValue = "여유텔레콤(본사창고)";
            rbType.EditValue = "1";
        }

 

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["ser_no"].ToString() != "")
            {
                this.txtser_no.EditValue = dr["ser_no"].ToString();
            }

        }

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();
            }
        }


        private void Open1()
        {
            Save01();
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM12_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_factory", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = btnFactory.EditValue;

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;

                        cmd.Parameters.Add("i_Type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbType.EditValue;


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

        private void Open2()
        {
            Save01();
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM12_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_factory", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = btnFactory.EditValue;

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;

                        cmd.Parameters.Add("i_Type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbType.EditValue;

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


        private void Save01()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM12_SAVE_01", con))
                    {

                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_YearMonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            Save01();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void btnFactory_Click(object sender, EventArgs e)
        {
            frmFactory FrmInfo = new frmFactory() { Factory = btnFactory, Factory_NM = txtFactory_NM };
            FrmInfo.ShowDialog();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            //popup = new frmTM12_Pop01();


            //popup.m_code = gridView1.GetFocusedRowCellValue("m_code").ToString();
            //popup.ser_no = gridView1.GetFocusedRowCellValue("ser_no").ToString();

            //popup.FormClosed += popup_FormClosed;
            //popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            //popup.FormClosed -= popup_FormClosed;

            //popup = null;
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(gridView1.GetFocusedDisplayText());
            e.Handled = true;
        }
    }
}
