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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;



namespace YL_TELECOM.BizFrm
{
    public partial class frmTM06 : FrmBase
    {
        public frmTM06()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "TM06";
            //폼명설정
            this.FrmName = "통신사 로그대비 매칭현황";
        }

        private void frmTM06_Load(object sender, EventArgs e)
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

            dt_s_date.EditValue = DateTime.Now;
            dt_e_date.EditValue = DateTime.Now;
            rb_Search_type.EditValue = "T";
            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            advBandedGridView1.OptionsView.ShowFooter = true;

            DataTable table = new DataTable();
            this.efwGridControl1.DataSource = table;
            (this.efwGridControl1.MainView as GridView).RowCellStyle += advBandedGridView1_RowCellStyle;

            //DataTable table1 = new DataTable();
            //this.efwGridControl2.DataSource = table1;
            //(this.efwGridControl2.MainView as GridView).RowCellStyle += advBandedGridView2_RowCellStyle;

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
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("telecom.USP_TM_TM06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dt_s_date.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dt_e_date.EditValue3;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = rb_Search_type.EditValue;

                        cmd.Parameters.Add("i_cust_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtSearch.EditValue;

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
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("telecom.USP_TM_TM06_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dt_s_date.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dt_e_date.EditValue3;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = rb_Search_type.EditValue;

                        cmd.Parameters.Add("i_cust_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtSearch.EditValue;

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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void advBandedGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(advBandedGridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }


        private void advBandedGridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    DevExpress.XtraGrid.Views.Grid.GridView View = sender as GridView;
            //    string err = View.GetRowCellDisplayText(e.RowHandle, View.Columns["check_memo"]);
            //    if (err != "Y")
            //    {
            //        e.Appearance.BackColor = Color.YellowGreen;
            //        e.Appearance.BackColor2 = Color.White;
            //    }
            //}
            //---------------------------------------------------------------------------
            GridView view = sender as GridView;
            if (view == null)
                return;

            if (e.RowHandle != view.FocusedRowHandle)
            {
                DevExpress.XtraGrid.Views.Grid.GridView View = sender as GridView;
                string err = View.GetRowCellDisplayText(e.RowHandle, View.Columns["check_memo"]);
                if (err != "Y")//Cell의 값이 APPLE인 경우 Cell색 변경
                {
                    e.Appearance.BackColor = Color.YellowGreen;
                    e.Appearance.BackColor2 = Color.White; //그라데이션 처리
                }
            }

        }

        private void advBandedGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(advBandedGridView2.GetFocusedDisplayText());
                e.Handled = true;
            }
        }
    }
}
