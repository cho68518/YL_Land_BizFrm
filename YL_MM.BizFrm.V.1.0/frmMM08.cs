using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
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

namespace YL_MM.BizFrm
{
    public partial class frmMM08 : FrmBase
    {

        public frmMM08()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "MM08";
            //폼명설정
            this.FrmName = "회원정보현황(텔레콤)";
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

            dtSDate.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dtEDate.EditValue = DateTime.Now;
            rbTelType.EditValue = "";
        }
        public override void Search()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    //using (MySqlCommand cmd = new MySqlCommand("telecom.USP_GM_GM01_POP03_SELECT_01", con))
                    using (MySqlCommand cmd = new MySqlCommand("telecom.USP_MM_MM08_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtSDate.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtEDate.EditValue3;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtSearch.EditValue;

                        cmd.Parameters.Add("i_TelType", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = rbTelType.EditValue.ToString();


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }

                lbCount.Text = String.Format("{0:#,##0}", Convert.ToInt32(gridView1.RowCount));
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

