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


                        // 스마트스토어
                        cmd.Parameters.Add(new MySqlParameter("o_lbl701", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl701"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl703", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl703"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl704", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl704"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl706", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl706"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl707", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl707"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl709", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl709"].Direction = ParameterDirection.Output;

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
                        //스마트스토어
                        lbl701.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl701"].Value);
                        lbl703.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl703"].Value);
                        lbl704.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl704"].Value);
                        lbl706.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl706"].Value);
                        lbl707.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl707"].Value);
                        lbl709.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl709"].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            Open3();
        }


        private void Open3()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;
                int n1 = 0; int n2 = 0; int n3 = 0; int n4 = 0; int n3_1 = 0; int n3_2 = 0;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("telecom.USP_GM_GM15_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = DateTime.Now.ToString("yyyyMMdd");

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = DateTime.Now.ToString("yyyyMMdd");

                        //Console.WriteLine(" i_sdate           ---> [" + cmd.Parameters[0].Value + "]");
                        //Console.WriteLine(" i_edate           ---> [" + cmd.Parameters[1].Value + "]");

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow[] rows = dt.Select();

                                //-----------------------------------------------------------------------------------
                                //텔레콤 정보현황
                                //-----------------------------------------------------------------------------------


                                //총누적
                                lblTel1.Text = String.Format("{0:#,##0}", rows[0]["tel1"]);
                                lblTel2.Text = String.Format("{0:#,##0}", rows[0]["tel2"]);
                                lblTel3.Text = String.Format("{0:#,##0}", rows[0]["tel3"]);
                                lblTel3_1.Text = String.Format("{0:#,##0}", rows[0]["tel3_1"]);
                                lblTel3_2.Text = String.Format("{0:#,##0}", rows[0]["tel3_2"]);
                                lblTel5.Text = String.Format("{0:#,##0}", rows[0]["tel5"]);


                                //당일개통
                                lblTel17.Text = String.Format("{0:#,##0}", rows[0]["teld17"]);
                                lblTel18.Text = String.Format("{0:#,##0}", rows[0]["teld18"]);
                                lblTel19.Text = String.Format("{0:#,##0}", rows[0]["teld19"]);
                                lblTel19_1.Text = String.Format("{0:#,##0}", rows[0]["teld19_1"]);
                                lblTel19_2.Text = String.Format("{0:#,##0}", rows[0]["teld19_2"]);
                                lblTel21.Text = String.Format("{0:#,##0}", rows[0]["teld21"]);

                                //당일해지
                                lblTel25.Text = String.Format("{0:#,##0}", rows[0]["teld25"]);
                                lblTel26.Text = String.Format("{0:#,##0}", rows[0]["teld26"]);
                                lblTel27.Text = String.Format("{0:#,##0}", rows[0]["teld27"]);
                                lblTel27_1.Text = String.Format("{0:#,##0}", rows[0]["teld27_1"]);
                                lblTel27_2.Text = String.Format("{0:#,##0}", rows[0]["teld27_2"]);
                                lblTel29.Text = String.Format("{0:#,##0}", rows[0]["teld29"]);

                                //월누적개통
                                lblTel41.Text = String.Format("{0:#,##0}", rows[0]["teld41"]);
                                lblTel42.Text = String.Format("{0:#,##0}", rows[0]["teld42"]);
                                lblTel43.Text = String.Format("{0:#,##0}", rows[0]["teld43"]);
                                lblTel43_1.Text = String.Format("{0:#,##0}", rows[0]["teld43_1"]);
                                lblTel43_2.Text = String.Format("{0:#,##0}", rows[0]["teld43_2"]);
                                lblTel45.Text = String.Format("{0:#,##0}", rows[0]["teld45"]);

                                //월누적해지
                                lblTel49.Text = String.Format("{0:#,##0}", rows[0]["teld49"]);
                                lblTel50.Text = String.Format("{0:#,##0}", rows[0]["teld50"]);
                                lblTel51.Text = String.Format("{0:#,##0}", rows[0]["teld51"]);
                                lblTel51_1.Text = String.Format("{0:#,##0}", rows[0]["teld51_1"]);
                                lblTel51_2.Text = String.Format("{0:#,##0}", rows[0]["teld51_2"]);
                                lblTel53.Text = String.Format("{0:#,##0}", rows[0]["teld53"]);

                                //일증감수
                                n1 = Convert.ToInt32(rows[0]["teld17"]) - Convert.ToInt32(rows[0]["teld25"]);
                                n2 = Convert.ToInt32(rows[0]["teld18"]) - Convert.ToInt32(rows[0]["teld26"]);
                                n3 = Convert.ToInt32(rows[0]["teld19"]) - Convert.ToInt32(rows[0]["teld27"]);
                                n3_1 = Convert.ToInt32(rows[0]["teld19_1"]) - Convert.ToInt32(rows[0]["teld27_1"]);
                                n3_2 = Convert.ToInt32(rows[0]["teld19_2"]) - Convert.ToInt32(rows[0]["teld27_2"]);
                                n4 = Convert.ToInt32(rows[0]["teld21"]) - Convert.ToInt32(rows[0]["teld29"]);
                                lblTel33.Text = String.Format("{0:#,##0}", n1);
                                lblTel34.Text = String.Format("{0:#,##0}", n2);
                                lblTel35.Text = String.Format("{0:#,##0}", n3);
                                lblTel35_1.Text = String.Format("{0:#,##0}", n3_1);
                                lblTel35_2.Text = String.Format("{0:#,##0}", n3_2);
                                lblTel37.Text = String.Format("{0:#,##0}", n4);

                                //월증감수
                                n1 = Convert.ToInt32(rows[0]["teld41"]) - Convert.ToInt32(rows[0]["teld49"]);
                                n2 = Convert.ToInt32(rows[0]["teld42"]) - Convert.ToInt32(rows[0]["teld50"]);
                                n3 = Convert.ToInt32(rows[0]["teld43"]) - Convert.ToInt32(rows[0]["teld51"]);
                                n3_1 = Convert.ToInt32(rows[0]["teld43_1"]) - Convert.ToInt32(rows[0]["teld51_1"]);
                                n3_2 = Convert.ToInt32(rows[0]["teld43_2"]) - Convert.ToInt32(rows[0]["teld51_2"]);
                                n4 = Convert.ToInt32(rows[0]["teld45"]) - Convert.ToInt32(rows[0]["teld53"]);
                                lblTel57.Text = String.Format("{0:#,##0}", n1);
                                lblTel58.Text = String.Format("{0:#,##0}", n2);
                                lblTel59.Text = String.Format("{0:#,##0}", n3);
                                lblTel59_1.Text = String.Format("{0:#,##0}", n3_1);
                                lblTel59_2.Text = String.Format("{0:#,##0}", n3_2);
                                lblTel61.Text = String.Format("{0:#,##0}", n4);

                                //총 합계
                                lblTel8.Text  = String.Format("{0:#,##0}", Convert.ToInt32(lblTel1.Text.Replace(",", "")) + Convert.ToInt32(lblTel2.Text.Replace(",", "")) + Convert.ToInt32(lblTel3.Text.Replace(",", "")) + Convert.ToInt32(lblTel3_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel3_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel5.Text.Replace(",", "")) );
                                lblTel24.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel17.Text.Replace(",", "")) + Convert.ToInt32(lblTel18.Text.Replace(",", "")) + Convert.ToInt32(lblTel19.Text.Replace(",", "")) + Convert.ToInt32(lblTel19_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel19_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel21.Text.Replace(",", "")));
                                lblTel32.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel25.Text.Replace(",", "")) + Convert.ToInt32(lblTel26.Text.Replace(",", "")) + Convert.ToInt32(lblTel27.Text.Replace(",", "")) + Convert.ToInt32(lblTel27_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel27_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel29.Text.Replace(",", "")));
                                lblTel48.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel24.Text.Replace(",", "")) + Convert.ToInt32(lblTel32.Text.Replace(",", "")) + Convert.ToInt32(lblTel43.Text.Replace(",", "")) + Convert.ToInt32(lblTel43_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel43_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel45.Text.Replace(",", "")));
                                lblTel56.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel49.Text.Replace(",", "")) + Convert.ToInt32(lblTel50.Text.Replace(",", "")) + Convert.ToInt32(lblTel51.Text.Replace(",", "")) + Convert.ToInt32(lblTel51_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel51_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel53.Text.Replace(",", "")));
                                lblTel40.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel24.Text.Replace(",", "")) - Convert.ToInt32(lblTel32.Text.Replace(",", "")));
                                lblTel64.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel48.Text.Replace(",", "")) - Convert.ToInt32(lblTel56.Text.Replace(",", "")) );
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
