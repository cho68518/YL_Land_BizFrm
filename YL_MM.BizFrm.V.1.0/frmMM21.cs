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

namespace YL_MM.BizFrm
{
    public partial class frmMM21 : FrmBase
    {
        public frmMM21()
        {
            InitializeComponent();
            this.QCode = "MM21";
            //폼명설정
            this.FrmName = "회원별 가입현황";
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
            rbIs_extend.EditValue = "T";

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
            //else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            //{
            //    Open4();
            //}
        }

        private void Open1()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM21_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = dt1T.EditValue3;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.Text;

                        cmd.Parameters.Add("i_is_extend", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbIs_extend.EditValue;

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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM21_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = dt1T.EditValue3;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.Text;

                        cmd.Parameters.Add("i_is_extend", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbIs_extend.EditValue;

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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM21_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = dt1T.EditValue3;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.Text;

                        cmd.Parameters.Add("i_is_extend", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbIs_extend.EditValue;

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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
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

        private void efwGridControl3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView3.GetFocusedDisplayText());
                e.Handled = true;
            }
        }
    }
}
