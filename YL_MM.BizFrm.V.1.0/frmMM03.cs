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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_MM.BizFrm.Dlg;

namespace YL_MM.BizFrm
{
    public partial class frmMM03 : FrmBase
    {
        #region Fields

        #endregion

        #region 생성자
        public frmMM03()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MM03";
            //폼명설정
            this.FrmName = "제품코드";

            //UserInfo.instance().ORG_CD;
        }
        #endregion

        #region FrmLoadEvent()
        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
            new ColumnControlSet("c_code1", txtC_Code1)
          , new ColumnControlSet("c_code2", txtC_Code2)
          , new ColumnControlSet("c_code3", txtC_Code3)
          , new ColumnControlSet("c_code4", txtC_Code4)
       );

            this.efwGridControl1.Click += efwGridControl1_Click;

            rbShowType.EditValue = "T";

            SetCmb();
        
        }


        private void efwGridControl1_Click(object sender, EventArgs e)
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

            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
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

                CodeAgent.MakeCodeControl(this.cmbSellers, codeArray);
            }
            cmbSellers.EditValue = "";



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

            //using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            //{
            //    con.Query = "SELECT '0' as DCODE, '선택하세요' as DNAME  " ;

            //    DataSet ds1 = con.selectQueryDataSet();
            //    DataRow[] dr1 = ds1.Tables[0].Select();
            //    CodeData[] codeArray = new CodeData[dr1.Length];

            //    for (int i = 0; i < dr1.Length; i++)
            //        codeArray[i] = new CodeData(dr1[i]["DCODE"].ToString(), dr1[i]["DNAME"].ToString());

            //    CodeAgent.MakeCodeControl(this.cmbCate_Code2, codeArray);
            //}

            //using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            //{
            //    con.Query = "SELECT '0' as DCODE, '선택하세요' as DNAME  " ;

            //    DataSet ds2 = con.selectQueryDataSet();
            //    DataRow[] dr2 = ds2.Tables[0].Select();
            //    CodeData[] codeArray = new CodeData[dr2.Length];

            //    for (int i = 0; i < dr2.Length; i++)
            //        codeArray[i] = new CodeData(dr2[i]["DCODE"].ToString(), dr2[i]["DNAME"].ToString());

            //    CodeAgent.MakeCodeControl(this.cmbCate_Code3, codeArray);
            //}

            //using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            //{
            //    con.Query = "SELECT '0' as DCODE, '선택하세요' as DNAME  ";

            //    DataSet ds3 = con.selectQueryDataSet();
            //    DataRow[] dr3 = ds3.Tables[0].Select();
            //    CodeData[] codeArray = new CodeData[dr3.Length];

            //    for (int i = 0; i < dr3.Length; i++)
            //        codeArray[i] = new CodeData(dr3[i]["DCODE"].ToString(), dr3[i]["DNAME"].ToString());

            //    CodeAgent.MakeCodeControl(this.cmbCate_Code4, codeArray);
            //}

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

                        if (cmbSellers.EditValue.ToString() != "Y" && cmbSellers.EditValue.ToString() != "N")
                            sShowType = null;
                        else
                            sShowType = cmbSellers.EditValue.ToString();

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

        private void cmbCate_Code1_EditValueChanged(object sender, EventArgs e)
        {
            string sCate_Code1 = cmbCate_Code1.EditValue.ToString();

            if (this.cmbCate_Code1.EditValue== null)
            {
                return;
            }

            cmbCate_Code2.Properties.DataSource = null;
            cmbCate_Code3.Properties.DataSource = null;
            cmbCate_Code4.Properties.DataSource = null;


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
            if (this.cmbCate_Code3.EditValue== null)
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


    }
}


