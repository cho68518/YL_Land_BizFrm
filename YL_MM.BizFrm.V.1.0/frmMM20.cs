using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Report;
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
using YL_MM.BizFrm.Dlg;

namespace YL_MM.BizFrm
{
    public partial class frmMM20 : FrmBase
    {
        frmMember_md popup_md;
        public frmMM20()
        {
            InitializeComponent();
            this.QCode = "MM20";
            //폼명설정
            this.FrmName = "권한 관리";

        }

        private void frmMM20_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;
            rbIs_Use.EditValue = "Y";
            rbGhop_Manager.EditValue = "N";
            rbTest_Grant.EditValue = "N";
            rbteam_leader.EditValue = "N";
            gridView1.OptionsView.ShowFooter = true;
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("u_id", txtU_Id)
                    , new ColumnControlSet("u_nickname", txtU_Nickname)
                    , new ColumnControlSet("u_name", txtU_Name)
                    , new ColumnControlSet("login_id", txtLogin_Id)
                    , new ColumnControlSet("is_use", rbIs_Use)
                    , new ColumnControlSet("ghop_manager", rbGhop_Manager)
                    , new ColumnControlSet("reg_date", txtReg_Date)
                    , new ColumnControlSet("test_grant", rbTest_Grant)
                    , new ColumnControlSet("team_leader", rbteam_leader)
                    , new ColumnControlSet("Remark", txtRemark));
            SetCmb();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
        }

        private void SetCmb()
        {
            // 회원검색

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domabiz.tb_member_grant_master group by code_id, code_nm  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbMember_Search, codeArray);
            }

            cmbMember_Search.EditValue = "4";

        }


        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();  // APP 체험샵 노출 가능 MD
            }

        }

        public void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM20_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtSearch.EditValue;

                        cmd.Parameters.Add("i_code_id", MySqlDbType.Int32, 10);
                        cmd.Parameters[1].Value = Convert.ToInt32(cmbMember_Search.EditValue);

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //    this.efwGridControl1.MyGridView.BestFitColumns();

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
            popup_md = new frmMember_md
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
            };
            popup_md.FormClosed += popup_FormClosed;
            popup_md.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup_md.FormClosed -= popup_FormClosed;
            if (popup_md.DialogResult == DialogResult.OK)
            {
                this.txtLogin_Id.EditValue = popup_md.USER_ID;
                this.txtU_Name.EditValue = popup_md.U_NAME;
                this.txtU_Nickname.EditValue = popup_md.U_NICKNAME;
                this.txtU_Id.EditValue = popup_md.U_ID;
            }
            popup_md = null;
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtU_Id.Text))
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
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM20_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "P";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_id"].Value = Convert.ToInt32(cmbMember_Search.EditValue);
                            cmd.Parameters["i_code_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_Id.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = rbIs_Use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ghop_manager", MySqlDbType.VarChar));
                            cmd.Parameters["i_ghop_manager"].Value = rbGhop_Manager.EditValue;
                            cmd.Parameters["i_ghop_manager"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_test_grant", MySqlDbType.VarChar));
                            cmd.Parameters["i_test_grant"].Value = rbTest_Grant.EditValue;
                            cmd.Parameters["i_test_grant"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_team_leader", MySqlDbType.VarChar));
                            cmd.Parameters["i_team_leader"].Value = rbteam_leader.EditValue;
                            cmd.Parameters["i_team_leader"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

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

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            from_new();
            rbIs_Use.EditValue = "Y";
            rbGhop_Manager.EditValue = "N";
            rbTest_Grant.EditValue = "N";
        }
        private void from_new()
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtU_Id.Text))
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
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM20_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "D";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_id"].Value = Convert.ToInt32(cmbMember_Search.EditValue);
                            cmd.Parameters["i_code_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_Id.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = rbIs_Use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ghop_manager", MySqlDbType.VarChar));
                            cmd.Parameters["i_ghop_manager"].Value = rbGhop_Manager.EditValue;
                            cmd.Parameters["i_ghop_manager"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_test_grant", MySqlDbType.VarChar));
                            cmd.Parameters["i_test_grant"].Value = rbTest_Grant.EditValue;
                            cmd.Parameters["i_test_grant"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

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
