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
    public partial class frmMM17 : FrmBase
    {
        public frmMM17()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "MM17";
            //폼명설정
            this.FrmName = "문자 전송 현황";
        }
        private void frmMM17_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            rbQType.EditValue = "A";
            dtSDATE.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtSDATE.Properties.Mask.EditMask = "yyyy-MM";
            dtSDATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtSDATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtSDATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtSDATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            chkA.EditValue = "A";
            chkD.EditValue = "N";
            chkV.EditValue = "N";
            chkG.EditValue = "N";
            chkB.EditValue = "N";
            chkS.EditValue = "N";

            if (chkA.EditValue.ToString() == "A")
            {
                this.chkD.Enabled = false;
                this.chkV.Enabled = false;
                this.chkG.Enabled = false;
                this.chkB.Enabled = false;
                this.chkS.Enabled = false;
            }
            else if (chkA.EditValue.ToString() == "N")
            {
                this.chkD.Enabled = true;
                this.chkV.Enabled = true;
                this.chkG.Enabled = true;
                this.chkB.Enabled = true;
                this.chkS.Enabled = true;
            }

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
                string sP_SHOW_TYPE = string.Empty;

                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM17_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtSDATE.EditValue3.Substring(0, 6);

                        string sQuery;
                        if (string.IsNullOrEmpty(this.txtQuery.Text))
                        {
                            sQuery = " ";
                        }
                        else
                        {
                            sQuery = txtQuery.EditValue.ToString();
                        }

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = sQuery;


                        cmd.Parameters.Add("i_QType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = rbQType.EditValue;

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
                string sP_SHOW_TYPE = string.Empty;

                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM17_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtI_SEARCH.EditValue;

                        cmd.Parameters.Add("i_all", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = chkA.EditValue;

                        cmd.Parameters.Add("i_doma", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = chkD.EditValue;

                        cmd.Parameters.Add("i_vip", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = chkV.EditValue;

                        cmd.Parameters.Add("i_gshop", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = chkG.EditValue;

                        cmd.Parameters.Add("i_biz", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = chkB.EditValue;

                        cmd.Parameters.Add("i_stock", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = chkS.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            // this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        private void txtI_SEARCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        private void chkA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkA.EditValue.ToString() == "A")
            {
                this.chkD.Enabled = false;
                this.chkV.Enabled = false;
                this.chkG.Enabled = false;
                this.chkB.Enabled = false;
                this.chkS.Enabled = false;
            }
            else if (chkA.EditValue.ToString() == "N")
            {
                this.chkD.Enabled = true;
                this.chkV.Enabled = true;
                this.chkG.Enabled = true;
                this.chkB.Enabled = true;
                this.chkS.Enabled = true;
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
                gridView2.SetRowCellValue(i, gridView2.Columns["chk"], "Y");
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
                gridView2.SetRowCellValue(i, gridView2.Columns["chk"], "N");
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "문자 전송을 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    var saveResult = new SaveTableResultInfo() { IsError = true };

                    var dt = efwGridControl2.GetChangeDataWithRowState;
                    var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][StatusColumn].ToString() == "U")
                        {
                            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SAVE_02", con))
                                {
                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_phone", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[0].Value = dt.Rows[i]["u_cell_num"].ToString();

                                    cmd.Parameters.Add("i_callback", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[1].Value = "1644-5646";

                                    cmd.Parameters.Add("i_msg", MySqlDbType.VarChar, 3000);
                                    cmd.Parameters[2].Value = txtMessage.EditValue;

                                    string sChk = dt.Rows[i]["chk"].ToString();
                                    if (sChk == "Y" )
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Informational, "문자 전송이 완료되었습니다.");
                }
            }
        }
    }
}
