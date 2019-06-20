using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmDN03 : FrmBase
    {
        frmDN03_Pop01 popup;
        frmDN03_Pop02 popup2;

        public frmDN03()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "DN03";
            //폼명설정
            this.FrmName = "기간별 머니 거래현황";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt1T.EditValue = DateTime.Now;

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["AD_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["AD_MONEY"].SummaryItem.FieldName = "AD_MONEY";
            gridView1.Columns["AD_MONEY"].SummaryItem.DisplayFormat = "AD: {0:c}";

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["TD_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["TD_MONEY"].SummaryItem.FieldName = "TD_MONEY";
            gridView1.Columns["TD_MONEY"].SummaryItem.DisplayFormat = "TD: {0:c}";

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["D_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["D_MONEY"].SummaryItem.FieldName = "D_MONEY";
            gridView1.Columns["D_MONEY"].SummaryItem.DisplayFormat = "D: {0:c}";

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["GD_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["GD_MONEY"].SummaryItem.FieldName = "GD_MONEY";
            gridView1.Columns["GD_MONEY"].SummaryItem.DisplayFormat = "GD: {0:c}";

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["TOT_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["TOT_MONEY"].SummaryItem.FieldName = "TOT_MONEY";
            gridView1.Columns["TOT_MONEY"].SummaryItem.DisplayFormat = "합계: {0:c}";

            gridView2.OptionsView.ShowFooter = true;
            gridView2.Columns["AD_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["AD_MONEY"].SummaryItem.FieldName = "AD_MONEY";
            gridView2.Columns["AD_MONEY"].SummaryItem.DisplayFormat = "AD: {0:c}";

            gridView2.OptionsView.ShowFooter = true;
            gridView2.Columns["TD_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["TD_MONEY"].SummaryItem.FieldName = "TD_MONEY";
            gridView2.Columns["TD_MONEY"].SummaryItem.DisplayFormat = "TD: {0:c}";

            gridView2.OptionsView.ShowFooter = true;
            gridView2.Columns["D_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["D_MONEY"].SummaryItem.FieldName = "D_MONEY";
            gridView2.Columns["D_MONEY"].SummaryItem.DisplayFormat = "D: {0:c}";

            gridView2.OptionsView.ShowFooter = true;
            gridView2.Columns["GD_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["GD_MONEY"].SummaryItem.FieldName = "GD_MONEY";
            gridView2.Columns["GD_MONEY"].SummaryItem.DisplayFormat = "GD: {0:c}";

            gridView2.OptionsView.ShowFooter = true;
            gridView2.Columns["TOT_MONEY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["TOT_MONEY"].SummaryItem.FieldName = "TOT_MONEY";
            gridView2.Columns["TOT_MONEY"].SummaryItem.DisplayFormat = "합계: {0:c}";

            
        }

        #endregion

        private void BtnSch1_Click(object sender, EventArgs e)
        {
            if (dt1F.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "기간을 선택하세요!");
                dt1F.Focus();
                return;
            }

            if (dt1T.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "기간을 선택하세요!");
                dt1T.Focus();
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            Open1();
            Open2();

            decimal amt1 = Convert.ToDecimal(gridView1.Columns["TOT_MONEY"].SummaryItem.SummaryValue);
            decimal amt2 = Convert.ToDecimal(gridView2.Columns["TOT_MONEY"].SummaryItem.SummaryValue);

            this.lblResAmount.Text = string.Format("{0:#,###}", amt1 - amt2);

            Cursor.Current = Cursors.Default;
        }

        private void Open1()
        {
            try
            {
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_DN03_SELECT_01"
                                                            , this.dt1F.EditValue3
                                                            , this.dt1T.EditValue3);

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open2()
        {
            try
            {
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_DN03_SELECT_02"
                                                            , this.dt1F.EditValue3
                                                            , this.dt1T.EditValue3);

                efwGridControl2.DataBind(ds);
                this.efwGridControl2.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
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

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            var rowIndex = gv.FocusedRowHandle;
            var columnIndex = gv.FocusedColumn.VisibleIndex;
            string sType1 = string.Empty;

            if (columnIndex >= 1 && columnIndex <= 4)
            {
                if (columnIndex == 1)
                    sType1 = "AD";
                else if (columnIndex == 2)
                    sType1 = "TD";
                else if (columnIndex == 3)
                    sType1 = "DM";
                else if (columnIndex == 4)
                    sType1 = "GD";

                DataRow dr = this.efwGridControl1.GetSelectedRow(0);

                popup = new frmDN03_Pop01();
                //popup.Owner = this;
                popup.pDATE = dr["REG_DATE"].ToString();
                popup.pMONEY_TYPE = sType1;
                popup.FormClosed += popup_FormClosed;
                popup.ShowDialog();
            }
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //if (popup.DialogResult == DialogResult.OK)
            //{
            //    this.txtX.EditValue = popup.nX;
            //    this.txtY.EditValue = popup.nY;
            //}

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }

        private void GridView2_DoubleClick(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            var rowIndex = gv.FocusedRowHandle;
            var columnIndex = gv.FocusedColumn.VisibleIndex;
            string sType1 = string.Empty;

            if (columnIndex >= 1 && columnIndex <= 4)
            {
                if (columnIndex == 1)
                    sType1 = "AD";
                else if (columnIndex == 2)
                    sType1 = "TD";
                else if (columnIndex == 3)
                    sType1 = "DM";
                else if (columnIndex == 4)
                    sType1 = "GD";

                DataRow dr = this.efwGridControl2.GetSelectedRow(0);

                popup2 = new frmDN03_Pop02();
                //popup.Owner = this;
                popup2.pDATE = dr["REG_DATE"].ToString();
                popup2.pMONEY_TYPE = sType1;
                popup2.FormClosed += popup2_FormClosed;
                popup2.ShowDialog();
            }
        }

        private void popup2_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup2.FormClosed -= popup2_FormClosed;

            //if (popup2.DialogResult == DialogResult.OK)
            //{
            //    this.txtX.EditValue = popup2.nX;
            //    this.txtY.EditValue = popup2.nY;
            //}

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup2 = null;
        }




    }
}
