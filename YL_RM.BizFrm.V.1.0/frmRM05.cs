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
using YL_RM.BizFrm;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using System.Windows;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;


namespace YL_RM.BizFrm
{
    public partial class frmRM05 : FrmBase
    {
        public frmRM05()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmRM05";
            //폼명설정
            this.FrmName = "가입쿠폰 발행";
        }

        private void FrmLoadEvent(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            ////efwLabel1.Font = new Font(efwLabel1.Font, FontStyle.Bold);
            //this.efwLabel1.Font = new System.Drawing.Font("맑은고딕", 26, System.Drawing.FontStyle.Bold);
            ////this.efwLabel1.Font = new Font(this.efwLabel1.Font, FontStyle.Bold);

            rbQ_type.EditValue = "01";
            rbP_type.EditValue = "01";
            rbis_use.EditValue = "T";
            setCmb();

        }

        private void setCmb()
        {

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00052'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbQ1, codeArray);
            }
            cmbQ1.EditValue = "0";

        }


        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM_RM05_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_q_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = rbQ_type.EditValue;

                        cmd.Parameters.Add("i_is_use", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbis_use.EditValue;

                        cmd.Parameters.Add("i_Q1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = txtQuery.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

            {
                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM_RM05_SAVE_01", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 2);
                    cmd.Parameters[0].Value = rbP_type.EditValue;

                    cmd.Parameters.Add("i_cnt", MySqlDbType.Int32);
                    cmd.Parameters[1].Value = Convert.ToInt32(txtCount.EditValue);
                    cmd.ExecuteNonQuery();
                }
            }



            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

            {
                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM_RM05_SELECT_02", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_p_type", MySqlDbType.VarChar, 1);
                    cmd.Parameters[0].Value = "P";

                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable ds = new DataTable();
                        sda.Fill(ds);
                        SatExportToExcel(ds);
                    }
                }
            }
 
        }


        private void SatExportToExcel(System.Data.DataTable ds)
        {

            //엑셀 저장 경로
            string path = @"C:\temp\회원가입쿠폰.xlsx";

            try
            {
                var excelApp = new Excel.Application();
                excelApp.Workbooks.Add();

                Excel._Worksheet workSheet = (Excel._Worksheet)excelApp.ActiveSheet;

                #region 서식변경
                /*
                 workSheet.Columns[3].NumberFormat = "@";
                 */

                var rngCelStr = (Excel.Range)workSheet.Cells[3];
                var rng = rngCelStr.EntireColumn;
                rng.NumberFormat = "@";



                #endregion

                for (var i = 0; i < ds.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = ds.Columns[i].ColumnName;
                }

                for (var i = 0; i < ds.Rows.Count; i++)
                {
                    for (var j = 0; j < ds.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = ds.Rows[i][j];
                    }
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                workSheet.SaveAs(path);
                excelApp.Quit();

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            
            try
            {
                MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Real);
                connection.Open();

                MySqlCommand updateCommand = new MySqlCommand();
                updateCommand.Connection = connection;

                updateCommand.CommandText = "update domamall.tb_am_coupon set " +
                                            "       is_print = 'Y' " +
                                            "   WHERE is_print = 'N' ";

                updateCommand.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            MessageAgent.MessageShow(MessageType.Informational, " C:TEMP 폴더에 회원가입쿠폰.xlsx 파일이 생성되었습니다.");
        }

    }
}
