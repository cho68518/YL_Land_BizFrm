﻿using DevExpress.XtraEditors;
using Easy.Framework.Common;
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
    public partial class frmDN10 : FrmBase
    {
        public frmDN10()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = " frmDN10";
            //폼명설정
            this.FrmName = "PS수수료 정산현황/마감";
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

            dtS_DATE.EditValue = DateTime.Now.ToString("yyyy-MM");

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

            gridView1.Columns["chef_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["chef_amt"].SummaryItem.FieldName = "chef_amt";
            gridView1.Columns["chef_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["t_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["t_cnt"].SummaryItem.FieldName = "t_cnt";
            gridView1.Columns["t_cnt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["o_donut_d_cost"].Visible = false;
            gridView1.Columns["o_donut_m_cost"].Visible = false;
            gridView1.Columns["o_donut_c_cost"].Visible = false;
            gridView1.Columns["o_delivery_cost"].Visible = false;
            gridView1.Columns["o_pay_type"].Visible = false;
            gridView1.Columns["is_order"].Visible = false;
            gridView1.Columns["s_company_name"].Visible = false;
            gridView1.Columns["o_delivery_comp_name"].Visible = false;
            gridView1.Columns["o_delivery_num"].Visible = false;
            gridView1.Columns["o_delivery_start_date"].Visible = false;
            gridView1.Columns["o_delivery_end_date"].Visible = false;

            //this.efwGridControl1.BindControlSet(
            //new ColumnControlSet("id", txtID)
            //  
            //this.efwGridControl1.Click += efwGridControl1_Click;
            rbP_SHOW_TYPE.EditValue = "T";

            chkD.Checked = true;
            chkF.Checked = true;
            chkE.Checked = true;

            dtS_DATE.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            setCmb();
        }

        #endregion

        #region 콤보박스

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

        #endregion

        #region 조회

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                chkD.Checked = true;
                chkF.Checked = false;
                chkE.Checked = true;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_DN_DN10_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = "";

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

                        cmd.Parameters.Add("i_o_type1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = "N";

                        cmd.Parameters.Add("i_o_type2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[7].Value = "N";

                        cmd.Parameters.Add("i_o_type3", MySqlDbType.VarChar, 10);
                        cmd.Parameters[8].Value = "N";

                        cmd.Parameters.Add("i_o_type4", MySqlDbType.VarChar, 10);
                        cmd.Parameters[9].Value = chkD.EditValue;

                        cmd.Parameters.Add("i_o_type5", MySqlDbType.VarChar, 10);
                        cmd.Parameters[10].Value = chkF.EditValue;

                        cmd.Parameters.Add("i_o_type6", MySqlDbType.VarChar, 10);
                        cmd.Parameters[11].Value = chkE.EditValue;

                        cmd.Parameters.Add("i_o_type7", MySqlDbType.VarChar, 10);
                        cmd.Parameters[12].Value = "N";

                        cmd.Parameters.Add("i_o_type8", MySqlDbType.VarChar, 10);
                        cmd.Parameters[13].Value = "N";

                        cmd.Parameters.Add("i_o_type9", MySqlDbType.VarChar, 10);
                        cmd.Parameters[14].Value = "N";

                        cmd.Parameters.Add("i_o_type10", MySqlDbType.VarChar, 10);
                        cmd.Parameters[15].Value = "N";

                        cmd.Parameters.Add("i_o_type11", MySqlDbType.VarChar, 10);
                        cmd.Parameters[16].Value = "N";

                        cmd.Parameters.Add("i_o_type12", MySqlDbType.VarChar, 10);
                        cmd.Parameters[17].Value = "N";

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

        private void GridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            //e.Appearance.BackColor = Color.White;
            e.Appearance.ForeColor = Color.Red;
        }

        private void BtnDetail_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton btn = sender as CheckButton;
            if (btn.Checked)
            {
                btn.Appearance.BackColor = Color.LightGreen;
                btn.Appearance.BackColor2 = Color.DarkGreen;

                gridView1.Columns["o_donut_d_cost"].Visible = true;
                gridView1.Columns["o_donut_m_cost"].Visible = true;
                gridView1.Columns["o_donut_c_cost"].Visible = true;
                gridView1.Columns["o_delivery_cost"].Visible = true;
                gridView1.Columns["o_pay_type"].Visible = true;
                gridView1.Columns["is_order"].Visible = true;
                gridView1.Columns["s_company_name"].Visible = true;
                gridView1.Columns["o_delivery_num"].Visible = true;
                gridView1.Columns["o_delivery_start_date"].Visible = true;
                gridView1.Columns["o_delivery_end_date"].Visible = true;
                gridView1.Columns["o_delivery_comp_name"].Visible = true;
            }
            else
            {
                //btn.Appearance.BackColor = Color.LightBlue;
                //btn.Appearance.BackColor2 = Color.DarkBlue;
                btn.Appearance.BackColor = Color.Transparent;
                btn.Appearance.BackColor2 = Color.Transparent;

                gridView1.Columns["o_donut_d_cost"].Visible = false;
                gridView1.Columns["o_donut_m_cost"].Visible = false;
                gridView1.Columns["o_donut_c_cost"].Visible = false;
                gridView1.Columns["o_delivery_cost"].Visible = false;
                gridView1.Columns["o_pay_type"].Visible = false;
                gridView1.Columns["is_order"].Visible = false;
                gridView1.Columns["s_company_name"].Visible = false;
                gridView1.Columns["o_delivery_num"].Visible = false;
                gridView1.Columns["o_delivery_start_date"].Visible = false;
                gridView1.Columns["o_delivery_end_date"].Visible = false;
                gridView1.Columns["o_delivery_comp_name"].Visible = false;
            }
        }
    }
}
