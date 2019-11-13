using Easy.Framework.Common;
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
using System.Xml;
using YL_MA.BizFrm.Dlg;

namespace YL_MA.BizFrm
{
    public partial class frmTest01 : FrmBase
    {

        public frmTest01()
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
           
        }

        string currentPage = "";  //현재 페이지
        string countPerPage = ""; //1페이지당 출력 갯수
        string confmKey = @"U01TX0FVVEgyMDE5MDQyOTEzMjc0MTEwODY4OTA=";
        string keyword = string.Empty;
        string apiurl = string.Empty;

        private void TxtZIPNO_EditValueChanged(object sender, EventArgs e)
        {
            ////frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = txtZIPNO, ParentAddr1 = txtADDR1, ParentAddr2 = txtADDR2 };
            ////FrmInfo.COMPANYCD = "YL01";
            ////FrmInfo.COMPANYNAME = "";
            ////FrmInfo.ShowDialog();
            ////txtADDR2.Focus();

            //try
            //{
            //    keyword = txtSch.Text.Trim();
            //    apiurl = "http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage=" + currentPage + "&countPerPage=" + countPerPage + "&keyword=" + keyword + "&confmKey=" + confmKey;

            //    //textBox2.Text = apiurl + "\r\n";
            //    WebClient wc = new WebClient();
            //    XmlReader read = new XmlTextReader(wc.OpenRead(apiurl));

            //    DataSet ds = new DataSet();
            //    ds.ReadXml(read);

            //    txtADDR1.EditValue = ds.Tables[0].Rows[0]["roadAddr"].ToString();
            //    txtADDR2.EditValue = ds.Tables[0].Rows[0]["jibunAddr"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            //}
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtSch.Text))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 도로명 또는 읍,면동,을 입력하세요!");
                    return;
                }

                keyword = txtSch.Text.Trim();
                apiurl = "http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage=" + currentPage + "&countPerPage=" + countPerPage + "&keyword=" + keyword + "&confmKey=" + confmKey;

                //textBox2.Text = apiurl + "\r\n";
                WebClient wc = new WebClient();
                XmlReader read = new XmlTextReader(wc.OpenRead(apiurl));

                DataSet ds = new DataSet();
                ds.ReadXml(read);
                DataRow[] rows = ds.Tables[0].Select();

                txtADDR1.EditValue = rows[0]["roadAddr"].ToString();

                //txtADDR1.EditValue = ds.Tables[0].Rows[0]["roadAddr"].ToString();
                //txtADDR2.EditValue = ds.Tables[0].Rows[0]["jibunAddr"].ToString();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
    }
}
