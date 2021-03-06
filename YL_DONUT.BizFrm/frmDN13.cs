﻿using Easy.Framework.Common;
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
    public partial class frmDN13 : FrmBase
    {
        public frmDN13()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = " frmDN13";
            //폼명설정
            this.FrmName = "PS수수료 월 정산요약";
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
            //GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_write");

           advBandedGridView1.OptionsView.ShowFooter = true;

            //gridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            ////gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "총주문금액: {0:c}";
            //gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";


            advBandedGridView1.Columns["g_cnt1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["g_cnt1"].SummaryItem.FieldName = "g_cnt1";
            advBandedGridView1.Columns["g_cnt1"].SummaryItem.DisplayFormat = "{0:###,###,##0}";

            advBandedGridView1.Columns["chef_refund"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["chef_refund"].SummaryItem.FieldName = "chef_refund";
            advBandedGridView1.Columns["chef_refund"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            advBandedGridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["o_donut_d_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["o_donut_d_cost"].SummaryItem.FieldName = "o_donut_d_cost";
            advBandedGridView1.Columns["o_donut_d_cost"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["o_donut_c_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["o_donut_c_cost"].SummaryItem.FieldName = "o_donut_c_cost";
            advBandedGridView1.Columns["o_donut_c_cost"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["o_purchase_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["o_purchase_cost"].SummaryItem.FieldName = "o_purchase_cost";
            advBandedGridView1.Columns["o_purchase_cost"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["t_cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["t_cnt"].SummaryItem.FieldName = "t_cnt";
            advBandedGridView1.Columns["t_cnt"].SummaryItem.DisplayFormat = "{0:###,###,##0}";

            advBandedGridView1.Columns["g_cnt2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["g_cnt2"].SummaryItem.FieldName = "g_cnt2";
            advBandedGridView1.Columns["g_cnt2"].SummaryItem.DisplayFormat = "{0:###,###,##0}";

            advBandedGridView1.Columns["g_chef_refund"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["g_chef_refund"].SummaryItem.FieldName = "g_chef_refund";
            advBandedGridView1.Columns["g_chef_refund"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["o_total_cost2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["o_total_cost2"].SummaryItem.FieldName = "o_total_cost2";
            advBandedGridView1.Columns["o_total_cost2"].SummaryItem.DisplayFormat = "{0:c}";

            advBandedGridView1.Columns["t_chef_refund"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["t_chef_refund"].SummaryItem.FieldName = "t_chef_refund";
            advBandedGridView1.Columns["t_chef_refund"].SummaryItem.DisplayFormat = "{0:c}";

            //this.efwGridControl1.BindControlSet(
            //new ColumnControlSet("id", txtID)
            //  
            //this.efwGridControl1.Click += efwGridControl1_Click;
            //rbP_SHOW_TYPE.EditValue = "T";

            dtS_DATE.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            //setCmb();

            //cmbORDER_SEARCH.EditValue = "1";
        }

        #endregion

        #region 조회

        public override void Search()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string sP_SHOW_TYPE = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN13_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[1].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        #endregion

        private void DtS_DATE_EditValueChanged(object sender, EventArgs e)
        {
            //Search();
        }
    }
}
