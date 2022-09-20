﻿using DevExpress.XtraEditors.Repository;
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
    public partial class frmDT07 : FrmBase
    {
        public frmDT07()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmDT07";
            //폼명설정
            this.FrmName = "게시글 신고관리";
        }

        private void frmDT07_Load(object sender, EventArgs e)
        {
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
            dtLock_Date.EditValue = DateTime.Now;


            this.efwGridControl1.BindControlSet(
               new ColumnControlSet("declare_type", rbDeclare_Type)
             , new ColumnControlSet("Idx", txtIdx)
             , new ColumnControlSet("u_id", txtU_id)
             , new ColumnControlSet("story_id", txtStory_Id)
             , new ColumnControlSet("comment_id", txtComment_Id)
             , new ColumnControlSet("declare_u_id", txtDeclare_u_id)
             , new ColumnControlSet("remark", txtRemark)
             , new ColumnControlSet("lock_date", dtLock_Date)
             , new ColumnControlSet("lock_type_code", rbLock_Type)
             , new ColumnControlSet("d_idx", txtD_idx)
             ); ;

            this.efwGridControl1.Click += efwGridControl1_Click;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

        }

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT07_SELECT_01", con))
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
        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT07_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "P";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            if (txtD_idx.EditValue == null)
                            {
                                txtD_idx.EditValue = "0";
                            }

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtD_idx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_id.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_story_id", MySqlDbType.Int32));
                            cmd.Parameters["i_story_id"].Value = Convert.ToInt32(txtStory_Id.EditValue);
                            cmd.Parameters["i_story_id"].Direction = ParameterDirection.Input;

                            // 
                            if (txtComment_Id.EditValue == null)
                            {
                                txtComment_Id.EditValue = "0";
                            }

                            cmd.Parameters.Add(new MySqlParameter("i_comment_id", MySqlDbType.Int32));
                            cmd.Parameters["i_comment_id"].Value = Convert.ToInt32(txtComment_Id.EditValue);
                            cmd.Parameters["i_comment_id"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_lock_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_lock_type"].Value = rbLock_Type.EditValue;
                            cmd.Parameters["i_lock_type"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_lock_date", MySqlDbType.Datetime));
                            cmd.Parameters["i_lock_date"].Value = dtLock_Date.EditValue;
                            cmd.Parameters["i_lock_date"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_declare_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_declare_type"].Value = rbDeclare_Type.EditValue;
                            cmd.Parameters["i_declare_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();
                            txtD_idx.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT07_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "D";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            if  ( txtD_idx.EditValue == null )
                            {
                                txtD_idx.EditValue = "0";
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtD_idx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_id.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_story_id", MySqlDbType.Int32));
                            cmd.Parameters["i_story_id"].Value = Convert.ToInt32(txtStory_Id.EditValue);
                            cmd.Parameters["i_story_id"].Direction = ParameterDirection.Input;

                            // 
                            if (txtComment_Id.EditValue == null)
                            {
                                txtComment_Id.EditValue = "0";
                            }

                            cmd.Parameters.Add(new MySqlParameter("i_comment_id", MySqlDbType.Int32));
                            cmd.Parameters["i_comment_id"].Value = Convert.ToInt32(txtComment_Id.EditValue);
                            cmd.Parameters["i_comment_id"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_lock_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_lock_type"].Value = rbLock_Type.EditValue;
                            cmd.Parameters["i_lock_type"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_lock_date", MySqlDbType.Datetime));
                            cmd.Parameters["i_lock_date"].Value = dtLock_Date.EditValue;
                            cmd.Parameters["i_lock_date"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_declare_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_declare_type"].Value = rbDeclare_Type.EditValue;
                            cmd.Parameters["i_declare_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();
                            txtD_idx.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
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
}
