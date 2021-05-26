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

namespace YL_MM.BizFrm
{
    public partial class frmMM25 : FrmBase
    {
        public frmMM25()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "MM25";
            //폼명설정
            this.FrmName = "G멀티샵 강등 및 예정현황";
        }

        private void frmMM25_Load(object sender, EventArgs e)
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

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM");
            dt1T.EditValue = DateTime.Now.ToString("yyyy-MM");

            DateTime NextMonth;
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "select concat(date_format(adddate(now(), interval 1 month), '%Y-%m')) as NextMonth FROM  dual ";
                DataSet ds = sql.selectQueryDataSet();

                NextMonth = Convert.ToDateTime(sql.selectQueryForSingleValue());
            }
            dt1T.EditValue = NextMonth;


            dt1F.Properties.Mask.EditMask = "yyyy-MM";
            dt1F.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dt1F.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dt1F.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dt1F.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;


            dt1T.Properties.Mask.EditMask = "yyyy-MM";
            dt1T.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dt1T.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dt1T.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dt1T.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["order_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["order_amt"].SummaryItem.FieldName = "order_amt";
            gridView1.Columns["order_amt"].SummaryItem.DisplayFormat = "합계: {0:c}";
            setCmb();
        }
        private void setCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00053' order by code_id  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];


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

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM25_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_qtype1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = dt1F.EditValue3.Substring(0, 6); ;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[2].Value = dt1T.EditValue3.Substring(0, 6); ;

                        cmd.Parameters.Add("i_txt", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtSearch.Text;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
                //lbCount.Text = String.Format("{0:#,##0}", Convert.ToInt32(gridView1.RowCount));
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

    }
}
