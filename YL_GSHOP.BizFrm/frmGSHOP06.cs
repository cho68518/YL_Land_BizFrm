using Easy.Framework.Common;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_GSHOP.BizFrm.Dlg;

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP06 : FrmBase
    {
        frmMemberInfo popup;

        public EventHandler efwGridControl1_Click { get; private set; }

        public frmGSHOP06()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "GSHOP06";   
            //폼명설정
            this.FrmName = "G멀티샵 MD등록";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = false;
            this.IsSearch = false;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //gridView1.OptionsView.ShowFooter = true;

            //this.efwGridControl1.BindControlSet(
            //          new ColumnControlSet("ID", txtID)
            //        , new ColumnControlSet("SETCODE", txtSETCODE)
            //          );

            //this.efwGridControl1.Click += efwGridControl1_Click;

            //setCmb();


            this.efwGridControl1.BindControlSet(

          new ColumnControlSet("u_name", txtU_NAME)
        , new ColumnControlSet("u_nickname", txtU_NICKNAME)
        , new ColumnControlSet("user_id", txtUSER_ID)
        , new ColumnControlSet("u_birthday", txtBIRTH)
        , new ColumnControlSet("u_gender", txtU_GENDER)
        , new ColumnControlSet("u_email", txtU_EMAIL)
        , new ColumnControlSet("reg_date", txtREG_DATE)
        , new ColumnControlSet("u_zip", txtU_ZIP)
        , new ColumnControlSet("u_addr", txtU_ADDR)
        , new ColumnControlSet("u_addr_detail", txtU_ADDR_DETAIL)
        , new ColumnControlSet("u_chef_level", txtU_CHEF_LEVEL)
        , new ColumnControlSet("is_stock_friend", chkIS_STOCK_FRIEND)
        , new ColumnControlSet("idx", txtIDX)
          );

            this.efwGridControl1.Click += efwGridControl1_Click;


            BtnNew_Click(null, null);
        }





        #endregion

        private void BtnNew_Click(object sender, EventArgs e)
        {
            txtU_NAME.EditValue = null;
            txtU_NICKNAME.EditValue = null;
            txtUSER_ID.EditValue = null;
            txtBIRTH.EditValue = null;
            txtU_GENDER.EditValue = null;
            txtU_CELL_NUM.EditValue = null;
            txtU_EMAIL.EditValue = null;
            txtLOGIN_DATE.EditValue = null;
            txtREG_DATE.EditValue = null;
            txtU_ZIP.EditValue = null;
            txtU_ADDR.EditValue = null;
            txtU_ADDR_DETAIL.EditValue = null;
            txtU_CHEF_LEVEL.EditValue = null;
            chkIS_STOCK_FRIEND.Checked = false;

        }

        //private void TxtPOSTNO_Click(object sender, EventArgs e)
        //{
        //    frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = txtPOSTNO, ParentAddr1 = txtADDRESS1, ParentAddr2 = txtADDRESS2 };
        //    FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
        //    FrmInfo.COMPANYNAME = txtCOMPANYCD.EditValue.ToString();
        //    FrmInfo.ShowDialog();
        //    txtADDRESS2.Focus();
        //}


        private void BtnMemberSch_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
            };
            popup.FormClosed += popup_FormClosed1;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.lblIdx.Text = popup.U_NAME;
                this.lbl_u_id.Text = popup.USER_ID;

                this.txtU_NAME.Text = popup.U_NAME;

                this.txtU_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtUSER_ID.EditValue = popup.USER_ID;
                this.txtBIRTH.EditValue = popup.BIRTH;
                this.txtU_GENDER.EditValue = popup.U_GENDER;
                this.txtU_CELL_NUM.EditValue = popup.U_CELL_NUM;
                this.txtU_EMAIL.EditValue = popup.U_EMAIL;
                this.txtLOGIN_DATE.EditValue = popup.LOGIN_DATE;
                this.txtREG_DATE.EditValue = popup.REG_DATE;
                this.txtU_ZIP.EditValue = popup.U_ZIP;
                this.txtU_ADDR.EditValue = popup.U_ADDR;
                this.txtU_ADDR_DETAIL.EditValue = popup.U_ADDR_DETAIL;
                this.txtU_CHEF_LEVEL.EditValue = popup.U_CHEF_LEVEL;

                this.chkIS_STOCK_FRIEND.EditValue = popup.IS_STOCK_FRIEND;


            }
            popup = null;
        }
        //#region 신규

        //public override void NewMode()
        //{
        //    base.NewMode();

        //    Eraser.Clear(this, "CLR1");
        //}

        //#endregion
        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = txtSearch.EditValue;


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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUSER_ID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP06_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_chk", MySqlDbType.VarChar));
                            cmd.Parameters["i_chk"].Value = chkIS_STOCK_FRIEND.EditValue;
                            cmd.Parameters["i_chk"].Direction = ParameterDirection.Input;

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
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
            }
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP06_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbSearch_Type.EditValue;

                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtSearch_Name.EditValue;

                        //Console.WriteLine(" i_Search_type           ---> [" + cmd.Parameters[0].Value + "]");
                        //Console.WriteLine(" i_search_Name           ---> [" + cmd.Parameters[1].Value + "]");
                        //Console.WriteLine(" i_member_type           ---> [" + cmd.Parameters[2].Value + "]");

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

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP06_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = txtSearch.EditValue;


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
    }
}
