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
using YL_MM.BizFrm;
using Tamir.SharpSsh;
using System.Collections;
using System.Net.Sockets;
using DevExpress.XtraGrid.Columns;
using System.Net;
using YL_MM.BizFrm.Dlg;


namespace YL_MM.BizFrm
{
    public partial class frmMM18 : FrmBase
    {
        frmMM03_Pop02 popup;
        public frmMM18()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "MM18";
            //폼명설정
            this.FrmName = "매인 배너관리";
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
        }

        private void frmMM18_Load(object sender, EventArgs e)
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

            gridView1.OptionsView.ShowFooter = true;

            rbStoryType.EditValue = "M";
            rbShowType.EditValue = "Y";
            rbLink.EditValue = "L";

            //SetCmb();
            this.efwGridControl1.BindControlSet(
             new ColumnControlSet("idx", txtIdx)
           , new ColumnControlSet("ImageURL", txtimg_url)
           , new ColumnControlSet("show_type", rbShowType)
           , new ColumnControlSet("img_link", txtImg_link)
           , new ColumnControlSet("remark", txtRemark)
           , new ColumnControlSet("show_turn", txtShow_Turn)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;
            SetCmb();

        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            if (dr != null && dr["category_no"].ToString() != "")
            {
                cmbCategory_no.EditValue = dr["category_no"].ToString();
                rbLink.EditValue = dr["link_type"].ToString();
                picBanner.LoadAsync(dr["ImageURL"].ToString());
                picLanding.LoadAsync(dr["img_link"].ToString());
            }
            //picBest_Pic1.LoadAsync(txtPic_Url1.EditValue.ToString());
            //picBest_Pic2.LoadAsync(txtPic_Url2.EditValue.ToString());
            //picBest_Pic3.LoadAsync(txtPic_Url3.EditValue.ToString());
            //picBest_Pic4.LoadAsync(txtPic_Url4.EditValue.ToString());
            //picBest_Pic5.LoadAsync(txtPic_Url5.EditValue.ToString());
        }

        private void SetCmb()
        {
            // 샵구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00036' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCategory_no, codeArray);
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



        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();



        public override void Search()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM18_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = rbStoryType.EditValue;

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

        private void rbStoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
            Search();
        }

        private void btnFileOpen4_Click(object sender, EventArgs e)
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
            string sftpDirectory = "/domalifefiles/files/events/banner/" ;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtOrg_FileName.EditValue = openFileDialog.SafeFileName;
                    txtPicPath1.EditValue = openFileDialog.FileName;
                    picBanner.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtPicPath1.EditValue.ToString();
                //string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sFileName = txtOrg_FileName.EditValue.ToString();
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Delete(sNewFile);
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

               //저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/events/banner/";
                string sFtpPath2 = "/domalifefiles/files/events/banner/" + sFileName;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtPicPath1.EditValue.ToString())
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
                txtimg_url.EditValue = "https://media.domalife.net/files/events/banner/" + sFileName;
            }
        }

        private void btnLink_Click(object sender, EventArgs e)
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
            string sftpDirectory = "/domalifefiles/files/events/landing/";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtOrg_FileName.EditValue = openFileDialog.SafeFileName;
                    txtPicPath1.EditValue = openFileDialog.FileName;
                    picLanding.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtPicPath1.EditValue.ToString();
                string sFileName = txtOrg_FileName.EditValue.ToString();
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Delete(sNewFile);
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                //저장 경로에 있는지 체크

                ArrayList ay = sSftp.GetFileList(sftpDirectory);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtPicPath1.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtImg_link.EditValue = "https://media.domalife.net/files/events/landing/" + sFileName;

            }
        }

        private void efwRadioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
            if (rbLink.EditValue.ToString() == "L")
            {
                btnLink.Enabled = false;
                txtImg_link.Enabled = true;
            }
            else
            {
                btnLink.Enabled = true;
                txtImg_link.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM18_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_show_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_show_type"].Value = rbShowType.EditValue;
                            cmd.Parameters["i_show_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_category_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_category_no"].Value = Convert.ToInt32(cmbCategory_no.EditValue);
                            cmd.Parameters["i_category_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_link_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_link_type"].Value = rbLink.EditValue;
                            cmd.Parameters["i_link_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_img_url", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url"].Value = txtimg_url.EditValue;
                            cmd.Parameters["i_img_url"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_img_link", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_link"].Value = txtImg_link.EditValue;
                            cmd.Parameters["i_img_link"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_show_turn", MySqlDbType.Int32));
                            cmd.Parameters["i_show_turn"].Value = Convert.ToInt32(txtShow_Turn.EditValue);
                            cmd.Parameters["i_show_turn"].Direction = ParameterDirection.Input;

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



        private void picBanner_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("1");
        }

        private void picLanding_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("2");
        }
        private void OpenDlg(string s1)
        {
            popup = new frmMM03_Pop02();
            //popup.Owner = this;

            if (s1 == "1")
                popup.pURL = txtimg_url.Text;
            else if (s1 == "2")
                popup.pURL = txtImg_link.Text;


            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");

        }


    }
}
