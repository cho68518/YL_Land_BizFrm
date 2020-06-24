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
            gridView2.CustomUnboundColumnData += gridView2_CustomUnboundColumnData;
            gridView3.CustomUnboundColumnData += gridView3_CustomUnboundColumnData;
            gridView4.CustomUnboundColumnData += gridView4_CustomUnboundColumnData;
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
            txtIs_Use.EditValue = "Y";
            rbUse_type.EditValue = "Y";
            rbis_use.EditValue = "Y";
            rbCategory_Code_Q.EditValue = "T";
            rbis_use_q.EditValue = "Y";
            rbcategory_code.EditValue = "62a94474-915c-11e8-987e-02001f5c0016";

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

            this.efwGridControl2.BindControlSet(
             new ColumnControlSet("event_id", txtEvent_Id)
           , new ColumnControlSet("event_title", txtEvent_Title)
           , new ColumnControlSet("event_bn_imgurl", txtEvent_bn_imgurl)
           , new ColumnControlSet("event_ld_imgurl", txtEvent_ld_imgurl)
           , new ColumnControlSet("event_start_date", dtS_DATE)
           , new ColumnControlSet("event_end_date", dtE_DATE)
           , new ColumnControlSet("event_bn_imgname", txtEvent_bn_imgname)
           , new ColumnControlSet("event_bn_imgpath", txtEvent_bn_imgpath)
           , new ColumnControlSet("event_ld_imgname", txtEvent_ld_imgname)
           , new ColumnControlSet("event_ld_imgpath", txtEvent_ld_imgpath)
           , new ColumnControlSet("is_use", rbIs_Use_S)
            );
            this.efwGridControl2.Click += efwGridControl2_Click;

            this.efwGridControl3.BindControlSet(
             new ColumnControlSet("idx", txtTeb3_Idx)
           , new ColumnControlSet("subject", txtSubject)
           , new ColumnControlSet("use_type", rbuse_type_S)
           , new ColumnControlSet("contents", txtContents)
           , new ColumnControlSet("ImageURL", txtTab3_imgurl)
           , new ColumnControlSet("file_name1", txtTab3_imgname)
            );
            this.efwGridControl3.Click += efwGridControl3_Click;


            this.efwGridControl4.BindControlSet(
             new ColumnControlSet("idx", txtTeb4_Idx)
           , new ColumnControlSet("file_name",txtfile_name)
           , new ColumnControlSet("is_use", rbis_use)
           , new ColumnControlSet("ImageURL", txtTab4_imgurl)
           , new ColumnControlSet("category_code", rbcategory_code)
           , new ColumnControlSet("Tab4_imgname", txtTab4_imgname)
            );
            this.efwGridControl4.Click += efwGridControl4_Click;


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
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["event_id"].ToString() != "")
            {
                picAddBanner.LoadAsync(dr["event_bn_imgurl"].ToString());
                picAddLanding.LoadAsync(dr["event_ld_imgurl"].ToString());
            }
        }
        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl3.GetSelectedRow(0);
            if (dr != null && dr["idx"].ToString() != "")
            {
                picTab3_Banner.LoadAsync(dr["ImageURL"].ToString());
            }
        }
        private void efwGridControl4_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl4.GetSelectedRow(0);
            if (dr != null && dr["idx"].ToString() != "")
            {
                picTab4_Banner.LoadAsync(dr["ImageURL"].ToString());
            }
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

        void gridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {

            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = dr["event_bn_imgurl"].ToString();
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

        void gridView3_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
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
        void gridView4_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
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
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();  // 문자전송
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open4();
            }

        }
        public void Open1()
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
        public void Open2()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM18_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = txtIs_Use.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
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
        public void Open3()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM18_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = rbUse_type.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
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
        public void Open4()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM18_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Category_Code_Q", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = rbCategory_Code_Q.EditValue;

                        cmd.Parameters.Add("i_is_use_Q", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbis_use_q.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
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
        private void picAddBanner_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("3");
        }

        private void picAddLanding_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("4");
        }

        private void picTab3_Banner_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("5");
        }

        private void picTab4_Banner_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("6");
        }

        private void OpenDlg(string s1)
        {
            popup = new frmMM03_Pop02();
            //popup.Owner = this;

            if (s1 == "1")
                popup.pURL = txtimg_url.Text;
            else if (s1 == "2")
                popup.pURL = txtImg_link.Text;
            else if (s1 == "3")
                popup.pURL = txtEvent_bn_imgurl.Text;
            else if (s1 == "4")
                popup.pURL = txtEvent_ld_imgurl.Text;
            else if (s1 == "5")
                popup.pURL = txtTab3_imgurl.Text;
            else if (s1 == "6")
                popup.pURL = txtTab4_imgurl.Text;

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
        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            base.NewMode();

            Eraser.Clear(this, "CLR3");
        }
        private void efwSimpleButton4_Click(object sender, EventArgs e)
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
            string sftpDirectory = "/domalifefiles/files/events/banner/";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtEvent_bn_imgname.EditValue = openFileDialog.SafeFileName;
                    txtAddPicPath1.EditValue = openFileDialog.FileName;
                    picAddBanner.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtAddPicPath1.EditValue.ToString();
                //string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sFileName = txtEvent_bn_imgname.EditValue.ToString();
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
                    if (ay[i].ToString() == txtAddPicPath1.EditValue.ToString())
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
                txtEvent_bn_imgurl.EditValue = "https://media.domalife.net/files/events/banner/" + sFileName;
                txtEvent_bn_imgpath.EditValue = "/domalifefiles/files/events/banner/" + sFileName;
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
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
                    txtEvent_ld_imgname.EditValue = openFileDialog.SafeFileName;
                    txtAddPicPath2.EditValue = openFileDialog.FileName;
                    picAddLanding.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtAddPicPath2.EditValue.ToString();
                string sFileName = txtEvent_ld_imgname.EditValue.ToString();
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
                    if (ay[i].ToString() == txtAddPicPath2.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtEvent_ld_imgurl.EditValue = "https://media.domalife.net/files/events/landing/" + sFileName;
                txtEvent_ld_imgpath.EditValue = "/domalifefiles/files/events/landing/" + sFileName;
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            base.NewMode();

            Eraser.Clear(this, "CLR2");
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM18_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_Id", MySqlDbType.Int32));
                            cmd.Parameters["i_Event_Id"].Value = Convert.ToInt32(txtEvent_Id.EditValue);
                            cmd.Parameters["i_Event_Id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_Title", MySqlDbType.VarChar));
                            cmd.Parameters["i_Event_Title"].Value = txtEvent_Title.EditValue;
                            cmd.Parameters["i_Event_Title"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_start_date", MySqlDbType.VarChar));
                            cmd.Parameters["i_Event_start_date"].Value = dtS_DATE.EditValue3;
                            cmd.Parameters["i_Event_start_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_end_date", MySqlDbType.VarChar));
                            cmd.Parameters["i_Event_end_date"].Value = dtE_DATE.EditValue3;
                            cmd.Parameters["i_Event_end_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Is_Us", MySqlDbType.VarChar));
                            cmd.Parameters["i_Is_Us"].Value = rbIs_Use_S.EditValue;
                            cmd.Parameters["i_Is_Us"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_bn_imgurl", MySqlDbType.VarChar));
                            cmd.Parameters["i_Event_bn_imgurl"].Value = txtEvent_bn_imgurl.EditValue;
                            cmd.Parameters["i_Event_bn_imgurl"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_bn_imgname", MySqlDbType.VarChar));
                            cmd.Parameters["i_Event_bn_imgname"].Value = txtEvent_bn_imgname.EditValue;
                            cmd.Parameters["i_Event_bn_imgname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_AddPicPath1", MySqlDbType.VarChar));
                            cmd.Parameters["i_AddPicPath1"].Value = txtAddPicPath1.EditValue;
                            cmd.Parameters["i_AddPicPath1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_bn_imgpath", MySqlDbType.VarChar));
                            cmd.Parameters["i_Event_bn_imgpath"].Value = txtEvent_bn_imgpath.EditValue;
                            cmd.Parameters["i_Event_bn_imgpath"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_ld_imgurl", MySqlDbType.VarChar));
                            cmd.Parameters["i_Event_ld_imgurl"].Value = txtEvent_ld_imgurl.EditValue;
                            cmd.Parameters["i_Event_ld_imgurl"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_ld_imgname", MySqlDbType.VarChar));
                            cmd.Parameters["i_Event_ld_imgname"].Value = txtEvent_ld_imgname.EditValue;
                            cmd.Parameters["i_Event_ld_imgname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_AddPicPath2", MySqlDbType.VarChar));
                            cmd.Parameters["i_AddPicPath2"].Value = txtAddPicPath2.EditValue;
                            cmd.Parameters["i_AddPicPath2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Event_ld_imgpath", MySqlDbType.VarChar));
                            cmd.Parameters["i_Event_ld_imgpath"].Value = txtEvent_ld_imgpath.EditValue;
                            cmd.Parameters["i_Event_ld_imgpath"].Direction = ParameterDirection.Input;

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
                Open2();
            }
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
                //string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sFileName = txtTab3_imgname.EditValue.ToString();
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Delete(sNewFile);
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

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM18_SAVE_03", con))
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
                            cmd.Parameters["i_File_name"].Value = txtTab3_imgname.EditValue;
                            cmd.Parameters["i_File_name"].Direction = ParameterDirection.Input;

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
                Open3();
            }
        }

        private void efwSimpleButton10_Click(object sender, EventArgs e)
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
            string sftpDirectory = "/domalifefiles/files/pds/";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtTab4_imgname.EditValue = openFileDialog.SafeFileName;
                    txtTab4_PicPath1.EditValue = openFileDialog.FileName;
                    picTab4_Banner.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtTab4_PicPath1.EditValue.ToString();
                //string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sFileName = txtTab4_imgname.EditValue.ToString();
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Delete(sNewFile);
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                //저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/pds/";
                string sFtpPath2 = "/domalifefiles/files/pds/" + sFileName;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtTab4_PicPath1.EditValue.ToString())
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

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            base.NewMode();

            Eraser.Clear(this, "CLR4");
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM18_SAVE_04", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtTeb4_Idx.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_File_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_File_name"].Value = txtfile_name.EditValue;
                            cmd.Parameters["i_File_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_imgname", MySqlDbType.VarChar));
                            cmd.Parameters["i_imgname"].Value = txtTab4_imgname.EditValue;
                            cmd.Parameters["i_imgname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_category_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_category_code"].Value = rbcategory_code.EditValue;
                            cmd.Parameters["i_category_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = rbis_use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

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
                Open4();
            }
        }
    }
}
