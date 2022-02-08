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
using Tamir.SharpSsh;
using System.Collections;
using System.Net.Sockets;
using DevExpress.XtraGrid.Columns;
using System.Net;
using YL_DT.BizFrm;
using System.Data.SqlClient;

namespace YL_DT.BizFrm
{
    public partial class frmDT02_Pop01 : FrmPopUpBase
    {
        public int category_no { get; set; }
        public string story_name { get; set; }
        frmDT02_Pop02 popup;
        public frmDT02_Pop01()
        {
            InitializeComponent();
        }

        private void frmDT02_Pop01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            txtCategory_No.EditValue = category_no;
            txtStory_Name.EditValue = story_name;
            txtTeb3_Idx.EditValue = "0";
            rbUse_Type.EditValue = "Y";
            txtTab3_imgpath.EditValue = "";
            txtNow_image.EditValue = "";
            picTab3_Banner.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            Open1();
        }
        //신규
        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            txtTeb3_Idx.EditValue = "0";
            txtTab3_imgname.EditValue = "";
            txtTab3_PicPath1.EditValue = "";
            txtTab3_imgpath.EditValue = "";
            txtSub_Code.EditValue = "";
            picTab3_Banner.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");  
        }
        private void Open1()
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                SqlCommand cmd = new SqlCommand();
                sql.Query = " select t1.idx as idx, t1.sub_code as sub_code, t1.image_path as image_path, t1.is_best as is_best, t1.use_type as use_type, " +
                            "        t1.reg_date as reg_date, t1.remark as remark, t1.file_name as file_name " +
                            "   from domalife.tb_story_masters_images t1  " +
                            "   where t1.category_no =  '" + txtCategory_No.EditValue + "'  " +
                            "  order by t1.idx ";

                DataSet ds = sql.selectQueryDataSet();
                gridControl2.DataSource = ds.Tables[0];
            }
        }

        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();

        private void layoutView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DataRow dr = (e.Row as DataRowView).Row;
            string url = string.Empty;

            if (e.Column.FieldName == "Image1")
            {
                url = dr["image_path"].ToString();
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
        //저장
        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            if (txtNow_image.EditValue.ToString() == "" ^ txtNow_image.EditValue.ToString() == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "이미지를 선택하세요!");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_Pop01_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtTeb3_Idx.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_category_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_category_no"].Value = txtCategory_No.EditValue;
                            cmd.Parameters["i_category_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sub_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_sub_code"].Value = txtSub_Code.EditValue;
                            cmd.Parameters["i_sub_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_best", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_best"].Value = chkIs_Best.EditValue;
                            cmd.Parameters["i_is_best"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_use_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_use_type"].Value = rbUse_Type.EditValue;
                            cmd.Parameters["i_use_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_File_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_File_name"].Value = txtNow_image.EditValue;
                            cmd.Parameters["i_File_name"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }
        //이미지 업로드
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
            string sftpDirectory = "/domalifefiles/files/story_image/" + txtCategory_No.EditValue;


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

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/story_image/";
                string sFtpPath2 = "/domalifefiles/files/story_image/" + txtCategory_No.EditValue;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtCategory_No.EditValue.ToString())
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
            }
            Open1();
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_Pop01_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtTeb3_Idx.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;


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

        private void gridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.layoutView1.GetDataRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                txtTeb3_Idx.EditValue = layoutView1.GetFocusedRowCellValue("idx").ToString();
                txtSub_Code.EditValue = layoutView1.GetFocusedRowCellValue("sub_code").ToString();
                chkIs_Best.EditValue = layoutView1.GetFocusedRowCellValue("is_best").ToString();
                rbUse_Type.EditValue = layoutView1.GetFocusedRowCellValue("use_type").ToString();
                txtRemark.EditValue = layoutView1.GetFocusedRowCellValue("remark").ToString();
                txtNow_image.EditValue = layoutView1.GetFocusedRowCellValue("file_name").ToString();
                txtTab3_imgpath.EditValue = layoutView1.GetFocusedRowCellValue("image_path").ToString();
                picTab3_Banner.LoadAsync(txtTab3_imgpath.EditValue.ToString());
            }

        }

        private void picTab3_Banner_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("1");
        }
        private void OpenDlg(string s1)
        {
            popup = new frmDT02_Pop02();
            //popup.Owner = this;

            if (s1 == "1")
               if ( txtTab3_imgpath.EditValue.ToString() == "")
                {
                    txtTab3_imgpath.EditValue = "http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg";
                }
                else
                {
                    popup.pURL = txtTab3_imgpath.EditValue.ToString();
                }
                

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
