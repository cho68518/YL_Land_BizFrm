#region "frmDN11 설명"
//===================================================================================================
//■Program Name  : frmDN11
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
    public partial class frmDN11 : FrmBase
    {
        public frmDN11()
        {
            InitializeComponent();
            this.QCode = "frmDN11";
            //폼명설정
            this.FrmName = "발주 처리";
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

            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["p_num"].SummaryItem.FieldName = "p_num";
            gridView1.Columns["p_num"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            //gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "총주문금액: {0:c}";
            gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";



            //this.efwGridControl1.BindControlSet(
            //new ColumnControlSet("id", txtID)
            //  
            //this.efwGridControl1.Click += efwGridControl1_Click;
            rbP_SHOW_TYPE.EditValue = "T";


            SetCmb();

        }
        private void SetCmb()
        {
            try
            {
                Dictionary<string, string> myRecord;


                string strQueruy1 = @"  SELECT
                              T1.DCODE, T1.DNAME
                              FROM
                              (  SELECT ''  DCODE, N'전체'  DNAME
	                             UNION ALL
	                             SELECT CODE  DCODE, CODE_NM DNAME
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
        }

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_DN_DN11_SELECT_01", con))
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

                        
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        public override void Save()
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    //cmbO_TYPE.Focus();
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domamall.USP_DN_DN11_SAVE_01", con))
                            {
                                // Console.WriteLine("********" + gridView1.GetRowCellValue(i, "is_fix"));


                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_id", MySqlDbType.Int32, 50);
                                cmd.Parameters[0].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "id"));

                                cmd.Parameters.Add("i_o_delivery_num", MySqlDbType.VarChar, 20);
                                cmd.Parameters[1].Value = gridView1.GetRowCellValue(i, "o_delivery_num");

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
        }

    }
}
