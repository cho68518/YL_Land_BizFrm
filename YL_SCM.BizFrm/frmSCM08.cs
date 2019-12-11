using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Common.PopUp;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_SCM.BizFrm;
using Tamir.SharpSsh;
using System.Collections;
using System.Net.Sockets;
using System.Net;

namespace YL_SCM.BizFrm
{
    public partial class frmSCM08 : FrmBase
    {
        public frmSCM08()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "MM03";
            //폼명설정
            this.FrmName = "제품코드 등록";

        }
        frmSCM08_Pop01 popup;

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

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "select concat('y1044_',lpad(max(substring(p_code,7,9)) + 1,9,'0')) from domamall.tb_am_product_masters " +
                             " where substring(p_code, 1, 6) = 'y1044_' ";
                DataSet ds = sql.selectQueryDataSet();

                txtP_Code.EditValue = sql.selectQueryForSingleValue();
            }
            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
            new ColumnControlSet("id", txtP_Id)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;
            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
            rbPC_Use_Type.EditValue = "N";
            rbShowType.EditValue = "T";
            
            New();

            SetCmb();
        }
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            
            if (dr != null && dr["id"].ToString() != "")
            {
                Open1();
            }
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

        private void SetCmb()
        {
            // 공급자구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(s_idx,'') as DCODE ,s_company_name as DNAME  FROM domaadmin.tb_sellers_info where s_status = 'Y'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_Seller_Id, codeArray);
            }
            cmbP_Seller_Id.EditValue = "1";
            

            // 샵구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00016' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbShops_Type, codeArray);
            }

            // 노출여부
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00017' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_Show_Type, codeArray);
            }
            cmbP_Show_Type.EditValue = "N";
            // 상태
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00018' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_Sell_Type, codeArray);
            }

            // VIP 이상노출 여부
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00019' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_Chef_Level, codeArray);
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT c_code as DCODE, c_name as DNAME  FROM domamall.tb_cate_masters " +
                    "         where c_level = 1 " +
                    "         group by c_code, c_name ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCate_Code1, codeArray);
            }
            // 전자상거래
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT c_code as DCODE, c_name as DNAME  FROM domamall.tb_cate_masters " +
                    "         where c_parent_code = 'DC002' " +
                    "         group by c_code, c_name ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_EC_Code, codeArray);
            }


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00020' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_Delivery_Type, codeArray);

            }


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00021' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_Delivery_Type, codeArray);


                CodeAgent.MakeCodeControl(this.cmbP_Taxation, codeArray);
            }


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00024' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_PS_Num, codeArray);
            }
 
        }


        #region SET CATE COMBO Event()
        private void SetLegacyCode_Mysql(RepositoryItemLookUpEdit cdControl, string codeGroup)
        {
            DataTable retDT = CodeAgent.GetLegacyCodeCollection(codeGroup);


            cdControl.DataSource = retDT;
            //컨트롤 초기화
            InitCodeControl(cdControl);
        }

        private void InitCodeControl(object cdControl)
        {
            string DNAME = string.Empty;

            DNAME = "DNAME";

            CodeAgent.InitCodeControl(cdControl, "코드명", "코드", DNAME, "DCODE", "선택하세요");
        }



        private void cmbCate_Code1_EditValueChanged(object sender, EventArgs e)
        {


            if (this.cmbCate_Code1.EditValue == null)
            {
                return;
            }
            string sCate_Code1 = cmbCate_Code1.EditValue.ToString();

            cmbCate_Code2.Properties.DataSource = null;
            cmbCate_Code3.Properties.DataSource = null;
            cmbCate_Code4.Properties.DataSource = null;
            cmbCate_Code2.EditValue = "0";
            cmbCate_Code3.EditValue = "0";
            cmbCate_Code4.EditValue = "0";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "select '0' as DCODE, '선택하세요' as DNAME  " +
                    "        union all " +
                    "        select c_code as DCODE, c_name as DNAME  FROM domamall.tb_cate_masters " +
                    "         where substr(c_code,1,7)  = " + "'" + sCate_Code1 + "'" + " and " +
                    "               c_level = 2 " +
                    "         group by c_code, c_name ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];


                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCate_Code2, codeArray);
            }
            set1();
        }

        private void set1()
        {
            if (this.cmbCate_Code2.EditValue == null)
            {
                return;
            }

            cmbCate_Code3.Properties.DataSource = null;
            cmbCate_Code4.Properties.DataSource = null;

            string sCate_Code1 = cmbCate_Code1.EditValue.ToString();
            string sCate_Code2 = cmbCate_Code2.EditValue.ToString();

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "select '0' as DCODE, '선택하세요' as DNAME  " +
                    "        union all " +
                    "        SELECT c_code as DCODE, c_name as DNAME  FROM domamall.tb_cate_masters " +
                    "         where substr(c_code,1,7)  = " + "'" + sCate_Code1 + "'" + " and " +
                    "               substr(c_code,1,9)  = " + "'" + sCate_Code2 + "'" + " and " +
                    "               c_level = 3 " +
                    "         group by c_code, c_name ";

                DataSet ds2 = con.selectQueryDataSet();
                DataRow[] dr2 = ds2.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr2.Length];


                for (int i = 0; i < dr2.Length; i++)
                    codeArray[i] = new CodeData(dr2[i]["DCODE"].ToString(), dr2[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCate_Code3, codeArray);


            }
        }


        private void cmbCate_Code2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbCate_Code2.EditValue == null)
            {
                return;
            }

            cmbCate_Code3.Properties.DataSource = null;

            cmbCate_Code4.Properties.DataSource = null;
            string sCate_Code1 = cmbCate_Code1.EditValue.ToString();
            string sCate_Code2 = cmbCate_Code2.EditValue.ToString();

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "select '0' as DCODE, '선택하세요' as DNAME  " +
                    "        union all " +
                    "        SELECT c_code as DCODE, c_name as DNAME  FROM domamall.tb_cate_masters " +
                    "         where substr(c_code,1,7)  = " + "'" + sCate_Code1 + "'" + " and " +
                    "               substr(c_code,1,9)  = " + "'" + sCate_Code2 + "'" + " and " +
                    "               c_level = 3 " +
                    "         group by c_code, c_name ";

                DataSet ds2 = con.selectQueryDataSet();
                DataRow[] dr2 = ds2.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr2.Length];


                for (int i = 0; i < dr2.Length; i++)
                    codeArray[i] = new CodeData(dr2[i]["DCODE"].ToString(), dr2[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCate_Code3, codeArray);


            }
        }

        private void cmbCate_Code3_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbCate_Code3.EditValue == null)
            {
                return;
            }

            cmbCate_Code4.Properties.DataSource = null;

            string sCate_Code1 = cmbCate_Code1.EditValue.ToString();
            string sCate_Code2 = cmbCate_Code2.EditValue.ToString();
            string sCate_Code3 = cmbCate_Code3.EditValue.ToString();

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "select '0' as DCODE, '선택하세요' as DNAME  " +
                    "        union all " +
                    "        SELECT c_code as DCODE, c_name as DNAME  FROM domamall.tb_cate_masters " +
                    "         where substr(c_code,1,7)  = " + "'" + sCate_Code1 + "'" + " and " +
                    "               substr(c_code,1,9)  = " + "'" + sCate_Code2 + "'" + " and " +
                    "               substr(c_code,1,11) = " + "'" + sCate_Code3 + "'" + " and " +
                    "               c_level = 4 " +
                    "         group by c_code, c_name ";

                DataSet ds3 = con.selectQueryDataSet();
                DataRow[] dr3 = ds3.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr3.Length];


                for (int i = 0; i < dr3.Length; i++)
                    codeArray[i] = new CodeData(dr3[i]["DCODE"].ToString(), dr3[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCate_Code4, codeArray);
            }
        }
        #endregion


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

                        if (cmbP_Seller_Id.EditValue.ToString() != "Y" && cmbP_Seller_Id.EditValue.ToString() != "N")
                            sShowType = null;
                        else
                            sShowType = cmbP_Seller_Id.EditValue.ToString();

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = sShowType;

                        cmd.Parameters.Add("i_ProdName", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtProdName.EditValue;

                        if (rbShowType.EditValue.ToString() != "Y" && rbShowType.EditValue.ToString() != "N")
                            sShow_Type = null;
                        else
                            sShow_Type = rbShowType.EditValue.ToString();

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[2].Value = sShow_Type;
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

        private void Open1()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SELECT_02", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("i_p_id", MySqlDbType.Int32));
                        cmd.Parameters["i_p_id"].Value = Convert.ToInt32(txtP_Id.EditValue);
                        cmd.Parameters["i_p_id"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_p_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_c_code1", MySqlDbType.VarChar));
                        cmd.Parameters["o_c_code1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_c_code2", MySqlDbType.VarChar));
                        cmd.Parameters["o_c_code2"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_c_code3", MySqlDbType.VarChar));
                        cmd.Parameters["o_c_code3"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_c_code4", MySqlDbType.VarChar));
                        cmd.Parameters["o_c_code4"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_code", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_code"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_seller_id", MySqlDbType.VarChar));
                        cmd.Parameters["o_seller_id"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_min_purchase_num", MySqlDbType.VarChar));
                        cmd.Parameters["o_min_purchase_num"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_multi_discount_cost", MySqlDbType.VarChar));
                        cmd.Parameters["o_multi_discount_cost"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_max_purchase_num", MySqlDbType.VarChar));
                        cmd.Parameters["o_max_purchase_num"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_compare_text", MySqlDbType.VarChar));
                        cmd.Parameters["o_compare_text"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_ompare_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_ompare_type"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_show_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_show_type"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_chef_level", MySqlDbType.VarChar));
                        cmd.Parameters["o_chef_level"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_sell_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_sell_type"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_max_send_num", MySqlDbType.VarChar));
                        cmd.Parameters["o_max_send_num"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_ec_code", MySqlDbType.VarChar));
                        cmd.Parameters["o_ec_code"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_img", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_img"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_img2", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_img2"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_contents", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_contents"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_delivery_price", MySqlDbType.Int32));
                        cmd.Parameters["o_p_delivery_price"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_delivery_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_delivery_type"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_basic_price", MySqlDbType.Int32));
                        cmd.Parameters["o_basic_price"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_vip_price", MySqlDbType.Int32));
                        cmd.Parameters["o_vip_price"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_ps_oper_price", MySqlDbType.Int32));
                        cmd.Parameters["o_ps_oper_price"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_ps_num", MySqlDbType.Int32));
                        cmd.Parameters["o_p_ps_num"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_taxation", MySqlDbType.Int32));
                        cmd.Parameters["o_p_taxation"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_org_price", MySqlDbType.Int32));
                        cmd.Parameters["o_p_org_price"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_discount_price", MySqlDbType.Int32));
                        cmd.Parameters["o_p_discount_price"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_compare_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_compare_type"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_discount_donut1", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_discount_donut1"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_discount_donut2", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_discount_donut2"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_discount_donut3", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_discount_donut3"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_price_show_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_price_show_type"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_refunds_dm", MySqlDbType.VarChar));
                        cmd.Parameters["o_refunds_dm"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_refunds_gd", MySqlDbType.Int32));
                        cmd.Parameters["o_refunds_gd"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_refunds_td", MySqlDbType.Int32));
                        cmd.Parameters["o_refunds_td"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_refunds_gm", MySqlDbType.Int32));
                        cmd.Parameters["o_refunds_gm"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_refunds_gf", MySqlDbType.Int32));
                        cmd.Parameters["o_refunds_gf"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_refunds_as", MySqlDbType.Int32));
                        cmd.Parameters["o_refunds_as"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_refunds_cg", MySqlDbType.Int32));
                        cmd.Parameters["o_refunds_cg"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_refunds_cr", MySqlDbType.Int32));
                        cmd.Parameters["o_refunds_cr"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_pc_title", MySqlDbType.VarChar));
                        cmd.Parameters["o_pc_title"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_pc_thumbnail", MySqlDbType.VarChar));
                        cmd.Parameters["o_pc_thumbnail"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_pc_content", MySqlDbType.VarChar));
                        cmd.Parameters["o_pc_content"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_pc_use_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_pc_use_type"].Direction = ParameterDirection.Output;


                        cmd.Parameters.Add(new MySqlParameter("o_shops_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_shops_type"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        txtP_Name.EditValue = cmd.Parameters["o_p_name"].Value.ToString();
                        txtC_Code1.EditValue = cmd.Parameters["o_c_code1"].Value.ToString();
                        txtC_Code2.EditValue = cmd.Parameters["o_c_code2"].Value.ToString();
                        txtC_Code3.EditValue = cmd.Parameters["o_c_code3"].Value.ToString();
                        txtC_Code4.EditValue = cmd.Parameters["o_c_code4"].Value.ToString();

                        Query_ComBo_Set();

                        txtP_Code.EditValue = cmd.Parameters["o_p_code"].Value.ToString();

                        cmbP_Seller_Id.EditValue = cmd.Parameters["o_seller_id"].Value.ToString();
                        txtP_Min_Purchase_Num.EditValue = cmd.Parameters["o_min_purchase_num"].Value.ToString();
                        txtP_Multi_Discount_Cost.EditValue = cmd.Parameters["o_multi_discount_cost"].Value.ToString();
                        txtP_Max_Purchase_Num.EditValue = cmd.Parameters["o_max_purchase_num"].Value.ToString();
                        txtP_Compare_Text.EditValue = cmd.Parameters["o_compare_text"].Value.ToString();
                        rbP_Compare_Type.EditValue = cmd.Parameters["o_ompare_type"].Value.ToString();
                        cmbP_Show_Type.EditValue = cmd.Parameters["o_show_type"].Value.ToString();
                        cmbP_Chef_Level.EditValue = cmd.Parameters["o_chef_level"].Value.ToString();
                        cmbP_Sell_Type.EditValue = cmd.Parameters["o_sell_type"].Value.ToString();
                        txtP_Max_Send_Num.EditValue = cmd.Parameters["o_max_send_num"].Value.ToString();
                        cmbP_EC_Code.EditValue = cmd.Parameters["o_ec_code"].Value.ToString();

                        txtP_Img.EditValue = cmd.Parameters["o_p_img"].Value.ToString();
                        picP_IMG.LoadAsync(cmd.Parameters["o_p_img"].Value.ToString());

                        txtP_Img2.EditValue = cmd.Parameters["o_p_img2"].Value.ToString();
                        picP_IMG2.LoadAsync(cmd.Parameters["o_p_img2"].Value.ToString());

                        txtP_Contents.EditValue = cmd.Parameters["o_p_contents"].Value.ToString();
                        picP_CONTENTS.LoadAsync(cmd.Parameters["o_p_contents"].Value.ToString());

                        txtP_Delivery_Price.EditValue = cmd.Parameters["o_p_delivery_price"].Value.ToString();
                        cmbP_Delivery_Type.EditValue = cmd.Parameters["o_p_delivery_type"].Value.ToString();
                        txtBasic_Price.EditValue = cmd.Parameters["o_basic_price"].Value.ToString();
                        txtVip_Price.EditValue = cmd.Parameters["o_vip_price"].Value.ToString();
                        txtPs_Oper_Price.EditValue = cmd.Parameters["o_ps_oper_price"].Value.ToString();
                        cmbP_PS_Num.EditValue = cmd.Parameters["o_p_ps_num"].Value.ToString();
                        cmbP_Taxation.EditValue = cmd.Parameters["o_p_taxation"].Value.ToString();
                        txtP_Org_Price.EditValue = cmd.Parameters["o_p_org_price"].Value.ToString();
                        txtDiscount_Price.EditValue = cmd.Parameters["o_p_discount_price"].Value.ToString();
                        rbP_Compare_Type.EditValue = cmd.Parameters["o_p_compare_type"].Value.ToString();
                        cbP_Discount_Donut1.EditValue = cmd.Parameters["o_p_discount_donut1"].Value.ToString();
                        cbP_Discount_Donut2.EditValue = cmd.Parameters["o_p_discount_donut2"].Value.ToString();
                        cbP_Discount_Donut3.EditValue = cmd.Parameters["o_p_discount_donut3"].Value.ToString();

                        rbPrice_Show_Type.EditValue = cmd.Parameters["o_price_show_type"].Value.ToString();

                        txtRefunds_DM.EditValue = cmd.Parameters["o_refunds_dm"].Value.ToString();
                        txtRefunds_GD.EditValue = cmd.Parameters["o_refunds_gd"].Value.ToString();
                        txtRefunds_TD.EditValue = cmd.Parameters["o_refunds_td"].Value.ToString();
                        txtRefunds_GM.EditValue = cmd.Parameters["o_refunds_gm"].Value.ToString();
                        txtRefunds_GF.EditValue = cmd.Parameters["o_refunds_gf"].Value.ToString();
                        txtRefunds_AS.EditValue = cmd.Parameters["o_refunds_as"].Value.ToString();
                        txtRefunds_CG.EditValue = cmd.Parameters["o_refunds_cg"].Value.ToString();
                        txtRefunds_CR.EditValue = cmd.Parameters["o_refunds_cr"].Value.ToString();

                        txtPC_Title.EditValue = cmd.Parameters["o_pc_title"].Value.ToString();
                        txtPc_Thumbnail.EditValue = cmd.Parameters["o_pc_thumbnail"].Value.ToString();
                        picPc_Thumbnail.LoadAsync(cmd.Parameters["o_pc_thumbnail"].Value.ToString());
                        rbPC_Use_Type.EditValue = cmd.Parameters["o_pc_use_type"].Value.ToString();
                        txtPC_Content.EditValue = cmd.Parameters["o_pc_content"].Value.ToString();
                        cmbShops_Type.EditValue = cmd.Parameters["o_shops_type"].Value.ToString();



                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        private void Query_ComBo_Set()
        {
            if (this.txtC_Code1.EditValue == null)
                return;
            else
                cmbCate_Code1.EditValue = txtC_Code1.EditValue.ToString();

            if (this.txtC_Code2.EditValue == null)
                return;
            else
                cmbCate_Code2.EditValue = txtC_Code2.EditValue.ToString();

            if (this.txtC_Code3.EditValue == null)
                return;
            else
                cmbCate_Code3.EditValue = txtC_Code3.EditValue.ToString();

            if (this.txtC_Code4.EditValue == null)
                return;
            else
                cmbCate_Code4.EditValue = txtC_Code4.EditValue.ToString();

        }

        private void bthNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void New()
        {
            cmbShops_Type.EditValue = "0";
            cmbCate_Code1.Properties.DataSource = null;
            cmbCate_Code2.Properties.DataSource = null;
            cmbCate_Code3.Properties.DataSource = null;
            cmbCate_Code4.Properties.DataSource = null;
            cmbCate_Code1.EditValue = "0";
            cmbCate_Code2.EditValue = "0";
            cmbCate_Code3.EditValue = "0";
            cmbCate_Code4.EditValue = "0";

            txtP_Name.EditValue = null;
            txtP_Id.EditValue = null;
            txtC_Code1.EditValue = null;
            txtC_Code2.EditValue = null;
            txtC_Code3.EditValue = null;
            txtC_Code4.EditValue = null;
            txtP_Img.EditValue = null;
            txtP_Img2.EditValue = null;
            txtP_Contents.EditValue = null;
            txtPc_Thumbnail.EditValue = null;
            txtPicPath1.EditValue = null;
            txtPicPath2.EditValue = null;
            txtPicPath3.EditValue = null;
            txtPicPath4.EditValue = null;

            txtP_Code.EditValue = null;
            txtP_Min_Purchase_Num.EditValue = "0";
            txtP_Multi_Discount_Cost.EditValue = "0";
            txtP_Max_Purchase_Num.EditValue = "0";
            rbP_Compare_Type.EditValue = "T";
            txtP_Compare_Text.EditValue = null;
            cmbP_Show_Type.EditValue = "N";
            cmbP_Sell_Type.EditValue = "Y";
            cmbP_Chef_Level.EditValue = "0";
            txtP_Max_Send_Num.EditValue = "0";
            cmbP_EC_Code.EditValue = "0";

            picP_IMG.EditValue = null;
            picP_IMG2.EditValue = null;
            picP_CONTENTS.EditValue = null;

            txtP_Delivery_Price.EditValue = null;
            cmbP_Delivery_Type.EditValue = "0";
            txtBasic_Price.EditValue = null;
            txtVip_Price.EditValue = null;
            txtPs_Oper_Price.EditValue = null;
            cmbP_PS_Num.EditValue = "0";
            cmbP_Taxation.EditValue = "0";
            txtP_Org_Price.EditValue = null;
            txtDiscount_Price.EditValue = null;

            cbP_Discount_Donut1.EditValue = null;
            cbP_Discount_Donut2.EditValue = null;
            cbP_Discount_Donut3.EditValue = null;
            rbPrice_Show_Type.EditValue = "1";

            txtRefunds_DM.EditValue = null;
            txtRefunds_GD.EditValue = null;
            txtRefunds_TD.EditValue = null;
            txtRefunds_GM.EditValue = null;
            txtRefunds_GF.EditValue = null;
            txtRefunds_AS.EditValue = null;
            txtRefunds_CG.EditValue = null;
            txtRefunds_CR.EditValue = null;
            rbPC_Use_Type.EditValue = "N";

            //gridView1.Columns.Clear();
            //gridView2.Columns.Clear();

            //while (gridView1.RowCount != 0)
            //{
            //    gridView1.SelectAll();
            //    gridView1.DeleteSelectedRows();
            //}

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "select concat('y1044_',lpad(max(substring(p_code,7,9)) + 1,9,'0')) from domamall.tb_am_product_masters " +
                             " where substring(p_code, 1, 6) = 'y1044_' ";
                DataSet ds = sql.selectQueryDataSet();

                txtP_Code.EditValue = sql.selectQueryForSingleValue();
            }

            SetCmb();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            // 필수항목 CHECK txtP_Img

            if (string.IsNullOrEmpty(this.txtP_Name.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "상품명을 입력하세요!");
                txtP_Name.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtP_Img.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "상품 이미지 썸네일 300*300을 선택해주세요 !");
                txtP_Name.Focus();
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Shops_Type", MySqlDbType.VarChar));
                            cmd.Parameters["i_Shops_Type"].Value = cmbShops_Type.EditValue;
                            cmd.Parameters["i_Shops_Type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Seller_Id", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Seller_Id"].Value = Convert.ToInt32(cmbP_Seller_Id.EditValue);
                            cmd.Parameters["i_P_Seller_Id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Id", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Id"].Value = Convert.ToInt32(txtP_Id.EditValue);
                            cmd.Parameters["i_P_Id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_C_Code1", MySqlDbType.VarChar));
                            cmd.Parameters["i_C_Code1"].Value = cmbCate_Code1.EditValue;
                            cmd.Parameters["i_C_Code1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_C_Code2", MySqlDbType.VarChar));
                            cmd.Parameters["i_C_Code2"].Value = cmbCate_Code2.EditValue;
                            cmd.Parameters["i_C_Code2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_C_Code3", MySqlDbType.VarChar));
                            cmd.Parameters["i_C_Code3"].Value = cmbCate_Code3.EditValue;
                            cmd.Parameters["i_C_Code3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_C_Code4", MySqlDbType.VarChar));
                            cmd.Parameters["i_C_Code4"].Value = cmbCate_Code4.EditValue;
                            cmd.Parameters["i_C_Code4"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Name", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Name"].Value = txtP_Name.EditValue;
                            cmd.Parameters["i_P_Name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Img", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Img"].Value = txtP_Img.EditValue;
                            cmd.Parameters["i_P_Img"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Img2", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Img2"].Value = txtP_Img2.EditValue;
                            cmd.Parameters["i_P_Img2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Contents", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Contents"].Value = txtP_Contents.EditValue;
                            cmd.Parameters["i_P_Contents"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Pc_Thumbnail", MySqlDbType.VarChar));
                            cmd.Parameters["i_Pc_Thumbnail"].Value = txtPc_Thumbnail.EditValue;
                            cmd.Parameters["i_Pc_Thumbnail"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Code"].Value = txtP_Code.EditValue;
                            cmd.Parameters["i_P_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Min_Purchase_Num", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Min_Purchase_Num"].Value = Convert.ToInt32(txtP_Min_Purchase_Num.EditValue);
                            cmd.Parameters["i_P_Min_Purchase_Num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Multi_Discount_Cost", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Multi_Discount_Cost"].Value = Convert.ToInt32(txtP_Multi_Discount_Cost.EditValue);
                            cmd.Parameters["i_P_Multi_Discount_Cost"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Max_Purchase_Num", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Max_Purchase_Num"].Value = Convert.ToInt32(txtP_Max_Purchase_Num.EditValue);
                            cmd.Parameters["i_P_Max_Purchase_Num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Compare_Type", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Compare_Type"].Value = rbP_Compare_Type.EditValue;
                            cmd.Parameters["i_P_Compare_Type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Compare_Text", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Compare_Text"].Value = txtP_Compare_Text.EditValue;
                            cmd.Parameters["i_P_Compare_Text"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Show_Type", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Show_Type"].Value = cmbP_Show_Type.EditValue;
                            cmd.Parameters["i_P_Show_Type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Sell_Type", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Sell_Type"].Value = cmbP_Sell_Type.EditValue;
                            cmd.Parameters["i_P_Sell_Type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Chef_Level", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Chef_Level"].Value = Convert.ToInt32(cmbP_Chef_Level.EditValue);
                            cmd.Parameters["i_P_Chef_Level"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Max_Send_Num", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Max_Send_Num"].Value = Convert.ToInt32(txtP_Max_Send_Num.EditValue);
                            cmd.Parameters["i_P_Max_Send_Num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_EC_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_EC_Code"].Value = cmbP_EC_Code.EditValue;
                            cmd.Parameters["i_P_EC_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Delivery_Price", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Delivery_Price"].Value = Convert.ToInt32(txtP_Delivery_Price.EditValue);
                            cmd.Parameters["i_P_Delivery_Price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Delivery_Type", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Delivery_Type"].Value = cmbP_Delivery_Type.EditValue;
                            cmd.Parameters["i_P_Delivery_Type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Basic_Price", MySqlDbType.Int32));
                            cmd.Parameters["i_Basic_Price"].Value = Convert.ToInt32(txtBasic_Price.EditValue);
                            cmd.Parameters["i_Basic_Price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Vip_Price", MySqlDbType.Int32));
                            cmd.Parameters["i_Vip_Price"].Value = Convert.ToInt32(txtVip_Price.EditValue);
                            cmd.Parameters["i_Vip_Price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Ps_Oper_Price", MySqlDbType.Int32));
                            cmd.Parameters["i_Ps_Oper_Price"].Value = Convert.ToInt32(txtPs_Oper_Price.EditValue);
                            cmd.Parameters["i_Ps_Oper_Price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_PS_Num", MySqlDbType.Int32));
                            cmd.Parameters["i_P_PS_Num"].Value = Convert.ToInt32(cmbP_PS_Num.EditValue);
                            cmd.Parameters["i_P_PS_Num"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Taxation", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Taxation"].Value = Convert.ToInt32(cmbP_Taxation.EditValue);
                            cmd.Parameters["i_P_Taxation"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Org_Price", MySqlDbType.Int32));
                            cmd.Parameters["i_P_Org_Price"].Value = Convert.ToInt32(txtP_Org_Price.EditValue);
                            cmd.Parameters["i_P_Org_Price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Discount_Price", MySqlDbType.Int32));
                            cmd.Parameters["i_Discount_Price"].Value = Convert.ToInt32(txtDiscount_Price.EditValue);
                            cmd.Parameters["i_Discount_Price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Discount_Donut1", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Discount_Donut1"].Value = cbP_Discount_Donut1.EditValue;
                            cmd.Parameters["i_P_Discount_Donut1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Discount_Donut2", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Discount_Donut2"].Value = cbP_Discount_Donut2.EditValue;
                            cmd.Parameters["i_P_Discount_Donut2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Discount_Donut3", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Discount_Donut3"].Value = cbP_Discount_Donut3.EditValue;
                            cmd.Parameters["i_P_Discount_Donut3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Price_Show_Type", MySqlDbType.Int32));
                            cmd.Parameters["i_Price_Show_Type"].Value = Convert.ToInt32(rbPrice_Show_Type.EditValue);
                            cmd.Parameters["i_Price_Show_Type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Refunds_DM", MySqlDbType.Int32));
                            cmd.Parameters["i_Refunds_DM"].Value = Convert.ToInt32(txtRefunds_DM.EditValue);
                            cmd.Parameters["i_Refunds_DM"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Refunds_GD", MySqlDbType.Int32));
                            cmd.Parameters["i_Refunds_GD"].Value = Convert.ToInt32(txtRefunds_GD.EditValue);
                            cmd.Parameters["i_Refunds_GD"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Refunds_TD", MySqlDbType.Int32));
                            cmd.Parameters["i_Refunds_TD"].Value = Convert.ToInt32(txtRefunds_TD.EditValue);
                            cmd.Parameters["i_Refunds_TD"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Refunds_GM", MySqlDbType.Int32));
                            cmd.Parameters["i_Refunds_GM"].Value = Convert.ToInt32(txtRefunds_GM.EditValue);
                            cmd.Parameters["i_Refunds_GM"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Refunds_GF", MySqlDbType.Int32));
                            cmd.Parameters["i_Refunds_GF"].Value = Convert.ToInt32(txtRefunds_GF.EditValue);
                            cmd.Parameters["i_Refunds_GF"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Refunds_AS", MySqlDbType.Int32));
                            cmd.Parameters["i_Refunds_AS"].Value = Convert.ToInt32(txtRefunds_AS.EditValue);
                            cmd.Parameters["i_Refunds_AS"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Refunds_CG", MySqlDbType.Int32));
                            cmd.Parameters["i_Refunds_CG"].Value = Convert.ToInt32(txtRefunds_CG.EditValue);
                            cmd.Parameters["i_Refunds_CG"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Refunds_CR", MySqlDbType.Int32));
                            cmd.Parameters["i_Refunds_CR"].Value = Convert.ToInt32(txtRefunds_CR.EditValue);
                            cmd.Parameters["i_Refunds_CR"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_PC_Title", MySqlDbType.VarChar));
                            cmd.Parameters["i_PC_Title"].Value = "";
                            cmd.Parameters["i_PC_Title"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_PC_Use_Type", MySqlDbType.VarChar));
                            cmd.Parameters["i_PC_Use_Type"].Value = "N";
                            cmd.Parameters["i_PC_Use_Type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_PC_Content", MySqlDbType.VarChar));
                            cmd.Parameters["i_PC_Content"].Value = "";
                            cmd.Parameters["i_PC_Content"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_User_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_User_id"].Value = UserInfo.instance().LOGIN_ID;
                            cmd.Parameters["i_User_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Order_Date", MySqlDbType.DateTime));
                            cmd.Parameters["i_Order_Date"].Value = dtOrder_Date.EditValue;
                            cmd.Parameters["i_Order_Date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Result_P_Id", MySqlDbType.VarChar));
                            cmd.Parameters["o_Result_P_Id"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;


                            cmd.ExecuteNonQuery();
                            txtP_Id.EditValue = cmd.Parameters["o_Result_P_Id"].Value.ToString();
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

        private void btnFileOpen1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/product/domamall/" + txtP_Code.EditValue;


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtPicPath1.EditValue = openFileDialog.FileName;
                    picP_IMG.LoadAsync(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }

                finally
                {
                    Cursor = Cursors.Default;
                }
                // 폴더생성

                DirectoryInfo di = new DirectoryInfo(@"c:\temp");
                if (di.Exists == false)
                {
                    di.Create();
                }

                // 선택된 파일을 위에서 만든 폴더에 이름을 바꿔 저장

                string sOldFile = txtPicPath1.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/product/domamall/";
                string sFtpPath2 = "/domalifefiles/files/product/domamall/" + txtP_Code.EditValue;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtP_Code.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtP_Img.EditValue = "http://media.domalife.net:8080/files/product/domamall/" + txtP_Code.EditValue + "/" + sFileName;
            }

        }

        private void btnFileOpen2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/product/domamall/" + txtP_Code.EditValue;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtPicPath2.EditValue = openFileDialog.FileName;
                    picP_IMG2.LoadAsync(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }

                finally
                {
                    Cursor = Cursors.Default;
                }

                // 폴더생성

                DirectoryInfo di = new DirectoryInfo(@"c:\temp");
                if (di.Exists == false)
                {
                    di.Create();
                }

                // 선택된 파일을 위에서 만든 폴더에 이름을 바꿔 저장

                string sOldFile = txtPicPath2.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/product/domamall/";
                string sFtpPath2 = "/domalifefiles/files/product/domamall/" + txtP_Code.EditValue;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtP_Code.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtP_Img2.EditValue = "http://media.domalife.net:8080/files/product/domamall/" + txtP_Code.EditValue + "/" + sFileName;



            }
        }

        private void btnFileOpen3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/product/domamall/" + txtP_Code.EditValue;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtPicPath3.EditValue = openFileDialog.FileName;
                    picP_CONTENTS.LoadAsync(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }

                finally
                {
                    Cursor = Cursors.Default;
                }

                // 폴더생성

                DirectoryInfo di = new DirectoryInfo(@"c:\temp");
                if (di.Exists == false)
                {
                    di.Create();
                }

                // 선택된 파일을 위에서 만든 폴더에 이름을 바꿔 저장

                string sOldFile = txtPicPath3.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/product/domamall/";
                string sFtpPath2 = "/domalifefiles/files/product/domamall/" + txtP_Code.EditValue;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtP_Code.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtP_Contents.EditValue = "http://media.domalife.net:8080/files/product/domamall/" + txtP_Code.EditValue + "/" + sFileName;



            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtP_Id.Text))
            {
                MessageAgent.MessageShow(MessageType.Informational, "삭제할 상품을 선택하세요.");
                return;
            }
            
            string sCount = "0";
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "SELECT count(*) as count FROM domamall.tb_am_product_order_datas " +
                             "WHERE p_id = " + txtP_Id.EditValue;
                DataSet ds = sql.selectQueryDataSet();

                sCount = sql.selectQueryForSingleValue();
            }

            if ( sCount != "0" )
            {
                MessageAgent.MessageShow(MessageType.Informational, "주문 이력이 존재하여 삭제할수 업습니다.");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "선택한 상품을 삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_p_id", MySqlDbType.Int32));
                            cmd.Parameters["i_p_id"].Value = Convert.ToInt32(txtP_Id.EditValue);
                            cmd.Parameters["i_p_id"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                New();
                Search();


            }
        }

        private void picP_IMG_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("1");
        }

        private void picP_IMG2_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("2");
        }

        private void picP_CONTENTS_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("3");
        }

        private void picPc_Thumbnail_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("4");
        }
        private void OpenDlg(string s1)
        {
            popup = new frmSCM08_Pop01();
            //popup.Owner = this;

            if (s1 == "1")
                popup.pURL = txtP_Img.Text;
            else if (s1 == "2")
                popup.pURL = txtP_Img2.Text;
            else if (s1 == "3")
                popup.pURL = txtP_Contents.Text;
            else if (s1 == "4")
                popup.pURL = txtPc_Thumbnail.Text;

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }

        private void txtProdName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
