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
using YL_TELECOM.BizFrm.Dlg;

namespace YL_TELECOM.BizFrm
{
    public partial class frmTM10 : FrmBase
    {
        frmTM10_Pop01 popup;
        public frmTM10()
        {
            InitializeComponent();
            this.QCode = "TM10";
            //폼명설정
            this.FrmName = "상품입고 등록";
        }

        private void frmTM10_Load(object sender, EventArgs e)
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
            this.IsExcel = true;

            dtInsert_Date.EditValue = DateTime.Now;
            txtWork_Idx.EditValue = "0";

            gridView1.OptionsView.ShowFooter = true;
            this.efwGridControl1.BindControlSet(
               new ColumnControlSet("idx", txtidx)
              , new ColumnControlSet("o_code", txtO_Code)
              , new ColumnControlSet("entr_no", txtEntr_No)
              , new ColumnControlSet("o_code", txtO_Code_Q)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;

            gridView2.OptionsView.ShowFooter = true;
            this.efwGridControl2.BindControlSet(
               new ColumnControlSet("idx", txtidx)
              , new ColumnControlSet("insert_user", txtInsert_User)
              , new ColumnControlSet("w_info", txtW_Info)
              , new ColumnControlSet("w_type", txtW_Type)
            );
            this.efwGridControl2.Click += efwGridControl2_Click;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                this.txtidx.EditValue = dr["idx"].ToString();
                this.txtO_Code_Q.EditValue = dr["o_code"].ToString();
                dtInsert_Date.EditValue = DateTime.Now;
                txtWork_Idx.EditValue = "0";
                Open1();
            }

        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                this.txtWork_Idx.EditValue = dr["idx"].ToString();
                this.dtInsert_Date.EditValue = dr["insert_date"].ToString();
            }

        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM10_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtSearch.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        public void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM10_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtO_Code_Q.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void btnExcelUpdate_Click(object sender, EventArgs e)
        {
            popup = new frmTM10_Pop01();
            popup.ShowDialog();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            txtWork_Idx.EditValue = "0";
            txtInsert_User.EditValue = "";
            txtW_Info.EditValue = "";
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (txtO_Code.EditValue.ToString() == "" ^ txtO_Code.EditValue.ToString() == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "접수번호를 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM10_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtWork_Idx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_code"].Value = txtO_Code.EditValue;
                            cmd.Parameters["i_o_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_w_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_w_type"].Value = txtW_Type.EditValue;
                            cmd.Parameters["i_w_type"].Direction = ParameterDirection.Input;

                            string sUserNmae = string.Empty;
                            if (string.IsNullOrEmpty(this.txtInsert_User.Text))
                            {
                                sUserNmae = UserInfo.instance().Name;
                            }
                            else
                            {
                                sUserNmae = txtInsert_User.EditValue.ToString();
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_insert_user", MySqlDbType.VarChar));
                            cmd.Parameters["i_insert_user"].Value = sUserNmae;
                            cmd.Parameters["i_insert_user"].Direction = ParameterDirection.Input;

                            string sInsert_date = string.Empty;
                            sInsert_date = dtInsert_Date.EditValue.ToString();
                            sInsert_date = sInsert_date.Replace("오전 ","").Replace("오후 ", "");

                            cmd.Parameters.Add(new MySqlParameter("i_insert_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_insert_date"].Value = Convert.ToDateTime(sInsert_date);
                            cmd.Parameters["i_insert_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_w_info", MySqlDbType.VarChar));
                            cmd.Parameters["i_w_info"].Value = txtW_Info.EditValue;
                            cmd.Parameters["i_w_info"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();
                            txtWork_Idx.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
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

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM10_DELETE_01", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                        cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtWork_Idx.EditValue);
                        cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;


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
            txtWork_Idx.EditValue = "0";
            Open1();
        }

    }
}
