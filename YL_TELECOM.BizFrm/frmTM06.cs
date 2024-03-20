using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using System.Net;
using DevExpress.XtraGrid.Columns;
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

            dtYear_Month.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtYear_Month.Properties.Mask.EditMask = "yyyy-MM";
            dtYear_Month.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtYear_Month.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtYear_Month.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtYear_Month.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

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
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_tel_type", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = "ALL";

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
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);
                        
                        cmd.Parameters.Add("i_tel_type", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = "lg";

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
            Open3();
        }


        private void Open3()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_yearmonth", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtYear_Month.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_tel_type", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = "kt";

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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
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


        private void advBandedGridView3_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
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

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {

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

        private void gridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView4.GetFocusedDisplayText());
                e.Handled = true;
            }
        }
    }
}
