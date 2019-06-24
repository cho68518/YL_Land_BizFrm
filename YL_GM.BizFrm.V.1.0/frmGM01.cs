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

namespace YL_GM.BizFrm
{
    public partial class frmGM01 : FrmBase
    {
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

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

    
        }

        #endregion

        public override void Search()
        {
            Open1();
            Open2();
        }

        private void Open1()
        {
            try
            {
                Dictionary<string, int> myRecord;
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
                                //1.총회원수
                                //현재회원수
                                lbl1.Text = String.Format("{0:#,##0}", rows[0]["lifeportalTotal"]);
                                lbl8.Text = String.Format("{0:#,##0}", rows[0]["lifeportalDay"]);
                                lbl15.Text = String.Format("{0:#,##0}", rows[0]["lifeportalDayBef"]);
                                lbl22.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["lifeportalDay"]) - Convert.ToInt32(rows[0]["lifeportalDayBef"]));

                                lbl29.Text = String.Format("{0:#,##0}", rows[0]["lifeportalMonth"]);
                                lbl36.Text = String.Format("{0:#,##0}", rows[0]["lifeportalMonthBef"]);
                                lbl43.Text = String.Format("{0:#,##0}", Convert.ToInt32(rows[0]["lifeportalMonth"]) - Convert.ToInt32(rows[0]["lifeportalMonthBef"]));
                                n1 = Convert.ToDecimal(rows[0]["lifeportalMonth"]);
                                n2 = Convert.ToDecimal(rows[0]["lifeportalMonthBef"]);
                                lbl50.Text = String.Format("{0:#,##0.00}", ((n1 - n2) / n2) * 100) + "%";
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
                Dictionary<string, int> myRecord;
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






    }
}
