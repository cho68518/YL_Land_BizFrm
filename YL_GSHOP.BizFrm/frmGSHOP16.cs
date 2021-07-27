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
    public partial class frmGSHOP16 : FrmBase
    {
        public frmGSHOP16()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GSHOP12";
            //폼명설정
            this.FrmName = "발모후기 검수";
        }

        private void frmGSHOP16_Load(object sender, EventArgs e)
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
            gridView1.OptionsView.ShowFooter = true;
            
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            this.efwGridControl3.BindControlSet(
            new ColumnControlSet("u_nickname", lbU_NickName)
            , new ColumnControlSet("u_id", txtU_id)
            );
            this.efwGridControl3.Click += efwGridControl3_Click;

            this.efwGridControl1.BindControlSet(
            new ColumnControlSet("story_id", txtstory_id)
            , new ColumnControlSet("is_refund", cbis_refund)
            , new ColumnControlSet("is_mileage", cbis_mileage)
            , new ColumnControlSet("reg_date", lbreg_date)
            );

            this.efwGridControl2.BindControlSet(
             new ColumnControlSet("remark", txtremark)
            ,new ColumnControlSet("story_id", txtrefund)
            );

            this.efwGridControl1.Click += efwGridControl1_Click;
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
            gridView2.CustomUnboundColumnData += gridView2_CustomUnboundColumnData;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
           // open1();
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

        void gridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP16_SELECT_01", con))
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
                            efwGridControl3.DataBind(ds);
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


        private void open1()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP16_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtU_id.EditValue;


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


        private void open2()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP16_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtU_id.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
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

        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            open1();
            open2();
        }

        private void btn_save2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtstory_id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "발모 후기스토리를 선택하세요");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP16_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_story_id", MySqlDbType.Int32));
                            cmd.Parameters["i_story_id"].Value = Convert.ToInt32(txtstory_id.EditValue);
                            cmd.Parameters["i_story_id"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_is_refund", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_refund"].Value = cbis_refund.EditValue;
                            cmd.Parameters["i_is_refund"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_is_mileage", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_mileage"].Value = cbis_mileage.EditValue;
                            cmd.Parameters["i_is_mileage"].Direction = ParameterDirection.Input;
                            // 
                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtremark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

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
                open1();
                open2();
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
