#region "frmDN01 설명"
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

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
        frmDN01_Pop01 popup;


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
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            cbG_Prod.EditValue = "0";
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

            gridView1.Columns["pp_ps_oper_price"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["pp_ps_oper_price"].SummaryItem.FieldName = "pp_ps_oper_price";
            gridView1.Columns["pp_ps_oper_price"].SummaryItem.DisplayFormat = "{0:c}";
            txtId.EditValue = "0";


            //this.efwGridControl1.BindControlSet(
            //new ColumnControlSet("id", txtID)
            //  
            //this.efwGridControl1.Click += efwGridControl1_Click;
            rbP_SHOW_TYPE.EditValue = "T";

            chkI.Checked = true;
            chkO.Checked = true;
            chkP.Checked = true;
            chkD.Checked = true;
            chkF.Checked = true;
            chkE.Checked = true;
            chkC.Checked = true;
            chkZ.Checked = false;
            chkX.Checked = false;
            chkT.Checked = false;
            chkA.Checked = true;
            chkB.Checked = true;

            this.efwGridControl1.BindControlSet( 
              new ColumnControlSet("id", txtId)
            );

            this.efwGridControl1.Click += efwGridControl1_Click;


            setCmb();


        }
        private void efwGridControl1_Click(object sender, EventArgs e)
        {


        }

        private void setCmb()
        {
            try
            {
                Dictionary<string, string> myRecord;


                string strQueruy1 = @"  SELECT
                              T1.DCODE, T1.DNAME
                              FROM
                              (  SELECT ''  DCODE, N'전체'  DNAME
	                             UNION ALL
	                             SELECT CODE DCODE, CODE_NM DNAME
                                 FROM dbo.ETCCODE
	                             WHERE GRP_CODE = 'MALL_TYPE' and Use_YN = 'Y' " + @") T1 ";

                CodeAgent.SetLegacyCode(cmbMALL_TYPE, strQueruy1);
                cmbMALL_TYPE.EditValue = "";


                string strQueruy2 = @"  SELECT
                              T1.DCODE, T1.DNAME
                              FROM
                              (  SELECT CODE  DCODE, CODE_NM DNAME
                                 FROM dbo.ETCCODE
	                             WHERE GRP_CODE = 'ORDER_SEARCH' " + @") T1 ";

                CodeAgent.SetLegacyCode(cmbORDER_SEARCH, strQueruy2);
                cmbORDER_SEARCH.EditValue = "1";


            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00025'   ";

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
            cmbChange_type.EditValue = "";

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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN01_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbORDER_SEARCH.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtI_SEARCH.EditValue;

                        if (rbP_SHOW_TYPE.EditValue.ToString() != "Y" && rbP_SHOW_TYPE.EditValue.ToString() != "N")
                            sP_SHOW_TYPE = null;
                        else
                            sP_SHOW_TYPE = rbP_SHOW_TYPE.EditValue.ToString();

                        // efwRadioGroup1.Properties.Items[efwRadioGroup1.SelectedIndex].Value.ToString()
                        cmd.Parameters.Add("i_is_order", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = sP_SHOW_TYPE;

                        cmd.Parameters.Add("i_order_mall_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = cmbMALL_TYPE.EditValue;

                        //Console.WriteLine(" i_sdate           ---> [" + cmd.Parameters[0].Value + "]" );
                        //Console.WriteLine(" i_edate           ---> [" + cmd.Parameters[1].Value + "]");
                        //Console.WriteLine(" i_type            ---> [" + cmd.Parameters[2].Value + "]");
                        //Console.WriteLine(" i_search          ---> [" + cmd.Parameters[3].Value + "]");
                        //Console.WriteLine(" i_is_order        ---> [" + cmd.Parameters[4].Value + "]");
                        //Console.WriteLine(" i_order_mall_type ---> [" + cmd.Parameters[5].Value + "]");


                        cmd.Parameters.Add("i_o_type1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = chkI.EditValue;

                        cmd.Parameters.Add("i_o_type2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[7].Value = chkO.EditValue;

                        cmd.Parameters.Add("i_o_type3", MySqlDbType.VarChar, 10);
                        cmd.Parameters[8].Value = chkP.EditValue;

                        cmd.Parameters.Add("i_o_type4", MySqlDbType.VarChar, 10);
                        cmd.Parameters[9].Value = chkD.EditValue;

                        cmd.Parameters.Add("i_o_type5", MySqlDbType.VarChar, 10);
                        cmd.Parameters[10].Value = chkF.EditValue;

                        cmd.Parameters.Add("i_o_type6", MySqlDbType.VarChar, 10);
                        cmd.Parameters[11].Value = chkE.EditValue;

                        cmd.Parameters.Add("i_o_type7", MySqlDbType.VarChar, 10);
                        cmd.Parameters[12].Value = chkC.EditValue;

                        cmd.Parameters.Add("i_o_type8", MySqlDbType.VarChar, 10);
                        cmd.Parameters[13].Value = chkZ.EditValue;

                        cmd.Parameters.Add("i_o_type9", MySqlDbType.VarChar, 10);
                        cmd.Parameters[14].Value = chkX.EditValue;

                        cmd.Parameters.Add("i_o_type10", MySqlDbType.VarChar, 10);
                        cmd.Parameters[15].Value = chkT.EditValue;

                        cmd.Parameters.Add("i_o_type11", MySqlDbType.VarChar, 10);
                        cmd.Parameters[16].Value = chkA.EditValue;

                        cmd.Parameters.Add("i_o_type12", MySqlDbType.VarChar, 10);
                        cmd.Parameters[17].Value = chkB.EditValue;

                        cmd.Parameters.Add("i_g_prod", MySqlDbType.VarChar, 10);
                        cmd.Parameters[18].Value = cbG_Prod.EditValue;

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
                        //Console.WriteLine("------------------------------------------------------------");
                        //Console.WriteLine("[U] " + dt.Rows[i]["o_code"].ToString());
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN01_SAVE_01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[0].Value = dt.Rows[i]["o_code"].ToString();

                                cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 250);
                                cmd.Parameters[1].Value = dt.Rows[i]["remark"].ToString();

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

        private void CmbORDER_SEARCH_EditValueChanged(object sender, EventArgs e)
        {
            txtI_SEARCH.EditValue = "";
        }

        private void EfwGridControl1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtI_SEARCH.Focus();
        }


        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                DataRow dr = this.efwGridControl1.GetSelectedRow(0);
                int nId = Convert.ToInt32(txtId.EditValue.ToString());
                if (nId != 0)
                {
                    this.txtId.EditValue = dr["id"].ToString();
                    popup = new frmDN01_Pop01();
                    popup.Id = Convert.ToInt32(txtId.EditValue.ToString());
                    txtId.EditValue = "0";
                    popup.ShowDialog();
                }
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
                gridView1.SetRowCellValue(i, gridView1.Columns["chk"], "Y");
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
                gridView1.SetRowCellValue(i, gridView1.Columns["chk"], "N");
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {

            if (MessageAgent.MessageShow(MessageType.Confirm, "발주완료 처리를 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {

                            if (gridView1.GetRowCellValue(i, "chk").ToString() == "Y")
                            {
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN01_SAVE_04", con))
                                {
                                    // Console.WriteLine("********" + gridView1.GetRowCellValue(i, "is_fix"));


                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_id", MySqlDbType.Int32, 50);
                                    cmd.Parameters[0].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "id"));

                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    MessageAgent.MessageShow(MessageType.Informational, "발주 완료처리 되었습니다.");
                    Search();
                }
                
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }

        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "주문 상태를 변경 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {

                            if (gridView1.GetRowCellValue(i, "chk").ToString() == "Y")
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

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            chkI.Checked = true;
            chkO.Checked = true;
            chkP.Checked = true;
            chkD.Checked = true;
            chkF.Checked = true;
            chkE.Checked = true;
            chkC.Checked = true;
            chkZ.Checked = false;
            chkX.Checked = false;
            chkT.Checked = false;
            chkA.Checked = true;
            chkB.Checked = true;
            Search();
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            chkI.Checked = true;
            chkO.Checked = true;
            chkP.Checked = true;
            chkD.Checked = true;
            chkF.Checked = false;
            chkE.Checked = true;
            chkC.Checked = false;
            chkZ.Checked = false;
            chkX.Checked = false;
            chkT.Checked = false;
            chkA.Checked = false;
            chkB.Checked = false;
            Search();
        }
    }
}
