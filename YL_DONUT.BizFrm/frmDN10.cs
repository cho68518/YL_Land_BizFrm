using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Views.Grid;
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
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //this.lblnum1.Font = new Font(this.lblnum1.Font, FontStyle.Bold);
            
            dtS_DATE.EditValue = DateTime.Now.ToString("yyyy-MM");

            //그리드 컬럼에 체크박스 레포지토리아이템 추가
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_write");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_psoper_order");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "month_close_yn");

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

            gridView1.Columns["fix_chef_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["fix_chef_amt"].SummaryItem.FieldName = "fix_chef_amt";
            gridView1.Columns["fix_chef_amt"].SummaryItem.DisplayFormat = "{0:c}";


            //gridView1.Columns["o_donut_d_cost"].Visible = false;
            gridView1.Columns["o_donut_m_cost"].Visible = false;
            //gridView1.Columns["o_donut_c_cost"].Visible = false;
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
            //rbP_SHOW_TYPE.EditValue = "T";

            //chkD.Checked = true;
            //chkF.Checked = true;
            //chkE.Checked = true;

            dtS_DATE.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            setCmb();

            cmbORDER_SEARCH.EditValue = "1";




            //월마감 데이타가 있으면 월마감데이타 조회
            if (MonthDataCnt() > 0)
            {
                toggleSwitch1.IsOn = true;
                //Search2();
            }
            else
            {
                toggleSwitch1.IsOn = false;
                //Search1();
            }
        }

        #endregion

        #region 콤보박스

        private void setCmb()
        {
            //try
            //{
            //    Dictionary<string, string> myRecord;


            //    string strQueruy1 = @"  SELECT
            //                  T1.DCODE, T1.DNAME
            //                  FROM
            //                  (  SELECT ''  DCODE, N'전체'  DNAME
	           //                  UNION ALL
	           //                  SELECT CODE  DCODE, CODE_NM DNAME
            //                     FROM dbo.ETCCODE
	           //                  WHERE GRP_CODE = 'MALL_TYPE' and Use_YN = 'Y' " + @") T1 ";

            //    CodeAgent.SetLegacyCode(cmbMALL_TYPE, strQueruy1);
            //    cmbMALL_TYPE.EditValue = "";

            //    //string strQueruy2 = @"  SELECT
            //    //              T1.DCODE, T1.DNAME
            //    //              FROM
            //    //              (  SELECT CODE  DCODE, CODE_NM DNAME
            //    //                 FROM dbo.ETCCODE
	           //    //              WHERE GRP_CODE = 'ORDER_SEARCH' " + @") T1 ";

            //    //CodeAgent.SetLegacyCode(cmbORDER_SEARCH, strQueruy2);
            //    //cmbORDER_SEARCH.EditValue = "1";
            //}
            //catch (Exception ex)
            //{
            //    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            //}
        }

        #endregion

        #region 주문내역 가져오기

        public override void Search()
        {
            //주문내역 가져오기

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string sP_SHOW_TYPE = string.Empty;

                //chkD.Checked = true;
                //chkF.Checked = false;
                //chkE.Checked = true;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN10_SELECT_01", con))
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

                SwitchChk();
            }
        }

        #endregion

        private void SwitchChk()
        {
            if (MonthDataCnt() > 0)
                toggleSwitch1.IsOn = true;
            else
                toggleSwitch1.IsOn = false;

        }

        private void GridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            //e.Appearance.BackColor = Color.White;
            
        }

        public override void Save()
        {
            if (dtAcc_Date.EditValue3 != "")
            {
                MessageAgent.MessageShow(MessageType.Confirm, "마감 처리되어 수정할수 없습니다.");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
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
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN10_SAVE_02", con))
                                {

                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_yymm", MySqlDbType.VarChar, 6);
                                    cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                                    cmd.Parameters.Add("i_acc_date", MySqlDbType.VarChar, 10);
                                    cmd.Parameters[1].Value = dtAcc_Date.EditValue3;

                                    cmd.Parameters.Add("o_date", MySqlDbType.DateTime, 50);
                                    cmd.Parameters[2].Value = Convert.ToDateTime(dt.Rows[i]["o_date"].ToString());

                                    cmd.Parameters.Add("o_id", MySqlDbType.Int32, 11);
                                    cmd.Parameters[3].Value = Convert.ToInt32(dt.Rows[i]["id"].ToString());

                                    cmd.Parameters.Add("o_code", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[4].Value = dt.Rows[i]["o_code"].ToString();

                                    cmd.Parameters.Add("o_u_id", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[5].Value = dt.Rows[i]["o_u_id"].ToString();

                                    cmd.Parameters.Add("o_u_name", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[6].Value = dt.Rows[i]["o_receive_name"].ToString();

                                    cmd.Parameters.Add("o_u_nickname", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[7].Value = dt.Rows[i]["u_nickname"].ToString();

                                    cmd.Parameters.Add("o_type", MySqlDbType.VarChar, 20);
                                    cmd.Parameters[8].Value = dt.Rows[i]["o_type"].ToString();

                                    cmd.Parameters.Add("p_id", MySqlDbType.Int32, 11);
                                    cmd.Parameters[9].Value = Convert.ToInt32(dt.Rows[i]["p_code"].ToString());

                                    cmd.Parameters.Add("product_name", MySqlDbType.VarChar, 1000);
                                    cmd.Parameters[10].Value = dt.Rows[i]["p_name"].ToString();

                                    cmd.Parameters.Add("option_id", MySqlDbType.Int32, 11);
                                    cmd.Parameters[11].Value = Convert.ToInt32(dt.Rows[i]["p_p_id"].ToString());

                                    cmd.Parameters.Add("option_name", MySqlDbType.VarChar, 1000);
                                    cmd.Parameters[12].Value = dt.Rows[i]["p_option_name"].ToString();

                                    cmd.Parameters.Add("o_qty", MySqlDbType.Int32, 10);
                                    cmd.Parameters[13].Value = Convert.ToInt32(dt.Rows[i]["p_num"].ToString());

                                    cmd.Parameters.Add("o_total_cost", MySqlDbType.Int32, 15);
                                    cmd.Parameters[14].Value = Convert.ToInt32(dt.Rows[i]["o_total_cost"].ToString());

                                    cmd.Parameters.Add("o_purchase_cost", MySqlDbType.Int32, 15);
                                    cmd.Parameters[15].Value = Convert.ToInt32(dt.Rows[i]["o_purchase_cost"].ToString());

                                    cmd.Parameters.Add("chef_login_id", MySqlDbType.VarChar, 128);
                                    cmd.Parameters[16].Value = dt.Rows[i]["u_id2"].ToString();

                                    cmd.Parameters.Add("chef_name", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[17].Value = dt.Rows[i]["chef_name"].ToString();

                                    cmd.Parameters.Add("chef_nickname", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[18].Value = dt.Rows[i]["chef_nickname"].ToString();

                                    cmd.Parameters.Add("chef_amt", MySqlDbType.Int32, 15);
                                    cmd.Parameters[19].Value = Convert.ToInt32(dt.Rows[i]["fix_chef_amt"].ToString());

                                    cmd.Parameters.Add("tweet_cnt", MySqlDbType.Int32, 11);
                                    cmd.Parameters[20].Value = Convert.ToInt32(dt.Rows[i]["t_cnt"].ToString());

                                    cmd.Parameters.Add("is_write", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[21].Value = dt.Rows[i]["is_write"].ToString();

                                    cmd.Parameters.Add("i_idx", MySqlDbType.Int32, 15);
                                    cmd.Parameters[22].Value = Convert.ToInt32(dt.Rows[i]["idx"].ToString());

                                    //Console.WriteLine("o_date--->" + dt.Rows[i]["o_date"].ToString());
                                    //Console.WriteLine("idx--->" + dt.Rows[i]["idx"].ToString());


                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    Search();
                }


                
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }








        private void BtnDetail_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton btn = sender as CheckButton;
            if (btn.Checked)
            {
                btn.Appearance.BackColor = Color.LightGreen;
                btn.Appearance.BackColor2 = Color.DarkGreen;

                //gridView1.Columns["o_donut_d_cost"].Visible = true;
                gridView1.Columns["o_donut_m_cost"].Visible = true;
                //gridView1.Columns["o_donut_c_cost"].Visible = true;
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

                //gridView1.Columns["o_donut_d_cost"].Visible = false;
                gridView1.Columns["o_donut_m_cost"].Visible = false;
                //gridView1.Columns["o_donut_c_cost"].Visible = false;
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

        private void TxtI_SEARCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void CmbORDER_SEARCH_Popup(object sender, EventArgs e)
        {
            LookUpEdit edit = sender as LookUpEdit;
            PopupLookUpEditForm f = (edit as IPopupControl).PopupWindow as PopupLookUpEditForm;
            f.Width = edit.Width + 100;
            f.Height = edit.Height + 190;
        }

        private void BtnFix_Click(object sender, EventArgs e)
        {
            if (gridView1.DataRowCount <= 0)
            {
                MessageAgent.MessageShow(MessageType.Confirm, "처리할 내역이 없습니다.");
                return;
            }

            if (MonthDataCnt() > 0)
            {
                if (MessageAgent.MessageShow(MessageType.Confirm, "선택된 해당월에 이미 마감처리되었습니다. 계속 하시겠습니까?") == DialogResult.OK)
                {
                    MonthClose();
                }
            }
            else
            {
                //마감처리
                if (MessageAgent.MessageShow(MessageType.Confirm, "수수료 적용을 하시겠습니까?") == DialogResult.OK)
                {
                    MonthClose();
                }
            }

            SwitchChk();
        }

        private void MonthClose()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //var saveResult = new SaveTableResultInfo() { IsError = true };

                //var dt = efwGridControl1.GetChangeDataWithRowState;
                //var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                ////saveResult.InsertRowcount = dt.Select(StatusColumn + "='I'").Length;
                ////saveResult.UpdateRowcount = dt.Select(StatusColumn + "='U'").Length;
                ////saveResult.DeleteRowcount = dt.Select(StatusColumn + "='D'").Length;

                //for (var i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (dt.Rows[i][StatusColumn].ToString() == "U")
                //    {
                //        //Console.WriteLine("------------------------------------------------------------");
                //        //Console.WriteLine("[U] " + dt.Rows[i]["chef_nickname"].ToString());
                //    }
                //}

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN10_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_yymm", MySqlDbType.VarChar, 10);
                            cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                            cmd.Parameters.Add("i_acc_date", MySqlDbType.VarChar, 10);
                            cmd.Parameters[1].Value = "";

                            cmd.Parameters.Add("o_date", MySqlDbType.DateTime, 50);
                            cmd.Parameters[2].Value = gridView1.GetRowCellValue(i, "o_date");

                            cmd.Parameters.Add("o_id", MySqlDbType.Int32, 11);
                            cmd.Parameters[3].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "id"));

                            cmd.Parameters.Add("o_code", MySqlDbType.VarChar, 50);
                            cmd.Parameters[4].Value = gridView1.GetRowCellValue(i, "o_code");

                            cmd.Parameters.Add("o_u_id", MySqlDbType.VarChar, 50);
                            cmd.Parameters[5].Value = gridView1.GetRowCellValue(i, "o_u_id");

                            cmd.Parameters.Add("o_u_name", MySqlDbType.VarChar, 50);
                            cmd.Parameters[6].Value = gridView1.GetRowCellValue(i, "o_receive_name");

                            cmd.Parameters.Add("o_u_nickname", MySqlDbType.VarChar, 50);
                            cmd.Parameters[7].Value = gridView1.GetRowCellValue(i, "u_nickname");

                            cmd.Parameters.Add("o_type", MySqlDbType.VarChar, 20);
                            cmd.Parameters[8].Value = gridView1.GetRowCellValue(i, "o_type");

                            cmd.Parameters.Add("p_id", MySqlDbType.Int32, 11);
                            cmd.Parameters[9].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "p_code"));

                            cmd.Parameters.Add("product_name", MySqlDbType.VarChar, 1000);
                            cmd.Parameters[10].Value = gridView1.GetRowCellValue(i, "p_name");

                            cmd.Parameters.Add("option_id", MySqlDbType.Int32, 11);
                            cmd.Parameters[11].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "p_p_id"));

                            cmd.Parameters.Add("option_name", MySqlDbType.VarChar, 1000);
                            cmd.Parameters[12].Value = gridView1.GetRowCellValue(i, "p_option_name");

                            cmd.Parameters.Add("o_qty", MySqlDbType.Int32, 10);
                            cmd.Parameters[13].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "p_num"));

                            cmd.Parameters.Add("o_total_cost", MySqlDbType.Int32, 15);
                            cmd.Parameters[14].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "o_total_cost"));

                            cmd.Parameters.Add("o_purchase_cost", MySqlDbType.Int32, 15);
                            cmd.Parameters[15].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "o_purchase_cost"));

                            cmd.Parameters.Add("chef_login_id", MySqlDbType.VarChar, 128);
                            cmd.Parameters[16].Value = gridView1.GetRowCellValue(i, "u_id2");
                            cmd.Parameters.Add("chef_name", MySqlDbType.VarChar, 50);
           
                            cmd.Parameters[17].Value = gridView1.GetRowCellValue(i, "chef_name");

                            cmd.Parameters.Add("chef_nickname", MySqlDbType.VarChar, 50);
                            cmd.Parameters[18].Value = gridView1.GetRowCellValue(i, "chef_nickname");

                            cmd.Parameters.Add("chef_amt", MySqlDbType.Int32, 15);
                            cmd.Parameters[19].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "chef_amt"));

                            cmd.Parameters.Add("tweet_cnt", MySqlDbType.Int32, 11);
                            cmd.Parameters[20].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "t_cnt"));

                            cmd.Parameters.Add("is_write", MySqlDbType.VarChar, 2);
                            cmd.Parameters[21].Value = gridView1.GetRowCellValue(i, "is_write");

                            cmd.Parameters.Add("i_idx", MySqlDbType.Int32, 15);
                            cmd.Parameters[22].Value = gridView1.GetRowCellValue(i, "idx");

                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                Cursor.Current = Cursors.Default;
                Search();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private int MonthDataCnt()
        {
            int nCnt = 0;

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "SELECT count(*) FROM domamall.tb_ps_charge_month " +
                             "WHERE yymm = '" + dtS_DATE.EditValue3.Substring(0, 6) + "' and LENGTH(acc_date) > 0  and p+type = '01' ";
                DataSet ds = sql.selectQueryDataSet();
                nCnt = Convert.ToInt32(sql.selectQueryForSingleValue());
            }

            return nCnt;
        }

        private void MonthDataChek()
        {
            try
            {
                if (MonthDataCnt() > 0)
                {
                    MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Real);
                    connection.Open();

                    MySqlCommand deleteCommand = new MySqlCommand();
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandText = "update domamall.tb_ps_charge_month set acc_date = ''  WHERE yymm=@wyymm and p_type = '01' ";

                    deleteCommand.Parameters.Add("@wyymm", MySqlDbType.VarChar, 10);
                    deleteCommand.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                    deleteCommand.ExecuteNonQuery();
                    connection.Close();

                    dtAcc_Date.EditValue = "";
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void MonthDataClose()
        {
            try
            {

                MySqlConnection connection = new MySqlConnection(ConstantLib.BasicConn_Real);
                connection.Open();
                // '" + dtS_DATE.EditValue3.Substring(0, 6) + "'
                MySqlCommand deleteCommand = new MySqlCommand();
                deleteCommand.Connection = connection;
                deleteCommand.CommandText = "update domamall.tb_ps_charge_month set acc_date = '"  + dtAcc_Date.EditValue3.Substring(0, 8) + "'  WHERE yymm=@wyymm and p_type = '01' ";

                deleteCommand.Parameters.Add("@wyymm", MySqlDbType.VarChar, 10);
                deleteCommand.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                deleteCommand.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void BtnFixCancel_Click(object sender, EventArgs e)
        {
            // 마감취소 
            try
            {
                //마감 취소처리
                if (MessageAgent.MessageShow(MessageType.Confirm, "마감 취소처리를 하시겠습니까?") == DialogResult.OK)
                {
                    MonthDataChek();
                    SwitchChk();

                    MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                }
                Search();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void BtnFixDate_Click(object sender, EventArgs e)
        {
            // 마감확정


            try
            {

                if (dtAcc_Date.EditValue3 == "")
                {
                    MessageAgent.MessageShow(MessageType.Confirm, "마감 일자를 선택하세요.");
                    return;
                }

                if (Convert.ToInt32(dtAcc_Date.EditValue3.Substring(0, 6)) <= Convert.ToInt32(dtS_DATE.EditValue3.Substring(0, 6)))
                {
                    MessageAgent.MessageShow(MessageType.Confirm, " [주문/마감 월(月)] 년월보다 이후의 [마감일자]를 선택하세요.");
                    return;
                }

                //마감 취소처리
                if (MessageAgent.MessageShow(MessageType.Confirm, "마감 처리를 하시겠습니까?") == DialogResult.OK)
                {
                    MonthDataClose();
                    SwitchChk();

                    MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                }
                Search();
            }
             
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void GridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column == view.Columns["fix_chef_amt"])
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }

        private void TxtI_SEARCH_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        //private void EfwGridControl1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        dtS_DATE.Focus();
        //}
    }
}
