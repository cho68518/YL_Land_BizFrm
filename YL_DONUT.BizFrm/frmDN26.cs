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

namespace YL_DONUT.BizFrm
{
    public partial class frmDN26 : FrmBase
    {
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
            setCmb();
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

        public override void Save()
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
    }
}
