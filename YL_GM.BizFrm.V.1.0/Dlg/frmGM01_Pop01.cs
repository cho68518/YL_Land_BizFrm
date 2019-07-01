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

namespace YL_GM.BizFrm.Dlg
{
    public partial class frmGM01_Pop01 : FrmPopUpBase
    {
        public string pCOMPANYCD { get; set; }

        public frmGM01_Pop01()
        {
            InitializeComponent();
        }

        private void FrmGM01_Pop01_Load(object sender, EventArgs e)
        {
            this.Text = "텔레콤 총누적 기초데이타";

            Open();
        }

        private void Open()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("telecom.USP_GM_GM01_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_idx", MySqlDbType.Int32, 15);
                        cmd.Parameters[0].Value = 1;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                txtLG_CNT1.EditValue = dt.Rows[0]["lg_bas_cnt1"];
                                txtLG_CNT2.EditValue = dt.Rows[0]["lg_bas_cnt2"];
                                txtLG_CNT3.EditValue = dt.Rows[0]["lg_bas_cnt3"];

                                txtKT_CNT1.EditValue = dt.Rows[0]["kt_bas_cnt1"];
                                txtKT_CNT2.EditValue = dt.Rows[0]["kt_bas_cnt2"];
                            }
                            else
                            {
                                txtLG_CNT1.EditValue = 0;
                                txtLG_CNT2.EditValue = 0;
                                txtLG_CNT3.EditValue = 0;

                                txtKT_CNT1.EditValue = 0;
                                txtKT_CNT2.EditValue = 0;
                            }
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
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("telecom.USP_GM01_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_lg_cnt1", MySqlDbType.Int32, 15);
                            cmd.Parameters[0].Value = Convert.ToInt32(txtLG_CNT1.EditValue);

                            cmd.Parameters.Add("i_lg_cnt2", MySqlDbType.Int32, 15);
                            cmd.Parameters[1].Value = Convert.ToInt32(txtLG_CNT2.EditValue);

                            cmd.Parameters.Add("i_lg_cnt3", MySqlDbType.Int32, 15);
                            cmd.Parameters[2].Value = Convert.ToInt32(txtLG_CNT3.EditValue);

                            cmd.Parameters.Add("i_kt_cnt1", MySqlDbType.Int32, 15);
                            cmd.Parameters[3].Value = Convert.ToInt32(txtKT_CNT1.EditValue);

                            cmd.Parameters.Add("i_kt_cnt2", MySqlDbType.Int32, 15);
                            cmd.Parameters[4].Value = Convert.ToInt32(txtKT_CNT2.EditValue);

                            cmd.ExecuteNonQuery();

                            MessageAgent.MessageShow(MessageType.Informational, "처리되었습니다!");
                            con.Close();

                            Open();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
