using DevExpress.CodeParser;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace YL_MA.BizFrm
{
    public partial class frmMA04 : FrmBase
    {
        public frmMA04()
        {
            InitializeComponent();
            this.QCode = "MA04";
            //폼명설정
            this.FrmName = "분기 결산현황";
        }

        private void frmMA04_Load(object sender, EventArgs e)
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

            dtS_DATE.EditValue = DateTime.Now;

            advBandedGridView1.OptionsView.ShowFooter = true;
            // 헤더 TATLE 추가
            //gridView1.ViewCaption = "1월"; gridView1.OptionsView.ShowViewCaption = true; Font dFont = gridView1.Appearance.ViewCaption.Font; gridView1.Appearance.ViewCaption.Font = new Font(dFont.FontFamily, dFont.Size + 20);

            advBandedGridView1.Columns["DM_Money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["DM_Money"].SummaryItem.FieldName = "DM_Money";
            advBandedGridView1.Columns["DM_Money"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["TD_Money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["TD_Money"].SummaryItem.FieldName = "TD_Money";
            advBandedGridView1.Columns["TD_Money"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["MD_Money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["MD_Money"].SummaryItem.FieldName = "MD_Money";
            advBandedGridView1.Columns["MD_Money"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["GD_Money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["GD_Money"].SummaryItem.FieldName = "GD_Money";
            advBandedGridView1.Columns["GD_Money"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView2.OptionsView.ShowFooter = true;

            advBandedGridView2.Columns["RECV_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView2.Columns["RECV_AMOUNT"].SummaryItem.FieldName = "RECV_AMOUNT";
            advBandedGridView2.Columns["RECV_AMOUNT"].SummaryItem.DisplayFormat = "{0:c}";
        }


        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();  
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open4();
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                Open5();
            }
        }

        public void Open1()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA04_SELECT_01"
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

        public void Open2()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA04_SELECT_02"
                                                            , dtS_DATE.EditValue3.ToString().Substring(0,4)
                                                            , this.txtQ.Text
                                                            );
                efwGridControl2.DataBind(ds);

                this.efwGridControl2.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Cursor.Current = Cursors.Default;
        }


        public void Open3()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA04_SELECT_03"
                                                            , dtS_DATE.EditValue3.ToString().Substring(0, 4)
                                                            , this.txtQ.Text
                                                            );
                efwGridControl3.DataBind(ds);

                this.efwGridControl3.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Cursor.Current = Cursors.Default;
        }

        public void Open4()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA04_SELECT_04"
                                                            , dtS_DATE.EditValue3.ToString().Substring(0, 4)
                                                            , this.txtQ.Text
                                                            );
                efwGridControl4.DataBind(ds);

                this.efwGridControl4.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Cursor.Current = Cursors.Default;
        }
        public void Open5()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MA_MA04_SELECT_05"
                                                            , dtS_DATE.EditValue3.ToString().Substring(0, 4)
                                                            , this.txtQ.Text
                                                            );
                efwGridControl5.DataBind(ds);

                this.efwGridControl5.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Cursor.Current = Cursors.Default;
        }

    }
}
