using DevExpress.XtraEditors.Repository;
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

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP14 : FrmBase
    {
        frmGSHOP12_Pop01 popup;
        public frmGSHOP14()
        {
            InitializeComponent();
            this.QCode = "GSHOP14";
            //폼명설정
            this.FrmName = "발모 후기현황";
        }

        private void frmGSHOP14_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            
            checkEdit1.EditValue = "Y";
            checkEdit2.EditValue = "N";
            checkEdit3.EditValue = "N";
            checkEdit4.EditValue = "N";

            txtIdx1.EditValue = "1";
            txtIdx2.EditValue = "2";
            txtIdx3.EditValue = "3";
            txtIdx4.EditValue = "4";

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

            );
            this.efwGridControl1.Click += efwGridControl1_Click;
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
            Open1();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            if (dr != null && dr["pic_url1"].ToString() != "")
            {
                picBest_Pic1.LoadAsync(txtPic_Url1.EditValue.ToString());
                picBest_Pic2.LoadAsync(txtPic_Url2.EditValue.ToString());
                picBest_Pic3.LoadAsync(txtPic_Url3.EditValue.ToString());
                picBest_Pic4.LoadAsync(txtPic_Url4.EditValue.ToString());
                picBest_Pic5.LoadAsync(txtPic_Url5.EditValue.ToString());
                txtStory_id.EditValue = dr["story_id"].ToString();
            }
            Cursor.Current = Cursors.Default;
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP14_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_gshop_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtgshop_name.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        public void Open1()
        {
            try
            {
                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = " select idx, before_url, after_url, sort,  story_id from  domalife.tb_gsite_story  ";
                    DataSet ds = con.selectQueryDataSet();
                    DataRow[] dr = ds.Tables[0].Select();
                    CodeData[] codeArray = new CodeData[dr.Length];

                    for (int i = 0; i < dr.Length; i++)
                    {
                        if (dr[i]["idx"].ToString() == "1")
                        {
                            txtIdx1.EditValue = dr[i]["idx"].ToString();
                            txtBChoice_Url1.EditValue = dr[i]["before_url"].ToString();
                            txtAChoice_Url1.EditValue = dr[i]["after_url"].ToString();
                            txtStory_id1.EditValue = dr[i]["story_id"].ToString();
                            txtSort1.EditValue = dr[i]["sort"].ToString();
                            picBChoice_img1.LoadAsync(txtBChoice_Url1.EditValue.ToString());
                            picAChoice_img1.LoadAsync(txtAChoice_Url1.EditValue.ToString());
                        }
                        if (dr[i]["idx"].ToString() == "2")
                        {
                            txtIdx2.EditValue = dr[i]["idx"].ToString();
                            txtBChoice_Url2.EditValue = dr[i]["before_url"].ToString();
                            txtAChoice_Url2.EditValue = dr[i]["after_url"].ToString();
                            txtStory_id2.EditValue = dr[i]["story_id"].ToString();
                            txtSort2.EditValue = dr[i]["sort"].ToString();
                            picBChoice_img2.LoadAsync(txtBChoice_Url2.EditValue.ToString());
                            picAChoice_img2.LoadAsync(txtAChoice_Url2.EditValue.ToString());
                        }
                        if (dr[i]["idx"].ToString() == "3")
                        {
                            txtIdx3.EditValue = dr[i]["idx"].ToString();
                            txtBChoice_Url3.EditValue = dr[i]["before_url"].ToString();
                            txtAChoice_Url3.EditValue = dr[i]["after_url"].ToString();
                            txtStory_id3.EditValue = dr[i]["story_id"].ToString();
                            txtSort3.EditValue = dr[i]["sort"].ToString();
                            picBChoice_img3.LoadAsync(txtBChoice_Url3.EditValue.ToString());
                            picAChoice_img3.LoadAsync(txtAChoice_Url3.EditValue.ToString());
                        }
                        if (dr[i]["idx"].ToString() == "4")
                        {
                            txtIdx4.EditValue = dr[i]["idx"].ToString();
                            txtBChoice_Url4.EditValue = dr[i]["before_url"].ToString();
                            txtAChoice_Url4.EditValue = dr[i]["after_url"].ToString();
                            txtStory_id4.EditValue = dr[i]["story_id"].ToString();
                            txtSort4.EditValue = dr[i]["sort"].ToString();
                            picBChoice_img4.LoadAsync(txtBChoice_Url4.EditValue.ToString());
                            picAChoice_img4.LoadAsync(txtAChoice_Url4.EditValue.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            Cursor.Current = Cursors.Default;
        }


        public override void Save()
        {

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP14_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            // 1 번째
                            cmd.Parameters.Add(new MySqlParameter("i_idx1", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx1"].Value = Convert.ToInt32(txtIdx1.EditValue);
                            cmd.Parameters["i_idx1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_bchoice_Url1", MySqlDbType.VarChar));
                            cmd.Parameters["i_bchoice_Url1"].Value = txtBChoice_Url1.EditValue;
                            cmd.Parameters["i_bchoice_Url1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_achoice_Url1", MySqlDbType.VarChar));
                            cmd.Parameters["i_achoice_Url1"].Value =txtAChoice_Url1.EditValue;
                            cmd.Parameters["i_achoice_Url1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_id1", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_id1"].Value = Convert.ToInt32(txtStory_id1.EditValue);
                            cmd.Parameters["i_story_id1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sort1", MySqlDbType.VarChar));
                            cmd.Parameters["i_sort1"].Value = Convert.ToInt32(txtSort1.EditValue);
                            cmd.Parameters["i_sort1"].Direction = ParameterDirection.Input;

                            // 2 번째
                            cmd.Parameters.Add(new MySqlParameter("i_idx2", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx2"].Value = Convert.ToInt32(txtIdx2.EditValue);
                            cmd.Parameters["i_idx2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_bchoice_Url2", MySqlDbType.VarChar));
                            cmd.Parameters["i_bchoice_Url2"].Value = txtBChoice_Url2.EditValue;
                            cmd.Parameters["i_bchoice_Url2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_achoice_Url2", MySqlDbType.VarChar));
                            cmd.Parameters["i_achoice_Url2"].Value = txtAChoice_Url2.EditValue;
                            cmd.Parameters["i_achoice_Url2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_id2", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_id2"].Value = Convert.ToInt32(txtStory_id2.EditValue);
                            cmd.Parameters["i_story_id2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sort2", MySqlDbType.VarChar));
                            cmd.Parameters["i_sort2"].Value = Convert.ToInt32(txtSort2.EditValue);
                            cmd.Parameters["i_sort2"].Direction = ParameterDirection.Input;

                            // 3 번째
                            cmd.Parameters.Add(new MySqlParameter("i_idx3", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx3"].Value = Convert.ToInt32(txtIdx3.EditValue);
                            cmd.Parameters["i_idx3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_bchoice_Url3", MySqlDbType.VarChar));
                            cmd.Parameters["i_bchoice_Url3"].Value = txtBChoice_Url3.EditValue;
                            cmd.Parameters["i_bchoice_Url3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_achoice_Url3", MySqlDbType.VarChar));
                            cmd.Parameters["i_achoice_Url3"].Value = txtAChoice_Url3.EditValue;
                            cmd.Parameters["i_achoice_Url3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_id3", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_id3"].Value = Convert.ToInt32(txtStory_id3.EditValue);
                            cmd.Parameters["i_story_id3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sort3", MySqlDbType.VarChar));
                            cmd.Parameters["i_sort3"].Value = Convert.ToInt32(txtSort3.EditValue);
                            cmd.Parameters["i_sort3"].Direction = ParameterDirection.Input;

                            // 4 번째
                            cmd.Parameters.Add(new MySqlParameter("i_idx4", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx4"].Value = Convert.ToInt32(txtIdx4.EditValue);
                            cmd.Parameters["i_idx4"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_bchoice_Url4", MySqlDbType.VarChar));
                            cmd.Parameters["i_bchoice_Url4"].Value = txtBChoice_Url4.EditValue;
                            cmd.Parameters["i_bchoice_Url4"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_achoice_Url4", MySqlDbType.VarChar));
                            cmd.Parameters["i_achoice_Url4"].Value = txtAChoice_Url4.EditValue;
                            cmd.Parameters["i_achoice_Url4"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_id4", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_id4"].Value = Convert.ToInt32(txtStory_id4.EditValue);
                            cmd.Parameters["i_story_id4"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sort4", MySqlDbType.VarChar));
                            cmd.Parameters["i_sort4"].Value = Convert.ToInt32(txtSort4.EditValue);
                            cmd.Parameters["i_sort4"].Direction = ParameterDirection.Input;

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

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            checkEdit2.EditValue = "N";
            checkEdit3.EditValue = "N";
            checkEdit4.EditValue = "N";
            efwPanel1.BackColor = Color.Aqua;
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            checkEdit1.EditValue = "N";
            checkEdit3.EditValue = "N";
            checkEdit4.EditValue = "N";
            efwPanel2.BackColor = Color.Red;
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            checkEdit1.EditValue = "N";
            checkEdit2.EditValue = "N";
            checkEdit4.EditValue = "N";
            efwPanel3.BackColor = Color.Red;
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            checkEdit1.EditValue = "N";
            checkEdit2.EditValue = "N";
            checkEdit3.EditValue = "N";
            efwPanel4.BackColor = Color.Red;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(this.txtPic_Url1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtBChoice_Url1.EditValue = txtPic_Url1.EditValue.ToString();

                if (txtPic_Url1.EditValue.ToString() != "")
                {
                    picBChoice_img1.LoadAsync(txtBChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }

            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtBChoice_Url2.EditValue = txtPic_Url1.EditValue.ToString();

                if (txtPic_Url1.EditValue.ToString() != "")
                {
                    picBChoice_img2.LoadAsync(txtBChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }

            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtBChoice_Url3.EditValue = txtPic_Url1.EditValue.ToString();
                if (txtPic_Url1.EditValue.ToString() != "")
                {
                    picBChoice_img3.LoadAsync(txtBChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }

            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtBChoice_Url4.EditValue = txtPic_Url1.EditValue.ToString();
                if (txtPic_Url1.EditValue.ToString() != "")
                {
                    picBChoice_img4.LoadAsync(txtBChoice_Url4.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }

            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtPic_Url1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtAChoice_Url1.EditValue = txtPic_Url1.EditValue.ToString();

                if (txtPic_Url1.EditValue.ToString() != "")
                {
                    picAChoice_img1.LoadAsync(txtAChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }

            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtAChoice_Url2.EditValue = txtPic_Url1.EditValue.ToString();
                if (txtPic_Url1.EditValue.ToString() != "")
                {
                    picAChoice_img2.LoadAsync(txtAChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }

            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtAChoice_Url3.EditValue = txtPic_Url1.EditValue.ToString();
                if (txtPic_Url1.EditValue.ToString() != "")
                {
                    picAChoice_img3.LoadAsync(txtAChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }

            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtAChoice_Url4.EditValue = txtPic_Url1.EditValue.ToString();
                if (txtPic_Url1.EditValue.ToString() != "")
                {
                    picAChoice_img4.LoadAsync(txtAChoice_Url4.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtPic_Url2.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtBChoice_Url1.EditValue = txtPic_Url2.EditValue.ToString();
                if (txtBChoice_Url1.EditValue.ToString() != "")
                {
                    picBChoice_img1.LoadAsync(txtBChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtBChoice_Url2.EditValue = txtPic_Url2.EditValue.ToString();
                if (txtBChoice_Url2.EditValue.ToString() != "")
                {
                    picBChoice_img2.LoadAsync(txtBChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtBChoice_Url3.EditValue = txtPic_Url2.EditValue.ToString();
                if (txtBChoice_Url3.EditValue.ToString() != "")
                {
                    picBChoice_img3.LoadAsync(txtBChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtBChoice_Url4.EditValue = txtPic_Url2.EditValue.ToString();
                if (txtBChoice_Url4.EditValue.ToString() != "")
                {
                    picBChoice_img4.LoadAsync(txtBChoice_Url4.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtPic_Url2.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtAChoice_Url1.EditValue = txtPic_Url2.EditValue.ToString();
                if (txtAChoice_Url1.EditValue.ToString() != "")
                {
                    picAChoice_img1.LoadAsync(txtAChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtAChoice_Url2.EditValue = txtPic_Url2.EditValue.ToString();
                if (txtAChoice_Url2.EditValue.ToString() != "")
                {
                    picAChoice_img2.LoadAsync(txtAChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtAChoice_Url3.EditValue = txtPic_Url2.EditValue.ToString();
                if (txtAChoice_Url3.EditValue.ToString() != "")
                {
                    picAChoice_img3.LoadAsync(txtAChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtAChoice_Url4.EditValue = txtPic_Url2.EditValue.ToString();
                if (txtAChoice_Url4.EditValue.ToString() != "")
                {
                    picAChoice_img4.LoadAsync(txtAChoice_Url2.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPic_Url3.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtBChoice_Url1.EditValue = txtPic_Url3.EditValue.ToString();
                if (txtBChoice_Url1.EditValue.ToString() != "")
                {
                    picBChoice_img1.LoadAsync(txtBChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtBChoice_Url2.EditValue = txtPic_Url3.EditValue.ToString();
                if (txtBChoice_Url2.EditValue.ToString() != "")
                {
                    picBChoice_img2.LoadAsync(txtBChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtBChoice_Url3.EditValue = txtPic_Url3.EditValue.ToString();
                if (txtBChoice_Url3.EditValue.ToString() != "")
                {
                    picBChoice_img3.LoadAsync(txtBChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtBChoice_Url4.EditValue = txtPic_Url3.EditValue.ToString();
                if (txtBChoice_Url4.EditValue.ToString() != "")
                {
                    picBChoice_img4.LoadAsync(txtBChoice_Url4.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPic_Url3.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtAChoice_Url1.EditValue = txtPic_Url3.EditValue.ToString();
                if (txtAChoice_Url1.EditValue.ToString() != "")
                {
                    picAChoice_img1.LoadAsync(txtAChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtAChoice_Url2.EditValue = txtPic_Url3.EditValue.ToString();
                if (txtAChoice_Url2.EditValue.ToString() != "")
                {
                    picAChoice_img2.LoadAsync(txtAChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtAChoice_Url3.EditValue = txtPic_Url3.EditValue.ToString();
                if (txtAChoice_Url3.EditValue.ToString() != "")
                {
                    picAChoice_img3.LoadAsync(txtAChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtAChoice_Url4.EditValue = txtPic_Url3.EditValue.ToString();
                if (txtAChoice_Url4.EditValue.ToString() != "")
                {
                    picAChoice_img4.LoadAsync(txtAChoice_Url4.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPic_Url4.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtBChoice_Url1.EditValue = txtPic_Url4.EditValue.ToString();
                if (txtBChoice_Url1.EditValue.ToString() != "")
                {
                    picBChoice_img1.LoadAsync(txtBChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtBChoice_Url2.EditValue = txtPic_Url4.EditValue.ToString();
                if (txtBChoice_Url2.EditValue.ToString() != "")
                {
                    picBChoice_img2.LoadAsync(txtBChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtBChoice_Url3.EditValue = txtPic_Url4.EditValue.ToString();
                if (txtBChoice_Url3.EditValue.ToString() != "")
                {
                    picBChoice_img3.LoadAsync(txtBChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtBChoice_Url4.EditValue = txtPic_Url4.EditValue.ToString();
                if (txtBChoice_Url4.EditValue.ToString() != "")
                {
                    picBChoice_img4.LoadAsync(txtBChoice_Url4.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPic_Url4.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtAChoice_Url1.EditValue = txtPic_Url4.EditValue.ToString();
                if (txtAChoice_Url1.EditValue.ToString() != "")
                {
                    picAChoice_img1.LoadAsync(txtAChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtAChoice_Url2.EditValue = txtPic_Url4.EditValue.ToString();
                if (txtAChoice_Url2.EditValue.ToString() != "")
                {
                    picAChoice_img2.LoadAsync(txtAChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtAChoice_Url3.EditValue = txtPic_Url4.EditValue.ToString();
                if (txtAChoice_Url3.EditValue.ToString() != "")
                {
                    picAChoice_img3.LoadAsync(txtAChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtAChoice_Url4.EditValue = txtPic_Url4.EditValue.ToString();
                if (txtAChoice_Url4.EditValue.ToString() != "")
                {
                    picAChoice_img4.LoadAsync(txtAChoice_Url4.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPic_Url5.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtBChoice_Url1.EditValue = txtPic_Url5.EditValue.ToString();
                if (txtBChoice_Url1.EditValue.ToString() != "")
                {
                    picAChoice_img1.LoadAsync(txtBChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtBChoice_Url2.EditValue = txtPic_Url5.EditValue.ToString();
                if (txtBChoice_Url2.EditValue.ToString() != "")
                {
                    picAChoice_img2.LoadAsync(txtBChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtBChoice_Url3.EditValue = txtPic_Url5.EditValue.ToString();
                if (txtBChoice_Url3.EditValue.ToString() != "")
                {
                    picAChoice_img3.LoadAsync(txtBChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtBChoice_Url4.EditValue = txtPic_Url5.EditValue.ToString();
                if (txtBChoice_Url4.EditValue.ToString() != "")
                {
                    picBChoice_img4.LoadAsync(txtBChoice_Url4.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPic_Url5.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "선택된 이미지가 없습니다!");
                return;
            }
            if (checkEdit1.EditValue.ToString() == "Y")
            {
                txtAChoice_Url1.EditValue = txtPic_Url5.EditValue.ToString();
                if (txtAChoice_Url1.EditValue.ToString() != "")
                {
                    picAChoice_img1.LoadAsync(txtAChoice_Url1.EditValue.ToString());
                    txtStory_id1.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit2.EditValue.ToString() == "Y")
            {
                txtAChoice_Url2.EditValue = txtPic_Url5.EditValue.ToString();
                if (txtAChoice_Url2.EditValue.ToString() != "")
                {
                    picAChoice_img2.LoadAsync(txtAChoice_Url2.EditValue.ToString());
                    txtStory_id2.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit3.EditValue.ToString() == "Y")
            {
                txtAChoice_Url3.EditValue = txtPic_Url5.EditValue.ToString();
                if (txtAChoice_Url3.EditValue.ToString() != "")
                {
                    picAChoice_img3.LoadAsync(txtAChoice_Url3.EditValue.ToString());
                    txtStory_id3.EditValue = txtStory_id.EditValue.ToString();
                }
            }
            else if (checkEdit4.EditValue.ToString() == "Y")
            {
                txtAChoice_Url4.EditValue = txtPic_Url5.EditValue.ToString();
                if (txtAChoice_Url4.EditValue.ToString() != "")
                {
                    picAChoice_img4.LoadAsync(txtAChoice_Url1.EditValue.ToString());
                    txtStory_id4.EditValue = txtStory_id.EditValue.ToString();
                }
            }
        }
    }

}
