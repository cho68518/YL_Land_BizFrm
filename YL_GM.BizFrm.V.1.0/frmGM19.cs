using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using YL_GM.BizFrm.Dlg;

namespace YL_GM.BizFrm
{
    public partial class frmGM19 : FrmBase
    {
        public frmGM19()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GM19"; 
            //폼명설정
            this.FrmName = "팀장별 체험샵 등록진행현황";
        }

        private void frmGM19_Load(object sender, EventArgs e)
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

            dt1F.EditValue = DateTime.Now.ToString("2021-04");
            dt1T.EditValue = DateTime.Now.ToString("yyyy-MM");
            rbType.EditValue = 'T';

            gridView2.Columns["visit_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["visit_cnt"].SummaryItem.FieldName = "visit_cnt";
            gridView2.Columns["visit_cnt"].SummaryItem.DisplayFormat = "{0}";

            gridView2.Columns["save_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["save_cnt"].SummaryItem.FieldName = "save_cnt";
            gridView2.Columns["save_cnt"].SummaryItem.DisplayFormat = "{0}";

            gridView2.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView2.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            gridView2.Columns["rate"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gridView2.Columns["rate"].SummaryItem.FieldName = "rate";
            gridView2.Columns["rate"].SummaryItem.DisplayFormat = "{0}";

            gridView2.Columns["amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["amount"].SummaryItem.FieldName = "amount";
            gridView2.Columns["amount"].SummaryItem.DisplayFormat = "{0:c}";


            gridView3.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView3.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            gridView3.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            gridView3.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView3.Columns["o_donut_g_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["o_donut_g_cost"].SummaryItem.FieldName = "o_donut_g_cost";
            gridView3.Columns["o_donut_g_cost"].SummaryItem.DisplayFormat = "{0:c}";


            gridView3.Columns["o_purchase_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["o_purchase_cost"].SummaryItem.FieldName = "o_purchase_cost";
            gridView3.Columns["o_purchase_cost"].SummaryItem.DisplayFormat = "{0:c}";


            gridView1.OptionsView.ShowFooter = true;
            gridView2.OptionsView.ShowFooter = true;
            gridView3.OptionsView.ShowFooter = true;
            this.efwGridControl2.BindControlSet(
                      new ColumnControlSet("u_id", txtU_id)
                      );

            this.efwGridControl2.Click += efwGridControl2_Click;
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            Open1();
        }

        public override void Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sP_SHOW_TYPE = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM19_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt1T.EditValue3;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Open1();
        }

        private void Open1()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM19_SELECT_02", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                            cmd.Parameters[0].Value = txtU_id.EditValue;

                            cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 1);
                            cmd.Parameters[1].Value = rbType.EditValue;

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
                if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM19_SELECT_03", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                            cmd.Parameters[0].Value = txtU_id.EditValue;

                            cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                            cmd.Parameters[1].Value = this.dt1F.EditValue3;

                            cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                            cmd.Parameters[2].Value = this.dt1T.EditValue3;

                            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                            {
                                DataTable ds = new DataTable();
                                sda.Fill(ds);
                                efwGridControl3.DataBind(ds);
                                this.efwGridControl3.MyGridView.BestFitColumns();
                            }

                        }

                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
