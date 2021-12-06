using DevExpress.XtraCharts;
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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_GM.BizFrm
{
    public partial class frmGM11_Pop06 : FrmPopUpBase
    {
        public frmGM11_Pop06()
        {
            InitializeComponent();
        }

        private void btnFileOpen1_Click(object sender, EventArgs e)
        {
            picP_IMG.EditValue = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtPicPath1.EditValue = openFileDialog.FileName;

                    string str = txtPicPath1.EditValue.ToString();
                    str = str.Substring(str.Length - 3, 3);

                    if (str != "jpg" && str != "JPG")
                    {
                        MessageAgent.MessageShow(MessageType.Informational, "jpg 파일을 선택하세요.");
                        return;
                    }    
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
  
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            string filename = txtPicPath1.EditValue.ToString();

            string str = txtPicPath1.EditValue.ToString();
            str = str.Substring(str.Length - 12, 12);

            string chk = str.Substring(0,4) + str.Substring(4, 2) + str.Substring(6, 6);
            
            if (chk.Length != 12)
            {
                MessageAgent.MessageShow(MessageType.Informational, "파일을 형식을 확인하세요.");
                return;
            }

            // 폴더생성

            DirectoryInfo di = new DirectoryInfo(@"c:\temp");
            if (di.Exists == false)
            {
                di.Create();
            }

            // 선택된 파일을 위에서 만든 폴더에 이름을 바꿔 저장

            string sOldFile = txtPicPath1.EditValue.ToString();
            string sFileName = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 6);
            string sNewFile = "c:\\temp\\" + sFileName;
            System.IO.File.Delete(sNewFile);
            System.IO.File.Copy(sOldFile, sNewFile);



            string ftpServerIP = "222.233.52.238";
            string ftpUserID = "ylftp";
            string ftpPassword = "fosem5646#@!";
            string ftpPort = "21";

            bool usePassive = true;

            FileInfo fileInfo = new FileInfo(sNewFile);

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
            byte[] buff = new byte[1000000];
            int contentLen;

            //파일 읽기
            FileStream fs = fileInfo.OpenRead();

            try
            {
                //업로드 할 파일 스트림을 가져옴.
                Stream strm = reqFTP.GetRequestStream();

                //2kb씩 파일 스트림을 읽은 후 길이 반환
                contentLen = fs.Read(buff, 0, 1000000);

                //스트림을 다 읽을때까지 반복.
                while (contentLen != 0)
                {
                    //FTP에 파일을 기록
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, 1000000);
                }
                strm.Close();
                fs.Close();
                
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "업로드가 완료 되었습니다.");
            System.IO.File.Delete(sNewFile);
        }
    }
}
