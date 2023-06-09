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
using YL_GSHOP.BizFrm.Dlg;

namespace YL_GSHOP.BizFrm
{
    
    public partial class frmGSHOP10 : FrmBase
    {
        frmMemberInfo popup;
        public frmGSHOP10()
        {
            InitializeComponent();

            this.QCode = "GSHOP10";
            //폼명설정
            this.FrmName = "이벤트 발주등록";

        }

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
            this.IsExcel = true;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            rbP_SHOW_TYPE.EditValue = "1";


            gridView2.OptionsView.ShowFooter = true;

            this.efwGridControl2.BindControlSet(
                     new ColumnControlSet("post_no", txtU_ZIP)
                    , new ColumnControlSet("addr1", txtADDRESS1)
                    , new ColumnControlSet("addr2", txtADDRESS2)
                    , new ColumnControlSet("ceo_name", txtU_NAME)
                    , new ColumnControlSet("u_nickname", txtU_NICKNAME)
                    , new ColumnControlSet("login_id", txtUSER_ID)
                    , new ColumnControlSet("u_id", txtU_ID_ADDR)

                    , new ColumnControlSet("post_no", txtPost_No)
                    , new ColumnControlSet("addr1", txtAddr1)
                    , new ColumnControlSet("addr2", txtAddr2)
                    , new ColumnControlSet("idx", txtIdx)
                    , new ColumnControlSet("gshop_name", txtGShop_Name)
                    , new ColumnControlSet("order_date", dtOrderDate)
                    , new ColumnControlSet("delivery_cd", cmbDelivery_Company)
                    , new ColumnControlSet("delivery_no", txtDelivery_No)
                    , new ColumnControlSet("remark", txtRemark)
                   );
            this.efwGridControl2.Click += efwGridControl2_Click;

            this.efwGridControl3.BindControlSet(
                     new ColumnControlSet("post_no", txtU_ZIP)
                    , new ColumnControlSet("addr1", txtADDRESS1)
                    , new ColumnControlSet("addr2", txtADDRESS2)
                    , new ColumnControlSet("idx", txtIDX_ADDR)
                    , new ColumnControlSet("post_no", txtPost_No)
                    , new ColumnControlSet("addr1", txtAddr1)
                    , new ColumnControlSet("addr2", txtAddr2)

                   );
            this.efwGridControl3.Click += efwGridControl3_Click;



            SetCmb();
        }

