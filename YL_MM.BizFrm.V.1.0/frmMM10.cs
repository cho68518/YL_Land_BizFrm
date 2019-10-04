using Easy.Framework.Common;
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
    public partial class frmMM10 : FrmBase
    {
        public frmMM10()
        {
            InitializeComponent();

            this.QCode = "MM10";
            //폼명설정
            this.FrmName = "회원별 만기현황";
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
            this.IsExcel = false;

            //dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            //dt1T.EditValue = DateTime.Now;
            //dtEDate.EditValue = DateTime.Now.AddDays(7);

            //dtSDate.EditValue = DateTime.Now;
            cmbQ1.EditValue = "1";
        }

        public override void Search()
        {
            //try
            //{

            //    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
            //    {
            //        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM09_SELECT_01", con))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
            //            cmd.Parameters[0].Value = dtSDate.EditValue3;

            //            cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
            //            cmd.Parameters[1].Value = cmbQ1.EditValue;

            //            cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
            //            cmd.Parameters[2].Value = txtSearch.EditValue;

            //            cmd.Parameters.Add("i_memberType", MySqlDbType.VarChar, 10);
            //            cmd.Parameters[3].Value = rbTelType.EditValue.ToString();


            //            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
            //            {
            //                DataTable ds = new DataTable();
            //                sda.Fill(ds);
            //                efwGridControl1.DataBind(ds);
            //                this.efwGridControl1.MyGridView.BestFitColumns();
            //            }
            //        }
            //    }

            //    lbCount.Text = String.Format("{0:#,##0}", Convert.ToInt32(gridView1.RowCount));
            //}
            //catch (Exception ex)
            //{
            //    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            //}
        }




    }
}
