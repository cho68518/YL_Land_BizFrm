﻿using DevExpress.Utils;
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
using YL_DONUT.BizFrm.Dlg;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;



namespace YL_DONUT.BizFrm
{
    public partial class frmDN26 : FrmBase
    {
        frmMember_info popup_member;
        frmDN26_Pop01 popup;


        public frmDN26()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmDN01";
            //폼명설정
            this.FrmName = "BIZ MD 정산상세내역";
        }

        private void frmDN026_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            efwLabel1.Text = "입금완료일";
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView1.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";
            rbis_biz.EditValue = "1";
            this.rbis_biz.Visible = false;

            advBandedGridView2.OptionsView.ShowFooter = true;
            this.efwGridControl6.BindControlSet(

               new ColumnControlSet("o_code", txtO_Code)
             , new ColumnControlSet("u_nickname", txtU_Nickname)
             , new ColumnControlSet("o_receive_name", txtU_Name)
             , new ColumnControlSet("u_chef_level", txtU_Chef_Level)
             , new ColumnControlSet("o_total_cost", txtTotal_Cost)
             , new ColumnControlSet("o_purchase_cost", txtPurchase_Cost)
             , new ColumnControlSet("gshop_nickname", txtGshop_Relations)
             , new ColumnControlSet("team_nickname", txtTeam_Relations)
             , new ColumnControlSet("gshop_relations", txtGshop_Relations_ID)
             , new ColumnControlSet("team_relations", txtTeam_Relations_ID)
             , new ColumnControlSet("cashback", txtCashback)
             , new ColumnControlSet("team_cashback", txtTeam_Cashback)

             ); 

            this.efwGridControl1.Click += efwGridControl1_Click;

