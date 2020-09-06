#region "frmDN02 설명"
//===================================================================================================
//■Program Name  : frmDN02
//■Description   : 머니 거래현황
//■Author        : 조정현
//■Date          : 2019.05.30
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.05.30][조정현] Base
//[2] [2019.05.30][조정현] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
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
using MySql.Data.MySqlClient;

namespace YL_DONUT.BizFrm
{
    public partial class frmDN02 : FrmBase
    {
        public frmDN02()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "DN02";
            //폼명설정
            this.FrmName = "머니 거래현황";
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
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["RECV_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["RECV_AMOUNT"].SummaryItem.FieldName = "RECV_AMOUNT";
            gridView1.Columns["RECV_AMOUNT"].SummaryItem.DisplayFormat = "적립머니: {0:c}";

            gridView2.OptionsView.ShowFooter = true;
            gridView2.Columns["SEND_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["SEND_AMOUNT"].SummaryItem.FieldName = "SEND_AMOUNT";
            gridView2.Columns["SEND_AMOUNT"].SummaryItem.DisplayFormat = "사용머니: {0:c}";

            Clear();
        }

        #endregion

        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");

            Clear();
        }

        private void Clear()
        {
            //lblCD.Text = "0";
            lblRes.Text = "0";
            lblAD.Text = "0";
            lblTD.Text = "0";
            lblD.Text = "0";
            lblSD.Text = "0";
            //lblGM.Text = "0";
            //lblMM.Text = "0";
            //lblAM.Text = "0";
            lblID.Text = "0";

            cmbSel.EditValue = 1;

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt1T.EditValue = DateTime.Now;
            dt2F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt2T.EditValue = DateTime.Now;
        }

        public override void Search()
        {
            base.Search();

            try
            {
                
                if (this.txtQ.EditValue == null || string.IsNullOrEmpty(this.txtQ.Text.ToString()))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 회원을 선택하세요!");
                    txtQ.Focus();
                    return;
                }

                //회원체크
                Member_Chk(this.txtQ.EditValue.ToString());

                if (lblID.Text.Trim() == "")
                    return;

                //DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_DN02_SELECT_01", cmbSel.EditValue, txtQ.EditValue);
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_DN02_SELECT_01", lblID.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRes.Text = ds.Tables[0].Rows[0]["AMT_TOT"].ToString();
                    lblAD.Text = ds.Tables[0].Rows[0]["AD_MONEY"].ToString();
                    lblTD.Text = ds.Tables[0].Rows[0]["TD_MONEY"].ToString();
                    lblD.Text = ds.Tables[0].Rows[0]["D_MONEY"].ToString();
                    lblSD.Text = ds.Tables[0].Rows[0]["SD_MONEY"].ToString();
                    lblGD.Text = ds.Tables[0].Rows[0]["GD_MONEY"].ToString();
                    //lblGM.Text = ds.Tables[0].Rows[0]["GM_MONEY"].ToString();
                    //lblMM.Text = ds.Tables[0].Rows[0]["MM_MONEY"].ToString();
                    //lblAM.Text = ds.Tables[0].Rows[0]["AM_MONEY"].ToString();
                    lblID.Text = ds.Tables[0].Rows[0]["USER_ID"].ToString();

                    Open1();  // 회원기본정보
                }
                else
                {
                    lblRes.Text = "0";
                    lblAD.Text = "0";
                    lblTD.Text = "0";
                    lblD.Text = "0";
                    lblSD.Text = "0";
                    //lblGM.Text = "0";
                    //lblMM.Text = "0";
                    //lblAM.Text = "0";
                    lblID.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Member_Chk(string p1)
        {
            Dictionary<string, string> myRecord;

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                if (cmbSel.EditValue.ToString() == "0")
                {
                    sql.Query = "SELECT login_id FROM domalife.member_master " +
                                 "WHERE u_name like '%" + p1 + "%' ";
                }
                else if (cmbSel.EditValue.ToString() == "1")
                {
                    sql.Query = "SELECT login_id FROM domalife.member_master " +
                                 "WHERE u_nickname like '%" + p1 + "%' ";
                }
                else if (cmbSel.EditValue.ToString() == "2")
                {
                    sql.Query = "SELECT login_id FROM domalife.member_master " +
                                 "WHERE login_id like '%" + p1 + "%' ";
                }
                else if (cmbSel.EditValue.ToString() == "3")
                {
                    sql.Query = "SELECT login_id FROM domalife.member_master " +
                                 "WHERE replace(u_cell_num, '-','') = '" + p1.Replace("-", "") + "'";
                }

                myRecord = sql.selectQueryForSingleRecord();

                if (myRecord != null && myRecord.Count > 0)
                {
                    lblID.Text = myRecord["login_id"].ToString();
                }
            }
        }

        private void Open1()
        {
            try
            {
                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_login_id", MySqlDbType.VarChar, 255);
                        cmd.Parameters[0].Value = lblID.Text;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            //efwGridControl1.DataBind(dt);
                            //this.efwGridControl1.MyGridView.BestFitColumns();

                            if (dt.Rows.Count > 0)
                            {
                                txtU_NAME.EditValue = dt.Rows[0]["u_name"];
                                txtU_NICKNAME.EditValue = dt.Rows[0]["u_nickname"];
                                txtU_CHEF_LEVEL.EditValue = dt.Rows[0]["u_chef_level"];
                            }
                            else
                            {
                                txtU_NAME.EditValue = null;
                                txtU_NICKNAME.EditValue = null;
                                txtU_CHEF_LEVEL.EditValue = null;
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

        private void TxtQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void BtnSch1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //머니 적립 거래리스트
            if (string.IsNullOrEmpty(this.lblID.Text) || this.lblID.Text == "0")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회원을 선택하세요!");
                txtQ.Focus();
                return;
            }

            if (dt1F.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "기간을 선택하세요!");
                dt1F.Focus();
                return;
            }

            if (dt1T.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "기간을 선택하세요!");
                dt1T.Focus();
                return;
            }

            if (this.cmbType1.EditValue == null)
                this.cmbType1.EditValue = "";

            try
            {
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_DN02_SELECT_02"
                                                            , this.lblID.Text
                                                            , this.dt1F.EditValue3
                                                            , this.dt1T.EditValue3
                                                            , this.cmbType1.EditValue.ToString().Replace("%", ""));

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Cursor.Current = Cursors.Default;
        }

        private void BtnSch2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //머니 사용 거래리스트
            if (string.IsNullOrEmpty(this.lblID.Text) || this.lblID.Text == "0")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회원을 선택하세요!");
                txtQ.Focus();
                return;
            }

            if (dt2F.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "기간을 선택하세요!");
                dt2F.Focus();
                return;
            }

            if (dt2T.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "기간을 선택하세요!");
                dt2T.Focus();
                return;
            }

            if (this.cmbType2.EditValue == null)
                this.cmbType2.EditValue = "";

            try
            {
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_DN02_SELECT_03"
                                                            , this.lblID.Text
                                                            , this.dt2F.EditValue3
                                                            , this.dt2T.EditValue3
                                                            , this.cmbType2.EditValue.ToString().Replace("%", ""));

                efwGridControl2.DataBind(ds);
                this.efwGridControl2.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Cursor.Current = Cursors.Default;
        }

        private void GridView1_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.ForeColor = Color.Red;
        }

        private void GridView2_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.ForeColor = Color.Red;
        }

    }
}
