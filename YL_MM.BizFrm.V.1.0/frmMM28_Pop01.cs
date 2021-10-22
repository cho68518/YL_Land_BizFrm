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
using YL_MM.BizFrm.Dlg;

namespace YL_MM.BizFrm
{
    public partial class frmMM28_Pop01 : FrmPopUpBase
    {
        public string pU_ID { get; set; }
        public string pCODE_ID { get; set; }

        public frmMM28_Pop01()
        {
            InitializeComponent();
        }

        private void frmMM28_Pop01_Load(object sender, EventArgs e)
        {
            //base.FrmLoadEvent();
            //DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            txtu_id.EditValue = pU_ID;
            txtCode_id.EditValue = pCODE_ID;

            gridView1.OptionsView.ShowFooter = true;
            gridView2.OptionsView.ShowFooter = true;
            gridView3.OptionsView.ShowFooter = true;
            gridView4.OptionsView.ShowFooter = true;
            gridView5.OptionsView.ShowFooter = true;
            gridView6.OptionsView.ShowFooter = true;
            gridView7.OptionsView.ShowFooter = true;
            gridView8.OptionsView.ShowFooter = true;
            gridView9.OptionsView.ShowFooter = true;
            gridView10.OptionsView.ShowFooter = true;

            gridView1.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView1.Columns["p_num"].SummaryItem.DisplayFormat = "{0}"; 

            gridView1.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView1.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView2.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView2.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView3.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView3.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView4.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView4.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView4.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView5.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView5.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView6.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView6.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            gridView6.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView6.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView6.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView7.Columns["youtube_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView7.Columns["youtube_qty"].SummaryItem.FieldName = "youtube_qty";
            gridView7.Columns["youtube_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView8.Columns["md_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView8.Columns["md_qty"].SummaryItem.FieldName = "md_qty";
            gridView8.Columns["md_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView9.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView9.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView9.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView10.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView10.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView10.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            gridView11.Columns["stock_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView11.Columns["stock_qty"].SummaryItem.FieldName = "stock_qty";
            gridView11.Columns["stock_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView12.Columns["ceo_pic_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView12.Columns["ceo_pic_qty"].SummaryItem.FieldName = "ceo_pic_qty";
            gridView12.Columns["ceo_pic_qty"].SummaryItem.DisplayFormat = "{0}";

            // 시작시 텝 1위치 지정
            if (txtCode_id.EditValue.ToString() == "1")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage1;
            }
            else if (txtCode_id.EditValue.ToString() == "2")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage2;
            }
            else if (txtCode_id.EditValue.ToString() == "3")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage3;
            }
            else if (txtCode_id.EditValue.ToString() == "4")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage4;
            }
            else if (txtCode_id.EditValue.ToString() == "5")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage5;
            }
            else if (txtCode_id.EditValue.ToString() == "6")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage6;
            }
            else if (txtCode_id.EditValue.ToString() == "7")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage7;
            }
            else if (txtCode_id.EditValue.ToString() == "8")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage8;
            }
            else if (txtCode_id.EditValue.ToString() == "9")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage9;
            }
            else if (txtCode_id.EditValue.ToString() == "10")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage10;
            }
            else if (txtCode_id.EditValue.ToString() == "11")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage11;
            }
            else if (txtCode_id.EditValue.ToString() == "12")
            {
                efwXtraTabControl1.SelectedTabPage = xtraTabPage12;
            }
            Search();
        }

        public  void Search()
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
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                Open5();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage6)
            {
                Open6();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage7)
            {
                Open7();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage8)
            {
                Open8();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage9)
            {
                Open9();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage10)
            {
                Open10();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage11)
            {
                Open11();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage12)
            {
                Open12();
            }
        }

        private void Open1()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            this.efwGridControl4.MyGridView.BestFitColumns();
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
        private void Open5()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
                            this.efwGridControl5.MyGridView.BestFitColumns();
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
        private void Open6()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_06", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl6.DataBind(ds);
                            this.efwGridControl6.MyGridView.BestFitColumns();
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
        private void Open7()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_07", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl7.DataBind(ds);
                            this.efwGridControl7.MyGridView.BestFitColumns();
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
        private void Open8()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_08", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl8.DataBind(ds);
                            this.efwGridControl8.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open9()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_09", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl9.DataBind(ds);
                            this.efwGridControl9.MyGridView.BestFitColumns();
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
        private void Open10()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_10", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl10.DataBind(ds);
                            this.efwGridControl10.MyGridView.BestFitColumns();
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
        private void Open11()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_11", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl11.DataBind(ds);
                            this.efwGridControl11.MyGridView.BestFitColumns();
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
        private void Open12()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM28_POP_SELECT_12", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtu_id.EditValue;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl12.DataBind(ds);
                            this.efwGridControl12.MyGridView.BestFitColumns();
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
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0 )
            {
                e.Info.DisplayText = e.RowHandle.ToString();
            }
        }
    }
}
