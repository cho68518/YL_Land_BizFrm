using DevExpress.XtraEditors.Repository;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using YL_COMM.BizFrm;
using YL_DT.BizFrm.Dlg;
using System.Data.SqlClient;
using System.IO;
using Tamir.SharpSsh;
using System.Collections;
using System.Net.Sockets;



namespace YL_DT.BizFrm
{

    public partial class frmDT02 : FrmBase
    {
        frmDT02_Pop01 popup;
        public frmDT02()
        {
            InitializeComponent();
            this.QCode = "DT02";
            //폼명설정
            this.FrmName = "마스터 관리";
        }
        
        private void frmDT02_Load(object sender, EventArgs e)
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

            //그리드 컬럼에 체크박스 레포지토리아이템 추가
            rbUse_Member_Writing.EditValue = "Y";
            rbUse_Reply.EditValue = "Y";
            rbUse_Emoticon.EditValue = "Y";
            rbC_Is_Use.EditValue = "Y";
            rbUse_Photo_Upload.EditValue = "Y";
            rbUse_Html.EditValue = "Y";
            rbIs_Letter.EditValue = "Y";
            rbDate_Add.EditValue = "N";
            cmbImage_Choice.EditValue = "00";
            nCategory_No.EditValue = "";
            //
            rbUse_type.EditValue = "1";
            rbTelecom.EditValue = "1";
            rbShowType.EditValue = "Y";

            efwXtraTabControl1.SelectedTabPage = xtraTabPage1;

            this.efwGridControl1.BindControlSet(
                new ColumnControlSet("contents_id", txtContents_id)
              , new ColumnControlSet("contents_name", txtContents_name)
              , new ColumnControlSet("event_code", txtEvent_code)
              , new ColumnControlSet("event_amt", txtEvent_amt)
              , new ColumnControlSet("remark", txtRemark)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;

            gridView2.OptionsView.ShowFooter = true;
            this.efwGridControl2.BindControlSet(
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
              , new ColumnControlSet("is_use", rbC_Is_Use)
              , new ColumnControlSet("use_reply", rbUse_Reply)
              , new ColumnControlSet("use_photo_upload", rbUse_Photo_Upload)
              , new ColumnControlSet("use_member_writing", rbUse_Member_Writing)
              , new ColumnControlSet("use_html", rbUse_Html)
              , new ColumnControlSet("use_emoticon", rbUse_Emoticon)
              , new ColumnControlSet("title", txtC_Title)
              , new ColumnControlSet("hint_message", txtHint_Message)
              , new ColumnControlSet("hash_tags", txtHash_Tags)
              , new ColumnControlSet("is_letter", rbIs_Letter)
              , new ColumnControlSet("creation_date", txtCreation_Date)
              , new ColumnControlSet("send_message", txtSend_Message)
              , new ColumnControlSet("image_choice", cmbImage_Choice)
              , new ColumnControlSet("image_path", txtImage_Path)
              , new ColumnControlSet("date_add", rbDate_Add)
            );
            this.efwGridControl2.Click += efwGridControl2_Click;
            SetCmb();
        }
        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                SqlCommand cmd = new SqlCommand();
                sql.Query = " select t1.idx as idx, t1.sub_code as sub_code, t1.image_path as image_path, t1.is_best as is_best, t1.use_type as use_type, " +
                            "        t1.reg_date as reg_date, t1.remark as remark, t1.file_name as file_name " +
                            "   from domalife.tb_story_masters_images t1  " +
                            "   where t1.category_no =  '" + nCategory_No.EditValue + "'  " +
                            "  order by t1.idx ";

