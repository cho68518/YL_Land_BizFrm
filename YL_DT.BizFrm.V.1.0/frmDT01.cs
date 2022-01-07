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
using YL_COMM.BizFrm;
using YL_DT.BizFrm.Dlg;

namespace YL_DT.BizFrm
{
    public partial class frmDT01 : FrmBase
    {
        frmMemberInfo popup;
        public frmDT01()
        {
            InitializeComponent();
            this.QCode = "DT01";
            //폼명설정
            this.FrmName = "머니이체 등록";
        }

        private void frmDT01_Load(object sender, EventArgs e)
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
            txtYN.Text = "N";
            SetCmb();
        }

        private void SetCmb()
        {
            // 회원검색

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00047' and code_id != 'AD' order by seq  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbDonut_Type, codeArray);
                CodeAgent.MakeCodeControl(this.cmbDonut_Type1, codeArray);
            }

            cmbDonut_Type.EditValue = "0";
            cmbDonut_Type1.EditValue = "0";
        }
        private void MoneySet()
        {
            string strQuery = string.Format(@" SELECT isnull(SUM(DM_MONTY),0) AS DM_MONTY, isnull(SUM(GD_MONTY),0) AS GD_MONTY, isnull(SUM(TD_MONTY),0) AS TD_MONTY FROM
                                                 (SELECT isnull(CASE WHEN MONEY_TYPE = 'DM'  THEN AMOUNT END,0) AS DM_MONTY,
                                                         isnull(CASE WHEN MONEY_TYPE = 'GD'  THEN AMOUNT END,0) AS GD_MONTY,
                                                         isnull(CASE WHEN MONEY_TYPE = 'TD'  THEN AMOUNT END,0) AS TD_MONTY
                                                    FROM YEOYOU_MONEY.dbo.TB_MILEAGE_NOW 
                                                   WHERE USER_ID =  '" + txtLogin_Id.EditValue.ToString() + "' AND AMOUNT > 0) T1 ");

            DataSet ds = ServiceAgent.ExecuteDataSetStr("CONIS_IBS", strQuery);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                lblTD_MONEY.Text = string.Format("{0:#,##0}", ds.Tables[0].Rows[0]["TD_MONTY"]);
                lblGD_MONEY.Text = string.Format("{0:#,##0}", ds.Tables[0].Rows[0]["GD_MONTY"]);
                lblDM_MONEY.Text = string.Format("{0:#,##0}", ds.Tables[0].Rows[0]["DM_MONTY"]);
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT01_SELECT_01", con))
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
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
            };
            popup.FormClosed += popup_FormClosed1;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtU_Id.Text = popup.U_ID;
                this.txtU_Name.Text = popup.U_NAME;
                this.txtU_Nickname.EditValue = popup.U_NICKNAME;
                this.txtLogin_Id.EditValue = popup.USER_ID;

                this.txtU_Id1.Text = popup.U_ID;
                this.txtU_Name1.Text = popup.U_NAME;
                this.txtU_Nickname1.EditValue = popup.U_NICKNAME;
                this.txtLogin_Id1.EditValue = popup.USER_ID;

                MoneySet();
            }
            popup = null;
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
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

                this.txtU_Id1.Text = popup.U_ID;
                this.txtU_Name1.Text = popup.U_NAME;
                this.txtU_Nickname1.EditValue = popup.U_NICKNAME;
                this.txtLogin_Id1.EditValue = popup.USER_ID;

                MoneySet();
            }
            popup = null;
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            this.txtU_Id.Text = "";
            this.txtU_Name.Text = "";
            this.txtU_Nickname.EditValue = "";
            this.txtLogin_Id.EditValue = "";

            this.txtU_Id1.Text = "";
            this.txtU_Name1.Text = "";
            this.txtU_Nickname1.EditValue = "";
            this.txtLogin_Id1.EditValue = "";

            txtdonut_count.EditValue = 0;
            txtsend_message.EditValue = "머니이체 보냄";
            txtreceive_message.EditValue = "머니이체 받음";
            txtYN.Text = "N";
            cmbDonut_Type.EditValue = "0";
            cmbDonut_Type1.EditValue = "0";

        }

        private void cmbDonut_Type_EditValueChanged(object sender, EventArgs e)
        {
            txtsend_message.EditValue =" 머니이체 보냄";
        }

        private void cmbDonut_Type1_EditValueChanged(object sender, EventArgs e)
        {
            txtsend_message.EditValue = cmbDonut_Type.EditValue.ToString() + " 머니 전환으로 " + cmbDonut_Type1.EditValue.ToString() + " 머니 이체보냄";
            txtreceive_message.EditValue = cmbDonut_Type.EditValue.ToString() + " 머니 전환으로 " + cmbDonut_Type1.EditValue.ToString() + " 머니 이체받음";
        }
        // 저장
        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if  (txtYN.EditValue.ToString() == "Y")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이체 처리 되었습니다. 신규 버튼을 눌른후 다시 저장 하세요. ");
                return;
            }
            if (cmbDonut_Type.EditValue.ToString() == "0")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 차감 머니 종류를 선택하세요. ");
                return;
            }
            if (cmbDonut_Type1.EditValue.ToString() == "0")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 적립 머니 종류를 선택하세요. ");
                return;
            }

            string shold_money;
            if (cmbDonut_Type.EditValue.ToString() == "GD")
                shold_money = lblGD_MONEY.Text;
            else if (cmbDonut_Type.EditValue.ToString() == "TD")
                shold_money = lblTD_MONEY.Text;
            else
                shold_money = lblDM_MONEY.Text;

            if  ( Convert.ToInt32(shold_money.Replace(",", "")) < Convert.ToInt32(txtdonut_count.EditValue.ToString().Replace(",", "")))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 보유금액보다 이체할 금액이 더 많습니다. ");
                return;
            }    

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT01_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_sender", MySqlDbType.VarChar));
                            cmd.Parameters["i_sender"].Value = txtU_Id.EditValue;
                            cmd.Parameters["i_sender"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_send_login_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_send_login_id"].Value = txtLogin_Id.EditValue;
                            cmd.Parameters["i_send_login_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_send_donut_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_send_donut_type"].Value = cmbDonut_Type.EditValue;
                            cmd.Parameters["i_send_donut_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_receiver", MySqlDbType.VarChar));
                            cmd.Parameters["i_receiver"].Value = txtU_Id1.EditValue;
                            cmd.Parameters["i_receiver"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_rec_login_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_rec_login_id"].Value = txtLogin_Id1.EditValue;
                            cmd.Parameters["i_rec_login_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_rec_donut_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_rec_donut_type"].Value = cmbDonut_Type1.EditValue;
                            cmd.Parameters["i_rec_donut_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_donut_count"].Value = Convert.ToInt32(txtdonut_count.EditValue);
                            cmd.Parameters["i_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hold_money", MySqlDbType.Int32));
                            cmd.Parameters["i_hold_money"].Value = Convert.ToInt32(shold_money.Replace(",", ""));
                            cmd.Parameters["i_hold_money"].Direction = ParameterDirection.Input;

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
                txtYN.Text = "Y";
                Search();
                MoneySet();
            }
        }
    }
}
