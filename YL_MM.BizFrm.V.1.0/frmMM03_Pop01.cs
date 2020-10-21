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
using YL_MM.BizFrm;
using Tamir.SharpSsh;
using System.Collections;
using System.Net.Sockets;
namespace YL_MM.BizFrm
{
    public partial class frmMM03_Pop01 : FrmPopUpBase
    {
        public int Id { get; set; }

        frmMM03_Pop02 popup;

        public frmMM03_Pop01()
        {
            InitializeComponent();
        }

        private void frmMM03_Pop01_Load(object sender, EventArgs e)
        {
            txtP_Id.EditValue = Id;
            cbP_Discount_Donut1.EditValue = null;
            cbP_Discount_Donut2.EditValue = null;
            cbP_Discount_Donut3.EditValue = null;

            chkAll.EditValue = '0';
            chkMember.EditValue = '0';
            chkVip.EditValue = '0';
            chkChef.EditValue = '0';
            chkGShop.EditValue = '0';
            ckdora_md.EditValue = '0';
            rbis_use.EditValue = 'Y';
            rbshow_level.EditValue = 1;
            gridView1.OptionsView.ShowFooter = true;
            SetCmb();
            Open1();
            
            if (txtP_Id.EditValue.ToString() == "0")
                New();

            this.efwGridControl3.BindControlSet(
                      new ColumnControlSet("idx", txtsale_idx)
                    , new ColumnControlSet("option_name", txtoption_name)
                    , new ColumnControlSet("sale_qty", txtsale_qty)
                    , new ColumnControlSet("sale_amt", txtsale_amt)
                    , new ColumnControlSet("is_use", rbis_use)
                    , new ColumnControlSet("show_level", rbshow_level)
                   ); ;

            this.efwGridControl3.Click += efwGridControl3_Click;



            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("id", txtOP_ID)
                      , new ColumnControlSet("pp_title", txtPP_Title)
                   ); ;

