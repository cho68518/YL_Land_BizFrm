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

            //   dtS_DATE.EditValue = DateTime.Now;
            dtS_DATE.EditValue = DateTime.Now;
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

        }

        public override void Search()
        {
            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM15_SELECT_01", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl11", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl11"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl12", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl12"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl13", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl13"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl14", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl14"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl15", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl15"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl16", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl16"].Direction = ParameterDirection.Output;

                        // BIZ

                        cmd.Parameters.Add(new MySqlParameter("o_lbl21", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl21"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl22", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl22"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl23", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl23"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl24", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl24"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl25", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl25"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl26", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl26"].Direction = ParameterDirection.Output;

                        // 멀티샵

                        cmd.Parameters.Add(new MySqlParameter("o_lbl31", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl31"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl32", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl32"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl33", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl33"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl34", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl34"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl35", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl35"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl36", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl36"].Direction = ParameterDirection.Output;
                        // 주문
                        cmd.Parameters.Add(new MySqlParameter("o_lbl101", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl101"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl102", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl102"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl103", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl103"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl104", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl104"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl105", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl105"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl106", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl106"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl107", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl107"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl108", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl108"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl109", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl109"].Direction = ParameterDirection.Output;

                        // BIZ 주문
                        cmd.Parameters.Add(new MySqlParameter("o_lbl201", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl201"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl202", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl202"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl203", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl203"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl204", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl204"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl205", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl205"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl206", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl206"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl207", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl207"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl208", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl208"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl209", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl209"].Direction = ParameterDirection.Output;


                        // 멀티샵 주문
                        cmd.Parameters.Add(new MySqlParameter("o_lbl301", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl301"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl302", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl302"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl303", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl303"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl304", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl304"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl305", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl305"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl306", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl306"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl307", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl307"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl308", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl308"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl309", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl309"].Direction = ParameterDirection.Output;

                        // 케페24
                        cmd.Parameters.Add(new MySqlParameter("o_lbl401", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl401"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl403", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl403"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl404", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl404"].Direction = ParameterDirection.Output;
                        
                        cmd.Parameters.Add(new MySqlParameter("o_lbl406", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl406"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl407", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl407"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl409", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl409"].Direction = ParameterDirection.Output;

                        // 오픈마켓
                        cmd.Parameters.Add(new MySqlParameter("o_lbl501", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl501"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl503", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl503"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl504", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl504"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl506", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl506"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl507", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl507"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl509", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl509"].Direction = ParameterDirection.Output;


                        // 외부주문
                        cmd.Parameters.Add(new MySqlParameter("o_lbl601", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl601"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl603", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl603"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl604", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl604"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl606", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl606"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl607", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl607"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl609", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl609"].Direction = ParameterDirection.Output;

                        //

                        cmd.ExecuteNonQuery();
                        // VIP    String.Format("{0:#,##0}", rows[0]["teld17"]);
                        lbl11.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl11"].Value);
                        lbl12.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl12"].Value);
                        lbl13.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl13"].Value);
                        lbl14.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl14"].Value);
                        lbl15.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl15"].Value);
                        lbl16.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl16"].Value);
                        // BIZ
                        lbl21.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl21"].Value);
                        lbl22.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl22"].Value);
                        lbl23.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl23"].Value);
                        lbl24.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl24"].Value);
                        lbl25.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl25"].Value);
                        lbl26.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl26"].Value);
                        // 멀티샵
                        lbl31.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl31"].Value);
                        lbl32.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl32"].Value);
                        lbl33.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl33"].Value);
                        lbl34.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl34"].Value);
                        lbl35.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl35"].Value);
                        lbl36.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl36"].Value);
                        // 주문
                        lbl101.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl101"].Value);
                        lbl102.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl102"].Value);
                        lbl103.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl103"].Value);
                        lbl104.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl104"].Value);
                        lbl105.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl105"].Value);
                        lbl106.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl106"].Value);
                        lbl107.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl107"].Value);
                        lbl108.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl108"].Value);
                        lbl109.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl109"].Value);
                        // BIZ주문
                        lbl201.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl201"].Value);
                        lbl202.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl202"].Value);
                        lbl203.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl203"].Value);
                        lbl204.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl204"].Value);
                        lbl205.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl205"].Value);
                        lbl206.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl206"].Value);
                        lbl207.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl207"].Value);
                        lbl208.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl208"].Value);
                        lbl209.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl209"].Value);
                        // 멀티샵
                        lbl301.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl301"].Value);
                        lbl302.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl302"].Value);
                        lbl303.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl303"].Value);
                        lbl304.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl304"].Value);
                        lbl305.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl305"].Value);
                        lbl306.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl306"].Value);
                        lbl307.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl307"].Value);
                        lbl308.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl308"].Value);
                        lbl309.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl309"].Value);
                        //카페24
                        lbl401.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl401"].Value);
                        lbl403.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl403"].Value);
                        lbl404.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl404"].Value);
                        lbl406.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl406"].Value);
                        lbl407.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl407"].Value);
                        lbl409.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl409"].Value);
                        //오픈마켓
                        lbl501.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl501"].Value);
                        lbl503.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl503"].Value);
                        lbl504.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl504"].Value);
                        lbl506.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl506"].Value);
                        lbl507.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl507"].Value);
                        lbl509.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl509"].Value);
                        //외부주문
                        lbl601.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl601"].Value);
                        lbl603.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl603"].Value);
                        lbl604.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl604"].Value);
                        lbl606.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl606"].Value);
                        lbl607.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl607"].Value);
                        lbl609.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl609"].Value);


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
