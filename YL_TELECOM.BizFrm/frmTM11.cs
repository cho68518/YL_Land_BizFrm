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
    public partial class frmTM11 : FrmBase
    {
        //frmTM11_Pop01 popup;
        public frmTM11()
        {
            InitializeComponent();
            this.QCode = "TM11";
            //폼명설정
            this.FrmName = "상품 출고등록";
        }

        private void frmTM11_Load(object sender, EventArgs e)
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

            dtYear_Month.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtYear_Month.Properties.Mask.EditMask = "yyyy-MM";
            dtYear_Month.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtYear_Month.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtYear_Month.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtYear_Month.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            
            this.efwLabel1.Text = "년월";
            this.dtYear_Month.Visible = true;
            this.dtS_DATE.Visible = false;
            this.dtE_DATE.Visible = false;
            efwLabel2.Visible = false;
            efwLabel4.Visible = false;
            this.txtIdx.Visible = false;
            this.txtEntr_No.Visible = false;
            this.btnEntr_No.Visible = false;

            rbG_Prod.EditValue = "S";


            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["month"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month"].SummaryItem.FieldName = "month";
            gridView1.Columns["month"].SummaryItem.DisplayFormat = "합계: {0}";

            gridView1.Columns["month1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month1"].SummaryItem.FieldName = "month1";
            gridView1.Columns["month1"].SummaryItem.DisplayFormat = "익월합계: {0}";

            gridView1.Columns["month2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month2"].SummaryItem.FieldName = "month2";
            gridView1.Columns["month2"].SummaryItem.DisplayFormat = "익월합계: {0}";

            this.efwGridControl1.BindControlSet(
               new ColumnControlSet("opening_store", txtOpening_Store)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;

            this.efwGridControl5.BindControlSet(
              new ColumnControlSet("idx", txtIdx)
            );

        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["opening_store"].ToString() != "")
            {
                this.txtOpening_Store.EditValue = dr["opening_store"].ToString();
            }
            Open1();
            Open2();
            Open3();
        }


        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open5();
            }
        }

        public void Open()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM11_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_q_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbG_Prod.EditValue;

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;


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

        public void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM11_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_q_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbG_Prod.EditValue;

                        cmd.Parameters.Add("i_opening_store", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtOpening_Store.EditValue;


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
        public void Open2()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM11_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_q_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbG_Prod.EditValue;

                        cmd.Parameters.Add("i_opening_store", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtOpening_Store.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();

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
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM11_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_q_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbG_Prod.EditValue;

                        cmd.Parameters.Add("i_opening_store", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtOpening_Store.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            this.efwGridControl4.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        public void Open5()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM11_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
                            this.efwGridControl5.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        private void btnExcelUpdate_Click(object sender, EventArgs e)
        {
           // popup = new frmTM11_Pop01();
           // popup.ShowDialog();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.efwLabel1.Text = "년월";
                this.dtYear_Month.Visible = true;
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                efwLabel2.Visible = false;
                
                efwLabel4.Visible = false;
                this.txtIdx.Visible = false;
                this.txtEntr_No.Visible = false;
                this.btnEntr_No.Visible = false;

            }

            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                this.efwLabel1.Text = "일자";
                this.dtYear_Month.Visible = false;
                this.dtS_DATE.Visible = true;
                this.dtE_DATE.Visible = true;
                efwLabel2.Visible = true;
                efwLabel4.Visible = true;
                this.txtIdx.Visible = false;
                this.txtEntr_No.Visible = true;
                this.btnEntr_No.Visible = true;

            }

        }

        private void gridView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView5.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView2.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView3.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView4.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView5.RowCount; i++)
                gridView5.SetRowCellValue(i, gridView5.Columns["chk"], "Y");
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView5.RowCount; i++)
                gridView5.SetRowCellValue(i, gridView5.Columns["chk"], "N");
        }

        private void btnEntr_No_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {

                    for (int i = 0; i < gridView5.DataRowCount; i++)
                    {
                        if (gridView5.GetRowCellValue(i, gridView5.Columns[0]).ToString() == "Y")
                        {
                            using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM11_SAVE_01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;


                                cmd.Parameters.Add("i_entr_no", MySqlDbType.VarChar, 14);
                                cmd.Parameters[0].Value = txtEntr_No.EditValue;

                                cmd.Parameters.Add("i_idx", MySqlDbType.Int32);
                                cmd.Parameters[1].Value = Convert.ToInt32(gridView5.GetRowCellValue(i, gridView5.Columns[1])).ToString();

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Open5();
        }
    }
}
