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
using YL_COMM.BizFrm;
using YL_DT.BizFrm.Dlg;
using System.Data.SqlClient;
using System.IO;
using Tamir.SharpSsh;
using System.Collections;
using System.Net.Sockets;



namespace YL_DT.BizFrm
{
    public partial class frmDT05 : FrmBase
    {
        public frmDT05()
        {
            InitializeComponent();
            this.QCode = "DT05";
            //폼명설정
            this.FrmName = "제품 관리";
        }

        private void frmDT05_Load(object sender, EventArgs e)
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
            rbShow_Type.EditValue = "A";
            rbShow_Level.EditValue = "A";
        }
        public override void Search()
        {
            Open1();
        }


        public void Open1()
        {
            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT05_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //  this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["id"].ToString() != "0")
            {
                this.txtId.Text = dr["id"].ToString();
                Open2();
            }
        }

        public void Open2()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;

                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT05_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Id", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = txtId.EditValue;

                        cmd.Parameters.Add("i_show_type", MySqlDbType.VarChar);
                        cmd.Parameters[1].Value = rbShow_Type.EditValue;

                        cmd.Parameters.Add("i_show_level", MySqlDbType.VarChar);
                        cmd.Parameters[2].Value = rbShow_Level.EditValue;

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar);
                        cmd.Parameters[3].Value = txtQuery.EditValue;


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

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGD_Rate.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "GD 사용율를 입력하세요");
                return;
            }

            for (int i = 0; i < gridView2.RowCount; i++)
                gridView2.SetRowCellValue(i, gridView2.Columns["gd_rate"], txtGD_Rate.EditValue );
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
                gridView2.SetRowCellValue(i, gridView2.Columns["gd_rate"], "0");
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            try
            {
                var saveResult = new SaveTableResultInfo() { IsError = true };
                var dt = efwGridControl2.GetChangeDataWithRowState;
                var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][StatusColumn].ToString() == "U")
                    {
                        //Console.WriteLine("------------------------------------------------------------");
                        //Console.WriteLine("[U] " + dt.Rows[i]["o_code"].ToString());
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT05_SAVE_01", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_pp_id", MySqlDbType.Int32);
                                cmd.Parameters[0].Value = dt.Rows[i]["pp_id"].ToString();

                                cmd.Parameters.Add("i_gd_grade", MySqlDbType.Int32);
                                cmd.Parameters[1].Value = dt.Rows[i]["gd_rate"].ToString();

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Open2();
        }
    }
}
