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
using YL_DONUT.BizFrm.Dlg;

namespace YL_DONUT.BizFrm
{
    public partial class frmDN25 : FrmBase
    {
        frmMember_info popup_member;
        public frmDN25()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmDN25";
            //폼명설정
            this.FrmName = "월 정산소급 등록";
        }
        private void frmDN25_Load(object sender, EventArgs e)
        {
           // base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            txtCoidNo.EditValue = UserInfo.instance().LOGIN_ID;
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;


            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {

                sql.Query = "select code_id FROM  domaadmin.tb_common_code where gcode_id = 00048 and code_nm  = '" + UserInfo.instance().Name + "' ";
                DataSet ds = sql.selectQueryDataSet();

                txtCoidName.EditValue = sql.selectQueryForSingleValue().ToString();
            }


            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("idx", txtidx)
            , new ColumnControlSet("r_u_name", txtr_u_name)
            , new ColumnControlSet("r_u_nickname", txtr_nickname)
            , new ColumnControlSet("r_login_id", txtr_login_id)
            , new ColumnControlSet("contents_id", txtcontents_id)
            );

            this.efwGridControl1.Click += efwGridControl1_Click;

            SetCmb();
        }


        private void SetCmb()
        {
            // 회원검색

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00047' order by seq  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbDonut_Type, codeArray);
            }

            cmbDonut_Type.EditValue = "0";
        }
        
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            txtcancel.EditValue = "";
            txtcontents_id.EditValue = "";
            if (dr != null && dr["idx"].ToString() != "0")
            {
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    sql.Query = "select ifnull(contents_id,'0') FROM  domalife.donut_shooting_list where contents_id  = '" + txtidx.EditValue + "' ";
                    DataSet ds = sql.selectQueryDataSet();
                    txtcontents_id.EditValue = sql.selectQueryForSingleValue().ToString();
                }

                if (txtcontents_id.Text.Length > 5 ^ txtr_login_id.EditValue.ToString() == "AdminLife")
                {
                    txtcancel.EditValue = "반환 불가";
                }
                else
                {
                    txtcancel.EditValue = "반환 가능"; 
                }
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN25_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtQuery.EditValue;

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

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            {
                popup_member = new frmMember_info
                {
                    COMPANYCD = "YL01",
                    COMPANYNAME = "(주)와이엘랜드",
                    MEMBER_TYPE = "ALL",
                };
                popup_member.FormClosed += popup_FormClosed;
                popup_member.ShowDialog();
            }
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup_member.FormClosed -= popup_FormClosed;
            if (popup_member.DialogResult == DialogResult.OK)
            {
                this.txtLogin_Id.EditValue = popup_member.USER_ID;
                this.txtU_Name.EditValue = popup_member.U_NAME;
                this.txtU_Nickname.EditValue = popup_member.U_NICKNAME;
                this.txtU_Id.EditValue = popup_member.U_ID;
            }
            popup_member = null;
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            from_new();
        }

        private void from_new()
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCoidName.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 처리할 권한이 없습니다!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtU_Id.Text)) 
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtsend_message.Text)) 
            {
                MessageAgent.MessageShow(MessageType.Warning, " 보낸 메세지를 입력하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtreceive_message.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 받는 메세지를 입력하세요!");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN25_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            // 등록자
                            cmd.Parameters.Add(new MySqlParameter("i_coid_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_coid_no"].Value = txtCoidName.EditValue;
                            cmd.Parameters["i_coid_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_receiver", MySqlDbType.VarChar));
                            cmd.Parameters["i_receiver"].Value = txtU_Id.EditValue;
                            cmd.Parameters["i_receiver"].Direction = ParameterDirection.Input; 

                            cmd.Parameters.Add(new MySqlParameter("i_rec_login_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_rec_login_id"].Value = txtLogin_Id.EditValue;
                            cmd.Parameters["i_rec_login_id"].Direction = ParameterDirection.Input; 

                            cmd.Parameters.Add(new MySqlParameter("i_donut_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_donut_type"].Value = cmbDonut_Type.EditValue;
                            cmd.Parameters["i_donut_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_donut_count", MySqlDbType.VarChar));
                            cmd.Parameters["i_donut_count"].Value = Convert.ToInt32(txtdonut_count.EditValue);
                            cmd.Parameters["i_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_send_message", MySqlDbType.VarChar));
                            cmd.Parameters["i_send_message"].Value = txtsend_message.EditValue;
                            cmd.Parameters["i_send_message"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_receive_message", MySqlDbType.VarChar));
                            cmd.Parameters["i_receive_message"].Value = txtreceive_message.EditValue;
                            cmd.Parameters["i_receive_message"].Direction = ParameterDirection.Input;

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

        private void efwTextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
        // 저장
        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {

            if (txtr_login_id.EditValue.ToString() == "AdminLife")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 반환할수 없는 ID 입니다 ");
                return;
            }

            if (string.IsNullOrEmpty(this.txtCoidName.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 처리할 권한이 없습니다!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtsend_message_b.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 보낸 메세지를 입력하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtreceive_message_b.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 받는 메세지를 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtidx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }

            if (txtcontents_id.Text.Length != 0)
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이미 반환된 정산 내역입니다 ");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "반환 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN25_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            // 등록자
                            cmd.Parameters.Add(new MySqlParameter("i_coid_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_coid_no"].Value = txtCoidName.EditValue;
                            cmd.Parameters["i_coid_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_send_message", MySqlDbType.VarChar));
                            cmd.Parameters["i_send_message"].Value = txtsend_message_b.EditValue;
                            cmd.Parameters["i_send_message"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_receive_message", MySqlDbType.VarChar));
                            cmd.Parameters["i_receive_message"].Value = txtreceive_message_b.EditValue;
                            cmd.Parameters["i_receive_message"].Direction = ParameterDirection.Input;

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
    }
}
