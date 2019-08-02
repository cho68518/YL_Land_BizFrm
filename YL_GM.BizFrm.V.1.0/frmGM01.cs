using DevExpress.XtraCharts;
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
using YL_GM.BizFrm.Dlg;

namespace YL_GM.BizFrm
{
    public partial class frmGM01 : FrmBase
    {
        frmGM01_Pop01 popup;
        frmGM01_Pop02 popup1;
        frmGM01_Pop03 popup2;

        #region Fields

        //bool _is_dupchk = false;

        #endregion

        #region 생성자

        public frmGM01()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "GM01";
            //폼명설정
            this.FrmName = "회원정보현황";

            //gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);
        }

        #endregion

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            //DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            //Font myFont = new Font(efwLabel12.Font, FontStyle.Underline);
            //efwLabel12.Font = myFont;

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            if (UserInfo.instance().UserId == "169.254.169.113" || UserInfo.instance().UserId == "0000000024")
            {
                panel5.Visible = true;
            }
            else
            {
                panel5.Visible = false;
            }
           
        }

        #endregion

        public override void Search()
        {
            lblDate.Text = DateTime.Now.ToString() + " 현재";

            Open1();
            Open2();
            Open3();

            TotAdd();

            View1();
            View2();
        }

        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;
                decimal n1 = 0; decimal n2 = 0;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.Member_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("DB_MEMBER.USP_GM_GM01_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = DateTime.Now.ToString("yyyyMMdd");

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = DateTime.Now.ToString("yyyyMMdd");

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow[] rows = dt.Select();

                                //-----------------------------------------------------------------------------------
                                //회원 정보현황
                                //-----------------------------------------------------------------------------------
                                ////1.총회원수
                                ////현재회원수
                                //lbl1.Text = String.Format("{0:#,##0}", rows[0]["lifeportalTotal"]);
                                ////금일인원
                                //lbl8.Text = String.Format("{0:#,##0}", rows[0]["lifeportalDay"]);
                                ////전일인원
                                //lbl15.Text = String.Format("{0:#,##0}", rows[0]["lifeportalDayBef"]);
                                ////일증감수
                                //lbl22.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["lifeportalDay"]) - Convert.ToInt32(rows[0]["lifeportalDayBef"]));
                                ////당월인원
                                //lbl29.Text = String.Format("{0:#,##0}", rows[0]["lifeportalMonth"]);
                                ////전월인원
                                //lbl36.Text = String.Format("{0:#,##0}", rows[0]["lifeportalMonthBef"]);
                                ////월증감수
                                //lbl43.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["lifeportalMonth"]) - Convert.ToInt32(rows[0]["lifeportalMonthBef"]));
                                ////전월대비증감율
                                //n1 = Convert.ToDecimal(rows[0]["lifeportalMonth"]);
                                //n2 = Convert.ToDecimal(rows[0]["lifeportalMonthBef"]);
                                //lbl50.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";

                                //2.포털가입 회원수
                                //현재회원수
                                lbl2.Text = String.Format("{0:#,##0}", rows[0]["portalTotal"]);
                                //금일인원
                                lbl9.Text = String.Format("{0:#,##0}", rows[0]["portalDay"]);
                                //전일인원
                                lbl16.Text = String.Format("{0:#,##0}", rows[0]["portalDayBef"]);
                                //일증감수
                                lbl23.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["portalDay"]) - Convert.ToInt32(rows[0]["portalDayBef"]));
                                //당월인원
                                lbl30.Text = String.Format("{0:#,##0}", rows[0]["portalMonth"]);
                                //전월인원
                                lbl37.Text = String.Format("{0:#,##0}", rows[0]["portalMonthBef"]);
                                //월증감수
                                lbl44.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["portalMonth"]) - Convert.ToInt32(rows[0]["portalMonthBef"]));
                                //전월대비증감율
                                n1 = Convert.ToDecimal(rows[0]["portalMonth"]);
                                n2 = Convert.ToDecimal(rows[0]["portalMonthBef"]);
                                lbl51.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";

                                //3.라이프가입 회원수
                                //현재회원수
                                //lbl3.Text = String.Format("{0:#,##0}", rows[0]["lifeTotal"]);
                                //금일인원
                                //lbl10.Text = String.Format("{0:#,##0}", rows[0]["lifeDay"]);
                                //전일인원
                                //lbl17.Text = String.Format("{0:#,##0}", rows[0]["lifeDayBef"]);
                                //일증감수
                                //lbl24.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["lifeDay"]) - Convert.ToInt32(rows[0]["lifeDayBef"]));
                                //당월인원
                                //lbl31.Text = String.Format("{0:#,##0}", rows[0]["lifeMonth"]);
                                //전월인원
                                //lbl38.Text = String.Format("{0:#,##0}", rows[0]["lifeMonthBef"]);
                                //월증감수
                                //lbl45.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["lifeMonth"]) - Convert.ToInt32(rows[0]["lifeMonthBef"]));
                                //전월대비증감율
                                //n1 = Convert.ToDecimal(rows[0]["lifeMonth"]);
                                //n2 = Convert.ToDecimal(rows[0]["lifeMonthBef"]);
                                //lbl52.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";
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

        private void Open2()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;
                decimal n1 = 0; decimal n2 = 0;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GM_GM01_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = DateTime.Now.ToString("yyyyMMdd");

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = DateTime.Now.ToString("yyyyMMdd");

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow[] rows = dt.Select();

                                //-----------------------------------------------------------------------------------
                                //회원 정보현황
                                //-----------------------------------------------------------------------------------
                                //1.셰프이사
                                //현재회원수
                                lbl4.Text = String.Format("{0:#,##0}", rows[0]["ChefTotal"]);
                                //금일인원
                                lbl11.Text = String.Format("{0:#,##0}", rows[0]["ChefDay"]);
                                //전일인원
                                lbl18.Text = String.Format("{0:#,##0}", rows[0]["ChefDayBef"]);
                                //일증감수
                                lbl25.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["ChefDay"]) - Convert.ToInt32(rows[0]["ChefDayBef"]));
                                //당월인원
                                lbl32.Text = String.Format("{0:#,##0}", rows[0]["ChefMonth"]);
                                //전월인원
                                lbl39.Text = String.Format("{0:#,##0}", rows[0]["ChefMonthBef"]);
                                //월증감수
                                lbl46.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["ChefMonth"]) - Convert.ToInt32(rows[0]["ChefMonthBef"]));
                                //전월대비증감율
                                n1 = Convert.ToDecimal(rows[0]["ChefMonth"]);
                                n2 = Convert.ToDecimal(rows[0]["ChefMonthBef"]);
                                if (n2 > 0)
                                    lbl53.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";
                                else
                                    lbl53.Text = "0";

                                //2.도마셰프(PS운영자)
                                //현재회원수
                                lbl5.Text = String.Format("{0:#,##0}", rows[0]["PsTotal"]);
                                //금일인원
                                lbl12.Text = String.Format("{0:#,##0}", rows[0]["PsDay"]);
                                //전일인원
                                lbl19.Text = String.Format("{0:#,##0}", rows[0]["PsDayBef"]);
                                //일증감수
                                lbl26.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["PsDay"]) - Convert.ToInt32(rows[0]["PsDayBef"]));
                                //당월인원
                                lbl33.Text = String.Format("{0:#,##0}", rows[0]["PsMonth"]);
                                //전월인원
                                lbl40.Text = String.Format("{0:#,##0}", rows[0]["PsMonthBef"]);
                                //월증감수
                                lbl47.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["PsMonth"]) - Convert.ToInt32(rows[0]["PsMonthBef"]));
                                //전월대비증감율
                                n1 = Convert.ToDecimal(rows[0]["PsMonth"]);
                                n2 = Convert.ToDecimal(rows[0]["PsMonthBef"]);
                                if (n2 > 0)
                                    lbl54.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";
                                else
                                    lbl54.Text = "0";

                                //3.VIP
                                //현재회원수
                                lbl6.Text = String.Format("{0:#,##0}", rows[0]["VipTotal"]);
                                //금일인원
                                lbl13.Text = String.Format("{0:#,##0}", rows[0]["VipDay"]);
                                //전일인원
                                lbl20.Text = String.Format("{0:#,##0}", rows[0]["VipDayBef"]);
                                //일증감수
                                lbl27.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["VipDay"]) - Convert.ToInt32(rows[0]["VipDayBef"]));
                                //당월인원
                                lbl34.Text = String.Format("{0:#,##0}", rows[0]["VipMonth"]);
                                //전월인원
                                lbl41.Text = String.Format("{0:#,##0}", rows[0]["VipMonthBef"]);
                                //월증감수
                                lbl48.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["VipMonth"]) - Convert.ToInt32(rows[0]["VipMonthBef"]));
                                //전월대비증감율
                                n1 = Convert.ToDecimal(rows[0]["VipMonth"]);
                                n2 = Convert.ToDecimal(rows[0]["VipMonthBef"]);
                                if (n2 > 0)
                                    lbl55.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";
                                else
                                    lbl55.Text = "0";

                                //4.도마
                                //현재회원수
                                lbl7.Text = String.Format("{0:#,##0}", rows[0]["DomaTotal"]);
                                //금일인원
                                lbl14.Text = String.Format("{0:#,##0}", rows[0]["DomaDay"]);
                                //전일인원
                                lbl21.Text = String.Format("{0:#,##0}", rows[0]["DomaDayBef"]);
                                //일증감수
                                lbl28.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["DomaDay"]) - Convert.ToInt32(rows[0]["DomaDayBef"]));
                                //당월인원
                                lbl35.Text = String.Format("{0:#,##0}", rows[0]["DomaMonth"]);
                                //전월인원
                                lbl42.Text = String.Format("{0:#,##0}", rows[0]["DomaMonthBef"]);
                                //월증감수
                                lbl49.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["DomaMonth"]) - Convert.ToInt32(rows[0]["DomaMonthBef"]));
                                //전월대비증감율
                                n1 = Convert.ToDecimal(rows[0]["DomaMonth"]);
                                n2 = Convert.ToDecimal(rows[0]["DomaMonthBef"]);
                                if (n2 > 0)
                                    lbl56.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";
                                else
                                    lbl56.Text = "0";

                                //라이프가입회원수 계산
                                //현재회원수
                                lbl3.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["ChefTotal"]) + Convert.ToInt32(rows[0]["PsTotal"]) + Convert.ToInt32(rows[0]["VipTotal"]) + Convert.ToInt32(rows[0]["DomaTotal"]));
                                //금일인원
                                lbl10.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["ChefDay"]) + Convert.ToInt32(rows[0]["PsDay"]) + Convert.ToInt32(rows[0]["VipDay"]) + Convert.ToInt32(rows[0]["DomaDay"]));
                                //전일인원
                                lbl17.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["ChefDayBef"]) + Convert.ToInt32(rows[0]["PsDayBef"]) + Convert.ToInt32(rows[0]["VipDayBef"]) + Convert.ToInt32(rows[0]["DomaDayBef"]));
                                //일증감수
                                n1 = Convert.ToInt32(lbl10.Text.Replace(",",""));
                                n2 = Convert.ToInt32(lbl17.Text.Replace(",", ""));
                                lbl24.Text = String.Format("{0:#,##0}", n1 - n2);
                                //당월인원
                                lbl31.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["ChefMonth"]) + Convert.ToInt32(rows[0]["PsMonth"]) + Convert.ToInt32(rows[0]["VipMonth"]) + Convert.ToInt32(rows[0]["DomaMonth"]));
                                //전월인원
                                lbl38.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["ChefMonthBef"]) + Convert.ToInt32(rows[0]["PsMonthBef"]) + Convert.ToInt32(rows[0]["VipMonthBef"]) + Convert.ToInt32(rows[0]["DomaMonthBef"]));
                                //월증감수
                                n1 = Convert.ToInt32(lbl31.Text.Replace(",", ""));
                                n2 = Convert.ToInt32(lbl38.Text.Replace(",", ""));
                                lbl45.Text = String.Format("{0:#,##0}", n1 - n2);
                                //전월대비증감율
                                if (n2 > 0)
                                    lbl52.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";
                                else
                                    lbl52.Text = "0";

                                //5.도라MD
                                //현재회원수
                                lbl57.Text = String.Format("{0:#,##0}", rows[0]["MdTotal"]);
                                //금일인원
                                lbl59.Text = String.Format("{0:#,##0}", rows[0]["MdDay"]);
                                //전일인원
                                lbl61.Text = String.Format("{0:#,##0}", rows[0]["MdDayBef"]);
                                //일증감수
                                lbl63.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["MdDay"]) - Convert.ToInt32(rows[0]["MdDayBef"]));
                                //당월인원
                                lbl65.Text = String.Format("{0:#,##0}", rows[0]["MdMonth"]);
                                //전월인원
                                lbl67.Text = String.Format("{0:#,##0}", rows[0]["MdMonthBef"]);
                                //월증감수
                                lbl69.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["MdMonth"]) - Convert.ToInt32(rows[0]["MdMonthBef"]));
                                //전월대비증감율
                                n1 = Convert.ToDecimal(rows[0]["MdMonth"]);
                                n2 = Convert.ToDecimal(rows[0]["MdMonthBef"]);
                                if (n2 > 0)
                                    lbl71.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";
                                else
                                    lbl71.Text = "0";

                                //6.G멀티샵
                                //현재회원수
                                lbl58.Text = String.Format("{0:#,##0}", rows[0]["BizTotal"]);
                                //금일인원
                                lbl60.Text = String.Format("{0:#,##0}", rows[0]["BizDay"]);
                                //전일인원
                                lbl62.Text = String.Format("{0:#,##0}", rows[0]["BizDayBef"]);
                                //일증감수
                                lbl64.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["BizDay"]) - Convert.ToInt32(rows[0]["BizDayBef"]));
                                //당월인원
                                lbl66.Text = String.Format("{0:#,##0}", rows[0]["BizMonth"]);
                                //전월인원
                                lbl68.Text = String.Format("{0:#,##0}", rows[0]["BizMonthBef"]);
                                //월증감수
                                lbl70.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["BizMonth"]) - Convert.ToInt32(rows[0]["BizMonthBef"]));
                                //전월대비증감율
                                n1 = Convert.ToDecimal(rows[0]["BizMonth"]);
                                n2 = Convert.ToDecimal(rows[0]["BizMonthBef"]);
                                if (n2 > 0)
                                    lbl72.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";
                                else
                                    lbl72.Text = "0";

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

        private void Open3()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;
                int n1 = 0; int n2 = 0; int n3 = 0; int n4 = 0;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("telecom.USP_GM_GM01_SELECT_01", con))
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
                                //당일신청
                                lblTel9.Text = String.Format("{0:#,##0}", rows[0]["teld9"]);
                                lblTel10.Text = String.Format("{0:#,##0}", rows[0]["teld10"]);
                                lblTel11.Text = String.Format("{0:#,##0}", rows[0]["teld11"]);
                                lblTel12.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld9"]) + Convert.ToInt32(rows[0]["teld10"]) + Convert.ToInt32(rows[0]["teld11"]));
                                lblTel13.Text = String.Format("{0:#,##0}", rows[0]["teld13"]);
                                lblTel15.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld13"]) + 0);

                                //당일개통
                                lblTel17.Text = String.Format("{0:#,##0}", rows[0]["teld17"]);
                                lblTel18.Text = String.Format("{0:#,##0}", rows[0]["teld18"]);
                                lblTel19.Text = String.Format("{0:#,##0}", rows[0]["teld19"]);
                                lblTel20.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld17"]) + Convert.ToInt32(rows[0]["teld18"]) + Convert.ToInt32(rows[0]["teld19"]));
                                lblTel21.Text = String.Format("{0:#,##0}", rows[0]["teld21"]);
                                lblTel23.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld21"]) + 0);

                                //당일해지
                                lblTel25.Text = String.Format("{0:#,##0}", rows[0]["teld25"]);
                                lblTel26.Text = String.Format("{0:#,##0}", rows[0]["teld26"]);
                                lblTel27.Text = String.Format("{0:#,##0}", rows[0]["teld27"]);
                                lblTel28.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld25"]) + Convert.ToInt32(rows[0]["teld26"]) + Convert.ToInt32(rows[0]["teld27"]));
                                lblTel29.Text = String.Format("{0:#,##0}", rows[0]["teld29"]);
                                lblTel31.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld29"]) + 0);

                                //월누적개통
                                lblTel41.Text = String.Format("{0:#,##0}", rows[0]["teld41"]);
                                lblTel42.Text = String.Format("{0:#,##0}", rows[0]["teld42"]);
                                lblTel43.Text = String.Format("{0:#,##0}", rows[0]["teld43"]);
                                lblTel44.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld41"]) + Convert.ToInt32(rows[0]["teld42"]) + Convert.ToInt32(rows[0]["teld43"]));
                                lblTel45.Text = String.Format("{0:#,##0}", rows[0]["teld45"]);
                                lblTel47.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld45"]) + 0);

                                //월누적해지
                                lblTel49.Text = String.Format("{0:#,##0}", rows[0]["teld49"]);
                                lblTel50.Text = String.Format("{0:#,##0}", rows[0]["teld50"]);
                                lblTel51.Text = String.Format("{0:#,##0}", rows[0]["teld51"]);
                                lblTel52.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld49"]) + Convert.ToInt32(rows[0]["teld50"]) + Convert.ToInt32(rows[0]["teld51"]));
                                lblTel53.Text = String.Format("{0:#,##0}", rows[0]["teld53"]);
                                lblTel55.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["teld53"]) + 0);

                                //일증감수
                                n1 = Convert.ToInt32(rows[0]["teld17"]) - Convert.ToInt32(rows[0]["teld25"]);
                                n2 = Convert.ToInt32(rows[0]["teld18"]) - Convert.ToInt32(rows[0]["teld26"]);
                                n3 = Convert.ToInt32(rows[0]["teld19"]) - Convert.ToInt32(rows[0]["teld27"]);
                                n4 = Convert.ToInt32(rows[0]["teld21"]) - Convert.ToInt32(rows[0]["teld29"]);
                                lblTel33.Text = String.Format("{0:#,##0}", n1);
                                lblTel34.Text = String.Format("{0:#,##0}", n2);
                                lblTel35.Text = String.Format("{0:#,##0}", n3);
                                lblTel36.Text = String.Format("{0:#,##0}", n1 + n2 + n3);
                                lblTel37.Text = String.Format("{0:#,##0}", n4);
                                lblTel39.Text = String.Format("{0:#,##0}", n4 + 0);

                                //월증감수
                                n1 = Convert.ToInt32(rows[0]["teld41"]) - Convert.ToInt32(rows[0]["teld49"]);
                                n2 = Convert.ToInt32(rows[0]["teld42"]) - Convert.ToInt32(rows[0]["teld50"]);
                                n3 = Convert.ToInt32(rows[0]["teld43"]) - Convert.ToInt32(rows[0]["teld51"]);
                                n4 = Convert.ToInt32(rows[0]["teld45"]) - Convert.ToInt32(rows[0]["teld53"]);
                                lblTel57.Text = String.Format("{0:#,##0}", n1);
                                lblTel58.Text = String.Format("{0:#,##0}", n2);
                                lblTel59.Text = String.Format("{0:#,##0}", n3);
                                lblTel60.Text = String.Format("{0:#,##0}", n1 + n2 + n3);
                                lblTel61.Text = String.Format("{0:#,##0}", n4);
                                lblTel63.Text = String.Format("{0:#,##0}", n4 + 0);

                                //총 합계
                                lblTel8.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel4.Text.Replace(",", "")) + Convert.ToInt32(lblTel7.Text.Replace(",", "")));
                                lblTel16.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel12.Text.Replace(",", "")) + Convert.ToInt32(lblTel15.Text.Replace(",", "")));
                                lblTel24.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel20.Text.Replace(",", "")) + Convert.ToInt32(lblTel23.Text.Replace(",", "")));
                                lblTel32.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel28.Text.Replace(",", "")) + Convert.ToInt32(lblTel31.Text.Replace(",", "")));
                                lblTel40.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel36.Text.Replace(",", "")) + Convert.ToInt32(lblTel39.Text.Replace(",", "")));
                                lblTel48.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel44.Text.Replace(",", "")) + Convert.ToInt32(lblTel47.Text.Replace(",", "")));
                                lblTel56.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel52.Text.Replace(",", "")) + Convert.ToInt32(lblTel55.Text.Replace(",", "")));
                                lblTel64.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel60.Text.Replace(",", "")) + Convert.ToInt32(lblTel63.Text.Replace(",", "")));
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

        int nLG_CNT1 = 0;
        int nLG_CNT2 = 0;
        int nLG_CNT3 = 0;
        int nKT_CNT1 = 0;
        int nKT_CNT2 = 0;

        private void TotAddGet()
        {
            //텔레콤 충누적데이타 Select
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("telecom.USP_GM_GM01_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_idx", MySqlDbType.Int32, 15);
                        cmd.Parameters[0].Value = 1;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                nLG_CNT1 = Convert.ToInt32(dt.Rows[0]["lg_cnt1"]);
                                nLG_CNT2 = Convert.ToInt32(dt.Rows[0]["lg_cnt2"]);
                                nLG_CNT3 = Convert.ToInt32(dt.Rows[0]["lg_cnt3"]);

                                nKT_CNT1 = Convert.ToInt32(dt.Rows[0]["kt_cnt1"]);
                                nKT_CNT2 = Convert.ToInt32(dt.Rows[0]["kt_cnt2"]);
                            }
                            else
                            {
                                nLG_CNT1 = 0;
                                nLG_CNT2 = 0;
                                nLG_CNT3 = 0;

                                nKT_CNT1 = 0;
                                nKT_CNT2 = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void TotAdd()
        {
            int ncnt1 = 0;
            int ncnt2 = 0;
            int ncnt3 = 0;
            int ncnt4 = 0;
            int ncnt5 = 0;

            //총누적
            TotAddGet();

            ncnt1 = nLG_CNT1 + Convert.ToInt32(lblTel33.Text);
            ncnt2 = nLG_CNT2 + Convert.ToInt32(lblTel34.Text);
            ncnt3 = nLG_CNT3 + Convert.ToInt32(lblTel35.Text);
            ncnt4 = nKT_CNT1 + Convert.ToInt32(lblTel37.Text);
            ncnt5 = nKT_CNT2 + Convert.ToInt32(lblTel38.Text);

            lblTel1.Text = String.Format("{0:#,##0}", ncnt1);
            lblTel2.Text = String.Format("{0:#,##0}", ncnt2);
            lblTel3.Text = String.Format("{0:#,##0}", ncnt3);
            lblTel4.Text = String.Format("{0:#,##0}", ncnt1 + ncnt2 + ncnt3);

            lblTel5.Text = String.Format("{0:#,##0}", ncnt4);
            lblTel6.Text = String.Format("{0:#,##0}", ncnt5);
            lblTel7.Text = String.Format("{0:#,##0}", ncnt4 + ncnt5);
            lblTel8.Text = String.Format("{0:#,##0}", ncnt1 + ncnt2 + ncnt3 + ncnt4 + ncnt5);
            //계산된 누적 저장.
            //배치로 작성해야함(1일 한번)
            //TotSave(ncnt1, ncnt2, ncnt3, ncnt4, ncnt5);

        }

        private void TotSave(int ncnt1, int ncnt2, int ncnt3, int ncnt4, int ncnt5)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM01_SAVE_02", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_lg_cnt1", MySqlDbType.Int32, 15);
                        cmd.Parameters[0].Value = ncnt1;

                        cmd.Parameters.Add("i_lg_cnt2", MySqlDbType.Int32, 15);
                        cmd.Parameters[1].Value = ncnt2;

                        cmd.Parameters.Add("i_lg_cnt3", MySqlDbType.Int32, 15);
                        cmd.Parameters[2].Value = ncnt3;

                        cmd.Parameters.Add("i_kt_cnt1", MySqlDbType.Int32, 15);
                        cmd.Parameters[3].Value = ncnt4;

                        cmd.Parameters.Add("i_kt_cnt2", MySqlDbType.Int32, 15);
                        cmd.Parameters[4].Value = ncnt5;

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            popup = new frmGM01_Pop01();
            //popup.Owner = this;
            popup.pCOMPANYCD = "";
            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //if (popup.DialogResult == DialogResult.OK)
            //{
            //    this.txtX.EditValue = popup.nX;
            //    this.txtY.EditValue = popup.nY;
            //}

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }

        private void View1()
        {
            for (int i = 0; i < chartControl2.Series.Count; i++)
                this.chartControl2.Series[i].Points.Clear();

            DataTable dt = GetData2(); new DataTable();

            //Label값을 데이터의 값 그대로 나오도록 설정

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //SeriesPoint sPont = new SeriesPoint(dt.Rows[i]["NAME"].ToString(), Convert.ToInt16(dt.Rows[i]["Quantity"]));
                SeriesPoint sPont = new SeriesPoint(dt.Rows[i]["NAME"].ToString(), dt.Rows[i]["Quantity"].ToString());
                this.chartControl2.Series["Series 1"].Points.Add(sPont);
            }
        }

        private DataTable GetData2()
        {
            DataTable dt = new DataTable();

            //DataColumn colName = new DataColumn("NAME", typeof(string));
            //DataColumn colYear = new DataColumn("YEAR", typeof(string));
            //DataColumn colQuantity = new DataColumn("Quantity", typeof(int));
            DataColumn colName = new DataColumn("NAME", typeof(string));
            //DataColumn colQuantity = new DataColumn("Quantity", typeof(int));
            DataColumn colQuantity = new DataColumn("Quantity", typeof(string));

            dt.Columns.Add(colName);
            dt.Columns.Add(colQuantity);

            //dt.Rows.Add("셰프이사", "1,000");
            //dt.Rows.Add("도마셰프", "2,000");
            //dt.Rows.Add("VIP", "3,000");
            //dt.Rows.Add("도마", "4,000");

            dt.Rows.Add("셰프이사", lbl4.Text);
            dt.Rows.Add("도마셰프", lbl5.Text);
            dt.Rows.Add("VIP", lbl6.Text);
            dt.Rows.Add("도마", lbl7.Text);

            return dt;
        }

        private void View2()
        {
            for (int i = 0; i < chartControl1.Series.Count; i++)
                this.chartControl1.Series[i].Points.Clear();

            DataTable dt = GetData3(); new DataTable();

            //Label값을 데이터의 값 그대로 나오도록 설정

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //SeriesPoint sPont = new SeriesPoint(dt.Rows[i]["NAME"].ToString(), Convert.ToInt16(dt.Rows[i]["Quantity"]));
                SeriesPoint sPont = new SeriesPoint(dt.Rows[i]["NAME"].ToString(), dt.Rows[i]["Quantity"].ToString());
                this.chartControl1.Series["Series 1"].Points.Add(sPont);
            }
        }

        private DataTable GetData3()
        {
            DataTable dt = new DataTable();

            //DataColumn colName = new DataColumn("NAME", typeof(string));
            //DataColumn colYear = new DataColumn("YEAR", typeof(string));
            //DataColumn colQuantity = new DataColumn("Quantity", typeof(int));
            DataColumn colName = new DataColumn("NAME", typeof(string));
            //DataColumn colQuantity = new DataColumn("Quantity", typeof(int));
            DataColumn colQuantity = new DataColumn("Quantity", typeof(string));

            dt.Columns.Add(colName);
            dt.Columns.Add(colQuantity);

            //dt.Rows.Add("셰프이사", "1,000");
            //dt.Rows.Add("도마셰프", "2,000");
            //dt.Rows.Add("VIP", "3,000");
            //dt.Rows.Add("도마", "4,000");

            dt.Rows.Add("LG(일반)", lblTel1.Text);
            dt.Rows.Add("LG(우체국)", lblTel2.Text);
            dt.Rows.Add("LG(선불)", lblTel3.Text);
            dt.Rows.Add("KT(일반)", lblTel5.Text);

            return dt;
        }


        //private void EfwLabel12_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("dddddddddddddddd");
        //}
        private void EfwSimpleButton6_Click(object sender, EventArgs e)
        {
            popup1 = new frmGM01_Pop02();
            popup1.pMember_NM = "라이프 가입 회원수";
            popup1.pQ3 = "%";

            popup1.FormClosed += popup_FormClosed1;
            popup1.ShowDialog();
        }
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            popup1 = new frmGM01_Pop02();
            popup1.pMember_NM = "셰프(이사)";
            popup1.pQ3 = "3";

            popup1.FormClosed += popup_FormClosed1;
            popup1.ShowDialog();
        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            popup1 = new frmGM01_Pop02();
            popup1.pMember_NM = "도마셰프";
            popup1.pQ3 = "2";

            popup1.FormClosed += popup_FormClosed1;
            popup1.ShowDialog();
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            popup1 = new frmGM01_Pop02();
            popup1.pMember_NM = "VIP";
            popup1.pQ3 = "1";

            popup1.FormClosed += popup_FormClosed1;
            popup1.ShowDialog();
        }

        private void EfwSimpleButton3_Click(object sender, EventArgs e)
        {
            popup1 = new frmGM01_Pop02();
            popup1.pMember_NM = "도마";
            popup1.pQ3 = "0";

            popup1.FormClosed += popup_FormClosed1;
            popup1.ShowDialog();
        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup1.FormClosed -= popup_FormClosed1;
            popup1 = null;
        }

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            popup2 = new frmGM01_Pop03();
            popup2.pMember_NM = "LG U+";
            popup2.pQ3 = "lg";

            popup2.FormClosed += popup_FormClosed2;
            popup2.ShowDialog();
        }

        private void EfwSimpleButton5_Click(object sender, EventArgs e)
        {
            popup2 = new frmGM01_Pop03();
            popup2.pMember_NM = "KT";
            popup2.pQ3 = "kt";

            popup2.FormClosed += popup_FormClosed2;
            popup2.ShowDialog();
        }
        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup2.FormClosed -= popup_FormClosed2;
            popup2 = null;
        }


    }
}