            setCmb();
        }
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            //btnPOST_NO.EditValue = "";

            //if (dr != null && dr["sido_code"].ToString() != "")
            //{
            //    this.cmbTAREA1.EditValue = dr["sido_code"].ToString();
            //    this.cmbSAREA1.EditValue = dr["gugun_code"].ToString();
            //}
            //if (dr != null && dr["post_no"].ToString() != "")
            //{
            //    this.btnPOST_NO.EditValue2 = dr["post_no"].ToString();
            //    this.btnPOST_NO.Text = dr["post_no"].ToString();
            //}
        }

        private void setCmb()
        {
            try
            {
                Dictionary<string, string> myRecord;


                string strQueruy2 = @"  SELECT
                              T1.DCODE, T1.DNAME
                              FROM
                              (  SELECT CODE  DCODE, CODE_NM DNAME
                                 FROM dbo.ETCCODE
	                             WHERE GRP_CODE = 'ORDER_SEARCH' " + @") T1 ";

                CodeAgent.SetLegacyCode(cmbORDER_SEARCH, strQueruy2);
                cmbORDER_SEARCH.EditValue = "1";
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
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
        }

        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbORDER_SEARCH.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtI_SEARCH.EditValue;

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

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbORDER_SEARCH.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtI_SEARCH.EditValue;

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

        private void Open3()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbORDER_SEARCH.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtI_SEARCH.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();
                        }
                    }
                }
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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = rbis_biz.EditValue;

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
        private void Open5()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_07", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbORDER_SEARCH.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtI_SEARCH.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
                            this.efwGridControl5.MyGridView.BestFitColumns();
                        }
                    }
                }
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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_08", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbORDER_SEARCH.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtI_SEARCH.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl6.DataBind(ds);
                            this.efwGridControl6.MyGridView.BestFitColumns();
                        }
                    }
                }
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
                string sP_SHOW_TYPE = string.Empty;
                 
                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_09", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl7.DataBind(ds);
                            this.efwGridControl7.MyGridView.BestFitColumns();
                        }
                    }
                }
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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_10", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_11", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl9.DataBind(ds);
                            this.efwGridControl9.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public override void Save()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Save1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Save2();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Save3();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                Save4();
            }
        }

        public void Save1()
        {
            try
            {
                if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
                { 
                    for (var i = 0; i < gridView1.DataRowCount; i++)
                    {
                        if (gridView1.GetRowCellValue(i, "md_leader").ToString().Length > 10)
                        {
                            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SAVE_02", con))
                                {
                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[0].Value = gridView1.GetRowCellValue(i, "o_code");

                                    cmd.Parameters.Add("i_o_u_id", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[1].Value = gridView1.GetRowCellValue(i, "o_u_id");

                                    cmd.Parameters.Add("i_remark1", MySqlDbType.VarChar, 255);
                                    cmd.Parameters[2].Value = gridView1.GetRowCellValue(i, "remark1");

                                    cmd.Parameters.Add("i_md_leader", MySqlDbType.VarChar, 255);
                                    cmd.Parameters[3].Value = gridView1.GetRowCellValue(i, "md_leader");

                                    cmd.Parameters.Add("i_Proc", MySqlDbType.VarChar, 255);
                                    cmd.Parameters[4].Value = "P";

                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            Search();
        }

        public void Save2()
        {
            try
            {
                if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
                {
                    for (var i = 0; i < gridView2.DataRowCount; i++)
                    {

                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SAVE_03", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[0].Value = gridView2.GetRowCellValue(i, "o_code");

                                cmd.Parameters.Add("i_confirm_yn", MySqlDbType.VarChar, 1);
                                cmd.Parameters[1].Value = gridView2.GetRowCellValue(i, "confirm_yn");

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            Search();
        }

        public void Save3()
        {
            try
            {
                if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
                {
                    for (var i = 0; i < gridView3.DataRowCount; i++)
                    {

                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SAVE_04", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[0].Value = gridView3.GetRowCellValue(i, "o_code");

                                cmd.Parameters.Add("i_on_confirm_yn", MySqlDbType.VarChar, 1);
                                cmd.Parameters[1].Value = gridView3.GetRowCellValue(i, "confirm_yn");

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            Search();
        }

        public void Save4()
        {
            try
            {
                if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
                {
                    for (var i = 0; i < advBandedGridView1.DataRowCount; i++)
                    {

                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SAVE_04", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[0].Value = advBandedGridView1.GetRowCellValue(i, "o_code");

                                cmd.Parameters.Add("i_member_chef", MySqlDbType.VarChar, 50);
                                cmd.Parameters[1].Value = advBandedGridView1.GetRowCellValue(i, "member_chef");

                                cmd.Parameters.Add("i_member_tax", MySqlDbType.Int32);
                                cmd.Parameters[2].Value = Convert.ToInt32(advBandedGridView1.GetRowCellValue(i, "member_tax"));

                                cmd.Parameters.Add("i_member_amt", MySqlDbType.Int32);
                                cmd.Parameters[3].Value = Convert.ToInt32(advBandedGridView1.GetRowCellValue(i, "member_amt"));

                                cmd.Parameters.Add("i_member_date", MySqlDbType.DateTime);
                                cmd.Parameters[4].Value = advBandedGridView1.GetRowCellValue(i, "member_date");

                                cmd.Parameters.Add("i_member_yn", MySqlDbType.VarChar, 1);
                                cmd.Parameters[5].Value = advBandedGridView1.GetRowCellValue(i, "member_yn");

                                cmd.Parameters.Add("i_member_remark", MySqlDbType.VarChar, 255);
                                cmd.Parameters[6].Value = advBandedGridView1.GetRowCellValue(i, "member_remark");

                                cmd.Parameters.Add("i_g_power_yn", MySqlDbType.VarChar, 1);
                                cmd.Parameters[7].Value = advBandedGridView1.GetRowCellValue(i, "g_power_yn");

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
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

        private void txtI_SEARCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            popup = new frmDN26_Pop01();

            popup.pU_NICKNAME = gridView1.GetFocusedRowCellValue("u_nickname").ToString();
            popup.pU_ID = gridView1.GetFocusedRowCellValue("o_u_id").ToString();
            popup.pMD_U_ID = gridView1.GetFocusedRowCellValue("md_leader").ToString();
            popup.pMD_U_NICKNAME = "";

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }


        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;
            if (popup.DialogResult == DialogResult.OK)
            {

                this.gridView1.SetRowCellValue(this.gridView1.FocusedRowHandle, this.gridView1.FocusedColumn, popup.pMD_U_NICKNAME);
                this.gridView1.SetRowCellValue(this.gridView1.FocusedRowHandle, gridView1.Columns["md_leader"], popup.pMD_U_ID);

            }
            popup = null;
        }

        private void delete_Click(object sender, EventArgs e)
        {

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                string sO_CODE = gridView1.GetFocusedRowCellValue("o_code").ToString();
                string sO_U_ID = gridView1.GetFocusedRowCellValue("o_u_id").ToString();
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SAVE_02", con))
                        {

                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                            cmd.Parameters[0].Value = sO_CODE;

                            cmd.Parameters.Add("i_o_u_id", MySqlDbType.VarChar, 50);
                            cmd.Parameters[1].Value = sO_U_ID;

                            cmd.Parameters.Add("i_remark1", MySqlDbType.VarChar, 255);
                            cmd.Parameters[2].Value = "";

                            cmd.Parameters.Add("i_md_leader", MySqlDbType.VarChar, 255);
                            cmd.Parameters[3].Value = "";

                            cmd.Parameters.Add("i_Proc", MySqlDbType.VarChar, 255);
                            cmd.Parameters[4].Value = "D";

                            cmd.ExecuteNonQuery();
                            con.Close();
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

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                efwLabel1.Text = "입금완료일";
                this.rbis_biz.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                efwLabel1.Text = "배송시작일";
                this.rbis_biz.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                efwLabel1.Text = "배송시작일";
                this.rbis_biz.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                efwLabel1.Text = "배송시작일";
                this.rbis_biz.Visible = true;
            }
        }

        private void rbis_biz_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbis_biz.EditValue.ToString() == "1")
            {
                this.gridView4.Columns[6].Caption = "50%캐시백(본인)";
            }
            else
            {
                this.gridView4.Columns[6].Caption = "40%캐시백(G멀티샵)";
            }
        }

        private void BtnMemberSch_Click(object sender, EventArgs e)
        {
            popup_member = new frmMember_info
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
            };
            popup_member.FormClosed += popup_FormClosed1;
            popup_member.ShowDialog();
        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup_member.FormClosed -= popup_FormClosed1;
            if (popup_member.DialogResult == DialogResult.OK)
            {
                this.txtGshop_Relations.EditValue = popup_member.U_NICKNAME;
                this.txtGshop_Relations_ID.EditValue = popup_member.U_ID;
            }
            popup_member = null;
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            popup_member = new frmMember_info
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
            };
            popup_member.FormClosed += popup_FormClosed2;
            popup_member.ShowDialog();
        }
        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup_member.FormClosed -= popup_FormClosed2;
            if (popup_member.DialogResult == DialogResult.OK)
            {
                this.txtTeam_Relations.EditValue = popup_member.U_NICKNAME;
                this.txtTeam_Relations_ID.EditValue = popup_member.U_ID;
            }
            popup_member = null;
        }

        private void btnAddress_Save_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SAVE_05", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_cashback", MySqlDbType.Int32));
                            cmd.Parameters["i_cashback"].Value = Convert.ToInt32(txtCashback.EditValue);
                            cmd.Parameters["i_cashback"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_team_cashback", MySqlDbType.Int32));
                            cmd.Parameters["i_team_cashback"].Value = Convert.ToInt32(txtTeam_Cashback.EditValue);
                            cmd.Parameters["i_team_cashback"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_relations", MySqlDbType.VarChar));
                            cmd.Parameters["i_gshop_relations"].Value = txtGshop_Relations_ID.EditValue;
                            cmd.Parameters["i_gshop_relations"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_team_relations", MySqlDbType.VarChar));
                            cmd.Parameters["i_team_relations"].Value = txtTeam_Relations_ID.EditValue;
                            cmd.Parameters["i_team_relations"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();

                            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
                Open1();
            }
        }

        private void txtGshop_Relations_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            txtGshop_Relations_ID.EditValue = "";
        }

        private void txtTeam_Relations_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            txtTeam_Relations_ID.EditValue = "";
        }
    }
}  
