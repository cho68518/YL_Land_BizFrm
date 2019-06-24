using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
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

namespace YL_DONUT.BizFrm
{
    public partial class frmDN06 : FrmBase
    {
        public frmDN06()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "DN06";
            //폼명설정
            this.FrmName = "스토리별 도넛적립 현황";
        }

        #region FrmLoadEvent()

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
            this.IsExcel = false;

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["donut_count"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["donut_count"].SummaryItem.FieldName = "donut_count";
            gridView1.Columns["donut_count"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView2.OptionsView.ShowFooter = true;
            //gridView2.Columns["RECV_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView2.Columns["RECV_AMOUNT"].SummaryItem.FieldName = "RECV_AMOUNT";
            //gridView2.Columns["RECV_AMOUNT"].SummaryItem.DisplayFormat = "사용머니: {0:c}";

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt1T.EditValue = DateTime.Now;

        }

        #endregion

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_DN_DN06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dt1T.EditValue3;

                        //Console.WriteLine(" i_sdate           ---> [" + cmd.Parameters[0].Value + "]");
                        //Console.WriteLine(" i_edate           ---> [" + cmd.Parameters[1].Value + "]");

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




    }
}
