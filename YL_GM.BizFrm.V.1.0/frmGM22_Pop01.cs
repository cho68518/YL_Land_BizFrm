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

namespace YL_GM.BizFrm
{
    public partial class frmGM22_Pop01 : FrmPopUpBase
    {
        public frmGM22_Pop01()
        {
            InitializeComponent();
        }

        private void frmGM22_Pop01_Load(object sender, EventArgs e)
        {
            SetCmb();
        }
        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00059'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCompany, codeArray);
            }
            cmbCompany.EditValue = "";
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (cmbCompany.EditValue.ToString() == "0")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회사구분을 선택하세요!");
                return;
            }
            efwGridControl1.DataSource = null;
            gridView1.RefreshData();

            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "xlsx";
            openFileDialog1.FileName = "*.*";
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

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (cmbCompany.EditValue.ToString() == "" )
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사구분을 선택하세요 !");
                return;
            }
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        if (gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString().Length > 10)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM22_POP01_SAVE01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;


                                cmd.Parameters.Add("i_company", MySqlDbType.VarChar, 10);
                                cmd.Parameters[0].Value = cmbCompany.EditValue;

                                string sAcc_Date = string.Empty;
                                sAcc_Date = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();
                                sAcc_Date = sAcc_Date.Substring(0, 4) + "-" + sAcc_Date.Substring(5, 2) + "-" + sAcc_Date.Substring(8, 2) +  " " + sAcc_Date.Substring(11, 8);

                                cmd.Parameters.Add("i_acc_date", MySqlDbType.DateTime);
                                cmd.Parameters[1].Value = sAcc_Date;

                                string sIn_Amt = string.Empty;
                                string sOut_Amt = string.Empty;
                                sIn_Amt = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();
                                if (sIn_Amt == "-")
                                {
                                    sIn_Amt = "0";
                                }
                                else
                                {
                                    sIn_Amt = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();
                                }

                                sOut_Amt = gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString();
                                if (sOut_Amt == "-")
                                {
                                    sOut_Amt = "0";
                                }
                                else
                                {
                                    sOut_Amt = gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString();
                                }

                                cmd.Parameters.Add("i_in_amt", MySqlDbType.Int32);
                                cmd.Parameters[2].Value = Convert.ToInt32(sIn_Amt).ToString();

                                cmd.Parameters.Add("i_out_amt", MySqlDbType.Int32);
                                cmd.Parameters[3].Value = Convert.ToInt32(sOut_Amt).ToString();

                                cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 255);
                                cmd.Parameters[4].Value = gridView1.GetRowCellValue(i, gridView1.Columns[3]).ToString();

                                cmd.Parameters.Add("i_summary", MySqlDbType.VarChar, 255);
                                cmd.Parameters[5].Value = gridView1.GetRowCellValue(i, gridView1.Columns[4]).ToString();

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
    }
}
