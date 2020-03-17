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
            this.FrmName = "체험 고객선정 등록";


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

            gridView1.OptionsView.ShowFooter = true;

            rbP_SHOW_TYPE.EditValue = "T";


            SetCmb();
            this.efwGridControl1.BindControlSet(
             new ColumnControlSet("idx", txtIdx)
           , new ColumnControlSet("age", dtAge)
           , new ColumnControlSet("name", dtName)
           , new ColumnControlSet("cust_date", dtCust_Date)
           , new ColumnControlSet("shop_name", txtShop_Name)
           , new ColumnControlSet("best_pic1", ckBest_Pic1)
           , new ColumnControlSet("best_pic2", ckBest_Pic2)
           , new ColumnControlSet("best_pic3", ckBest_Pic3)
           , new ColumnControlSet("best_pic4", ckBest_Pic4)
           , new ColumnControlSet("best_pic5", ckBest_Pic5)

           , new ColumnControlSet("pic_url1", txtPic_Url1)
           , new ColumnControlSet("pic_url2", txtPic_Url2)
           , new ColumnControlSet("pic_url3", txtPic_Url3)
           , new ColumnControlSet("pic_url4", txtPic_Url4)
           , new ColumnControlSet("pic_url5", txtPic_Url5)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
        }
        // picP_IMG.LoadAsync(cmd.Parameters["o_p_img"].Value.ToString());
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            picBest_Pic1.LoadAsync(txtPic_Url1.EditValue.ToString());
            picBest_Pic2.LoadAsync(txtPic_Url2.EditValue.ToString());
            picBest_Pic3.LoadAsync(txtPic_Url3.EditValue.ToString());
            picBest_Pic4.LoadAsync(txtPic_Url4.EditValue.ToString());
            picBest_Pic5.LoadAsync(txtPic_Url5.EditValue.ToString());
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


        }



        private void SetCmb()
        {
            // 공급자구분

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00033'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbMember_Search, codeArray);
            }

            cmbMember_Search.EditValue = "00";

            // 공급자구분

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00035'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbPrint_No, codeArray);
            }

            cmbPrint_No.EditValue = "00";

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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP13_SELECT_02", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.ExecuteNonQuery();
                    }
                }

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP13_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;


                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbMember_Search.EditValue.ToString();


                        if (rbP_SHOW_TYPE.EditValue.ToString() != "Y" && rbP_SHOW_TYPE.EditValue.ToString() != "N")
                            sShow_Type = null;
                        else
                            sShow_Type = rbP_SHOW_TYPE.EditValue.ToString();

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = sShow_Type;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //  this.efwGridControl1.MyGridView.BestFitColumns();
                        }
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

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP13_SAVE_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_idx", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtIdx.EditValue);

                        cmd.Parameters.Add("i_cust_date", MySqlDbType.DateTime);
                        cmd.Parameters[1].Value = dtCust_Date.EditValue;

                        cmd.Parameters.Add("i_best_pic1", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = ckBest_Pic1.EditValue;

                        cmd.Parameters.Add("i_best_pic2", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = ckBest_Pic2.EditValue;

                        cmd.Parameters.Add("i_best_pic3", MySqlDbType.VarChar, 1);
                        cmd.Parameters[4].Value = ckBest_Pic3.EditValue;

                        cmd.Parameters.Add("i_best_pic4", MySqlDbType.VarChar, 1);
                        cmd.Parameters[5].Value = ckBest_Pic4.EditValue;

                        cmd.Parameters.Add("i_best_pic5", MySqlDbType.VarChar, 1);
                        cmd.Parameters[6].Value = ckBest_Pic5.EditValue; 
                        
                        cmd.Parameters.Add("i_print_no", MySqlDbType.VarChar, 10);
                        cmd.Parameters[7].Value = cmbPrint_No.EditValue; 


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //  this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

    }
}