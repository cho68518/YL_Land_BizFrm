using DevExpress.XtraGrid.Columns;
using Easy.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using MySql.Data.MySqlClient;
using DevExpress.XtraGrid.Views.Grid;

namespace YL_DONUT.BizFrm
{
    public partial class frmTest03 : FrmBase
    {
        public frmTest03()
        {
            InitializeComponent();
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Pumpkin");

            //This set the style to use skin technology
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;

            //Here we specify the skin to use by its name           
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = false;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            this.lblSec.Font = new Font(this.lblSec.Font, FontStyle.Bold);

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
        }





        #endregion

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            row_clear(gridView3);
            row_clear(gridView4);
            row_clear(gridView5);
            row_clear(gridView6);
            row_clear(gridView7);
            row_clear(gridView8);
            row_clear(gridView11);
            row_clear(gridView10);
            row_clear(gridView9);
            efwMemoEdit1.EditValue = null;
            OpenOrder();  // 주문내역 조회
            Cursor.Current = Cursors.Default;
        }

        private void BasicOpen()
        {
            //if (txtLogin_Id.EditValue == null)
            //{
            //    return;
            //}

            //Dictionary<string, string> myRecord;

            //using (MySQLConn sql = new MySQLConn(ConstantLib.Dev_Conn))
            //{
            //    sql.Query = " SELECT t1.u_nickname, t1.u_name, t1.login_id as ID, t1.u_chef_level as 등급, t1.u_id U_ID " +
            //                " FROM domalife.member_master t1 " +
            //                " WHERE t1.login_id = @login_id LIMIT 1";

            //    sql.addParam("@login_id", txtLogin_Id.EditValue);

            //    myRecord = sql.selectQueryForSingleRecord();

            //    for (int i = 0; i < myRecord.Count; i++)
            //    {
            //        txt_U_NickName.EditValue = myRecord["u_nickname"];
            //    }
            //}
        }

        private void OpenRel1(string uid)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = " select t3.u_nickname as 닉네임, t3.u_name as 이름, t3.login_id as ID, t3.u_chef_level as 등급, t3.u_id U_ID, t3.is_biz " +
                            "  FROM domalife.member_master  t1 " +
                            "      left join domalife.member_relations t2 on t2.recv_id = t1.u_id and t2.is_use = 'Y' " +
                            "      left join domalife.member_master t3 on t3.u_id = t2.send_id " +
                            " WHERE t1.u_id = '" + uid + "'";

                DataSet ds = sql.selectQueryDataSet();

