using DevExpress.XtraEditors.Repository;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using YL_TELECOM.BizFrm.Dlg;
using YL_COMM.BizFrm;

namespace YL_TELECOM.BizFrm
{
    public partial class frmTM10_Pop01 : FrmPopUpBase
    {
        public frmTM10_Pop01()
        {
            InitializeComponent();
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {

            efwGridControl1.DataSource = null;
            gridView1.RefreshData();

            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "xlsx";
            openFileDialog1.FileName = "메모.xlsx";
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
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {

                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        if (gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString().Length > 5)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM10_POP01_SAVE02", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_entr_no", MySqlDbType.VarChar);
                                cmd.Parameters[0].Value = gridView1.GetRowCellValue(i, gridView1.Columns[0]).ToString();

                                cmd.Parameters.Add("i_w_info", MySqlDbType.VarChar);
                                cmd.Parameters[1].Value = gridView1.GetRowCellValue(i, gridView1.Columns[1]).ToString();

                                cmd.Parameters.Add("i_insert_user", MySqlDbType.VarChar);
                                cmd.Parameters[2].Value = txtInsert_User.EditValue;

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
