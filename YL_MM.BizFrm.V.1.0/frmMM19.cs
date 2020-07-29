using DevExpress.CodeParser;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_MM.BizFrm
{
    public partial class frmMM19 : FrmBase
    {
        frmMM19_Pop01 popup;
        frmMM19_Pop02 popup1;
        frmMM19_Pop03 popup3;

        public frmMM19()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MM19";
            //폼명설정
            this.FrmName = "고객 상당 등록";

        }

        private void frmMM16_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            this.IsMenuVw = false;
            //DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 12);
            ckPic_Send.EditValue = "N";
            rbAdvice_Type.EditValue = "01";
            rbType.EditValue = "00";
            //this.LabName.Appearance.Options.UseFont = true;
            //this.LabName.Appearance.Options.UseForeColor = true;
            //this.LabName.Appearance.Options.UseTextOptions = true;
            //this.LabName.Size = new System.Drawing.Size(200, 40);

            
            dtE_DATE.EditValue = DateTime.Now;

            this.efwLabel5.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel4.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel13.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel6.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel11.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel9.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);

            this.efwLabel10.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel8.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel12.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel14.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel15.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);
            this.efwLabel9.Appearance.Font = new System.Drawing.Font("맑은고딕", 11);


            gridView3.CustomUnboundColumnData += gridView3_CustomUnboundColumnData;
            txtMember_chk.EditValue = "0";

            // 문자 클릭
            gridView1.OptionsView.ShowFooter = true;
            this.efwGridControl1.BindControlSet(
                new ColumnControlSet("code_memo", txtMessage)
              );

            this.efwGridControl1.Click += efwGridControl1_Click;



            // 햔황에서 클릭시
            gridView2.OptionsView.ShowFooter = true;
            this.efwGridControl2.BindControlSet(
                new ColumnControlSet("content", txtAdviceQuery)
               , new ColumnControlSet("content", txtContent)
               , new ColumnControlSet("name", txtName)
               , new ColumnControlSet("hp_no", txtHp_No)
               , new ColumnControlSet("shop_name", txtShop_Name)
               , new ColumnControlSet("post_no", btnPost_No)
               , new ColumnControlSet("addr", txtAddr)
               , new ColumnControlSet("addr_detail", txtAddr_Detail)
               , new ColumnControlSet("advice_type", rbAdvice_Type)
               , new ColumnControlSet("Last_coid_name", LabLast_Name.Text)
               , new ColumnControlSet("md_name", txtMd_Name)
               , new ColumnControlSet("pic_send", ckPic_Send)
               , new ColumnControlSet("u_id", txtU_Id)
               , new ColumnControlSet("advice_date", dtAdvice_Date)
               , new ColumnControlSet("idx", txtIdx)
              );

            this.efwGridControl2.Click += efwGridControl2_Click;

            // 문자 클릭
            gridView4.OptionsView.ShowFooter = true;
            this.efwGridControl4.BindControlSet(
                new ColumnControlSet("code_memo", txtCode_Memo)
              );

            this.efwGridControl4.Click += efwGridControl4_Click;

            // 최근상담내용
            gridView5.OptionsView.ShowFooter = true;
            this.efwGridControl5.BindControlSet(
                new ColumnControlSet("content", txtAdvice_Content)
              );

            this.efwGridControl5.Click += efwGridControl5_Click;


            LabName.Text = UserInfo.instance().Name;
            OpenProd();
            OpenMessage();
            Product1();
            from_new();
            Advice_Content();
        }

        void gridView3_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = dr["ImageURL"].ToString();
                if (iconsCache.ContainsKey(url))
                {
                    e.Value = iconsCache[url];
                    return;
                }
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        e.Value = Bitmap.FromStream(stream);
                        iconsCache.Add(url, (Bitmap)e.Value);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["code_memo"].ToString() != "")
            {
                this.txtMessage.EditValue = dr["code_memo"].ToString();
            }
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["post_no"].ToString() != "")
            {
                this.btnPost_No.EditValue2 = dr["post_no"].ToString();
                this.btnPost_No.Text = dr["post_no"].ToString();
                this.txtIdx.Text = dr["idx"].ToString();
            }
        }
        private void efwGridControl4_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl4.GetSelectedRow(0);

        }
        private void efwGridControl5_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl5.GetSelectedRow(0);

        }
        public void OpenProd()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_ProdName", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            //  this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public void OpenMessage()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_ProdName", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //  this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();
        //  그리드 상품구매 클릭 
 

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }

        private void btnPost_No_Click(object sender, EventArgs e)
        {
            Dlg.frmZipNoInfo FrmInfo = new Dlg.frmZipNoInfo() { ParentBtn = btnPost_No, ParentAddr1 = txtAddr, ParentAddr2 = txtAddr_Detail };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
            txtAddr_Detail.Focus();
        }

        private void from_new()
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
            rbAdvice_Type.EditValue = "01";
            txtHp_No.EditValue = "010-";
            txtMember_chk.EditValue = "0";


            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SELECT_06", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("i_CoidName", MySqlDbType.VarChar));
                        cmd.Parameters["i_CoidName"].Value = UserInfo.instance().Name;
                        cmd.Parameters["i_CoidName"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_tot", MySqlDbType.Int32));
                        cmd.Parameters["o_tot"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_coid_tot", MySqlDbType.Int32));
                        cmd.Parameters["o_coid_tot"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_order_tot", MySqlDbType.Int32));
                        cmd.Parameters["o_order_tot"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_order_coid", MySqlDbType.Int32));
                        cmd.Parameters["o_order_coid"].Direction = ParameterDirection.Output;
                        //

                        cmd.ExecuteNonQuery();

                        lbTot.Text = cmd.Parameters["o_tot"].Value.ToString();
                        lbCoid_tot.Text = cmd.Parameters["o_coid_tot"].Value.ToString();
                        lbOrder_Tot.Text = cmd.Parameters["o_order_tot"].Value.ToString();
                        lbOrder_Coid.Text = cmd.Parameters["o_order_coid"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }
        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {

            from_new();
        }
        // 문자전송
        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtHp_No.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 수신자 전화번호를 확인하세요 !");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, " 문자 전송을 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_phone", MySqlDbType.VarChar));
                            cmd.Parameters["i_phone"].Value = txtHp_No.EditValue;
                            cmd.Parameters["i_phone"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_callback", MySqlDbType.VarChar));
                            cmd.Parameters["i_callback"].Value = "1644-5646";
                            cmd.Parameters["i_callback"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_msg", MySqlDbType.VarChar));
                            cmd.Parameters["i_msg"].Value = txtMessage.EditValue;
                            cmd.Parameters["i_msg"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Informational, "문자 전송이 완료되었습니다.");
                }

            }
        }
        // 단일 ROW 일시 Query
        public void OneRowSelect(string sFildName)
        {
            try
            {
                string sQuery = string.Empty;

                if (sFildName == "1")
                {
                    sQuery = txtName.EditValue.ToString();
                }
                else if (sFildName == "2")
                {
                    sQuery = txtHp_No.EditValue.ToString();
                }
                else if (sFildName == "3")
                {
                    sQuery = txtMd_Name.EditValue.ToString();
                }
                else if (sFildName == "4")
                {
                    sQuery = txtShop_Name.EditValue.ToString();
                }



                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SELECT_03", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("i_FildName", MySqlDbType.VarChar));
                        cmd.Parameters["i_FildName"].Value = sFildName;
                        cmd.Parameters["i_FildName"].Direction = ParameterDirection.Input;


                        cmd.Parameters.Add(new MySqlParameter("i_Query", MySqlDbType.VarChar));
                        cmd.Parameters["i_Query"].Value = sQuery;
                        cmd.Parameters["i_Query"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.VarChar));
                        cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_hp_no", MySqlDbType.VarChar));
                        cmd.Parameters["o_hp_no"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_shop_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_shop_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_post_no", MySqlDbType.VarChar));
                        cmd.Parameters["o_post_no"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_addr", MySqlDbType.VarChar));
                        cmd.Parameters["o_addr"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_addr_detail", MySqlDbType.VarChar));
                        cmd.Parameters["o_addr_detail"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_advice_type", MySqlDbType.VarChar));
                        cmd.Parameters["o_advice_type"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_coid_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_coid_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_md_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_md_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_pic_send", MySqlDbType.VarChar));
                        cmd.Parameters["o_pic_send"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_Last_coid_name", MySqlDbType.VarChar));
                        cmd.Parameters["o_Last_coid_name"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_u_id", MySqlDbType.VarChar));
                        cmd.Parameters["o_u_id"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(new MySqlParameter("o_tot_cnt", MySqlDbType.VarChar));
                        cmd.Parameters["o_tot_cnt"].Direction = ParameterDirection.Output;
                        //

                        cmd.ExecuteNonQuery();

                        //txtIdx.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                        txtName.EditValue = cmd.Parameters["o_name"].Value.ToString();
                        txtHp_No.EditValue = cmd.Parameters["o_hp_no"].Value.ToString();
                        txtShop_Name.EditValue = cmd.Parameters["o_shop_name"].Value.ToString();
                        btnPost_No.EditValue = cmd.Parameters["o_post_no"].Value.ToString();
                        txtAddr.EditValue = cmd.Parameters["o_addr"].Value.ToString();
                        txtAddr_Detail.EditValue = cmd.Parameters["o_addr_detail"].Value.ToString();
                        rbAdvice_Type.EditValue = cmd.Parameters["o_advice_type"].Value.ToString();
                        txtMd_Name.EditValue = cmd.Parameters["o_coid_name"].Value.ToString();
                        txtMd_Name.EditValue = cmd.Parameters["o_md_name"].Value.ToString();
                        ckPic_Send.EditValue = cmd.Parameters["o_pic_send"].Value.ToString();
                        LabLast_Name.Text = cmd.Parameters["o_Last_coid_name"].Value.ToString();
                        txtU_Id.EditValue = cmd.Parameters["o_u_id"].Value.ToString();
                        LaTot_Cnt.Text = cmd.Parameters["o_tot_cnt"].Value.ToString();
                        txtU_Id.EditValue = cmd.Parameters["o_u_id"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        // 성명으로 
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            // 이름으로 검색

            if (e.KeyCode == Keys.Enter)
            {
                if (txtMember_chk.EditValue == "0")
                {
                    txtU_Id.EditValue = "";
                    int nCount;
                    using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                    {
                        sql.Query = "select count(*) as nCount FROM  domabiz.cust_advice where name  like concat('%','" + txtName.EditValue + "', '%') ";
                        DataSet ds = sql.selectQueryDataSet();

                        nCount = Convert.ToInt32(sql.selectQueryForSingleValue());
                    }
                    if (nCount == 1)
                    {

                        OneRowSelect("1");
                    }
                    else if (nCount > 1)
                    {
                        popup1 = new frmMM19_Pop02();

                        popup1.sFildQuery = txtName.EditValue.ToString();

                        popup1.FormClosed += popup_FormClosed1;
                        popup1.ShowDialog();
                    }
                    else
                    {
                        if (txtMember_chk.EditValue == "0")
                        {
                            txtMember_chk.EditValue = "1";
                            MessageAgent.MessageShow(MessageType.Informational, "신규 상담 고객 입니다.");
                        }
                    }
                }

                if (txtShop_Name.Text.Length == 0)
                    txtShop_Name.Focus();
            }
        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup1.FormClosed -= popup_FormClosed1;
            if (popup1.DialogResult == DialogResult.OK)
            {
                //this.txtIdx.EditValue = popup1.nIdx;
                this.txtName.EditValue = popup1.sName;
                this.txtHp_No.EditValue = popup1.sHp_No;
                this.txtShop_Name.EditValue = popup1.sShop_Name;
                this.ckPic_Send.EditValue = popup1.sPic_Send;
                this.rbAdvice_Type.EditValue = popup1.sAdvice_Type;
                this.txtU_Id.EditValue = popup1.sU_id;
                this.btnPost_No.EditValue = popup1.sPost_No;
                this.txtAddr.EditValue = popup1.sAddr;
                this.txtAddr_Detail.EditValue = popup1.sAddr_Detail;
                this.LabLast_Name.Text = popup1.sLast_Coid_Name;
                this.LaTot_Cnt.Text = popup1.sTot_Cnt;
            }
            popup1 = null;
        }
        // 핸드폰번호
        private void txtHp_No_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtMember_chk.EditValue == "0")
                {
                    txtU_Id.EditValue = "";
                    int nCount;
                    using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                    {
                        sql.Query = "select count(*) as nCount FROM  domabiz.cust_advice where hp_no like concat('%','" + txtHp_No.EditValue + "', '%') ";
                        DataSet ds = sql.selectQueryDataSet();

                        nCount = Convert.ToInt32(sql.selectQueryForSingleValue());
                    }
                    if (nCount == 1)
                    {

                        OneRowSelect("2");
                    }
                    else if (nCount > 1)
                    {
                        popup1 = new frmMM19_Pop02();

                        popup1.sFildQuery = txtHp_No.EditValue.ToString();

                        popup1.FormClosed += popup_FormClosed1;
                        popup1.ShowDialog();
                    }
                    else
                    {
                        if (txtMember_chk.EditValue == "0")
                        {
                            txtMember_chk.EditValue = "1";
                            MessageAgent.MessageShow(MessageType.Informational, "신규 상담 고객 입니다.");
                        }
                    }
                }

                if (txtName.Text.Length == 0)
                    txtName.Focus();

            }
        }
        // MD 명
        private void txtMd_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int nCount;
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    sql.Query = "select count(*) as nCount FROM  domabiz.cust_advice where md_name like concat('%','" + txtMd_Name.EditValue + "', '%') ";
                    DataSet ds = sql.selectQueryDataSet();

                    nCount = Convert.ToInt32(sql.selectQueryForSingleValue());
                }
                if (nCount == 1)
                {

                    OneRowSelect("3");
                }
                else if (nCount > 1)
                {
                    popup1 = new frmMM19_Pop02();

                    popup1.sFildQuery = txtMd_Name.EditValue.ToString();

                    popup1.FormClosed += popup_FormClosed1;
                    popup1.ShowDialog();
                }
                else
                {
                    if (txtMember_chk.EditValue == "0")
                    {
                        txtMember_chk.EditValue = "1";
                        MessageAgent.MessageShow(MessageType.Informational, "신규 상담 고객 입니다.");
                    }
                }
            }
        }
        // 샵명
        private void txtShop_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMember_chk.EditValue = "1";
                if (txtMd_Name.Text.Length == 0)
                    txtMd_Name.Focus();
            }
        }
        // 제품 성명, 대응방안

        private void txtAddr_Detail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtContent.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtContent.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 상담 내역을 입력하세요 ");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_name"].Value = txtName.EditValue;
                            cmd.Parameters["i_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hp_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_hp_no"].Value = txtHp_No.EditValue;
                            cmd.Parameters["i_hp_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_shop_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_shop_name"].Value = txtShop_Name.EditValue;
                            cmd.Parameters["i_shop_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pic_send", MySqlDbType.VarChar));
                            cmd.Parameters["i_pic_send"].Value = ckPic_Send.EditValue;
                            cmd.Parameters["i_pic_send"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_advice_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_advice_type"].Value = rbAdvice_Type.EditValue;
                            cmd.Parameters["i_advice_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = btnPost_No.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_addr"].Value = txtAddr.EditValue;
                            cmd.Parameters["i_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_addr_detail", MySqlDbType.VarChar));
                            cmd.Parameters["i_addr_detail"].Value = txtAddr_Detail.EditValue;
                            cmd.Parameters["i_addr_detail"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_content", MySqlDbType.VarChar));
                            cmd.Parameters["i_content"].Value = txtContent.EditValue;
                            cmd.Parameters["i_content"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_Id.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_coid_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_coid_name"].Value = LabName.Text;
                            cmd.Parameters["i_coid_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_name"].Value = txtMd_Name.EditValue;
                            cmd.Parameters["i_md_name"].Direction = ParameterDirection.Input;

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
                Advice_Content();
            }
        }

        private void Type_Query()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;

                        cmd.Parameters.Add("i_Type", MySqlDbType.VarChar, 2);
                        cmd.Parameters[3].Value = rbType.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwTextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Type_Query();
            }
        }

        private void btnType_Query_Click(object sender, EventArgs e)
        {
            Type_Query();
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAddr.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 배종지 주소를 입력하세요 !");
                return;
            }

            string sNickName = string.Empty ;

            if (txtName.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "주문 고객을 선택하세요");
                return;
            }


            if (txtU_Id.EditValue == "" ^ txtU_Id.EditValue == null)
            {
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    sql.Query = "select ifnull(u_nickname,'0') as u_nickname FROM  domalife.member_master where u_name = '" + txtName.EditValue + "' and  u_cell_num = '" + txtHp_No.EditValue + "' and advice_seq = 0 limit 1  ";
                    DataSet ds = sql.selectQueryDataSet();
                    sNickName = sql.selectQueryForSingleValue();
                }
                if ( sNickName.Length > 2 )
                {
                    MessageAgent.MessageShow(MessageType.Informational, " 도마 라이프 가입 이력이 존재합니다. \r\n 도넛라이프 앱에서 주문 하세요 \r\n -->  " + sNickName);
                    return;
                }
                else if (sNickName.Length == 0)
                {
                    New_Member();
                }
            }


            if (txtU_Id.Text.Length > 10 ) 
            {
                popup = new frmMM19_Pop01();

                popup.p_Id = Convert.ToInt32(gridView3.GetFocusedRowCellValue("id").ToString());
                popup.u_Id = txtU_Id.Text;
                // popup.u_Id = "d1c907d4562fbce2144db86ae43ef7f6";
                popup.FormClosed += popup_FormClosed;
                popup.ShowDialog();

            }

        }

        private void New_Member()
        {
            if (string.IsNullOrEmpty(this.txtU_Id.Text))
            {
                if (txtName.EditValue == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 성명을 입력하세요!");
                    return;
                }

                if (txtHp_No.EditValue == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 전화번호를 입력하세요!");
                    return;
                }

                if (MessageAgent.MessageShow(MessageType.Confirm, "도넛 라이프 GUEST 계정을 생성 하시겠습니까?") == DialogResult.OK)
                {
                    using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                    {
                        sql.Query = "select concat('ADVICE', cast( (max(advice_seq) + 1) as char)) as advice_seq FROM  domalife.member_master  ";
                        DataSet ds = sql.selectQueryDataSet();

                        txtLogin_Id.EditValue = sql.selectQueryForSingleValue();
                    }

                    try
                    {
                        //DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_PM_PM01_SAVE_01"
                        int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MM_MM19_SAVE_03"
                                                                   , this.txtName.EditValue
                                                                   , this.txtHp_No.EditValue
                                                                   , this.btnPost_No.EditValue
                                                                   , this.txtAddr.EditValue
                                                                   , this.txtAddr_Detail.EditValue
                                                                   , this.txtLogin_Id.EditValue
                                                                  );


                        if (retVal > 0)
                        {
                            MessageAgent.MessageShow(MessageType.Informational, "GUEST 계성이 생성되었습니다.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    }

                    using (MySqlConnection con = new MySqlConnection(ConstantLib.Member_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("DB_MEMBER.USP_MM_MM16_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_id"].Value = txtLogin_Id.EditValue;
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pwd", MySqlDbType.VarChar));
                            cmd.Parameters["i_pwd"].Value = "0000";
                            cmd.Parameters["i_pwd"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_name"].Value = txtName.EditValue;
                            cmd.Parameters["i_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_nickname", MySqlDbType.VarChar));
                            cmd.Parameters["i_nickname"].Value = txtLogin_Id.EditValue;
                            cmd.Parameters["i_nickname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_birth", MySqlDbType.VarChar));
                            cmd.Parameters["i_birth"].Value = "";
                            cmd.Parameters["i_birth"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hpno", MySqlDbType.VarChar));
                            cmd.Parameters["i_hpno"].Value = txtHp_No.EditValue;
                            cmd.Parameters["i_hpno"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gender", MySqlDbType.VarChar));
                            cmd.Parameters["i_gender"].Value = "M";
                            cmd.Parameters["i_gender"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["o_u_id"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            txtU_Id.EditValue = cmd.Parameters["o_u_id"].Value.ToString();

                            string sU_ID = string.Empty;
                            sU_ID = txtU_Id.EditValue.ToString();

                            //if (sU_ID == "N")
                            //    MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                            //else
                            db_member();
                        }
                    }
                }
            }

        }

        private void db_member()
        {
            try
            {
                using (MySqlConnection con2 = new MySqlConnection(ConstantLib.BasicConn_Real))
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.Member_Real))
                {
                    using (MySqlCommand cmd2 = new MySqlCommand("domalife.USP_MM_MM19_SAVE_04", con2))
                    {
                        con2.Open();
                        cmd2.CommandType = CommandType.StoredProcedure;

                        cmd2.Parameters.Add(new MySqlParameter("i_o_u_id", MySqlDbType.VarChar));
                        cmd2.Parameters["i_o_u_id"].Value = txtU_Id.EditValue;
                        cmd2.Parameters["i_o_u_id"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.VarChar));
                        cmd2.Parameters["i_id"].Value = txtLogin_Id.EditValue;
                        cmd2.Parameters["i_id"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_pwd", MySqlDbType.VarChar));
                        cmd2.Parameters["i_pwd"].Value = "0000";
                        cmd2.Parameters["i_pwd"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                        cmd2.Parameters["i_name"].Value = txtName.EditValue;
                        cmd2.Parameters["i_name"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_nickname", MySqlDbType.VarChar));
                        cmd2.Parameters["i_nickname"].Value = txtLogin_Id.EditValue;
                        cmd2.Parameters["i_nickname"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_birth", MySqlDbType.VarChar));
                        cmd2.Parameters["i_birth"].Value = "";
                        cmd2.Parameters["i_birth"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_hpno", MySqlDbType.VarChar));
                        cmd2.Parameters["i_hpno"].Value = txtHp_No.EditValue;
                        cmd2.Parameters["i_hpno"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_gender", MySqlDbType.VarChar));
                        cmd2.Parameters["i_gender"].Value = "M";
                        cmd2.Parameters["i_gender"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                        cmd2.Parameters["i_post_no"].Value = btnPost_No.EditValue;
                        cmd2.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_addr", MySqlDbType.VarChar));
                        cmd2.Parameters["i_addr"].Value = txtAddr.EditValue;
                        cmd2.Parameters["i_addr"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("i_addr_detail", MySqlDbType.VarChar));
                        cmd2.Parameters["i_addr_detail"].Value = txtAddr_Detail.EditValue;
                        cmd2.Parameters["i_addr_detail"].Direction = ParameterDirection.Input;

                        cmd2.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                        cmd2.Parameters["o_Return"].Direction = ParameterDirection.Output;

                        cmd2.ExecuteNonQuery();

                        //MessageBox.Show(cmd2.Parameters["o_Return"].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public void Product1()
        {
            // 지파워
            if (Convert.ToInt32(txtP_Id.EditValue) == 600)
            {
                txtProduct1.EditValue =
                    " 지파워의 대한 문의 건 " +
                    " \r\n " +
                    " Q.지파워의 공정상태가 궁금합니다. 그리고 물은 깨끗한가요?" +
                    " \r\n " +
                    " A.	HACCP(위해요소분석과 중요관리점 양문약자로써 해썹 또는 식품안전관리인증기준)을 완료한 제품으로써," +
                    " \r\n " +
                    " 청결한 제품입니다." +
                    " \r\n " +
                    " \r\n " +

                    " Q.지파워가 생산되는 공장은 어디인가요?" + " \r\n " +
                    " \r\n " +
                    " A.경기도 광주시에 위치해 있습니다." +
                    " \r\n " +
                    " \r\n " +

                    " Q.지파워의 성분은 어떻게 되나요?" +
                    " \r\n " +
                    " A.1초에 3만번 전극분해하여 활성수소를 만들어내며" +
                    " \r\n " +
                    "몸에 빠르게 흡수되는 미네랄 비타민 혼합음료입니다." +
                                        " \r\n " +
                    " \r\n " +

                    " Q.지파워 보관법이 어떻게 되나요?" +
                    " \r\n " +
                    " A.	직사광선이 없는 건조하고 서늘한곳에 보관하시길 추천드립니다." +
                    " \r\n " +
                    " \r\n " +

                    " Q.지파워를 안심하고 마셔도 되나요?" +
                    " \r\n " +
                    " A.당사는 경기 광주공장에서 생산하는 혼합음료에 대해 식품의약품안전처로부터" +
                    " \r\n " +
                    "HACCP(위해요소분석과 중요관리점 양문약자로써 해썹 또는 식품안전관리인증기준)인증을 완료하였으므로, 안심하고 드셔도 됩니다." +
                    " \r\n " +
                    " \r\n " +

                    " Q.지파워를 끓여서 마시거나 아기들 분유에 타서 먹여도 될까요?" +
                    " \r\n " +
                    " A.안심하고 드셔도 되십니다." +
                    " \r\n " +
                    " \r\n " +

                    " Q.다른 생수보다 가격이 비싼 이유가 있을까요?" +
                    " \r\n " +
                    " A.물분자를 1초당 3만분의 1로 분해하여 활성수소를 만들기 때문에 일반생수와는 차별화가 되어 있기 때문에 가격이 높다고 생각되실 수 있습니다." +
                    " \r\n " +
                    " \r\n " +

                    " Q.기존의 지파워와 차이점은 무엇인가요?" +
                    " \r\n " +
                    " A.미네랄과 비타민의 함량을 높였으며 500ml로 용량이 변경되었습니다.";

            }

            // 지치약
            else if (Convert.ToInt32(txtP_Id.EditValue) == 2529)
            {
                txtProduct1.EditValue =
                    "지파동수 치약의 대한 문의 건" +
                    " \r\n " +
                    "Q.지파동수 치약의 효능, 효과를 알고 싶습니다." +
                    " \r\n " +
                    "A.특허원료를 이용하여 치아와 구간건강을 이롭게 하는 성분을 함유하여" +
                    " \r\n " +
                    "충치 또는 치은염, 구취제거 및 치태제거에 효능이 있습니다." +
                    " \r\n " +
                    " \r\n " +

                    "Q.지파동수 치약의 주요 성분이 궁금합니다." +
                    " \r\n " +
                    "A.지파동수, 아미노카프로선(잇몸질환,치태제거,잇몸출혈을 막음), 알란토인클로르히드록시알루미늄(잇몸질환예방성분), 이산화규소(고결방지제)등이 함유되어 있습니다." +
                    " \r\n " +
                    " \r\n " +

                    "Q.치약 사용방법이 궁금합니다." +
                    " \r\n " +
                    "A. 1일 3~6회 사용을 권장하고 있습니다." +
                    " \r\n " +
                    " \r\n " +

                    "Q.치약을 5회나 사용하면 치아에 안좋은 영향은 주지 않나요?" +
                    " \r\n " +
                    "A.일반치약과 달리 순한성분으로 만들어져 안심하게 사용가능 하도록 만들어진 제품입니다." +
                    " \r\n " +
                    "지파동수 치약은 치은염, 치주염(치조농루)의예방 잇몸질환 예방에좋으며 치태제거(안티프라그)에 도움을줍니다.";

            }

            else
            {
                txtProduct1.EditValue = " 상품 설명을 준비중 입니다 ";
            }
            //

            txtProduct2.EditValue =
                    "[주문/결제]	" +
                    " \r\n " +
                    "Q.[주문/결제] 전화로 상품 주문이 가능한가요?" +
                    " \r\n " +
                    "A.전화주문은 불가하며, 스마트폰(안드로이드)에서 '도넛라이프' 앱을 설치 후 회원가입 하시면 주문이 가능합니다." +
                                        " \r\n " +
                    " \r\n " +

                    "Q.결제방법에는 어떤 것이 있나요?" +
                    " \r\n " +
                    "A.신용카드와 무통장입금(가상계좌)으로 결제가 가능하며," +
                    " \r\n " +
                    "  복합결제늕 불가합니다." +
                    " \r\n " +
                    " \r\n " +

                    "Q.결제 후 주문내역 확인은 어떻게 할 수 있나요?" +
                    " \r\n " +
                    "A.주문내역은 [프라이빗샵]-오른쪽상단[MY]-[주문내역]에서 확인하실 수 있습니다." +
                    " \r\n " +
                    " \r\n " +

                    "Q.현금영수증 신청이 가능한가요?" +
                    " \r\n " +
                    "A.현금영수증은 가상계좌 결제 시 신청이 가능하며, 결제 진행 중 현금영수증 신청란에 정보를 입력하시면 발급이 가능합니다." +
                    " \r\n " +
                    " ※결제가 완료 된 이후 현금영수증 발급은 불가합니다." +
                    " \r\n " +
                    " \r\n " +

                    "Q.무통장입금은 언제까지 입금해야 하나요?" +
                    " \r\n " +
                    "A.무통장입금은 주문일로부터 7일이내까지 입금가능합니다." +
                    " \r\n " +
                    "7일이후 미입금 건은 자동으로 취소되니 유의바랍니다.";


         }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            Open7();
        }
        private void Open7()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SELECT_07", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            //  this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            // 이름으로 검색

            if (e.KeyCode == Keys.Enter)
            {

                Open7();
            }
        }


        private void Advice_Content()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SELECT_08", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_coid_name", MySqlDbType.VarChar, 20);
                        cmd.Parameters[0].Value = UserInfo.instance().Name;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
                            //  this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 삭제할 상담 내용을 선택 하세요 ");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

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
                Advice_Content();
            }
        }


        private void BtnDispYes_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtAddr.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 배종지 주소를 입력하세요 !");
                return;
            }

            string sNickName = string.Empty;

            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "주문 고객을 선택하세요");
                return;
            }

            if (string.IsNullOrEmpty(this.txtHp_No.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "전화번호를 입력하세요");
                return;
            }

            if (string.IsNullOrEmpty(this.txtU_Id.Text))
            {
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    sql.Query = "select u_id as u_id FROM  domalife.member_master where u_name = '" + txtName.EditValue + "' and  u_cell_num = '" + txtHp_No.EditValue + "'  and advice_seq > 0 ";
                    DataSet ds = sql.selectQueryDataSet();
                    txtU_Id.EditValue = sql.selectQueryForSingleValue();
                }
                if (string.IsNullOrEmpty(this.txtU_Id.Text))
                {
                    New_Member();
                }
            }


            txtP_Id.EditValue = Convert.ToInt32(gridView3.GetFocusedRowCellValue("id").ToString());

            string u_id = txtU_Id.EditValue.ToString();
            string u_name = txtName.EditValue.ToString();
            string p_id = gridView3.GetFocusedRowCellValue("id").ToString();
            string opt_id = gridView3.GetFocusedRowCellValue("opt_id").ToString();
            string p_name = gridView3.GetFocusedRowCellValue("p_name").ToString();
            string p_num = txtProdQty.EditValue.ToString();
            string p_price = gridView3.GetFocusedRowCellValue("p_org_price").ToString();
            int n_amt = Convert.ToInt32(p_num) * Convert.ToInt32(p_price);
            string p_amt = n_amt.ToString();
            string u_email;
            if (string.IsNullOrEmpty(this.txtE_Mail.Text))
            {
                u_email = "";
            }
            else
            {
                u_email = txtE_Mail.EditValue.ToString();
            }

            //string u_email = txtE_Mail.EditValue.ToString();
            string surl = "https://callpay.eyeoyou.com/lgu_pay/pay_crossplatform.aspx?&u_id=" + u_id + "&u_name=" + u_name + "&p_id=" + p_id + "&opt_id=" + opt_id + "&p_name=" + p_name + "&p_num=" + p_num + "&p_amt=" + p_amt + "&p_price=" + p_price + "&u_email=" + u_email + "&pay_code=SC0040" + "&p_type=01";

            System.Diagnostics.Process.Start(surl);

        }
        // 체험샵 회원 생성
        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            popup3 = new frmMM19_Pop03();

            popup3.sName = txtName.Text;
            popup3.sGshop_name = txtShop_Name.Text;
            popup3.sEMail = "";
            popup3.sHpNo = txtHp_No.Text;
            if (txtHp_No.EditValue == "010-")
            {
                popup3.sHpNo = "";
            }
            popup3.sPostNo = btnPost_No.Text;
            popup3.sAddr = txtAddr.Text;
            popup3.sAddrDetail = txtAddr_Detail.Text;
            

            popup3.FormClosed += popup_FormClosed3;
            popup3.ShowDialog();
        }
        private void popup_FormClosed3(object sender, FormClosedEventArgs e)
        {
            popup3.FormClosed -= popup_FormClosed3;
            //if (popup3.DialogResult == DialogResult.OK)
            //{
            //    this.txtName.EditValue = popup3.sName;
            //}
            popup3 = null;
        }


    }
}
