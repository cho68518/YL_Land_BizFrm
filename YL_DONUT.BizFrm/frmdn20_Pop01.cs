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
    public partial class frmDN20_Pop01 : FrmPopUpBase
    {
        public frmDN20_Pop01()
        {
            InitializeComponent();
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {

            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "xls";
            openFileDialog1.FileName = "delivery_num_upload_송장일괄등록.xlsx";
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

                        if (fileName.IndexOf("delivery_num_upload_송장일괄등록.xls") < 0)
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
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM04_SAVE_02", con))
                        {

                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_id", MySqlDbType.Int32, 11);
                            cmd.Parameters[0].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString());

                            cmd.Parameters.Add("i_delivery_num", MySqlDbType.VarChar, 10);
                            cmd.Parameters[1].Value = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();

                            cmd.Parameters.Add("i_delivery_code", MySqlDbType.VarChar, 500);
                            cmd.Parameters[2].Value = gridView1.GetRowCellValue(i, gridView1.Columns[2]).ToString();

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
        }
    }
}
