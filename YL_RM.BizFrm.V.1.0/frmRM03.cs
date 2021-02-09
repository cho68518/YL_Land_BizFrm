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
using YL_RM.BizFrm.Dlg;
using DevExpress.XtraGrid.Columns;
using System.Net;

namespace YL_RM.BizFrm
{
    public partial class frmRM03 : FrmBase
    {
        frmGMember_info popup_member;
        public frmRM03()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "GM03";
            //폼명설정
            this.FrmName = "배스트샵 등록";
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
            gridView2.CustomUnboundColumnData += gridView2_CustomUnboundColumnData;
        }

        private void frmRM03_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            picBest_Pic1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBest_Pic2.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picP_IMG.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            rbshow_type.EditValue ="Y";
            rbDshow_type.EditValue = "Y";
            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
                new ColumnControlSet("gshop_name", txtgshop_name)
              , new ColumnControlSet("road_addr", txtAddr)
              , new ColumnControlSet("tel_no", txtTelNo)
              , new ColumnControlSet("show_type", rbshow_type)
              , new ColumnControlSet("gshop_id", txtgshop_id)
              , new ColumnControlSet("idx", txtidx)
              , new ColumnControlSet("img_url", txtimg_url)
              , new ColumnControlSet("input_cause", txtinput_cause)
              , new ColumnControlSet("customer_reaction", txtcustomer_reaction)
              , new ColumnControlSet("good_writing", txtgood_writing)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;



            this.efwGridControl2.BindControlSet(
                new ColumnControlSet("idx", txtD_idx)
              , new ColumnControlSet("bef_url", txtbef_url)
              , new ColumnControlSet("after_url", txtafter_url)
              , new ColumnControlSet("remark", txtremark)
              , new ColumnControlSet("show_type", rbDshow_type)
              , new ColumnControlSet("bef_date", dtS_DATE)
              , new ColumnControlSet("after_date", dtE_DATE)
            );

