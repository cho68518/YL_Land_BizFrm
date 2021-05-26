using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
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
    public partial class frmMM26 : FrmBase
    {
        public frmMM26()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "MM26";
            //폼명설정
            this.FrmName = "회원별 머니보유 현황";
        }

        private void frmMM26_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;

        }

        public override void Search()
        {
            Cursor.Current = Cursors.WaitCursor;



            try
            {

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM26_SELECT_01"
                                                            , this.txtQ.Text
                                                            );
                efwGridControl1.DataBind(ds);



                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Cursor.Current = Cursors.Default;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void advBandedGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(advBandedGridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

    }
}
