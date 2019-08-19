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
    public partial class frmDN08 : FrmBase
    {
        public frmDN08()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "DN08";
            //폼명설정
            this.FrmName = "스토리뱅크 현황";

            //gridView1.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            ////gridView1.OptionsBehavior.Editable = false;
            //gridView1.OptionsBehavior.ReadOnly = true;
            //btnDispYes.ButtonClick += btnDispYes_ButtonClick;
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

            //gridView1.OptionsView.ShowFooter = true;
            //gridView1.Columns["donut_count"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["donut_count"].SummaryItem.FieldName = "donut_count";
            //gridView1.Columns["donut_count"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView2.OptionsView.ShowFooter = true;
            //gridView2.Columns["RECV_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView2.Columns["RECV_AMOUNT"].SummaryItem.FieldName = "RECV_AMOUNT";
            //gridView2.Columns["RECV_AMOUNT"].SummaryItem.DisplayFormat = "사용머니: {0:c}";

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_write2");

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["donut_count"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["donut_count"].SummaryItem.FieldName = "donut_count";
            gridView1.Columns["donut_count"].SummaryItem.DisplayFormat = "{0:c}";

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt1T.EditValue = DateTime.Now;
            cmbQ1.EditValue = "1";
            cmbWriteYn.EditValue = "%";
            setCmb();
            chkCmb_Story.CheckAll();
        }

        #endregion

        private void setCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT category_no as DCODE ,story_name as DNAME  FROM domaadmin.tb_story_info ORDER BY id ";

                DataSet ds = con.selectQueryDataSet();
                ////DataTable retDT = ds.Tables[0];
                //DataRow[] dr = ds.Tables[0].Select();
                //CodeData[] codeArray = new CodeData[dr.Length];

                //// cmbTAREA1.EditValue = "";
                //// cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                //for (int i = 0; i < dr.Length; i++)
                //    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                //CodeAgent.MakeCodeControl(this.cmbTAREA1, codeArray);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkCmb_Story.SelectedText = "전체선택";
                    chkCmb_Story.Properties.DisplayMember = "DNAME";
                    chkCmb_Story.Properties.ValueMember = "DCODE";
                    chkCmb_Story.Properties.DataSource = ds.Tables[0];

                }
            }
        }

        public override void Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sP_SHOW_TYPE = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN08_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt1T.EditValue3;

                        cmd.Parameters.Add("i_writeyn", MySqlDbType.VarChar, 2);
                        cmd.Parameters[2].Value = this.cmbWriteYn.EditValue;

                        cmd.Parameters.Add("i_qtype", MySqlDbType.VarChar, 2);
                        cmd.Parameters[3].Value = this.cmbQ1.EditValue;

                        cmd.Parameters.Add("i_qtext", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = this.txtSearch.EditValue;

                        cmd.Parameters.Add("i_qstory", MySqlDbType.VarChar, 1000);
                        cmd.Parameters[5].Value = this.chkCmb_Story.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
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
