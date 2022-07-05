#region "frmGSHOP02 설명"
//===================================================================================================
//■Program Name  : frmGSHOP04
//■Description   : G 멀티샵 구분별  현황
//■Author        : 송호철
//■Date          : 2019.06.12
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.06.12][송호철] Base
//[2] [2019.06.12][송호철] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

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
    public partial class frmGSHOP04 : FrmBase
    {
        frmMemberInfo popup;
        public frmGSHOP04()
        {
            InitializeComponent();

             //단축코드 설정 
            this.QCode = "GSHOP04";
            //폼명설정
            this.FrmName = "G멀티샵 현황";

                 


        }

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


            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            cmbQ1.EditValue = "0";
            gridView1.OptionsView.ShowFooter = true;
            chkIs_Best.EditValue = "N";
      //      cmbTAREA1.EditValue = "00";
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("gshop_id", txtGSHOP_ID)
                    , new ColumnControlSet("ceo_name", txtCEO_NAME)
                    , new ColumnControlSet("gshop_name", txtGSHOP_NAME)
                    , new ColumnControlSet("u_nicknam", txtU_NICKNAME)
                    , new ColumnControlSet("register_no", txtREGISTER_NO)
                    , new ColumnControlSet("email", txtEMAIL)
                    , new ColumnControlSet("tel_no", txtTEL_NO)
                    , new ColumnControlSet("road_addr", txtADDRESS1)
                    , new ColumnControlSet("road_addr2", txtADDRESS2)
            //        , new ColumnControlSet("sido_code", cmbTAREA1)
            //        , new ColumnControlSet("gugun_code", cmbSAREA1)
                    , new ColumnControlSet("recomm_nicknm", txtRECOMM_NM)
                    , new ColumnControlSet("recomm_u_id", txtRECOMM_U_ID)
                    , new ColumnControlSet("hp_no", txtHP_NO)
                    , new ColumnControlSet("post_no", btnPOST_NO)
                    , new ColumnControlSet("md_u_id", txtMD_U_ID)
                    , new ColumnControlSet("u_id", txtU_ID)
                    , new ColumnControlSet("md_nicknm", txtMD_NICKNAME)
                    , new ColumnControlSet("is_best", chkIs_Best)
                    , new ColumnControlSet("team_nickname", txtTEAM_NICKNAME)
                    , new ColumnControlSet("team_leader", txtTEAM_LEADER)
                    , new ColumnControlSet("experience_shop", chkExperience_Shop)
                    , new ColumnControlSet("area", txtArea)
                   ); ;

            this.efwGridControl1.Click += efwGridControl1_Click;
            rbCom.EditValue = "T";
            SetCmb();
        }
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            btnPOST_NO.EditValue = "";

            if (dr != null && dr["sido_code"].ToString() != "")
            {
                this.cmbTAREA1.EditValue = dr["sido_code"].ToString();
                this.cmbSAREA1.EditValue = dr["gugun_code"].ToString();
            }
            if (dr != null && dr["post_no"].ToString() != "")
            {
                this.btnPOST_NO.EditValue2 = dr["post_no"].ToString();
                this.btnPOST_NO.Text = dr["post_no"].ToString();


            }
            Open1();
        }

        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT ifnull(sido,'00') as DCODE ,ifnull(sido_nm,'전국') as DNAME  FROM domabiz.place_master GROUP BY sido, sido_nm ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbTAREA1, codeArray);


            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00043'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbOrderDate, codeArray);
            }
            cmbOrderDate.EditValue = "1";

        }

        private void CmbTAREA1_EditValueChanged(object sender, EventArgs e)
        {
            string sCSIDO = string.Empty;

            //if (cmbTAREA1.EditValue.ToString() == null && cmbTAREA1.EditValue.ToString() == "")
            //if (cmbTAREA1.EditValue.ToString() == "")
            //{
            //    sCSIDO = "00";
            //}
            //else
            //{
                sCSIDO = cmbTAREA1.EditValue.ToString();
            //}



            if (string.IsNullOrEmpty(this.cmbTAREA1.EditValue.ToString()))
            {
                return;
            }


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                //con.Query = "select DCODE, DNAME from ( select gugun as DCODE, concat(gugun_nm, IFNULL((select '   ( 선점 )' from domabiz.md_place where sido = a.sido and gugun = a.gugun), '')) as DNAME from domabiz.place_master a  where sido = " + sCSIDO + "  ) AS t1 group by dcode, dname ";
                con.Query = "select DCODE, DNAME from " +
                            " ( select ifnull(gugun,'00') as DCODE, " +
                            "          concat(gugun_nm, IFNULL((select '   ( 선점 )' from domabiz.md_place where sido = a.sido and gugun = a.gugun), '')) as DNAME " +
                            "     from domabiz.place_master a  " +
                            "    where sido = " + sCSIDO + "  ) AS t1 " +
                            "group by dcode, dname ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();
                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbSAREA1, codeArray);

            }
        }


        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
            cmbTAREA1.EditValue = "00";
            cmbSAREA1.EditValue = "00";
        }

        public override void Search()
        {
            try
            {
                cmbTAREA1.EditValue = "00";
                cmbSAREA1.EditValue = "00";
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP04_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtSearch.EditValue;

                        cmd.Parameters.Add("i_road_addr", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtROAD_ADDR.EditValue;

                        cmd.Parameters.Add("i_Com", MySqlDbType.VarChar, 1);
                        cmd.Parameters[5].Value = rbCom.EditValue;

                        cmd.Parameters.Add("i_order_date", MySqlDbType.VarChar, 1);
                        cmd.Parameters[6].Value = cmbOrderDate.EditValue;

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
        #endregion

        public void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP04_SELECT_02", con))
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
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void TxtREC_U_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void txtROAD_ADDR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtRECOMM_NM.EditValue = popup.U_NICKNAME;
                this.txtRECOMM_U_ID.EditValue = popup.U_ID;
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

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "MD",
            };
            popup.FormClosed += popup_FormClosed2;
            PopUpBizAgent.Show(this, popup);
        }



        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed2;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtMD_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtMD_U_ID.EditValue = popup.U_ID;
            }
            popup = null;
        }

        private void BtnMemberSch_Click(object sender, EventArgs e)
        {

        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void EfwSimpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGSHOP_ID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "담당 MD를 제외 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP04_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGSHOP_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ceo_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_ceo_name"].Value = txtCEO_NAME.EditValue;
                            cmd.Parameters["i_ceo_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_gshop_name"].Value = txtGSHOP_NAME.EditValue;
                            cmd.Parameters["i_gshop_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_nickname", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_nickname"].Value = txtU_NICKNAME.EditValue;
                            cmd.Parameters["i_u_nickname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_register_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_register_no"].Value = txtREGISTER_NO.EditValue;
                            cmd.Parameters["i_register_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_email", MySqlDbType.VarChar));
                            cmd.Parameters["i_email"].Value = txtEMAIL.EditValue;
                            cmd.Parameters["i_email"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel_no"].Value = txtTEL_NO.EditValue;
                            cmd.Parameters["i_tel_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hp_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_hp_no"].Value = txtHP_NO.EditValue;
                            cmd.Parameters["i_hp_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = btnPOST_NO.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_jibun_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_jibun_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_jibun_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_road_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sido", MySqlDbType.VarChar));
                            cmd.Parameters["i_sido"].Value = cmbTAREA1.EditValue;
                            cmd.Parameters["i_sido"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gugun", MySqlDbType.VarChar));
                            cmd.Parameters["i_gugun"].Value = cmbSAREA1.EditValue;
                            cmd.Parameters["i_gugun"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recomm_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_recomm_nm"].Value = txtRECOMM_NM.EditValue;
                            cmd.Parameters["i_recomm_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recomm_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_recomm_u_id"].Value = txtRECOMM_U_ID.EditValue;
                            cmd.Parameters["i_recomm_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_u_id"].Value = "";
                            cmd.Parameters["i_md_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr2", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr2"].Value = txtADDRESS2.EditValue;
                            cmd.Parameters["i_road_addr2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_best", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_best"].Value = chkIs_Best.EditValue;
                            cmd.Parameters["i_is_best"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_team_leader", MySqlDbType.VarChar));
                            cmd.Parameters["i_team_leader"].Value = txtTEAM_LEADER.EditValue;
                            cmd.Parameters["i_team_leader"].Direction = ParameterDirection.Input;

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
                txtMD_NICKNAME.Text = "";
                Search();
            }
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGSHOP_ID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }

            if (chkIs_Best.EditValue.ToString() == "Y")
            {
                if (txtTEAM_NICKNAME.EditValue.ToString() == "")
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 담당 팀장을 선택하세요 ");
                    return;
                }
            }
            else
            {
                string sGshop_name;
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    sql.Query = "select gshop_name as nCount FROM  domabiz.gshop_master where md_u_id = '" + txtU_ID.EditValue + "' limit 1 ";
                    DataSet ds = sql.selectQueryDataSet();

                    sGshop_name = sql.selectQueryForSingleValue();
                }
                //if (sGshop_name.Length > 2)
                //{
                //    MessageAgent.MessageShow(MessageType.Warning, sGshop_name + "에서 배스트 샵으로 등록되어 제외 할수가 없습니다");
                //    return;
                //}
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP04_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGSHOP_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ceo_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_ceo_name"].Value = txtCEO_NAME.EditValue;
                            cmd.Parameters["i_ceo_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_gshop_name"].Value = txtGSHOP_NAME.EditValue;
                            cmd.Parameters["i_gshop_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_nickname", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_nickname"].Value = txtU_NICKNAME.EditValue;
                            cmd.Parameters["i_u_nickname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_register_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_register_no"].Value = txtREGISTER_NO.EditValue;
                            cmd.Parameters["i_register_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_email", MySqlDbType.VarChar));
                            cmd.Parameters["i_email"].Value = txtEMAIL.EditValue;
                            cmd.Parameters["i_email"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel_no"].Value = txtTEL_NO.EditValue;
                            cmd.Parameters["i_tel_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hp_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_hp_no"].Value = txtHP_NO.EditValue;
                            cmd.Parameters["i_hp_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = btnPOST_NO.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_jibun_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_jibun_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_jibun_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_road_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sido", MySqlDbType.VarChar));
                            cmd.Parameters["i_sido"].Value = cmbTAREA1.EditValue;
                            cmd.Parameters["i_sido"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gugun", MySqlDbType.VarChar));
                            cmd.Parameters["i_gugun"].Value = cmbSAREA1.EditValue;
                            cmd.Parameters["i_gugun"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recomm_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_recomm_nm"].Value = txtRECOMM_NM.EditValue;
                            cmd.Parameters["i_recomm_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recomm_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_recomm_u_id"].Value = txtRECOMM_U_ID.EditValue;
                            cmd.Parameters["i_recomm_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_u_id"].Value = txtMD_U_ID.EditValue;
                            cmd.Parameters["i_md_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr2", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr2"].Value = txtADDRESS2.EditValue;
                            cmd.Parameters["i_road_addr2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_best", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_best"].Value = chkIs_Best.EditValue;
                            cmd.Parameters["i_is_best"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_team_leader", MySqlDbType.VarChar));
                            cmd.Parameters["i_team_leader"].Value = txtTEAM_LEADER.EditValue;
                            cmd.Parameters["i_team_leader"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_area", MySqlDbType.VarChar));
                            cmd.Parameters["i_area"].Value = txtArea.EditValue;
                            cmd.Parameters["i_area"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            if (UserInfo.instance().UserId != "0000000029" && UserInfo.instance().UserId != "0000000012" && UserInfo.instance().UserId != "0000000013" && UserInfo.instance().UserId != "0000000024")
            {
                MessageAgent.MessageShow(MessageType.Warning, "삭제할 권한이 없습니다 IT 사업부에 문의하세요!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "(주의) 삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP04_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGSHOP_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGSHOP_ID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }



            if (MessageAgent.MessageShow(MessageType.Confirm, "담당 팀장을 제외 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP04_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGSHOP_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ceo_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_ceo_name"].Value = txtCEO_NAME.EditValue;
                            cmd.Parameters["i_ceo_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_gshop_name"].Value = txtGSHOP_NAME.EditValue;
                            cmd.Parameters["i_gshop_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_nickname", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_nickname"].Value = txtU_NICKNAME.EditValue;
                            cmd.Parameters["i_u_nickname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_register_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_register_no"].Value = txtREGISTER_NO.EditValue;
                            cmd.Parameters["i_register_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_email", MySqlDbType.VarChar));
                            cmd.Parameters["i_email"].Value = txtEMAIL.EditValue;
                            cmd.Parameters["i_email"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel_no"].Value = txtTEL_NO.EditValue;
                            cmd.Parameters["i_tel_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hp_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_hp_no"].Value = txtHP_NO.EditValue;
                            cmd.Parameters["i_hp_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = btnPOST_NO.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_jibun_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_jibun_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_jibun_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_road_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sido", MySqlDbType.VarChar));
                            cmd.Parameters["i_sido"].Value = cmbTAREA1.EditValue;
                            cmd.Parameters["i_sido"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gugun", MySqlDbType.VarChar));
                            cmd.Parameters["i_gugun"].Value = cmbSAREA1.EditValue;
                            cmd.Parameters["i_gugun"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recomm_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_recomm_nm"].Value = txtRECOMM_NM.EditValue;
                            cmd.Parameters["i_recomm_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recomm_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_recomm_u_id"].Value = txtRECOMM_U_ID.EditValue;
                            cmd.Parameters["i_recomm_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_u_id"].Value = "";
                            cmd.Parameters["i_md_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr2", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr2"].Value = txtADDRESS2.EditValue;
                            cmd.Parameters["i_road_addr2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_best", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_best"].Value = chkIs_Best.EditValue;
                            cmd.Parameters["i_is_best"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_team_leader", MySqlDbType.VarChar));
                            cmd.Parameters["i_team_leader"].Value = null;
                            cmd.Parameters["i_team_leader"].Direction = ParameterDirection.Input;

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
                txtTEAM_NICKNAME.Text = "";
                Search();
            }
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if ( chkIs_Best.EditValue.ToString() == "N" )
            {
                MessageAgent.MessageShow(MessageType.Warning, " 배스트샵 구분을 확인하세요 ");
                return;
            }

            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "MD",
            };
            popup.FormClosed += popup_FormClosed3;
            PopUpBizAgent.Show(this, popup);
        }
        private void popup_FormClosed3(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed3;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtTEAM_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtTEAM_LEADER.EditValue = popup.U_ID;
            }
            popup = null;
        }

        private void chkIs_Best_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (chkIs_Best.EditValue.ToString() == "N")
                {
                    txtTEAM_NICKNAME.Text = "";
                    txtTEAM_LEADER.Text = "";
                    return;
                }
            }
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
            };
            popup.FormClosed += popup_FormClosed4;
            PopUpBizAgent.Show(this, popup);

        }

        private void popup_FormClosed4(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed4;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtCEO_NAME.EditValue = popup.U_NAME;
                this.txtU_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtGSHOP_NAME.EditValue = popup.U_NICKNAME;
                this.txtEMAIL.EditValue = popup.U_EMAIL;
                this.txtHP_NO.EditValue = popup.U_CELL_NUM;
                this.btnPOST_NO.EditValue = popup.U_ZIP;
                this.txtADDRESS1.EditValue = popup.U_ADDR;
                this.txtADDRESS2.EditValue = popup.U_ADDR_DETAIL;
                this.txtU_ID.EditValue = popup.U_ID;
            }
            popup = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtU_ID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회원을 선택하세요!");
                return;
            }

            if (chkIs_Best.EditValue.ToString() == "Y")
            {
                if (txtTEAM_NICKNAME.EditValue.ToString() == "")
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 담당 팀장을 선택하세요 ");
                    return;
                }
            }
            else
            {
                string sGshop_name;
                using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    sql.Query = "select gshop_name as nCount FROM  domabiz.gshop_master where md_u_id = '" + txtU_ID.EditValue + "' limit 1 ";
                    DataSet ds = sql.selectQueryDataSet();

                    sGshop_name = sql.selectQueryForSingleValue();
                }
                if (sGshop_name.Length > 2)
                {
                    MessageAgent.MessageShow(MessageType.Warning, sGshop_name + "에서 배스트 샵으로 등록되어 제외 할수가 없습니다");
                    return;
                }
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "체험샵을 등록 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP04_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGSHOP_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ceo_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_ceo_name"].Value = txtCEO_NAME.EditValue;
                            cmd.Parameters["i_ceo_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_gshop_name"].Value = txtGSHOP_NAME.EditValue;
                            cmd.Parameters["i_gshop_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_nickname", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_nickname"].Value = txtU_NICKNAME.EditValue;
                            cmd.Parameters["i_u_nickname"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_register_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_register_no"].Value = txtREGISTER_NO.EditValue;
                            cmd.Parameters["i_register_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_email", MySqlDbType.VarChar));
                            cmd.Parameters["i_email"].Value = txtEMAIL.EditValue;
                            cmd.Parameters["i_email"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel_no"].Value = txtTEL_NO.EditValue;
                            cmd.Parameters["i_tel_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hp_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_hp_no"].Value = txtHP_NO.EditValue;
                            cmd.Parameters["i_hp_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = btnPOST_NO.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_jibun_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_jibun_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_jibun_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_road_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sido", MySqlDbType.VarChar));
                            cmd.Parameters["i_sido"].Value = cmbTAREA1.EditValue;
                            cmd.Parameters["i_sido"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gugun", MySqlDbType.VarChar));
                            cmd.Parameters["i_gugun"].Value = cmbSAREA1.EditValue;
                            cmd.Parameters["i_gugun"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recomm_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_recomm_nm"].Value = txtRECOMM_NM.EditValue;
                            cmd.Parameters["i_recomm_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recomm_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_recomm_u_id"].Value = txtRECOMM_U_ID.EditValue;
                            cmd.Parameters["i_recomm_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_u_id"].Value = txtMD_U_ID.EditValue;
                            cmd.Parameters["i_md_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr2", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr2"].Value = txtADDRESS2.EditValue;
                            cmd.Parameters["i_road_addr2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_best", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_best"].Value = chkIs_Best.EditValue;
                            cmd.Parameters["i_is_best"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_team_leader", MySqlDbType.VarChar));
                            cmd.Parameters["i_team_leader"].Value = txtTEAM_LEADER.EditValue;
                            cmd.Parameters["i_team_leader"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }
    }
}
