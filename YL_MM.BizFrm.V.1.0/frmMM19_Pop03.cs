using DevExpress.CodeParser;
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
using YL_MM.BizFrm.Dlg;
namespace YL_MM.BizFrm
{
    public partial class frmMM19_Pop03 : FrmPopUpBase
    {
        frmMember_doma popup_doma;
        frmMember_md popup_md;
        public String sName { get; set; }
        public String sGshop_name { get; set; }
        public String sRegisterNo { get; set; }
        public String sEMail { get; set; }
        public String sTelNo { get; set; }
        public String sHpNo { get; set; }
        public String sPostNo { get; set; }
        public String sAddr { get; set; }
        public String sAddrDetail { get; set; }


        public frmMM19_Pop03()
        {
            InitializeComponent();

        }

        private void frmMM19_Pop03_Load(object sender, EventArgs e)
        {
            txtId_yn.EditValue = "N";
            txtNick_yn.EditValue = "N";
            txtCEO_NAME.EditValue = sName;
            txtGSHOP_NAME.EditValue = sGshop_name;
            txtU_NICKNAME.EditValue = sGshop_name;
            txtREGISTER_NO.EditValue = sRegisterNo;
            txtEMAIL.EditValue = sEMail;
            txtTEL_NO.EditValue = sTelNo;
            txtHP_NO.EditValue = sHpNo;
            btnPOST_NO.EditValue = sPostNo;
            txtADDRESS1.EditValue = sAddr;
            txtADDRESS2.EditValue = sAddrDetail;
            message();
            message1();
        }

