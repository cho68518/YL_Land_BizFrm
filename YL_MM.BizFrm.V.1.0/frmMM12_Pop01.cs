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

namespace YL_MM.BizFrm
{
    public partial class frmMM12_Pop01 : FrmPopUpBase
    {
        public string OrderMallType { get; set; }
        public string U_Name { get; set; }
        public string u_NickName { get; set; }
        public string Login_Id { get; set; }
        public string O_Code { get; set; }
        public DateTime O_Date { get; set; }
                
        public frmMM12_Pop01()
        {
            InitializeComponent();
        }



        //private void BtnDispYes_Click(object sender, EventArgs e)
        //{

        //    popup = new frmMM03_Pop01();

        //    popup.Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString());

        //    popup.FormClosed += popup_FormClosed;
        //    popup.ShowDialog();
        //}

        //private void popup_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    popup.FormClosed -= popup_FormClosed;

        //    popup = null;
        //}

    }
}
