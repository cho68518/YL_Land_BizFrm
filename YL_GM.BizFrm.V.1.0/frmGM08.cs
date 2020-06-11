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


namespace YL_GM.BizFrm
{
    public partial class frmGM08 : FrmBase
    {
        public frmGM08()
        {
            InitializeComponent();
            this.QCode = "GM08";
            //폼명설정
            this.FrmName = "상담 대응방안 등록";
        }


        private void frmGM08_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            txtGCode_Nm.EditValue = "고객상담 문자";
            txtGCode_Nm1.EditValue= "제품설명";
            txtGCode_Nm2.EditValue = "질문과답변";
            txtGcode_Id.EditValue = "00038";
            txtGcode_Id1.EditValue = "00039";
            txtGcode_Id2.EditValue = "00040";

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            this.efwGridControl2.BindControlSet(
                      new ColumnControlSet("seq", txtSeq)
                    , new ColumnControlSet("gcode_id", txtGcode_Id)
                    , new ColumnControlSet("code_id", txtCode_Id)
                    , new ColumnControlSet("code_nm", txtCode_NM)
                    , new ColumnControlSet("code_memo", txtCode_Memo)
                      );

            this.efwGridControl2.Click += efwGridControl2_Click;


            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("seq", txtSeq1)
                    , new ColumnControlSet("gcode_id", txtGcode_Id1)
                    , new ColumnControlSet("code_id", txtCode_Id1)
                    , new ColumnControlSet("code_nm", txtCode_NM1)
                    , new ColumnControlSet("code_memo", txtCode_Memo1)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;

            this.efwGridControl3.BindControlSet(
                      new ColumnControlSet("seq", txtSeq2)
                    , new ColumnControlSet("gcode_id", txtGcode_Id2)
                    , new ColumnControlSet("code_id", txtCode_Id2)
                    , new ColumnControlSet("code_nm", txtCode_NM2)
                    , new ColumnControlSet("code_memo", txtCode_Memo2)
                      );

            this.efwGridControl3.Click += efwGridControl3_Click;
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
        }
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
        }
        private void efwGridControl3_Click(object sender, EventArgs e)
        {
        }
        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();  // 문자전송
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();  
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();  
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM08_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            //  this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }
        public void Open2()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM08_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //  this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        public void Open3()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM08_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            //  this.efwGridControl2.MyGridView.BestFitColumns();

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
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM08_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_seq", MySqlDbType.Int32));
                            cmd.Parameters["i_seq"].Value = Convert.ToInt32(txtSeq.EditValue);
                            cmd.Parameters["i_seq"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gcode_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_gcode_id"].Value = txtGcode_Id.EditValue;
                            cmd.Parameters["i_gcode_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gcode_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_gcode_nm"].Value = txtGCode_Nm.EditValue;
                            cmd.Parameters["i_gcode_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_id"].Value = txtCode_Id.EditValue;
                            cmd.Parameters["i_code_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_nm"].Value = txtCode_NM.EditValue;
                            cmd.Parameters["i_code_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_memo", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_memo"].Value = txtCode_Memo.EditValue;
                            cmd.Parameters["i_code_memo"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_return", MySqlDbType.VarChar));
                            cmd.Parameters["o_return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(cmd.Parameters["o_return"].Value.ToString());
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
        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM08_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_seq", MySqlDbType.Int32));
                            cmd.Parameters["i_seq"].Value = Convert.ToInt32(txtSeq1.EditValue);
                            cmd.Parameters["i_seq"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gcode_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_gcode_id"].Value = txtGcode_Id1.EditValue;
                            cmd.Parameters["i_gcode_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gcode_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_gcode_nm"].Value = txtGCode_Nm1.EditValue;
                            cmd.Parameters["i_gcode_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_id"].Value = txtCode_Id1.EditValue;
                            cmd.Parameters["i_code_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_nm"].Value = txtCode_NM1.EditValue;
                            cmd.Parameters["i_code_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_memo", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_memo"].Value = txtCode_Memo1.EditValue;
                            cmd.Parameters["i_code_memo"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_return", MySqlDbType.VarChar));
                            cmd.Parameters["o_return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(cmd.Parameters["o_return"].Value.ToString());
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

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM08_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_seq", MySqlDbType.Int32));
                            cmd.Parameters["i_seq"].Value = Convert.ToInt32(txtSeq2.EditValue);
                            cmd.Parameters["i_seq"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gcode_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_gcode_id"].Value = txtGcode_Id2.EditValue;
                            cmd.Parameters["i_gcode_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gcode_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_gcode_nm"].Value = txtGCode_Nm2.EditValue;
                            cmd.Parameters["i_gcode_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_id"].Value = txtCode_Id2.EditValue;
                            cmd.Parameters["i_code_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_nm"].Value = txtCode_NM2.EditValue;
                            cmd.Parameters["i_code_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_memo", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_memo"].Value = txtCode_Memo2.EditValue;
                            cmd.Parameters["i_code_memo"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_return", MySqlDbType.VarChar));
                            cmd.Parameters["o_return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(cmd.Parameters["o_return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open3();
            }
        }
        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            base.NewMode();
            Eraser.Clear(this, "CLR2");
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            base.NewMode();
            Eraser.Clear(this, "CLR3");
        }

    }
}
