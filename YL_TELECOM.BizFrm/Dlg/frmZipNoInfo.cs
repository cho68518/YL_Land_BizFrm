﻿using Easy.Framework.Common;
using Easy.Framework.WinForm.Control;
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


namespace YL_TELECOM.BizFrm.Dlg
{
    public partial class frmZipNoInfo : DevExpress.XtraEditors.XtraForm
    {
        #region Propertys

        public string COMPANYCD
        {
            get;
            set;
        }

        public string COMPANYNAME
        {
            get;
            set;
        }

        public efwButtonEdit ParentBtn
        {
            get;
            set;
        }

        public efwTextEdit ParentAddr1
        {
            get;
            set;
        }

        public efwTextEdit ParentAddr2
        {
            get;
            set;
        }

        #endregion

        public frmZipNoInfo()
        {
            InitializeComponent();
        }

        private void frmZipNoInfo_Load(object sender, EventArgs e)
        {
            txtSch.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string currentPage = "";  //현재 페이지
        string countPerPage = ""; //1페이지당 출력 갯수
        string confmKey = @"U01TX0FVVEgyMDE5MDQyOTEzMjc0MTEwODY4OTA=";
        string keyword = string.Empty;
        string apiurl = string.Empty;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtSch.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 도로명 또는 읍,면,동을 입력하세요!");
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(this.txtSch.Text))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 도로명 또는 읍,면,동을 입력하세요!");
                    return;
                }
                keyword = txtSch.Text.Trim();
                apiurl = "http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage=" + currentPage + "&countPerPage=" + countPerPage + "&keyword=" + keyword + "&confmKey=" + confmKey;

                //textBox2.Text = apiurl + "\r\n";
                WebClient wc = new WebClient();
                XmlReader read = new XmlTextReader(wc.OpenRead(apiurl));

                DataSet ds = new DataSet();
                ds.ReadXml(read);

                //dataGridView1.DataSource = ds.Tables[0];

                DataRow[] rows = ds.Tables[0].Select();

                //textBox2.Text += rows[0]["totalCount"].ToString();

                //if (rows[0]["totalCount"].ToString() != "0")
                //{
                //    dataGridView2.DataSource = ds.Tables[1];
                //}

                //dataGridView1.DataSource = ds.Tables[1];

                efwGridControl1.DataBind(ds.Tables[1]);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void txtSch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void efwGridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (ParentBtn != null)
            {
                DataRow dr = this.efwGridControl1.GetSelectedRow(0);
                this.ParentBtn.Text = dr["zipNo"].ToString();
                this.ParentBtn.EditValue2 = dr["zipNo"];
                this.ParentAddr1.EditValue = dr["roadAddr"];
                this.ParentAddr2.EditValue = "";
                this.ParentAddr2.Focus();
                this.Close();
            }
        }

        private void FrmZipNoInfo_Shown(object sender, EventArgs e)
        {
            txtSch.Focus();
        }
    }
}
