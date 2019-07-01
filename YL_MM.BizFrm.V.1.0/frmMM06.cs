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

namespace YL_MM.BizFrm
{
    public partial class frmMM06 : FrmBase
    {
        public frmMM06()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MM06";
            //폼명설정
            this.FrmName = "회원정보(도넛라이프)";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_gr_md");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_doramd");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_biz");

            this.efwGridControl1.BindControlSet(
                       new ColumnControlSet("u_name", txtU_NAME)
                     , new ColumnControlSet("user_id", txtUSER_ID)
                     , new ColumnControlSet("u_id", txtU_ID)
                     , new ColumnControlSet("u_nickname", txtU_NICKNAME)
                     , new ColumnControlSet("u_gender", txtU_GENDER)
                     , new ColumnControlSet("u_birthday", txtBIRTH)
                     , new ColumnControlSet("u_email", txtU_EMAIL)
                     , new ColumnControlSet("u_cell_num", txtU_CELL_NUM)
                     , new ColumnControlSet("reg_date", txtREG_DATE)
                     , new ColumnControlSet("login_date", txtLOGIN_DATE)
                     , new ColumnControlSet("u_zip", txtU_ZIP)
                     , new ColumnControlSet("u_addr", txtU_ADDR)
                     , new ColumnControlSet("u_addr_detail", txtU_ADDR_DETAIL)
                     , new ColumnControlSet("u_chef_level_cd", cmbU_CHEF_LEVEL)
                     , new ColumnControlSet("is_al_friend_yn", chkIS_AL_FRIEND)
                     , new ColumnControlSet("is_stock_friend_yn", chkIS_STOCK_FRIEND)
                     , new ColumnControlSet("idx", txtIDX)
                     , new ColumnControlSet("is_gr_md", chkIS_GR_MD)
                     , new ColumnControlSet("is_doramd", chkIS_DORAMD)
                     , new ColumnControlSet("is_biz", chkIS_BIZ)
                     );

            this.efwGridControl1.Click += efwGridControl1_Click;

            New();
        }

        #endregion

        public override void NewMode()
        {
            base.NewMode();

            New();
        }

        private void New()
        {
            Eraser.Clear(this, "CLR1");
            cmbQ1.EditValue = "0";
            cmbQ3.EditValue = "%";
            txtSearch.Focus();
        }

        #region 조회

        public override void Search()
        {
            //base.Search();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string s1 = string.Empty;
                string s2 = string.Empty;

                if (this.cmbQ1.EditValue == null)
                    s1 = "";
                else
                    s1 = this.cmbQ1.EditValue.ToString().Replace("%", "");

                if (this.cmbQ3.EditValue == null)
                    s2 = "";
                else
                    s2 = this.cmbQ3.EditValue.ToString().Replace("%", "");

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_MM_MM06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_companycd", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = this.txtCOMPANYCD.EditValue;

                        cmd.Parameters.Add("i_sch1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = s1;

                        cmd.Parameters.Add("i_schtxt1", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = this.txtSearch.EditValue;

                        cmd.Parameters.Add("i_sch2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = s2;

                        cmd.Parameters.Add("i_date1", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = this.dt1.EditValue3;

                        cmd.Parameters.Add("i_date2", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = this.dt2.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            efwGridControl1.DataBind(dt);
                            this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
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
            }

            if (dr != null && dr["rec_u_id"].ToString() != "")
            {
                this.txtU_RECOMMENDER.EditValue2 = dr["rec_u_id"].ToString();
                this.txtU_RECOMMENDER.Text = dr["rec_u_name"].ToString();
            }
            else
            {
                this.txtU_RECOMMENDER.EditValue2 = null;
                this.txtU_RECOMMENDER.Text = null;
            }

            if (dr != null && dr["vip_u_id"].ToString() != "")
            {
                this.txtU_VIP.EditValue2 = dr["vip_u_id"].ToString();
                this.txtU_VIP.Text = dr["vip_u_name"].ToString();
            }
            else
            {
                this.txtU_VIP.EditValue2 = null;
                this.txtU_VIP.Text = null;
            }

            if (dr != null && dr["ps_u_id"].ToString() != "")
            {
                this.txtU_PS.EditValue2 = dr["ps_u_id"].ToString();
                this.txtU_PS.Text = dr["ps_u_name"].ToString();
            }
            else
            {
                this.txtU_PS.EditValue2 = null;
                this.txtU_PS.Text = null;
            }

            if (dr != null && dr["chef_u_id"].ToString() != "")
            {
                this.txtU_CHEF.EditValue2 = dr["chef_u_id"].ToString();
                this.txtU_CHEF.Text = dr["chef_u_name"].ToString();
            }
            else
            {
                this.txtU_CHEF.EditValue2 = null;
                this.txtU_CHEF.Text = null;
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
