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
            
            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl2.BindControlSet(
                      new ColumnControlSet("idx", txtIDX_ADDR)
                    , new ColumnControlSet("post_no", btnPOST_NO)
                    , new ColumnControlSet("addr1", txtADDRESS1)
                    , new ColumnControlSet("addr2", txtADDRESS2)
                   );
            this.efwGridControl2.Click += efwGridControl2_Click;


            this.efwGridControl3.BindControlSet(
                      new ColumnControlSet("id", txtP_Id)
                    , new ColumnControlSet("p_name", txtP_Name)
                    , new ColumnControlSet("option_code", txtOptionCode)
               //     , new ColumnControlSet("option_name", txtU_ADDR)
                   );

            SetCmb();
        }

        private void SetCmb()
        {
            // 공급자구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(s_idx,'') as DCODE ,s_company_name as DNAME  FROM domaadmin.tb_sellers_info where s_status = 'Y' order by s_company_name ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbSellers, codeArray);
            }
            cmbSellers.EditValue = "1";
        }


        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            txtU_ZIP.EditValue = "";
            if (dr != null && dr["post_no"].ToString() != "")
            {
                this.btnPOST_NO.EditValue = dr["post_no"].ToString();
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

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtProdNameQ.EditValue;

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


        private void btnGShopQ_Click(object sender, EventArgs e)
        {
            Open1();
        }

        private void btnEventProd_Click(object sender, EventArgs e)
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

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtProdNameQ.EditValue;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGShop_Id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " G 멀티샵 매장을 선택하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtP_Id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 사은품을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32( txtIdx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_order_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_order_date"].Value = dtOrderDate.EditValue;
                            cmd.Parameters["i_order_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_Id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_Id"].Value = Convert.ToInt32(txtGShop_Id.EditValue);
                            cmd.Parameters["i_gshop_Id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sellers", MySqlDbType.VarChar));
                            cmd.Parameters["i_sellers"].Value = cmbSellers.EditValue;
                            cmd.Parameters["i_sellers"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_id", MySqlDbType.Int32));
                            cmd.Parameters["i_p_id"].Value = Convert.ToInt32(txtP_Id.EditValue);
                            cmd.Parameters["i_p_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_option_id", MySqlDbType.Int32));
                            cmd.Parameters["i_option_id"].Value = Convert.ToInt32(txtOptionCode.EditValue);
                            cmd.Parameters["i_option_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_event_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_event_code"].Value = cmbEventCode.EditValue;
                            cmd.Parameters["i_event_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_rank", MySqlDbType.Int32));
                            cmd.Parameters["i_rank"].Value = Convert.ToInt32(txtRank.EditValue);
                            cmd.Parameters["i_rank"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Qty", MySqlDbType.Int32));
                            cmd.Parameters["i_Qty"].Value = Convert.ToInt32(txtQty.EditValue);
                            cmd.Parameters["i_Qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = txtU_ZIP.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_addr1", MySqlDbType.VarChar));
                            cmd.Parameters["i_addr1"].Value = txtU_ADDR.EditValue;
                            cmd.Parameters["i_addr1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_addr2", MySqlDbType.VarChar));
                            cmd.Parameters["i_addr2"].Value = txtU_ADDR_DETAIL.EditValue;
                            cmd.Parameters["i_addr2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtREMARK.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
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
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = btnPOST_NO, ParentAddr1 = txtADDRESS1, ParentAddr2 = txtADDRESS2 };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
            txtADDRESS2.Focus();
        }

        private void btnSaveAddress_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtU_ID_ADDR.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "ID를 선택하세요");
                return;
            }
            if (string.IsNullOrEmpty(this.btnPOST_NO.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "우편번호를 선택하세요");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP10_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIDX_ADDR.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID_ADDR.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = btnPOST_NO.EditValue;
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
            }
        }


    }
}
