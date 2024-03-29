﻿using DevExpress.XtraEditors.Repository;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_GSHOP.BizFrm.Dlg;
using DevExpress.XtraGrid.Columns;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Tile;

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP12 : FrmBase
    {
        frmGSHOP12_Pop01 popup;
        public frmGSHOP12()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GSHOP12";
            //폼명설정
            this.FrmName = "발모 후기현황";


            //    efwGridControl1.DataSource = GetData();



        }

        private void frmGSHOP12_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            txtOverlap_Pic1.EditValue = "0";
            txtOverlap_Pic2.EditValue = "0";
            txtOverlap_Pic3.EditValue = "0";
            txtOverlap_Pic4.EditValue = "0";
            txtOverlap_Pic5.EditValue = "0";

            txtOverlap_Loc1.EditValue = "0";
            txtOverlap_Loc2.EditValue = "0";
            txtOverlap_Loc3.EditValue = "0";
            txtOverlap_Loc4.EditValue = "0";
            txtOverlap_Loc5.EditValue = "0";

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
             new ColumnControlSet("pic_url1", txtPic_Url1)
           , new ColumnControlSet("pic_url2", txtPic_Url2)
           , new ColumnControlSet("pic_url3", txtPic_Url3)
           , new ColumnControlSet("pic_url4", txtPic_Url4)
           , new ColumnControlSet("pic_url5", txtPic_Url5)
           , new ColumnControlSet("shooting_date1", txtShooting_Date1)
           , new ColumnControlSet("shooting_date2", txtShooting_Date2)
           , new ColumnControlSet("shooting_date3", txtShooting_Date3)
           , new ColumnControlSet("shooting_date4", txtShooting_Date4)
           , new ColumnControlSet("shooting_date5", txtShooting_Date5)
           , new ColumnControlSet("story_id", txtstory_id)
           , new ColumnControlSet("is_use", chis_use)
           , new ColumnControlSet("overlap_pic1", txtOverlap_Pic1)
           , new ColumnControlSet("overlap_pic2", txtOverlap_Pic2)
           , new ColumnControlSet("overlap_pic3", txtOverlap_Pic3)
           , new ColumnControlSet("overlap_pic4", txtOverlap_Pic4)
           , new ColumnControlSet("overlap_pic5", txtOverlap_Pic5)
           , new ColumnControlSet("overlap_loc1", txtOverlap_Loc1)
           , new ColumnControlSet("overlap_loc2", txtOverlap_Loc2)
           , new ColumnControlSet("overlap_loc3", txtOverlap_Loc3)
           , new ColumnControlSet("overlap_loc4", txtOverlap_Loc4)
           , new ColumnControlSet("overlap_loc5", txtOverlap_Loc5)
           , new ColumnControlSet("u_id", txtU_Id)
           , new ColumnControlSet("u_nickname", lbU_Nickname)
            );

            this.efwGridControl1.Click += efwGridControl1_Click;
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

        }
        // picP_IMG.LoadAsync(cmd.Parameters["o_p_img"].Value.ToString());
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            if (dr != null && dr["story_id"].ToString() != "")
            {
                picBest_Pic1.LoadAsync(txtPic_Url1.EditValue.ToString());
                picBest_Pic2.LoadAsync(txtPic_Url2.EditValue.ToString());
                picBest_Pic3.LoadAsync(txtPic_Url3.EditValue.ToString());
                picBest_Pic4.LoadAsync(txtPic_Url4.EditValue.ToString());
                picBest_Pic5.LoadAsync(txtPic_Url5.EditValue.ToString());
            }
            Cursor.Current = Cursors.Arrow;

        }

        

        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();

        void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {

            DataRow dr = (e.Row as DataRowView).Row;
            string url = string.Empty;

            if (e.Column.FieldName == "Image0")
            {
                url = dr["profile_url"].ToString();
            }

            if (e.Column.FieldName == "Image1")
            {
                url = dr["pic_url1"].ToString();
            }

            if (e.Column.FieldName == "Image2")
            {
                url = dr["pic_url2"].ToString();
            }

            if (e.Column.FieldName == "Image3")
            {
                url = dr["pic_url3"].ToString();
            }

            if (iconsCache.ContainsKey(url))
            {
                e.Value = iconsCache[url];
                return;
            }

            if (url != "")
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        e.Value = Bitmap.FromStream(stream);
                        iconsCache.Add(url, (Bitmap)e.Value);
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        public override void Search()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;
                
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP12_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_gshop_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtgshop_name.EditValue;


                        cmd.Parameters.Add("i_category_no", MySqlDbType.VarChar, 3);
                        cmd.Parameters[3].Value = rbP_SHOW_TYPE.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                        Cursor.Current = Cursors.Arrow;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void picBest_Pic1_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("1");
        }

        private void picBest_Pic2_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("2");
        }

        private void picBest_Pic3_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("3");
        }

        private void picBest_Pic4_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("4");
        }

        private void picBest_Pic5_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("5");
        }

        private void OpenDlg(string s1)
        {
            popup = new frmGSHOP12_Pop01();
            //popup.Owner = this;

            if (s1 == "1")
                popup.pURL = txtPic_Url1.Text;
            else if (s1 == "2")
                popup.pURL = txtPic_Url2.Text;
            else if (s1 == "3")
                popup.pURL = txtPic_Url3.Text;
            else if (s1 == "4")
                popup.pURL = txtPic_Url4.Text;
            else if (s1 == "5")
                popup.pURL = txtPic_Url5.Text;

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }

        private void efwGridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtstory_id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 발모후기 스토리를 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP12_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_story_id", MySqlDbType.Int32));
                            cmd.Parameters["i_story_id"].Value = Convert.ToInt32(txtstory_id.EditValue);
                            cmd.Parameters["i_story_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = chis_use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Shooting_Date1", MySqlDbType.VarChar));
                            cmd.Parameters["i_Shooting_Date1"].Value = txtShooting_Date1.EditValue;
                            cmd.Parameters["i_Shooting_Date1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Shooting_Date2", MySqlDbType.VarChar));
                            cmd.Parameters["i_Shooting_Date2"].Value = txtShooting_Date2.EditValue;
                            cmd.Parameters["i_Shooting_Date2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Shooting_Date3", MySqlDbType.VarChar));
                            cmd.Parameters["i_Shooting_Date3"].Value = txtShooting_Date3.EditValue;
                            cmd.Parameters["i_Shooting_Date3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Shooting_Date4", MySqlDbType.VarChar));
                            cmd.Parameters["i_Shooting_Date4"].Value = txtShooting_Date4.EditValue;
                            cmd.Parameters["i_Shooting_Date4"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Shooting_Date5", MySqlDbType.VarChar));
                            cmd.Parameters["i_Shooting_Date5"].Value = txtShooting_Date5.EditValue;
                            cmd.Parameters["i_Shooting_Date5"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_pic1", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_pic1"].Value = Convert.ToInt32(txtOverlap_Pic1.EditValue);
                            cmd.Parameters["i_overlap_pic1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_pic2", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_pic2"].Value = Convert.ToInt32(txtOverlap_Pic2.EditValue);
                            cmd.Parameters["i_overlap_pic2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_pic3", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_pic3"].Value = Convert.ToInt32(txtOverlap_Pic3.EditValue);
                            cmd.Parameters["i_overlap_pic3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_pic4", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_pic4"].Value = Convert.ToInt32(txtOverlap_Pic4.EditValue);
                            cmd.Parameters["i_overlap_pic4"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_pic5", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_pic5"].Value = Convert.ToInt32(txtOverlap_Pic5.EditValue);
                            cmd.Parameters["i_overlap_pic5"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_Loc1", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_Loc1"].Value = Convert.ToInt32(txtOverlap_Loc1.EditValue);
                            cmd.Parameters["i_overlap_Loc1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_Loc2", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_Loc2"].Value = Convert.ToInt32(txtOverlap_Loc2.EditValue);
                            cmd.Parameters["i_overlap_Loc2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_Loc3", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_Loc3"].Value = Convert.ToInt32(txtOverlap_Loc3.EditValue);
                            cmd.Parameters["i_overlap_Loc3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_Loc4", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_Loc4"].Value = Convert.ToInt32(txtOverlap_Loc4.EditValue);
                            cmd.Parameters["i_overlap_Loc4"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_Loc5", MySqlDbType.Int32));
                            cmd.Parameters["i_overlap_Loc5"].Value = Convert.ToInt32(txtOverlap_Loc5.EditValue);
                            cmd.Parameters["i_overlap_Loc5"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_customer_num", MySqlDbType.Int32));
                            cmd.Parameters["i_customer_num"].Value = Convert.ToInt32(txtCustomer_Num.EditValue);
                            cmd.Parameters["i_customer_num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
            }
        }

        private void Open1()
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                SqlCommand cmd = new SqlCommand();
                sql.Query = " select t2.idx as idx, t1.reg_date as reg_date, t3.u_nickname as u_nickname, t1.is_use as is_use, t2.image_url as pic_url1, " +
                            "        t2.contents_id as contents_id, t2.order_key as order_key, t2.overlap_pic as overlap_pic, t2.overlap_loc as overlap_loc,  t2.customer_num as customer_num " +
                            "   from domalife.story_list t1  " +
                            "        inner join domalife.y_thumbnail_list t2 on t2.contents_id = t1.story_id  and t2.is_use = 'Y' " +
                            "        inner join domalife.member_master    t3 on t3.u_id = t1.u_id    " +
                            "   where t1.category_no = 249 and t1.u_id =  '" +  txtU_Id.EditValue +  "'  " +
                            "  order by t1.story_id, t2.order_key ";

                DataSet ds = sql.selectQueryDataSet();
                gridControl1.DataSource = ds.Tables[0];
            }
        }

        private void layoutView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DataRow dr = (e.Row as DataRowView).Row;
            string url = string.Empty;

            if (e.Column.FieldName == "Image1")
            {
                url = dr["pic_url1"].ToString();
            }

            if (iconsCache.ContainsKey(url))
            {
                e.Value = iconsCache[url];
                return;
            }

            if (url != "")
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        e.Value = Bitmap.FromStream(stream);
                        iconsCache.Add(url, (Bitmap)e.Value);
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open2();
            }
        }


        private void layoutView1_Click(object sender, EventArgs e)
        {
            txtContents_Id.EditValue = tileView1.GetFocusedRowCellValue("contents_id").ToString();
            txtOrder_Key.EditValue = tileView1.GetFocusedRowCellValue("order_key").ToString();
            cbIs_Use.EditValue = tileView1.GetFocusedRowCellValue("is_use").ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtstory_id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 발모후기 스토리를 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP12_SAVE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_story_id", MySqlDbType.Int32));
                            cmd.Parameters["i_story_id"].Value = Convert.ToInt32(txtContents_Id.EditValue);
                            cmd.Parameters["i_story_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_order_key", MySqlDbType.Int32));
                            cmd.Parameters["i_order_key"].Value = Convert.ToInt32(txtOrder_Key.EditValue);
                            cmd.Parameters["i_order_key"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = cbIs_Use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_pic", MySqlDbType.VarChar));
                            cmd.Parameters["i_overlap_pic"].Value = Convert.ToInt32(txtOverlap_Pic.EditValue);
                            cmd.Parameters["i_overlap_pic"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_overlap_Loc", MySqlDbType.VarChar));
                            cmd.Parameters["i_overlap_Loc"].Value = Convert.ToInt32(txtOverlap_Loc.EditValue);
                            cmd.Parameters["i_overlap_Loc"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open1();
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            txtURL.EditValue = tileView1.GetFocusedRowCellValue("pic_url1").ToString();
            OpenDlg();
        }

        private void OpenDlg()
        {
            popup = new frmGSHOP12_Pop01();
            //popup.Owner = this;

            popup.pURL = txtURL.EditValue.ToString();


            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void Open2()
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                SqlCommand cmd = new SqlCommand();
                sql.Query = " select t2.idx as idx, t1.reg_date as reg_date, t1.is_use as is_use, t2.image_url as pic_url1, " +
                            "        t2.contents_id as contents_id, t2.order_key as order_key, t2.overlap_pic as overlap_pic, t2.overlap_loc as overlap_loc" +
                            "   from domalife.story_list t1  " +
                            "        inner join domalife.y_thumbnail_list t2 on t2.contents_id = t1.story_id  and t2.is_use = 'Y' " +
                            "   where t1.category_no = 249 and t1.u_id =  '" + txtU_Id.EditValue + "' and  t2.overlap_pic != 0 " +
                            " union ALL " +
                            " select t2.idx as idx, t1.reg_date as reg_date, t2.is_use as is_use, t2.image_url as pic_url1, " +
                            "        t2.contents_id as contents_id, t2.order_key as order_key, t2.overlap_pic as overlap_pic, t2.overlap_loc as overlap_loc " +
                            "   from domalife.story_list t1 " +
                            "        inner join domalife.y_thumbnail_list t2 on t2.contents_id = t1.story_id   and t2.is_use = 'Y' " +
                            "  where t1.category_no = 249 and t1.u_id = '" + txtU_Id.EditValue + "'  AND " +
                            "        t2.contents_id in (select overlap_pic " +
                            "                             from domalife.story_list a " +
                            "                                  inner join domalife.y_thumbnail_list b on b.contents_id = a.story_id   and a.is_use = 'Y' " +
                            "                             where a.category_no = 249 and a.u_id = '" + txtU_Id.EditValue + "'  and overlap_pic != 0) and " +
                            "        t2.order_key   in (select overlap_loc " +
                            "                            from domalife.story_list a " +
                            "                                 inner join domalife.y_thumbnail_list b on b.contents_id = a.story_id   and a.is_use = 'Y' " +
                            "                            where a.category_no = 249 and a.u_id = '" + txtU_Id.EditValue + "' and overlap_loc != 0) ";

                DataSet ds1 = sql.selectQueryDataSet();
                gridControl2.DataSource = ds1.Tables[0];
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                SqlCommand cmd = new SqlCommand();
                sql.Query = " select t2.idx as idx, t1.reg_date as reg_date, t3.u_nickname as u_nickname, t1.is_use as is_use, t2.image_url as pic_url1, " +
                            "        t2.contents_id as contents_id, t2.order_key as order_key, t2.overlap_pic as overlap_pic, t2.overlap_loc as overlap_loc" +
                            "   from domalife.story_list t1  " +
                            "        inner join domalife.y_thumbnail_list t2 on t2.contents_id = t1.story_id  and t2.is_use = 'Y' " +
                            "        inner join domalife.member_master    t3 on t3.u_id = t1.u_id    " +
                            "   where t1.category_no = 249 and t1.u_id =  '" + txtU_Id.EditValue + "' and  t2.overlap_pic = 0 " +
                            "  order by t1.story_id, t2.order_key ";

                DataSet ds2 = sql.selectQueryDataSet();
                gridControl1.DataSource = ds2.Tables[0];
            }
        }
    }
}