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

namespace YL_DONUT.BizFrm
{
    public partial class frmDN24 : FrmBase
    {
        public frmDN24()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "DN24";
            //폼명설정
            this.FrmName = "온라인 주문등록";

        }

        private void frmDN24_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            dtO_Date.EditValue = DateTime.Now;
            cmbO_Company.EditValue = "1";
            cmbO_Sub_Company.EditValue = "0";

            gridView1.OptionsView.ShowFooter = true;
            //      cmbTAREA1.EditValue = "00";
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("idx", txtIdx)
                    , new ColumnControlSet("o_date", dtO_Date)
                    , new ColumnControlSet("O_Qty", txtO_Qty)
                    , new ColumnControlSet("O_Amt", txtO_Amt)
                    , new ColumnControlSet("Remark", txtRemark)
                    , new ColumnControlSet("o_company", cmbO_Company)
                    , new ColumnControlSet("o_sub_company", cmbO_Sub_Company)
                   ); ;

            this.efwGridControl1.Click += efwGridControl1_Click;
            SetCmb();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            //DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            //if (dr != null && dr["o_company"].ToString() != "")
            //{
            //    this.cmbO_Company.EditValue = dr["o_company"].ToString();
            //}
        }

        private void SetCmb()
        {

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT code_id as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = 00042 order by code_id ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbO_Sub_Company, codeArray);
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT code_id as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = 00041 order by code_id ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbO_Company, codeArray);
            }



        }
        private void from_new()
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
            dtO_Date.EditValue = DateTime.Now;
            cmbO_Sub_Company.EditValue = "0";
        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

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

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbO_Company.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 온라인몰을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "P";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_o_date"].Value = dtO_Date.EditValue;
                            cmd.Parameters["i_o_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_company"].Value = cmbO_Company.EditValue;
                            cmd.Parameters["i_o_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_sub_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_sub_company"].Value = cmbO_Sub_Company.EditValue;
                            cmd.Parameters["i_o_sub_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_qty", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_qty"].Value = Convert.ToInt32(txtO_Qty.EditValue);
                            cmd.Parameters["i_o_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_amt", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_amt"].Value = Convert.ToInt32(txtO_Amt.EditValue);
                            cmd.Parameters["i_o_amt"].Direction = ParameterDirection.Input;

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

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 삭제할 온라인몰의 일자를 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "D";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_o_date"].Value = dtO_Date.EditValue;
                            cmd.Parameters["i_o_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_company"].Value = cmbO_Company.EditValue;
                            cmd.Parameters["i_o_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_sub_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_sub_company"].Value = cmbO_Sub_Company.EditValue;
                            cmd.Parameters["i_o_sub_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_qty", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_qty"].Value = Convert.ToInt32(txtO_Qty.EditValue);
                            cmd.Parameters["i_o_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_amt", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_amt"].Value = Convert.ToInt32(txtO_Amt.EditValue);
                            cmd.Parameters["i_o_amt"].Direction = ParameterDirection.Input;

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
                from_new();
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            from_new();
        }



    }
}
