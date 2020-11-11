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
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

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

            );
            this.efwGridControl1.Click += efwGridControl1_Click;
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
        }
        // picP_IMG.LoadAsync(cmd.Parameters["o_p_img"].Value.ToString());
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
            }

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
    }
}