            this.efwGridControl1.Click += efwGridControl1_Click;

        }

        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl3.GetSelectedRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                this.rbis_use.EditValue = dr["is_use"].ToString();
                this.rbshow_level.EditValue = dr["show_level"].ToString();
            }
        }


        private void SetCmb()
        {
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

            // 상품등록업체
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT s_idx as DCODE, s_company_name as DNAME  FROM domaadmin.tb_sellers_info " +
                    "          order by s_idx ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_Seller_Id, codeArray);
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


            // 옵션 내용에 포함될 COMBO

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT  code_id as DCODE, code_nm as DNAME FROM domaadmin.tb_common_code " +
                    "         where gcode_id = '00022' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataTable retDT = ds.Tables[0];

                repositoryItemLookUpEdit1.DataSource = retDT;
                //컨트롤 초기화
                InitCodeControl(repositoryItemLookUpEdit1);

                repositoryItemLookUpEdit1.EndUpdate();
            }


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT  code_id as DCODE, code_nm as DNAME FROM domaadmin.tb_common_code " +
                    "         where gcode_id = '00023' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataTable retDT = ds.Tables[0];

                repositoryItemLookUpEdit2.DataSource = retDT;
                //컨트롤 초기화
                InitCodeControl(repositoryItemLookUpEdit2);

                repositoryItemLookUpEdit2.EndUpdate();
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

        private void Open1()
        {
            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
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
                        //
                        
                        cmd.Parameters.Add(new MySqlParameter("o_P_Member_level_All", MySqlDbType.VarChar));
                        cmd.Parameters["o_P_Member_level_All"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_P_Member_level_Member", MySqlDbType.VarChar));
                        cmd.Parameters["o_P_Member_level_Member"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_P_Member_level_Vip", MySqlDbType.VarChar));
                        cmd.Parameters["o_P_Member_level_Vip"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_P_Member_level_Chef", MySqlDbType.VarChar));
                        cmd.Parameters["o_P_Member_level_Chef"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_P_Member_level_GShop", MySqlDbType.VarChar));
                        cmd.Parameters["o_P_Member_level_GShop"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_P_Member_level_DoraMd", MySqlDbType.VarChar));
                        cmd.Parameters["o_P_Member_level_DoraMd"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_explanation", MySqlDbType.Text));
                        cmd.Parameters["o_p_explanation"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_title", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_title"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_p_represent", MySqlDbType.VarChar));
                        cmd.Parameters["o_p_represent"].Direction = ParameterDirection.Output;

                        //

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

                        chkAll.EditValue = cmd.Parameters["o_P_Member_level_All"].Value.ToString();
                        chkMember.EditValue = cmd.Parameters["o_P_Member_level_Member"].Value.ToString();
                        chkVip.EditValue = cmd.Parameters["o_P_Member_level_Vip"].Value.ToString();
                        chkChef.EditValue = cmd.Parameters["o_P_Member_level_Chef"].Value.ToString();
                        chkGShop.EditValue = cmd.Parameters["o_P_Member_level_GShop"].Value.ToString();
                        ckdora_md.EditValue = cmd.Parameters["o_P_Member_level_DoraMd"].Value.ToString();
                        txtp_explanation.EditValue = cmd.Parameters["o_p_explanation"].Value.ToString();
                        txtp_title.EditValue = cmd.Parameters["o_p_title"].Value.ToString();
                        cbp_represent.EditValue = cmd.Parameters["o_p_represent"].Value.ToString();


                        if (chkAll.EditValue.ToString() == "1")
                        {
                            this.chkMember.Enabled = false;
                            this.chkVip.Enabled = false;
                            this.chkChef.Enabled = false;
                            this.chkGShop.Enabled = false;
                            this.ckdora_md.Enabled = false;
                        }
                        else if (chkAll.EditValue.ToString() == "0")
                        {
                            this.chkMember.Enabled = true;
                            this.chkVip.Enabled = true;
                            this.chkChef.Enabled = true;
                            this.chkGShop.Enabled = true;
                            this.ckdora_md.Enabled = true;
                        }

                        Open2();
                        // 전사 상거래 상세 내용
                        Open3();
                        // 전사 상거래 상세 내용
                        Open4();



                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open2()
        {
            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_p_id", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = txtP_Id.EditValue;

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

        private void Open3()
        {
            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_p_id", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = txtP_Id.EditValue;

                        cmd.Parameters.Add("i_p_ce_code", MySqlDbType.VarChar);
                        cmd.Parameters[1].Value = cmbP_EC_Code.EditValue;


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

        private void Open4()
        {
            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_p_id", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = txtP_Id.EditValue;

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

        private void cmbP_EC_Code_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_p_id", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = txtP_Id.EditValue;

                        cmd.Parameters.Add("i_p_ce_code", MySqlDbType.VarChar);
                        cmd.Parameters[1].Value = cmbP_EC_Code.EditValue;


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
            popup = new frmMM03_Pop02();
            //popup.Owner = this;

            if (s1 == "1")
                popup.pURL = txtP_Img.Text;
            else if (s1 == "2")
                popup.pURL = txtP_Img2.Text;
            else if (s1 == "3")
                popup.pURL = txtP_Contents.Text;
            else if (s1 == "4")
                popup.pURL = txtPc_Thumbnail.Text;
            else if (s1 == "5")
                popup.pURL = picOP_IMG.Text;
            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }

        private void btnFileOpen1_Click(object sender, EventArgs e)
        { 
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

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
                string sNewFile = "c:\\temp\\"+ sFileName;
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
                    if (ay[i].ToString() == txtP_Code.EditValue.ToString() )
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
                txtP_Img.EditValue = "https://media.domalife.net/files/product/domamall/" + txtP_Code.EditValue+"/"+sFileName;
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
                txtP_Img2.EditValue = "https://media.domalife.net/files/product/domamall/" + txtP_Code.EditValue + "/" + sFileName;



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
                txtP_Contents.EditValue = "https://media.domalife.net/files/product/domamall/" + txtP_Code.EditValue + "/" + sFileName;



            }
        }

        private void btnFileOpen4_Click(object sender, EventArgs e)
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

                    txtPicPath4.EditValue = openFileDialog.FileName;
                    picPc_Thumbnail.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtPicPath4.EditValue.ToString();
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
                txtPc_Thumbnail.EditValue = "https://media.domalife.net/files/product/domamall/" + txtP_Code.EditValue + "/" + sFileName;


            }
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
            txtP_Min_Purchase_Num.EditValue = null;
            txtP_Multi_Discount_Cost.EditValue = null;
            txtP_Max_Purchase_Num.EditValue = null;
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
            picPc_Thumbnail.EditValue = null;

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

            txtPC_Title.EditValue = null;
            rbPC_Use_Type.EditValue = 'Y';
            txtPC_Content.EditValue = null;

            txtp_explanation.EditValue = null;

            chkAll.EditValue = '0';
            chkMember.EditValue = '0';
            chkVip.EditValue = '0';
            chkChef.EditValue = '0';
            chkGShop.EditValue = '0';
            ckdora_md.EditValue = '0';
            txtp_title.EditValue = null;
            cbp_represent.EditValue = "N";


            //gridView1.Columns.Clear();
            //gridView2.Columns.Clear();

            while (gridView1.RowCount != 0)
            {
                gridView1.SelectAll();
                gridView1.DeleteSelectedRows();
            }
            while (gridView2.RowCount != 0)
            {
                gridView2.SelectAll();
                gridView2.DeleteSelectedRows();
            }

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
                    //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
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
                            cmd.Parameters["i_PC_Title"].Value = txtPC_Title.EditValue;
                            cmd.Parameters["i_PC_Title"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_PC_Use_Type", MySqlDbType.VarChar));
                            cmd.Parameters["i_PC_Use_Type"].Value = rbPC_Use_Type.EditValue;
                            cmd.Parameters["i_PC_Use_Type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_PC_Content", MySqlDbType.VarChar));
                            cmd.Parameters["i_PC_Content"].Value = txtPC_Content.EditValue;
                            cmd.Parameters["i_PC_Content"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_User_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_User_id"].Value = UserInfo.instance().LOGIN_ID;
                            cmd.Parameters["i_User_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Order_Date", MySqlDbType.DateTime));
                            cmd.Parameters["i_Order_Date"].Value = dtOrder_Date.EditValue;
                            cmd.Parameters["i_Order_Date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Member_level_All", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Member_level_All"].Value = chkAll.EditValue;
                            cmd.Parameters["i_P_Member_level_All"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Member_level_Member", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Member_level_Member"].Value = chkMember.EditValue;
                            cmd.Parameters["i_P_Member_level_Member"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Member_level_Vip", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Member_level_Vip"].Value = chkVip.EditValue;
                            cmd.Parameters["i_P_Member_level_Vip"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Member_level_Chef", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Member_level_Chef"].Value = chkChef.EditValue;
                            cmd.Parameters["i_P_Member_level_Chef"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Member_level_GShop", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Member_level_GShop"].Value = chkGShop.EditValue;
                            cmd.Parameters["i_P_Member_level_GShop"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Member_level_DoraMd", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Member_level_DoraMd"].Value = ckdora_md.EditValue;
                            cmd.Parameters["i_P_Member_level_DoraMd"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_explanation", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_explanation"].Value = txtp_explanation.EditValue;
                            cmd.Parameters["i_p_explanation"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_title", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_title"].Value = txtp_title.EditValue;
                            cmd.Parameters["i_p_title"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_represent", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_represent"].Value = cbp_represent.EditValue;
                            cmd.Parameters["i_p_represent"].Direction = ParameterDirection.Input;

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
                Save1();
                Save2();
                Open1();

            }
        }

        private void Save1()
        {

            try
            {
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView2.DataRowCount; i++)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SAVE_02", con))
                        {

                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_p_id", MySqlDbType.Int32, 11);
                            cmd.Parameters[0].Value = txtP_Id.EditValue;

                            cmd.Parameters.Add("i_c_code", MySqlDbType.VarChar, 50);
                            cmd.Parameters[1].Value = gridView2.GetRowCellValue(i, "c_code");

                            cmd.Parameters.Add("i_ec_sub_content", MySqlDbType.VarChar, 255);
                            cmd.Parameters[2].Value = gridView2.GetRowCellValue(i, "ec_sub_content");

                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }
        private void Save2()
        {

            try
            {
                int nId = 0;
                string sId = "";
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SAVE_03", con))
                        {

                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_p_id", MySqlDbType.Int32, 11);
                            cmd.Parameters[0].Value = txtP_Id.EditValue;

                            sId = gridView1.GetRowCellValue(i, "id").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "id"));
                            cmd.Parameters.Add("i_id", MySqlDbType.Int32, 11);
                            cmd.Parameters[1].Value = nId;
                            //

                            cmd.Parameters.Add("i_pp_title", MySqlDbType.VarChar, 255);
                            cmd.Parameters[2].Value = gridView1.GetRowCellValue(i, "pp_title");
                            //

                            
                            sId = gridView1.GetRowCellValue(i, "pp_price").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "pp_price"));
                            cmd.Parameters.Add("i_pp_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[3].Value = nId;

                            //
                            
                            sId = gridView1.GetRowCellValue(i, "pp_org_price").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "pp_org_price"));
                            cmd.Parameters.Add("i_pp_org_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[4].Value = nId;

                            //
                            
                            sId = gridView1.GetRowCellValue(i, "pp_vip_price").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "pp_vip_price"));
                            cmd.Parameters.Add("i_pp_vip_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[5].Value = nId;
                            //
                            
                            sId = gridView1.GetRowCellValue(i, "pp_ps_oper_price").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "pp_ps_oper_price"));
                            cmd.Parameters.Add("i_pp_ps_oper_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[6].Value = nId;
                            //
                            
                            sId = gridView1.GetRowCellValue(i, "pp_discount_price").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "pp_discount_price"));
                            cmd.Parameters.Add("i_pp_discount_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[7].Value = nId;
                            //
                            
                            sId = gridView1.GetRowCellValue(i, "pp_md_chef_price").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "pp_md_chef_price"));
                            cmd.Parameters.Add("i_pp_md_chef_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[8].Value = nId;

                            
                            sId = gridView1.GetRowCellValue(i, "pp_chef_price").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "pp_chef_price"));
                            cmd.Parameters.Add("i_pp_chef_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[9].Value = nId;

                            
                            sId = gridView1.GetRowCellValue(i, "pp_num").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "pp_num"));
                            cmd.Parameters.Add("i_pp_num", MySqlDbType.Int32, 11);
                            cmd.Parameters[10].Value = nId;

                            
                            sId = gridView1.GetRowCellValue(i, "refund_donut_cost").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "refund_donut_cost"));
                            cmd.Parameters.Add("i_refund_donut_cost", MySqlDbType.Int32, 11);
                            cmd.Parameters[11].Value = nId;

                            cmd.Parameters.Add("i_is_use", MySqlDbType.VarChar, 1);
                            cmd.Parameters[12].Value = gridView1.GetRowCellValue(i, "is_use");

                            cmd.Parameters.Add("i_is_donut", MySqlDbType.VarChar, 1);
                            cmd.Parameters[13].Value = gridView1.GetRowCellValue(i, "is_donut");

                            cmd.Parameters.Add("i_p_name", MySqlDbType.VarChar, 100);
                            cmd.Parameters[14].Value = txtP_Name.EditValue;


                            cmd.Parameters.Add("i_p_delivery_price", MySqlDbType.Int32, 11);
                            cmd.Parameters[15].Value = Convert.ToInt32(txtP_Delivery_Price.EditValue);

                            sId = gridView1.GetRowCellValue(i, "td_donut").ToString();
                            if (sId == "")
                                nId = 0;
                            else
                                nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "td_donut"));
                            cmd.Parameters.Add("i_td_donut", MySqlDbType.Int32, 11);
                            cmd.Parameters[16].Value = nId;


                            cmd.Parameters.Add("i_op_img", MySqlDbType.VarChar, 255);
                            cmd.Parameters[17].Value = txtOP_IMG.EditValue;


                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int nOption_id = 0;
            string sOption_Id = gridView1.GetFocusedRowCellValue("id").ToString();

            if (sOption_Id == "")
                nOption_id = 0;
            else
                nOption_id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("id").ToString());


            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Id", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Id"].Value = txtP_Id.EditValue;
                            cmd.Parameters["i_P_Id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Option_Id", MySqlDbType.Int32));
                            cmd.Parameters["i_Option_Id"].Value = nOption_id;
                            cmd.Parameters["i_Option_Id"].Direction = ParameterDirection.Input;

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
        private void PicUp()
        {

            //if (Pic1 == "1")
            //    popup.pURL = txtP_Img.Text;
            //else if (Pic1 == "2")
            //    popup.pURL = txtP_Img2.Text;
            //else if (Pic1 == "3")
            //    popup.pURL = txtP_Contents.Text;
            //else if (Pic1 == "4")
            //    popup.pURL = txtPc_Thumbnail.Text;

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.EditValue.ToString() == "1")
            {
                chkMember.EditValue = '0';
                chkVip.EditValue = '0';
                chkChef.EditValue = '0';
                chkGShop.EditValue = '0';
                ckdora_md.EditValue = '0';
                this.chkMember.Enabled = false;
                this.chkVip.Enabled = false;
                this.chkChef.Enabled = false;
                this.chkGShop.Enabled = false;
                this.ckdora_md.Enabled = false;
            }
            else if (chkAll.EditValue.ToString() == "0")
            {
                chkMember.EditValue = '0';
                chkVip.EditValue = '0';
                chkChef.EditValue = '0';
                chkGShop.EditValue = '0';
                ckdora_md.EditValue = '0';
                this.chkMember.Enabled = true;
                this.chkVip.Enabled = true;
                this.chkChef.Enabled = true;
                this.chkGShop.Enabled = true;
                this.ckdora_md.Enabled = true;
            }

        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        { 
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SAVE_04", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt64(txtsale_idx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_P_Id", MySqlDbType.VarChar));
                            cmd.Parameters["i_P_Id"].Value = txtP_Id.EditValue;
                            cmd.Parameters["i_P_Id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_show_level", MySqlDbType.Int32));
                            cmd.Parameters["i_show_level"].Value = rbshow_level.EditValue;
                            cmd.Parameters["i_show_level"].Direction = ParameterDirection.Input; 

                            cmd.Parameters.Add(new MySqlParameter("i_option_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_option_name"].Value = txtoption_name.EditValue;
                            cmd.Parameters["i_option_name"].Direction = ParameterDirection.Input; 

                            cmd.Parameters.Add(new MySqlParameter("i_qty", MySqlDbType.VarChar));
                            cmd.Parameters["i_qty"].Value = Convert.ToInt64(txtsale_qty.EditValue);
                            cmd.Parameters["i_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_amt", MySqlDbType.VarChar));
                            cmd.Parameters["i_amt"].Value = Convert.ToInt64(txtsale_amt.EditValue);
                            cmd.Parameters["i_amt"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = rbis_use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_max_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_max_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            txtsale_idx.EditValue = cmd.Parameters["o_max_idx"].Value.ToString();
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open4();
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            txtsale_idx.EditValue = 0;
            rbshow_level.EditValue = 1;
            txtsale_qty.EditValue = 0;
            txtsale_amt.EditValue = 0;
            rbis_use.EditValue = "Y";
            rbis_use.EditValue = "Y";
            txtoption_name.EditValue = "";
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
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
            string sftpDirectory = "/domalifefiles/files/product/domamall/" + txtOP_ID.EditValue;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtPicPath5.EditValue = openFileDialog.FileName;
                    picOP_IMG.LoadAsync(openFileDialog.FileName);
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

                string sOldFile = txtPicPath5.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/product/domamall/";
                string sFtpPath2 = "/domalifefiles/files/product/domamall/" + txtOP_ID.EditValue;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtOP_ID.EditValue.ToString())
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
                txtOP_IMG.EditValue = "https://media.domalife.net/files/product/domamall/" + txtOP_ID.EditValue + "/" + sFileName;


            }
        }


        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["id"].ToString() != "")
            {
                this.txtOP_ID.EditValue = dr["id"].ToString();
              }

            if (dr != null && dr["op_img"].ToString() != "")
            {
                this.txtOP_IMG.EditValue = dr["op_img"].ToString();
                picOP_IMG.LoadAsync(txtOP_IMG.EditValue.ToString());
            }

        }

        private void picOP_IMG_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("5");
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            try
            {

                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {


                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM03_SAVE_05", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_p_id", MySqlDbType.Int32, 11);
                        cmd.Parameters[0].Value = txtP_Id.EditValue;

                        cmd.Parameters.Add("i_id", MySqlDbType.Int32, 11);
                        cmd.Parameters[1].Value = txtOP_ID.EditValue;
                            //

                        cmd.Parameters.Add("i_op_img", MySqlDbType.VarChar, 255);
                        cmd.Parameters[2].Value = txtOP_IMG.EditValue;


                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                    }

                }
            }

            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Open2();
        }
    }
}
