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
    public partial class frmTest04 : FrmBase
    {
        public frmTest04()
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

            gridView1.OptionsView.ColumnAutoWidth = true;
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
            row_clear(gridView11);
            row_clear(gridView10);
            efwMemoEdit1.EditValue = null;
            Search1();
            Cursor.Current = Cursors.Default;
        }

        private void Search1()
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = " SELECT DATE_FORMAT(t1.create_date,'%Y-%m-%d %H:%i:%S') as 가입일, ";
                sql.Query += "        t1.gshop_name, t1.u_nickname, t1.login_id, t1.u_id, t100.u_chef_level as 등급, ";
                sql.Query += "        t1.ceo_name, ";
                sql.Query += "        t1.register_no, t1.tel_no, t1.hp_no, ";
                sql.Query += "        t1.md_u_id as 담당MD, t200.u_nickname as 담당MD닉네임, t200.u_name as 담당MD이름, t200.u_chef_level as 등급,";
                sql.Query += "        is_pay, is_approval ";
                sql.Query += "  FROM domabiz.gshop_master t1 ";
                sql.Query += "       left join  domalife.member_master t100 on t100.u_id = t1.u_id ";
                sql.Query += "       left join  domalife.member_master t200 on t200.u_id = t1.md_u_id ";
                sql.Query += " WHERE t100.u_nickname like '%" + txt1.EditValue + "%' ";
                sql.Query += "   AND date_format(t1.create_date, '%Y%m%d') between '" + dtS_DATE.EditValue3 + "' AND '" + dtE_DATE.EditValue3 + "' ";
                sql.Query += "   AND experience_shop = 'Y' ";
                sql.Query += " ORDER BY t1.create_date desc ";


                DataSet ds = sql.selectQueryDataSet();

                efwGridControl1.DataBind(ds);

            }
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            efwMemoEdit1.EditValue = null;

            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr is null)
                return;

            //txtO_Code.EditValue = dr["o_code"].ToString();
            //txtO_Type.EditValue = dr["o_type"].ToString();

            OpenRel1(dr["u_id"].ToString());  // 일반추천인
            OpenRel2(dr["u_id"].ToString());  // VIP추천인
            OpenRel3(dr["u_id"].ToString());  // 담당Biz

            //G제품주문
            //PS후기스토리   : 본인
            //알뜰지원스토리 : 본인
            //PS축하스토리   : 일반추천인 
            string st = "■ 뉴G멀티샵가입 \r\n";
            st += "   - 도마오픈스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            st += "   - 도마추천스토리 : 담당MD (" + gridView5.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
            st += "   - PR등록스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            st += "   - PR추천스토리   : 담당MD (" + gridView5.GetRowCellValue(0, "닉네임").ToString() + ") \r\n\r\n";
            st += "   ※ 담당MD가 G멀티샵인경우 일반추천인에게 PR추천스토리를 생성한다. 일반추천인이 G멀티샵인 경우는 생성되지 않는다.\r\n\r\n";
            if (gridView5.GetRowCellValue(0, "is_biz").ToString() == "Y")
            {
                if (gridView3.GetRowCellValue(0, "is_biz").ToString() == "Y")
                    st += "   ■■■ 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") G멀티샵이므로 PR추천스토리는 생성되지 않는다.";
                else
                    st += "   ■■■ 담당MD (" + gridView5.GetRowCellValue(0, "닉네임").ToString() + ") G멀티샵이므로 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") 에게 PR추천스토리 생성.";
            }
            else
            {
                st += "   ■■■ 담당MD (" + gridView5.GetRowCellValue(0, "닉네임").ToString() + ") G멀티샵 아니므로 담당MD(" + gridView5.GetRowCellValue(0, "닉네임").ToString() + ") 에게 PR추천스토리 생성.";
            }
            

            efwMemoEdit1.EditValue = st;

            StoryOpen(dr["u_id"].ToString(), gridView5.GetRowCellValue(0, "U_ID").ToString());


            //if (dr["u_chef_level"].ToString() == "0")
            //{
            //    //일반도마 주문시
            //    if (dr["G제품"].ToString() == "G")
            //    {
            //        //G제품주문
            //        //PS후기스토리   : 본인
            //        //알뜰지원스토리 : 본인
            //        //PS축하스토리   : 일반추천인 
            //        string st = "■ 일반도마 -> G제품주문 \r\n";
            //        st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //        st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //        st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

            //        efwMemoEdit1.EditValue = st;

            //        StoryOpen(dr["o_code"].ToString());
            //    }
            //    else
            //    {
            //        //일반제품 주문
            //        //PS후기스토리 : 본인
            //        //알뜰지원스토리 : 본인
            //        //PS축하스토리 : 일반추천인 
            //        string st = "■ 일반도마 -> 일반제품주문 \r\n";
            //        st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //        st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //        st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

            //        efwMemoEdit1.EditValue = st;

            //        StoryOpen(dr["o_code"].ToString());
            //    }
            //}
            //else if (dr["u_chef_level"].ToString() == "1")
            //{
            //    //VIP 주문시
            //    if (dr["G제품"].ToString() == "G")
            //    {
            //        //G제품주문
            //        //PS후기스토리   : 본인
            //        //알뜰지원스토리 : 본인
            //        //PS축하스토리   : 일반추천인 
            //        //GV축하스토리   : VIP추천인 
            //        string st = "■ VIP -> G제품주문 \r\n";
            //        st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //        st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //        st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
            //        st += "   - GV축하스토리   : VIP추천인 (" + gridView4.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
            //        st += "    \r\n";
            //        st += "   ※ 일반추천인과 VIP추천인이 같은경우 GV축하스토리 생성됨. \r\n";

            //        efwMemoEdit1.EditValue = st;

            //        StoryOpen(dr["o_code"].ToString());
            //    }
            //    else
            //    {
            //        //일반제품 주문
            //        //PS후기스토리 : 본인
            //        //알뜰지원스토리 : 본인
            //        //PS축하스토리 : 일반추천인 
            //        string st = "■ VIP -> 일반제품주문 \r\n";
            //        st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //        st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //        st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

            //        efwMemoEdit1.EditValue = st;

            //        StoryOpen(dr["o_code"].ToString());
            //    }
            //}
            //else if (dr["u_chef_level"].ToString() == "2" || dr["u_chef_level"].ToString() == "3")
            //{
            //    if (dr["G멀티샵"].ToString() == "Y")
            //    {
            //        //G멀티샵 주문시
            //        if (dr["G제품"].ToString() == "G")
            //        {
            //            //G제품주문
            //            //PS후기스토리   : 본인
            //            //알뜰지원스토리 : 본인
            //            //PS축하스토리   : 일반추천인 
            //            //GM축하스토리   : 담당Biz(담당MD, 담당베스트샵)추천인 
            //            string st = "■ G멀티샵 -> G제품주문 \r\n";
            //            st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //            st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //            st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
            //            st += "   - GM축하스토리   : 담당Biz추천인 (" + gridView5.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
            //            st += "    \r\n";
            //            st += "   ※ 일반추천인과 담당Biz추천인이 같은경우 GM축하스토리 생성됨. \r\n";

            //            efwMemoEdit1.EditValue = st;

            //            StoryOpen(dr["o_code"].ToString());
            //        }
            //        else
            //        {
            //            //일반제품 주문
            //            //PS후기스토리   : 본인
            //            //알뜰지원스토리 : 본인
            //            //PS축하스토리   : 일반추천인 
            //            string st = "■ G멀티샵 -> 일반제품주문 \r\n";
            //            st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //            st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //            st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

            //            efwMemoEdit1.EditValue = st;

            //            StoryOpen(dr["o_code"].ToString());
            //        }
            //    }
            //    else
            //    {
            //        if (dr["G제품"].ToString() == "G")
            //        {
            //            //G제품주문
            //            //PS후기스토리   : 본인
            //            //알뜰지원스토리 : 본인
            //            //PS축하스토리   : 일반추천인 
            //            //GB축하스토리   : 담당Biz추천인 
            //            string st = "■ Biz -> G제품주문 \r\n";
            //            st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //            st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //            st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
            //            st += "   - GB축하스토리   : 담당Biz추천인 (" + gridView5.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";
            //            st += "    \r\n";
            //            st += "   ※ 일반추천인과 담당Biz추천인이 같은경우 GB축하스토리 생성됨. \r\n";

            //            efwMemoEdit1.EditValue = st;

            //            StoryOpen(dr["o_code"].ToString());
            //        }
            //        else
            //        {
            //            //일반제품 주문
            //            //PS후기스토리   : 본인
            //            //알뜰지원스토리 : 본인
            //            //PS축하스토리   : 일반추천인 
            //            string st = "■ Biz -> 일반제품주문 \r\n";
            //            st += "   - PS후기스토리   : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //            st += "   - 알뜰지원스토리 : 본인 (" + dr["u_nickname"].ToString() + ") \r\n";
            //            st += "   - PS축하스토리   : 일반추천인 (" + gridView3.GetRowCellValue(0, "닉네임").ToString() + ") \r\n";

            //            efwMemoEdit1.EditValue = st;

            //            StoryOpen(dr["o_code"].ToString());
            //        }
            //    }
            //}


        }

        private void StoryOpen(string s_u_id, string s_recomm_u_id)
        {
            efwGridControl6.DataBind(StoryOpen2(s_u_id, "224"));                  // 도마오픈스토리
            efwGridControl7.DataBind(StoryOpen3(s_u_id, s_recomm_u_id, "220"));   // 도마추천스토리
            efwGridControl11.DataBind(StoryOpen2(s_u_id, "223"));                 // PR등록스토리

            //담당Biz가 G멀티샵인경우는 일반추천인에게 생성.
            if(gridView5.GetRowCellValue(0, "is_biz").ToString() == "Y")
            {
                s_recomm_u_id = gridView3.GetRowCellValue(0, "U_ID").ToString();
            }
            else
            {
                s_recomm_u_id = gridView5.GetRowCellValue(0, "U_ID").ToString();
            }

            efwGridControl10.DataBind(StoryOpen3(s_u_id, s_recomm_u_id, "243"));  // PR추천스토리

            this.efwGridControl6.MyGridView.BestFitColumns();
            this.efwGridControl7.MyGridView.BestFitColumns();
            this.efwGridControl11.MyGridView.BestFitColumns();
            this.efwGridControl10.MyGridView.BestFitColumns();
        }

        private DataSet StoryOpen2(string s_u_id, string story_cd)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = sql.Query = " select o_code, user_nickname, user_name, login_id, is_write, story_id, DATE_FORMAT(write_date,'%Y-%m-%d %H:%i:%S') as write_date, user_id, " +
                                        "        money_type, money, money_fix " +
                                        "  FROM domalife.story_donut_banks t1 " +
                                        " WHERE t1.contents_type = '" + story_cd + "'" +
                                        "   AND t1.user_id = '" + s_u_id + "'" +
                                        " Order By reg_date DESC ";

                DataSet ds = sql.selectQueryDataSet();

                return ds;

            }
        }

        private DataSet StoryOpen3(string s_u_id, string s_recomm_u_id, string story_cd)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = sql.Query = " select o_code, user_nickname, user_name, login_id, is_write, story_id, DATE_FORMAT(write_date,'%Y-%m-%d %H:%i:%S') as write_date, user_id, " +
                                        "        money_type, money, money_fix " +
                                        "  FROM domalife.story_donut_banks t1 " +
                                        " WHERE t1.contents_type = '" + story_cd + "'" +
                                        "   AND t1.user_id = '" + s_recomm_u_id + "'" +
                                        "   AND t1.recomender_u_id = '" + s_u_id + "'" +
                                        " Order By reg_date DESC ";

                DataSet ds = sql.selectQueryDataSet();

                return ds;

            }
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

        private void gridView6_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView7_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView11_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void gridView10_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.BackColor2 = Color.SeaShell;
            }
        }

        private void row_clear(GridView sView)
        {
            for (int i = 0; i < sView.RowCount;)
                sView.DeleteRow(i);
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
    }
}
