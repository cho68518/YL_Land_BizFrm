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
using YL_MM.BizFrm.Dlg;

namespace YL_MM.BizFrm
{

    public partial class frmMM07_Pop01 : FrmPopUpBase
    {
        frmMemberInfo popup;

        public string pIDX { get; set; }
        public string pLEVEL { get; set; }
        public string pSEND_ID { get; set; }
        public string pU_NAME { get; set; }
        public string pU_NICKNAME { get; set; }
        public string pRECV_ID  { get; set; }
        public string pRECV_U_NAME { get; set; }
        public string pRECV_U_NICKNAME { get; set; }
        public string pDOMA_ID { get; set; }
        public string pDOMA_U_NAME { get; set; }
        public string pDOMA_U_NICKNAME { get; set; }



        public frmMM07_Pop01()
        {
            InitializeComponent();
        }

        private void frmMM07_Pop01_Load(object sender, EventArgs e)
        {
            txtIDX.EditValue = pIDX;
            txtLEVEL.EditValue = pLEVEL;
            txtSEND_ID.EditValue = pSEND_ID;
            txtU_NAME.EditValue = pU_NAME;
            txtU_NICKNAME.EditValue = pU_NICKNAME;
            txtRECV_ID.EditValue = pRECV_ID;
            txtRECV_U_NAME.EditValue = pRECV_U_NAME;
            txtRECV_U_NICKNAME.EditValue = pRECV_U_NICKNAME;
            txtDOMA_ID.EditValue = pDOMA_ID;
            txtDOMA_U_NAME.EditValue = pDOMA_U_NAME;
            txtDOMA_U_NICKNAME.EditValue = pDOMA_U_NICKNAME;
            //txtRECV_U_ID.EditValue = "1";
        }

        private void BtnMemberSch_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MTYPE = "0"
            };
            popup.FormClosed += popup_FormClosed1;
            //PopUpBizAgent.Show(this, popup01);
            popup.ShowDialog();
        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtRECV_ID.EditValue = popup.USER_ID;
                this.txtRECV_U_NAME.EditValue = popup.U_NAME;
                this.txtRECV_U_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtRECV_U_ID.EditValue = popup.U_ID;

            }
            popup = null;
        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domalife.USP_MM_MM07_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_recv_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_recv_id"].Value = txtIDX.EditValue;
                            cmd.Parameters["i_recv_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_send_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_send_id"].Value = txtRECV_U_ID.EditValue;
                            cmd.Parameters["i_send_id"].Direction = ParameterDirection.Input;
                            //txtDOMA_U_ID
                            cmd.Parameters.Add(new MySqlParameter("i_level", MySqlDbType.VarChar));
                            cmd.Parameters["i_level"].Value = txtLEVEL.EditValue;
                            cmd.Parameters["i_level"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_doma_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_doma_id"].Value = txtDOMA_U_ID.EditValue;
                            cmd.Parameters["i_doma_id"].Direction = ParameterDirection.Input;

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
                finally
                {
                    //EfwSimpleButton1_Click(null, null);
                }
            }
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MTYPE = "0"
            };
            popup.FormClosed += popup_FormClosed2;
            //PopUpBizAgent.Show(this, popup01);
            popup.ShowDialog();
        }

        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtDOMA_ID.EditValue = popup.USER_ID;
                this.txtDOMA_U_NAME.EditValue = popup.U_NAME;
                this.txtDOMA_U_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtDOMA_U_ID.EditValue = popup.U_ID;

            }
            popup = null;
        }

        private void BtnREL_SAVE_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtRECV_U_ID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 변경할 추천인을 선택하세요!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domalife.USP_MM_MM07_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_recv_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_recv_id"].Value = txtIDX.EditValue;
                            cmd.Parameters["i_recv_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_send_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_send_id"].Value = txtRECV_U_ID.EditValue;
                            cmd.Parameters["i_send_id"].Direction = ParameterDirection.Input;
                            //txtDOMA_U_ID
                            cmd.Parameters.Add(new MySqlParameter("i_level", MySqlDbType.VarChar));
                            cmd.Parameters["i_level"].Value = txtLEVEL.EditValue;
                            cmd.Parameters["i_level"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_doma_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_doma_id"].Value = txtDOMA_U_ID.EditValue;
                            cmd.Parameters["i_doma_id"].Direction = ParameterDirection.Input;

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
                finally
                {
                    //EfwSimpleButton1_Click(null, null);
                }
            }
        }
    }
}
