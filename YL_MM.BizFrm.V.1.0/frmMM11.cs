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

namespace YL_MM.BizFrm
{
    public partial class frmMM11 : FrmBase
    {
        public frmMM11()
        {
            InitializeComponent();
            this.QCode = "MM11";
            //폼명설정
            this.FrmName = "회원별 결재현황";
        }

        public override void FrmLoadEvent()
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

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt1T.EditValue = DateTime.Now;
            //dtEDate.EditValue = DateTime.Now.AddDays(7);

            //dtSDate.EditValue = DateTime.Now;
            cmbQ1.EditValue = "1";
            cmbQ2.EditValue = "0";
            
            chkT.EditValue = "N";
            chkO.EditValue = "O";
            chkI.EditValue = "N";
            chkC.EditValue = "N";
            chkZ.EditValue = "N";
            gridView1.OptionsView.ShowFooter = true;
            gridView2.OptionsView.ShowFooter = true;
            gridView3.OptionsView.ShowFooter = true;

            gridView1.Columns["lgd_amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["lgd_amount"].SummaryItem.FieldName = "lgd_amount";
            gridView1.Columns["lgd_amount"].SummaryItem.DisplayFormat = "{0:c}";

            gridView2.Columns["lgd_amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["lgd_amount"].SummaryItem.FieldName = "lgd_amount";
            gridView2.Columns["lgd_amount"].SummaryItem.DisplayFormat = "{0:c}";

            gridView3.Columns["lgd_amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["lgd_amount"].SummaryItem.FieldName = "lgd_amount";
            gridView3.Columns["lgd_amount"].SummaryItem.DisplayFormat = "{0:c}";



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
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open4();
            }
        }
        private void Open1()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM11_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_qtype1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_qtype2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = cmbQ2.EditValue;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = dt1T.EditValue3;

                        cmd.Parameters.Add("i_txt", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtSearch.Text;

                        cmd.Parameters.Add("i_o_type1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = chkT.EditValue;

                        cmd.Parameters.Add("i_o_type2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = chkO.EditValue;

                        cmd.Parameters.Add("i_o_type3", MySqlDbType.VarChar, 10);
                        cmd.Parameters[7].Value = chkI.EditValue;

                        cmd.Parameters.Add("i_o_type4", MySqlDbType.VarChar, 10);
                        cmd.Parameters[8].Value = chkC.EditValue;

                        cmd.Parameters.Add("i_o_type5", MySqlDbType.VarChar, 10);
                        cmd.Parameters[9].Value = chkZ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
                //lbCount.Text = String.Format("{0:#,##0}", Convert.ToInt32(gridView1.RowCount));
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open2()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM11_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_qtype1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_qtype2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = cmbQ2.EditValue;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = dt1T.EditValue3;

                        cmd.Parameters.Add("i_txt", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtSearch.Text;

                        cmd.Parameters.Add("i_o_type1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = chkT.EditValue;

                        cmd.Parameters.Add("i_o_type2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = chkO.EditValue;

                        cmd.Parameters.Add("i_o_type3", MySqlDbType.VarChar, 10);
                        cmd.Parameters[7].Value = chkI.EditValue;

                        cmd.Parameters.Add("i_o_type4", MySqlDbType.VarChar, 10);
                        cmd.Parameters[8].Value = chkC.EditValue;

                        cmd.Parameters.Add("i_o_type5", MySqlDbType.VarChar, 10);
                        cmd.Parameters[9].Value = chkZ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
                        }
                    }
                }
                //lbCount.Text = String.Format("{0:#,##0}", Convert.ToInt32(gridView1.RowCount));
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open3()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM11_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_qtype1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_qtype2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = cmbQ2.EditValue;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = dt1T.EditValue3;

                        cmd.Parameters.Add("i_txt", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtSearch.Text;

                        cmd.Parameters.Add("i_o_type1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = chkT.EditValue;

                        cmd.Parameters.Add("i_o_type2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = chkO.EditValue;

                        cmd.Parameters.Add("i_o_type3", MySqlDbType.VarChar, 10);
                        cmd.Parameters[7].Value = chkI.EditValue;

                        cmd.Parameters.Add("i_o_type4", MySqlDbType.VarChar, 10);
                        cmd.Parameters[8].Value = chkC.EditValue;

                        cmd.Parameters.Add("i_o_type5", MySqlDbType.VarChar, 10);
                        cmd.Parameters[9].Value = chkZ.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();
                        }
                    }
                }
                //lbCount.Text = String.Format("{0:#,##0}", Convert.ToInt32(gridView1.RowCount));
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open4()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM11_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_qtype1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_qtype2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = cmbQ2.EditValue;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = dt1T.EditValue3;

                        cmd.Parameters.Add("i_txt", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtSearch.Text;

                        cmd.Parameters.Add("i_o_type1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = chkT.EditValue;

                        cmd.Parameters.Add("i_o_type2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = chkO.EditValue;

                        cmd.Parameters.Add("i_o_type3", MySqlDbType.VarChar, 10);
                        cmd.Parameters[7].Value = chkI.EditValue;

                        cmd.Parameters.Add("i_o_type4", MySqlDbType.VarChar, 10);
                        cmd.Parameters[8].Value = chkC.EditValue;

                        cmd.Parameters.Add("i_o_type5", MySqlDbType.VarChar, 10);
                        cmd.Parameters[9].Value = chkZ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
                        }
                    }
                }
                //lbCount.Text = String.Format("{0:#,##0}", Convert.ToInt32(gridView1.RowCount));
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }


        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSearch.Focus();
        }
    }
}
