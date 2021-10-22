﻿using DevExpress.XtraTreeList;
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
//using YL_COMM.BizFrm;

namespace YL_DONUT.BizFrm
{
    public partial class frmDN24_Pop01 : FrmPopUpBase
    {
        public frmDN24_Pop01()
        {
            InitializeComponent();
        }


        private void frmDN24_Pop01_Load(object sender, EventArgs e)
        {
            SetCmb();
        }

        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT code_id as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = 00042 and code_memo = 'Y' order by code_id ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());
                CodeAgent.MakeCodeControl(this.cmbO_Sub_Company, codeArray);
                cmbO_Sub_Company.EditValue = "0";
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            // 쿠팡
            if (cmbO_Sub_Company.EditValue.ToString() == "4")
            {
                Excel_Set1();
            }
            // 네이버
            else if (cmbO_Sub_Company.EditValue.ToString() == "5")
            {
                Excel_Set2();
            }
        }

        private void Excel_Set1()
        {
            if (cmbO_Sub_Company.EditValue.ToString() == "0")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 온라인몰을 선택하세요!");
                return;
            }
            efwGridControl2.DataSource = null;
            gridView2.RefreshData();

            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "xlsx";
            openFileDialog1.FileName = "기초재고.xlsx";
            openFileDialog1.Filter = "Excel97 - 2003 통합문서|*.xlsx";
            openFileDialog1.Title = "엑셀데이터 가져오기";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (openFileDialog1.FileName != string.Empty)
                    {
                        string fileName = openFileDialog1.FileName;

                        //if (fileName.IndexOf("기초재고.xlsx") < 0)
                        //{
                        //    MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                        //    return;
                        //}

                        this.efwGridControl1.DataSource = ExcelDataBaseHelper.OpenFile2(fileName);

                        //this.efwGridControl1.DataSource = ExcelDataBaseHelper.OpenFile(fileName);

                        this.efwGridControl1.MyGridView.BestFitColumns();
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
        private void Excel_Set2()
        {
            if (cmbO_Sub_Company.EditValue.ToString() == "0")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 온라인몰을 선택하세요!");
                return;
            }
            efwGridControl1.DataSource = null;
            gridView1.RefreshData();

            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "xlsx";
            openFileDialog1.FileName = "기초재고.xlsx";
            openFileDialog1.Filter = "Excel97 - 2003 통합문서|*.xlsx";
            openFileDialog1.Title = "엑셀데이터 가져오기";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (openFileDialog1.FileName != string.Empty)
                    {
                        string fileName = openFileDialog1.FileName;

                        //if (fileName.IndexOf("기초재고.xlsx") < 0)
                        //{
                        //    MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                        //    return;
                        //}

                        this.efwGridControl2.DataSource = ExcelDataBaseHelper.OpenFile2(fileName);

                        //this.efwGridControl1.DataSource = ExcelDataBaseHelper.OpenFile(fileName);

                        this.efwGridControl2.MyGridView.BestFitColumns();
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

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (cmbO_Sub_Company.EditValue.ToString() == "0")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 온라인몰을 선택하세요!");
                return;
            }
            // 쿠팡
            if  (cmbO_Sub_Company.EditValue.ToString() == "4")
            {
                Save1();
            }
            // 네이버
            else if (cmbO_Sub_Company.EditValue.ToString() == "5")
            {
                Save2();
            }
        }


        private void Save1()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        if (gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString().Length > 10)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_POP01_SAVE01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;


                                cmd.Parameters.Add("i_o_sub_company", MySqlDbType.VarChar, 10);
                                cmd.Parameters[0].Value = cmbO_Sub_Company.EditValue;


                                cmd.Parameters.Add("i_o_date", MySqlDbType.DateTime);
                                cmd.Parameters[1].Value = gridView1.GetRowCellValue(i, gridView1.Columns[9]).ToString();

                                cmd.Parameters.Add("i_o_qty", MySqlDbType.Int32);
                                cmd.Parameters[2].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, gridView1.Columns[22])).ToString();

                                cmd.Parameters.Add("i_o_amt", MySqlDbType.Int32);
                                cmd.Parameters[3].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, gridView1.Columns[23])).ToString();

                                cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 255);
                                cmd.Parameters[4].Value = gridView1.GetRowCellValue(i, gridView1.Columns[10]).ToString();

                                cmd.Parameters.Add("i_option_name", MySqlDbType.VarChar, 255);
                                cmd.Parameters[5].Value = gridView1.GetRowCellValue(i, gridView1.Columns[12]).ToString();

                                cmd.Parameters.Add("i_o_receive_name", MySqlDbType.VarChar, 45);
                                cmd.Parameters[6].Value = gridView1.GetRowCellValue(i, gridView1.Columns[24]).ToString();

                                cmd.Parameters.Add("i_o_receive_contact", MySqlDbType.VarChar, 20);
                                cmd.Parameters[7].Value = gridView1.GetRowCellValue(i, gridView1.Columns[25]).ToString();

                                cmd.Parameters.Add("i_o_receive_name1", MySqlDbType.VarChar, 45);
                                cmd.Parameters[8].Value = gridView1.GetRowCellValue(i, gridView1.Columns[26]).ToString();

                                cmd.Parameters.Add("i_o_receive_contact1", MySqlDbType.VarChar, 20);
                                cmd.Parameters[9].Value = gridView1.GetRowCellValue(i, gridView1.Columns[27]).ToString();

                                cmd.Parameters.Add("i_o_receive_zipcode1", MySqlDbType.VarChar, 6);
                                cmd.Parameters[10].Value = gridView1.GetRowCellValue(i, gridView1.Columns[28]).ToString();

                                cmd.Parameters.Add("i_o_receive_address1", MySqlDbType.VarChar, 254);
                                cmd.Parameters[11].Value = gridView1.GetRowCellValue(i, gridView1.Columns[29]).ToString();

                                cmd.Parameters.Add("i_o_receive_message1", MySqlDbType.VarChar, 254);
                                cmd.Parameters[12].Value = gridView1.GetRowCellValue(i, gridView1.Columns[30]).ToString();

                                cmd.Parameters.Add("i_mall_o_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[13].Value = gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString();
                                
                                cmd.Parameters.Add("i_p_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[14].Value = gridView1.GetRowCellValue(i, gridView1.Columns[13]).ToString();

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
        }

        private void Save2()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView2.DataRowCount; i++)
                    {
                        if (gridView2.GetRowCellValue(i, gridView2.Columns[1]).ToString().Length > 10)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_POP01_SAVE01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;


                                cmd.Parameters.Add("i_o_sub_company", MySqlDbType.VarChar, 10);
                                cmd.Parameters[0].Value = cmbO_Sub_Company.EditValue;

                                string sO_Date = string.Empty;

                                sO_Date = gridView2.GetRowCellValue(i, gridView2.Columns[1]).ToString().Substring(0, 8);
                                sO_Date = sO_Date.Substring(0,4) + "-" + sO_Date.Substring(4,2) + "-" + sO_Date.Substring(6,2) + " 12:01:01";

                                cmd.Parameters.Add("i_o_date", MySqlDbType.DateTime);
                                cmd.Parameters[1].Value = sO_Date;

                                cmd.Parameters.Add("i_o_qty", MySqlDbType.Int32);
                                cmd.Parameters[2].Value = Convert.ToInt32(gridView2.GetRowCellValue(i, gridView2.Columns[20])).ToString();

                                cmd.Parameters.Add("i_o_amt", MySqlDbType.Int32);
                                cmd.Parameters[3].Value = Convert.ToInt32(gridView2.GetRowCellValue(i, gridView2.Columns[22])).ToString();

                                cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 255);
                                cmd.Parameters[4].Value = gridView2.GetRowCellValue(i, gridView2.Columns[16]).ToString();

                                cmd.Parameters.Add("i_option_name", MySqlDbType.VarChar, 255);
                                cmd.Parameters[5].Value = gridView2.GetRowCellValue(i, gridView2.Columns[18]).ToString();

                                cmd.Parameters.Add("i_o_receive_name", MySqlDbType.VarChar, 45);
                                cmd.Parameters[6].Value = gridView2.GetRowCellValue(i, gridView2.Columns[8]).ToString();

                                cmd.Parameters.Add("i_o_receive_contact", MySqlDbType.VarChar, 20);
                                cmd.Parameters[7].Value = gridView2.GetRowCellValue(i, gridView2.Columns[43]).ToString();

                                cmd.Parameters.Add("i_o_receive_name1", MySqlDbType.VarChar, 45);
                                cmd.Parameters[8].Value = gridView2.GetRowCellValue(i, gridView2.Columns[10]).ToString();

                                cmd.Parameters.Add("i_o_receive_contact1", MySqlDbType.VarChar, 20);
                                cmd.Parameters[9].Value = gridView2.GetRowCellValue(i, gridView2.Columns[40]).ToString();

                                cmd.Parameters.Add("i_o_receive_zipcode1", MySqlDbType.VarChar, 6);
                                cmd.Parameters[10].Value = gridView2.GetRowCellValue(i, gridView2.Columns[44]).ToString();

                                cmd.Parameters.Add("i_o_receive_address1", MySqlDbType.VarChar, 254);
                                cmd.Parameters[11].Value = gridView2.GetRowCellValue(i, gridView2.Columns[42]).ToString();

                                cmd.Parameters.Add("i_o_receive_message1", MySqlDbType.VarChar, 254);
                                cmd.Parameters[12].Value = gridView2.GetRowCellValue(i, gridView2.Columns[45]).ToString();

                                cmd.Parameters.Add("i_mall_o_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[13].Value = gridView2.GetRowCellValue(i, gridView2.Columns[1]).ToString();

                                cmd.Parameters.Add("i_p_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[14].Value = gridView2.GetRowCellValue(i, gridView2.Columns[15]).ToString();

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
        }

        private void cmbO_Sub_Company_EditValueChanged(object sender, EventArgs e)
        {
            // 쿠팡
            if (cmbO_Sub_Company.EditValue.ToString() == "4")
            {
                this.efwXtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            }
            // 네이버
            else if (cmbO_Sub_Company.EditValue.ToString() == "5")
            {
                this.efwXtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            }
        }
    }
}