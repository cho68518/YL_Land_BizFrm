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
    public partial class frmDN18 : FrmBase
    {
        public frmDN18()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = " frmDN18";
            //폼명설정
            this.FrmName = "G멀티샵 수수료 정산현황/마감";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            gridView1.OptionsView.ShowFooter = true;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "month_close_yn");

            gridView1.Columns["p_num"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["p_num"].SummaryItem.FieldName = "p_num";
            //gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "총주문금액: {0:c}";
            gridView1.Columns["p_num"].SummaryItem.DisplayFormat = "수량: {0}";


            gridView1.Columns["o_total_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_total_cost"].SummaryItem.FieldName = "o_total_cost";
            //gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "총주문금액: {0:c}";
            gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["chef_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["chef_amt"].SummaryItem.FieldName = "chef_amt";
            gridView1.Columns["chef_amt"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["fix_chef_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["fix_chef_amt"].SummaryItem.FieldName = "fix_chef_amt";
            gridView1.Columns["fix_chef_amt"].SummaryItem.DisplayFormat = "{0:c}";

            dtS_DATE.Properties.Mask.EditMask = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatString = "yyyy-MM";
            dtS_DATE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtS_DATE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dtS_DATE.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            dtS_DATE.EditValue = DateTime.Now.ToString("yyyy-MM");
            cmbORDER_SEARCH.EditValue = "1";

            SwitchChk();
        }

        #endregion


        #region 신규

        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
        }
        #endregion

        #region 조회

        public override void Search()
        {
            try
            {



                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN18_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 6);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 6);

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbORDER_SEARCH.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtI_SEARCH.EditValue;

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
            finally
            {
                this.Cursor = Cursors.Default;

                SwitchChk();
            }
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
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN18_SAVE_02", con))
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
                                    cmd.Parameters[18].Value = dt.Rows[i]["rec_u_nickname"].ToString();

                                    cmd.Parameters.Add("chef_amt", MySqlDbType.Int32, 15);
                                    cmd.Parameters[19].Value = Convert.ToInt32(dt.Rows[i]["fix_chef_amt"].ToString());

                                    cmd.Parameters.Add("tweet_cnt", MySqlDbType.Int32, 11);
                                    cmd.Parameters[20].Value = Convert.ToInt32(dt.Rows[i]["t_cnt"].ToString());

                                    cmd.Parameters.Add("is_write", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[21].Value = dt.Rows[i]["is_write"].ToString();

                                    cmd.Parameters.Add("i_idx", MySqlDbType.Int32, 15);
                                    cmd.Parameters[22].Value = Convert.ToInt32(dt.Rows[i]["idx"].ToString());

                                    cmd.Parameters.Add("gshop_name", MySqlDbType.VarChar, 100);
                                    cmd.Parameters[23].Value = Convert.ToInt32(dt.Rows[i]["idx"].ToString());

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


        #endregion



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
        private void SwitchChk()
        {
            if (MonthDataCnt() > 0)
                toggleSwitch1.IsOn = true;
            else
                toggleSwitch1.IsOn = false;

            string sAccDate = "";

            dtAcc_Date.EditValue = "";
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "SELECT date_format(acc_date, '%Y-%m-%d') FROM domamall.tb_ps_charge_month " +
                             "WHERE yymm = '" + dtS_DATE.EditValue3.Substring(0, 6) + "' and LENGTH(acc_date) > 0  and p_type = '02' group by acc_date ";
                DataSet ds = sql.selectQueryDataSet();
                sAccDate = sql.selectQueryForSingleValue();
            }
            dtAcc_Date.EditValue = sAccDate;
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
                    deleteCommand.CommandText = "update domamall.tb_ps_charge_month set acc_date = ''  WHERE yymm=@wyymm and p_type = '02' ";

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
        private int MonthDataCnt()
        {
            int nCnt = 0;

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "SELECT count(*) FROM domamall.tb_ps_charge_month " +
                             "WHERE yymm = '" + dtS_DATE.EditValue3.Substring(0, 6) + "' and LENGTH(acc_date) > 0 and p_type = '02' ";
                DataSet ds = sql.selectQueryDataSet();
                nCnt = Convert.ToInt32(sql.selectQueryForSingleValue());
            }

            return nCnt;
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
                deleteCommand.CommandText = "update domamall.tb_ps_charge_month set acc_date = '" + dtAcc_Date.EditValue3.Substring(0, 8) + "'  WHERE yymm=@wyymm and p_type = '02' ";

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
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN18_SAVE_01", con))
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

                            cmd.Parameters.Add("gshop_name", MySqlDbType.VarChar, 100);
                            cmd.Parameters[23].Value = gridView1.GetRowCellValue(i, "o_receive_name");


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

        private void TxtI_SEARCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void DtS_DATE_EditValueChanged(object sender, EventArgs e)
        {
            Search();
        }


    }
}
