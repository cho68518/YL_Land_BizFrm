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
using YL_MM.BizFrm.Dlg;
using DevExpress.XtraGrid.Columns;

namespace YL_MM.BizFrm
{
    public partial class frmMM15 : FrmBase
    {
        frmMM15_Pop01 popup;
        frmMM15_Pop02 popup1;
        public frmMM15()
        {
            InitializeComponent();
            this.QCode = "MM15";
            //폼명설정
            this.FrmName = "기획상품 할인율 등록";

            gridView2.CustomUnboundColumnData += gridView2_CustomUnboundColumnData;
            bandedGridView2.CustomUnboundColumnData += bandedGridView2_CustomUnboundColumnData;

            this.gridColumn17.OptionsColumn.ReadOnly = false;
            //this.gridColumn14.OptionsColumn.ReadOnly = true;
            //this.gridColumn7.OptionsColumn.ReadOnly = true;
            //this.gridColumn8.OptionsColumn.ReadOnly = true;
        }


        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            gridView1.OptionsView.ShowFooter = true;
            gridView2.OptionsView.ShowFooter = true;
            bandedGridView2.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("id", txtId)
                    , new ColumnControlSet("pm_order", txtPm_order)
                    , new ColumnControlSet("pm_name", txtId_Name)
                    , new ColumnControlSet("pm_use_type", rbPm_Use_Type)
                    , new ColumnControlSet("pm_name", txtPm_Name)


                      );

            this.efwGridControl2.BindControlSet(
                     new ColumnControlSet("p_name", txtProd_Name)
                   , new ColumnControlSet("p_id", txtP_Id)
                   , new ColumnControlSet("pm_prodser", txtPm_prodser)
                   , new ColumnControlSet("doma", txtDoma_Rate)
                   //, new ColumnControlSet("is_banner", rbIs_Banner)
                   , new ColumnControlSet("doma_money", cmbDoma_Money)
                   , new ColumnControlSet("vip", txtVip_Rate)
                   , new ColumnControlSet("vip_money", cmbVip_Money)
                   , new ColumnControlSet("chef", txtChef_Rate)
                   , new ColumnControlSet("chef_money", cmbChef_Money)
                   , new ColumnControlSet("chef2", txtMd_Rate)
                   , new ColumnControlSet("chef2_money", cmbMd_Money)
                   , new ColumnControlSet("gshop", txtGshop_Rate)
                   , new ColumnControlSet("gshop_money", cmbGshop_Money)
                      );
            rbIs_Banner.EditValue = "N";

