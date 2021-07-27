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
    public partial class frmMM15_Pop01 : FrmPopUpBase
    {
        public int Id { get; set; }
        public string pm_name { get; set; }


        frmMM15_Pop01 popup;
        public frmMM15_Pop01()
        {
            InitializeComponent();
            gridView2.CustomUnboundColumnData += gridView2_CustomUnboundColumnData;
            gridView3.CustomUnboundColumnData += gridView3_CustomUnboundColumnData;
        }
        
        private void frmMM15_Pop01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");
            gridView2.OptionsView.ShowFooter = true;
            txtPm_id.EditValue = Id;
            rbShowType.EditValue = "T";
            SetCmb();
            Open1();
        }

        private void SetCmb()
        {
            // 공급자구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '0' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(s_idx,'') as DCODE ,s_company_name as DNAME  FROM domaadmin.tb_sellers_info where s_status = 'Y'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbSellers, codeArray);

            }
            cmbSellers.EditValue = "0";



            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select code_id as DCODE, code_nm as DNAME  FROM domaadmin.tb_common_code  where gcode_id = '00030'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbQuery, codeArray);
            }
            cmbQuery.EditValue = "0";



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
                        cmd.Parameters[0].Value = txtPm_id.EditValue;

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


        private void efwSimpleButton8_Click_1(object sender, EventArgs e)
        {
            Open2();
        }
        public void Open2()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = cmbSellers.EditValue;

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbQuery.EditValue.ToString();

                        cmd.Parameters.Add("i_ProdName", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtProdName.EditValue;

                        if (rbShowType.EditValue.ToString() != "Y" && rbShowType.EditValue.ToString() != "N")
                            sShow_Type = null;
                        else
                            sShow_Type = rbShowType.EditValue.ToString();

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = sShow_Type;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
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

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView3.RowCount; i++)
                gridView3.SetRowCellValue(i, gridView3.Columns["chk"], "Y");
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView3.RowCount; i++)
                gridView3.SetRowCellValue(i, gridView3.Columns["chk"], "N");
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                var saveResult = new SaveTableResultInfo() { IsError = true };

                var dt = efwGridControl3.GetChangeDataWithRowState;
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
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM15_SAVE_03", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_pm_id", MySqlDbType.Int32, 5);
                                cmd.Parameters[0].Value = Convert.ToInt32(txtPm_id.EditValue);

                                cmd.Parameters.Add("i_p_id", MySqlDbType.Int32, 50);
                                cmd.Parameters[1].Value = Convert.ToInt32(dt.Rows[i]["id"]).ToString();

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            Open1();
        }

        private void txtProdName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Open2();
        }
    }
}

