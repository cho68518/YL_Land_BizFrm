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
    public partial class frmMM14 : FrmBase
    {
        frmMM03_Pop01 popup;
        public frmMM14()
        {
            InitializeComponent();
            this.QCode = "MM03";
            //폼명설정
            this.FrmName = "상품 세부정보";

            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

            this.efwGridControl1.BindControlSet(
                new ColumnControlSet("id", txtP_Id)
              , new ColumnControlSet("p_name", txtP_Name)
              );

            this.efwGridControl1.Click += efwGridControl1_Click;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            txtP_Id.EditValue = 0;
            if (dr != null && dr["id"].ToString() != "0")
            {
                this.txtP_Id.EditValue = dr["id"].ToString();
                this.txtP_Name.EditValue = dr["p_name"].ToString();

            }
            Open1();
        }

        public override void FrmLoadEvent()
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
            rbShowType.EditValue = "T";
            SetCmb();

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

        void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
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
        public override void Search()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SELECT_01", con))
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

        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();


        private void BtnDispYes_Click(object sender, EventArgs e)
        {

            popup = new frmMM03_Pop01();

            popup.Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString());

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }

        private void txtProdName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void Open1()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM14_SELECT_01", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("i_p_id", MySqlDbType.Int32));
                        cmd.Parameters["i_p_id"].Value = Convert.ToInt32(txtP_Id.EditValue);
                        cmd.Parameters["i_p_id"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_doma_rate", MySqlDbType.Int32));
                        cmd.Parameters["o_doma_rate"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_vip_rate", MySqlDbType.Int32));
                        cmd.Parameters["o_vip_rate"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_chef_rate", MySqlDbType.Int32));
                        cmd.Parameters["o_chef_rate"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_md_rate", MySqlDbType.Int32));
                        cmd.Parameters["o_md_rate"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_gshop_rate", MySqlDbType.Int32));
                        cmd.Parameters["o_gshop_rate"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_doma_money", MySqlDbType.VarChar));
                        cmd.Parameters["o_doma_money"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_vip_money", MySqlDbType.VarChar));
                        cmd.Parameters["o_vip_money"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_chef_money", MySqlDbType.VarChar));
                        cmd.Parameters["o_chef_money"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_md_money", MySqlDbType.VarChar));
                        cmd.Parameters["o_md_money"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_gshop_money", MySqlDbType.VarChar));
                        cmd.Parameters["o_gshop_money"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        txtdoma_rate.EditValue = cmd.Parameters["o_doma_rate"].Value.ToString();
                        txtvip_rate.EditValue = cmd.Parameters["o_vip_rate"].Value.ToString();
                        txtchef_rate.EditValue = cmd.Parameters["o_chef_rate"].Value.ToString();
                        txtmd_rate.EditValue = cmd.Parameters["o_md_rate"].Value.ToString();
                        txtgshop_rate.EditValue = cmd.Parameters["o_gshop_rate"].Value.ToString();

                        cmbDoma_Money.EditValue = cmd.Parameters["o_doma_money"].Value.ToString();
                        cmbVip_Money.EditValue = cmd.Parameters["o_vip_money"].Value.ToString();
                        cmbChef_Money.EditValue = cmd.Parameters["o_chef_money"].Value.ToString();
                        cmbMd_Money.EditValue = cmd.Parameters["o_md_money"].Value.ToString();
                        cmbGshop_Money.EditValue = cmd.Parameters["o_gshop_money"].Value.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            // 필수항목 CHECK txtP_Img

            if (string.IsNullOrEmpty(this.txtP_Name.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "상품을 선택하세요!");
                txtP_Name.Focus();
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM14_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Id", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Id"].Value = Convert.ToInt32(txtP_Id.EditValue);
                            cmd.Parameters["i_P_Id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_doma_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_doma_rate"].Value = Convert.ToInt32(txtdoma_rate.EditValue);
                            cmd.Parameters["i_doma_rate"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_vip_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_vip_rate"].Value = Convert.ToInt32(txtvip_rate.EditValue);
                            cmd.Parameters["i_vip_rate"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_chef_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_chef_rate"].Value = Convert.ToInt32(txtchef_rate.EditValue);
                            cmd.Parameters["i_chef_rate"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_md_rate"].Value = Convert.ToInt32(txtmd_rate.EditValue);
                            cmd.Parameters["i_md_rate"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_rate", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_rate"].Value = Convert.ToInt32(txtgshop_rate.EditValue);
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
    }
}
