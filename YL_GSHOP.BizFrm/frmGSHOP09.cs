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
using YL_GSHOP.BizFrm.Dlg;

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP09 : FrmBase
    {
        frmMemberInfo popup;
        public EventHandler efwGridControl1_Click { get; private set; }
        public frmGSHOP09()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "GSHOP09";
            //폼명설정
            this.FrmName = "MD 워크샵 참가등록";
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
            this.IsPrint = true;
            this.IsExcel = true;

            dtS_DATE.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dtE_DATE.EditValue = DateTime.Now;

            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("u_name", txtU_NAME)
            , new ColumnControlSet("u_nickname", txtU_NICKNAME)
            , new ColumnControlSet("user_id", txtUSER_ID)
            , new ColumnControlSet("u_birthday", txtBIRTH)
            , new ColumnControlSet("u_gender", txtU_GENDER)
            , new ColumnControlSet("u_email", txtU_EMAIL)
            , new ColumnControlSet("reg_date", txtREG_DATE)
            , new ColumnControlSet("u_zip", txtU_ZIP)
            , new ColumnControlSet("u_addr", txtU_ADDR)
            , new ColumnControlSet("u_addr_detail", txtU_ADDR_DETAIL)
            , new ColumnControlSet("idx", txtIDX)
            , new ColumnControlSet("workshop_date", dtWORKSHOP_DATE)
            , new ColumnControlSet("start_time", dfSTART_TIME)
            , new ColumnControlSet("end_time", dfEND_TIME)
            , new ColumnControlSet("u_id", txtU_ID)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;




        }

        public override void Search()
        {
            Open1();
        }
        public void Open1()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP09_SELECT_01", con))
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
                Cursor.Current = Cursors.Default;
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUSER_ID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP09_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_workshop_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_workshop_date"].Value = dtWORKSHOP_DATE.EditValue;
                            cmd.Parameters["i_workshop_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_start_time", MySqlDbType.VarChar));
                            cmd.Parameters["i_start_time"].Value = dfSTART_TIME.EditValue;
                            cmd.Parameters["i_start_time"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_end_time", MySqlDbType.VarChar));
                            cmd.Parameters["i_end_time"].Value = dfEND_TIME.EditValue;
                            cmd.Parameters["i_end_time"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();


                            Open1();
                        }
                    }
                }
                
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        private void BtnMemberSch_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
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
                this.txtIDX.Text = popup.IDX;
                this.txtU_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtUSER_ID.EditValue = popup.USER_ID;
                this.txtBIRTH.EditValue = popup.BIRTH;
                this.txtU_GENDER.EditValue = popup.U_GENDER;
                this.txtU_CELL_NUM.EditValue = popup.U_CELL_NUM;
                this.txtU_EMAIL.EditValue = popup.U_EMAIL;
                this.txtLOGIN_DATE.EditValue = popup.LOGIN_DATE;
                this.txtREG_DATE.EditValue = popup.REG_DATE;
                this.txtU_ZIP.EditValue = popup.U_ZIP;
                this.txtU_ADDR.EditValue = popup.U_ADDR;
                this.txtU_ADDR_DETAIL.EditValue = popup.U_ADDR_DETAIL;
                this.txtU_ID.EditValue = popup.U_ID;

            }
            popup = null;
        }
    }
}
