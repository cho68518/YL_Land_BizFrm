using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Easy.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraNavBar;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using YL_COMM.BizFrm;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace YL_DONUT.BizFrm
{
    public partial class frmTest01 : FrmBase
    {
        public frmTest01()
        {
            InitializeComponent();

            this.gridView1.MouseDown += GridView1_MouseDown;
            this.efwGridControl1.DataSource = CreateTable(5);

        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Pumpkin");

            //This set the style to use skin technology
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;

            //Here we specify the skin to use by its name           
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //DataSet ds = new DataSet();
            ////efwGridControl1.DataSource = ds;
            //efwGridControl1.DataBind(ds);

            //gridView1.InitNewRow += GridView1_InitNewRow;
        }


        #endregion

     



        #region 엘리먼트 구하기 - GetElement(name, style, text, expanded)

        /// <summary>
        /// 엘리먼트 구하기
        /// </summary>
        /// <param name="name">명칭</param>
        /// <param name="style">스타일</param>
        /// <param name="text">텍스트</param>
        /// <param name="expanded">확장 여부</param>
        /// <returns>아코디언 컨트롤 엘리먼트</returns>
        private AccordionControlElement GetElement(string name, ElementStyle style, string text, bool expanded = true)
        {
            AccordionControlElement element = new AccordionControlElement
            {
                Name = name,
                Style = style,
                Text = text,
                Expanded = true
            };

            return element;
        }

        #endregion

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            //XtraReport report = new XtraReport();
            //report.DataSource = CreateData();

            //ReportPrintTool tool = new ReportPrintTool(report);
            //tool.ShowPreview();

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "select a.id as ID_Q, a.p_code as P_CODE_Q,  a.p_name as P_NAME_Q,  " +
                            "       b.idx as IDX_Q, b.name as NAME_Q, b.unit as UNIT_Q, b.qty as QTY_Q, b.endornot as ENDORNOT_Q, b.remark as REMARK_Q, prodseq as PRODSEQ_Q " +
                            "  from domamall.tb_am_product_masters a,  " +
                            "        domamall.tb_am_setproduct b  " +
                            "  where a.id = 1781 and " +
                            "        a.id = b.id  " +
                            "  order by a.p_name, b.idx ";

                ds = con.selectQueryDataSet();
                //ReportDataAgent.Instance().AddDataSource("DataSet1", ds.Tables[0]);
            }

            ds.WriteXmlSchema("DataSet1.xsd");

            XtraReport1 report = new XtraReport1();
            //report.parameter1.Value = 30;
            //report.parameter1.Visible = false;
            report.DataSource = ds.Tables[0];
            report.ShowPreview();
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            navBarControl1.Dock = DockStyle.Right;
            navBarControl1.PaintStyleName = "SkinExplorerBarView";
            navBarControl1.SmallImages = imageList1;
            NavBarItem separator = navBarControl1.Items.Add(true);

            //-----------------------------------------------------------------------------------
            NavBarGroup groupLocal = new NavBarGroup("Local");
            NavBarItem itemInbox = new NavBarItem("Inbox");
            itemInbox.SmallImageIndex = 0;
            NavBarItem itemOutbox = new NavBarItem("Outbox");
            itemOutbox.SmallImageIndex = 0;
            NavBarItem itemSentItems = new NavBarItem("Sent Items");
            itemSentItems.SmallImageIndex = 0;
            itemSentItems.Enabled = false;
            //-----------------------------------------------------------------------------------
            NavBarGroup groupLocal2 = new NavBarGroup("Local2");
            NavBarItem itemInbox2 = new NavBarItem("Inbox2");
            itemInbox2.SmallImageIndex = 0;
            NavBarItem itemOutbox2 = new NavBarItem("Outbox2");
            itemOutbox2.SmallImageIndex = 0;
            NavBarItem itemSentItems2 = new NavBarItem("Sent Items2");
            itemSentItems2.SmallImageIndex = 0;
            itemSentItems2.Enabled = false;
            //-----------------------------------------------------------------------------------

            navBarControl1.BeginUpdate();
            navBarControl1.Groups.Add(groupLocal);
            groupLocal.ItemLinks.Add(itemInbox);
            groupLocal.ItemLinks.Add(separator);
            groupLocal.ItemLinks.Add(itemOutbox);
            groupLocal.ItemLinks.Add(separator);
            groupLocal.ItemLinks.Add(itemSentItems);
            groupLocal.Expanded = true;

            navBarControl1.Groups.Add(groupLocal2);
            groupLocal2.ItemLinks.Add(itemInbox2);
            groupLocal2.ItemLinks.Add(separator);
            groupLocal2.ItemLinks.Add(itemOutbox2);
            groupLocal2.ItemLinks.Add(separator);
            groupLocal2.ItemLinks.Add(itemSentItems2);
            groupLocal2.Expanded = true;

            navBarControl1.EndUpdate();
            // Specify the event handler which will be invoked when any link is clicked. 
            navBarControl1.LinkClicked += new NavBarLinkEventHandler(navBar_LinkClicked);
        }

        private void navBar_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            MessageBox.Show(string.Format("The {0} link has been clicked", e.Link.Caption));
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            string query = txtAddr.EditValue.ToString(); // 검색할 주소
            string url = "https://naveropenapi.apigw.ntruss.com/map-geocode/v2/geocode?query=" + query; // 결과가 JSON 포맷
            // string url = "https://openapi.naver.com/v3/map/geocode.xml?query=" + query;  // 결과가 XML 포맷

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //request.Headers.Add("X-Naver-Client-Id", "6zafgdatx9"); // 클라이언트 아이디
            //request.Headers.Add("X-Naver-Client-Secret", "L66RXbCynYQqXd0GyMbGPJIZmRBBGKrrsDR5MjkV");       // 클라이언트 시크릿

            request.Headers.Add("X-NCP-APIGW-API-KEY-ID", "f8oxrloutm"); // 클라이언트 아이디
            request.Headers.Add("X-NCP-APIGW-API-KEY", "OU1DJDLlOiQXemqkYGb3pIMP5ZUXC5NFqSz3uWDO");       // 클라이언트 시크릿

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();

            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                //Console.WriteLine(text);
                efwMemoEdit1.EditValue = text;

                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(text);

                DataTable dataTable = dataSet.Tables["Table1"];

                Console.WriteLine("Count : " + dataTable.Rows.Count);

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(row["roadAddress"] + " - " + row["jibunAddress"]);
                }

                //efwGridControl1.DataBind(ds);
                //dataGridView1.DataSource = ds;
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
        }

        private void EfwSimpleButton3_Click(object sender, EventArgs e)
        {
            string query = txtAddr.EditValue.ToString(); // 검색할 주소
            string url = "https://naveropenapi.apigw.ntruss.com/map-geocode/v2/geocode?query=" + query; // 결과가 JSON 포맷
            // string url = "https://openapi.naver.com/v3/map/geocode.xml?query=" + query;  // 결과가 XML 포맷

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Headers.Add("X-NCP-APIGW-API-KEY-ID", "f8oxrloutm"); // 클라이언트 아이디
            request.Headers.Add("X-NCP-APIGW-API-KEY", "OU1DJDLlOiQXemqkYGb3pIMP5ZUXC5NFqSz3uWDO");       // 클라이언트 시크릿

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();

            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();

                //Console.WriteLine(text);

                //RootObject addr = JsonConvert.DeserializeObject<RootObject>(text);
                //Console.WriteLine(addr.addresses..roadAddress);

                var addr = JsonConvert.DeserializeObject<clsAddress.RootObject>(text);

                Console.WriteLine(addr.addresses);
                
                List<clsAddress.Address> allRecords = addr.addresses;

                for (int i = 0; i < allRecords.Count; i++)
                {
                    Console.WriteLine(allRecords[i].jibunAddress);
                    Console.WriteLine(allRecords[i].roadAddress);
                    Console.WriteLine(allRecords[i].x);
                    Console.WriteLine(allRecords[i].y);
                }

                DataTable dt2 = clsDataConvert.ListToDataTable(allRecords);
                dataGridView1.DataSource = dt2;
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
        }

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            string query = txtAddr.EditValue.ToString(); // 검색할 주소
            string url = "https://naveropenapi.apigw.ntruss.com/map-place/v1/search?query=" + query + "&coordinate=127.1054328,37.3595963"; // 결과가 JSON 포맷

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Headers.Add("X-NCP-APIGW-API-KEY-ID", "hgzn99ko5d"); // 클라이언트 아이디
            request.Headers.Add("X-NCP-APIGW-API-KEY", "vV7ylEQC12bj1xGTzl3IjD762oRx3GowfW0qImJd");       // 클라이언트 시크릿

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();

            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();

                //Console.WriteLine(text);

                //RootObject addr = JsonConvert.DeserializeObject<RootObject>(text);
                //Console.WriteLine(addr.addresses..roadAddress);

                var addr = JsonConvert.DeserializeObject<clsAddrSearch.RootObject>(text);

                List<clsAddrSearch.Place> allRecords = addr.places;

                for (int i = 0; i < allRecords.Count; i++)
                {
                    Console.WriteLine(allRecords[i].name);
                    Console.WriteLine(allRecords[i].road_address);
                    Console.WriteLine(allRecords[i].jibun_address);
                    Console.WriteLine(allRecords[i].phone_number);
                    Console.WriteLine(allRecords[i].x);
                    Console.WriteLine(allRecords[i].y);
                    Console.WriteLine(allRecords[i].distance);
                    Console.WriteLine(allRecords[i].sessionId);
                }

                DataTable dt2 = clsDataConvert.ListToDataTable(allRecords);
                dataGridView1.DataSource = dt2;
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
            }
        }

        private void CheckButton1_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton btn = sender as CheckButton;
            if (btn.Checked)
            {
                btn.Appearance.BackColor = Color.LightGreen;
                btn.Appearance.BackColor2 = Color.DarkGreen;
            }
            else
            {
                //btn.Appearance.BackColor = Color.LightBlue;
                //btn.Appearance.BackColor2 = Color.DarkBlue;
                btn.Appearance.BackColor = Color.Transparent;
                btn.Appearance.BackColor2 = Color.Transparent;
            }
        }

        private void EfwSimpleButton5_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
            //int rowHandler = gridView1.RowCount - 1;

            //gridView1.SetRowCellValue(rowHandler, "col1", "0");
            //gridView1.SetRowCellValue(rowHandler, "col2", "0");
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            ColumnView View = sender as ColumnView;
            View.SetRowCellValue(e.RowHandle, View.Columns["col1"], "");
            View.SetRowCellValue(e.RowHandle, View.Columns["col2"], "");
        }




        private void GridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gridView1.CalcHitInfo(e.Location);
            ClearForm(); if (e.Button == MouseButtons.Right && (hitInfo.HitTest == GridHitTest.RowCell || hitInfo.HitTest == GridHitTest.Row))
            {
                DateEdit dateEdit = new DateEdit();
                dateEdit.EditValue = this.gridView1.GetRowCellValue(hitInfo.RowHandle, gridView1.Columns["Date"]);
                dateEditForm = new VistaPopupDateEditForm(dateEdit);
                dateEditForm.Calendar.EditDateModified += new EventHandler(OnEditDateModified); dateEditForm.ClosePopup();
                dateEditForm.Tag = hitInfo.RowHandle; dateEditForm.Location = PointToScreen(e.Location);
                dateEditForm.Size = dateEditForm.CalcFormSize(); dateEditForm.ShowPopupForm();
            }
        }

        private DataTable CreateTable(int rowCount)
        {
            DataTable table = new DataTable();
            table.Columns.Add("@STATUS", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Number", typeof(int));
            table.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < rowCount; i++)
            {
                //table.Rows.Add(new object[] { string.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) });
                table.Rows.Add(new object[] { "A", string.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) });
            }
            return table;
        }

        VistaPopupDateEditForm dateEditForm;

        private void ClearForm()
        {
            if (dateEditForm != null)
            {
                dateEditForm.Calendar.EditDateModified -= new EventHandler(OnEditDateModified);
                dateEditForm.ClosePopup();
                dateEditForm.Dispose();
            }
        }

        private void OnEditDateModified(object sender, EventArgs e)
        {
            int rowHandle = Convert.ToInt32(dateEditForm.Tag);
            gridView1.SetRowCellValue(rowHandle, gridView1.Columns["Date"], dateEditForm.Calendar.DateTime);
            dateEditForm.HidePopupForm();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            txtFileName.EditValue = openFileDialog.FileName;

            Upload(openFileDialog.FileName);
        }

        private void Upload(string fileName)
        {
            try
            {
                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential("twoone@admin", "twoone5906");
                client.UploadFile("ftp://14.34.8.38/Upgrade/ikoas/111.xml.zip", @"D:\111.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnGuId_Click(object sender, EventArgs e)
        {
            string sGuId = string.Empty;

            sGuId = Guid.NewGuid().ToString().Replace("-", "");

            txtGuId.EditValue = sGuId;
            txtImgName.EditValue = "gshop_00000016" + "_" + sGuId + ".";
        }

        private void checkButton2_CheckedChanged(object sender, EventArgs e)
        {
            //http://domamall.domalife.net:8000/domamall/product/list.do?u_id=d1c907d4562fbce2144db86ae43ef7f6&chef_room_id=&mallType=private&my_shop=&m_web=Y&id=7&sortType=N

            string url = "http://domamall.domalife.net:8000/domamall/product/list.do";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            //전달할 파라메타
            string sendData = "u_id=d1c907d4562fbce2144db86ae43ef7f6&chef_room_id=&mallType=private&my_shop=&m_web=Y&id=7&sortType=N";

            byte[] buffer;
            buffer = Encoding.Default.GetBytes(sendData);
            request.ContentLength = buffer.Length;
            Stream sendStream = request.GetRequestStream();
            sendStream.Write(buffer, 0, buffer.Length);
            sendStream.Close();

        }

        private void checkButton3_CheckedChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://domamall.domalife.net:8000/domamall/product/list.do?u_id=d1c907d4562fbce2144db86ae43ef7f6&chef_room_id=&mallType=private&my_shop=&m_web=Y&id=7&sortType=N");
        }

        private void checkButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("http://domamall.domalife.net:8000/domamall/product/list.do?u_id=d1c907d4562fbce2144db86ae43ef7f6&chef_room_id=&mallType=private&my_shop=&m_web=Y&id=7&sortType=N");
        }
    }
}
