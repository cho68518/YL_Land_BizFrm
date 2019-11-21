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

namespace YL_DONUT.BizFrm
{
    public partial class frmDN17 : FrmBase
    {
        public frmDN17()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = " frmDN17";
            //폼명설정
            this.FrmName = "정보트윗 현황관리";
        }

        #region FrmLoadEvent()

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

            //GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_write1");
            //GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_write2");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl2, "Y", "N", "is_open");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl2, "Y", "N", "is_reserve");

            this.dt1.EditValue = DateTime.Now;
            this.dt2.EditValue = DateTime.Now;
            cmbQ1.EditValue = "0";

            gridView1.Columns["t_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["t_cnt"].SummaryItem.FieldName = "t_cnt";
            gridView1.Columns["t_cnt"].SummaryItem.DisplayFormat = "총 트윗건수: {0}";

            this.efwGridControl1.Click += efwGridControl1_Click;
        }

        #endregion

        public override void Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN17_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt1.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt2.EditValue3;

                        cmd.Parameters.Add("i_qtype", MySqlDbType.VarChar, 20);
                        cmd.Parameters[2].Value = this.cmbQ1.EditValue;

                        cmd.Parameters.Add("i_qtext", MySqlDbType.VarChar, 100);
                        cmd.Parameters[3].Value = this.txtSearch.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl1.DataBind(ds);
                            //this.efwGridControl1.MyGridView.BestFitColumns();
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

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN17_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_regdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = dr["reg_date"].ToString().Replace("-","");

                        cmd.Parameters.Add("i_login_id", MySqlDbType.VarChar, 255);
                        cmd.Parameters[1].Value = dr["login_id"].ToString();

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

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
