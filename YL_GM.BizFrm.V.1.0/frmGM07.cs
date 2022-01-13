using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Report;
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

namespace YL_GM.BizFrm
{
    public partial class frmGM07 : FrmBase
    {
        public frmGM07()
        {
            InitializeComponent();
            this.QCode = "GM07";
            //폼명설정
            this.FrmName = "스토리 마스터";
        }

        private void frmGM07_Load(object sender, EventArgs e)
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

            //그리드 컬럼에 체크박스 레포지토리아이템 추가
            rbUse_Member_Writing.EditValue = "Y";
            rbUse_Reply.EditValue = "Y";
            rbUse_Emoticon.EditValue = "Y";
            rbC_Is_Use.EditValue = "Y";
            rbUse_Photo_Upload.EditValue = "Y";
            rbUse_Html.EditValue = "Y";
            dtReg_Date.EditValue = DateTime.Now;
            efwXtraTabControl1.SelectedTabPage = xtraTabPage1;

            gridView1.OptionsView.ShowFooter = true;
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("id", nID)
                    , new ColumnControlSet("category_no", nCategory_No)
                    , new ColumnControlSet("story_name", txtStory_Name)
                    , new ColumnControlSet("story_category", txtStory_Category)
                    , new ColumnControlSet("story_donut_cost", nStory_Donut_Cost)
                    , new ColumnControlSet("story_donut_type", txtStory_Donut_Type)
                    , new ColumnControlSet("payment_cost", nPayment_Cost)
                    , new ColumnControlSet("expiration_add_time", nExpiration_Add_Time)
                    , new ColumnControlSet("sort_id", nSort_Id)
                    , new ColumnControlSet("comment", txtComment)
                    , new ColumnControlSet("etc", txtEtc)
                    , new ColumnControlSet("Payment_Type", txtPayment_Type)

                      );

            this.efwGridControl2.BindControlSet(
                      new ColumnControlSet("seq", txtSeq)
                    , new ColumnControlSet("gcode_id", txtGcode_Id)
                    , new ColumnControlSet("code_id", txtCode_Id)
                    , new ColumnControlSet("code_nm", txtCode_NM)
                    , new ColumnControlSet("code_memo", txtCode_Memo)
                      );

            this.efwGridControl2.Click += efwGridControl2_Click;

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl4.BindControlSet(
              new ColumnControlSet("idx", txtidx)
            , new ColumnControlSet("pay_code", txtpay_code)
            , new ColumnControlSet("pay_name", txtpay_name)
            , new ColumnControlSet("donut_count", txtdonut_count)
            , new ColumnControlSet("vip_donut_count", txtvip_donut_count)
            , new ColumnControlSet("period_donut_count", txtperiod_donut_count)
            , new ColumnControlSet("period_vip_donut_count", txtperiod_vip_donut_count)
            , new ColumnControlSet("recommend_donut_count", txtrecommend_donut_count)
            , new ColumnControlSet("is_use", cbis_use)
            , new ColumnControlSet("al_open_donut_count", txtal_open_donut_count)
            );
            cbis_use.EditValue = "Y";

            this.efwGridControl5.BindControlSet(
              new ColumnControlSet("version_code", txtVersion_code)
            , new ColumnControlSet("version_name", txtVersion_name)
            , new ColumnControlSet("update_code", txtUpdate_code)
            , new ColumnControlSet("title", txtTitle)
            , new ColumnControlSet("message", txtMessage)
            , new ColumnControlSet("is_published", rbIs_published)
            );

            gridView6.OptionsView.ShowFooter = true;
            this.efwGridControl6.BindControlSet(
                      new ColumnControlSet("idx", txtCost_idx)
                    , new ColumnControlSet("major_code", cmbMajor_Code)
                    , new ColumnControlSet("large_code", cmbLarge_Code)
                    , new ColumnControlSet("middle_code", txtMiddle_Code)
                    , new ColumnControlSet("middle_name", txtMiddle_Name)
                    , new ColumnControlSet("small_code", cmbSmall_Code)
                    , new ColumnControlSet("sort", txtSort)
                    , new ColumnControlSet("use_yn", tbUse_YN)
                    , new ColumnControlSet("remark", txtCustRemark)
                      );

            this.efwGridControl6.Click += efwGridControl6_Click;

