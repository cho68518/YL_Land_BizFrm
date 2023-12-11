#region "frmMM05 설명"
//===================================================================================================
//■Program Name  : frmMM05
//■Description   : 회원코드
//■Author        : 조정현
//■Date          : 2019.04.19
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.04.19][조정현] Base
//[2] [2019.04.19][조정현] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using YL_MM.BizFrm.Dlg;

namespace YL_MM.BizFrm
{
    public partial class frmMM05 : FrmBase
    {
        frmMemberInfo popup;
        frmMemberInfo_Dup popup2;

        #region Fields

        bool _is_dupchk = false;

        #endregion

        #region 생성자
        public frmMM05()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MM05";
            //폼명설정
            this.FrmName = "회원정보(도넛라이프)";

            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);
        }
        #endregion

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
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

            dt_s_date.EditValue = DateTime.Now;
            dt_e_date.EditValue = DateTime.Now;
            rb_Search_type.EditValue = "1";
            //chkQ1.EditValue = "1";

            if (UserInfo.instance().UserId == "169.254.169.113" || UserInfo.instance().UserId == "0000000024")
            {
                layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            if (UserInfo.instance().ORG_CD != null && UserInfo.instance().ORG_CD.ToString() != "")
            {
                txtCOMPANYCD.EditValue = UserInfo.instance().ORG_CD;
            }
            else
            {
                txtCOMPANYCD.EditValue = "YL01";
            }

            if (UserInfo.instance().ORG_NM != null && UserInfo.instance().ORG_NM.ToString() != "")
            {
                txtCOMPANYNAME.EditValue = UserInfo.instance().ORG_NM;
            }
            else
            {
                txtCOMPANYNAME.EditValue = "(주)와이엘랜드";
            }

            //chkQ1.Checked = false;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_gr_md");
            //GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_doramd");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_biz");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_support_team");

            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["reg_date"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns["reg_date"].SummaryItem.FieldName = "reg_date";
            //gridView1.Columns["MCNT"].SummaryItem.DisplayFormat = "총인원: {0:n2}";
            gridView1.Columns["reg_date"].SummaryItem.DisplayFormat = "총인원: {0:0}";

            gridView1.Columns["DM_Money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["DM_Money"].SummaryItem.FieldName = "DM_Money";
            gridView1.Columns["DM_Money"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["TD_Money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["TD_Money"].SummaryItem.FieldName = "TD_Money";
            gridView1.Columns["TD_Money"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["AD_Money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["AD_Money"].SummaryItem.FieldName = "AD_Money";
            gridView1.Columns["AD_Money"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["GD_Money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["GD_Money"].SummaryItem.FieldName = "GD_Money";
            gridView1.Columns["GD_Money"].SummaryItem.DisplayFormat = "{0:c}";

            gridView1.Columns["CD_Money"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["CD_Money"].SummaryItem.FieldName = "CD_Money";
            gridView1.Columns["CD_Money"].SummaryItem.DisplayFormat = "{0:c}";

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                        new ColumnControlSet("u_name"            , txtU_NAME)
                      , new ColumnControlSet("user_id"           , txtUSER_ID)
                      , new ColumnControlSet("u_id"              , txtU_ID)
                      , new ColumnControlSet("u_nickname"        , txtU_NICKNAME)
                      , new ColumnControlSet("u_gender"          , txtU_GENDER)
                      , new ColumnControlSet("u_birthday"        , txtBIRTH)
                      , new ColumnControlSet("u_email"           , txtU_EMAIL)
                      , new ColumnControlSet("u_cell_num"        , txtU_CELL_NUM)
                      , new ColumnControlSet("reg_date"          , txtREG_DATE)
                      , new ColumnControlSet("login_date"        , txtLOGIN_DATE)
                      , new ColumnControlSet("u_zip"             , txtU_ZIP)
                      , new ColumnControlSet("u_addr"            , txtU_ADDR)
                      , new ColumnControlSet("u_addr_detail"     , txtU_ADDR_DETAIL)
                      , new ColumnControlSet("u_chef_level_cd"   , cmbU_CHEF_LEVEL)
                      , new ColumnControlSet("is_al_friend_yn"   , chkIS_AL_FRIEND)
                      , new ColumnControlSet("is_stock_friend_yn", chkIS_STOCK_FRIEND)
                      , new ColumnControlSet("idx"               , txtIDX)
                      , new ColumnControlSet("is_doramd"         , chkIS_DORAMD)

                      , new ColumnControlSet("DM_Money"          , txtD)
                      , new ColumnControlSet("TD_Money"          , txtTD)
                      , new ColumnControlSet("AD_Money"          , txtAD)
                      , new ColumnControlSet("GD_Money"          , txtGD)
                      , new ColumnControlSet("CD_Money"          , txtCD)
                      , new ColumnControlSet("VIP_Money"         , txtCOUPON)
                      , new ColumnControlSet("is_gr_md"          , chkIS_GR_MD)
                      , new ColumnControlSet("is_doramd"         , chkIS_DORAMD)
                      , new ColumnControlSet("is_biz"            , chkIS_BIZ)
                      , new ColumnControlSet("chef_u_nickname"   , txtCHEF_U_NICKNAME)
                      , new ColumnControlSet("vip_reco_nickname" , txtVIP_RECO_NICKNAME)
                      , new ColumnControlSet("doramd_type"       , cmbDORAMD_TYPE)
                      , new ColumnControlSet("is_support_team"   , chkIS_SUPPORT_TEAM)
                      , new ColumnControlSet("is_comp_md"        , chkIS_COMP_MD)
                      , new ColumnControlSet("is_official"       , ckis_official)
                      , new ColumnControlSet("is_vvip"           , cbis_vvip)
                      , new ColumnControlSet("bank"              , txtBank)
                      , new ColumnControlSet("acc_no"            , txtAccNo)
                      , new ColumnControlSet("account_holder"    , txtAccount_Holder)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;
        }

        #endregion

        #region Shown

        public override void FrmShown()
        {
            base.FrmShown();

            New();
            cmbQ3.ItemIndex = 0;
            cmbQ3_1.ItemIndex = 0;
        }

        #endregion

        #region NewMode

        public override void NewMode()
        {
            base.NewMode();

            New();
        }

        private void New()
        {
            cmbQ1.EditValue = "0";
            cmbQ2.EditValue = "0";
            cmbQ1_1.EditValue = "0";
            //cmbQ3.EditValue = "전체";
            Eraser.Clear(this, "CLR1");

            //chkQ1.Checked = false;
            cmbQ3.ItemIndex = 0;
            cmbQ3_1.ItemIndex = 0;
            txtSearch.Focus();
        }

        #endregion

        #region 조회


        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open2();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open3();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open4();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open5();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                Open6();
            }
        }

        public void Open2()
        {
            //base.Search();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string s1 = string.Empty;

                if (this.cmbQ3.EditValue == null)
                {
                    s1 = "";
                }
                else
                {
                    s1 = this.cmbQ3.EditValue.ToString().Replace("%", "");
                }
                //base.Search();
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM05_SELECT_01"
                , this.txtCOMPANYCD.Text
                , this.cmbQ1.EditValue
                , this.txtSearch.Text
                , this.cmbQ2.EditValue
                , s1
                , this.dt1.EditValue3
                , this.dt2.EditValue3
                , "1"
                );

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion


        public void Open6()
        {
            //base.Search();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string s1 = string.Empty;

                if (this.cmbQ3_1.EditValue == null)
                {
                    s1 = "";
                }
                else
                {
                    s1 = this.cmbQ3_1.EditValue.ToString().Replace("%", "");
                }
                //base.Search();
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM05_SELECT_01"
                , this.txtCOMPANYCD.Text
                , "0"
                , this.txtSearch_1.Text
                , this.cmbQ2.EditValue
                , s1
                , this.dt1.EditValue3
                , this.dt2.EditValue3
                , "1"
                );

                efwGridControl5.DataBind(ds);
                this.efwGridControl5.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public void Open1()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM05_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar);
                        cmd.Parameters[0].Value = txtU_ID.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public void Open3()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM05_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        

                        cmd.Parameters.Add("i_search_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = rb_Search_type.EditValue;


                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dt_s_date.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[2].Value = dt_e_date.EditValue3;

                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar);
                        cmd.Parameters[3].Value = txtQuery.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl4.DataBind(ds);
                            this.efwGridControl4.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public void Open4()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM05_SELECT_06", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar);
                        cmd.Parameters[0].Value = txtMember_query.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public void Open5()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM05_SELECT_07", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar);
                        cmd.Parameters[0].Value = txtLogin_Query.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl6.DataBind(ds);
                            this.efwGridControl6.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #region 저장




        private void BtnSave_Click(object sender, EventArgs e)
        {



            //if (UserInfo.instance().UserId != "0000000024" && UserInfo.instance().UserId != "0000000012" && UserInfo.instance().UserId != "169.254.169.113" && UserInfo.instance().UserId != "0000000027" && UserInfo.instance().UserId != "2020020002" && UserInfo.instance().UserId != "0000000036" && UserInfo.instance().UserId != "0000000036" )
            //{
            //    MessageAgent.MessageShow(MessageType.Error, "처리권한이 없습니다!");
            //    IsAutoSearch = false;
            //    return;
            //}

            if (string.IsNullOrEmpty(this.txtIDX.Text))
            {
                MessageAgent.MessageShow(MessageType.Error, "회원을 선택하신후에 처리하세요!");
                IsAutoSearch = false;
                return;
            }


            if (cmbU_CHEF_LEVEL.EditValue.ToString() == "4")
            {
                MessageAgent.MessageShow(MessageType.Error, "멀티샵 등급인경우 헬퍼로 선택후 미용실(G샵) 체크하세요 !");
                IsAutoSearch = false;
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {

                if (ValidationAgentEx.IsRequireCheck(this.layoutControl1.Controls, "R1"))
                {
                    try
                    {
                        //히스토리 내용 생성
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM05_SAVE_03", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                                cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIDX.EditValue);
                                cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                                cmd.Parameters["i_u_id"].Value = txtU_ID.EditValue;
                                cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                                cmd.Parameters["i_name"].Value = txtU_NAME.EditValue;
                                cmd.Parameters["i_name"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_nickname", MySqlDbType.VarChar));
                                cmd.Parameters["i_nickname"].Value = txtU_NICKNAME.EditValue;
                                cmd.Parameters["i_nickname"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_u_email", MySqlDbType.VarChar));
                                cmd.Parameters["i_u_email"].Value = txtU_EMAIL.EditValue;
                                cmd.Parameters["i_u_email"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_doramd_type", MySqlDbType.VarChar));
                                cmd.Parameters["i_doramd_type"].Value = cmbDORAMD_TYPE.EditValue;
                                cmd.Parameters["i_doramd_type"].Direction = ParameterDirection.Input;
                                
                                cmd.Parameters.Add(new MySqlParameter("i_is_comp_md", MySqlDbType.VarChar));
                                cmd.Parameters["i_is_comp_md"].Value = chkIS_COMP_MD.EditValue;
                                cmd.Parameters["i_is_comp_md"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                                cmd.Parameters["i_post_no"].Value = txtU_ZIP.EditValue;
                                cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_addr", MySqlDbType.VarChar));
                                cmd.Parameters["i_addr"].Value = txtU_ADDR.EditValue;
                                cmd.Parameters["i_addr"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_addr_detail", MySqlDbType.VarChar));
                                cmd.Parameters["i_addr_detail"].Value = txtU_ADDR_DETAIL.EditValue;
                                cmd.Parameters["i_addr_detail"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_cell_num", MySqlDbType.VarChar));
                                cmd.Parameters["i_cell_num"].Value = txtU_CELL_NUM.EditValue;
                                cmd.Parameters["i_cell_num"].Direction = ParameterDirection.Input; 


                                cmd.Parameters.Add(new MySqlParameter("i_is_stock_friend", MySqlDbType.VarChar));
                                cmd.Parameters["i_is_stock_friend"].Value = chkIS_STOCK_FRIEND.EditValue;
                                cmd.Parameters["i_is_stock_friend"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_u_chef_level", MySqlDbType.VarChar));
                                cmd.Parameters["i_u_chef_level"].Value = cmbU_CHEF_LEVEL.EditValue;
                                cmd.Parameters["i_u_chef_level"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_is_official", MySqlDbType.VarChar));
                                cmd.Parameters["i_is_official"].Value = ckis_official.EditValue;
                                cmd.Parameters["i_is_official"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_is_vvip", MySqlDbType.VarChar));
                                cmd.Parameters["i_is_vvip"].Value = cbis_vvip.EditValue;
                                cmd.Parameters["i_is_vvip"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_is_biz", MySqlDbType.VarChar));
                                cmd.Parameters["i_is_biz"].Value = chkIS_BIZ.EditValue;
                                cmd.Parameters["i_is_biz"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_bank", MySqlDbType.VarChar));
                                cmd.Parameters["i_bank"].Value = txtBank.EditValue;
                                cmd.Parameters["i_bank"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_acc_no", MySqlDbType.VarChar));
                                cmd.Parameters["i_acc_no"].Value = txtAccNo.EditValue;
                                cmd.Parameters["i_acc_no"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_account_holder", MySqlDbType.VarChar));
                                cmd.Parameters["i_account_holder"].Value = txtAccount_Holder.EditValue;
                                cmd.Parameters["i_account_holder"].Direction = ParameterDirection.Input;

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }

                        Cursor.Current = Cursors.WaitCursor;



                        //int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MM_MM05_SAVE_01"
                        //                                         , UserInfo.instance().UserId
                        //                                         , this.txtCOMPANYCD.EditValue
                        //                                         , Convert.ToInt64(this.txtIDX.EditValue)
                        //                                         , this.txtU_NICKNAME.EditValue
                        //                                         //, this.chkUNIONORNOT.Checked == true ? "Y" : "N"
                        //                                         );

                        //if (retVal > 0)
                        //{
                        //    //this.txtUSERID.EditValue = ds.Tables[0].Rows[0]["NEW_ID"];
                        //    MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                        //    Search();
                        //    //NewMode();
                        //}

                        //DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM05_SAVE_01"
                        //                                        , UserInfo.instance().UserId
                        //                                        , this.txtCOMPANYCD.EditValue
                        //                                        , Convert.ToInt64(this.txtIDX.EditValue)
                        //                                        , this.txtU_ID.EditValue
                        //                                        , this.txtU_NICKNAME.EditValue
                        //                                        , this.txtU_CELL_NUM.EditValue
                        //                                        , this.txtU_EMAIL.EditValue
                        //                                        , this.txtU_ZIP.Text
                        //                                        , this.txtU_ADDR.EditValue
                        //                                        , this.txtU_ADDR_DETAIL.EditValue
                        //                                        , Convert.ToInt16(this.cmbU_CHEF_LEVEL.EditValue)
                        //                                        , this.chkIS_AL_FRIEND.EditValue
                        //                                        , this.chkIS_STOCK_FRIEND.EditValue
                        //                                        , this.txtCHEF_U_NICKNAME.EditValue2
                        //                                        , this.txtCHEF_U_NICKNAME.Text.Replace("선택하세요","")
                        //                                        , this.txtVIP_RECO_NICKNAME.EditValue2
                        //                                        , this.txtVIP_RECO_NICKNAME.Text.Replace("선택하세요", "")
                        //                                        , this.chkIS_GR_MD.Checked == true ? "Y" : "N"
                        //                                        , this.chkIS_DORAMD.Checked == true ? "Y" : "N"
                        //                                        , this.chkIS_BIZ.Checked == true ? "Y" : "N"
                        //                                        );

                        DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM05_SAVE_02"
                                                                , this.txtUSER_ID.EditValue
                                                                , this.txtCOMPANYCD.EditValue
                                                                , Convert.ToInt64(this.txtIDX.EditValue)
                                                                , this.txtU_ID.EditValue
                                                                , this.txtU_NICKNAME.EditValue
                                                                , this.txtU_EMAIL.EditValue
                                                                , this.txtU_NAME.EditValue
                                                                , this.cmbU_CHEF_LEVEL.EditValue
                                                                , this.cmbDORAMD_TYPE.EditValue
                                                                , this.chkIS_SUPPORT_TEAM.EditValue
                                                                , this.txtU_CELL_NUM.EditValue
                                                                , this.txtU_ZIP.EditValue
                                                                , this.txtU_ADDR.EditValue
                                                                , this.txtU_ADDR_DETAIL.EditValue
                                                                );

                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["ERRC"].ToString() == "ERR")
                            {
                                MessageAgent.MessageShow(MessageType.Error, ds.Tables[0].Rows[0]["ERRM"].ToString());
                            }
                            else
                            {
                                MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                                IsAutoSearch = false;
                                Search();
                                Open1();
                                //NewMode();
                            }
                        }

                        Cursor.Current = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                        Cursor.Current = Cursors.Default;
                    }
                    
                }
            }
        }


        #endregion

        #region 삭제

        public override void Delete()
        {
            base.Delete();

            if (UserInfo.instance().UserId != "0000000024" && UserInfo.instance().UserId != "2019040001" && UserInfo.instance().UserId != "0000000012" && UserInfo.instance().UserId != "169.254.169.113")
            {
                MessageAgent.MessageShow(MessageType.Error, "처리권한이 없습니다!");
                IsAutoSearch = false;
                return;
            }

            if (string.IsNullOrEmpty(this.txtIDX.Text))
            {
                MessageAgent.MessageShow(MessageType.Error, "회원을 선택하신후에 처리하세요!");
                IsAutoSearch = false;
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MM_MM05_DELETE_01"
                                                             , UserInfo.instance().UserId
                                                             , this.txtCOMPANYCD.EditValue
                                                             , Convert.ToInt64(this.txtIDX.EditValue)
                                                             , this.txtU_ID.EditValue
                                                             );

                    if (retVal > 0)
                    {
                        //this.txtUSERID.EditValue = ds.Tables[0].Rows[0]["NEW_ID"];
                        MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                        Search();
                        //NewMode();
                    }
                }
                catch (Exception ex)
                {
                    //MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                finally
                {
                    MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                    NewMode();
                }

                //try
                //{
                //    Cursor.Current = Cursors.WaitCursor;

                //    int affectedRows = 0;
                //    using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                //    {
                //        sql.Query = "DELETE FROM domalife.member_master WHERE idx=@Idx";
                //        sql.addParam("@Idx", Convert.ToInt64(this.txtIDX.EditValue));
                //        affectedRows = sql.deleteQuery();
                //    }

                //    //using (MySQLConn sql2 = new MySQLConn(ConstantLib.BasicConn_Real))
                //    //{
                //    //    sql2.Query = "DELETE FROM domalife.F_VW_USER_INFOS WHERE system_key=@system_key";
                //    //    sql2.addParam("@system_key", this.txtU_ID.EditValue);
                //    //    affectedRows = sql2.deleteQuery();
                //    //}

                //    NewMode();
                //    Search();

                //    Cursor.Current = Cursors.Default;
                //}
                //catch (Exception ex)
                //{
                //    //MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                //}

                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                //{
                //    using (MySqlCommand cmd = new MySqlCommand("member_del", con))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Parameters.AddWithValue("@idx", txtU_NAME.EditValue);

                //        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                //        {
                //            DataTable ds = new DataTable();
                //            efwGridControl1.DataBind(ds);
                //            this.efwGridControl1.MyGridView.BestFitColumns();
                //        }
                //    }
                //}
            }
        }

        #endregion


        #region 이벤트

        private void BtnSch1_Click(object sender, EventArgs e)
        {
            //이름+생년월일 중복자 검색 
            //try
            //{
            //    Cursor.Current = Cursors.WaitCursor;

            //    _is_dupchk = true;

            //    DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM05_SELECT_02"
            //    , this.txtCOMPANYCD.Text
            //    , this.cmbQ1.EditValue
            //    , this.txtSearch.Text
            //    , this.cmbQ2.EditValue
            //    , this.cmbQ3.EditValue
            //    );

            //    efwGridControl1.DataBind(ds);
            //    this.efwGridControl1.MyGridView.BestFitColumns();
            //}
            //catch (Exception ex)
            //{
            //    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            //}
            //finally
            //{
            //    Cursor.Current = Cursors.Default;
            //}

            popup2 = new frmMemberInfo_Dup
            {
                COMPANYCD = txtCOMPANYCD.EditValue.ToString(),
                COMPANYNAME = txtCOMPANYNAME.EditValue.ToString()
            };
            popup2.FormClosed += popup2_FormClosed;
            PopUpBizAgent.Show(this, popup2);
        }


        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            //if (dr != null && dr["FLR"].ToString() != "0" && dr["FLR"].ToString() != "")
            //    this.xtraTabPage3.PageEnabled = true;
            //else
            //    this.xtraTabPage3.PageEnabled = false;

            if (dr != null && dr["u_chef_level_cd"].ToString() != "")
                this.cmbU_CHEF_LEVEL.EditValue = dr["u_chef_level_cd"].ToString();

            if (dr != null && dr["u_zip"].ToString() != "")
            {
                this.txtU_ZIP.EditValue2 = dr["u_zip"].ToString();
                this.txtU_ZIP.Text = dr["u_zip"].ToString();
                this.txtU_ADDR.Text = dr["u_addr"].ToString() + dr["u_addr_detail"].ToString();
            }

            if (dr != null && dr["chef_u_nickname"].ToString() != "")
            {
                this.txtCHEF_U_NICKNAME.EditValue2 = dr["chef_u_nickname"].ToString();
                this.txtCHEF_U_NICKNAME.Text = dr["chef_u_nickname"].ToString();
            }
            else
            {
                this.txtCHEF_U_NICKNAME.EditValue2 = null;
                this.txtCHEF_U_NICKNAME.Text = null;
            }

            if (dr != null && dr["vip_reco_nickname"].ToString() != "")
            {
                this.txtVIP_RECO_NICKNAME.EditValue2 = dr["vip_reco_nickname"].ToString();
                this.txtVIP_RECO_NICKNAME.Text = dr["vip_reco_nickname"].ToString();
            }
            else
            {
                this.txtVIP_RECO_NICKNAME.EditValue2 = null;
                this.txtVIP_RECO_NICKNAME.Text = null;
            }

            if (dr != null && dr["doramd_type"].ToString() != "")
                this.cmbDORAMD_TYPE.EditValue = dr["doramd_type"].ToString();

            if (dr != null && dr["is_vvip"].ToString() != "")
                this.cbis_vvip.EditValue = dr["is_vvip"].ToString();




            Open1();
        }

        private void GridView1_CustomDrawGroupPanel(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
           
        }

        private void GridView1_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
            //e.Appearance.DrawString(e.Cache, e.Info.DisplayText, e.Bounds);
            e.Appearance.ForeColor = Color.Red;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void TxtU_VIP_Click(object sender, EventArgs e)
        {
            // VIP회원 Help
            //frmMemberInfo FrmInfo = new frmMemberInfo() { ParentBtn = txtU_VIP };
            //FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
            //FrmInfo.COMPANYNAME = txtCOMPANYNAME.EditValue.ToString();
            //FrmInfo.MTYPE = "2"; 
            //FrmInfo.ShowDialog();


            popup = new frmMemberInfo
            {
                COMPANYCD = txtCOMPANYCD.EditValue.ToString(),
                COMPANYNAME = txtCOMPANYNAME.EditValue.ToString(),
                MTYPE = "2"
            };
            popup.FormClosed += popup_FormClosed1;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtVIP_RECO_NICKNAME.Text = popup.U_NAME;
                this.txtVIP_RECO_NICKNAME.EditValue2 = popup.USER_ID;
            }
            popup = null;
        }

        private void TxtU_PS_Click(object sender, EventArgs e)
        {
            // PS운영자회원 Help
            //frmMemberInfo FrmInfo = new frmMemberInfo() { ParentBtn = txtU_PS };
            //FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
            //FrmInfo.COMPANYNAME = txtCOMPANYNAME.EditValue.ToString();
            //FrmInfo.MTYPE = "3";
            //FrmInfo.ShowDialog();

            popup = new frmMemberInfo
            {
                COMPANYCD = txtCOMPANYCD.EditValue.ToString(),
                COMPANYNAME = txtCOMPANYNAME.EditValue.ToString(),
                MTYPE = "3"
            };
            popup.FormClosed += popup_FormClosed2;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed2;
            //if (popup.DialogResult == DialogResult.OK)
            //{
            //    this.txtU_PS.Text = popup.U_NAME;
            //    this.txtU_PS.EditValue2 = popup.USER_ID;
            //}
            popup = null;
        }

        private void TxtU_CHEF_Click(object sender, EventArgs e)
        {
            // G메니저회원 Help
            //frmMemberInfo FrmInfo = new frmMemberInfo() { ParentBtn = txtU_CHEF };
            //FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
            //FrmInfo.COMPANYNAME = txtCOMPANYNAME.EditValue.ToString();
            //FrmInfo.MTYPE = "4";
            //FrmInfo.ShowDialog();

            popup = new frmMemberInfo
            {
                COMPANYCD = txtCOMPANYCD.EditValue.ToString(),
                COMPANYNAME = txtCOMPANYNAME.EditValue.ToString(),
                MTYPE = "4"
            };
            popup.FormClosed += popup_FormClosed3;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed3(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed3;
            if (popup.DialogResult == DialogResult.OK)
            {
                //this.txtU_CHEF.Text = popup.U_NAME;
                //this.txtU_CHEF.EditValue2 = popup.USER_ID;
            }
            popup = null;
        }


        private void TxtU_RECOMMENDER_Click(object sender, EventArgs e)
        {
            // 추천회원 Help
            //frmMemberInfo FrmInfo = new frmMemberInfo() { ParentBtn = txtU_RECOMMENDER };
            //FrmInfo.COMPANYCD = txtCOMPANYCD.EditValue.ToString();
            //FrmInfo.COMPANYNAME = txtCOMPANYNAME.EditValue.ToString();
            //FrmInfo.MTYPE = "0";
            //FrmInfo.ShowDialog();

            popup = new frmMemberInfo
            {
                COMPANYCD = txtCOMPANYCD.EditValue.ToString(),
                COMPANYNAME = txtCOMPANYNAME.EditValue.ToString(),
                MTYPE = "0"
            };
            popup.FormClosed += popup_FormClosed4;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed4(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed4;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtCHEF_U_NICKNAME.Text = popup.U_NAME;
                this.txtCHEF_U_NICKNAME.EditValue2 = popup.USER_ID;
            }
            popup = null;
        }

        private void popup2_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup2.FormClosed -= popup2_FormClosed;
            if (popup2.DialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(popup2.U_NAME))
                {
                    this.cmbQ1.EditValue = "0";
                    this.cmbQ3.ItemIndex = 0;
                    this.cmbQ2.ItemIndex = 0;
                    this.dt1.EditValue = null;
                    this.dt2.EditValue = null;
                    this.txtSearch.EditValue = popup2.U_NAME;
                    Search();
                }
            }
            popup2 = null;
        }





        #endregion


        private void txtU_ZIP_Click(object sender, EventArgs e)
        {
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = txtU_ZIP, ParentAddr1 = txtU_ADDR, ParentAddr2 = txtU_ADDR_DETAIL };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
            
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void txtMember_query_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void txtLogin_Query_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView3.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void txtSearch_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
