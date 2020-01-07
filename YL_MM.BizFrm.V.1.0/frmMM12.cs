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
    public partial class frmMM12 : FrmBase
        
    {
        public frmMM12()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = " frmMM12";
            //폼명설정
            this.FrmName = "스와이프 결재등록";

        }


        public override void FrmLoadEvent()
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

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;


            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("mall_type", cmbU_Chef_Level)
            , new ColumnControlSet("u_name", txtU_Name)
            , new ColumnControlSet("u_nickname", txtU_Nickname)
            , new ColumnControlSet("login_id", txtLogin_Id)
            , new ColumnControlSet("o_code", txtO_Code)
            , new ColumnControlSet("o_date", txtO_Date)
            , new ColumnControlSet("o_deposit_confirm_date", txtO_Deposit_Confirm_Date)
            , new ColumnControlSet("remark", txtRemark)
            , new ColumnControlSet("remark", txtRemark)
            );

            this.efwGridControl1.Click += efwGridControl1_Click;


            setCmb();


        }
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            
            if (dr != null && dr["o_code"].ToString() != "")
            {
                this.cmbU_Chef_Level.EditValue = dr["mall_type"].ToString();
                this.txtU_Name.EditValue = dr["u_name"].ToString();
                this.txtU_Nickname.EditValue = dr["u_nickname"].ToString();
                this.txtLogin_Id.EditValue = dr["login_id"].ToString();
                this.txtO_Code.EditValue = dr["o_code"].ToString();
                this.txtO_Date.EditValue = dr["o_date"].ToString();
                this.txtO_Deposit_Confirm_Date.EditValue = dr["o_deposit_confirm_date"].ToString();
                this.txtRemark.EditValue = dr["remark"].ToString();
            }
        }

        private void setCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select  ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00026'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbU_Chef_Level, codeArray);
            }
            cmbU_Chef_Level.EditValue = "1";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select  '' as DCODE, '선택하세요' as DNAME union all select  ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00026'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbOrderMallTypeQ, codeArray);
            }
            cmbOrderMallTypeQ.EditValue = "";


        }

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM12_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_order_mall_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = this.cmbOrderMallTypeQ.EditValue;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = this.cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = this.txtSearch.EditValue;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(this.txtO_Code.Text))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 주무번호를 선택하세요!");
                    return;
                }


                if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM12_SAVE_01", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                                cmd.Parameters["i_proc_type"].Value = "A";
                                cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_mall_type", MySqlDbType.VarChar));
                                cmd.Parameters["i_mall_type"].Value = cmbU_Chef_Level.EditValue;
                                cmd.Parameters["i_mall_type"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                                cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                                cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_o_deposit_confirm_date", MySqlDbType.DateTime));
                                cmd.Parameters["i_o_deposit_confirm_date"].Value = txtO_Deposit_Confirm_Date.EditValue;
                                cmd.Parameters["i_o_deposit_confirm_date"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                                cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                                cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    }
                    MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                    Search();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(this.txtO_Code.Text))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 주무번호를 선택하세요!");
                    return;
                }


                if (MessageAgent.MessageShow(MessageType.Confirm, "취소 하시겠습니까?") == DialogResult.OK)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM12_SAVE_01", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                                cmd.Parameters["i_proc_type"].Value = "D";
                                cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_mall_type", MySqlDbType.VarChar));
                                cmd.Parameters["i_mall_type"].Value = cmbU_Chef_Level.EditValue;
                                cmd.Parameters["i_mall_type"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                                cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                                cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_o_deposit_confirm_date", MySqlDbType.DateTime));
                                cmd.Parameters["i_o_deposit_confirm_date"].Value = null;
                                cmd.Parameters["i_o_deposit_confirm_date"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                                cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                                cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    }
                    MessageAgent.MessageShow(MessageType.Informational, "취소 되었습니다.");
                    Search();
                }
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(this.txtO_Code.Text))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 주무번호를 선택하세요!");
                    return;
                }


                if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM12_SAVE_01", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                                cmd.Parameters["i_proc_type"].Value = "A";
                                cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_mall_type", MySqlDbType.VarChar));
                                cmd.Parameters["i_mall_type"].Value = cmbU_Chef_Level.EditValue;
                                cmd.Parameters["i_mall_type"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                                cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                                cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_o_deposit_confirm_date", MySqlDbType.DateTime));
                                cmd.Parameters["i_o_deposit_confirm_date"].Value = txtO_Deposit_Confirm_Date.EditValue;
                                cmd.Parameters["i_o_deposit_confirm_date"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                                cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                                cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    }
                    MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                    Search();
                }
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(this.txtO_Code.Text))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 주무번호를 선택하세요!");
                    return;
                }


                if (MessageAgent.MessageShow(MessageType.Confirm, "취소 하시겠습니까?") == DialogResult.OK)
                {
                    try
                    {
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM12_SAVE_01", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                                cmd.Parameters["i_proc_type"].Value = "D";
                                cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_mall_type", MySqlDbType.VarChar));
                                cmd.Parameters["i_mall_type"].Value = cmbU_Chef_Level.EditValue;
                                cmd.Parameters["i_mall_type"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                                cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                                cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_o_deposit_confirm_date", MySqlDbType.DateTime));
                                cmd.Parameters["i_o_deposit_confirm_date"].Value = null;
                                cmd.Parameters["i_o_deposit_confirm_date"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                                cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                                cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    }
                    MessageAgent.MessageShow(MessageType.Informational, "취소 되었습니다.");
                    Search();
                }
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}


