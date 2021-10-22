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
using YL_GSHOP.BizFrm.Dlg;
using DevExpress.XtraGrid.Columns;

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP19 : FrmBase
    {
        frmMemberInfo popup;
        public frmGSHOP19()
        {
            InitializeComponent();
            this.QCode = "GSHOP12";
            //폼명설정
            this.FrmName = "G멀티샵 현수막 X배너 지급관리";
        }

        private void frmGSHOP19_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;


            dtReg_Date.EditValue = DateTime.Now;
            gridView2.OptionsView.ShowFooter = true;

            this.efwGridControl2.BindControlSet(
              new ColumnControlSet("gshop_id", txtGShop_ID)
             ,new ColumnControlSet("gshop_name", dfGShop_name)
            );
            this.efwGridControl2.Click += efwGridControl2_Click;


            gridView3.OptionsView.ShowFooter = true;

            this.efwGridControl3.BindControlSet(
               new ColumnControlSet("gshop_id", txtGShop_ID)
             , new ColumnControlSet("reg_date", dtReg_Date)
             , new ColumnControlSet("banner", chkBanner)
             , new ColumnControlSet("x_banner", chkX_Banner)
             , new ColumnControlSet("remark", txtRemark)
             , new ColumnControlSet("rank1", txtRank1)
             , new ColumnControlSet("rank2", txtRank2)
             , new ColumnControlSet("stock_qty", txtStock_Qty)
             , new ColumnControlSet("youtube_qty", txtYoutube_Qty)
             , new ColumnControlSet("ceo_pic_qty", txtCeo_Pic_Qty)
             , new ColumnControlSet("youtube_relations", txtYouTube_Relations)
             , new ColumnControlSet("utube_nic", txtUtube_nic)
            );
            this.efwGridControl2.Click += efwGridControl2_Click;

        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["gshop_id"].ToString() != "")
            {
                Open1();
            }
            Cursor.Current = Cursors.Default;
        }

        public override void Search()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;
                        if (string.IsNullOrEmpty(this.txtQRank1.Text))
                        {
                            txtRank1.EditValue = "0";
                        }
                        cmd.Parameters.Add("i_QRank1", MySqlDbType.Int32);
                        cmd.Parameters[1].Value = Convert.ToInt32(txtQRank1.EditValue);
                        if (string.IsNullOrEmpty(this.txtQRank2.Text))
                        {
                            txtRank1.EditValue = "0";
                        }
                        cmd.Parameters.Add("i_QRank2", MySqlDbType.Int32);
                        cmd.Parameters[2].Value = Convert.ToInt32(txtQRank2.EditValue);

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        private void Open1()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_gshow_id", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtGShop_ID.EditValue);


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGShop_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Reg_Date", MySqlDbType.DateTime));
                            cmd.Parameters["i_Reg_Date"].Value = dtReg_Date.EditValue;
                            cmd.Parameters["i_Reg_Date"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_Banner", MySqlDbType.VarChar));
                            cmd.Parameters["i_Banner"].Value = chkBanner.EditValue;
                            cmd.Parameters["i_Banner"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_X_Banner", MySqlDbType.VarChar));
                            cmd.Parameters["i_X_Banner"].Value = chkX_Banner.EditValue;
                            cmd.Parameters["i_X_Banner"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_Remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_Remark"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtRank1.Text))
                            {
                                txtRank1.EditValue = "0";
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_rank1", MySqlDbType.Int32));
                            cmd.Parameters["i_rank1"].Value = Convert.ToInt32(txtRank1.EditValue);
                            cmd.Parameters["i_rank1"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtRank2.Text))
                            {
                                txtRank1.EditValue = "0";
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_rank2", MySqlDbType.Int32));
                            cmd.Parameters["i_rank2"].Value = Convert.ToInt32(txtRank2.EditValue);
                            cmd.Parameters["i_rank2"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtStock_Qty.Text))
                            {
                                txtStock_Qty.EditValue = "0"; 
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_stock_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_stock_qty"].Value = Convert.ToInt32(txtStock_Qty.EditValue);
                            cmd.Parameters["i_stock_qty"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtYoutube_Qty.Text))
                            {
                                txtYoutube_Qty.EditValue = "0"; 
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_youtube_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_youtube_qty"].Value = Convert.ToInt32(txtYoutube_Qty.EditValue);
                            cmd.Parameters["i_youtube_qty"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtCeo_Pic_Qty.Text))
                            {
                                txtCeo_Pic_Qty.EditValue = "0";
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_ceo_pic_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_ceo_pic_qty"].Value = Convert.ToInt32(txtCeo_Pic_Qty.EditValue);
                            cmd.Parameters["i_ceo_pic_qty"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_YouTube_Relations", MySqlDbType.VarChar));
                            cmd.Parameters["i_YouTube_Relations"].Value = txtYouTube_Relations.EditValue;
                            cmd.Parameters["i_YouTube_Relations"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGShop_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Reg_Date", MySqlDbType.DateTime));
                            cmd.Parameters["i_Reg_Date"].Value = dtReg_Date.EditValue;
                            cmd.Parameters["i_Reg_Date"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
            };
            popup.FormClosed += popup_FormClosed2;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed2;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtYouTube_Relations.EditValue = popup.U_ID;
                this.txtUtube_nic.EditValue = popup.U_NICKNAME;
            }
            popup = null;
        }
    }
}