            SetCmb();
            Search();
        }
        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");

            cmbDoma_Money.EditValue = "0";
            cmbVip_Money.EditValue = "0";
            cmbChef_Money.EditValue = "0";
            cmbMd_Money.EditValue = "0";
            cmbGshop_Money.EditValue = "0";


            Search();
        }


        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            
            if (dr != null && dr["id"].ToString() != "0")
            {
                this.txtId.Text = dr["id"].ToString();

            }
             Open1();
            txtDoma_Rate.EditValue = "";
            txtVip_Rate.EditValue = "";
            txtChef_Rate.EditValue = "";
            txtMd_Rate.EditValue = "";
            txtGshop_Rate.EditValue = "";

            cmbDoma_Money.EditValue = "0";
            cmbVip_Money.EditValue = "0";
            cmbChef_Money.EditValue = "0";
            cmbMd_Money.EditValue = "0";
            cmbGshop_Money.EditValue = "0";
        }

        private void SetCmb()
        {

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select code_id as DCODE ,code_nm as DNAME  from domaadmin.tb_common_code  where gcode_id = '00028'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbDoma_Money, codeArray);
                CodeAgent.MakeCodeControl(this.cmbVip_Money, codeArray);
                CodeAgent.MakeCodeControl(this.cmbChef_Money, codeArray);
                CodeAgent.MakeCodeControl(this.cmbMd_Money, codeArray);
                CodeAgent.MakeCodeControl(this.cmbGshop_Money, codeArray);
            }
            cmbDoma_Money.EditValue = "0";
            cmbVip_Money.EditValue = "0";
            cmbChef_Money.EditValue = "0";
            cmbMd_Money.EditValue = "0";
            cmbGshop_Money.EditValue = "0";

        }

        public override void Search()
        {
            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

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
        void bandedGridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = dr["ImageURL"].ToString();
                if (iconsCache.ContainsKey(url))
                {
                    e.Value = iconsCache[url];
                    return;
                }
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
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }
        void gridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = dr["ImageURL"].ToString();
                if (iconsCache.ContainsKey(url))
                {
                    e.Value = iconsCache[url];
                    return;
                }
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
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }
        void gridView3_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = dr["ImageURL"].ToString();
                if (iconsCache.ContainsKey(url))
                {
                    e.Value = iconsCache[url];
                    return;
                }
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
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();
        public void Open1()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;
                
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Id", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = txtId.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
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

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
                gridView2.SetRowCellValue(i, gridView2.Columns["chk"], "Y");
            for (int i = 0; i < bandedGridView2.RowCount; i++)
                bandedGridView2.SetRowCellValue(i, bandedGridView2.Columns["chk"], "Y");
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
                gridView2.SetRowCellValue(i, gridView2.Columns["chk"], "N");
            for (int i = 0; i < bandedGridView2.RowCount; i++)
                bandedGridView2.SetRowCellValue(i, bandedGridView2.Columns["chk"], "N");
        }



        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtId.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "기획 대분류를 선택하세요!");
                return;
            }

            popup = new frmMM15_Pop01();
            popup.FormClosed += popup_FormClosed;

            popup.Id = Convert.ToInt32(txtId.EditValue.ToString());
            popup.pm_name = txtId_Name.EditValue.ToString();

            popup.ShowDialog();
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }

        private void popup1_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup1.FormClosed -= popup1_FormClosed;

            popup1 = null;
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageAgent.MessageShow(MessageType.Confirm, "선택된 품목을 제외 하시겠습니까?") == DialogResult.OK)
                {
                    var saveResult = new SaveTableResultInfo() { IsError = true };

                    var dt = efwGridControl2.GetChangeDataWithRowState;
                    //var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        //string sCHK = string.Empty;
                        //sCHK = dt.Rows[i]["chk"].ToString();

                        if (dt.Rows[i]["chk"].ToString() == "Y")
                        //if (dt.Rows[i][StatusColumn].ToString() == "U")
                        {
                            //Console.WriteLine("------------------------------------------------------------");
                            //Console.WriteLine("[U] " + dt.Rows[i]["o_code"].ToString());
                            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_DELETE_01", con))
                                {

                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_id", MySqlDbType.Int32, 5);
                                    cmd.Parameters[0].Value = Convert.ToInt32(dt.Rows[i]["pm_id"]).ToString();

                                    cmd.Parameters.Add("i_pm_prodser", MySqlDbType.Int32, 5);
                                    cmd.Parameters[1].Value = Convert.ToInt32(dt.Rows[i]["pm_prodser"]).ToString();

                                    cmd.Parameters.Add("i_p_id", MySqlDbType.Int32, 50);
                                    cmd.Parameters[2].Value = Convert.ToInt32(dt.Rows[i]["p_id"]).ToString();

                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            Search();
            Open1();
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void txtPm_Name_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCategory_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPm_order.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "순서를 입력 하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_SAVE_01", con))
                        {   // UserInfo.instance().UserId
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            //cmd.Parameters.Add(new MySqlParameter("i_user_id", MySqlDbType.VarChar));
                            //cmd.Parameters["i_user_id"].Value = UserInfo.instance().LOGIN_ID;
                            //cmd.Parameters["i_user_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "P";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                            cmd.Parameters["i_id"].Value = Convert.ToInt32(txtId.EditValue);
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pm_order", MySqlDbType.Int32));
                            cmd.Parameters["i_pm_order"].Value = Convert.ToInt32(txtPm_order.EditValue);
                            cmd.Parameters["i_pm_order"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_id_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_id_name"].Value = txtId_Name.EditValue; 
                            cmd.Parameters["i_id_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pm_use_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_pm_use_type"].Value = rbPm_Use_Type.EditValue; 
                            cmd.Parameters["i_pm_use_type"].Direction = ParameterDirection.Input;


                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                Search();
            }
        }

        private void btnCategory_Delete_Click(object sender, EventArgs e)
        {

            if (MessageAgent.MessageShow(MessageType.Confirm, "(주의) 삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_SAVE_01", con))
                        {   // UserInfo.instance().UserId
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            //cmd.Parameters.Add(new MySqlParameter("i_user_id", MySqlDbType.VarChar));
                            //cmd.Parameters["i_user_id"].Value = UserInfo.instance().LOGIN_ID;
                            //cmd.Parameters["i_user_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "D";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                            cmd.Parameters["i_id"].Value = Convert.ToInt32(txtId.EditValue);
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pm_order", MySqlDbType.Int32));
                            cmd.Parameters["i_pm_order"].Value = Convert.ToInt32(txtPm_order.EditValue);
                            cmd.Parameters["i_pm_order"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_id_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_id_name"].Value = txtId_Name.EditValue;
                            cmd.Parameters["i_id_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pm_use_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_pm_use_type"].Value = rbPm_Use_Type.EditValue;
                            cmd.Parameters["i_pm_use_type"].Direction = ParameterDirection.Input;


                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                MessageAgent.MessageShow(MessageType.Informational, "삭제 되었습니다.");
                Search();
            }
        }

        private void efwSimpleButton7_Click_1(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(this.txtPm_prodser.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "상품을 선택하세요!");
                return;
            }

            // 도마
            string sDoma_Rate = string.Empty;
            sDoma_Rate = string.IsNullOrEmpty(this.txtDoma_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtDoma_Rate.Text)) || (txtDoma_Rate.EditValue.ToString() == "0"))
            {
                sDoma_Rate = "0";
            }
            if ((sDoma_Rate == "0" && cmbDoma_Money.EditValue.ToString() != "0") || (sDoma_Rate != "0" && cmbDoma_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "일반도마 할인율과 사용머니를 선택하세요!");
                return;
            }

            // Vip
            string sVip_Rate = string.Empty;
            sVip_Rate = string.IsNullOrEmpty(this.txtVip_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtVip_Rate.Text)) || (txtVip_Rate.EditValue.ToString() == "0"))
            {
                sVip_Rate = "0";
            }
            if ((sVip_Rate == "0" && cmbDoma_Money.EditValue.ToString() != "0") || (sVip_Rate != "0" && cmbVip_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "VIP 할인율과 사용머니를 선택하세요!");
                return;
            }

            // Chef
            string sChef_Rate = string.Empty;
            sChef_Rate = string.IsNullOrEmpty(this.txtChef_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtChef_Rate.Text)) || (txtChef_Rate.EditValue.ToString() == "0"))
            {
                sChef_Rate = "0";
            }
            if ((sChef_Rate == "0" && cmbChef_Money.EditValue.ToString() != "0") || (sChef_Rate != "0" && cmbChef_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "G 매니저 할인율과 사용머니를 선택하세요!");
                return;
            }

            // Md
            string sMd_Rate = string.Empty;
            sMd_Rate = string.IsNullOrEmpty(this.txtMd_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtMd_Rate.Text)) || (txtMd_Rate.EditValue.ToString() == "0"))
            {
                sMd_Rate = "0";
            }
            if ((sMd_Rate == "0" && cmbMd_Money.EditValue.ToString() != "0") || (sMd_Rate != "0" && cmbMd_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "셰프이사 할인율과 사용머니를 선택하세요!");
                return;
            }

            // Gshop
            string sGshop_Rate = string.Empty;
            sGshop_Rate = string.IsNullOrEmpty(this.txtGshop_Rate.Text).ToString();
            if ((string.IsNullOrEmpty(this.txtGshop_Rate.Text)) || (txtGshop_Rate.EditValue.ToString() == "0"))
            {
                sGshop_Rate = "0";
            }
            if ((sGshop_Rate == "0" && cmbGshop_Money.EditValue.ToString() != "0") || (sGshop_Rate != "0" && cmbGshop_Money.EditValue.ToString() == "0"))
            {
                MessageAgent.MessageShow(MessageType.Warning, "G 멀티샵 할인율과 사용머니를 선택하세요!");
                return;
            }



            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.Add(new MySqlParameter("i_Id", MySqlDbType.Int32));
                            cmd.Parameters["i_Id"].Value = Convert.ToInt32(txtId.EditValue);
                            cmd.Parameters["i_Id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pm_prodser", MySqlDbType.Int32));
                            cmd.Parameters["i_pm_prodser"].Value = Convert.ToInt32(txtPm_prodser.EditValue);
                            cmd.Parameters["i_pm_prodser"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Id", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Id"].Value = Convert.ToInt32(txtP_Id.EditValue);
                            cmd.Parameters["i_P_Id"].Direction = ParameterDirection.Input;

                            // doma
                            int nDoma_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtDoma_Rate.Text))
                               nDoma_Rate = 0;
                            else
                                nDoma_Rate = Convert.ToInt32(txtDoma_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_doma_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_doma_rate"].Value = nDoma_Rate;
                            cmd.Parameters["i_doma_rate"].Direction = ParameterDirection.Input;

                            // Vip
                            int nVip_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtVip_Rate.Text))
                                nVip_Rate = 0;
                            else
                                nVip_Rate = Convert.ToInt32(txtVip_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_vip_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_vip_rate"].Value = nVip_Rate;
                            cmd.Parameters["i_vip_rate"].Direction = ParameterDirection.Input;

                            // Chef
                            int nChef_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtChef_Rate.Text))
                                nChef_Rate = 0;
                            else
                                nChef_Rate = Convert.ToInt32(txtChef_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_chef_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_chef_rate"].Value = nChef_Rate;
                            cmd.Parameters["i_chef_rate"].Direction = ParameterDirection.Input;

                            // MD
                            int nMd_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtMd_Rate.Text))
                                nMd_Rate = 0;
                            else
                                nMd_Rate = Convert.ToInt32(txtMd_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_md_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_md_rate"].Value = nMd_Rate;
                            cmd.Parameters["i_md_rate"].Direction = ParameterDirection.Input;

                            // Gshop
                            int nGshop_Rate = 0;
                            if (string.IsNullOrEmpty(this.txtGshop_Rate.Text))
                                nGshop_Rate = 0;
                            else
                                nGshop_Rate = Convert.ToInt32(txtGshop_Rate.EditValue);
                            cmd.Parameters.Add(new MySqlParameter("i_gshop_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_rate"].Value = nGshop_Rate;
                            cmd.Parameters["i_gshop_rate"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_doma_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_doma_money"].Value = cmbDoma_Money.EditValue;
                            cmd.Parameters["i_doma_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_vip_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_vip_money"].Value = cmbVip_Money.EditValue;
                            cmd.Parameters["i_vip_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_chef_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_chef_money"].Value = cmbChef_Money.EditValue;
                            cmd.Parameters["i_chef_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_money"].Value = cmbMd_Money.EditValue;
                            cmd.Parameters["i_md_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_money", MySqlDbType.VarChar));
                            cmd.Parameters["i_gshop_money"].Value = cmbGshop_Money.EditValue;
                            cmd.Parameters["i_gshop_money"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_banner", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_banner"].Value = rbIs_Banner.EditValue;
                            cmd.Parameters["i_is_banner"].Direction = ParameterDirection.Input;

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
            Open1();
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtId.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "기획 대분류를 선택하세요!");
                return;
            }

            popup1 = new frmMM15_Pop02();
            popup1.FormClosed += popup1_FormClosed;

            popup1.Id = Convert.ToInt32(txtId.EditValue.ToString());
            popup1.pm_name = txtId_Name.EditValue.ToString();

            popup1.ShowDialog();
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageAgent.MessageShow(MessageType.Confirm, "선택된 품목의 순서를 변경 하시겠습니까?") == DialogResult.OK)
                {
                    var saveResult = new SaveTableResultInfo() { IsError = true };

                    var dt = efwGridControl2.GetChangeDataWithRowState;
                    //var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        //string sCHK = string.Empty;
                        //sCHK = dt.Rows[i]["chk"].ToString();

                        //if (dt.Rows[i]["chk"].ToString() == "Y")
                        //if (dt.Rows[i][StatusColumn].ToString() == "U")
                        {
                            //Console.WriteLine("------------------------------------------------------------");
                            //Console.WriteLine("[U] " + dt.Rows[i]["o_code"].ToString());
                            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_SAVE_05", con))
                                {

                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_id", MySqlDbType.Int32, 5);
                                    cmd.Parameters[0].Value = Convert.ToInt32(dt.Rows[i]["pm_id"]).ToString();

                                    cmd.Parameters.Add("i_pm_prodser", MySqlDbType.Int32, 5);
                                    cmd.Parameters[1].Value = Convert.ToInt32(dt.Rows[i]["pm_prodser"]).ToString();

                                    cmd.Parameters.Add("i_p_id", MySqlDbType.Int32, 50);
                                    cmd.Parameters[2].Value = Convert.ToInt32(dt.Rows[i]["p_id"]).ToString();

                                    cmd.Parameters.Add("i_p_order", MySqlDbType.Int32, 50);
                                    cmd.Parameters[3].Value = Convert.ToInt32(dt.Rows[i]["p_order"]).ToString();

                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            Search();
            Open1();
        }
    }
}