            this.efwGridControl2.Click += efwGridControl2_Click;

        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            if (dr != null && dr["idx"].ToString() != "0")
            {
                picP_IMG.LoadAsync(dr["img_url"].ToString());
                picBest_Pic1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
                Open1();
            }
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["idx"].ToString() != "0")
            {
                picBest_Pic1.LoadAsync(dr["bef_url"].ToString());
                picBest_Pic2.LoadAsync(dr["after_url"].ToString());
            }
        }



        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            Eraser.Clear(this, "CLR1");
            Eraser.Clear(this, "CLR2");
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            picP_IMG.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBest_Pic1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBest_Pic2.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");

            popup_member = new frmGMember_info
            {
                COMPANYNAME = "(주)와이엘랜드",
            };
            popup_member.FormClosed += popup_FormClosed;
            popup_member.ShowDialog();
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup_member.FormClosed -= popup_FormClosed;
            if (popup_member.DialogResult == DialogResult.OK)
            {
                this.txtgshop_id.EditValue = popup_member.GSHOP_ID;

                int nCount;
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {

                    sql.Query = "select count(*) as nCount FROM domabiz.tb_poto_best_gshop where gshop_id  = '" + txtgshop_id.EditValue + "'  ";
                    DataSet ds = sql.selectQueryDataSet();

                    nCount = Convert.ToInt32(sql.selectQueryForSingleValue());
                }
                if (nCount == 1)
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 선정 이력이 존재합니다 목록에서 선택하여 수정 하세요~ ");
                    this.txtSearch_Name.EditValue = popup_member.GSHHOP_NAME;
                    Search();
                }
                this.txtgshop_name.EditValue = popup_member.GSHHOP_NAME;
                this.txtTelNo.EditValue = popup_member.U_CELL_NUM;
                this.txtAddr.EditValue = popup_member.U_ADDR;
            }
            popup_member = null;
        }
        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM_RM03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtSearch_Name.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            // this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM_RM03_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_master_idx", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = txtidx.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);

                            // this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }



       Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();

        void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = dr["img_url"].ToString();
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

        void gridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = string.Empty;

                if (e.Column.FieldName == "img1")
                {
                    url = dr["bef_url"].ToString();
                }

                if (e.Column.FieldName == "img2")
                {
                    url = dr["after_url"].ToString();
                }

                 if (iconsCache.ContainsKey(url))
                {
                    e.Value = iconsCache[url];
                    return;
                }

                if (url != "")
                {
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
                Cursor.Current = Cursors.Default;


            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }


        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtgshop_id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "G 멀티샵을 선택하세요");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM_RM04_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtgshop_id.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_img_url", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url"].Value = txtimg_url.EditValue;
                            cmd.Parameters["i_img_url"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_input_cause", MySqlDbType.VarChar));
                            cmd.Parameters["i_input_cause"].Value = txtinput_cause.EditValue;
                            cmd.Parameters["i_input_cause"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_customer_reaction", MySqlDbType.VarChar));
                            cmd.Parameters["i_customer_reaction"].Value = txtcustomer_reaction.EditValue;
                            cmd.Parameters["i_customer_reaction"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_good_writing", MySqlDbType.VarChar));
                            cmd.Parameters["i_good_writing"].Value = txtgood_writing.EditValue;
                            cmd.Parameters["i_good_writing"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_show_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_show_type"].Value = rbshow_type.EditValue;
                            cmd.Parameters["i_show_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;


                            cmd.ExecuteNonQuery();
                            txtidx.EditValue = cmd.Parameters["o_idx"].Value.ToString();
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtgshop_id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "배스트샵을 선택하세요 !");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.jpg";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/gshop_best_poto/" + Convert.ToString(txtgshop_id.EditValue);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtimg_url.EditValue = openFileDialog.FileName;
                    picP_IMG.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtimg_url.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/gshop_best_poto/";
                string sFtpPath2 = "/domalifefiles/files/gshop_best_poto/" + Convert.ToString(txtgshop_id.EditValue);

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == Convert.ToString(txtgshop_id.EditValue).ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtimg_url.EditValue = "https://media.domalife.net/files/gshop_best_poto/" + Convert.ToString(txtgshop_id.EditValue) + "/" + sFileName;


            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtidx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "G 멀티샵을 선택 또는 기본 정보를 저장하세요 !");
                return;
            }
            if (string.IsNullOrEmpty(this.dtS_DATE.Text))
            {
                dtS_DATE.EditValue = DateTime.Now;
            }
            if (string.IsNullOrEmpty(this.dtE_DATE.Text))
            {
                dtE_DATE.EditValue = DateTime.Now;
            }

            
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM_RM04_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtD_idx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_master_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_master_idx"].Value = Convert.ToInt32(txtidx.EditValue);
                            cmd.Parameters["i_master_idx"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_bef_url", MySqlDbType.VarChar));
                            cmd.Parameters["i_bef_url"].Value = txtbef_url.EditValue;
                            cmd.Parameters["i_bef_url"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_bef_date", MySqlDbType.Datetime));
                            cmd.Parameters["i_bef_date"].Value = dtS_DATE.EditValue;
                            cmd.Parameters["i_bef_date"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_after_url", MySqlDbType.VarChar));
                            cmd.Parameters["i_after_url"].Value = txtafter_url.EditValue;
                            cmd.Parameters["i_after_url"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_after_date", MySqlDbType.VarChar));
                            cmd.Parameters["i_after_date"].Value = dtE_DATE.EditValue3;
                            cmd.Parameters["i_after_date"].Direction = ParameterDirection.Input;
                            // 

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtremark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Dshow_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_Dshow_type"].Value = rbDshow_type.EditValue;
                            cmd.Parameters["i_Dshow_type"].Direction = ParameterDirection.Input;

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
                Open1();
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtidx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "배스트샵을 선택하세요 !");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.jpg";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/gshop_best_poto/" + Convert.ToString(txtgshop_id.EditValue);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtbef_url.EditValue = openFileDialog.FileName;
                    picBest_Pic1.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtbef_url.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/gshop_best_poto/";
                string sFtpPath2 = "/domalifefiles/files/gshop_best_poto/" + Convert.ToString(txtgshop_id.EditValue);

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == Convert.ToString(txtgshop_id.EditValue).ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtbef_url.EditValue = "https://media.domalife.net/files/gshop_best_poto/" + Convert.ToString(txtgshop_id.EditValue) + "/" + sFileName;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtidx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "배스트샵을 선택하세요 !");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.jpg";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/gshop_best_poto/" + Convert.ToString(txtgshop_id.EditValue);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtafter_url.EditValue = openFileDialog.FileName;
                    picBest_Pic2.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtafter_url.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/gshop_best_poto/";
                string sFtpPath2 = "/domalifefiles/files/gshop_best_poto/" + Convert.ToString(txtgshop_id.EditValue);

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == Convert.ToString(txtgshop_id.EditValue).ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtafter_url.EditValue = "https://media.domalife.net/files/gshop_best_poto/" + Convert.ToString(txtgshop_id.EditValue) + "/" + sFileName;
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            Eraser.Clear(this, "CLR2");
            picBest_Pic1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBest_Pic2.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
        }
    }
}