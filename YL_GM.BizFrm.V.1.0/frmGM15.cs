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


                        // 멀티샵 연장( 재가입 )


                        cmd.Parameters.Add(new MySqlParameter("o_lbl41", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl41"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl42", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl42"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl43", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl43"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl44", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl44"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl45", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl45"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl46", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl46"].Direction = ParameterDirection.Output;

                        // 일반멤버 가입
                        cmd.Parameters.Add(new MySqlParameter("o_lbl51", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl51"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl52", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl52"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl56", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl56"].Direction = ParameterDirection.Output;



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

                        cmd.Parameters.Add(new MySqlParameter("o_lbl402", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl402"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl403", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl403"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl404", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl404"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl405", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl405"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl406", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl406"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl407", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl407"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl408", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl408"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl409", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl409"].Direction = ParameterDirection.Output;

                        // 오픈마켓
                        cmd.Parameters.Add(new MySqlParameter("o_lbl501", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl501"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl502", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl502"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl503", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl503"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl504", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl504"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl505", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl505"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl506", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl506"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl507", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl507"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl508", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl508"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl509", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl509"].Direction = ParameterDirection.Output;


                        // 외부주문
                        cmd.Parameters.Add(new MySqlParameter("o_lbl601", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl601"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl602", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl602"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl603", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl603"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl604", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl604"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl605", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl605"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl606", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl606"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl607", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl607"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl608", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl608"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl609", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl609"].Direction = ParameterDirection.Output;


                        cmd.Parameters.Add(new MySqlParameter("o_lbl104_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl104_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl204_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl204_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl304_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl304_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl404_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl404_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl504_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl504_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl604_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl604_1"].Direction = ParameterDirection.Output;


                        cmd.Parameters.Add(new MySqlParameter("o_lbl106_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl106_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl206_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl206_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl306_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl306_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl406_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl406_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl506_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl506_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl606_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl606_1"].Direction = ParameterDirection.Output;


                        cmd.Parameters.Add(new MySqlParameter("o_lbl105_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl105_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl205_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl205_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl305_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl305_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl405_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl405_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl505_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl505_1"].Direction = ParameterDirection.Output;

                        // 합계
                        cmd.Parameters.Add(new MySqlParameter("o_lbl801", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl801"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl802", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl802"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl803", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl803"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl804", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl804"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl805", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl805"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl806", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl806"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl804_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl804_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl805_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl805_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl806_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl806_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl807", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl807"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl808", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl808"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl809", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl809"].Direction = ParameterDirection.Output;

                        // 일반상품
                        cmd.Parameters.Add(new MySqlParameter("o_lbl901", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl901"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl902", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl902"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl903", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl903"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl904", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl904"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl905", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl905"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl906", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl906"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl904_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl904_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl905_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl905_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl906_1", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl906_1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl907", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl907"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl908", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl908"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl909", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl909"].Direction = ParameterDirection.Output;

                        // 체험샵현황
                        cmd.Parameters.Add(new MySqlParameter("o_lbl61", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl61"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl62", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl62"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl63", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl63"].Direction = ParameterDirection.Output;

                        // 판매샵
                        cmd.Parameters.Add(new MySqlParameter("o_lbl71", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl71"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl72", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl72"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl73", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl73"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1001", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1001"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1002", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1002"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1003", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1003"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1004", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1004"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1005", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1005"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1006", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1006"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1007", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1007"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1008", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1008"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1009", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1009"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1010", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1010"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1011", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1011"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_lbl1012", MySqlDbType.Int32));
                        cmd.Parameters["o_lbl1012"].Direction = ParameterDirection.Output;


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
                        // 멀티샵 ( 연장 재가입 )
                        lbl41.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl41"].Value);
                        lbl42.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl42"].Value);
                        lbl43.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl43"].Value);
                        lbl44.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl44"].Value);
                        lbl45.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl45"].Value);
                        lbl46.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl46"].Value);

                        lbl51.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl51"].Value);
                        lbl52.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl52"].Value);
                        lbl56.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl56"].Value);

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
                        lbl402.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl402"].Value);
                        lbl404.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl404"].Value);
                        lbl405.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl405"].Value);
                        lbl406.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl406"].Value);
                        lbl407.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl407"].Value);
                        lbl408.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl408"].Value);
                        lbl409.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl409"].Value);
                        //오픈마켓
                        lbl501.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl501"].Value);
                        lbl502.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl502"].Value);
                        lbl503.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl503"].Value);
                        lbl504.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl504"].Value);
                        lbl505.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl505"].Value);
                        lbl506.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl506"].Value);
                        lbl507.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl507"].Value);
                        lbl508.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl508"].Value);
                        lbl509.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl509"].Value);
                        //외부주문
                        lbl601.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl601"].Value);
                        lbl602.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl602"].Value);
                        lbl603.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl603"].Value);
                        lbl604.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl604"].Value);
                        lbl605.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl605"].Value);
                        lbl606.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl606"].Value);
                        lbl607.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl607"].Value);
                        lbl608.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl608"].Value);
                        lbl609.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl609"].Value);

                        //전월
                        lbl104_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl104_1"].Value);
                        lbl204_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl204_1"].Value);
                        lbl304_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl304_1"].Value);
                        lbl404_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl404_1"].Value);
                        lbl504_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl504_1"].Value);
                        lbl604_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl604_1"].Value);

                        lbl106_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl106_1"].Value);
                        lbl206_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl206_1"].Value);
                        lbl306_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl306_1"].Value);
                        lbl406_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl406_1"].Value);
                        lbl506_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl506_1"].Value);
                        lbl606_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl606_1"].Value);


                        lbl105_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl105_1"].Value);
                        lbl205_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl205_1"].Value);
                        lbl305_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl305_1"].Value);
                        lbl405_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl405_1"].Value);
                        lbl505_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl505_1"].Value);

                        // 합계

                        lbl801.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl801"].Value);
                        lbl802.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl802"].Value);
                        lbl803.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl803"].Value);
                        lbl804.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl804"].Value);
                        lbl805.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl805"].Value);
                        lbl806.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl806"].Value);
                        lbl804_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl804_1"].Value);
                        lbl805_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl805_1"].Value);
                        lbl806_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl806_1"].Value);
                        lbl807.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl807"].Value);
                        lbl808.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl808"].Value);
                        lbl809.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl809"].Value);
                        // 일반상품

                        lbl901.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl901"].Value);
                        lbl902.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl902"].Value);
                        lbl903.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl903"].Value);
                        lbl904.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl904"].Value);
                        lbl905.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl905"].Value);
                        lbl906.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl906"].Value);
                        lbl904_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl904_1"].Value);
                        lbl905_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl905_1"].Value);
                        lbl906_1.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl906_1"].Value);
                        lbl907.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl907"].Value);
                        lbl908.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl908"].Value);
                        lbl909.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl909"].Value);

                        // 체험샵현황
                        lbl61.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl61"].Value);
                        lbl62.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl62"].Value);
                        lbl63.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl63"].Value);


                        // 판매샵
                        lbl71.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl71"].Value);
                        lbl72.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl72"].Value);
                        lbl73.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl73"].Value);

                        lbl1001.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1001"].Value);
                        lbl1002.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1002"].Value);
                        lbl1003.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1003"].Value);
                        lbl1004.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1004"].Value);
                        lbl1005.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1005"].Value);
                        lbl1006.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1006"].Value);
                        lbl1007.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1007"].Value);
                        lbl1008.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1008"].Value);
                        lbl1009.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1009"].Value);
                        lbl1010.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1010"].Value);
                        lbl1011.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1011"].Value);
                        lbl1012.Text = String.Format("{0:#,##0}", cmd.Parameters["o_lbl1012"].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
 
        }

        private void efwLabel15_Click(object sender, EventArgs e)
        {

        }
    }
}
