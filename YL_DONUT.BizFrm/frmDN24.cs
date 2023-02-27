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
using YL_DONUT.BizFrm.Dlg;

namespace YL_DONUT.BizFrm
{
    public partial class frmDN24 : FrmBase
    {
        frmDN24_Pop01 popup;
        public frmDN24()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "DN24";
            //폼명설정
            this.FrmName = "온라인 주문등록";
        }

        private void frmDN24_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            dtO_Date.EditValue = DateTime.Now;
            cmbO_Company.EditValue = "2";
            cmbO_Sub_Company.EditValue = "0";
            dtevent_end_date.EditValue = DateTime.Now;
            rbp_bunch_delivery.EditValue = "N";
            rbp_bunch_delivery1.EditValue = "N";

            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["o_qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_qty"].SummaryItem.FieldName = "o_qty";
            //gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "총주문금액: {0:c}";
            gridView1.Columns["o_qty"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["o_amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["o_amt"].SummaryItem.FieldName = "o_amt";
            //gridView1.Columns["o_total_cost"].SummaryItem.DisplayFormat = "총주문금액: {0:c}";
            gridView1.Columns["o_amt"].SummaryItem.DisplayFormat = "{0:c}";

            //      cmbTAREA1.EditValue = "00";
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("idx", txtIdx)
                    , new ColumnControlSet("o_date", dtO_Date)
                    , new ColumnControlSet("O_Qty", txtO_Qty)
                    , new ColumnControlSet("O_Amt", txtO_Amt)
                    , new ColumnControlSet("Remark", txtRemark)
                    , new ColumnControlSet("o_company", cmbO_Company)
                    , new ColumnControlSet("o_sub_company", cmbO_Sub_Company)
                    , new ColumnControlSet("o_type", txto_type)
                    , new ColumnControlSet("p_name", txtp_name)
                    , new ColumnControlSet("option_name", txtoption_name)
                    , new ColumnControlSet("p_bunch_delivery", rbp_bunch_delivery)
                    , new ColumnControlSet("o_receive_name", txto_receive_name)
                    , new ColumnControlSet("o_receive_contact", txto_receive_contact)
                    , new ColumnControlSet("o_receive_name1", txto_receive_name1)
                    , new ColumnControlSet("o_receive_contact1", txto_receive_contact1)
                    , new ColumnControlSet("o_receive_zipcode1", txto_receive_zipcode1)
                    , new ColumnControlSet("o_receive_address1", txto_receive_address1)
                    , new ColumnControlSet("o_receive_message1", txto_receive_message1)
                    , new ColumnControlSet("o_delivery_num", txto_delivery_num)
                    , new ColumnControlSet("add_p_name", txtadd_p_name)
                    , new ColumnControlSet("o_delivery_comp_code", cmbo_delivery_comp_code)
                    , new ColumnControlSet("event_name", txtevent_name)
                   ); ;

            this.efwGridControl1.Click += efwGridControl1_Click;

            this.efwGridControl2.BindControlSet(
                      new ColumnControlSet("Idx", txtIdx1)
                    , new ColumnControlSet("Remark", txtRemark1)
                    , new ColumnControlSet("o_sub_company", cmbO_Sub_Company1)
                    , new ColumnControlSet("p_code", txtp_code)
                    , new ColumnControlSet("p_name", txtp_name1)
                    , new ColumnControlSet("option_name", txtoption_name1)
                    , new ColumnControlSet("event_name", txtevent_name1)
                    , new ColumnControlSet("p_bunch_delivery", rbp_bunch_delivery1)
                    , new ColumnControlSet("add_p_name", txtadd_p_name1)
                    , new ColumnControlSet("p_bunch_delivery", rbp_bunch_delivery1)
                   ); ;

            this.efwGridControl2.Click += efwGridControl2_Click;
            SetCmb();
        }


