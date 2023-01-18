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
using System.Xml;

namespace YL_DT.BizFrm
{
    public partial class frmDT06 : FrmBase
    {
        frmDT02_Pop02 popup;
        public frmDT06()
        {
            InitializeComponent();
            this.QCode = "DT06";
             //폼명설정
            this.FrmName = "Donut Letter(알림+게시)";
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        }
        private void frmDT06_Load(object sender, EventArgs e)
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

            chkA.EditValue = 'N';
            chkT.EditValue = 'N';
            chkH.EditValue = 'N';
            chkGM.EditValue = 'N';
            chkMM.EditValue = 'N';
            chkStock.EditValue = 'N';

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
             new ColumnControlSet("idx", txtTeb3_Idx)
           , new ColumnControlSet("subject", txtSubject)
           , new ColumnControlSet("use_type", rbuse_type_S)
           , new ColumnControlSet("contents", txtContents)
           , new ColumnControlSet("ImageURL", txtTab3_imgurl)
           , new ColumnControlSet("ImageURL", txtNow_image)
           , new ColumnControlSet("file_name1", txtTab3_imgname)
           , new ColumnControlSet("open_type", rbOpen_Type)
           , new ColumnControlSet("code_id", txtCode_id)
           , new ColumnControlSet("send_text", txtSend_Text)
           , new ColumnControlSet("send_type", rbSend_Type)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;

            rbUse_type.EditValue = "Y";
            rbuse_type_S.EditValue = "Y";
            rbOpen_Type.EditValue = "Y";

        }

        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            if (dr != null && dr["idx"].ToString() != "")
            {
                picTab3_Banner.LoadAsync(dr["ImageURL"].ToString());
            }
        }


        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.txtNow_image.Text))
            //{
            //    MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
            //    return;
            //}
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT06_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtTeb3_Idx.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Subject", MySqlDbType.VarChar));
                            cmd.Parameters["i_Subject"].Value = txtSubject.EditValue;
                            cmd.Parameters["i_Subject"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Use_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_Use_type"].Value = rbuse_type_S.EditValue;
                            cmd.Parameters["i_Use_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_File_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_File_name"].Value = txtNow_image.EditValue;
                            cmd.Parameters["i_File_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_open_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_open_type"].Value = rbOpen_Type.EditValue;
                            cmd.Parameters["i_open_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_id"].Value = txtCode_id.EditValue;
                            cmd.Parameters["i_code_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Send_Text", MySqlDbType.VarChar));
                            cmd.Parameters["i_Send_Text"].Value = txtSend_Text.EditValue;
                            cmd.Parameters["i_Send_Text"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Send_Type", MySqlDbType.VarChar));
                            cmd.Parameters["i_Send_Type"].Value = rbSend_Type.EditValue;
                            cmd.Parameters["i_Send_Type"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();
            }
        }

        public void Open1()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = rbUse_type.EditValue;

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


        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            base.NewMode();

            Eraser.Clear(this, "CLR3");
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            //string sftpDirectory = "/uploadFiles/files/landing/" + Convert.ToString(System.DateTime.Now.ToString("yyyyMMdd"));
            string sftpDirectory = "/domalifefiles/files/board/";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtTab3_imgname.EditValue = openFileDialog.SafeFileName;
                    txtTab3_PicPath1.EditValue = openFileDialog.FileName;
                    picTab3_Banner.LoadAsync(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }

                finally
                {
                    Cursor = Cursors.Default;
                }


                // 폴더생성

                DirectoryInfo di = new DirectoryInfo(@"c:\temp");
                if (di.Exists == false)
                {
                    di.Create();
                }

                // 선택된 파일을 위에서 만든 폴더에 이름을 바꿔 저장

                string sOldFile = txtTab3_PicPath1.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                txtNow_image.EditValue = sFileName;
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                //저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/board/";
                string sFtpPath2 = "/domalifefiles/files/board/" + sFileName;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtTab3_PicPath1.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }
                //if (isdir == false)
                //{

                //    sSftp.Mkdir(sFtpPath2);
                //}

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
            }
        }

        void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {

            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = dr["ImageURL"].ToString();
                if (iconsCache.ContainsKey(url))
                {
                    e.Value = iconsCache[url];
                    return;
                }
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        e.Value = Bitmap.FromStream(stream);
                        iconsCache.Add(url, (Bitmap)e.Value);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        private void picTab3_Banner_DoubleClick(object sender, EventArgs e)
        {
            popup = new frmDT02_Pop02();
            //popup.Owner = this;
            popup.pURL = txtTab3_imgurl.Text;
            
            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }
        public void Open2()
        {

            if (string.IsNullOrEmpty(this.txtCode_id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " Donut Letter 관리코드를 선택하세요!");
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT06_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_code_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtCode_id.EditValue;

                        cmd.Parameters.Add("i_member_chk1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = chkA.EditValue;
                        
                        cmd.Parameters.Add("i_member_chk2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = chkT.EditValue;
                        
                        cmd.Parameters.Add("i_member_chk3", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = chkH.EditValue;

                        cmd.Parameters.Add("i_member_chk4", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = chkGM.EditValue;

                        cmd.Parameters.Add("i_member_chk5", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = chkMM.EditValue;

                        cmd.Parameters.Add("i_member_chk6", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = chkStock.EditValue;

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

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            Open2();
        }

        private void chkA_CheckedChanged(object sender, EventArgs e)
        {
            efwGridControl2.DataSource = null;
            gridView2.RefreshData();
        }

        private void chkT_CheckedChanged(object sender, EventArgs e)
        {
            efwGridControl2.DataSource = null;
            gridView2.RefreshData();
        }

        private void chkH_CheckedChanged(object sender, EventArgs e)
        {
            efwGridControl2.DataSource = null;
            gridView2.RefreshData();
        }

        private void chkGM_CheckedChanged(object sender, EventArgs e)
        {
            efwGridControl2.DataSource = null;
            gridView2.RefreshData();
        }
        string apiurl = string.Empty;
        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView2.DataRowCount; i++)
                    {
                        string chk1 = gridView2.GetRowCellValue(i, gridView2.Columns[5]).ToString();
                        string chk2 = gridView2.GetRowCellValue(i, gridView2.Columns[7]).ToString();

                        if (gridView2.GetRowCellValue(i, gridView2.Columns[5]).ToString() == "" && gridView2.GetRowCellValue(i, gridView2.Columns[7]).ToString().Length > 20 )
                        {
                            apiurl = gridView2.GetRowCellValue(i, gridView2.Columns[6]).ToString();

                            WebClient wc = new WebClient();
                            XmlReader read = new XmlTextReader(wc.OpenRead(apiurl));

                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT06_SAVE_02", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_code_id", MySqlDbType.VarChar, 10);
                                cmd.Parameters[0].Value = txtCode_id.EditValue;

                                cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                                cmd.Parameters[1].Value = gridView2.GetRowCellValue(i, gridView2.Columns[7]).ToString();

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
