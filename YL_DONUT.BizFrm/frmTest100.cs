using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Export;
using Easy.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_DONUT.BizFrm
{
    public partial class frmTest100 : FrmBase
    {
        public frmTest100()
        {
            InitializeComponent();
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            //저장
            richEditControl1.SaveDocument("SavedDocument.rtf", DocumentFormat.Rtf);
            System.IO.FileInfo fi = new System.IO.FileInfo("SavedDocument.rtf");
            string msg = String.Format("The size of the file is {0:#,#} bytes.", fi.Length.ToString("#,#"));
            MessageBox.Show(msg);
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            //조회
        }

        private void richEditControl1_BeforeExport(object sender, BeforeExportEventArgs e)
        {
        }
    }
}