        private void SetCmb()
        {
            // 공급자구분

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE ,code_nm  as DNAME  FROM  domaadmin.tb_common_code  where gcode_id = '00010' and use_yn = 'Y' order by code_id ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbEvent, codeArray);
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT d_code as DCODE ,d_name as DNAME  FROM domamall.tb_am_product_delivers  order by sort ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbDelivery_Company, codeArray);
                cmbDelivery_Company.EditValue = "08";
            }

        }


        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            txtU_ZIP.EditValue = "";
            txtIDX_ADDR.EditValue = "";
            if (dr != null && dr["post_no"].ToString() != "")
            {
                this.txtU_ZIP.EditValue = dr["post_no"].ToString();
            }

            //cmbDelivery_Company.EditValue = dr["delivery_cd"].ToString();

            if (dr != null && dr["delivery_cd"].ToString() == "")
            {
                cmbDelivery_Company.EditValue = "08";
            }
            Open1();
        }

        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl3.GetSelectedRow(0);
            txtU_ZIP.EditValue = "";
            txtPost_No.EditValue = "";
            if (dr != null && dr["post_no"].ToString() != "")
            {
                this.txtU_ZIP.EditValue = dr["post_no"].ToString();
            }

            if (dr != null && dr["post_no"].ToString() != "")
            {
                this.txtPost_No.EditValue = dr["post_no"].ToString();
            }

        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_event", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbEvent.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtProdNameQ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            // this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtU_ID_ADDR.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();

                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            // this.efwGridControl2.MyGridView.BestFitColumns();

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
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtProdNameQ.EditValue;

                        cmd.Parameters.Add("i_date_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = rbP_SHOW_TYPE.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            // this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        //private void btnEventProd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string sCOMFIRM = string.Empty;
        //        //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
        //        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

        //        {
        //            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SELECT_03", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
        //                cmd.Parameters[0].Value = txtProdNameQ.EditValue;

        //                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
        //                {
        //                    DataTable ds = new DataTable();
        //                    sda.Fill(ds);
        //                    efwGridControl3.DataBind(ds);
        //                    // this.efwGridControl2.MyGridView.BestFitColumns();

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
        //    }
        //}


        public override void Save()
        {
            try
            {

                var saveResult = new SaveTableResultInfo() { IsError = true };

                var dt = efwGridControl1.GetChangeDataWithRowState;
                var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                string sBunch = string.Empty;
                string sChkDate = string.Empty;
                DateTime sOrder_Date;
                DateTime sO_Date;

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][StatusColumn].ToString() == "U")
                    {
                        
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SAVE_01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;


                                sChkDate = dt.Rows[i]["order_date"].ToString();
                                if (sChkDate == "")
                                    sOrder_Date = Convert.ToDateTime(new System.DateTime(2018, 12, 31, 12, 01, 01, 0));
                                else
                                    sOrder_Date = Convert.ToDateTime(dt.Rows[i]["order_date"].ToString());

                                cmd.Parameters.Add("i_order_date", MySqlDbType.DateTime);
                                cmd.Parameters[0].Value = sOrder_Date;

                                cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                                cmd.Parameters[1].Value = dt.Rows[i]["u_id"].ToString();

                                sChkDate = dt.Rows[i]["o_date"].ToString();
                                if (sChkDate == "")
                                    sO_Date = Convert.ToDateTime(new System.DateTime(2018, 12, 31, 12, 01, 01, 0));
                                else
                                    sO_Date = Convert.ToDateTime(dt.Rows[i]["o_date"].ToString());

                                cmd.Parameters.Add("i_o_date", MySqlDbType.DateTime);
                                cmd.Parameters[2].Value = sO_Date;


                                cmd.Parameters.Add("i_gshop_id", MySqlDbType.Int32, 10);
                                cmd.Parameters[3].Value = Convert.ToInt32(dt.Rows[i]["gshop_id"]).ToString();

                                cmd.Parameters.Add("i_common_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[4].Value = dt.Rows[i]["common_code"].ToString();

                                cmd.Parameters.Add("i_rank", MySqlDbType.Int32, 10);
                                cmd.Parameters[5].Value = Convert.ToInt32(dt.Rows[i]["rank"]).ToString();

                                cmd.Parameters.Add("i_event_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[6].Value = dt.Rows[i]["event_code"].ToString();

                                cmd.Parameters.Add("i_prod_qty", MySqlDbType.Int32, 10);
                                cmd.Parameters[7].Value = Convert.ToInt32(dt.Rows[i]["prod_qty"]).ToString();

                                cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                                cmd.Parameters[8].Value = dt.Rows[i]["o_code"].ToString();

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


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "수정할 발주내역을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SAVE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_work_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_type"].Value = "A";
                            cmd.Parameters["i_work_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = txtU_ZIP.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_addr1", MySqlDbType.VarChar));
                            cmd.Parameters["i_addr1"].Value = txtAddr1.EditValue;
                            cmd.Parameters["i_addr1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_addr2", MySqlDbType.VarChar));
                            cmd.Parameters["i_addr2"].Value = txtAddr2.EditValue;
                            cmd.Parameters["i_addr2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_delivery_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_delivery_company"].Value = cmbDelivery_Company.EditValue;
                            cmd.Parameters["i_delivery_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_delivery_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_delivery_no"].Value = txtDelivery_No.EditValue;
                            cmd.Parameters["i_delivery_no"].Direction = ParameterDirection.Input;



                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open2();
            }
        }

        private void btnMemberSch_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
            };
            popup.FormClosed += popup_FormClosed1;
            PopUpBizAgent.Show(this, popup);
            Open1();

        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtU_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtU_NAME.EditValue = popup.U_NAME;
                this.txtU_ID_ADDR.EditValue = popup.U_ID;
                this.txtUSER_ID.EditValue = popup.USER_ID;
            }
            popup = null;
        }

        private void BtnPOST_NO_Click(object sender, EventArgs e)
        {
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = txtU_ZIP, ParentAddr1 = txtADDRESS1, ParentAddr2 = txtADDRESS2 };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
            txtADDRESS2.Focus();
        }
        private void txtPost_No_Click(object sender, EventArgs e)
        {
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = txtPost_No, ParentAddr1 = txtAddr1, ParentAddr2 = txtAddr2 };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
            txtAddr2.Focus();
        }


        private void btnSaveAddress_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtU_ID_ADDR.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "ID를 선택하세요");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try

                {
                    string sIDX_ADDR = string.Empty;
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;


                            if (txtIDX_ADDR.EditValue.ToString() == "")
                                sIDX_ADDR = "0";
                            else
                                sIDX_ADDR = txtIDX_ADDR.EditValue.ToString();

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(sIDX_ADDR);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID_ADDR.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = txtU_ZIP.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_address1", MySqlDbType.VarChar));
                            cmd.Parameters["i_address1"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_address1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_address2", MySqlDbType.VarChar));
                            cmd.Parameters["i_address2"].Value = txtADDRESS2.EditValue;
                            cmd.Parameters["i_address2"].Direction = ParameterDirection.Input;


                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open1();
            }
        }

        private void btnOrder_Query_Click(object sender, EventArgs e)
        {
            Open2();
        }

        private void btnAddress_Click(object sender, EventArgs e)
        {
            Open1();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "수정할 발주내역을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SAVE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_work_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_type"].Value = "D";
                            cmd.Parameters["i_work_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = txtU_ZIP.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_addr1", MySqlDbType.VarChar));
                            cmd.Parameters["i_addr1"].Value = txtAddr1.EditValue;
                            cmd.Parameters["i_addr1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_addr2", MySqlDbType.VarChar));
                            cmd.Parameters["i_addr2"].Value = txtAddr2.EditValue;
                            cmd.Parameters["i_addr2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_delivery_company", MySqlDbType.VarChar));
                            cmd.Parameters["i_delivery_company"].Value = cmbDelivery_Company.EditValue;
                            cmd.Parameters["i_delivery_company"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_delivery_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_delivery_no"].Value = txtDelivery_No.EditValue;
                            cmd.Parameters["i_delivery_no"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open2();
            }
        }

        private void txtProdNameQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void efwTextEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Open2();
        }

        private void cmbEvent_EditValueChanged(object sender, EventArgs e)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "SELECT code_val1 as u_name FROM domaadmin.tb_common_code " +
                             "WHERE gcode_id = '00010' and code_id = " + cmbEvent.EditValue;
                DataSet ds = sql.selectQueryDataSet();

                txtIssue.EditValue = sql.selectQueryForSingleValue();
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
                gridView1.SetRowCellValue(i, gridView1.Columns["order_date"], DateTime.Now);
        }
    }
}
