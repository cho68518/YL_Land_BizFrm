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
    public partial class frmDN01_Pop01 : FrmPopUpBase
    {
        public int Id { get; set; }
        public frmDN01_Pop01()
        {
            InitializeComponent();
        }

        private void frmDN01_Pop01_Load(object sender, EventArgs e)
        {
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            txtID.EditValue = Id;
            Open1();
        }

        private void Open1()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN01_SELECT_02", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                        cmd.Parameters["i_id"].Value = Convert.ToInt32(txtID.EditValue);
                        cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_o_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_type"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_code", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_code"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_u_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_u_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_date", MySqlDbType.DateTime));
                        cmd.Parameters["o_o_date"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_zipcode", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_zipcode"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_address", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_address"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_contact", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_contact"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_message", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_message"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_zipcode1", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_zipcode1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_address1", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_address1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_name1", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_name1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_contact1", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_contact1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_receive_message1", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_receive_message1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_total_cost", MySqlDbType.Int32));
                        cmd.Parameters["o_o_total_cost"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_delivery_cost", MySqlDbType.Int32));
                        cmd.Parameters["o_o_delivery_cost"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_donut_d_cost", MySqlDbType.Int32));
                        cmd.Parameters["o_o_donut_d_cost"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_donut_m_cost", MySqlDbType.Int32));
                        cmd.Parameters["o_o_donut_m_cost"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_donut_c_cost", MySqlDbType.Int32));
                        cmd.Parameters["o_o_donut_c_cost"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_purchase_cost", MySqlDbType.Int32));
                        cmd.Parameters["o_o_purchase_cost"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_deposit_confirm_date", MySqlDbType.DateTime));
                        cmd.Parameters["o_o_deposit_confirm_date"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_cancel_date", MySqlDbType.DateTime));
                        cmd.Parameters["o_o_cancel_date"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_complete_date", MySqlDbType.DateTime));
                        cmd.Parameters["o_o_complete_date"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_return_success_date", MySqlDbType.DateTime));
                        cmd.Parameters["o_o_return_success_date"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_remark", MySqlDbType.VarChar));
                        cmd.Parameters["o_remark"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_return_reason_detail", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_return_reason_detail"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_code", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_code"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_p_id", MySqlDbType.Int32));
                        cmd.Parameters["o_p_p_id"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_pp_title", MySqlDbType.VarChar));
                        cmd.Parameters["o_pp_title"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_num", MySqlDbType.Int32));
                        cmd.Parameters["o_p_num"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_price", MySqlDbType.Int32));
                        cmd.Parameters["o_p_price"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_response_code", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_response_code"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_response_msg", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_response_msg"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_amount", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_amount"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_tid", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_tid"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_paytype", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_paytype"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_paydate", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_paydate"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_financecode", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_financecode"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_financename", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_financename"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_accountnum", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_accountnum"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_accountowner", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_accountowner"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_payer", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_payer"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_castamount", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_castamount"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_cascamount", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_cascamount"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_casflag", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_casflag"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_casseqno", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_casseqno"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lgd_saowner", MySqlDbType.VarChar));
                        cmd.Parameters["o_lgd_saowner"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_delivery_num", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_delivery_num"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_delivery_comp_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_o_delivery_comp_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_delivery_start_date", MySqlDbType.DateTime));
                        cmd.Parameters["o_o_delivery_start_date"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_o_delivery_end_date", MySqlDbType.DateTime));
                        cmd.Parameters["o_o_delivery_end_date"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_id", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_id"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        txtO_Type.EditValue = cmd.Parameters["o_o_type"].Value.ToString();
                        txtO_Code.EditValue = cmd.Parameters["o_o_code"].Value.ToString();
                        txtU_Name.EditValue = cmd.Parameters["o_u_name"].Value.ToString();
                        dtO_Date.EditValue = cmd.Parameters["o_o_date"].Value.ToString();
                        txtO_Receive_ZipCode.EditValue = cmd.Parameters["o_o_receive_zipcode"].Value.ToString();
                        txtO_Receive_Address.EditValue = cmd.Parameters["o_o_receive_address"].Value.ToString();
                        txtO_Receive_Name.EditValue = cmd.Parameters["o_o_receive_name"].Value.ToString();
                        txtO_Receive_Contact.EditValue = cmd.Parameters["o_o_receive_contact"].Value.ToString();
                        txtO_Receive_Message.EditValue = cmd.Parameters["o_o_receive_message"].Value.ToString();
                        txtO_Receive_ZipCode1.EditValue = cmd.Parameters["o_o_receive_zipcode1"].Value.ToString();
                        txtO_Receive_Address1.EditValue = cmd.Parameters["o_o_receive_address1"].Value.ToString();
                        txtO_Receive_Name1.EditValue = cmd.Parameters["o_o_receive_name1"].Value.ToString();
                        txtO_Receive_Contact1.EditValue = cmd.Parameters["o_o_receive_contact1"].Value.ToString();
                        txtO_Receive_Message1.EditValue = cmd.Parameters["o_o_receive_message1"].Value.ToString();
                        txtO_Total_Cost.EditValue = cmd.Parameters["o_o_total_cost"].Value.ToString();
                        txtO_Delivery_Cost.EditValue = cmd.Parameters["o_o_delivery_cost"].Value.ToString();
                        txtO_Donut_D_Cost.EditValue = cmd.Parameters["o_o_donut_d_cost"].Value.ToString();
                        txtO_Donut_M_Cost.EditValue = cmd.Parameters["o_o_donut_m_cost"].Value.ToString();
                        txtO_Donut_C_Cost.EditValue = cmd.Parameters["o_o_donut_c_cost"].Value.ToString();
                        txtO_Purchase_Cost.EditValue = cmd.Parameters["o_o_purchase_cost"].Value.ToString();
                        txtO_Deposit_Confirm_Date.EditValue = cmd.Parameters["o_o_deposit_confirm_date"].Value.ToString();
                        txtO_Cancel_Date.EditValue = cmd.Parameters["o_o_cancel_date"].Value.ToString();
                        txtO_Complete_Date.EditValue = cmd.Parameters["o_o_complete_date"].Value.ToString();
                        txtO_Return_Success_Date.EditValue = cmd.Parameters["o_o_return_success_date"].Value.ToString();
                        txtRemark.EditValue = cmd.Parameters["o_remark"].Value.ToString();
                        txtO_Return_Reason_Detail.EditValue = cmd.Parameters["o_o_return_reason_detail"].Value.ToString();
                        txtP_Code.EditValue = cmd.Parameters["o_p_code"].Value.ToString();
                        txtP_Name.EditValue = cmd.Parameters["o_p_name"].Value.ToString();
                        txtP_P_Id.EditValue = cmd.Parameters["o_p_p_id"].Value.ToString();
                        txtPP_Title.EditValue = cmd.Parameters["o_pp_title"].Value.ToString();
                        txtP_num.EditValue = cmd.Parameters["o_p_num"].Value.ToString();
                        txtP_Price.EditValue = cmd.Parameters["o_p_price"].Value.ToString();
                        txtLGD_Response_Code.EditValue = cmd.Parameters["o_lgd_response_code"].Value.ToString();
                        txtLGD_Response_Msg.EditValue = cmd.Parameters["o_lgd_response_msg"].Value.ToString();
                        txtLGD_Amount.EditValue = cmd.Parameters["o_lgd_amount"].Value.ToString();
                        txtLGD_Tid.EditValue = cmd.Parameters["o_lgd_tid"].Value.ToString();
                        txtLGD_PayType.EditValue = cmd.Parameters["o_lgd_paytype"].Value.ToString();
                        txtLGD_PayDate.EditValue = cmd.Parameters["o_lgd_paydate"].Value.ToString();
                        txtLGD_FinanceCode.EditValue = cmd.Parameters["o_lgd_financecode"].Value.ToString();
                        txtLGD_FinanceName.EditValue = cmd.Parameters["o_lgd_financename"].Value.ToString();
                        txtLGD_AccountNum.EditValue = cmd.Parameters["o_lgd_accountnum"].Value.ToString();
                        txtLGD_AccountOwner.EditValue = cmd.Parameters["o_lgd_accountowner"].Value.ToString();
                        txtLGD_Payer.EditValue = cmd.Parameters["o_lgd_payer"].Value.ToString();
                        txtLGD_CastAmount.EditValue = cmd.Parameters["o_lgd_castamount"].Value.ToString();
                        txtLGD_CascAmount.EditValue = cmd.Parameters["o_lgd_cascamount"].Value.ToString();
                        txtLGD_CasFlag.EditValue = cmd.Parameters["o_lgd_casflag"].Value.ToString();
                        txtLGD_CasseqNo.EditValue = cmd.Parameters["o_lgd_casseqno"].Value.ToString();
                        txtLGD_SaOwner.EditValue = cmd.Parameters["o_lgd_saowner"].Value.ToString();
                        txtO_Delivery_Num.EditValue = cmd.Parameters["o_o_delivery_num"].Value.ToString();
                        txtO_Delivery_Comp_Name.EditValue = cmd.Parameters["o_o_delivery_comp_name"].Value.ToString();
                        txtO_Delivery_Start_Date.EditValue = cmd.Parameters["o_o_delivery_start_date"].Value.ToString();
                        txtO_Delivery_End_Date.EditValue = cmd.Parameters["o_o_delivery_end_date"].Value.ToString();
                        txtP_ID.EditValue = cmd.Parameters["o_p_id"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN01_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                            cmd.Parameters["i_id"].Value = Convert.ToInt32(txtID.EditValue);
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_zipcode", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_zipcode"].Value = txtO_Receive_ZipCode.EditValue;
                            cmd.Parameters["i_o_receive_zipcode"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_address", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_address"].Value = txtO_Receive_Address.EditValue;
                            cmd.Parameters["i_o_receive_address"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_name"].Value = txtO_Receive_Name.EditValue;
                            cmd.Parameters["i_o_receive_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_contact", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_contact"].Value = txtO_Receive_Contact.EditValue;
                            cmd.Parameters["i_o_receive_contact"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_message", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_message"].Value = txtO_Receive_Message.EditValue;
                            cmd.Parameters["i_o_receive_message"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_zipcode1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_zipcode1"].Value = txtO_Receive_ZipCode1.EditValue;
                            cmd.Parameters["i_o_receive_zipcode1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_address1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_address1"].Value = txtO_Receive_Address1.EditValue;
                            cmd.Parameters["i_o_receive_address1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_name1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_name1"].Value = txtO_Receive_Name1.EditValue;
                            cmd.Parameters["i_o_receive_name1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_contact1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_contact1"].Value = txtO_Receive_Contact1.EditValue;
                            cmd.Parameters["i_o_receive_contact1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_message1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_message1"].Value = txtO_Receive_Message1.EditValue;
                            cmd.Parameters["i_o_receive_message1"].Direction = ParameterDirection.Input;

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
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void Cancel()
        {

            if (MessageAgent.MessageShow(MessageType.Confirm, "취소/반품 승인을 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN01_SAVE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                            cmd.Parameters["i_id"].Value = Convert.ToInt32(txtID.EditValue);
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }

                Open1();
            }



        }



    }
}
