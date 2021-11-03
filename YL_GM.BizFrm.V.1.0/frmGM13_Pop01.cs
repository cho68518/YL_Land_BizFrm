using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using YL_GM.BizFrm.Dlg;

namespace YL_GM.BizFrm
{
    public partial class frmGM13_Pop01 : FrmPopUpBase
    {

        public string pO_Code { get; set; }
        public string pShop_Code { get; set; }
        public string pStory_208 { get; set; }
        public string pStory_221 { get; set; }
        public string pStory_248 { get; set; }
        public string pStory_232 { get; set; }
        public string pStory_247 { get; set; }
        public string pStory_244 { get; set; }
        public string pStory_223 { get; set; }
        public string pStory_243 { get; set; }

        public string pU_NickNAme { get; set; }
        public string pU_Chef_Level{ get; set; }
        public string pDoma { get; set; }
        public string pVip { get; set; }
        public string pBiz { get; set; }

        public frmGM13_Pop01()
        {
            InitializeComponent();
        }

        private void frmGM13_Pop01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            txtO_Code.EditValue = pO_Code;
            txtGShop_O_Code.EditValue = pShop_Code;
            txtStory_208.EditValue = pStory_208;
            txtStory_221.EditValue = pStory_221;
            txtStory_248.EditValue = pStory_248;
            txtStory_232.EditValue = pStory_232;
            txtStory_247.EditValue = pStory_247;
            txtStory_244.EditValue = pStory_244;
            txtStory_223.EditValue = pStory_223;
            txtStory_243.EditValue = pStory_243;

            txtU_NickName.EditValue = pU_NickNAme;
            txtU_Chef_Level.EditValue = pU_Chef_Level;
            txtDoma.EditValue = pDoma;
            txtVip.EditValue = pVip;
            txtBiz.EditValue = pBiz;

            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("idx", txtIdx)
                    , new ColumnControlSet("contents_name", txtContents_Name)
                    , new ColumnControlSet("expiration_date", dtExpiration_Date)
                    , new ColumnControlSet("is_write", txtIs_Write)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;

            Open1();
        }
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                this.txtIdx.EditValue = dr["idx"].ToString();
                this.dtExpiration_Date.EditValue = dr["expiration_date"].ToString();
            }
        }

        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM13_POP01_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtO_Code.EditValue;

                        cmd.Parameters.Add("i_gshop_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtGShop_O_Code.EditValue;

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

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (txtStory_221.EditValue.ToString() == "O")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이미 생성된 스토리 입니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "PS축하 스토리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PROD_ORDER_221", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (txtStory_208.EditValue.ToString() == "O")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이미 생성된 스토리 입니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "PS후기 스토리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PROD_ORDER_208", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            if (txtStory_248.EditValue.ToString() == "O")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이미 생성된 스토리 입니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "GD축하 스토리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PROD_ORDER_248", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            if (txtStory_232.EditValue.ToString() == "O")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이미 생성된 스토리 입니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "알뜰지원 스토리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PROD_ORDER_232", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (txtStory_247.EditValue.ToString() == "O")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이미 생성된 스토리 입니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "GV축하 스토리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PROD_ORDER_247", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            if (txtStory_244.EditValue.ToString() == "O")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이미 생성된 스토리 입니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "GM축하 스토리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PROD_ORDER_244", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            if (txtIs_Write.EditValue.ToString() == "Y")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 작성된 스토리는 삭제할수 없습니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "스토리를 삭제하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM13_POP01_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            if (txtIs_Write.EditValue.ToString() == "Y")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 작성된 스토리는 변경할수 없습니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "작성기간을 조정 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM13_POP01_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_expiration_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_expiration_date"].Value = dtExpiration_Date.EditValue;
                            cmd.Parameters["i_expiration_date"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }

        private void efwSimpleButton10_Click(object sender, EventArgs e)
        {
            if (txtStory_223.EditValue.ToString() == "O")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이미 생성된 스토리 입니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "PR등록 스토리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PROD_ORDER_223", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtGShop_O_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            if (txtStory_243.EditValue.ToString() == "O")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이미 생성된 스토리 입니다");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "PR추천 스토리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PROD_ORDER_243", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtGShop_O_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "완료 되었습니다. 조회 결과를 확인하세요.");
                Open1();
            }
        }
    }
}
