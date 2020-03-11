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
using YL_GM.BizFrm.Dlg;

namespace YL_GM.BizFrm
{
    public partial class frmGM06 : FrmBase
    {
        frmMemberInfo popup;
        string sStoryNo = string.Empty;
        public frmGM06()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmDN01";
            //폼명설정
            this.FrmName = "스토리 현황";
            

        }

        public override void FrmLoadEvent()
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

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            
            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("idx", txtIdx)
            , new ColumnControlSet("money_type", txtMoney_Type)
            , new ColumnControlSet("money", txtMoney)
            , new ColumnControlSet("recomender_u_name", txtU_NAME_UPDATE)
            , new ColumnControlSet("recomender_login_id", txtUSER_ID_UPDATE)
            , new ColumnControlSet("recomender_u_nickname", txtU_NICKNAME_UPDATE)
            , new ColumnControlSet("recomender_u_id", txtU_Id_UPDATE)
            , new ColumnControlSet("expiration_date", dtExpiration_Date)
            );

            //this.efwGridControl1.Click += efwGridControl1_Click;

            SetCmb();
        }

        private void SetCmb()
        {
            // 회원검색

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00034'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbMember_Search, codeArray);
            }

            cmbMember_Search.EditValue = "01";

            // 스토리

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT category_no as DCODE, story_name  as DNAME  FROM  domaadmin.tb_story_info order by id  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbStory, codeArray);
            }

            cmbStory.EditValue = "";

        }

        
        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbMember_Search.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtI_SEARCH.EditValue;

                        cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtO_CodeQ.EditValue;

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


        public void O_Code()
        {
            if (string.IsNullOrEmpty(this.txtO_Code.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 주문 번호를 선택하세요!");
                return;
            }
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand(sStoryNo, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtO_Code.EditValue;

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
            Search();
        }

        public void U_Id()
        {
            if (string.IsNullOrEmpty(this.txtU_Id.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회원을 선택하세요!");
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand(sStoryNo, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtU_Id.EditValue;

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
            Search();
        }
        
        private void btnMemberSch_Click_1(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL"
            };
            popup.FormClosed += popup_FormClosed1;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtU_NAME.Text = popup.U_NAME;
                this.txtU_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtUSER_ID.EditValue = popup.USER_ID;
                this.txtU_Id.EditValue = popup.U_ID;

            }
            popup = null;
        }
        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL"
            };
            popup.FormClosed += popup_FormClosed2;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed2;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtU_NAME_UPDATE.Text = popup.U_NAME;
                this.txtU_NICKNAME_UPDATE.EditValue = popup.U_NICKNAME;
                this.txtUSER_ID_UPDATE.EditValue = popup.USER_ID;
                this.txtU_Id_UPDATE.EditValue = popup.U_ID;

            }
            popup = null;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 수정할 스토리를 선택하세요!");
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM06_SAVE_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_Idx", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtIdx.EditValue);

                        cmd.Parameters.Add("i_proc_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = "U";

                        cmd.Parameters.Add("i_money_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = txtMoney_Type.EditValue;

                        cmd.Parameters.Add("i_money", MySqlDbType.Int32);
                        cmd.Parameters[3].Value = Convert.ToInt32(txtMoney.EditValue);

                        cmd.Parameters.Add("i_recomender_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtU_Id_UPDATE.EditValue;

                        cmd.Parameters.Add("i_recomender_u_name", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = txtU_NAME_UPDATE.EditValue;

                        cmd.Parameters.Add("i_recomender_u_nickname", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = txtU_NICKNAME_UPDATE.EditValue;

                        cmd.Parameters.Add("i_recomender_login_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[7].Value = txtUSER_ID_UPDATE.EditValue;

                        cmd.Parameters.Add("i_expiration_date", MySqlDbType.DateTime);
                        cmd.Parameters[8].Value = dtExpiration_Date.EditValue;

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
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Search();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 삭제할 스토리를 선택하세요!");
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM06_SAVE_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_Idx", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtIdx.EditValue);

                        cmd.Parameters.Add("i_proc_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value ="D";

                        cmd.Parameters.Add("i_money_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = txtMoney_Type.EditValue;

                        cmd.Parameters.Add("i_money", MySqlDbType.Int32);
                        cmd.Parameters[3].Value = Convert.ToInt32(txtMoney.EditValue);

                        cmd.Parameters.Add("i_recomender_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtU_Id_UPDATE.EditValue;

                        cmd.Parameters.Add("i_recomender_u_name", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = txtU_NAME_UPDATE.EditValue;

                        cmd.Parameters.Add("i_recomender_u_nickname", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = txtU_NICKNAME_UPDATE.EditValue;

                        cmd.Parameters.Add("i_recomender_login_id", MySqlDbType.VarChar, 10);
                        cmd.Parameters[7].Value = txtUSER_ID_UPDATE.EditValue;

                        cmd.Parameters.Add("i_expiration_date", MySqlDbType.DateTime);
                        cmd.Parameters[8].Value = dtExpiration_Date.EditValue;

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
            MessageAgent.MessageShow(MessageType.Informational, "삭제 되었습니다.");
            Search();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sStory = cmbStory.EditValue.ToString();

            if (string.IsNullOrEmpty(this.cmbStory.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 생성할 스토리를 선택하세요");
                return;
            }

            //if (sStory == "208" || sStory == "221" || sStory == "232" || sStory == "242" || sStory == "244" || sStory == "229" || sStory == "245" || sStory == "222")

            // PS 후기스토리   o_code
            if (sStory == "208")
                {  
                    sStoryNo = "domabiz.USP_GM_GM06_INSERT_01";
                    O_Code();
                }
            // PS 축하스토리   o_code
            else if (sStory == "221")
                {   
                    sStoryNo = "domabiz.USP_GM_GM06_INSERT_02";
                    O_Code();
                }
            // 알뜰지원스토리  o_code
            else if (sStory == "232")
                {  
                    sStoryNo = "domabiz.USP_GM_GM06_INSERT_03";
                    O_Code();
                }
            // GS 축하스토리   o_code
            else if (sStory == "242")
                {   
                    sStoryNo = "domabiz.USP_GM_GM06_INSERT_04";
                    O_Code();
                }
            // GR 축하스토리   o_code
            else if (sStory == "244")
                {  
                    sStoryNo = "domabiz.USP_GM_GM06_INSERT_05";
                    O_Code();
                }
            // PS 감사스토리   o_code
            else if (sStory == "229")
                {   
                    sStoryNo = "domabiz.USP_GM_GM06_INSERT_06";
                    O_Code();
                }
            // PS 연장스토리   u_id
            else if (sStory == "245")
                {  
                    sStoryNo = "domabiz.USP_GM_GM06_INSERT_001";
                    U_Id();
                }
            // 알뜰 추천스토리 u_id
            else if (sStory == "222")
                {  
                    sStoryNo = "domabiz.USP_GM_GM06_INSERT_002";
                    U_Id();
                }
            else
            {
                MessageAgent.MessageShow(MessageType.Warning, "준비중.. 입니다");
                return;
            }

        }

    }
}


