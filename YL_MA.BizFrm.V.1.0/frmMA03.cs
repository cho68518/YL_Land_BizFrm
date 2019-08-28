﻿#region "frmMA03 설명"
//===================================================================================================
//■Program Name  : frmMA03
//■Description   : 월별결산자료현황
//■Author        : 송호철
//■Date          : 2019.08.12
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.08.12][송호철] Base
//[2] [2019.08.12][송호철] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

using DevExpress.XtraTreeList;
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
using YL_MA.BizFrm.Dlg;

namespace YL_MA.BizFrm
{
    public partial class frmMA03 : FrmBase
    {
        frmMA03_Pop01 popup;

        public frmMA03()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "MA03";
            //폼명설정
            this.FrmName = "월 결산 현황";
        }

        private void frmMA03_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = false;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;
            dtDATE.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtDATE.Properties.Mask.EditMask = "yyyy-MM";
            dtDATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtDATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtDATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtDATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            dtSDATE.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtSDATE.Properties.Mask.EditMask = "yyyy-MM";
            dtSDATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtSDATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtSDATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtSDATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            dtEDATE.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtEDATE.Properties.Mask.EditMask = "yyyy-MM";
            dtEDATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtEDATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtEDATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtEDATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;


        }

        private void Open1()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                base.Search();

                DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MA03_SELECT_01"
                    , dtDATE.EditValue3.Replace("-","")
                    );

                DataRow[] dr = ds.Tables[0].Select();

                efwArea0.Text  = String.Format("{0:#,##0}", dr[0]["AMT"]);
                efwArea1.Text  = String.Format("{0:#,##0}", dr[1]["AMT"]);
                efwArea2.Text  = String.Format("{0:#,##0}", dr[2]["AMT"]);
                efwArea3.Text  = String.Format("{0:#,##0}", dr[3]["AMT"]);
                efwArea4.Text  = String.Format("{0:#,##0}", dr[4]["AMT"]);
                efwArea5.Text  = String.Format("{0:#,##0}", dr[5]["AMT"]);
                efwArea6.Text  = String.Format("{0:#,##0}", dr[6]["AMT"]);
                efwArea7.Text  = String.Format("{0:#,##0}", dr[7]["AMT"]);
                efwArea8.Text  = String.Format("{0:#,##0}", dr[8]["AMT"]);
                efwArea9.Text  = String.Format("{0:#,##0}", dr[9]["AMT"]);
                efwArea10.Text = String.Format("{0:#,##0}", dr[10]["AMT"]);
                efwArea11.Text = String.Format("{0:#,##0}", dr[11]["AMT"]);
                efwArea12.Text = String.Format("{0:#,##0}", dr[12]["AMT"]);
                efwArea13.Text = String.Format("{0:#,##0}", dr[13]["AMT"]);
                efwArea14.Text = String.Format("{0:#,##0}", dr[14]["AMT"]);
                efwArea15.Text = String.Format("{0:#,##0}", dr[15]["AMT"]);
                efwArea16.Text = String.Format("{0:#,##0}", dr[16]["AMT"]);
                efwArea17.Text = String.Format("{0:#,##0}", dr[17]["AMT"]);
                efwArea18.Text = String.Format("{0:#,##0}", dr[18]["AMT"]);
                efwArea19.Text = String.Format("{0:#,##0}", dr[19]["AMT"]);
                efwArea20.Text = String.Format("{0:#,##0}", dr[20]["AMT"]);

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void Open2()
        {
            try
            {
                string sSDate = string.Empty;
                string sEDate = string.Empty;

                sSDate = dtSDATE.EditValue3.Substring(0, 4) + "-" + dtSDATE.EditValue3.Substring(4, 2) + "-01";
                sEDate = dtEDATE.EditValue3.Substring(0, 4) + "-" + dtEDATE.EditValue3.Substring(4, 2) + "-31";


                Cursor.Current = Cursors.WaitCursor;
                base.Search();

                DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MA03_SELECT_02"
                    , sSDate
                    , sEDate
                    );

                
                DataRow[] dr = ds.Tables[0].Select();

                efwYY0.Text = String.Format("{0:#,##0}", dr[0]["TOT"]);
                efwYY1.Text = String.Format("{0:#,##0}", dr[0]["ADDTAX"]);
                efwYY2.Text = String.Format("{0:#,##0}", dr[0]["NOTTAX"]);
                

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void Open3()
        {
            try
            {
                string sSDate = string.Empty;
                string sEDate = string.Empty;

                sSDate = dtSDATE.EditValue3.Substring(0, 4) + "-" + dtSDATE.EditValue3.Substring(4, 2) + "-01";
                sEDate = dtEDATE.EditValue3.Substring(0, 4) + "-" + dtEDATE.EditValue3.Substring(4, 2) + "-31";


                Cursor.Current = Cursors.WaitCursor;
                base.Search();

                DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MA03_SELECT_03"
                    , sSDate
                    , sEDate
                    );


                DataRow[] dr = ds.Tables[0].Select();

                efwYY3.Text = String.Format("{0:#,##0}", dr[0]["TOTAL"]);
                efwYY4.Text = String.Format("{0:#,##0}", dr[0]["ADDTAX"]);
                efwYY5.Text = String.Format("{0:#,##0}", dr[0]["NOTTAX"]);


            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnCardOpen_Click(object sender, EventArgs e)
        {
            Open1();
        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            Open2();
            Open3();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            popup = new frmMA03_Pop01();

            popup.pSDate = dtSDATE.EditValue3.Substring(0, 4) + "-" + dtSDATE.EditValue3.Substring(4, 2);
            popup.pEDate = dtEDATE.EditValue3.Substring(0, 4) + "-" + dtEDATE.EditValue3.Substring(4, 2);

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }


}
}
