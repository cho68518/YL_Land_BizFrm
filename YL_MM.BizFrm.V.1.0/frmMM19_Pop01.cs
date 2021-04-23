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
using System.Collections;
using System.Net.Sockets;
using System.Web;

namespace YL_MM.BizFrm
{
    public partial class frmMM19_Pop01 : FrmPopUpBase
    {
        public int p_Id { get; set; }
        public String u_Id { get; set; }

        public frmMM19_Pop01()
        {
            InitializeComponent();
        }

        private void frmMM19_Pop01_Load(object sender, EventArgs e)
        {
            string sNavigate;
            //sNavigate = "http://domamall.domalife.net:8000/domamall/product/list.do?u_id=" + u_Id + "&chef_room_id = &mallType =private&my_shop=&m_web=Y&id=7&sortType=N";

            string u_id = "d1c907d4562fbce2144db86ae43ef7f6";
            string u_name = "조정현";
            u_name = HttpUtility.UrlEncode(u_name);
            string p_id = "2569";
            string p_name = "테스트 G제품";
            p_name = HttpUtility.UrlEncode(p_name);

            sNavigate = "https://callpay.eyeoyou.com/lgu_pay/pay_crossplatform.aspx?&u_id=" + u_id + "&u_name=" + u_name + "&p_id="+ p_id + "&opt_id="+ p_id + "&p_name="+ p_name + "&p_num=1&p_amt=100000&p_price=3000&u_email=finetoday@hanmail.net&pay_code=SC0040";


            webBrowser1.Navigate(sNavigate);
        }

    }
}
