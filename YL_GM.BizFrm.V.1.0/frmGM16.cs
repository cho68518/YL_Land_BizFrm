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
    public partial class frmGM16 : FrmBase
    {
        public frmGM16()
        {
            InitializeComponent();
            this.QCode = "GM16";
            //폼명설정
            this.FrmName = "대리점별 개통현황";
        }


        private void frmGM16_Load(object sender, EventArgs e)
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
            dtE_DATE.EditValue = DateTime.Now;
        }

        public override void Search()
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
                                lblTel8.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel1.Text.Replace(",", "")) + Convert.ToInt32(lblTel2.Text.Replace(",", "")) + Convert.ToInt32(lblTel3.Text.Replace(",", "")) + Convert.ToInt32(lblTel3_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel3_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel5.Text.Replace(",", "")));
                                lblTel24.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel17.Text.Replace(",", "")) + Convert.ToInt32(lblTel18.Text.Replace(",", "")) + Convert.ToInt32(lblTel19.Text.Replace(",", "")) + Convert.ToInt32(lblTel19_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel19_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel21.Text.Replace(",", "")));
                                lblTel32.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel25.Text.Replace(",", "")) + Convert.ToInt32(lblTel26.Text.Replace(",", "")) + Convert.ToInt32(lblTel27.Text.Replace(",", "")) + Convert.ToInt32(lblTel27_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel27_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel29.Text.Replace(",", "")));
                                lblTel48.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel24.Text.Replace(",", "")) + Convert.ToInt32(lblTel32.Text.Replace(",", "")) + Convert.ToInt32(lblTel43.Text.Replace(",", "")) + Convert.ToInt32(lblTel43_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel43_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel45.Text.Replace(",", "")));
                                lblTel56.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel49.Text.Replace(",", "")) + Convert.ToInt32(lblTel50.Text.Replace(",", "")) + Convert.ToInt32(lblTel51.Text.Replace(",", "")) + Convert.ToInt32(lblTel51_1.Text.Replace(",", "")) + Convert.ToInt32(lblTel51_2.Text.Replace(",", "")) + Convert.ToInt32(lblTel53.Text.Replace(",", "")));
                                lblTel40.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel24.Text.Replace(",", "")) - Convert.ToInt32(lblTel32.Text.Replace(",", "")));
                                lblTel64.Text = String.Format("{0:#,##0}", Convert.ToInt32(lblTel48.Text.Replace(",", "")) - Convert.ToInt32(lblTel56.Text.Replace(",", "")));
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
            //Open1();
            Open2();
        }
        //private void Open1()
        //{
        //    try
        //    {
        //        string sCOMFIRM = string.Empty;
        //        string sCom = string.Empty;
        //        //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
        //        //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
        //        using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

        //        {
        //            using (MySqlCommand cmd = new MySqlCommand("telecom.USP_GM_GM16_SELECT_02", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 50);
        //                cmd.Parameters[0].Value = dtE_DATE.EditValue3.Substring(0, 4);

        //                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
        //                {
        //                    DataTable ds = new DataTable();
        //                    sda.Fill(ds);
        //                    efwGridControl1.DataBind(ds);
        //                    //  this.efwGridControl2.MyGridView.BestFitColumns();

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
        //    }
        //}

        private void Open2()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;
                int n1 = 0; int n2 = 0; int n3 = 0; int n4 = 0; int n3_1 = 0; int n3_2 = 0;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("telecom.USP_GM_GM16_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = DateTime.Now.ToString("yyyy");


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
                                bl1.Text = String.Format("{0:#,##0}", rows[0]["Agency1"]);
                                bl2.Text = String.Format("{0:#,##0}", rows[0]["Agency2"]);
                                bl3.Text = String.Format("{0:#,##0}", rows[0]["Agency3"]);
                                bl4.Text = String.Format("{0:#,##0}", rows[0]["Agency4"]);
                                bl5.Text = String.Format("{0:#,##0}", rows[0]["Agency5"]);
                                bl6.Text = String.Format("{0:#,##0}", rows[0]["Agency6"]);
                                bl7.Text = String.Format("{0:#,##0}", rows[0]["Agency7"]);
                                bl8.Text = String.Format("{0:#,##0}", rows[0]["Agency8"]);
                                bl9.Text = String.Format("{0:#,##0}", rows[0]["Agency9"]);
                                bl10.Text = String.Format("{0:#,##0}", rows[0]["Agency10"]);
                                bl11.Text = String.Format("{0:#,##0}", rows[0]["Agency11"]);
                                bl12.Text = String.Format("{0:#,##0}", rows[0]["Agency12"]);
                                bl13.Text = String.Format("{0:#,##0}", rows[0]["Agency13"]);
                                bl14.Text = String.Format("{0:#,##0}", rows[0]["Agency14"]);
                                bl15.Text = String.Format("{0:#,##0}", rows[0]["Agency15"]);
                                bl16.Text = String.Format("{0:#,##0}", rows[0]["Agency16"]);
                                bl17.Text = String.Format("{0:#,##0}", rows[0]["Agency17"]);
                                bl18.Text = String.Format("{0:#,##0}", rows[0]["Agency18"]);
                                bl19.Text = String.Format("{0:#,##0}", rows[0]["Agency19"]);
                                bl20.Text = String.Format("{0:#,##0}", rows[0]["Agency20"]);
                                bl21.Text = String.Format("{0:#,##0}", rows[0]["Agency21"]);
                                bl22.Text = String.Format("{0:#,##0}", rows[0]["Agency22"]);
                                bl23.Text = String.Format("{0:#,##0}", rows[0]["Agency23"]);
                                bl24.Text = String.Format("{0:#,##0}", rows[0]["Agency24"]);
                                //
                                bl1_1.Text = String.Format("{0:#,##0}", rows[0]["Tel1"]);
                                bl2_1.Text = String.Format("{0:#,##0}", rows[0]["Tel2"]);
                                bl3_1.Text = String.Format("{0:#,##0}", rows[0]["Tel3"]);
                                bl4_1.Text = String.Format("{0:#,##0}", rows[0]["Tel4"]);
                                bl5_1.Text = String.Format("{0:#,##0}", rows[0]["Tel5"]);
                                bl6_1.Text = String.Format("{0:#,##0}", rows[0]["Tel6"]);
                                bl7_1.Text = String.Format("{0:#,##0}", rows[0]["Tel7"]);
                                bl8_1.Text = String.Format("{0:#,##0}", rows[0]["Tel8"]);
                                bl9_1.Text = String.Format("{0:#,##0}", rows[0]["Tel9"]);
                                bl10_1.Text = String.Format("{0:#,##0}", rows[0]["Tel10"]);
                                bl11_1.Text = String.Format("{0:#,##0}", rows[0]["Tel11"]);
                                bl12_1.Text = String.Format("{0:#,##0}", rows[0]["Tel12"]);
                                bl13_1.Text = String.Format("{0:#,##0}", rows[0]["Tel13"]);
                                bl14_1.Text = String.Format("{0:#,##0}", rows[0]["Tel14"]);
                                bl15_1.Text = String.Format("{0:#,##0}", rows[0]["Tel15"]);
                                bl16_1.Text = String.Format("{0:#,##0}", rows[0]["Tel16"]);
                                bl17_1.Text = String.Format("{0:#,##0}", rows[0]["Tel17"]);
                                bl18_1.Text = String.Format("{0:#,##0}", rows[0]["Tel18"]);
                                bl19_1.Text = String.Format("{0:#,##0}", rows[0]["Tel19"]);
                                bl20_1.Text = String.Format("{0:#,##0}", rows[0]["Tel20"]);
                                bl21_1.Text = String.Format("{0:#,##0}", rows[0]["Tel21"]);
                                bl22_1.Text = String.Format("{0:#,##0}", rows[0]["Tel22"]);
                                bl23_1.Text = String.Format("{0:#,##0}", rows[0]["Tel23"]);
                                bl24_1.Text = String.Format("{0:#,##0}", rows[0]["Tel24"]);
                                //
                                bl1_2.Text = String.Format("{0:#,##0}", rows[0]["Del1"]);
                                bl2_2.Text = String.Format("{0:#,##0}", rows[0]["Del2"]);
                                bl3_2.Text = String.Format("{0:#,##0}", rows[0]["Del3"]);
                                bl4_2.Text = String.Format("{0:#,##0}", rows[0]["Del4"]);
                                bl5_2.Text = String.Format("{0:#,##0}", rows[0]["Del5"]);
                                bl6_2.Text = String.Format("{0:#,##0}", rows[0]["Del6"]);
                                bl7_2.Text = String.Format("{0:#,##0}", rows[0]["Del7"]);
                                bl8_2.Text = String.Format("{0:#,##0}", rows[0]["Del8"]);
                                bl9_2.Text = String.Format("{0:#,##0}", rows[0]["Del9"]);
                                bl10_2.Text = String.Format("{0:#,##0}", rows[0]["Del10"]);
                                bl11_2.Text = String.Format("{0:#,##0}", rows[0]["Del11"]);
                                bl12_2.Text = String.Format("{0:#,##0}", rows[0]["Del12"]);
                                bl13_2.Text = String.Format("{0:#,##0}", rows[0]["Del13"]);
                                bl14_2.Text = String.Format("{0:#,##0}", rows[0]["Del14"]);
                                bl15_2.Text = String.Format("{0:#,##0}", rows[0]["Del15"]);
                                bl16_2.Text = String.Format("{0:#,##0}", rows[0]["Del16"]);
                                bl17_2.Text = String.Format("{0:#,##0}", rows[0]["Del17"]);
                                bl18_2.Text = String.Format("{0:#,##0}", rows[0]["Del18"]);
                                bl19_2.Text = String.Format("{0:#,##0}", rows[0]["Del19"]);
                                bl20_2.Text = String.Format("{0:#,##0}", rows[0]["Del20"]);
                                bl21_2.Text = String.Format("{0:#,##0}", rows[0]["Del21"]);
                                bl22_2.Text = String.Format("{0:#,##0}", rows[0]["Del22"]);
                                bl23_2.Text = String.Format("{0:#,##0}", rows[0]["Del23"]);
                                bl24_2.Text = String.Format("{0:#,##0}", rows[0]["Del24"]);

                                bl1_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl1_1.Text.Replace(",", "")) - Convert.ToInt32(bl1_2.Text.Replace(",", "")));
                                bl2_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl2_1.Text.Replace(",", "")) - Convert.ToInt32(bl2_2.Text.Replace(",", "")));
                                bl3_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl3_1.Text.Replace(",", "")) - Convert.ToInt32(bl3_2.Text.Replace(",", "")));
                                bl4_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl4_1.Text.Replace(",", "")) - Convert.ToInt32(bl4_2.Text.Replace(",", "")));
                                bl5_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl5_1.Text.Replace(",", "")) - Convert.ToInt32(bl5_2.Text.Replace(",", "")));
                                bl6_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl6_1.Text.Replace(",", "")) - Convert.ToInt32(bl6_2.Text.Replace(",", "")));
                                bl7_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl7_1.Text.Replace(",", "")) - Convert.ToInt32(bl7_2.Text.Replace(",", "")));
                                bl8_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl8_1.Text.Replace(",", "")) - Convert.ToInt32(bl8_2.Text.Replace(",", "")));
                                bl9_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl9_1.Text.Replace(",", "")) - Convert.ToInt32(bl9_2.Text.Replace(",", "")));
                                bl10_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl10_1.Text.Replace(",", "")) - Convert.ToInt32(bl10_2.Text.Replace(",", "")));
                                bl11_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl11_1.Text.Replace(",", "")) - Convert.ToInt32(bl11_2.Text.Replace(",", "")));
                                bl12_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl12_1.Text.Replace(",", "")) - Convert.ToInt32(bl12_2.Text.Replace(",", "")));
                                bl13_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl13_1.Text.Replace(",", "")) - Convert.ToInt32(bl13_2.Text.Replace(",", "")));
                                bl14_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl14_1.Text.Replace(",", "")) - Convert.ToInt32(bl14_2.Text.Replace(",", "")));
                                bl15_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl15_1.Text.Replace(",", "")) - Convert.ToInt32(bl15_2.Text.Replace(",", "")));
                                bl16_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl16_1.Text.Replace(",", "")) - Convert.ToInt32(bl16_2.Text.Replace(",", "")));
                                bl17_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl17_1.Text.Replace(",", "")) - Convert.ToInt32(bl17_2.Text.Replace(",", "")));
                                bl18_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl18_1.Text.Replace(",", "")) - Convert.ToInt32(bl18_2.Text.Replace(",", "")));
                                bl19_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl19_1.Text.Replace(",", "")) - Convert.ToInt32(bl19_2.Text.Replace(",", "")));
                                bl20_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl20_1.Text.Replace(",", "")) - Convert.ToInt32(bl20_2.Text.Replace(",", "")));
                                bl21_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl21_1.Text.Replace(",", "")) - Convert.ToInt32(bl21_2.Text.Replace(",", "")));
                                bl22_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl22_1.Text.Replace(",", "")) - Convert.ToInt32(bl22_2.Text.Replace(",", "")));
                                bl23_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl23_1.Text.Replace(",", "")) - Convert.ToInt32(bl23_2.Text.Replace(",", "")));
                                bl24_3.Text = String.Format("{0:#,##0}", Convert.ToInt32(bl24_1.Text.Replace(",", "")) - Convert.ToInt32(bl24_2.Text.Replace(",", "")));


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
