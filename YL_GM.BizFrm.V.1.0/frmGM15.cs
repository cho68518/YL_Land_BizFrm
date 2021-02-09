using DevExpress.XtraCharts;
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


namespace YL_GM.BizFrm
{
    public partial class frmGM15 : FrmBase
    {
        public frmGM15()
        {
            InitializeComponent();
            this.QCode = "GM15";
            //폼명설정
            this.FrmName = "매출현황";
        }


        private void frmGM15_Load(object sender, EventArgs e)
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

       //     dtS_DATE.EditValue = DateTime.Now;

        }

        private void Search()
        {
            try
            {
                //string sP_SHOW_TYPE = string.Empty;
                //decimal n1 = 0; decimal n2 = 0;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM03_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 8);
                  //      cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);
                  //

                //        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 1);
                //        cmd.Parameters[1].Value = rbYearType.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow[] rows = dt.Select();
                                // 지역별 COUNT
                    //            efwArea1.Text = String.Format("{0:#,##0}", rows[0]["Area1"]);

                            }
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

    }
}
