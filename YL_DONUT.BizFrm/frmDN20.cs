﻿using DevExpress.XtraTreeList;
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

namespace YL_DONUT.BizFrm
{
    public partial class frmDN20 : FrmBase
    {
        frmDN20_Pop01 popup;
        frmDN20_Pop02 popup2;
        public frmDN20()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmDN20";
            //폼명설정
            this.FrmName = "주문 간편양식";
        }
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

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            dtOut_Date.EditValue = DateTime.Now;
            rbCompany.EditValue = "1";
            rbo_type.EditValue = "O";

            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;

            gridView3.OptionsView.ShowFooter = true;
            gridView3.Columns["tot_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView3.Columns["tot_qty"].SummaryItem.FieldName = "tot_qty";
            gridView3.Columns["tot_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView5.OptionsView.ShowFooter = true;
            gridView5.Columns["tot_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView5.Columns["tot_qty"].SummaryItem.FieldName = "tot_qty";
            gridView5.Columns["tot_qty"].SummaryItem.DisplayFormat = "{0}";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT d_code as DCODE ,d_name as DNAME  FROM domamall.tb_am_product_delivers order by sort";

                DataSet ds = con.selectQueryDataSet();
                DataTable retDT = ds.Tables[0];

                repositoryItemLookUpEdit1.DataSource = retDT;
                //컨트롤 초기화
                InitCodeControl(repositoryItemLookUpEdit1);

                repositoryItemLookUpEdit1.EndUpdate();

            }


            gridView4.OptionsView.ShowFooter = true;

            this.efwGridControl4.BindControlSet(
            new ColumnControlSet("p_code", txtP_Code)
            );
            this.efwGridControl4.Click += efwGridControl4_Click;
            SetCmb();
        }

        private void InitCodeControl(object cdControl)
        {
            string DNAME = string.Empty;

            DNAME = "DNAME";

            CodeAgent.InitCodeControl(cdControl, "코드명", "코드", DNAME, "DCODE", "선택하세요");
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
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00025' and code_id in ('O','P','D')  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbChange_type, codeArray);
            }
            cmbChange_type.EditValue = "P";
        }


        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open4();
            }
        }

        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN20_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[2].Value = cmbSellers.EditValue;

                        cmd.Parameters.Add("i_company", MySqlDbType.VarChar);
                        cmd.Parameters[3].Value = rbCompany.EditValue;

                        cmd.Parameters.Add("i_o_type", MySqlDbType.VarChar);
                        cmd.Parameters[4].Value = rbo_type.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            // this.efwGridControl1.MyGridView.BestFitColumns();

                        }
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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN20_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[2].Value = cmbSellers.EditValue;

                        cmd.Parameters.Add("i_company", MySqlDbType.VarChar);
                        cmd.Parameters[3].Value = rbCompany.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            // this.efwGridControl1.MyGridView.BestFitColumns();

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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN20_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[2].Value = cmbSellers.EditValue;

                        cmd.Parameters.Add("i_company", MySqlDbType.VarChar);
                        cmd.Parameters[3].Value = rbCompany.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();

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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN20_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[2].Value = cmbSellers.EditValue;

                        cmd.Parameters.Add("i_company", MySqlDbType.VarChar);
                        cmd.Parameters[3].Value = rbCompany.EditValue;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            //this.efwGridControl4.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        private void Open5()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN20_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[2].Value = cmbSellers.EditValue;

                        cmd.Parameters.Add("i_company", MySqlDbType.VarChar);
                        cmd.Parameters[3].Value = rbCompany.EditValue;

                        cmd.Parameters.Add("i_p_code", MySqlDbType.Int32);
                        cmd.Parameters[4].Value = Convert.ToInt32(txtP_Code.EditValue).ToString();


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
                            this.efwGridControl5.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void btnExcelUpdate_Click(object sender, EventArgs e)
        {
            popup = new frmDN20_Pop01();
            popup.ShowDialog();
        }

        //public override void Save()
        //{
        //    if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
        //    {
        //        try
        //        {
        //            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
        //            {
        //                var saveResult = new SaveTableResultInfo() { IsError = true };
        //                var dt = efwGridControl1.GetChangeDataWithRowState;
        //                var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

        //                for (var i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    if (dt.Rows[i][StatusColumn].ToString() == "U")
        //                    {
        //                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM04_SAVE_02", con))
        //                        {

        //                            con.Open();
        //                            cmd.CommandType = CommandType.StoredProcedure;

        //                            cmd.Parameters.Add("i_id", MySqlDbType.VarChar, 50);
        //                            cmd.Parameters[0].Value = gridView1.GetRowCellValue(i, "id");

        //                            cmd.Parameters.Add("i_delivery_num", MySqlDbType.VarChar, 50);
        //                            cmd.Parameters[1].Value = gridView1.GetRowCellValue(i, "o_delivery_num");

        //                            cmd.Parameters.Add("i_delivery_code", MySqlDbType.VarChar, 2);
        //                            cmd.Parameters[2].Value = gridView1.GetRowCellValue(i, "delivers");

        //                            cmd.ExecuteNonQuery();
        //                            con.Close();
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageAgent.MessageShow(MessageType.Error, ex.ToString());
        //        }
        //    }
        //}



        public override void Save()
        {
            try
            {

                var saveResult = new SaveTableResultInfo() { IsError = true };

                var dt = efwGridControl1.GetChangeDataWithRowState;
                var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][StatusColumn].ToString() == "U")
                    {

                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM04_SAVE_02", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;


                                cmd.Parameters.Add("i_id", MySqlDbType.Int32, 10);
                                cmd.Parameters[0].Value = Convert.ToInt32(dt.Rows[i]["id"]).ToString();

                                cmd.Parameters.Add("i_delivery_num", MySqlDbType.VarChar, 250);
                                cmd.Parameters[1].Value = dt.Rows[i]["o_delivery_num"].ToString();

                                cmd.Parameters.Add("i_delivery_code", MySqlDbType.VarChar, 10);
                                cmd.Parameters[2].Value = dt.Rows[i]["delivers"].ToString();

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
        }


        private void efwSimpleButton3_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
                gridView1.SetRowCellValue(i, gridView1.Columns["chk"], "Y");
        }

        private void efwSimpleButton4_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
                gridView1.SetRowCellValue(i, gridView1.Columns["chk"], "N");
        }

        private void efwSimpleButton6_Click_1(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "주문 상태를 변경 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {

                            if (gridView1.GetRowCellValue(i, "chk").ToString() == "Y"  )
                            {
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN01_SAVE_05", con))
                                {
                                    // Console.WriteLine("********" + gridView1.GetRowCellValue(i, "is_fix"));


                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_id", MySqlDbType.Int32, 50);
                                    cmd.Parameters[0].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "id"));

                                    cmd.Parameters.Add("i_o_type", MySqlDbType.VarChar, 10);
                                    cmd.Parameters[1].Value = cmbChange_type.EditValue;

                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    MessageAgent.MessageShow(MessageType.Informational, "주문 상태가 변경 되었습니다.");
                    Search();
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            popup2 = new frmDN20_Pop02();
            popup2.ShowDialog();
        }

        private void efwXtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void efwGridControl4_Click(object sender, EventArgs e)
        { 
            DataRow dr = this.efwGridControl4.GetSelectedRow(0);
            if (dr != null && dr["p_code"].ToString() != "")
            {
                Open5();
            }
            
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {

                        for (int i = 0; i < gridView4.DataRowCount; i++)
                        {
                            if (gridView4.GetRowCellValue(i, gridView4.Columns[3]).ToString().Length > 0)
                            {
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DM20_SAVE01", con))
                                {

                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_out_date", MySqlDbType.DateTime);
                                    cmd.Parameters[0].Value = Convert.ToDateTime(dtOut_Date.EditValue);

                                    cmd.Parameters.Add("i_out_p_code", MySqlDbType.Int32);
                                    cmd.Parameters[1].Value = Convert.ToInt32(gridView4.GetRowCellValue(i, gridView4.Columns[1]).ToString());

                                    cmd.Parameters.Add("i_qty", MySqlDbType.Int32);
                                    cmd.Parameters[2].Value = Convert.ToInt32(gridView4.GetRowCellValue(i, gridView4.Columns[3]).ToString());

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
                MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            }

        }
    }
}