                efwGridControl3.DataBind(ds);
                this.efwGridControl3.MyGridView.BestFitColumns();
            }
        }

        private void OpenRel2(string uid)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = " select t3.u_nickname as 닉네임, t3.u_name as 이름, t3.login_id as ID, t3.u_chef_level as 등급, t3.u_id U_ID " +
                            "  FROM domalife.member_master  t1 " +
                            "      left join domalife.member_vip_relations t2 on t2.recv_id = t1.u_id and t2.is_use = 'Y' " +
                            "      left join domalife.member_master t3 on t3.u_id = t2.send_id " +
                            " WHERE t1.u_id = '" + uid + "'";

                DataSet ds = sql.selectQueryDataSet();

                efwGridControl4.DataBind(ds);
                this.efwGridControl4.MyGridView.BestFitColumns();
            }
        }

        private void OpenRel3(string uid)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = " select t3.u_nickname as 닉네임, t3.u_name as 이름, t3.login_id as ID, t3.u_chef_level as 등급, t3.u_id U_ID, t3.is_biz " +
                            "  FROM domalife.member_master  t1 " +
                            "      left join domamall.tb_doma_chef_relations t2 on t2.user_id = t1.u_id and t2.is_use = 'Y' " +
                            "      left join domalife.member_master t3 on t3.u_id = t2.chef_id " +
                            " WHERE t1.u_id = '" + uid + "'";

                DataSet ds = sql.selectQueryDataSet();

                efwGridControl5.DataBind(ds);
                this.efwGridControl5.MyGridView.BestFitColumns();
            }
        }

        private void OpenOrder()
        {
            txtO_Code.EditValue = null;
            txtO_Type.EditValue = null;

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = " SELECT DATE_FORMAT(t1.o_date,'%Y-%m-%d %H:%i:%S') as o_date, ";
                sql.Query += "        t1.o_code, ";
                sql.Query += "        t200.o_mcode, ";
                sql.Query += "        t1.o_type, ";
                sql.Query += "        case t1.o_type when 'T' then '임시저장' ";
                sql.Query += "                       when 'O' then '주문완료' ";
                sql.Query += "                       when 'P' then '상품준비중' ";
                sql.Query += "                       when 'D' then '배송중' ";
                sql.Query += "                       when 'F' then '배송완료' ";
                sql.Query += "                       when 'E' then '구매완료' ";
                sql.Query += "                       when 'C' then '구매취소' ";
                sql.Query += "                       when 'X' then '주문실패' ";
                sql.Query += "                       when 'I' then '결제진행중' ";
                sql.Query += "                       when 'Z' then '품절취소' ";
                sql.Query += "                       when 'A' then '반품처리중' ";
                sql.Query += "                       when 'B' then '반품완료'  ";
                sql.Query += "        end as 주문상태,  ";
                sql.Query += "        t1.u_id, t1.u_name, t1.u_nickname, t1.login_id, t1.u_chef_level, ";
                sql.Query += "        t1.is_biz as G멀티샵, t1.g_prod as G제품, ";
                sql.Query += "        IFNULL(t100.experience_shop,'N') as 체험샵, ";
                sql.Query += "        t1.p_id, t1.option_id, ";
                sql.Query += "        t1.product_name, t1.option_name, t1.p_num as 수량, ";
                sql.Query += "        t200.o_total_cost as 총금액, t200.o_donut_d_cost as d머니, t200.o_donut_s_cost SD머니, t200.o_donut_g_cost as GD머니,";
                sql.Query += "        t200.o_delivery_cost as 배송료, t200.o_purchase_cost as 총결제금액,";
                sql.Query += "        case t200.o_pay_type when '1' then '카드' ";
                sql.Query += "                             when '3' then '가상계좌' end as 결제구분,";

                sql.Query += "        domabiz.func_221_story_money_type(t1.g_prod) as PS축하, ";
                sql.Query += "        domabiz.func_221_story_money(t1.o_code) as PS축하금액 ";
                sql.Query += "  FROM domabiz.vw_order_product_info t1 ";
                sql.Query += "       left join  domabiz.gshop_master t100 on t100.u_id = t1.u_id ";
                sql.Query += "       left join  domamall.tb_am_product_orders t200 on t200.o_code = t1.o_code ";
                sql.Query += " WHERE t1.login_id like '" + txtLogin_Id.EditValue + "%' ";
                sql.Query += "   AND date_format(t1.o_date, '%Y%m%d') between '" + dtS_DATE.EditValue3 + "' AND '" + dtE_DATE.EditValue3 + "' ";

                if (chk1.Checked == true)
                   sql.Query += "   AND IFNULL(t100.experience_shop,'N') = 'Y' ";

                if (chk2.Checked == true)
                    sql.Query += "   AND t1.o_type IN('O', 'P', 'D', 'E', 'F') ";

                sql.Query += " ORDER BY t1.o_date desc ";

                DataSet ds = sql.selectQueryDataSet();

                efwGridControl1.DataBind(ds);

            }
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            efwMemoEdit1.EditValue = null;

            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            txtO_Code.EditValue = dr["o_code"].ToString();
            txtO_Type.EditValue = dr["o_type"].ToString();

            OpenRel1(dr["u_id"].ToString());  // 일반추천인
            OpenRel2(dr["u_id"].ToString());  // VIP추천인
            OpenRel3(dr["u_id"].ToString());  // 담당Biz

            if (dr["u_chef_level"].ToString() == "0")
            {
                //일반도마 주문시
                if (dr["G제품"].ToString() == "G")
                {
                    //G제품주문
                    //PS후기스토리   : 본인
                    //알뜰지원스토리 : 본인
                    //PS축하스토리   : 일반추천인 
                    string st = "■ 일반도마 -> G제품주문 \r\n";
                    st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                    st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                    st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

                    efwMemoEdit1.EditValue = st;

                    StoryOpen(dr["o_code"].ToString());
                }
                else
                {
                    //일반제품 주문
                    //PS후기스토리 : 본인
                    //알뜰지원스토리 : 본인
                    //PS축하스토리 : 일반추천인 
                    string st = "■ 일반도마 -> 일반제품주문 \r\n";
                    st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                    st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                    st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

                    efwMemoEdit1.EditValue = st;

                    StoryOpen(dr["o_code"].ToString());
                }
            }
            else if (dr["u_chef_level"].ToString() == "1")
            {
                //VIP 주문시
                if (dr["G제품"].ToString() == "G")
                {
                    //G제품주문
                    //PS후기스토리   : 본인
                    //알뜰지원스토리 : 본인
                    //PS축하스토리   : 일반추천인 
                    //GV축하스토리   : VIP추천인 
                    string st = "■ VIP -> G제품주문 \r\n";
                    st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                    st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                    st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
                    st += "   - GV축하스토리   : VIP추천인 (" + gridView4.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
                    st += "    \r\n";
                    st += "   ※ 일반추천인과 VIP추천인이 같은경우 GV축하스토리 생성됨. \r\n";

                    efwMemoEdit1.EditValue = st;

                    StoryOpen(dr["o_code"].ToString());
                }
                else
                {
                    //일반제품 주문
                    //PS후기스토리 : 본인
                    //알뜰지원스토리 : 본인
                    //PS축하스토리 : 일반추천인 
                    string st = "■ VIP -> 일반제품주문 \r\n";
                    st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                    st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                    st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

                    efwMemoEdit1.EditValue = st;

                    StoryOpen(dr["o_code"].ToString());
                }
            }
            else if (dr["u_chef_level"].ToString() == "2" || dr["u_chef_level"].ToString() == "3")
            {
                if (dr["G멀티샵"].ToString() == "Y")
                {
                    //G멀티샵 주문시
                    if (dr["G제품"].ToString() == "G")
                    {
                        //G제품주문
                        //PS후기스토리   : 본인
                        //알뜰지원스토리 : 본인
                        //PS축하스토리   : 일반추천인 
                        //GM축하스토리   : 담당Biz(담당MD, 담당베스트샵)추천인 
                        string st = "■ G멀티샵 -> G제품주문 \r\n";
                        st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                        st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                        st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
                        st += "   - GM축하스토리   : 담당Biz추천인 (" + gridView5.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
                        st += "    \r\n";
                        st += "   ※ 일반추천인과 담당Biz추천인이 같은경우 GM축하스토리 생성됨. \r\n";

                        efwMemoEdit1.EditValue = st;

                        StoryOpen(dr["o_code"].ToString());
                    }
                    else
                    {
                        //일반제품 주문
                        //PS후기스토리   : 본인
                        //알뜰지원스토리 : 본인
                        //PS축하스토리   : 일반추천인 
                        string st = "■ G멀티샵 -> 일반제품주문 \r\n";
                        st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                        st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                        st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

                        efwMemoEdit1.EditValue = st;

                        StoryOpen(dr["o_code"].ToString());
                    }
                } 
                else
                {
                    if (dr["G제품"].ToString() == "G")
                    {
                        //G제품주문
                        //PS후기스토리   : 본인
                        //알뜰지원스토리 : 본인
                        //PS축하스토리   : 일반추천인 
                        //GB축하스토리   : 담당Biz추천인 
                        string st = "■ Biz -> G제품주문 \r\n";
                        st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                        st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                        st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
                        st += "   - GB축하스토리   : 담당Biz추천인 (" + gridView5.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
                        st += "    \r\n";
                        st += "   ※ 일반추천인과 담당Biz추천인이 같은경우 GB축하스토리 생성됨. \r\n";

                        efwMemoEdit1.EditValue = st;

                        StoryOpen(dr["o_code"].ToString());
                    }
                    else
                    {
                        //일반제품 주문
                        //PS후기스토리   : 본인
                        //알뜰지원스토리 : 본인
                        //PS축하스토리   : 일반추천인 
                        string st = "■ Biz -> 일반제품주문 \r\n";
                        st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                        st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
                        st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

                        efwMemoEdit1.EditValue = st;

                        StoryOpen(dr["o_code"].ToString());
                    }
                }
            }
        }

        private void StoryOpen(string s_o_code)
        {
            efwGridControl6.DataBind(StoryOpen2(s_o_code, "208"));  // PS후기스토리
            efwGridControl7.DataBind(StoryOpen2(s_o_code, "232")) ; // 알뜰지원스토리
            efwGridControl8.DataBind(StoryOpen2(s_o_code, "221"));  // PS축하스토리
            efwGridControl11.DataBind(StoryOpen2(s_o_code, "247")); // GV축하스토리
            efwGridControl10.DataBind(StoryOpen2(s_o_code, "242")); // GB축하스토리
            efwGridControl9.DataBind(StoryOpen2(s_o_code, "244"));  // GM축하스토리

            this.efwGridControl6.MyGridView.BestFitColumns();
            this.efwGridControl7.MyGridView.BestFitColumns();
            this.efwGridControl8.MyGridView.BestFitColumns();
            this.efwGridControl11.MyGridView.BestFitColumns();
            this.efwGridControl10.MyGridView.BestFitColumns();
            this.efwGridControl9.MyGridView.BestFitColumns();
        }

        private void StoryOpen1(string s_o_code, string story_cd)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = " select o_code, user_nickname, user_name, login_id, is_write, story_id, DATE_FORMAT(write_date,'%Y-%m-%d %H:%i:%S') as write_date, user_id, " +
                            "        money_type, money, money_fix " +
                            "  FROM domalife.story_donut_banks t1 " +
                            " WHERE t1.contents_type = '" + story_cd + "'" +
                            "   AND t1.o_code = '" + s_o_code + "'" +
                            " Order By reg_date DESC ";

                DataSet ds = sql.selectQueryDataSet();

                efwGridControl6.DataBind(ds);
            }
        }

        private DataSet StoryOpen2(string s_o_code, string story_cd)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = sql.Query = " select o_code, user_nickname, user_name, login_id, is_write, story_id, DATE_FORMAT(write_date,'%Y-%m-%d %H:%i:%S') as write_date, user_id, " +
                                        "        money_type, money, money_fix " +
                                        "  FROM domalife.story_donut_banks t1 " +
                                        " WHERE t1.contents_type = '" + story_cd + "'" +
                                        "   AND t1.o_code = '" + s_o_code + "'" +
                                        " Order By reg_date DESC ";

                DataSet ds = sql.selectQueryDataSet();

                return ds;
                
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (txtO_Code.EditValue == null)
            {
                return;
            }

            if (txtO_Type.EditValue.ToString() != "T")
            {
                MessageBox.Show("임시 주문건만 완료처리할 수 있습니다!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "주문완료 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Real);
                    connection.Open();

                    MySqlCommand updateCommand = new MySqlCommand();
                    updateCommand.Connection = connection;

                    updateCommand.CommandText = "UPDATE domamall.tb_am_product_orders " +
                                                "   SET o_type='O', " +
                                                "       lgd_response_msg='결제성공', " +
                                                "       lgd_casflag='I', " +
                                                "       o_deposit_confirm_date=NOW() " +
                                                "   WHERE o_code=@o_code ";

                    updateCommand.Parameters.Add("@o_code", MySqlDbType.VarChar, 50);
                    updateCommand.Parameters[0].Value = txtO_Code.EditValue;

                    updateCommand.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                finally
                {
                    MessageBox.Show("처리 되었습니다!");
                }
            }
        }

        private void gridView6_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                //e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView7_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                //e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView8_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                //e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView11_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                //e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView10_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                //e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView9_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                //e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView3.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView4.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView5.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView6.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView7.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView8.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView11.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView10.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView9.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void gridView3_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView4_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView5_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string stype = View.GetRowCellDisplayText(e.RowHandle, View.Columns["o_type"]);
                if (stype == "O" || stype == "P" || stype == "D" || stype == "F" || stype == "E")
                {
                    //e.Appearance.BackColor = Color.OrangeRed;
                    //e.Appearance.BackColor2 = Color.White;
                    e.Appearance.BackColor = Color.LightGreen;
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }

        private int a = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            a++;
            //lblSec.Text = a + "초 경과";
            lblSec.Text = a.ToString();

            if (a == Convert.ToInt16(txtNum.EditValue))
            {
                efwSimpleButton7_Click(null, null);
                a = 0;
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            a = 0;
            lblSec.Text = "0";
            timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            lblSec.Text = "";
            timer1.Stop();
        }

        private void row_clear(GridView sView)
        {
            for (int i = 0; i < sView.RowCount;)
                sView.DeleteRow(i);
        }
    }
}