        private void SetCmb()
        {

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT code_id as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = 00042 order by code_id ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbO_Sub_Company, codeArray);
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT code_id as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = 00041 order by code_id ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbO_Company, codeArray);
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT code_id as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = 00042 order by code_id ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbO_Sub_Company1, codeArray);
            }

        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                this.txtIdx.EditValue = dr["idx"].ToString();
                this.txto_receive_zipcode1.EditValue = dr["o_receive_zipcode1"].ToString();
            }
        }

        private void from_new()
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
            dtO_Date.EditValue = DateTime.Now;
            cmbO_Sub_Company.EditValue = "0";
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
        }
        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

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
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbO_Company.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 온라인몰을 선택하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtO_Qty.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 수량을 입력하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtO_Amt.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 금액을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtevent_name.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 이벤트명을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtp_name.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 제품명을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtoption_name.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 옵션명을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtadd_p_name.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 발주옵션명을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txto_receive_name.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 주문자명을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txto_receive_contact.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 주문자명 연락처를 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txto_receive_name1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 수령자명을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txto_receive_contact1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 수령자 연락처를 입력하세요!");
                return;
            }


            if (string.IsNullOrEmpty(this.txto_receive_address1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 주소를 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txto_receive_zipcode1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 우편번호을 입력하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txto_receive_message1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 배송메세지를 입력하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "P";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_o_date"].Value = dtO_Date.EditValue;
                            cmd.Parameters["i_o_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_company"].Value = cmbO_Company.EditValue;
                            cmd.Parameters["i_o_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_sub_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_sub_company"].Value = cmbO_Sub_Company.EditValue;
                            cmd.Parameters["i_o_sub_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_qty", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_qty"].Value = Convert.ToInt32(txtO_Qty.EditValue);
                            cmd.Parameters["i_o_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_amt", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_amt"].Value = Convert.ToInt32(txtO_Amt.EditValue.ToString().Replace(",", "").Replace(".", ""));

                            cmd.Parameters["i_o_amt"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_name"].Value = txtp_name.EditValue;
                            cmd.Parameters["i_p_name"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_option_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_option_name"].Value = txtoption_name.EditValue;
                            cmd.Parameters["i_option_name"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_add_p_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_add_p_name"].Value = txtadd_p_name.EditValue;
                            cmd.Parameters["i_add_p_name"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_p_bunch_delivery", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_bunch_delivery"].Value = rbp_bunch_delivery.EditValue;
                            cmd.Parameters["i_p_bunch_delivery"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_name"].Value = txto_receive_name.EditValue;
                            cmd.Parameters["i_o_receive_name"].Direction = ParameterDirection.Input;



                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_contact", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_contact"].Value = txto_receive_contact.EditValue;
                            cmd.Parameters["i_o_receive_contact"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_name1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_name1"].Value = txto_receive_name1.EditValue;
                            cmd.Parameters["i_o_receive_name1"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_contact1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_contact1"].Value = txto_receive_contact1.EditValue;
                            cmd.Parameters["i_o_receive_contact1"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_zipcode1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_zipcode1"].Value = txto_receive_zipcode1.EditValue;
                            cmd.Parameters["i_o_receive_zipcode1"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_address1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_address1"].Value = txto_receive_address1.EditValue;
                            cmd.Parameters["i_o_receive_address1"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_message1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_message1"].Value = txto_receive_message1.EditValue;
                            cmd.Parameters["i_o_receive_message1"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_event_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_event_name"].Value = txtevent_name.EditValue;
                            cmd.Parameters["i_event_name"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
            }
        }


        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            rbp_bunch_delivery.EditValue = "N";
            from_new();

        }

        private void txto_receive_zipcode1_Click(object sender, EventArgs e)
        {
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = txto_receive_zipcode1, ParentAddr1 = txto_receive_address1, ParentAddr2 = txto_receive_address2 };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
        }

        private void btnExcelUpdate_Click(object sender, EventArgs e)
        {
            popup = new frmDN24_Pop01();
            popup.ShowDialog();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            Eraser.Clear(this, "CLR2");
            cmbO_Sub_Company.EditValue = "0";
            dtevent_end_date.EditValue = DateTime.Now;
            rbp_bunch_delivery1.EditValue = "N";
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbO_Sub_Company1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 온라인몰을 선택하세요!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "P";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx1.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_sub_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_sub_company"].Value = cmbO_Sub_Company1.EditValue;
                            cmd.Parameters["i_o_sub_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_code"].Value = txtp_code.EditValue;
                            cmd.Parameters["i_p_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark1.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                           cmd.Parameters.Add(new MySqlParameter("i_p_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_name"].Value = txtp_name1.EditValue;
                            cmd.Parameters["i_p_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_option_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_option_name"].Value = txtoption_name1.EditValue;
                            cmd.Parameters["i_option_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_event_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_event_name"].Value = txtevent_name1.EditValue;
                            cmd.Parameters["i_event_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_add_p_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_add_p_name"].Value = txtadd_p_name1.EditValue;
                            cmd.Parameters["i_add_p_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_bunch_delivery", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_bunch_delivery"].Value = rbp_bunch_delivery1.EditValue;
                            cmd.Parameters["i_p_bunch_delivery"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_event_end_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_event_end_date"].Value = dtevent_end_date.EditValue;
                            cmd.Parameters["i_event_end_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
            }
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdx1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 삭제할 온라인몰의 제품을 선택하세요!");
                return;
            }



            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "D";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx1.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_sub_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_sub_company"].Value = cmbO_Sub_Company1.EditValue;
                            cmd.Parameters["i_o_sub_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_code"].Value = txtp_code.EditValue;
                            cmd.Parameters["i_p_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark1.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_name"].Value = txtp_name1.EditValue;
                            cmd.Parameters["i_p_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_option_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_option_name"].Value = txtoption_name1.EditValue;
                            cmd.Parameters["i_option_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_event_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_event_name"].Value = txtevent_name1.EditValue;
                            cmd.Parameters["i_event_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_add_p_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_add_p_name"].Value = txtadd_p_name1.EditValue;
                            cmd.Parameters["i_add_p_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_bunch_delivery", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_bunch_delivery"].Value = rbp_bunch_delivery1.EditValue;
                            cmd.Parameters["i_p_bunch_delivery"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_event_end_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_event_end_date"].Value = dtevent_end_date.EditValue;
                            cmd.Parameters["i_event_end_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            
                            cmd.ExecuteNonQuery();


                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
                Eraser.Clear(this, "CLR2");
                cmbO_Sub_Company.EditValue = "0";
                dtevent_end_date.EditValue = DateTime.Now;
            }
        }

        private void efwXtraTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                efwLabel5.Text = "주문일";
                dtS_DATE.EditValue = DateTime.Now;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                efwLabel5.Text = "등록일";
                dtS_DATE.EditValue = "2021-01-01";
            }
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["idx"].ToString() != "")
            {
                this.txtIdx1.EditValue = dr["idx"].ToString();
            }
        }

        private void efwSimpleButton7_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 삭제할 온라인몰의 일자를 선택하세요!");
                return;
            }


            if (txto_delivery_num.EditValue.ToString().Length > 2)
            {
                MessageAgent.MessageShow(MessageType.Warning, " 배송중인 품목이므로 삭제할수 없습니다!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN24_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "D";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_o_date"].Value = dtO_Date.EditValue;
                            cmd.Parameters["i_o_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_company"].Value = cmbO_Company.EditValue;
                            cmd.Parameters["i_o_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_sub_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_sub_company"].Value = cmbO_Sub_Company.EditValue;
                            cmd.Parameters["i_o_sub_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_qty", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_qty"].Value = Convert.ToInt32(txtO_Qty.EditValue);
                            cmd.Parameters["i_o_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_amt", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_amt"].Value = Convert.ToInt32(txtO_Amt.EditValue);
                            cmd.Parameters["i_o_amt"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_name"].Value = txtp_name.EditValue;
                            cmd.Parameters["i_p_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_option_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_option_name"].Value = txtoption_name.EditValue;
                            cmd.Parameters["i_option_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_add_p_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_add_p_name"].Value = txtadd_p_name.EditValue;
                            cmd.Parameters["i_add_p_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_bunch_delivery", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_bunch_delivery"].Value = rbp_bunch_delivery.EditValue;
                            cmd.Parameters["i_p_bunch_delivery"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_name"].Value = txto_receive_name.EditValue;
                            cmd.Parameters["i_o_receive_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_contact", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_contact"].Value = txto_receive_contact.EditValue;
                            cmd.Parameters["i_o_receive_contact"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_name1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_name1"].Value = txto_receive_name1.EditValue;
                            cmd.Parameters["i_o_receive_name1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_contact1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_contact1"].Value = txto_receive_contact.EditValue;
                            cmd.Parameters["i_o_receive_contact1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_zipcode1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_zipcode1"].Value = txto_receive_zipcode1.EditValue;
                            cmd.Parameters["i_o_receive_zipcode1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_address1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_address1"].Value = txto_receive_address1.EditValue;
                            cmd.Parameters["i_o_receive_address1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_o_receive_message1", MySqlDbType.VarChar));
                            cmd.Parameters["i_o_receive_message1"].Value = txto_receive_message1.EditValue;
                            cmd.Parameters["i_o_receive_message1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_event_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_event_name"].Value = txtevent_name.EditValue;
                            cmd.Parameters["i_event_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
                from_new();
            }

        }
    }
}
