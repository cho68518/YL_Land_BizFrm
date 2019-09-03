#region "frmDN14 설명"
//===================================================================================================
//■Program Name  : frmDN14
//■Description   : 후기 머니관리
//■Author        : 송호철
//■Date          : 2019.07.30
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.07.30][송호철] Base
//[2] [2019.07.30][송호철] 
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
    public partial class frmDN14 : FrmBase
    {
        public frmDN14()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "DN14";
            //폼명설정
            this.FrmName = "후기 머니관리";
        }

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
            this.IsExcel = true;

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("idx", txtIDX)
                    , new ColumnControlSet("option_id", txtOPTION_ID)
                    , new ColumnControlSet("product_name", txtPROD_NM)
                    , new ColumnControlSet("customer_price", txtCUSTOMER_PRICE)
                    , new ColumnControlSet("lowest_price", txtLOWEST_PRICE)
                    , new ColumnControlSet("supply_price", txtSUPPLY_PRICE)
                    , new ColumnControlSet("delivery_price", txtDELIVERY_PRICE)
                    , new ColumnControlSet("ps_donut01", txtPS_DONUT01)
                    , new ColumnControlSet("ps_donut02", txtPS_DONUT02)
                    , new ColumnControlSet("vip_price", txtVIP_PRICE)
                    , new ColumnControlSet("ps_price", txtPS_PRICE)
                    , new ColumnControlSet("ps_oper_price", txtPS_OPER_PRICE)
                    , new ColumnControlSet("chef_commission01", txtCHEF_COMMISSION01)
                    , new ColumnControlSet("chef_commission02", txtCHEF_COMMISSION02)
                    , new ColumnControlSet("td_donut", txtTD_DONUT)
                    , new ColumnControlSet("ad_donut", txtAD_DONUT)
                    , new ColumnControlSet("reco_donut", txtRECO_DONUT)
                    , new ColumnControlSet("d_gs_cele_story", txtD_GS_CELE_STORY)
                    , new ColumnControlSet("cash_gr_cele_story", txtCASH_GR_CELE_STORY)
                    , new ColumnControlSet("remark", txtREMARK)
                   );

            this.efwGridControl1.Click += efwGridControl1_Click;

        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            Open1();
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
        }


        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN14_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_prod_nm", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtPRODUCT_NAME.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                           // this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            Open1();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN14_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_option_id", MySqlDbType.Int32, 10);
                        cmd.Parameters[0].Value = txtOPTION_ID.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            // this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void TxtPRODUCT_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

        }

        private void BtnExcelSample_Click(object sender, EventArgs e)
        {
            byte[] _SampleFile = YL_DONUT.BizFrm.Properties.Resources.후기머니관리_UPDATE_sample;
            string _SaveFileName = "후기머니관리_UPDATE_sample.xls";

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

        private void BtnGetExcel_Click(object sender, EventArgs e)
        {


            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "xls";
            openFileDialog1.FileName = "후기머니관리_UPDATE.xls";
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

                        if (fileName.IndexOf("후기머니관리_UPDATE.xls") < 0)
                        {
                            MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                            return;
                        }

                        this.efwGridControl3.DataSource = ExcelDataBaseHelper.OpenFile2(fileName);

                        //this.efwGridControl1.DataSource = ExcelDataBaseHelper.OpenFile(fileName);

                        this.efwGridControl3.MyGridView.BestFitColumns();
                        lblCnt.Text = string.Format("{0:#,###}", gridView1.DataRowCount.ToString());
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

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView3.DataRowCount; i++)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN14_SAVE_02", con))
                        {

                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_idx", MySqlDbType.Int32, 11);
                            cmd.Parameters[0].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[0]).ToString());

                            cmd.Parameters.Add("i_p_id", MySqlDbType.Int32, 11);
                            cmd.Parameters[1].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[1]).ToString());

                            cmd.Parameters.Add("i_product_name", MySqlDbType.VarChar, 500);
                            cmd.Parameters[2].Value = gridView3.GetRowCellValue(i, gridView3.Columns[2]).ToString();

                            cmd.Parameters.Add("i_option_id", MySqlDbType.Int32, 11);
                            cmd.Parameters[3].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[3]).ToString());
                                                        
                            cmd.Parameters.Add("i_option_name", MySqlDbType.VarChar, 500);
                            cmd.Parameters[4].Value = gridView3.GetRowCellValue(i, gridView3.Columns[4]).ToString();

                            cmd.Parameters.Add("i_customer_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[5].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[5]).ToString());

                            cmd.Parameters.Add("i_lowest_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[6].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[6]).ToString());

                            cmd.Parameters.Add("i_supply_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[7].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[7]).ToString());

                            cmd.Parameters.Add("i_delivery_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[8].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[8]).ToString());

                            cmd.Parameters.Add("i_ps_donut01", MySqlDbType.Int32, 11);
                            cmd.Parameters[9].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[9]).ToString());

                            cmd.Parameters.Add("i_ps_donut02", MySqlDbType.Int32, 11);
                            cmd.Parameters[10].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[10]).ToString());

                            cmd.Parameters.Add("i_vip_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[11].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[11]).ToString());

                            cmd.Parameters.Add("i_ps_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[12].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[12]).ToString());

                            cmd.Parameters.Add("i_ps_oper_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[13].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[13]).ToString());

                            cmd.Parameters.Add("i_chef_commission01", MySqlDbType.Int32, 11);
                            cmd.Parameters[14].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[14]).ToString());

                            cmd.Parameters.Add("i_chef_commission02", MySqlDbType.Int32, 11);
                            cmd.Parameters[15].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[15]).ToString());

                            cmd.Parameters.Add("i_td_donut", MySqlDbType.Int32, 11);
                            cmd.Parameters[16].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[16]).ToString());

                            cmd.Parameters.Add("i_ad_donut", MySqlDbType.Int32, 11);
                            cmd.Parameters[17].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[17]).ToString());

                            cmd.Parameters.Add("i_reco_donut", MySqlDbType.Int32, 11);
                            cmd.Parameters[18].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[18]).ToString());
                            
                            cmd.Parameters.Add("i_d_gs_cele_story", MySqlDbType.Int32, 11);
                            cmd.Parameters[19].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[19]).ToString());

                            cmd.Parameters.Add("i_cash_gr_cele_story", MySqlDbType.Int32, 11);
                            cmd.Parameters[20].Value = Convert.ToInt32(gridView3.GetRowCellValue(i, gridView3.Columns[20]).ToString());

                            cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 500);
                            cmd.Parameters[21].Value = gridView3.GetRowCellValue(i, gridView3.Columns[21]).ToString();

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
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Search();
        }
    }
}
