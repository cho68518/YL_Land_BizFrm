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
using YL_MM.BizFrm.Dlg;

namespace YL_MM.BizFrm
{
    public partial class frmMM11 : FrmBase
    {
        frmMemberInfo popup;
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

            this.efwGridControl1.BindControlSet(
                new ColumnControlSet("o_code", txtO_Code)
              , new ColumnControlSet("u_nickname", txtU_Nickname)
              , new ColumnControlSet("u_name", txtU_Name)
              , new ColumnControlSet("login_id", txtLogin_ID)
              , new ColumnControlSet("o_deposit_confirm_date", txtConfirm_Date)
       //       , new ColumnControlSet("o_cancel_date", dtO_Cancel_Date)
            ); 

            this.efwGridControl1.Click += efwGridControl1_Click;

            this.efwGridControl2.BindControlSet(
                new ColumnControlSet("member_relations", txtMember_Relations)
              , new ColumnControlSet("chef_relations", txtChef_Relations)
              , new ColumnControlSet("md_leader", txtMd_Leader)
              , new ColumnControlSet("member_u_id", txtMember_u_id)
              , new ColumnControlSet("chef_u_id", txtChef_u_id)
              , new ColumnControlSet("md_u_id", txtMd_u_id)
              , new ColumnControlSet("account_number", txtAccount_Number)
              , new ColumnControlSet("o_u_id", txtU_id)
              , new ColumnControlSet("u_nickname", txtNickname)
              , new ColumnControlSet("o_code",txtPS_O_Code)
            );
            this.efwGridControl2.Click += efwGridControl2_Click;

        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["o_cancel_date"].ToString() != "")
            {
                this.dtO_Cancel_Date.EditValue = dr["o_cancel_date"].ToString();
            }
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);

            if (dr != null && dr["o_u_id"].ToString() != "")
            {
                this.txtMember_Relations.EditValue = dr["member_relations"].ToString();
                this.txtChef_Relations.EditValue = dr["chef_relations"].ToString();
                this.txtMd_Leader.EditValue = dr["md_leader"].ToString();
                this.txtMember_u_id.EditValue = dr["member_u_id"].ToString();
                this.txtChef_u_id.EditValue = dr["chef_u_id"].ToString();
                this.txtMd_u_id.EditValue = dr["md_u_id"].ToString();
                this.txtAccount_Number.EditValue = dr["account_number"].ToString();
                this.txtNickname.EditValue = dr["u_nickname"].ToString();
                this.txtU_id.EditValue = dr["o_u_id"].ToString();
                this.txtPS_O_Code.EditValue = dr["o_code"].ToString();
            }
        }

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {   // 2021-08-30 BIZ 회원을 헬퍼로 변경
                Open2();
                Open5();
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

        private void Open5()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM11_SELECT_05", con))
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
                Search();
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.dtO_Cancel_Date.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "멤버쉽 결제를 취소 하시겠습니다?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM11_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_cancel_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_cancel_date"].Value = dtO_Cancel_Date.EditValue;
                            cmd.Parameters["i_cancel_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
            }
        }

        private void btnSAVE_Click(object sender, EventArgs e)
        {

        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MTYPE = "0"
            };
            popup.FormClosed += popup_FormClosed1;
            popup.ShowDialog();
        }
        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtChef_Relations.EditValue = popup.U_NICKNAME;
                this.txtChef_u_id.EditValue = popup.U_ID;
            }
            popup = null;
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MTYPE = "0"
            };
            popup.FormClosed += popup_FormClosed2;
            popup.ShowDialog();
        }
        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed2;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtMd_Leader.EditValue = popup.U_NICKNAME;
                this.txtMd_u_id.EditValue = popup.U_ID;
            }
            popup = null;
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MTYPE = "0"
            };
            popup.FormClosed += popup_FormClosed3;
            popup.ShowDialog();
        }
        private void popup_FormClosed3(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed3;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtMember_Relations.EditValue = popup.U_NICKNAME;
                this.txtMember_u_id.EditValue = popup.U_ID;
            }
            popup = null;
        }

        private void btnSAVE_Click_1(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM11_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_id.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Chef_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_Chef_u_id"].Value = txtChef_u_id.EditValue;
                            cmd.Parameters["i_Chef_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Md_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_Md_u_id"].Value = txtMd_u_id.EditValue;
                            cmd.Parameters["i_Md_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Member_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_Member_u_id"].Value = txtMember_u_id.EditValue;
                            cmd.Parameters["i_Member_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Account_Number", MySqlDbType.VarChar));
                            cmd.Parameters["i_Account_Number"].Value = txtAccount_Number.EditValue;
                            cmd.Parameters["i_Account_Number"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_return", MySqlDbType.VarChar));
                            cmd.Parameters["o_return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open5();

            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "PS축하 스토리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PROD_ORDER_221", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtPS_O_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();

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

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView2.GetFocusedDisplayText());
                e.Handled = true;
            }
        }
    }
}
