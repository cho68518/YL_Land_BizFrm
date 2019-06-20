using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_DONUT.BizFrm
{
    public partial class frmDN03_Pop01 : FrmPopUpBase
    {
        public string pDATE { get; set; }
        public string pMONEY_TYPE { get; set; }

        public frmDN03_Pop01()
        {
            InitializeComponent();
        }

        private void FrmDN03_Pop01_Load(object sender, EventArgs e)
        {
            this.Text = "거래 상세정보";
            efwLabel2.Text = pDATE;
            efwLabel3.Text = "(" + pMONEY_TYPE.Replace("DM", "D") + "머니 적립내역)";

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["RECV_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["RECV_AMOUNT"].SummaryItem.FieldName = "RECV_AMOUNT";
            gridView1.Columns["RECV_AMOUNT"].SummaryItem.DisplayFormat = "적립머니: {0:c}";

            
        }

        private void FrmDN03_Pop01_Shown(object sender, EventArgs e)
        {
            Open();
        }

        private void Open()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_DN03_SELECT_03"
                                                            , pDATE
                                                            , pMONEY_TYPE);

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Cursor.Current = Cursors.Default;
        }

        private void GridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.BackColor = Color.White;
            e.Appearance.ForeColor = Color.Red;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
