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
using YL_SCM.BizFrm.Dlg;


namespace YL_SCM.BizFrm
{
    public partial class frmSCM03_Pop01 : FrmPopUpBase
    {
        public Int32 pOrderNo { get; set; }

        public frmSCM03_Pop01()
        {
            InitializeComponent();
        }

        private void frmSCM03_Pop01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            txtCallBack.EditValue = "1644-5646";
            txtCallBack_Name.EditValue = "(주)와이엘랜드";
            txtOrderNo.EditValue = pOrderNo;

            SetCmb();

        }

        private void SetCmb()
        {
            // 공급자구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(s_idx,'') as DCODE ,s_company_name as DNAME  FROM domaadmin.tb_sellers_info where s_status = 'Y' order by s_company_name ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbSellers, codeArray);
            }

            cmbSellers.EditValue = "1";
            txtMSG.Focus();
        }

        private void bthSendSMS_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtOrderNo.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "주문 번호를 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "SMS 문자 전송을 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_SMS))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("DB_SMS.USP_SCM_SCM03_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_sellers", MySqlDbType.VarChar));
                            cmd.Parameters["i_sellers"].Value = cmbSellers.EditValue;
                            cmd.Parameters["i_sellers"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_phone", MySqlDbType.VarChar));
                            cmd.Parameters["i_phone"].Value = txtPhone.EditValue;
                            cmd.Parameters["i_phone"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_callback", MySqlDbType.VarChar));
                            cmd.Parameters["i_callback"].Value = txtCallBack.EditValue;
                            cmd.Parameters["i_callback"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_orderno", MySqlDbType.VarChar));
                            cmd.Parameters["i_orderno"].Value = txtOrderNo.EditValue;
                            cmd.Parameters["i_orderno"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_msg", MySqlDbType.VarChar));
                            cmd.Parameters["i_msg"].Value = txtMSG.EditValue;
                            cmd.Parameters["i_msg"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Informational, "문자 전송이 완료되었습니다.");
                }
                
            }
        }
    }
}
