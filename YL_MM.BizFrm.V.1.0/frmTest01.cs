using Easy.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Easy.Framework.WinForm.Control;
using DevExpress.XtraGrid.Views.Grid;

namespace YL_MM.BizFrm
{
    public partial class frmTest01 : FrmBase
    {
        MySqlConnection sconn = new MySqlConnection(ConstantLib.BasicConn_Dev);
        //MySqlConnection sconn = new MySqlConnection(ConstantLib.BasicConn_Real);

        public frmTest01()
        {
            InitializeComponent();
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();

            this.IsMenuVw = true;

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                         new ColumnControlSet("u_name", txtU_NAME)
                        ,new ColumnControlSet("idx"   , txtIDX)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                String sql = "SELECT idx, u_name FROM domalife.member_master_20170530 " +
                                "ORDER BY u_name";

                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, sconn);

                adpt.Fill(ds, "members");
                if (ds.Tables.Count > 0)
                {
                    //foreach (DataRow r in ds.Tables[0].Rows)
                    //{
                    //    Console.WriteLine(r["name"]);
                    //}

                    efwGridControl1.DataBind(ds);
                    this.efwGridControl1.MyGridView.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Dev);
                //MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Real);

                connection.Open();

                string ss = "MySQL version: " + connection.ServerVersion;


                efwMemoEdit1.EditValue = ss;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void EfwSimpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Dev);
                //MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Real);

                connection.Open();

                string ss = "MySQL version: " + connection.ServerVersion;


                efwMemoEdit1.EditValue = ss;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            efwMemoEdit1.EditValue = null;
        }

        private void EfwSimpleButton6_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Dev);
                    connection.Open();

                    MySqlCommand deleteCommand = new MySqlCommand();
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandText = "DELETE FROM domalife.member_master_20170530 WHERE idx=@idx";

                    deleteCommand.Parameters.Add("@idx", MySqlDbType.Int32, 11);
                    deleteCommand.Parameters[0].Value = Convert.ToInt32(txtIDX.EditValue);

                    deleteCommand.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
            }
        }

        private void EfwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Dev);
                    connection.Open();

                    MySqlCommand updateCommand = new MySqlCommand();
                    updateCommand.Connection = connection;
                    updateCommand.CommandText = "UPDATE domalife.member_master_20170530 SET u_name=@u_name WHERE idx=@idx";

                    updateCommand.Parameters.Add("@u_name", MySqlDbType.VarChar, 50);
                    updateCommand.Parameters[0].Value = txtU_NAME.EditValue;

                    updateCommand.Parameters.Add("@idx", MySqlDbType.Int32, 11);
                    updateCommand.Parameters[1].Value = Convert.ToInt32(txtIDX.EditValue);

                    updateCommand.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
            }
        }

        private void EfwSimpleButton7_Click(object sender, EventArgs e)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real)) 
            {
                //sql.Query = "SELECT idx as idx, u_name as u_name FROM domalife.member_master_20170530 " +
                //             "ORDER BY u_name";
                sql.Query = "SELECT idx as idx, u_name as u_name FROM domalife.member_master " +
                             "ORDER BY u_name";

                DataSet ds = sql.selectQueryDataSet();

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
        }

        private void EfwSimpleButton8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIDX1.Text))
            {
                return;
            }

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Dev))
            {
                sql.Query = "SELECT u_name as u_name FROM domalife.member_master_20170530 " +
                             "WHERE idx = " + txtIDX1.EditValue;
                DataSet ds = sql.selectQueryDataSet();

                efwMemoEdit2.EditValue = sql.selectQueryForSingleValue();
            }
        }

        private void EfwSimpleButton9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIDX2.Text))
            {
                return;
            }

            Dictionary<string, string> myRecord;

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Dev))
            {
                sql.Query = "SELECT * FROM domalife.member_master_20170530 " +
                             "WHERE idx = @idx LIMIT 1";

                sql.addParam("@Idx", txtIDX2.EditValue);

                myRecord = sql.selectQueryForSingleRecord();

                for (int i=0; i < myRecord.Count; i++)
                {
                    efwMemoEdit3.EditValue = "idx          : " + myRecord["idx"]        + "\r\n"+
                                             "u_name       : " + myRecord["u_name"]     + "\r\n" +
                                             "u_id         : " + myRecord["u_id"]       + "\r\n" +
                                             "u_nickname   : " + myRecord["u_nickname"] + "\r\n" +
                                             "login_id     : " + myRecord["login_id"];
                }
            }
        }

        private void EfwSimpleButton10_Click(object sender, EventArgs e)
        {
            List<string> myColVals;
            StringBuilder sb = new StringBuilder();

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Dev))
            {
                sql.Query = "SELECT u_name FROM domalife.member_master_20170530";
                myColVals = sql.selectQueryForSingleColumn();

                for (int i = 0; i < myColVals.Count; i++)
                {

                    sb.Append(myColVals[i] + "\r\n");
                }

                efwMemoEdit4.EditValue = sb.ToString();
            }
        }

        private void EfwSimpleButton11_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
            {
                using (MySqlCommand cmd = new MySqlCommand("domalife.SheetTest_01", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_idx", 0);

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

        // Procedure 저장
        private void EfwSimpleButton13_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domalife.SaveTest_01", con))
                        {
                            con.Open();

                            cmd.CommandType = CommandType.StoredProcedure;


                            //cmd.Parameters.Add(new MySqlParameter("p_idx", MySqlDbType.Int32));
                            //cmd.Parameters["p_idx"].Value = Convert.ToInt32(txtIDX3.EditValue);

                            cmd.Parameters.Add(new MySqlParameter("p_idx", MySqlDbType.VarChar));
                            cmd.Parameters["p_idx"].Value = txtIDX3.EditValue;
                            cmd.Parameters["p_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_NAME", MySqlDbType.VarChar));
                            cmd.Parameters["p_NAME"].Value = txtU_NAME3.EditValue;
                            cmd.Parameters["p_NAME"].Direction = ParameterDirection.Input;

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
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
            }

        }
        // Procedure 삭제
        private void EfwSimpleButton14_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domalife.DeleteTest_01", con))
                        {
                            con.Open();

                            cmd.CommandType = CommandType.StoredProcedure;

                            //cmd.Parameters.Add(new MySqlParameter("p_idx", MySqlDbType.Int32));
                            //cmd.Parameters["p_idx"].Value = Convert.ToInt32(txtIDX3.EditValue);

                            cmd.Parameters.Add(new MySqlParameter("p_idx", MySqlDbType.VarChar));
                            cmd.Parameters["p_idx"].Value = txtIDX3.EditValue;
                            cmd.Parameters["p_idx"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("p_NAME", MySqlDbType.VarChar));
                            cmd.Parameters["p_NAME"].Value = txtU_NAME3.EditValue;
                            cmd.Parameters["p_NAME"].Direction = ParameterDirection.Input;

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
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
            }
        }

        private void EfwSimpleButton12_Click(object sender, EventArgs e)
        {

        }
    }
}
