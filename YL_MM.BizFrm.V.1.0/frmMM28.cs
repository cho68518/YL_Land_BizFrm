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
using YL_MM.BizFrm.Dlg;


namespace YL_MM.BizFrm
{
    public partial class frmMM28 : FrmBase
    {
        frmMM28_Pop01 popup;
        public frmMM28()
        {
            InitializeComponent();
            this.QCode = "MM28";
            //폼명설정
            this.FrmName = "주식적립 현황";

        }

        private void frmMM28_Load(object sender, EventArgs e)
        {
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            gridView4.OptionsView.ShowFooter = true;

            gridView4.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView4.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView4.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView4.Columns["year_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView4.Columns["year_qty"].SummaryItem.FieldName = "year_qty";
            gridView4.Columns["year_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView4.Columns["take_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView4.Columns["take_qty"].SummaryItem.FieldName = "take_qty";
            gridView4.Columns["take_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView4.Columns["tot_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView4.Columns["tot_qty"].SummaryItem.FieldName = "tot_qty";
            gridView4.Columns["tot_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView4.Columns["bef_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView4.Columns["bef_qty"].SummaryItem.FieldName = "bef_qty";
            gridView4.Columns["bef_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView3.OptionsView.ShowFooter = true;

            gridView3.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView3.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            this.efwGridControl4.BindControlSet(
                      new ColumnControlSet("u_id",txtu_id)
                    , new ColumnControlSet("u_nickname", txtu_nickname)
                   ); ;

            this.efwGridControl4.Click += efwGridControl4_Click;

        }
        private void efwGridControl4_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl4.GetSelectedRow(0);

            if (dr != null && dr["u_id"].ToString() != "")
            {
                this.txtu_id.EditValue = dr["u_id"].ToString();
                this.txtu_nickname.EditValue = dr["u_nickname"].ToString();
                this.txtLogin_id.EditValue = dr["login_id"].ToString();
                Open1();
            }
            // string strQuery = string.Format(@"SELECT idx AS SEQ FROM y2k2.dbo.Y2K2_member where id  = '" + txtLogin_id.EditValue + "' ");
            string strQuery = string.Format(@"SELECT TOP(1) stockPoint AS stockPoint,  CONVERT(CHAR(10), stockRegDate, 23) as stockRegDate  FROM YEOYOU_STOCK.dbo.UT_STOCKOPTION WHERE memberID =  '" + txtLogin_id.EditValue + "' AND  stockCode = 'multi_stock'  ");

            DataSet ds = ServiceAgent.ExecuteDataSetStr("CONIS_IBS", strQuery);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                txtstockPoint.EditValue = ds.Tables[0].Rows[0]["stockPoint"];
                txtstockRegDate.EditValue = ds.Tables[0].Rows[0]["stockRegDate"];
            }

        }

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open2();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open3();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open4();
            }
        }

        public void Open2()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtSearch.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            efwGridControl4.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtu_id.EditValue;

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

        public void Open3()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtSearch.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            efwGridControl2.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public void Open4()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtSearch.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            efwGridControl3.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {

            popup = new frmMM28_Pop01();

            popup.pU_ID = txtu_id.EditValue.ToString(); ;
            popup.pCODE_ID = gridView1.GetFocusedRowCellValue("code_id").ToString();

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }
    }
}
