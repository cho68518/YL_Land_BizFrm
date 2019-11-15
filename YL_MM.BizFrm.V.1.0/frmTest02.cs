using Easy.Framework.Common;
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
using System.Web.Script.Serialization;
using Tamir.SharpSsh;
using Tamir.Streams;
using DevExpress.Utils.OAuth.Provider;

namespace YL_MM.BizFrm
{
    public partial class frmTest02 : FrmBase
    {
        public frmTest02()
        {
            InitializeComponent();
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();

            this.IsMenuVw = true;

        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            Console.Write("검색 질의:");

            //string input = Console.ReadLine();
            string input = efwTextEdit1.EditValue.ToString();

            string site = "https://dapi.kakao.com/v2/local/search/keyword.json";

            string query = string.Format("{0}?query={1}", site, input);

            WebRequest request = WebRequest.Create(query);



            string rkey = "b8a3be930345a32d8b101063b56dcc48";

            string header = "KakaoAK " + rkey;



            request.Headers.Add("Authorization", header);



            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            String json = reader.ReadToEnd();

            stream.Close();



            JavaScriptSerializer js = new JavaScriptSerializer();

            dynamic dob = js.Deserialize<dynamic>(json);

            dynamic docs = dob["documents"];

            object[] buf = docs;

            int length = buf.Length;

            for (int i = 0; i < length; i++)

            {

                string lname = docs[i]["place_name"];

                string x = docs[i]["x"];

                string y = docs[i]["y"];

                Console.WriteLine("{0},{1},{2}", lname, x, y);

            }


        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string sftpURL = "14.63.165.36";
                string sUserName = "root";
                string sPassword = "@dhkdldpf2!";
                int nPort = 22023;
                string sftpDirectory = "/domalifefiles/files/product/domamall";

                // 저장 경로 
                
                string LocalDirectory = "D:\\temp"; //Local directory from where the files will be uploaded
                string FileName = "test1.jpg";    //File name, which one will be uploaded
                 
                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);
                
                
                sSftp.Connect(nPort);
               // sSftp.Mkdir("/domalifefiles/files/product/domamall/temp");
                sSftp.Put(LocalDirectory + "/" + FileName, sftpDirectory + "/" + FileName);
                sSftp.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
