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
    public partial class frmMM16 : FrmBase
    {
        public frmMM16()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MM16";
            //폼명설정
            this.FrmName = "통합회원 정보";

        }

        private void frmMM16_Load(object sender, EventArgs e)
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


            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("id", txtID)
            , new ColumnControlSet("pwd", txtPWD)
            , new ColumnControlSet("name", txtNAME)
            , new ColumnControlSet("nickname", txtNICKNAME)
            , new ColumnControlSet("birth", txtBIRTH)
            , new ColumnControlSet("hpno", txtHPNO)
            , new ColumnControlSet("gender", rbGENDER)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;


            cmbQ1.EditValue = "1";
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
        }

        public override void Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM16_SELECT_01"
                    , this.cmbQ1.EditValue
                    , this.txtSearch.Text
                    );

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }


        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }

            txtO_U_ID.EditValue = "";
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.Member_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("DB_MEMBER.USP_MM_MM16_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_id"].Value = txtID.EditValue;
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pwd", MySqlDbType.VarChar));
                            cmd.Parameters["i_pwd"].Value = txtPWD.EditValue;
                            cmd.Parameters["i_pwd"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_name"].Value = txtNAME.EditValue;
                            cmd.Parameters["i_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_nickname", MySqlDbType.VarChar));
                            cmd.Parameters["i_nickname"].Value = txtNICKNAME.EditValue;
                            cmd.Parameters["i_nickname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_birth", MySqlDbType.VarChar));
                            cmd.Parameters["i_birth"].Value = txtBIRTH.EditValue;
                            cmd.Parameters["i_birth"].Direction = ParameterDirection.Input;
                     
                            cmd.Parameters.Add(new MySqlParameter("i_hpno", MySqlDbType.VarChar));
                            cmd.Parameters["i_hpno"].Value = txtHPNO.EditValue;
                            cmd.Parameters["i_hpno"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gender", MySqlDbType.VarChar));
                            cmd.Parameters["i_gender"].Value = rbGENDER.EditValue;
                            cmd.Parameters["i_gender"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["o_u_id"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            txtO_U_ID.EditValue = cmd.Parameters["o_u_id"].Value.ToString();

                            string sU_ID = string.Empty;
                            sU_ID = txtO_U_ID.EditValue.ToString();

                            if (sU_ID == "N")
                               MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                            else
                                db_member();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }

            }
        }

        private void db_member()
        {
            try
            {
                using (MySqlConnection con2 = new MySqlConnection(ConstantLib.BasicConn_Real))
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.Member_Real))
                {
                    using (MySqlCommand cmd2 = new MySqlCommand("domalife.USP_MM_MM16_SAVE_02", con2))
                    {
                        con2.Open();
                        cmd2.CommandType = CommandType.StoredProcedure;

                        cmd2.Parameters.Add(new MySqlParameter("i_o_u_id", MySqlDbType.VarChar));
                        cmd2.Parameters["i_o_u_id"].Value = txtO_U_ID.EditValue;
                        cmd2.Parameters["i_o_u_id"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.VarChar));
                        cmd2.Parameters["i_id"].Value = txtID.EditValue;
                        cmd2.Parameters["i_id"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_pwd", MySqlDbType.VarChar));
                        cmd2.Parameters["i_pwd"].Value = txtPWD.EditValue;
                        cmd2.Parameters["i_pwd"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                        cmd2.Parameters["i_name"].Value = txtNAME.EditValue;
                        cmd2.Parameters["i_name"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_nickname", MySqlDbType.VarChar));
                        cmd2.Parameters["i_nickname"].Value = txtNICKNAME.EditValue;
                        cmd2.Parameters["i_nickname"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_birth", MySqlDbType.VarChar));
                        cmd2.Parameters["i_birth"].Value = txtBIRTH.EditValue;
                        cmd2.Parameters["i_birth"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_hpno", MySqlDbType.VarChar));
                        cmd2.Parameters["i_hpno"].Value = txtHPNO.EditValue;
                        cmd2.Parameters["i_hpno"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_gender", MySqlDbType.VarChar));
                        cmd2.Parameters["i_gender"].Value = rbGENDER.EditValue;
                        cmd2.Parameters["i_gender"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                        cmd2.Parameters["o_Return"].Direction = ParameterDirection.Output;

                        cmd2.ExecuteNonQuery();

                        MessageBox.Show(cmd2.Parameters["o_Return"].Value.ToString());
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
