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

namespace YL_MM.BizFrm
{
    public partial class frmMM17 : FrmBase
    {
        public frmMM17()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "MM17";
            //폼명설정
            this.FrmName = "문자 전송 현황";
        }

        private void frmMM17_Load(object sender, EventArgs e)
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

            rbQType.EditValue = "A";
            dtSDATE.EditValue = DateTime.Now.ToString("yyyy-MM");
            dtSDATE.Properties.Mask.EditMask = "yyyy-MM";
            dtSDATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtSDATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtSDATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtSDATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {

        }

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM17_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtSDATE.EditValue3.Substring(0,6);

                        string sQuery;
                        if (string.IsNullOrEmpty(this.txtQuery.Text))
                        {
                            sQuery = " ";
                        }
                        else
                        {
                            sQuery = txtQuery.EditValue.ToString();
                        }

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar,50);
                        cmd.Parameters[1].Value = sQuery;


                        cmd.Parameters.Add("i_QType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = rbQType.EditValue;

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

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }
    }
}