            gridView7.OptionsView.ShowFooter = true;
            this.efwGridControl7.BindControlSet(
                      new ColumnControlSet("code", txtCode)
                    , new ColumnControlSet("name", txtName)
                    , new ColumnControlSet("is_use", rbC_Is_Use)
                    , new ColumnControlSet("reg_date", dtReg_Date)
                    , new ColumnControlSet("use_reply", rbUse_Reply)
                    , new ColumnControlSet("use_photo_upload", rbUse_Photo_Upload)
                    , new ColumnControlSet("use_member_writing", rbUse_Member_Writing)
                    , new ColumnControlSet("use_html", rbUse_Html)
                    , new ColumnControlSet("use_emoticon", rbUse_Emoticon)
                    , new ColumnControlSet("title", txtC_Title)
                    , new ColumnControlSet("hint_message", txtHint_Message)
                   );


            cbis_use.EditValue = "Y";
            rbIs_published.EditValue = "Y";
            tbUse_YN.EditValue = "Y";
            SetCmb();
        }
        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00056'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbmajor_codeQ, codeArray);
                CodeAgent.MakeCodeControl(this.cmbMajor_Code, codeArray);
            }

            cmbmajor_codeQ.EditValue = "4";
            cmbMajor_Code.EditValue = "4";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00057' and code_id in ('01','02','03','98') ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbLarge_Code, codeArray);
            }

            cmbLarge_Code.EditValue = "01";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00058' and code_id = '00' ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbSmall_Code, codeArray);
            }

            cmbSmall_Code.EditValue = "00";

        }


        private void efwGridControl1_Click(object sender, EventArgs e)
        {
        }
        private void efwGridControl2_Click(object sender, EventArgs e)
        {
        }

        private void efwGridControl6_Click(object sender, EventArgs e)
        {

        }
        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();  //스토리 마스터
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();  //스토리 색상코드 마스터
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();  //스토리 색상코드 마스터
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open4();  //스토리 색상코드 마스터
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                Open5();  //도라앱 버전
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage6)
            {
                Open6();  //원가항목
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage7)
            {
                Open7();  // 스토리 카테고리
            }
        }
        //스토리 마스터
        public void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

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
        //스토리별 색상
        public void Open2()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
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
        // MySql 공통코드
        public void Open3()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
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
        // 요금제
        public void Open4()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_pay_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtPay_CodeQ.EditValue;

                        cmd.Parameters.Add("i_pay_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtPay_NameQ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
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
        // 도라앱 버전
        public void Open5()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl5.DataBind(ds);
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
        public void Open6()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = cmbmajor_codeQ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl6.DataBind(ds);
                            this.efwGridControl6.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public void Open7()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SELECT_06", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = cmbmajor_codeQ.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl7.DataBind(ds);
                            this.efwGridControl7.MyGridView.BestFitColumns();

                        }
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
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                            cmd.Parameters["i_id"].Value = Convert.ToInt32(nID.EditValue);
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_category_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_category_no"].Value = Convert.ToInt32(nCategory_No.EditValue);
                            cmd.Parameters["i_category_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_name"].Value = txtStory_Name.EditValue;
                            cmd.Parameters["i_story_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_category", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_category"].Value = txtStory_Category.EditValue;
                            cmd.Parameters["i_story_category"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_donut_cost", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_donut_cost"].Value = Convert.ToInt32(nStory_Donut_Cost.EditValue);
                            cmd.Parameters["i_story_donut_cost"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_donut_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_donut_type"].Value = txtStory_Donut_Type.EditValue;
                            cmd.Parameters["i_story_donut_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_payment_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_payment_type"].Value = txtPayment_Type.EditValue;
                            cmd.Parameters["i_payment_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_payment_cost", MySqlDbType.VarChar));
                            cmd.Parameters["i_payment_cost"].Value = Convert.ToInt32(nPayment_Cost.EditValue);
                            cmd.Parameters["i_payment_cost"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_expiration_add_time", MySqlDbType.VarChar));
                            cmd.Parameters["i_expiration_add_time"].Value = Convert.ToInt32(nExpiration_Add_Time.EditValue);
                            cmd.Parameters["i_expiration_add_time"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sort_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_sort_id"].Value = Convert.ToInt32(nSort_Id.EditValue);
                            cmd.Parameters["i_sort_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_comment", MySqlDbType.VarChar));
                            cmd.Parameters["i_comment"].Value = txtComment.EditValue;
                            cmd.Parameters["i_comment"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_etc", MySqlDbType.VarChar));
                            cmd.Parameters["i_etc"].Value = txtEtc.EditValue;
                            cmd.Parameters["i_etc"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_return", MySqlDbType.VarChar));
                            cmd.Parameters["o_return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show(cmd.Parameters["o_return"].Value.ToString());

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

        private void txtPay_CodeQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void txtPay_NameQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM03_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pay_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_pay_code"].Value = txtpay_code.EditValue;
                            cmd.Parameters["i_pay_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pay_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_pay_name"].Value = txtpay_name.EditValue;
                            cmd.Parameters["i_pay_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_donut_count"].Value = Convert.ToInt32(txtdonut_count.EditValue);
                            cmd.Parameters["i_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_vip_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_vip_donut_count"].Value = Convert.ToInt32(txtvip_donut_count.EditValue);
                            cmd.Parameters["i_vip_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_period_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_period_donut_count"].Value = Convert.ToInt32(txtperiod_donut_count.EditValue);
                            cmd.Parameters["i_period_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_period_vip_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_period_vip_donut_count"].Value = Convert.ToInt32(txtperiod_vip_donut_count.EditValue);
                            cmd.Parameters["i_period_vip_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recommend_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_recommend_donut_count"].Value = Convert.ToInt32(txtrecommend_donut_count.EditValue);
                            cmd.Parameters["i_recommend_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = cbis_use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_al_open_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_al_open_donut_count"].Value = Convert.ToInt32(txtal_open_donut_count.EditValue);
                            cmd.Parameters["i_al_open_donut_count"].Direction = ParameterDirection.Input;

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
                Open4();
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtidx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM03_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue);
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
                Open4();
            }
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Version_code", MySqlDbType.Int32));
                            cmd.Parameters["i_Version_code"].Value = Convert.ToInt32(txtVersion_code.EditValue);
                            cmd.Parameters["i_Version_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Version_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_Version_name"].Value = txtVersion_name.EditValue;
                            cmd.Parameters["i_Version_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Update_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_Update_code"].Value = Convert.ToInt32(txtUpdate_code.EditValue);
                            cmd.Parameters["i_Update_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Title", MySqlDbType.VarChar));
                            cmd.Parameters["i_Title"].Value = txtTitle.EditValue;
                            cmd.Parameters["i_Title"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Message", MySqlDbType.VarChar));
                            cmd.Parameters["i_Message"].Value = txtMessage.EditValue;
                            cmd.Parameters["i_Message"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Is_published", MySqlDbType.VarChar));
                            cmd.Parameters["i_Is_published"].Value = rbIs_published.EditValue;
                            cmd.Parameters["i_Is_published"].Direction = ParameterDirection.Input;

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
                Open5();
            }
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            base.NewMode();
            Eraser.Clear(this, "CLR5");
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Version_code", MySqlDbType.Int32));
                            cmd.Parameters["i_Version_code"].Value = Convert.ToInt32(txtVersion_code.EditValue);
                            cmd.Parameters["i_Version_code"].Direction = ParameterDirection.Input;

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
                Open5();
                Eraser.Clear(this, "CLR5");
            }
        }

        private void cmbMajor_Code_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbMajor_Code.EditValue.ToString() == "4")
            {
                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00057'  and code_memo = '1'  ";

                    DataSet ds = con.selectQueryDataSet();
                    //DataTable retDT = ds.Tables[0];
                    DataRow[] dr = ds.Tables[0].Select();
                    CodeData[] codeArray = new CodeData[dr.Length];

                    // cmbTAREA1.EditValue = "";
                    // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                    for (int i = 0; i < dr.Length; i++)
                        codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                    CodeAgent.MakeCodeControl(this.cmbLarge_Code, codeArray);
                }

                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00058' ";

                    DataSet ds = con.selectQueryDataSet();
                    //DataTable retDT = ds.Tables[0];
                    DataRow[] dr = ds.Tables[0].Select();
                    CodeData[] codeArray = new CodeData[dr.Length];

                    // cmbTAREA1.EditValue = "";
                    // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                    for (int i = 0; i < dr.Length; i++)
                        codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                    CodeAgent.MakeCodeControl(this.cmbSmall_Code, codeArray);
                }

                cmbLarge_Code.EditValue = "01";
                cmbSmall_Code.EditValue = "00";
            }
            else
            {
                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00057'  and code_memo = '2'   ";

                    DataSet ds = con.selectQueryDataSet();
                    //DataTable retDT = ds.Tables[0];
                    DataRow[] dr = ds.Tables[0].Select();
                    CodeData[] codeArray = new CodeData[dr.Length];

                    // cmbTAREA1.EditValue = "";
                    // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                    for (int i = 0; i < dr.Length; i++)
                        codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                    CodeAgent.MakeCodeControl(this.cmbLarge_Code, codeArray);
                }

                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00058' and code_id <> '00' ";

                    DataSet ds = con.selectQueryDataSet();
                    //DataTable retDT = ds.Tables[0];
                    DataRow[] dr = ds.Tables[0].Select();
                    CodeData[] codeArray = new CodeData[dr.Length];

                    // cmbTAREA1.EditValue = "";
                    // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                    for (int i = 0; i < dr.Length; i++)
                        codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                    CodeAgent.MakeCodeControl(this.cmbSmall_Code, codeArray);
                }
                cmbLarge_Code.EditValue = "04";
                cmbSmall_Code.EditValue = "01";
            }
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_DELETE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtCost_idx.EditValue).ToString();
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
                txtCost_idx.EditValue = "0";
                txtCustRemark.EditValue = "";
                txtMiddle_Code.EditValue = "";
                txtMiddle_Name.EditValue = "";
                txtCustRemark.EditValue = "";
                txtSort.EditValue = "0";

                Open6();

            }
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            txtCost_idx.EditValue = "0";
            txtCustRemark.EditValue = "";
            txtMiddle_Code.EditValue = "";
            txtMiddle_Name.EditValue = "";
            txtCustRemark.EditValue = "";
            txtSort.EditValue = "0";
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SAVE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtCost_idx.EditValue).ToString();
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Major_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_Major_Code"].Value = cmbMajor_Code.EditValue;
                            cmd.Parameters["i_Major_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Large_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_Large_Code"].Value = cmbLarge_Code.EditValue;
                            cmd.Parameters["i_Large_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Middle_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_Middle_Code"].Value = txtMiddle_Code.EditValue;
                            cmd.Parameters["i_Middle_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Middle_Name", MySqlDbType.VarChar));
                            cmd.Parameters["i_Middle_Name"].Value = txtMiddle_Name.EditValue;
                            cmd.Parameters["i_Middle_Name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Small_Code", MySqlDbType.VarChar));
                            cmd.Parameters["i_Small_Code"].Value = cmbSmall_Code.EditValue;
                            cmd.Parameters["i_Small_Code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Sort", MySqlDbType.Int32));
                            cmd.Parameters["i_Sort"].Value = Convert.ToInt32(txtSort.EditValue).ToString();
                            cmd.Parameters["i_Sort"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_use_yn", MySqlDbType.VarChar));
                            cmd.Parameters["i_use_yn"].Value = tbUse_YN.EditValue;
                            cmd.Parameters["i_use_yn"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_Remark"].Value = txtCustRemark.EditValue;
                            cmd.Parameters["i_Remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            txtCost_idx.EditValue = cmd.Parameters["o_idx"].Value.ToString();
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Open6();
            }
        }

        private void cmbmajor_codeQ_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void efwSimpleButton10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCode.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 카테코리 코드를 입력하세요!");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SAVE_04", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_code", MySqlDbType.Int32));
                            cmd.Parameters["i_code"].Value = Convert.ToInt32(txtCode.EditValue).ToString();
                            cmd.Parameters["i_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_name"].Value = txtName.EditValue;
                            cmd.Parameters["i_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = rbC_Is_Use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_use_reply", MySqlDbType.VarChar));
                            cmd.Parameters["i_use_reply"].Value = rbUse_Reply.EditValue;
                            cmd.Parameters["i_use_reply"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_use_photo_upload", MySqlDbType.VarChar));
                            cmd.Parameters["i_use_photo_upload"].Value = rbUse_Photo_Upload.EditValue;
                            cmd.Parameters["i_use_photo_upload"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_use_member_writing", MySqlDbType.VarChar));
                            cmd.Parameters["i_use_member_writing"].Value = rbUse_Member_Writing.EditValue;
                            cmd.Parameters["i_use_member_writing"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_use_html", MySqlDbType.VarChar));
                            cmd.Parameters["i_use_html"].Value = rbUse_Html.EditValue;
                            cmd.Parameters["i_use_html"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_use_emoticon", MySqlDbType.VarChar));
                            cmd.Parameters["i_use_emoticon"].Value = rbUse_Emoticon.EditValue;
                            cmd.Parameters["i_use_emoticon"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_title", MySqlDbType.VarChar));
                            cmd.Parameters["i_title"].Value = txtC_Title.EditValue;
                            cmd.Parameters["i_title"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hint_message", MySqlDbType.VarChar));
                            cmd.Parameters["i_hint_message"].Value = txtHint_Message.EditValue;
                            cmd.Parameters["i_hint_message"].Direction = ParameterDirection.Input;


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
                Open6();
            }
        }

        private void efwSimpleButton11_Click(object sender, EventArgs e)
        {

            txtCode.EditValue = "";
            txtName.EditValue = "";
            rbUse_Member_Writing.EditValue = "Y";
            rbUse_Reply.EditValue = "Y";
            rbUse_Emoticon.EditValue = "Y";
            rbC_Is_Use.EditValue = "Y";
            rbUse_Photo_Upload.EditValue = "Y";
            rbUse_Html.EditValue = "Y";
            txtC_Title.EditValue = "";
            txtHint_Message.EditValue = "";
            dtReg_Date.EditValue = DateTime.Now;
        }
    }
}
