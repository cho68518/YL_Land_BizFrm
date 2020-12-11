using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Common.PopUp;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_RM.BizFrm;
using Tamir.SharpSsh;
using System.Collections;
using System.Net.Sockets;


namespace YL_RM.BizFrm
{
    public partial class frmRM04 : FrmBase
    {
        public frmRM04()
        {
            InitializeComponent();
            this.QCode = "RM04";
            //폼명설정
            this.FrmName = "1:1 문의";

        }

        private void frmRM04_Load(object sender, EventArgs e)
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


            rbanswer_Q.EditValue = "T";
            rbis_open.EditValue = "N";
            
            rbanswer_type.EditValue = "N";

            this.efwGridControl1.BindControlSet(
                new ColumnControlSet("idx", txt_idx2)
              , new ColumnControlSet("subject", txtsubject)
              , new ColumnControlSet("is_open", rbis_open)
              , new ColumnControlSet("remark", txtremark)
              , new ColumnControlSet("answer_type", rbanswer_type)
              , new ColumnControlSet("content", txtcontent)
              , new ColumnControlSet("u_name", txtu_name)
              , new ColumnControlSet("tel_no", txttel_no)
          );
            this.efwGridControl1.Click += efwGridControl1_Click;
            SetCmb();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            if (dr != null && dr["idx"].ToString() != "0" )
            {
                rbanswer_type.EditValue = dr["answer_type"].ToString();
                rbis_open.EditValue = dr["is_open"].ToString();
            }
                
        }

        private void SetCmb()
        {
            try
            {
                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = " SELECT code_id as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00049' order by code_id ";

                    DataSet ds = con.selectQueryDataSet();
                    //DataTable retDT = ds.Tables[0];
                    DataRow[] dr = ds.Tables[0].Select();
                    CodeData[] codeArray = new CodeData[dr.Length];

                    for (int i = 0; i < dr.Length; i++)
                        codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                    CodeAgent.MakeCodeControl(this.cmbBoard_Type, codeArray);
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            cmbBoard_Type.EditValue = "0";
        }
        public override void Search()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM04_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_board_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = cmbBoard_Type.EditValue.ToString();

                        cmd.Parameters.Add("i_sch", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txt_sch2.EditValue;

                        cmd.Parameters.Add("i_answer_Q", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = rbanswer_Q.EditValue.ToString();

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txt_sch2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void btn_save2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM04_SAVE_01", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("i_userid", MySqlDbType.VarChar));
                        cmd.Parameters["i_userid"].Value = UserInfo.instance().UserId;
                        cmd.Parameters["i_userid"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                        cmd.Parameters["i_idx"].Value = txt_idx2.EditValue;
                        cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_content", MySqlDbType.VarChar));
                        cmd.Parameters["i_content"].Value = txtcontent.EditValue;
                        cmd.Parameters["i_content"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_is_open", MySqlDbType.VarChar));
                        cmd.Parameters["i_is_open"].Value = rbis_open.EditValue;
                        cmd.Parameters["i_is_open"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_answer_type", MySqlDbType.VarChar));
                        cmd.Parameters["i_answer_type"].Value = rbanswer_type.EditValue;
                        cmd.Parameters["i_answer_type"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                        cmd.Parameters["i_remark"].Value = txtremark.EditValue;
                        cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                }

                MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                Cursor.Current = Cursors.Default;
                Search();

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
