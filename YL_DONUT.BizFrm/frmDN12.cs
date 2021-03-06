﻿using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using Easy.Framework.Util;
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
using YL_COMM.BizFrm;

namespace YL_DONUT.BizFrm
{
    public partial class frmDN12 : FrmBase
    {
        #region Fields

        #endregion

        public frmDN12()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = " frmDN12";
            //폼명설정
            this.FrmName = "D머니 적립";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = false;
            this.IsSearch = false;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //dtS_DATE.EditValue = DateTime.Now.ToString("yyyy-MM");
            //cmbORDER_SEARCH.EditValue = "1";

            dtS_DATE.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            dtS_DATE2.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE2.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE2.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE2.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            dtS_DATE3.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE3.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE3.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE3.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            dtS_DATE4.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE4.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE4.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE4.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            dtS_DATE6.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE6.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE6.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE6.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE6.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            dtS_DATE.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtS_DATE2.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtS_DATE3.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtS_DATE4.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtS_DATE6.EditValue = DateTime.Now.ToString("yyyy-MM");
            autoOpen();
        }

        #endregion


        #region PS운영자_추천지원금_정산내역_D머니적립

        private void BtnGetExcel_Click(object sender, EventArgs e)
        {
            //엑셀데이타 가져오기
            //openFileDialog1.DefaultExt = "xls";
            //openFileDialog1.FileName = "PS운영자_추천지원금_정산내역_D머니적립.xls";
            //openFileDialog1.Filter = "Excel97 - 2003 통합문서|*.xls|Excel 통합문서|*.xlsx|모든파일|*.*";

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        Cursor = Cursors.WaitCursor;
            //        string fileName = openFileDialog1.FileName;

            //        efwGridControl1.DataSource = ExcelDataBaseHelper.OpenFile(fileName);
            //        this.efwGridControl1.MyGridView.BestFitColumns();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageAgent.MessageShow(MessageType.Error, String.Format("엑셀 파일에 문제가 있습니다. 엑셀 파일을 확인해 주십시오.{0}", ex));
            //    }
            //    finally
            //    {
            //        Cursor = Cursors.Default;
            //    }
            //}

            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "xls";
            openFileDialog1.FileName = "PS운영자_추천지원금_정산내역_D머니적립.xls";
            openFileDialog1.Filter = "Excel97 - 2003 통합문서|*.xls";
            openFileDialog1.Title = "엑셀데이터 가져오기";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (openFileDialog1.FileName != string.Empty)
                    {
                        string fileName = openFileDialog1.FileName;

                        if (fileName.IndexOf("PS운영자_추천지원금_정산내역_D머니적립.xls") < 0)
                        {
                            MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                            return;
                        }

                        this.efwGridControl1.DataSource = ExcelDataBaseHelper.OpenFile2(fileName);

                        //this.efwGridControl1.DataSource = ExcelDataBaseHelper.OpenFile(fileName);

                        this.efwGridControl1.MyGridView.BestFitColumns();
                        lblCnt1.Text = string.Format("{0:#,###}", gridView1.DataRowCount.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("엑셀 파일 드라이버가 잘못되었거나 엑셀파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void BtnExcelSample_Click(object sender, EventArgs e)
        {
            //엑셀양식 가져오기
            byte[] _SampleFile = YL_DONUT.BizFrm.Properties.Resources.PS운영자_추천지원금_정산내역_D머니적립_sample;
            string _SaveFileName = "PS운영자_추천지원금_정산내역_D머니적립.xls";

            try
            {
                Cursor = Cursors.WaitCursor;
                //리소스에 있는 엑셀파일 다운로드 
                string saveFileFullPath = Path.GetTempPath() + _SaveFileName;
                FileInfo fInfo = new FileInfo(saveFileFullPath);
                if (fInfo.Exists)
                {
                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
                else
                {
                    FileStream stream = new FileStream(saveFileFullPath, FileMode.OpenOrCreate);
                    stream.Write(_SampleFile, 0, _SampleFile.Length);
                    stream.Close();

                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
            }
            catch (Exception ex)
            {
                if (ex is System.IO.IOException)
                    MessageAgent.MessageShow(MessageType.Warning, "샘플파일이 이미 열려 있습니다.");
                else
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //PS운영자 추천지원금 정산내역 D머니적립
            if (gridView1.DataRowCount <= 0)
            {
                MessageAgent.MessageShow(MessageType.Error, "처리할 내역이 없습니다.");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "적립처리를 하시겠습니까?");

                if (drt == DialogResult.OK)
                {
                    try
                    {
                        string strIN_DAY  = DateTime.Now.ToString("yyyyMMdd");
                        string smsg       = string.Empty;
                        string sfrom_id   = string.Empty;
                        string sto_id     = string.Empty;
                        string sname      = string.Empty;
                        string snickname  = string.Empty;
                        int namt          = 0;
                        string smemo      = string.Empty;
                        string sdate      = string.Empty;
                        string sfrom_type = string.Empty;
                        string sto_type   = string.Empty;
                        string swork_type = string.Empty;
                        string subject    = string.Empty;

                        TransactionPack tPack  = new TransactionPack();
                        TransactionPack tPack2 = new TransactionPack();

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            // @fromID         (sender_id)   AdminYL, AdminTelecom, AdminYL, AdminLife, AdminDoma  ( = 차감)                                                                               
                            // @toID	       (receiver_id)                                                       ( = 적립)                             
                            // @fromType	   (from_type)                                                                                      
                            // @toType	       (to_type)                                                                                        
                            // @reqAmount	   (donut_count2)                                                                                   
                            // @feeType	       (send_code)   Fee가 있는 경우 1 ( TRUE ), 없는경우 0 ( FALSE )                                         
                            // @EventSystemID  (send_type)   EventSystemID : Telecom,DoToc,DonutToc,Shop,Show(장터),Blog,AD(광고),Movie,Charge(충전)
                            // @EventID	       (contents_id2)                                                                                   
                            // @EventComment   (contents_type2)                                                                                 
                            // @EventEtc	   (mileage_message)                                                                                
                            // @result

                            swork_type = "01";
                            subject    = "[관리] PS추천지원금 정산";
                            sfrom_type = "DM";
                            sto_type   = "DM";
                            sfrom_id   = "AdminDoma";
                            sto_id     = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();
                            sname      = gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString();
                            snickname  = gridView1.GetRowCellValue(i, gridView1.Columns[3]).ToString();
                            namt       = Convert.ToInt32(gridView1.GetRowCellValue(i, gridView1.Columns[4]).ToString());
                            smemo      = gridView1.GetRowCellValue(i, gridView1.Columns[5]).ToString();
                            sdate      = gridView1.GetRowCellValue(i, gridView1.Columns[6]).ToString();

                            smsg = smemo + "(D머니) [" + sdate.Substring(0, 10) + "]: " + string.Format("{0:#,###}", namt)  + "원";

                            //Console.WriteLine(sto_id);
                            //Console.WriteLine(sname);
                            //Console.WriteLine(snickname);
                            //Console.WriteLine(smemo);
                            //Console.WriteLine(smsg);
                            //Console.WriteLine("------------------------------------");


                            tPack.Add("YEOYOU_MONEY.dbo.MONEY_ADD_PROC"
                                    , sfrom_id
                                    , sto_id
                                    , sfrom_type
                                    , sto_type
                                    , namt
                                    , 0
                                    , "AdminCharge"
                                    , "AdminCharge"
                                    , subject
                                    , smsg
                                    , 0
                                );

                            tPack2.Add("YEOYOU_MONEY.dbo.USP_DN_DN12_SAVE_10"
                                    , UserInfo.instance().UserId
                                    , swork_type
                                    , DateTime.Now.ToString("yyyyMMdd")
                                    , DateTime.Now.ToString("yyyyMM")
                                    , sto_id
                                    , sname
                                    , snickname
                                    , namt
                                    , sfrom_type
                                    , sto_type
                                    , subject
                                    , smemo
                                    , sdate
                                ); ;
                        }
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack);
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack2);

                        BtnHistoryQ_Click(null, null);

                        MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                        Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.Message);
                        Cursor = Cursors.Default;
                        return;
                    }
                    His_MySql1();
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void BtnHistoryQ_Click(object sender, EventArgs e)
        {
            DataSet dsres = openHistory("01", dtS_DATE.EditValue3.Substring(0, 6));

            efwGridControl3.DataBind(dsres);
            this.efwGridControl3.MyGridView.BestFitColumns();
        }

        #endregion


        #region PS운영자_수수료_정산내역_D머니적립

        private void BtnGetExcel2_Click(object sender, EventArgs e)
        {
            //엑셀데이타 가져오기
            openFileDialog1.DefaultExt = "xls";
            openFileDialog1.FileName = "PS운영자_PS수수료_정산내역_D머니적립.xls";
            openFileDialog1.Filter = "Excel97 - 2003 통합문서|*.xls";
            openFileDialog1.Title = "엑셀데이터 가져오기";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (openFileDialog1.FileName != string.Empty)
                    {
                        string fileName = openFileDialog1.FileName;

                        if (fileName.IndexOf("PS운영자_PS수수료_정산내역_D머니적립.xls") < 0)
                        {
                            MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                            return;
                        }

                        this.efwGridControl2.DataSource = ExcelDataBaseHelper.OpenFile2(fileName);
                        //this.efwGridControl2.DataSource = ExcelDataBaseHelper.OpenFile(fileName);

                        this.efwGridControl2.MyGridView.BestFitColumns();
                        lblCnt2.Text = string.Format("{0:#,###}", gridView2.DataRowCount.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("엑셀 파일 드라이버가 잘못되었거나 엑셀파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void BtnExcelSample2_Click(object sender, EventArgs e)
        {
            //엑셀양식 가져오기
            byte[] _SampleFile = YL_DONUT.BizFrm.Properties.Resources.PS운영자_PS수수료_정산내역_D머니적립_sample;
            string _SaveFileName = "PS운영자_PS수수료_정산내역_D머니적립.xls";

            try
            {
                Cursor = Cursors.WaitCursor;
                //리소스에 있는 엑셀파일 다운로드 
                string saveFileFullPath = Path.GetTempPath() + _SaveFileName;
                FileInfo fInfo = new FileInfo(saveFileFullPath);
                if (fInfo.Exists)
                {
                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
                else
                {
                    FileStream stream = new FileStream(saveFileFullPath, FileMode.OpenOrCreate);
                    stream.Write(_SampleFile, 0, _SampleFile.Length);
                    stream.Close();

                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
            }
            catch (Exception ex)
            {
                if (ex is System.IO.IOException)
                    MessageAgent.MessageShow(MessageType.Warning, "샘플파일이 이미 열려 있습니다.");
                else
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnSave2_Click(object sender, EventArgs e)
        {
            //PS운영자 수수료 D머니적립
            if (gridView2.DataRowCount <= 0)
            {
                MessageAgent.MessageShow(MessageType.Error, "처리할 내역이 없습니다.");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "적립처리를 하시겠습니까?");

                if (drt == DialogResult.OK)
                {
                    try
                    {
                        string strIN_DAY  = DateTime.Now.ToString("yyyyMMdd");
                        string smsg       = string.Empty;
                        string sfrom_id   = string.Empty;
                        string sto_id     = string.Empty;
                        string sname      = string.Empty;
                        string snickname  = string.Empty;
                        int namt          = 0;
                        string smemo      = string.Empty;
                        string sdate      = string.Empty;
                        string sfrom_type = string.Empty;
                        string sto_type   = string.Empty;
                        string swork_type = string.Empty;
                        string subject    = string.Empty;

                        TransactionPack tPack = new TransactionPack();
                        TransactionPack tPack2 = new TransactionPack();

                        for (int i = 0; i < gridView2.DataRowCount; i++)
                        {
                            // @fromID         (sender_id)   AdminYL, AdminTelecom, AdminYL, AdminLife, AdminDoma  ( = 차감)                                                                               
                            // @toID	       (receiver_id)                                                       ( = 적립)                             
                            // @fromType	   (from_type)                                                                                      
                            // @toType	       (to_type)                                                                                        
                            // @reqAmount	   (donut_count2)                                                                                   
                            // @feeType	       (send_code)   Fee가 있는 경우 1 ( TRUE ), 없는경우 0 ( FALSE )                                         
                            // @EventSystemID  (send_type)   EventSystemID : Telecom,DoToc,DonutToc,Shop,Show(장터),Blog,AD(광고),Movie,Charge(충전)
                            // @EventID	       (contents_id2)                                                                                   
                            // @EventComment   (contents_type2)                                                                                 
                            // @EventEtc	   (mileage_message)                                                                                
                            // @result

                            swork_type = "02";
                            subject    = "[관리] PS운영자 수수료 정산";
                            sfrom_type = "DM";
                            sto_type   = "DM";
                            sfrom_id   = "AdminDoma";
                            sto_id     = gridView2.GetRowCellValue(i, gridView2.Columns[1]).ToString();
                            sname      = gridView2.GetRowCellValue(i, gridView2.Columns[2]).ToString();
                            snickname  = gridView2.GetRowCellValue(i, gridView2.Columns[3]).ToString();
                            namt       = Convert.ToInt32(gridView2.GetRowCellValue(i, gridView2.Columns[4]).ToString());
                            smemo      = gridView2.GetRowCellValue(i, gridView2.Columns[5]).ToString();
                            sdate      = gridView2.GetRowCellValue(i, gridView2.Columns[6]).ToString();

                            smsg = smemo + "(D머니) [" + sdate.Substring(0, 10) + "]: " + string.Format("{0:#,###}", namt) + "원";

                            //Console.WriteLine(sto_id);
                            //Console.WriteLine(sname);
                            //Console.WriteLine(snickname);
                            //Console.WriteLine(smemo);
                            //Console.WriteLine(smsg);
                            //Console.WriteLine("------------------------------------");


                            tPack.Add("YEOYOU_MONEY.dbo.MONEY_ADD_PROC"
                                    , sfrom_id
                                    , sto_id
                                    , sfrom_type
                                    , sto_type
                                    , namt
                                    , 0
                                    , "AdminCharge"
                                    , "AdminCharge"
                                    , subject
                                    , smsg
                                    , 0
                                );

                            tPack2.Add("YEOYOU_MONEY.dbo.USP_DN_DN12_SAVE_10"
                                    , UserInfo.instance().UserId
                                    , swork_type
                                    , DateTime.Now.ToString("yyyyMMdd")
                                    , DateTime.Now.ToString("yyyyMM")
                                    , sto_id
                                    , sname
                                    , snickname
                                    , namt
                                    , sfrom_type
                                    , sto_type
                                    , subject
                                    , smemo
                                    , sdate
                                ); ;
                        }
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack);
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack2);

                        BtnHistoryQ2_Click(null, null);

                        MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                        Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.Message);
                        Cursor = Cursors.Default;
                        return;
                    }
                    His_MySql2();
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void BtnHistoryQ2_Click(object sender, EventArgs e)
        {
            DataSet dsres = openHistory("02", dtS_DATE2.EditValue3.Substring(0, 6));

            efwGridControl4.DataBind(dsres);
            this.efwGridControl4.MyGridView.BestFitColumns();
        }

        #endregion



        //------------------------------------------------------------------------------------------------------------------

        #region 여유텔레콤 도넛차액 환불(적립)

        private void BtnGetExcel3_Click(object sender, EventArgs e)
        {
            //엑셀데이타 가져오기
            openFileDialog1.DefaultExt = "xls";
            openFileDialog1.FileName = "도넛_차액반환_TD머니적립.xls";
            openFileDialog1.Filter = "Excel97 - 2003 통합문서|*.xls";
            openFileDialog1.Title = "엑셀데이터 가져오기";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (openFileDialog1.FileName != string.Empty)
                    {
                        string fileName = openFileDialog1.FileName;

                        if (fileName.IndexOf("도넛_차액반환_TD머니적립.xls") < 0)
                        {
                            MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                            return;
                        }

                        this.efwGridControl5.DataSource = ExcelDataBaseHelper.OpenFile2(fileName);
                        //this.efwGridControl5.DataSource = ExcelDataBaseHelper.OpenFile(fileName);

                        this.efwGridControl5.MyGridView.BestFitColumns();
                        lblCnt3.Text = string.Format("{0:#,###}", gridView5.DataRowCount.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("엑셀 파일 드라이버가 잘못되었거나 엑셀파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void BtnSave3_Click(object sender, EventArgs e)
        {
            //여유텔레콤 도넛차액 환불(적립)
            if (gridView5.DataRowCount <= 0)
            {
                MessageAgent.MessageShow(MessageType.Error, "처리할 내역이 없습니다.");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "적립처리를 하시겠습니까?");

                if (drt == DialogResult.OK)
                {
                    try
                    {
                        string strIN_DAY  = DateTime.Now.ToString("yyyyMMdd");
                        string smsg       = string.Empty;
                        string sfrom_id   = string.Empty;
                        string sto_id     = string.Empty;
                        string sname      = string.Empty;
                        string snickname  = string.Empty;
                        int namt          = 0;
                        string smemo      = string.Empty;
                        string sdate      = string.Empty;
                        string sfrom_type = string.Empty;
                        string sto_type   = string.Empty;
                        string swork_type = string.Empty;
                        string subject    = string.Empty;

                        TransactionPack tPack = new TransactionPack();
                        TransactionPack tPack2 = new TransactionPack();

                        for (int i = 0; i < gridView5.DataRowCount; i++)
                        {
                            // @fromID         (sender_id)   AdminYL, AdminTelecom, AdminYL, AdminLife, AdminDoma  ( = 차감)                                                                               
                            // @toID	       (receiver_id)                                                       ( = 적립)                             
                            // @fromType	   (from_type)                                                                                      
                            // @toType	       (to_type)                                                                                        
                            // @reqAmount	   (donut_count2)                                                                                   
                            // @feeType	       (send_code)   Fee가 있는 경우 1 ( TRUE ), 없는경우 0 ( FALSE )                                         
                            // @EventSystemID  (send_type)   EventSystemID : Telecom,DoToc,DonutToc,Shop,Show(장터),Blog,AD(광고),Movie,Charge(충전)
                            // @EventID	       (contents_id2)                                                                                   
                            // @EventComment   (contents_type2)                                                                                 
                            // @EventEtc	   (mileage_message)                                                                                
                            // @result

                            swork_type = "03";
                            subject    = "[관리] 도넛 차액 환불";
                            sfrom_type = "TD";
                            sto_type   = "TD";
                            sfrom_id   = "AdminDoma";
                            sto_id     = gridView5.GetRowCellValue(i, gridView5.Columns[1]).ToString();
                            sname      = gridView5.GetRowCellValue(i, gridView5.Columns[2]).ToString();
                            snickname  = gridView5.GetRowCellValue(i, gridView5.Columns[3]).ToString();
                            namt       = Convert.ToInt32(gridView5.GetRowCellValue(i, gridView5.Columns[4]).ToString());
                            smemo      = gridView5.GetRowCellValue(i, gridView5.Columns[5]).ToString();
                            sdate      = gridView5.GetRowCellValue(i, gridView5.Columns[6]).ToString();

                            smsg = smemo + "(TD머니) : " + string.Format("{0:#,###}", namt) + "원";

                            //Console.WriteLine(sto_id);
                            //Console.WriteLine(sname);
                            //Console.WriteLine(snickname);
                            //Console.WriteLine(smemo);
                            //Console.WriteLine(smsg);
                            //Console.WriteLine("------------------------------------");


                            tPack.Add("YEOYOU_MONEY.dbo.MONEY_ADD_PROC"
                                    , sfrom_id
                                    , sto_id
                                    , sfrom_type
                                    , sto_type
                                    , namt
                                    , 0
                                    , "AdminCharge"
                                    , "AdminCharge"
                                    , subject
                                    , smsg
                                    , 0
                                );

                            tPack2.Add("YEOYOU_MONEY.dbo.USP_DN_DN12_SAVE_10"
                                    , UserInfo.instance().UserId
                                    , swork_type
                                    , DateTime.Now.ToString("yyyyMMdd")
                                    , DateTime.Now.ToString("yyyyMM")
                                    , sto_id
                                    , sname
                                    , snickname
                                    , namt
                                    , sfrom_type
                                    , sto_type
                                    , subject
                                    , smemo
                                    , sdate
                                );
                        }
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack);
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack2);

                        BtnHistoryQ3_Click(null, null);

                        MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                        Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.Message);
                        Cursor = Cursors.Default;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void BtnExcelSample3_Click(object sender, EventArgs e)
        {
            //엑셀양식 가져오기
            byte[] _SampleFile = YL_DONUT.BizFrm.Properties.Resources.도넛_차액반환_TD머니적립_sample;
            string _SaveFileName = "도넛_차액반환_TD머니적립.xls";

            try
            {
                Cursor = Cursors.WaitCursor;
                //리소스에 있는 엑셀파일 다운로드 
                string saveFileFullPath = Path.GetTempPath() + _SaveFileName;
                FileInfo fInfo = new FileInfo(saveFileFullPath);
                if (fInfo.Exists)
                {
                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
                else
                {
                    FileStream stream = new FileStream(saveFileFullPath, FileMode.OpenOrCreate);
                    stream.Write(_SampleFile, 0, _SampleFile.Length);
                    stream.Close();

                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
            }
            catch (Exception ex)
            {
                if (ex is System.IO.IOException)
                    MessageAgent.MessageShow(MessageType.Warning, "샘플파일이 이미 열려 있습니다.");
                else
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnHistoryQ3_Click(object sender, EventArgs e)
        {
            DataSet dsres = openHistory("03", dtS_DATE3.EditValue3.Substring(0, 6));

            efwGridControl6.DataBind(dsres);
            this.efwGridControl6.MyGridView.BestFitColumns();
        }

        #endregion


        #region 이벤트

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.FileName = "";
            //openFileDialog1.Filter = "xls파일(*.xls)|*.xls|xlsx파일(*.xlsx)|*.xlsx";
            //openFileDialog1.Title = "엑셀 저장";
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        if (openFileDialog1.FileName != string.Empty)
            //        {
            //            string fileName = openFileDialog1.FileName;

            //            efwGridControl1.DataSource = ExcelDataBaseHelper.OpenFile(fileName);
            //            this.efwGridControl1.MyGridView.BestFitColumns();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("엑셀 파일 드라이버가 잘못되었거나 엑셀파일이 문제가 있습니다." + "\r\n" + ex.ToString());
            //    }
            //}
        }

       
        private DataSet openHistory(string pType, string pYyMm)
        {
            DataSet ds = null;

            try
            {
                ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "YEOYOU_MONEY.dbo.USP_DN_DN12_SELECT_01"
                    , pType
                    , pYyMm
                    );
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            return ds;
        }

        private void autoOpen()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                BtnHistoryQ_Click(null, null);
            }
            else if(efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                BtnHistoryQ2_Click(null, null);
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                BtnHistoryQ3_Click(null, null);
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                BtnHistoryQ4_Click(null, null);
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage6)
            {
                btnHistoryQ6_Click(null, null);
            }
        }

        private void EfwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            autoOpen();
        }


        #endregion

        private void BtnExcelSample4_Click(object sender, EventArgs e)
        {
            //엑셀양식 가져오기
            byte[] _SampleFile = YL_DONUT.BizFrm.Properties.Resources.YSMS및개인간거래_AD머니적립_sample;
            string _SaveFileName = "YSMS및개인간거래 AD머니적립.xls";

            try
            {
                Cursor = Cursors.WaitCursor;
                //리소스에 있는 엑셀파일 다운로드 
                string saveFileFullPath = Path.GetTempPath() + _SaveFileName;
                FileInfo fInfo = new FileInfo(saveFileFullPath);
                if (fInfo.Exists)
                {
                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
                else
                {
                    FileStream stream = new FileStream(saveFileFullPath, FileMode.OpenOrCreate);
                    stream.Write(_SampleFile, 0, _SampleFile.Length);
                    stream.Close();

                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
            }
            catch (Exception ex)
            {
                if (ex is System.IO.IOException)
                    MessageAgent.MessageShow(MessageType.Warning, "샘플파일이 이미 열려 있습니다.");
                else
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnGetExcel4_Click(object sender, EventArgs e)
        {
            //엑셀데이타 가져오기
            openFileDialog1.DefaultExt = "xls";
            openFileDialog1.FileName = "YSMS및개인간거래 AD머니적립.xls";
            openFileDialog1.Filter = "Excel97 - 2003 통합문서|*.xls";
            openFileDialog1.Title = "엑셀데이터 가져오기";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (openFileDialog1.FileName != string.Empty)
                    {
                        string fileName = openFileDialog1.FileName;

                        if (fileName.IndexOf("YSMS및개인간거래 AD머니적립.xls") < 0)
                        {
                            MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                            return;
                        }

                        this.efwGridControl8.DataSource = ExcelDataBaseHelper.OpenFile2(fileName);
                        //this.efwGridControl8.DataSource = ExcelDataBaseHelper.OpenFile(fileName);

                        this.efwGridControl8.MyGridView.BestFitColumns();
                        lblCnt4.Text = string.Format("{0:#,###}", gridView8.DataRowCount.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("엑셀 파일 드라이버가 잘못되었거나 엑셀파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void BtnSave4_Click(object sender, EventArgs e)
        {
            //YSMS및개인간거래 AD머니적립
            if (gridView8.DataRowCount <= 0)
            {
                MessageAgent.MessageShow(MessageType.Error, "처리할 내역이 없습니다.");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "적립처리를 하시겠습니까?");

                if (drt == DialogResult.OK)
                {
                    try
                    {
                        string strIN_DAY = DateTime.Now.ToString("yyyyMMdd");
                        string smsg = string.Empty;
                        string sfrom_id = string.Empty;
                        string sto_id = string.Empty;
                        string sname = string.Empty;
                        string snickname = string.Empty;
                        int namt = 0;
                        string smemo = string.Empty;
                        string sdate = string.Empty;
                        string sfrom_type = string.Empty;
                        string sto_type = string.Empty;
                        string swork_type = string.Empty;
                        string subject = string.Empty;

                        TransactionPack tPack = new TransactionPack();
                        TransactionPack tPack2 = new TransactionPack();

                        for (int i = 0; i < gridView8.DataRowCount; i++)
                        {
                            // @fromID         (sender_id)   AdminYL, AdminTelecom, AdminYL, AdminLife, AdminDoma  ( = 차감)                                                                               
                            // @toID	       (receiver_id)                                                       ( = 적립)                             
                            // @fromType	   (from_type)                                                                                      
                            // @toType	       (to_type)                                                                                        
                            // @reqAmount	   (donut_count2)                                                                                   
                            // @feeType	       (send_code)   Fee가 있는 경우 1 ( TRUE ), 없는경우 0 ( FALSE )                                         
                            // @EventSystemID  (send_type)   EventSystemID : Telecom,DoToc,DonutToc,Shop,Show(장터),Blog,AD(광고),Movie,Charge(충전)
                            // @EventID	       (contents_id2)                                                                                   
                            // @EventComment   (contents_type2)                                                                                 
                            // @EventEtc	   (mileage_message)                                                                                
                            // @result

                            swork_type = "04";
                            subject = "[관리] YSMS거래 AD머니 적립";
                            sfrom_type = "AD";
                            sto_type = "AD";
                            sfrom_id = "AdminDoma";
                            sto_id = gridView8.GetRowCellValue(i, gridView8.Columns[1]).ToString();
                            sname = gridView8.GetRowCellValue(i, gridView8.Columns[2]).ToString();
                            snickname = gridView8.GetRowCellValue(i, gridView8.Columns[3]).ToString();
                            namt = Convert.ToInt32(gridView8.GetRowCellValue(i, gridView8.Columns[4]).ToString());
                            smemo = gridView8.GetRowCellValue(i, gridView8.Columns[5]).ToString();
                            sdate = gridView8.GetRowCellValue(i, gridView8.Columns[6]).ToString();

                            smsg = smemo + "(AD머니) : " + string.Format("{0:#,###}", namt) + "원";

                            Console.WriteLine(sto_id);
                            Console.WriteLine(sname);
                            Console.WriteLine(snickname);
                            Console.WriteLine(smemo);
                            Console.WriteLine(smsg);
                            Console.WriteLine("------------------------------------");


                            tPack.Add("YEOYOU_MONEY.dbo.MONEY_ADD_PROC"
                                    , sfrom_id
                                    , sto_id
                                    , sfrom_type
                                    , sto_type
                                    , namt
                                    , 0
                                    , "AdminCharge"
                                    , "AdminCharge"
                                    , subject
                                    , smsg
                                    , 0
                                );

                            tPack2.Add("YEOYOU_MONEY.dbo.USP_DN_DN12_SAVE_10"
                                    , UserInfo.instance().UserId
                                    , swork_type
                                    , DateTime.Now.ToString("yyyyMMdd")
                                    , DateTime.Now.ToString("yyyyMM")
                                    , sto_id
                                    , sname
                                    , snickname
                                    , namt
                                    , sfrom_type
                                    , sto_type
                                    , subject
                                    , smemo
                                    , sdate
                                );
                        }
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack);
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack2);

                        BtnHistoryQ4_Click(null, null);

                        MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                        Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.Message);
                        Cursor = Cursors.Default;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void BtnHistoryQ4_Click(object sender, EventArgs e)
        {
            DataSet dsres = openHistory("04", dtS_DATE4.EditValue3.Substring(0, 6));

            efwGridControl7.DataBind(dsres);
            this.efwGridControl7.MyGridView.BestFitColumns();
        }

        private void DtS_DATE_EditValueChanged(object sender, EventArgs e)
        {
            BtnHistoryQ_Click(null, null);
        }

        private void DtS_DATE2_EditValueChanged(object sender, EventArgs e)
        {
            BtnHistoryQ2_Click(null, null);
        }

        private void DtS_DATE3_EditValueChanged(object sender, EventArgs e)
        {
            BtnHistoryQ3_Click(null, null);
        }

        private void DtS_DATE4_EditValueChanged(object sender, EventArgs e)
        {
            BtnHistoryQ4_Click(null, null);
        }

        private void His_MySql1()
        {
            try
            {
                // 버튼 통합시 변수는 삭제
                string sfrom_type = string.Empty;
                string sto_type = string.Empty;
                string swork_type = string.Empty;
                string subject = string.Empty;

                swork_type = "01";
                subject = "[관리] PS추천지원금 정산";
                sfrom_type = "DM";
                sto_type = "DM";

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN12_SAVE_01", con))
                        {

                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_user_id", MySqlDbType.VarChar, 60);
                            cmd.Parameters[0].Value = UserInfo.instance().UserId;

                            cmd.Parameters.Add("i_work_type", MySqlDbType.VarChar, 4);
                            cmd.Parameters[1].Value = swork_type;

                            cmd.Parameters.Add("i_work_date", MySqlDbType.VarChar, 8);
                            cmd.Parameters[2].Value = DateTime.Now.ToString("yyyyMMdd");

                            cmd.Parameters.Add("i_work_yymm", MySqlDbType.VarChar, 6);
                            cmd.Parameters[3].Value = DateTime.Now.ToString("yyyyMM");

                            cmd.Parameters.Add("i_to_id", MySqlDbType.VarChar, 128);
                            cmd.Parameters[4].Value = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();

                            cmd.Parameters.Add("i_name", MySqlDbType.VarChar, 50);
                            cmd.Parameters[5].Value = gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString();

                            cmd.Parameters.Add("i_nickname", MySqlDbType.VarChar, 50);
                            cmd.Parameters[6].Value = gridView1.GetRowCellValue(i, gridView1.Columns[3]).ToString();

                            cmd.Parameters.Add("i_amt", MySqlDbType.Int32, 11);
                            cmd.Parameters[7].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, gridView1.Columns[4]).ToString());

                            cmd.Parameters.Add("i_from_type", MySqlDbType.VarChar, 20);
                            cmd.Parameters[8].Value = sfrom_type;

                            cmd.Parameters.Add("i_to_type", MySqlDbType.VarChar, 20);
                            cmd.Parameters[9].Value = sto_type;

                            cmd.Parameters.Add("i_ubject", MySqlDbType.VarChar, 500);
                            cmd.Parameters[10].Value = subject;

                            cmd.Parameters.Add("i_memo", MySqlDbType.VarChar, 500);
                            cmd.Parameters[11].Value = gridView1.GetRowCellValue(i, gridView1.Columns[5]).ToString();

                            cmd.Parameters.Add("i_date", MySqlDbType.VarChar, 500);
                            cmd.Parameters[12].Value = gridView1.GetRowCellValue(i, gridView1.Columns[6]).ToString();

                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        private void His_MySql2()
        {
            try
            {
                // 버튼 통합시 변수는 삭제
                string sfrom_type = string.Empty;
                string sto_type = string.Empty;
                string swork_type = string.Empty;
                string subject = string.Empty;

                swork_type = "02";
                subject = "[관리] PS운영자 수수료 정산";
                sfrom_type = "DM";
                sto_type = "DM";

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView2.DataRowCount; i++)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN12_SAVE_01", con))
                        {

                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_user_id", MySqlDbType.VarChar, 60);
                            cmd.Parameters[0].Value = UserInfo.instance().UserId;

                            cmd.Parameters.Add("i_work_type", MySqlDbType.VarChar, 4);
                            cmd.Parameters[1].Value = swork_type;

                            cmd.Parameters.Add("i_work_date", MySqlDbType.VarChar, 8);
                            cmd.Parameters[2].Value = DateTime.Now.ToString("yyyyMMdd");

                            cmd.Parameters.Add("i_work_yymm", MySqlDbType.VarChar, 6);
                            cmd.Parameters[3].Value = DateTime.Now.ToString("yyyyMM");

                            cmd.Parameters.Add("i_to_id", MySqlDbType.VarChar, 128);
                            cmd.Parameters[4].Value = gridView2.GetRowCellValue(i, gridView2.Columns[1]).ToString();

                            cmd.Parameters.Add("i_name", MySqlDbType.VarChar, 50);
                            cmd.Parameters[5].Value = gridView2.GetRowCellValue(i, gridView2.Columns[2]).ToString();

                            cmd.Parameters.Add("i_nickname", MySqlDbType.VarChar, 50);
                            cmd.Parameters[6].Value = gridView2.GetRowCellValue(i, gridView2.Columns[3]).ToString();

                            cmd.Parameters.Add("i_amt", MySqlDbType.Int32, 11);
                            cmd.Parameters[7].Value = Convert.ToInt32(gridView2.GetRowCellValue(i, gridView2.Columns[4]).ToString());

                            cmd.Parameters.Add("i_from_type", MySqlDbType.VarChar, 20);
                            cmd.Parameters[8].Value = sfrom_type;

                            cmd.Parameters.Add("i_to_type", MySqlDbType.VarChar, 20);
                            cmd.Parameters[9].Value = sto_type;

                            cmd.Parameters.Add("i_ubject", MySqlDbType.VarChar, 500);
                            cmd.Parameters[10].Value = subject;

                            cmd.Parameters.Add("i_memo", MySqlDbType.VarChar, 500);
                            cmd.Parameters[11].Value = gridView2.GetRowCellValue(i, gridView2.Columns[5]).ToString();

                            cmd.Parameters.Add("i_date", MySqlDbType.VarChar, 500);
                            cmd.Parameters[12].Value = gridView2.GetRowCellValue(i, gridView2.Columns[6]).ToString();

                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void btnGetExcel5_Click(object sender, EventArgs e)
        {
            //엑셀데이타 가져오기
            openFileDialog1.DefaultExt = "xls";
            openFileDialog1.FileName = "텔레콤_도넛_TD머니차감.xls";
            openFileDialog1.Filter = "Excel97 - 2003 통합문서|*.xls";
            openFileDialog1.Title = "엑셀데이터 가져오기";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (openFileDialog1.FileName != string.Empty)
                    {
                        string fileName = openFileDialog1.FileName;

                        if (fileName.IndexOf("텔레콤_도넛_TD머니차감.xls") < 0)
                        {
                            MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                            return;
                        }

                        this.efwGridControl10.DataSource = ExcelDataBaseHelper.OpenFile2(fileName);
                        //this.efwGridControl10.DataSource = ExcelDataBaseHelper.OpenFile(fileName);

                        this.efwGridControl10.MyGridView.BestFitColumns();
                        lblCnt5.Text = string.Format("{0:#,###}", gridView10.DataRowCount.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("엑셀 파일 드라이버가 잘못되었거나 엑셀파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void btnSave5_Click(object sender, EventArgs e)
        {
            //텔레콤_도넛_TD머니차감
            if (gridView10.DataRowCount <= 0)
            {
                MessageAgent.MessageShow(MessageType.Error, "처리할 내역이 없습니다.");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "적립처리를 하시겠습니까?");

                if (drt == DialogResult.OK)
                {
                    try
                    {
                        string strIN_DAY = DateTime.Now.ToString("yyyyMMdd");
                        string smsg = string.Empty;
                        string sfrom_id = string.Empty;
                        string sto_id = string.Empty;
                        string sname = string.Empty;
                        string snickname = string.Empty;
                        int namt = 0;
                        string smemo = string.Empty;
                        string sdate = string.Empty;
                        string sfrom_type = string.Empty;
                        string sto_type = string.Empty;
                        string swork_type = string.Empty;
                        string subject = string.Empty;

                        TransactionPack tPack = new TransactionPack();
                        TransactionPack tPack2 = new TransactionPack();

                        for (int i = 0; i < gridView10.DataRowCount; i++)
                        {
                            // @fromID         (sender_id)   AdminYL, AdminTelecom, AdminYL, AdminLife, AdminDoma  ( = 차감)                                                                               
                            // @toID	       (receiver_id)                                                       ( = 적립)                             
                            // @fromType	   (from_type)                                                                                      
                            // @toType	       (to_type)                                                                                        
                            // @reqAmount	   (donut_count2)                                                                                   
                            // @feeType	       (send_code)   Fee가 있는 경우 1 ( TRUE ), 없는경우 0 ( FALSE )                                         
                            // @EventSystemID  (send_type)   EventSystemID : Telecom,DoToc,DonutToc,Shop,Show(장터),Blog,AD(광고),Movie,Charge(충전)
                            // @EventID	       (contents_id2)                                                                                   
                            // @EventComment   (contents_type2)                                                                                 
                            // @EventEtc	   (mileage_message)                                                                                
                            // @result

                            swork_type = "04";
                            subject = "[관리] 텔레콤 TD머니 임시차감";
                            sfrom_type = "TD";
                            sto_type = "TD";
                            sfrom_id = gridView10.GetRowCellValue(i, gridView10.Columns[1]).ToString();
                            sto_id = "AdminTelecom";
                            sname = gridView10.GetRowCellValue(i, gridView10.Columns[2]).ToString();
                            snickname = gridView10.GetRowCellValue(i, gridView10.Columns[3]).ToString();
                            namt = Convert.ToInt32(gridView10.GetRowCellValue(i, gridView10.Columns[4]).ToString());
                            smemo = gridView10.GetRowCellValue(i, gridView10.Columns[5]).ToString();
                            sdate = gridView10.GetRowCellValue(i, gridView10.Columns[6]).ToString();

                            //smsg = smemo + "(AD머니) : " + string.Format("{0:#,###}", namt) + "원";
                            smsg = smemo;

                            Console.WriteLine(sto_id);
                            Console.WriteLine(sname);
                            Console.WriteLine(snickname);
                            Console.WriteLine(smemo);
                            Console.WriteLine(smsg);
                            Console.WriteLine("------------------------------------");


                            tPack.Add("YEOYOU_MONEY.dbo.MONEY_ADD_PROC"
                                    , sfrom_id
                                    , sto_id
                                    , sfrom_type
                                    , sto_type
                                    , namt
                                    , 0
                                    , "AdminCharge"
                                    , "AdminCharge"
                                    , subject
                                    , smsg
                                    , 0
                                );

                            tPack2.Add("YEOYOU_MONEY.dbo.USP_DN_DN12_SAVE_10"
                                    , UserInfo.instance().UserId
                                    , swork_type
                                    , DateTime.Now.ToString("yyyyMMdd")
                                    , DateTime.Now.ToString("yyyyMM")
                                    , sto_id
                                    , sname
                                    , snickname
                                    , namt
                                    , sfrom_type
                                    , sto_type
                                    , subject
                                    , smemo
                                    , sdate
                                );
                        }
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack);
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack2);

                        BtnHistoryQ4_Click(null, null);

                        MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                        Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.Message);
                        Cursor = Cursors.Default;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void btnGetExcel6_Click(object sender, EventArgs e)
        {
            //PR체험스토리_D머니적립 --> 엑셀데이타 가져오기
            openFileDialog1.DefaultExt = "xls";
            openFileDialog1.FileName = "PR체험스토리_D머니적립.xls";
            openFileDialog1.Filter = "Excel97 - 2003 통합문서|*.xls";
            openFileDialog1.Title = "엑셀데이터 가져오기";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (openFileDialog1.FileName != string.Empty)
                    {
                        string fileName = openFileDialog1.FileName;

                        if (fileName.IndexOf("PR체험스토리_D머니적립.xls") < 0)
                        {
                            MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                            return;
                        }

                        this.efwGridControl11.DataSource = ExcelDataBaseHelper.OpenFile2(fileName);
                        //this.efwGridControl11.DataSource = ExcelDataBaseHelper.OpenFile(fileName);

                        this.efwGridControl11.MyGridView.BestFitColumns();
                        lblCnt6.Text = string.Format("{0:#,###}", gridView11.DataRowCount.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("엑셀 파일 드라이버가 잘못되었거나 엑셀파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void btnSave6_Click(object sender, EventArgs e)
        {
            //PR체험스토리_D머니적립
            if (gridView11.DataRowCount <= 0)
            {
                MessageAgent.MessageShow(MessageType.Error, "처리할 내역이 없습니다.");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "적립처리를 하시겠습니까?");

                if (drt == DialogResult.OK)
                {
                    try
                    {
                        string strIN_DAY = DateTime.Now.ToString("yyyyMMdd");
                        string smsg = string.Empty;
                        string sfrom_id = string.Empty;
                        string sto_id = string.Empty;
                        string sname = string.Empty;
                        string snickname = string.Empty;
                        int namt = 0;
                        string smemo = string.Empty;
                        string sdate = string.Empty;
                        string sfrom_type = string.Empty;
                        string sto_type = string.Empty;
                        string swork_type = string.Empty;
                        string subject = string.Empty;

                        TransactionPack tPack = new TransactionPack();
                        TransactionPack tPack2 = new TransactionPack();

                        for (int i = 0; i < gridView11.DataRowCount; i++)
                        {
                            // @fromID         (sender_id)   AdminYL, AdminTelecom, AdminYL, AdminLife, AdminDoma  ( = 차감)                                                                               
                            // @toID	       (receiver_id)                                                       ( = 적립)                             
                            // @fromType	   (from_type)                                                                                      
                            // @toType	       (to_type)                                                                                        
                            // @reqAmount	   (donut_count2)                                                                                   
                            // @feeType	       (send_code)   Fee가 있는 경우 1 ( TRUE ), 없는경우 0 ( FALSE )                                         
                            // @EventSystemID  (send_type)   EventSystemID : Telecom,DoToc,DonutToc,Shop,Show(장터),Blog,AD(광고),Movie,Charge(충전)
                            // @EventID	       (contents_id2)                                                                                   
                            // @EventComment   (contents_type2)                                                                                 
                            // @EventEtc	   (mileage_message)                                                                                
                            // @result

                            swork_type = "06";
                            subject = "[관리] PR체험스토리 D머니 적립";
                            sfrom_type = "DM";
                            sto_type = "DM";
                            sfrom_id = "AdminDoma";
                            sto_id = gridView11.GetRowCellValue(i, gridView11.Columns[1]).ToString();
                            sname = gridView11.GetRowCellValue(i, gridView11.Columns[2]).ToString();
                            snickname = gridView11.GetRowCellValue(i, gridView11.Columns[3]).ToString();
                            namt = Convert.ToInt32(gridView11.GetRowCellValue(i, gridView11.Columns[4]).ToString());
                            smemo = gridView11.GetRowCellValue(i, gridView11.Columns[5]).ToString();
                            sdate = gridView11.GetRowCellValue(i, gridView11.Columns[6]).ToString();

                            smsg = smemo + "(D머니) : " + string.Format("{0:#,###}", namt) + "원";

                            Console.WriteLine(sto_id);
                            Console.WriteLine(sname);
                            Console.WriteLine(snickname);
                            Console.WriteLine(smemo);
                            Console.WriteLine(smsg);
                            Console.WriteLine("------------------------------------");


                            tPack.Add("YEOYOU_MONEY.dbo.MONEY_ADD_PROC"
                                    , sfrom_id
                                    , sto_id
                                    , sfrom_type
                                    , sto_type
                                    , namt
                                    , 0
                                    , "AdminCharge"
                                    , "AdminCharge"
                                    , subject
                                    , smsg
                                    , 0
                                );

                            tPack2.Add("YEOYOU_MONEY.dbo.USP_DN_DN12_SAVE_10"
                                    , UserInfo.instance().UserId
                                    , swork_type
                                    , DateTime.Now.ToString("yyyyMMdd")
                                    , DateTime.Now.ToString("yyyyMM")
                                    , sto_id
                                    , sname
                                    , snickname
                                    , namt
                                    , sfrom_type
                                    , sto_type
                                    , subject
                                    , smemo
                                    , sdate
                                );
                        }
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack);
                        ServiceAgent.ExecuteNoneQuery("CONIS_IBS", tPack2);

                        btnHistoryQ6_Click(null, null);

                        MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                        Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.Message);
                        Cursor = Cursors.Default;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void btnHistoryQ6_Click(object sender, EventArgs e)
        {
            DataSet dsres = openHistory("06", dtS_DATE6.EditValue3.Substring(0, 6));

            efwGridControl12.DataBind(dsres);
            this.efwGridControl12.MyGridView.BestFitColumns();
        }

        private void btnExcelSample6_Click(object sender, EventArgs e)
        {
            //엑셀양식 가져오기
            byte[] _SampleFile = YL_DONUT.BizFrm.Properties.Resources.PR체험스토리_D머니적립_sample;
            string _SaveFileName = "PR체험스토리_D머니적립.xls";

            try
            {
                Cursor = Cursors.WaitCursor;
                //리소스에 있는 엑셀파일 다운로드 
                string saveFileFullPath = Path.GetTempPath() + _SaveFileName;
                FileInfo fInfo = new FileInfo(saveFileFullPath);
                if (fInfo.Exists)
                {
                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
                else
                {
                    FileStream stream = new FileStream(saveFileFullPath, FileMode.OpenOrCreate);
                    stream.Write(_SampleFile, 0, _SampleFile.Length);
                    stream.Close();

                    //엑셀 뛰우기
                    System.Diagnostics.Process.Start(saveFileFullPath);
                }
            }
            catch (Exception ex)
            {
                if (ex is System.IO.IOException)
                    MessageAgent.MessageShow(MessageType.Warning, "샘플파일이 이미 열려 있습니다.");
                else
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}