                DataSet ds = sql.selectQueryDataSet();
                gridControl2.DataSource = ds.Tables[0];
            }
        }
        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();
        
        private void layoutView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DataRow dr = (e.Row as DataRowView).Row;
            string url = string.Empty;

            if (e.Column.FieldName == "Image1")
            {
                url = dr["image_path"].ToString();
            }

            if (iconsCache.ContainsKey(url))
            {
                e.Value = iconsCache[url];
                return;
            }

            if (url != "")
            {
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
            Cursor.Current = Cursors.Default;
        }

        private void SetLegacyCode_Mysql(RepositoryItemLookUpEdit cdControl, string codeGroup)
        {
            DataTable retDT = CodeAgent.GetLegacyCodeCollection(codeGroup);


            cdControl.DataSource = retDT;
            //컨트롤 초기화
            InitCodeControl(cdControl);
        }

        private void InitCodeControl(object cdControl)
        {
            string DNAME = string.Empty;

            DNAME = "DNAME";

            CodeAgent.InitCodeControl(cdControl, "코드명", "코드", DNAME, "DCODE", "선택하세요");
        }

        private void SetCmb()
        {

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00079'  order by code_id";

                DataSet ds = con.selectQueryDataSet();
                DataTable retDT = ds.Tables[0];

                repositoryItemLookUpEdit2.DataSource = retDT;
                //컨트롤 초기화
                InitCodeControl(repositoryItemLookUpEdit2);

                repositoryItemLookUpEdit2.EndUpdate();
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00079' order by code_id";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbImage_Choice, codeArray);
            }
            cmbImage_Choice.EditValue = "00";
        }


        public override void NewMode()
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");

            Eraser.Clear(this, "CLR2");
            rbUse_Member_Writing.EditValue = "Y";
            rbUse_Reply.EditValue = "Y";
            rbUse_Emoticon.EditValue = "Y";
            rbC_Is_Use.EditValue = "Y";
            rbUse_Photo_Upload.EditValue = "Y";
            rbUse_Html.EditValue = "Y";
            rbDate_Add.EditValue = "N";
            rbIs_Letter.EditValue = "Y";
            txtCreation_Date.EditValue = "0";
            cmbImage_Choice.EditValue = "00";
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["contents_id"].ToString() != "")
            {
                this.txtContents_name.EditValue = dr["contents_name"].ToString();
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            txtContents_id.EditValue = "";
            txtContents_name.EditValue = "";
            txtEvent_code.EditValue = "";
            txtEvent_amt.EditValue = "";
            txtRemark.EditValue = "";
        }

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();
            }

            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open5();
            }
        }

        public override void Save()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Save1();
            }

            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Save2();
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Save4();
            }

        }

        public void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

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
        // 스토리 마스터
        public void Open2()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

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

        // 공지권한
        public void Open3()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            this.efwGridControl4.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }
        //공지 상세
        public void Open4()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        private void layoutView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DataRow dr = (e.Row as DataRowView).Row;
            string url = string.Empty;

            if (e.Column.FieldName == "Image1")
            {
                url = dr["ImageURL"].ToString();
            }

            if (iconsCache.ContainsKey(url))
            {
                e.Value = iconsCache[url];
                return;
            }

            if (url != "")
            {
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
            Cursor.Current = Cursors.Default;
        }


        //추천요금제
        public void Open5()
        {
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                SqlCommand cmd = new SqlCommand();
                sql.Query = " select t1.idx as idx, t1.category_no as category_no, (select name from domalife.story_category t2 where t2.code = t1.category_no ) as category_name, t1.img_url as ImageURL, " +
                            "        t1.show_type as show_type, t1.img_link as img_link, t1.remark as remark, t1.link_type  as link_type, t1.show_turn as show_turn, t1.telecom_type as telecom_type  " +
                            "   from domaadmin.tb_story_banner t1  " +
                            "   where category_no = 800 and " +
                            "         telecom_type =  '" + rbUse_type.EditValue + "'  " +
                            "  order by t1.idx ";

                DataSet ds = sql.selectQueryDataSet();
                gridControl1.DataSource = ds.Tables[0];
            }

        }

        public void Save1()
        {
            if (txtContents_id.EditValue.ToString() == "" ^ txtContents_id.EditValue.ToString() == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "컨텐츠 번호를 입력 또는 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_contents_id", MySqlDbType.Int32));
                            cmd.Parameters["i_contents_id"].Value = Convert.ToInt32(txtContents_id.EditValue);
                            cmd.Parameters["i_contents_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_contents_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_contents_name"].Value = txtContents_name.EditValue;
                            cmd.Parameters["i_contents_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_event_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_event_code"].Value = Convert.ToInt32(txtEvent_code.EditValue);
                            cmd.Parameters["i_event_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_event_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_event_name"].Value = txtEvent_name.EditValue;
                            cmd.Parameters["i_event_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_event_amt", MySqlDbType.VarChar));
                            cmd.Parameters["i_event_amt"].Value = Convert.ToInt32(txtEvent_amt.EditValue);
                            cmd.Parameters["i_event_amt"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

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

        public void Save2()
        {
            if (nCategory_No.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "스토리 번호를 입력하세요!");
                return;
            }
            if (txtStory_Name.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "스토리명을 입력하세요!");
                return;
            }
            if (txtStory_Donut_Type.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "머니 타입을 입력하세요!");
                return;
            }
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_SAVE_02", con))
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

                            cmd.Parameters.Add(new MySqlParameter("i_hash_tags", MySqlDbType.VarChar));
                            cmd.Parameters["i_hash_tags"].Value = txtHash_Tags.EditValue;
                            cmd.Parameters["i_hash_tags"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_letter", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_letter"].Value = rbIs_Letter.EditValue;
                            cmd.Parameters["i_is_letter"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_creation_date", MySqlDbType.VarChar));
                            cmd.Parameters["i_creation_date"].Value = txtCreation_Date.EditValue;
                            cmd.Parameters["i_creation_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_send_message", MySqlDbType.VarChar));
                            cmd.Parameters["i_send_message"].Value = txtSend_Message.EditValue;
                            cmd.Parameters["i_send_message"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_image_choice", MySqlDbType.VarChar));
                            cmd.Parameters["i_image_choice"].Value = cmbImage_Choice.EditValue;
                            cmd.Parameters["i_image_choice"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_image_path", MySqlDbType.VarChar));
                            cmd.Parameters["i_image_path"].Value = txtImage_Path.EditValue;
                            cmd.Parameters["i_image_path"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_date_add", MySqlDbType.VarChar));
                            cmd.Parameters["i_date_add"].Value = rbDate_Add.EditValue;
                            cmd.Parameters["i_date_add"].Direction = ParameterDirection.Input;

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

        public void Save4()
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_SAVE_04", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_show_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_show_type"].Value = rbShowType.EditValue;
                            cmd.Parameters["i_show_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_category_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_category_no"].Value = Convert.ToInt32("800");
                            cmd.Parameters["i_category_no"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_img_url", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url"].Value = txtimg_url.EditValue; 
                            cmd.Parameters["i_img_url"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_show_turn", MySqlDbType.Int32));
                            cmd.Parameters["i_show_turn"].Value = Convert.ToInt32(txtShow_Turn.EditValue);
                            cmd.Parameters["i_show_turn"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_telecom_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_telecom_type"].Value = rbTelecom.EditValue;
                            cmd.Parameters["i_telecom_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtTab4_Remark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

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

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(gridView1.GetFocusedDisplayText()); 
            e.Handled = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (nCategory_No.EditValue.ToString() == null ^ nCategory_No.EditValue.ToString() == "")
            {
                MessageAgent.MessageShow(MessageType.Warning, "스토리 번호를 선택하세요!");
                return;
            }
            popup = new frmDT02_Pop01();

            popup.category_no = Convert.ToInt32(nCategory_No.EditValue.ToString());
            popup.story_name = txtStory_Name.EditValue.ToString();

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
            }

            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
            }
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
            }
        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            Open4();
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {

                    var saveResult = new SaveTableResultInfo() { IsError = true };

                    var dt = efwGridControl3.GetChangeDataWithRowState;
                    var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][StatusColumn].ToString() == "U")
                        {
                            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                            {
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT02_SAVE_03", con))
                                {

                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                                    cmd.Parameters[0].Value = dt.Rows[i]["u_id"].ToString();

                                    cmd.Parameters.Add("i_code_1", MySqlDbType.VarChar,1);
                                    cmd.Parameters[1].Value = dt.Rows[i]["code_1"].ToString();

                                    cmd.Parameters.Add("i_code_2", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[2].Value = dt.Rows[i]["code_2"].ToString();

                                    cmd.Parameters.Add("i_code_3", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[3].Value = dt.Rows[i]["code_3"].ToString();

                                    cmd.Parameters.Add("i_code_4", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[4].Value = dt.Rows[i]["code_4"].ToString();

                                    cmd.Parameters.Add("i_code_5", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[5].Value = dt.Rows[i]["code_5"].ToString();

                                    cmd.Parameters.Add("i_code_6", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[6].Value = dt.Rows[i]["code_6"].ToString();

                                    cmd.Parameters.Add("i_code_7", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[7].Value = dt.Rows[i]["code_7"].ToString();

                                    cmd.Parameters.Add("i_code_8", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[8].Value = dt.Rows[i]["code_8"].ToString();

                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        private void txtM_Query_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Open4();
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            base.NewMode();
            Eraser.Clear(this, "CLR3");
            rbShowType.EditValue = "Y";
            txtShow_Turn.EditValue = "0";
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            //string sftpDirectory = "/domalifefiles/files/events/banner/" + Convert.ToString(System.DateTime.Now.ToString("yyyyMMdd"));
            string sftpDirectory = "/domalifefiles/files/events/banner/";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtOrg_FileName.EditValue = openFileDialog.SafeFileName;
                    txtPicPath1.EditValue = openFileDialog.FileName;
                    picBanner.LoadAsync(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }

                finally
                {
                    Cursor = Cursors.Default;
                }


                // 폴더생성

                DirectoryInfo di = new DirectoryInfo(@"c:\temp");
                if (di.Exists == false)
                {
                    di.Create();
                }

                // 선택된 파일을 위에서 만든 폴더에 이름을 바꿔 저장

                string sOldFile = txtPicPath1.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                //string sFileName = txtOrg_FileName.EditValue.ToString();
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Delete(sNewFile);
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                //저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/events/banner/";
                string sFtpPath2 = "/domalifefiles/files/events/banner/" + sFileName;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtPicPath1.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }
                //if (isdir == false)
                //{

                //    sSftp.Mkdir(sFtpPath2);
                //}

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtimg_url.EditValue = "https://media.domalife.net/files/events/banner/" + sFileName;
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.layoutView2.GetDataRow(0);

            if (dr != null && dr["idx"].ToString() != "")
            {
                txtIdx.EditValue = layoutView2.GetFocusedRowCellValue("idx").ToString();
                rbTelecom.EditValue = layoutView2.GetFocusedRowCellValue("telecom_type").ToString();
                rbShowType.EditValue = layoutView2.GetFocusedRowCellValue("show_type").ToString();
                txtShow_Turn.EditValue = layoutView2.GetFocusedRowCellValue("show_turn").ToString();
                txtTab4_Remark.EditValue = layoutView2.GetFocusedRowCellValue("remark").ToString();
                txtimg_url.EditValue = layoutView2.GetFocusedRowCellValue("ImageURL").ToString();
                picBanner.LoadAsync(txtimg_url.EditValue.ToString());
            }
        }

        private void rbUse_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Open5();
        }
    }
}
