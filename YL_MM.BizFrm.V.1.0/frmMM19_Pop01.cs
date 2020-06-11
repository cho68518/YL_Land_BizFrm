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
            sNavigate = "http://domamall.domalife.net:8000/domamall/product/list.do?u_id=" + u_Id + "&chef_room_id = &mallType =private&my_shop=&m_web=Y&id=7&sortType=N";
            webBrowser1.Navigate(sNavigate);
        }
    }
}
