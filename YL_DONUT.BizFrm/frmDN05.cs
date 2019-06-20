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
    public partial class frmDN05 : FrmBase
    {
        public frmDN05()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "DN05";
            //폼명설정
            this.FrmName = "기간별 머니적립/사용 현황";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
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
            this.IsExcel = false;

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["RECV_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["RECV_AMOUNT"].SummaryItem.FieldName = "RECV_AMOUNT";
            gridView1.Columns["RECV_AMOUNT"].SummaryItem.DisplayFormat = "적립머니: {0:c}";

            gridView2.OptionsView.ShowFooter = true;
            gridView2.Columns["RECV_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["RECV_AMOUNT"].SummaryItem.FieldName = "RECV_AMOUNT";
            gridView2.Columns["RECV_AMOUNT"].SummaryItem.DisplayFormat = "사용머니: {0:c}";

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt1T.EditValue = DateTime.Now;

        }

        #endregion

        public override void Search()
        {
            DataSet ds = new DataSet();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
                {
                    ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_DN05_SELECT_01"
                                                                , this.dt1F.EditValue3
                                                                , this.dt1T.EditValue3);

                    efwGridControl1.DataBind(ds);
                    this.efwGridControl1.MyGridView.BestFitColumns();
                }
                else
                {
                    ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_DN05_SELECT_02"
                                                                , this.dt1F.EditValue3
                                                                , this.dt1T.EditValue3);

                    efwGridControl2.DataBind(ds);
                    this.efwGridControl2.MyGridView.BestFitColumns();
                }


                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void BtnSch1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void GridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.BackColor = Color.White;
            e.Appearance.ForeColor = Color.Red;
        }

        private void GridView2_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.BackColor = Color.White;
            e.Appearance.ForeColor = Color.Red;
        }

 
    }
}
