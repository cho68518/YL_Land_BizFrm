﻿#region "frmDN01 설명"
//===================================================================================================
//■Program Name  : frmDN01
//■Description   : 주문현황(도넛라이프)
//■Author        : 송호철
//■Date          : 2019.05.30
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.05.30][송호철] Base
//[2] [2019.05.30][송호철] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

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


namespace YL_DONUT.BizFrm
{
    public partial class frmDN01 : FrmBase
    {
        public frmDN01()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = " frmDN01";
            //폼명설정
            this.FrmName = "주문 현황";
        }
        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

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

            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            //gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "총주문금액: {0:c}";
            gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";


            gridView1.Columns["o_donut_d_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_donut_d_cost"].SummaryItem.FieldName = "o_donut_d_cost";
            gridView1.Columns["o_donut_d_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_donut_m_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_donut_m_cost"].SummaryItem.FieldName = "o_donut_m_cost";
            gridView1.Columns["o_donut_m_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_donut_c_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_donut_c_cost"].SummaryItem.FieldName = "o_donut_c_cost";
            gridView1.Columns["o_donut_c_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_delivery_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_delivery_cost"].SummaryItem.FieldName = "o_delivery_cost";
            gridView1.Columns["o_delivery_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_purchase_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_purchase_cost"].SummaryItem.FieldName = "o_purchase_cost";
            gridView1.Columns["o_purchase_cost"].SummaryItem.DisplayFormat = "{0:c}";

            //this.efwGridControl1.BindControlSet(
            //new ColumnControlSet("id", txtID)
            //);
            //this.efwGridControl1.Click += efwGridControl1_Click;
            rbP_SHOW_TYPE.EditValue = "T";
            setCmb();
        }

        private void setCmb()
        {
            try
            {
                Dictionary<string, string> myRecord;

                string strQueruy = @"  SELECT
                              T1.DCODE, T1.DNAME
                              FROM
                              (  SELECT ''  DCODE, N'전체'  DNAME
	                             UNION ALL
	                             SELECT CODE    DCODE
                                       ,CODE_NM DNAME
                                 FROM dbo.ETCCODE
	                             WHERE GRP_CODE = 'DELIVERYTYPE' " + @") T1 ";

                CodeAgent.SetLegacyCode(cmbO_TYPE, strQueruy);
                cmbO_TYPE.EditValue = "";


                string strQueruy1 = @"  SELECT
                              T1.DCODE, T1.DNAME
                              FROM
                              (  SELECT ''  DCODE, N'전체'  DNAME
	                             UNION ALL
	                             SELECT CODE  DCODE, CODE_NM DNAME
                                 FROM dbo.ETCCODE
	                             WHERE GRP_CODE = 'MALL_TYPE' " + @") T1 ";

                CodeAgent.SetLegacyCode(cmbMALL_TYPE, strQueruy1);
                cmbMALL_TYPE.EditValue = "";

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }


        }

        #region 신규

        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
        }

        #endregion


        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                 using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_DN_DN01_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtP_NAME.EditValue;

                        if (rbP_SHOW_TYPE.EditValue.ToString() != "Y" && rbP_SHOW_TYPE.EditValue.ToString() != "N")
                            sP_SHOW_TYPE = null;
                        else
                            sP_SHOW_TYPE = rbP_SHOW_TYPE.EditValue.ToString();

                        // efwRadioGroup1.Properties.Items[efwRadioGroup1.SelectedIndex].Value.ToString()
                        cmd.Parameters.Add("i_is_order", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = sP_SHOW_TYPE;

                        cmd.Parameters.Add("i_o_receive_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtO_RECEIVE_NAME.EditValue;

                        cmd.Parameters.Add("i_u_nickname", MySqlDbType.VarChar, 50);
                        cmd.Parameters[5].Value = txtU_NICKNAME.EditValue;

                        cmd.Parameters.Add("i_o_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = cmbO_TYPE.EditValue;

                        cmd.Parameters.Add("i_s_company_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[7].Value = txtS_COMPANY_NAME.EditValue;

                        cmd.Parameters.Add("i_order_mall_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[8].Value = cmbMALL_TYPE.EditValue;

                        //Console.WriteLine(" i_sdate           ---> [" + cmd.Parameters[0].Value + "]" );
                        //Console.WriteLine(" i_edate           ---> [" + cmd.Parameters[1].Value + "]");
                        //Console.WriteLine(" i_name            ---> [" + cmd.Parameters[2].Value + "]");
                        //Console.WriteLine(" i_is_order        ---> [" + cmd.Parameters[3].Value + "]");
                        //Console.WriteLine(" i_o_receive_name  ---> [" + cmd.Parameters[4].Value + "]");
                        //Console.WriteLine(" i_u_nickname      ---> [" + cmd.Parameters[5].Value + "]");
                        //Console.WriteLine(" i_o_type          ---> [" + cmd.Parameters[6].Value + "]");
                        //Console.WriteLine(" i_s_company_name  ---> [" + cmd.Parameters[7].Value + "]");
                        //Console.WriteLine(" i_order_mall_type ---> [" + cmd.Parameters[8].Value + "]");

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        #endregion

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
        }

        private void BtnCardOpen_Click(object sender, EventArgs e)
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_DN_DN01_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtP_NAME.EditValue;

                        if (rbP_SHOW_TYPE.EditValue.ToString() != "Y" && rbP_SHOW_TYPE.EditValue.ToString() != "N")
                            sP_SHOW_TYPE = null;
                        else
                            sP_SHOW_TYPE = rbP_SHOW_TYPE.EditValue.ToString();

                        // efwRadioGroup1.Properties.Items[efwRadioGroup1.SelectedIndex].Value.ToString()
                        cmd.Parameters.Add("i_is_order", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = sP_SHOW_TYPE;

                        cmd.Parameters.Add("i_o_receive_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtO_RECEIVE_NAME.EditValue;

                        cmd.Parameters.Add("i_u_nickname", MySqlDbType.VarChar, 50);
                        cmd.Parameters[5].Value = txtU_NICKNAME.EditValue;

                        cmd.Parameters.Add("i_o_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = cmbO_TYPE.EditValue;

                        cmd.Parameters.Add("i_s_company_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[7].Value = txtS_COMPANY_NAME.EditValue;

                        cmd.Parameters.Add("i_order_mall_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[8].Value = cmbMALL_TYPE.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_DN_DN01_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtP_NAME.EditValue;

                        if (rbP_SHOW_TYPE.EditValue.ToString() != "Y" && rbP_SHOW_TYPE.EditValue.ToString() != "N")
                            sP_SHOW_TYPE = null;
                        else
                            sP_SHOW_TYPE = rbP_SHOW_TYPE.EditValue.ToString();

                        // efwRadioGroup1.Properties.Items[efwRadioGroup1.SelectedIndex].Value.ToString()
                        cmd.Parameters.Add("i_is_order", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = sP_SHOW_TYPE;

                        cmd.Parameters.Add("i_o_receive_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtO_RECEIVE_NAME.EditValue;

                        cmd.Parameters.Add("i_u_nickname", MySqlDbType.VarChar, 50);
                        cmd.Parameters[5].Value = txtU_NICKNAME.EditValue;

                        cmd.Parameters.Add("i_o_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = cmbO_TYPE.EditValue;

                        cmd.Parameters.Add("i_s_company_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[7].Value = txtS_COMPANY_NAME.EditValue;

                        cmd.Parameters.Add("i_order_mall_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[8].Value = cmbMALL_TYPE.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void TxtP_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void TxtS_COMPANY_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void TxtO_RECEIVE_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void TxtU_NICKNAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
