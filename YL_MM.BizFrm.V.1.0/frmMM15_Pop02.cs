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
using YL_MM.BizFrm.Dlg;
using DevExpress.XtraGrid.Columns;


namespace YL_MM.BizFrm
{
    public partial class frmMM15_Pop02 : FrmPopUpBase
    {
        public int Id { get; set; }
        public string pm_name { get; set; }
        public frmMM15_Pop02()
        {
            InitializeComponent();
        }

        private void frmMM15_Pop02_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            txtPm_id.EditValue = Id;
            txtPm_Name.EditValue = pm_name;
            rbIs_Banner.EditValue = "N";
            SetCmb();
        }

        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select code_id as DCODE ,code_nm as DNAME  from domaadmin.tb_common_code  where gcode_id = '00028'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbDoma_Money, codeArray);
                CodeAgent.MakeCodeControl(this.cmbVip_Money, codeArray);
                CodeAgent.MakeCodeControl(this.cmbChef_Money, codeArray);
                CodeAgent.MakeCodeControl(this.cmbMd_Money, codeArray);
                CodeAgent.MakeCodeControl(this.cmbGshop_Money, codeArray);
            }
            cmbDoma_Money.EditValue = "0";
            cmbVip_Money.EditValue = "0";
            cmbChef_Money.EditValue = "0";
            cmbMd_Money.EditValue = "0";
            cmbGshop_Money.EditValue = "0";

        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {


            // 도마
            string sDoma_Rate = string.Empty;
            sDoma_Rate = string.IsNullOrEmpty(this.txtDoma_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtDoma_Rate.Text)) || (txtDoma_Rate.EditValue.ToString() == "0"))
            {
                sDoma_Rate = "0";
            }
            if ((sDoma_Rate == "0" && cmbDoma_Money.EditValue.ToString() != "0") || (sDoma_Rate != "0" && cmbDoma_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "일반도마 할인율과 사용머니를 선택하세요!");
                return;
            }

            // Vip
            string sVip_Rate = string.Empty;
            sVip_Rate = string.IsNullOrEmpty(this.txtVip_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtVip_Rate.Text)) || (txtVip_Rate.EditValue.ToString() == "0"))
            {
                sVip_Rate = "0";
            }
            if ((sVip_Rate == "0" && cmbDoma_Money.EditValue.ToString() != "0") || (sVip_Rate != "0" && cmbVip_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "VIP 할인율과 사용머니를 선택하세요!");
                return;
            }

            // Chef
            string sChef_Rate = string.Empty;
            sChef_Rate = string.IsNullOrEmpty(this.txtChef_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtChef_Rate.Text)) || (txtChef_Rate.EditValue.ToString() == "0"))
            {
                sChef_Rate = "0";
            }
            if ((sChef_Rate == "0" && cmbChef_Money.EditValue.ToString() != "0") || (sChef_Rate != "0" && cmbChef_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "G 매니저 할인율과 사용머니를 선택하세요!");
                return;
            }

            // Md
            string sMd_Rate = string.Empty;
            sMd_Rate = string.IsNullOrEmpty(this.txtMd_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtMd_Rate.Text)) || (txtMd_Rate.EditValue.ToString() == "0"))
            {
                sMd_Rate = "0";
            }
            if ((sMd_Rate == "0" && cmbMd_Money.EditValue.ToString() != "0") || (sMd_Rate != "0" && cmbMd_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "셰프이사 할인율과 사용머니를 선택하세요!");
                return;
            }

            // Gshop
            string sGshop_Rate = string.Empty;
            sGshop_Rate = string.IsNullOrEmpty(this.txtGshop_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtGshop_Rate.Text)) || (txtGshop_Rate.EditValue.ToString() == "0"))
            {
                sGshop_Rate = "0";
            }
            if ((sGshop_Rate == "0" && cmbGshop_Money.EditValue.ToString() != "0") || (sGshop_Rate != "0" && cmbGshop_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "G 멀티샵 할인율과 사용머니를 선택하세요!");
                return;
            }




            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_SAVE_04", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.Add(new MySqlParameter("i_Id", MySqlDbType.Int32));
                            cmd.Parameters["i_Id"].Value = Convert.ToInt32(txtPm_id.EditValue);
                            cmd.Parameters["i_Id"].Direction = ParameterDirection.Input;

                            // doma
                            int nDoma_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtDoma_Rate.Text))
                                nDoma_Rate = 0;
                            else
                                nDoma_Rate = Convert.ToInt32(txtDoma_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_doma_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_doma_rate"].Value = nDoma_Rate;
                            cmd.Parameters["i_doma_rate"].Direction = ParameterDirection.Input;

                            // Vip
                            int nVip_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtVip_Rate.Text))
                                nVip_Rate = 0;
                            else
                                nVip_Rate = Convert.ToInt32(txtVip_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_vip_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_vip_rate"].Value = nVip_Rate;
                            cmd.Parameters["i_vip_rate"].Direction = ParameterDirection.Input;

                            // Chef
                            int nChef_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtChef_Rate.Text))
                                nChef_Rate = 0;
                            else
                                nChef_Rate = Convert.ToInt32(txtChef_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_chef_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_chef_rate"].Value = nChef_Rate;
                            cmd.Parameters["i_chef_rate"].Direction = ParameterDirection.Input;

                            // MD
                            int nMd_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtMd_Rate.Text))
                                nMd_Rate = 0;
                            else
                                nMd_Rate = Convert.ToInt32(txtMd_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_md_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_md_rate"].Value = nMd_Rate;
                            cmd.Parameters["i_md_rate"].Direction = ParameterDirection.Input;

                            // Gshop
                            int nGshop_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtGshop_Rate.Text))
                                nGshop_Rate = 0;
                            else
                                nGshop_Rate = Convert.ToInt32(txtGshop_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_gshop_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_rate"].Value = nGshop_Rate;
                            cmd.Parameters["i_gshop_rate"].Direction = ParameterDirection.Input;



                            cmd.Parameters.Add(new MySqlParameter("i_doma_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_doma_money"].Value = cmbDoma_Money.EditValue;
                            cmd.Parameters["i_doma_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_vip_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_vip_money"].Value = cmbVip_Money.EditValue;
                            cmd.Parameters["i_vip_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_chef_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_chef_money"].Value = cmbChef_Money.EditValue;
                            cmd.Parameters["i_chef_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_money"].Value = cmbMd_Money.EditValue;
                            cmd.Parameters["i_md_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_gshop_money"].Value = cmbGshop_Money.EditValue;
                            cmd.Parameters["i_gshop_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_banner", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_banner"].Value = rbIs_Banner.EditValue;
                            cmd.Parameters["i_is_banner"].Direction = ParameterDirection.Input;


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
    }
}
