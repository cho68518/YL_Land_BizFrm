using Easy.Framework.Common;
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
            //  DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            ckPic_Send.EditValue = "N";
            rbAdvice_Type.EditValue = "01";
            //this.LabName.Appearance.Options.UseFont = true;
            //this.LabName.Appearance.Options.UseForeColor = true;
            //this.LabName.Appearance.Options.UseTextOptions = true;
            //this.LabName.Size = new System.Drawing.Size(200, 40);

            this.efwLabel5.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel4.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel13.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel6.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel11.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel9.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);

            this.efwLabel10.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel8.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel12.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel14.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel15.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);
            this.efwLabel9.Appearance.Font = new System.Drawing.Font("맑은고딕", 11F);


            gridView3.CustomUnboundColumnData += gridView3_CustomUnboundColumnData;


            // 문자 클릭
            gridView1.OptionsView.ShowFooter = true;
            this.efwGridControl1.BindControlSet(
                new ColumnControlSet("code_memo", txtMessage)
              );

            this.efwGridControl1.Click += efwGridControl1_Click;

            LabName.Text = UserInfo.instance().Name;
            OpenProd();
            OpenMessage();
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

        private void BtnDispYes_Click(object sender, EventArgs e)
        {

            popup = new frmMM19_Pop01();

            popup.p_Id = Convert.ToInt32(gridView3.GetFocusedRowCellValue("id").ToString());
            popup.u_Id = txtU_Id.Text;

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

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
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
            rbAdvice_Type.EditValue = "01";
            txtHp_No.EditValue = "010-";
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

                        //

                        cmd.ExecuteNonQuery();

                        txtIdx.EditValue = cmd.Parameters["o_idx"].Value.ToString();
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

                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
       
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            // 이름으로 검색
            if (e.KeyCode == Keys.Enter)
            {
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
                    MessageAgent.MessageShow(MessageType.Informational, "신규 상담 고객 입니다.");
                }
            }
        }
        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup1.FormClosed -= popup_FormClosed1;
            if (popup1.DialogResult == DialogResult.OK)
            {
                this.txtIdx.EditValue = popup1.nIdx;
                this.txtName.EditValue = popup1.sName;
                this.txtHp_No.EditValue = popup1.sHp_No;
                this.txtShop_Name.EditValue = popup1.sShop_Name;
                this.ckPic_Send.EditValue = popup1.sPic_Send;
                this.rbAdvice_Type.EditValue = popup1.sAdvice_Type;
                this.btnPost_No.EditValue = popup1.sPost_No;
                this.txtAddr.EditValue = popup1.sAddr;
                this.txtAddr_Detail.EditValue = popup1.sAddr_Detail;
                this.LabLast_Name.Text = popup1.sLast_Coid_Name;
            }
            popup1 = null;
        }

        private void txtHp_No_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
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
                    MessageAgent.MessageShow(MessageType.Informational, "신규 상담 고객 입니다.");
                }
            }
        }

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
                    MessageAgent.MessageShow(MessageType.Informational, "신규 상담 고객 입니다.");
                }
            }
        }

        private void txtShop_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int nCount;
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    sql.Query = "select count(*) as nCount FROM  domabiz.cust_advice where shop_name  like concat('%','" + txtShop_Name.EditValue + "', '%') ";
                    DataSet ds = sql.selectQueryDataSet();

                    nCount = Convert.ToInt32(sql.selectQueryForSingleValue());
                }
                if (nCount == 1)
                {

                    OneRowSelect("4");
                }
                else if (nCount > 1)
                {
                    popup1 = new frmMM19_Pop02();

                    popup1.sFildQuery = txtShop_Name.EditValue.ToString();

                    popup1.FormClosed += popup_FormClosed1;
                    popup1.ShowDialog();
                }
                else
                {
                    MessageAgent.MessageShow(MessageType.Informational, "신규 상담 고객 입니다.");
                }
            }
        }

    }
}
