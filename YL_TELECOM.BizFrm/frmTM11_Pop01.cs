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
//using YL_COMM.BizFrm;

namespace YL_TELECOM.BizFrm
{
    public partial class frmTM11_Pop01 : FrmPopUpBase
    {
        public frmTM11_Pop01()
        {
            InitializeComponent();
        }

        private void frmTM10_Pop01_Load(object sender, EventArgs e)
        {
            dtS_DATE.EditValue = DateTime.Now;
            setCmb();
        }

        private void setCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.TelConn_Real))
            {
                con.Query = " SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM erp.tb_common_code where gcode_id = '00003'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbOut_Factory, codeArray);
            }
            cmbOut_Factory.EditValue = "000001";


            using (MySQLConn con = new MySQLConn(ConstantLib.TelConn_Real))
            {
                con.Query = " SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM erp.tb_common_code where gcode_id = '00003'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbIn_Factory, codeArray);
            }
            cmbIn_Factory.EditValue = "000001";

        }


        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "xlsx";
            openFileDialog1.FileName = "상품출고.xlsx";
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

                        if (fileName.IndexOf("상품출고.xlsx") < 0)
                        {
                            MessageAgent.MessageShow(MessageType.Error, "엑셀파일명에 문제가 있습니다. 파일명을 확인하세요.");
                            return;
                        }

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
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {

                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        if (gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString().Length > 2)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM11_SAVE_01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_out_date", MySqlDbType.VarChar, 8);
                                cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                                cmd.Parameters.Add("i_out_factory", MySqlDbType.VarChar, 50);
                                cmd.Parameters[1].Value = cmbOut_Factory.EditValue;

                                cmd.Parameters.Add("i_in_factory", MySqlDbType.VarChar, 50);
                                cmd.Parameters[2].Value = cmbIn_Factory.EditValue;

                                cmd.Parameters.Add("i_m_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[3].Value = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();

                                cmd.Parameters.Add("i_ser_no", MySqlDbType.VarChar, 50);
                                cmd.Parameters[4].Value = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();

                                cmd.Parameters.Add("i_qty", MySqlDbType.Int32, 11);
                                cmd.Parameters[5].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString());


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
