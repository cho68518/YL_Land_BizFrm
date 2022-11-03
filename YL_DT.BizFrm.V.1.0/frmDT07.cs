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
    public partial class frmDT07 : FrmBase
    {
        frmDT02_Pop02 popup;
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

            //app_img_url1.LoadAsync("http://14.48.175.173/~ylland/data/write/1666593495f2d8a81fae37ef43095a79e1778e2c0399065eb1630239e5b97d10df0faeb42ccaecef5b28b465d89a6e5b8f3480a13e4586ced0c23f98fc74a4154b3ca7f838.jpg");
            //picP_IMG.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
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
             ); ;

            this.efwGridControl1.Click += efwGridControl1_Click;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["app_img_url1"].ToString() != "")
            {
                imapp_img_url1.LoadAsync(dr["app_img_url1"].ToString());
                txtImg_url1.EditValue = dr["app_img_url1"].ToString();
            }
            else
            {
                imapp_img_url1.LoadAsync(null);
            }

            if (dr != null && dr["app_img_url2"].ToString() != "")
            {
                imapp_img_url2.LoadAsync(dr["app_img_url2"].ToString());
                txtImg_url2.EditValue = dr["app_img_url2"].ToString();
            }
            else
            {
                imapp_img_url2.LoadAsync(null);
            }
            if (dr != null && dr["app_img_url3"].ToString() != "")
            {
                imapp_img_url3.LoadAsync(dr["app_img_url3"].ToString());
                txtImg_url3.EditValue = dr["app_img_url3"].ToString();
            }
            else
            {
                imapp_img_url3.LoadAsync(null);
            }
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
            if (string.IsNullOrEmpty(this.txtRemark.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "사유를 입력하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.dtLock_Date.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "종료일을 입력하세요!");
                return;
            }

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

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            string app_list = "http://14.48.175.173/~ylland/adm/deal_list.php?idx=" + txtStory_Id.EditValue.ToString();
            System.Diagnostics.Process.Start(app_list);
        }

        private void imapp_img_url1_DoubleClick(object sender, EventArgs e)
        {
            popup = new frmDT02_Pop02();
            //popup.Owner = this;

            popup.pURL = txtImg_url1.Text;
            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }


        private void imapp_img_url2_DoubleClick(object sender, EventArgs e)
        {
            popup = new frmDT02_Pop02();
            //popup.Owner = this;

            popup.pURL = txtImg_url2.Text;
            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void imapp_img_url3_DoubleClick(object sender, EventArgs e)
        {
            popup = new frmDT02_Pop02();

            popup.pURL = txtImg_url3.Text;
            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }
    }
}