        private void message()
        {

            if (txtLogin_id.Text.Length > 4)
            {
                int nCount;
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    sql.Query = "select count(*) as nCount FROM  domalife.member_master where login_id  = '" + txtLogin_id.EditValue + "' ";
                    DataSet ds = sql.selectQueryDataSet();

                    nCount = Convert.ToInt32(sql.selectQueryForSingleValue());
                }
                if (nCount == 1)
                {
                    txtId_yn.EditValue = "N";
                    lbMessage.Text = "사용중인 아이디 입니다";
                    return;
                }
                else
                {
                    txtId_yn.EditValue = "Y";
                    lbMessage.Text = "사용가능한 아이디 입니다";
                }

                string strQuery = string.Format(@"SELECT idx AS SEQ FROM y2k2.dbo.Y2K2_member where id  = '" + txtLogin_id.EditValue + "' ");

                DataSet ds1 = ServiceAgent.ExecuteDataSetStr("CONIS_IBS", strQuery);

                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0 && ds1.Tables[0].Rows[0][0] != DBNull.Value)
                {
                   // txtCnt.EditValue = ds.Tables[0].Rows[0]["SEQ"];
                    txtId_yn.EditValue = "N";
                    lbMessage.Text = "사용중인 아이디 입니다";

                }
                else
                {
                    txtId_yn.EditValue = "Y";
                    lbMessage.Text = "사용가능한 아이디 입니다";
                }
            }



        }

        private void message1()
        {
            if (txtU_NICKNAME.Text.Length > 4) 
            {
                int nCount;
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {

                    sql.Query = "select count(*) as nCount FROM  domalife.member_master where login_id  = '" + txtU_NICKNAME.EditValue + "' ";
                    DataSet ds = sql.selectQueryDataSet();

                    nCount = Convert.ToInt32(sql.selectQueryForSingleValue());
                }
                if (nCount == 1)
                {
                    txtNick_yn.EditValue = "N";
                    lbMessage.Text = "사용중인 닉네임 입니다";
                }
                else
                {
                    txtNick_yn.EditValue = "Y";
                    lbMessage.Text = "사용가능한 닉네임 입니다";
                }
            }


        }



        private void efwTextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                message();
                if (txtId_yn.EditValue == "Y")
                {
                    txtCEO_NAME.Focus();
                }
            }
               
                    }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtCEO_NAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 성명을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtHP_NO.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 전화번호를 입력하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtU_NICKNAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 닉네임을 입력하세요!");
                return;
            }

            if (txtId_yn.EditValue == "N")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 바른 로그인 ID를 입력하세요!");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "도넛 라이프 체험샵 계정을 생성 하시겠습니까?") == DialogResult.OK)
            {

                try
                {
                    int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MM_MM19_POP01_SAVE_03"
                                                               , this.txtCEO_NAME.EditValue
                                                               , this.txtHP_NO.EditValue
                                                               , this.btnPOST_NO.EditValue
                                                               , this.txtADDRESS1.EditValue
                                                               , this.txtADDRESS2.EditValue
                                                               , this.txtLogin_id.EditValue
                                                              );


                    if (retVal > 0)
                    {
                        MessageAgent.MessageShow(MessageType.Informational, "체험샵 계성이 생성되었습니다.");
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }

                using (MySqlConnection con = new MySqlConnection(ConstantLib.Member_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("DB_MEMBER.USP_MM_MM19_POP01_SAVE_01", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.VarChar));
                        cmd.Parameters["i_id"].Value = txtLogin_id.EditValue;
                        cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_pwd", MySqlDbType.VarChar));
                        cmd.Parameters["i_pwd"].Value = "0000";
                        cmd.Parameters["i_pwd"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                        cmd.Parameters["i_name"].Value = txtCEO_NAME.EditValue;
                        cmd.Parameters["i_name"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_nickname", MySqlDbType.VarChar));
                        cmd.Parameters["i_nickname"].Value = txtU_NICKNAME.EditValue;
                        cmd.Parameters["i_nickname"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_birth", MySqlDbType.VarChar));
                        cmd.Parameters["i_birth"].Value = "";
                        cmd.Parameters["i_birth"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_hpno", MySqlDbType.VarChar));
                        cmd.Parameters["i_hpno"].Value = txtHP_NO.EditValue;
                        cmd.Parameters["i_hpno"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_gender", MySqlDbType.VarChar));
                        cmd.Parameters["i_gender"].Value = "M";
                        cmd.Parameters["i_gender"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_u_id", MySqlDbType.VarChar));
                        cmd.Parameters["o_u_id"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                        cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        txtU_Id.EditValue = cmd.Parameters["o_u_id"].Value.ToString();

                        string sU_ID = string.Empty;
                        sU_ID = txtU_Id.EditValue.ToString();

                        //if (sU_ID == "N")
                        //    MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        //else
                        db_member();
                    }

                }

            }
        }

        private void db_member()
        {
            try
            {
                using (MySqlConnection con2 = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd2 = new MySqlCommand("domalife.USP_MM_MM19_POP01_SAVE_04", con2))
                    {
                        con2.Open();
                        cmd2.CommandType = CommandType.StoredProcedure;

                        cmd2.Parameters.Add(new MySqlParameter("i_o_u_id", MySqlDbType.VarChar));
                        cmd2.Parameters["i_o_u_id"].Value = txtU_Id.EditValue;
                        cmd2.Parameters["i_o_u_id"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.VarChar));
                        cmd2.Parameters["i_id"].Value = txtLogin_id.EditValue;
                        cmd2.Parameters["i_id"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_pwd", MySqlDbType.VarChar));
                        cmd2.Parameters["i_pwd"].Value = "0000";
                        cmd2.Parameters["i_pwd"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                        cmd2.Parameters["i_name"].Value = txtCEO_NAME.EditValue;
                        cmd2.Parameters["i_name"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_nickname", MySqlDbType.VarChar));
                        cmd2.Parameters["i_nickname"].Value = txtU_NICKNAME.EditValue;
                        cmd2.Parameters["i_nickname"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_birth", MySqlDbType.VarChar));
                        cmd2.Parameters["i_birth"].Value = "";
                        cmd2.Parameters["i_birth"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_hpno", MySqlDbType.VarChar));
                        cmd2.Parameters["i_hpno"].Value = txtHP_NO.EditValue;
                        cmd2.Parameters["i_hpno"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_gender", MySqlDbType.VarChar));
                        cmd2.Parameters["i_gender"].Value = "M";
                        cmd2.Parameters["i_gender"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                        cmd2.Parameters["i_post_no"].Value = btnPOST_NO.EditValue;
                        cmd2.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_addr", MySqlDbType.VarChar));
                        cmd2.Parameters["i_addr"].Value = txtADDRESS1.EditValue;
                        cmd2.Parameters["i_addr"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_addr_detail", MySqlDbType.VarChar));
                        cmd2.Parameters["i_addr_detail"].Value = txtADDRESS2.EditValue;
                        cmd2.Parameters["i_addr_detail"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                        cmd2.Parameters["o_Return"].Direction = ParameterDirection.Output;

                        cmd2.ExecuteNonQuery();

                        //MessageBox.Show(cmd2.Parameters["o_Return"].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            db_member1();
        }
        private void db_member1()
        {
            if (string.IsNullOrEmpty(this.txtU_Id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회원을 선택하세요!");
                return;
            }
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_POP03_SAVE_02", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                        cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32("0");
                        cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_ceo_name", MySqlDbType.VarChar));
                        cmd.Parameters["i_ceo_name"].Value = txtCEO_NAME.EditValue;
                        cmd.Parameters["i_ceo_name"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_gshop_name", MySqlDbType.VarChar));
                        cmd.Parameters["i_gshop_name"].Value = txtGSHOP_NAME.EditValue;
                        cmd.Parameters["i_gshop_name"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_u_nickname", MySqlDbType.VarChar));
                        cmd.Parameters["i_u_nickname"].Value = txtU_NICKNAME.EditValue;
                        cmd.Parameters["i_u_nickname"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_register_no", MySqlDbType.VarChar));
                        cmd.Parameters["i_register_no"].Value = txtREGISTER_NO.EditValue;
                        cmd.Parameters["i_register_no"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_email", MySqlDbType.VarChar));
                        cmd.Parameters["i_email"].Value = txtEMAIL.EditValue;
                        cmd.Parameters["i_email"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_tel_no", MySqlDbType.VarChar));
                        cmd.Parameters["i_tel_no"].Value = txtTEL_NO.EditValue;
                        cmd.Parameters["i_tel_no"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_hp_no", MySqlDbType.VarChar));
                        cmd.Parameters["i_hp_no"].Value = txtHP_NO.EditValue;
                        cmd.Parameters["i_hp_no"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                        cmd.Parameters["i_post_no"].Value = btnPOST_NO.EditValue;
                        cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_jibun_addr", MySqlDbType.VarChar));
                        cmd.Parameters["i_jibun_addr"].Value = txtADDRESS1.EditValue;
                        cmd.Parameters["i_jibun_addr"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_road_addr", MySqlDbType.VarChar));
                        cmd.Parameters["i_road_addr"].Value = txtADDRESS1.EditValue;
                        cmd.Parameters["i_road_addr"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_sido", MySqlDbType.VarChar));
                        cmd.Parameters["i_sido"].Value = "";
                        cmd.Parameters["i_sido"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_gugun", MySqlDbType.VarChar));
                        cmd.Parameters["i_gugun"].Value = "";
                        cmd.Parameters["i_gugun"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_recomm_nm", MySqlDbType.VarChar));
                        cmd.Parameters["i_recomm_nm"].Value = "";
                        cmd.Parameters["i_recomm_nm"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_recomm_u_id", MySqlDbType.VarChar));
                        cmd.Parameters["i_recomm_u_id"].Value = "";
                        cmd.Parameters["i_recomm_u_id"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_md_u_id", MySqlDbType.VarChar));
                        cmd.Parameters["i_md_u_id"].Value = txtMD_U_ID.EditValue;
                        cmd.Parameters["i_md_u_id"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_road_addr2", MySqlDbType.VarChar));
                        cmd.Parameters["i_road_addr2"].Value = txtADDRESS2.EditValue;
                        cmd.Parameters["i_road_addr2"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                        cmd.Parameters["i_u_id"].Value = txtU_Id.EditValue;
                        cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_is_best", MySqlDbType.VarChar));
                        cmd.Parameters["i_is_best"].Value = "N";
                        cmd.Parameters["i_is_best"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_team_leader", MySqlDbType.VarChar));
                        cmd.Parameters["i_team_leader"].Value = "";
                        cmd.Parameters["i_team_leader"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                        cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            payment();
        }
 
        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {

            popup_md = new frmMember_md
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "MD",
            };
            popup_md.FormClosed += popup_FormClosed2;
            //PopUpBizAgent.Show(this, popup);
            popup_md.ShowDialog();
        }



        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup_md.FormClosed -= popup_FormClosed2;
            if (popup_md.DialogResult == DialogResult.OK)
            {
                this.txtMD_NICKNAME.EditValue = popup_md.U_NICKNAME;
                this.txtMD_U_ID.EditValue = popup_md.U_ID;
            }
            popup_md = null;
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            txtId_yn.EditValue = "Y";
            txtNick_yn.EditValue = "Y";
            popup_doma = new frmMember_doma
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
        };
            popup_doma.FormClosed += popup_FormClosed4;
            popup_doma.ShowDialog();
        }
        private void popup_FormClosed4(object sender, FormClosedEventArgs e)
        {
            popup_doma.FormClosed -= popup_FormClosed4;
            if (popup_doma.DialogResult == DialogResult.OK)
            {
                this.txtLogin_id.EditValue = popup_doma.USER_ID;
                this.txtCEO_NAME.EditValue = popup_doma.U_NAME;
                this.txtU_NICKNAME.EditValue = popup_doma.U_NICKNAME;
                this.txtGSHOP_NAME.EditValue = popup_doma.U_NICKNAME;
                this.txtEMAIL.EditValue = popup_doma.U_EMAIL;
                this.txtHP_NO.EditValue = popup_doma.U_CELL_NUM;
                this.btnPOST_NO.EditValue = popup_doma.U_ZIP;
                this.txtADDRESS1.EditValue = popup_doma.U_ADDR;
                this.txtADDRESS2.EditValue = popup_doma.U_ADDR_DETAIL;
                this.txtU_Id.EditValue = popup_doma.U_ID;
            }
            popup_doma = null;
        }

        private void txtU_NICKNAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                message1();
                if (txtNick_yn.EditValue == "Y")
                {
                    txtREGISTER_NO.Focus();
                }
            }


        }

        private void txtCEO_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtGSHOP_NAME.Focus();
        }

        private void txtREGISTER_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtEMAIL.Focus();
        }

        private void txtEMAIL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtHP_NO.Focus();
        }

        private void txtADDRESS1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtADDRESS2.Focus();
        }

        private void btnPOST_NO_Click(object sender, EventArgs e)
        {
            Dlg.frmZipNoInfo FrmInfo = new Dlg.frmZipNoInfo() { ParentBtn = btnPOST_NO, ParentAddr1 = txtADDRESS1, ParentAddr2 = txtADDRESS2 };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
            txtADDRESS2.Focus();
        }


        private void payment()
        {

            string sNickName = string.Empty;

            if (string.IsNullOrEmpty(this.txtCEO_NAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 고객을 선택하세요");
                return;
            }

            if (string.IsNullOrEmpty(this.txtHP_NO.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "전화번호를 입력하세요");
                return;
            }


            string u_id = txtU_Id.EditValue.ToString();
            string u_name = txtCEO_NAME.EditValue.ToString();
            string p_id = "0";
            string opt_id = "0";
            string p_name = "체헙샵 가입비";
            string p_num = "1";
            string p_price = "50000";
            int n_amt = Convert.ToInt32(p_num) * Convert.ToInt32(p_price);
            string p_amt = n_amt.ToString();
            string u_email;
            if (string.IsNullOrEmpty(this.txtEMAIL.Text))
            {
                u_email = "";
            }
            else
            {
                u_email = txtEMAIL.EditValue.ToString();
            }

            //string u_email = txtE_Mail.EditValue.ToString();
            //  domabiz.USP_ORDER_CRE
            //  domabiz.USP_ORDER_UPDT
            string surl = "https://callpay.eyeoyou.com/lgu_pay/pay_crossplatform.aspx?&u_id=" + u_id + "&u_name=" + u_name + "&p_id=" + p_id + "&opt_id=" + opt_id + "&p_name=" + p_name + "&p_num=" + p_num + "&p_amt=" + p_amt + "&p_price=" + p_price + "&u_email=" + u_email + "&pay_code=SC0040" + "&p_type=02";

            System.Diagnostics.Process.Start(surl);
        }

    }
}
