using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
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
using System.Net;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;


namespace YL_GSHOP.BizFrm
{
    public partial class frmTest01 : FrmBase
    {
        DataSet _dsDeptInfo = null;
        DataSet _dsDeptInfo2 = null;

        public frmTest01()
        {
            InitializeComponent();
        }

        private void frmTest01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = false;
            this.IsSearch = false;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;
            bool usePassive = false;
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                string uri = "ftp://" + "222.233.52.238" + "/" + "2021-11-09" + ".jpg";
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("ylftp", "fosem5646#@!");
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (FileStream stream = File.OpenRead("2048"))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    stream.Close();
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(buffer, 0, buffer.Length);
                        reqStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {

            string filename = "c:/Temp/aaaa.jpg";

            string ftpServerIP = "222.233.52.238";
            string ftpUserID = "ylftp";
            string ftpPassword = "fosem5646#@!";
            string ftpPort = "21";

            bool usePassive = true;

            FileInfo fileInfo = new FileInfo(filename);

            string uri = String.Format("ftp://{0}:{1}/{2}/{3}", ftpServerIP, ftpPort, "/", fileInfo.Name);
            FtpWebRequest reqFTP;

            //FtpWebRequest object 생성
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            //아이디, 패스워드 검증
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            //서버에 대한 연결이 소멸되지 않아야 하면 true, 소멸되어야 하면 false 
            //KeepAlive의 기본값은 원래 true임.
            reqFTP.KeepAlive = false;


            //지정한 업로드 명령을 실행
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            //전송 타입설정
            reqFTP.UseBinary = true;

            //passive모드 사용여부
            reqFTP.UsePassive = usePassive;

            //서버에 파일사이즈를 알림
            reqFTP.ContentLength = fileInfo.Length;

            //버퍼사이즈 지정
            byte[] buff = new byte[2048];
            int contentLen;

            //파일 읽기
            FileStream fs = fileInfo.OpenRead();

            try
            {
                //업로드 할 파일 스트림을 가져옴.
                Stream strm = reqFTP.GetRequestStream();

                //2kb씩 파일 스트림을 읽은 후 길이 반환
                contentLen = fs.Read(buff, 0, 2048);

                //스트림을 다 읽을때까지 반복.
                while (contentLen != 0)
                {
                    //FTP에 파일을 기록
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, 2048);
                }
                strm.Close();
                fs.Close();

            }

            catch (Exception ex)
            {
            }

        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"d:\telecom");

            foreach (System.IO.FileInfo file in di.GetFiles())

                try
                {
                    using (MySqlConnection con2 = new MySqlConnection(ConstantLib.BasicConn_Real))
                    //using (MySqlConnection con = new MySqlConnection(ConstantLib.Member_Real))
                    {
                        using (MySqlCommand cmd2 = new MySqlCommand("domabiz.PROC_FILE_QUERY", con2))
                        {
                            con2.Open();
                            cmd2.CommandType = CommandType.StoredProcedure;

                            cmd2.Parameters.Add(new MySqlParameter("i_file_name", MySqlDbType.VarChar));
                            cmd2.Parameters["i_file_name"].Value = file.Name;
                            cmd2.Parameters["i_file_name"].Direction = ParameterDirection.Input;


                            cmd2.ExecuteNonQuery();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"d:\telecom");

            foreach (System.IO.FileInfo file in di.GetFiles())

                try
                {
                    using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                    {
                        sql.Query = "select chk " +
                                    "  from  domabiz.table38  " +
                                    "where file_name = '" + file.Name + "'  ";
                        DataSet ds = sql.selectQueryDataSet();

                        int nCnt = Convert.ToInt32(sql.selectQueryForSingleValue());

                        string fileName = @"d:\telecom\" + file.Name + " ";

                        if (nCnt == 0)
                            File.Delete(fileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
        }
    }
}
