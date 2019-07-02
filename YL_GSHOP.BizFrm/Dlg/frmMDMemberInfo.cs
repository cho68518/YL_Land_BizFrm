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

namespace YL_GSHOP.BizFrm.Dlg
{
    public partial class frmMDMemberInfo : FrmPopUpBase
    {
        #region Propertys

        public string COMPANYCD
        {
            get;
            set;
        }

        public string COMPANYNAME
        {
            get;
            set;
        }
        public string IDX
        {
            get;
            set;
        }


        public string USER_ID
        {
            get;
            set;
        }
        public string U_ID
        {
            get;
            set;
        }
        public string U_NAME
        {
            get;
            set;
        }

        public efwButtonEdit ParentBtn
        {
            get;
            set;
        }

        public string U_NICKNAME
        {
            get;
            set;
        }

        public string BIRTH
        {
            get;
            set;
        }
        public string U_GENDER
        {
            get;
            set;
        }
        public string U_CELL_NUM
        {
            get;
            set;
        }
        public string U_EMAIL
        {
            get;
            set;
        }
        public string LOGIN_DATE
        {
            get;
            set;
        }
        public string REG_DATE
        {
            get;
            set;
        }
        public string U_ZIP
        {
            get;
            set;
        }
        public string U_ADDR
        {
            get;
            set;
        }
        public string U_ADDR_DETAIL
        {
            get;
            set;
        }
        public string U_CHEF_LEVEL
        {
            get;
            set;
        }
        public string IS_STOCK_FRIEND
        {
            get;
            set;
        }

        #endregion

        #region Fields


        #endregion
        public frmMDMemberInfo()
        {
            InitializeComponent();
        }

        private void FrmMDMemberInfo_Load(object sender, EventArgs e)
        {
            this.Text = " MD 회원정보 (" + COMPANYNAME + ")";

            //txtCOMPANYNAME.Text = COMPANYNAME;
            gridView1.IndicatorWidth = 24;

            SetCmb();
            cmbSearch_Type.EditValue = "0";
        }

        private void SetCmb()
        {
            try
            {

                //대분류
                string strQueruy3 = @"SELECT
                                        CODE    DCODE
                                       ,CODE_NM DNAME
                                     FROM ETCCODE
                                    WHERE GRP_CODE = 'MEMBERQ_GBN3' and CODE in ('2','3')
                                    ORDER BY GRP_CODE ASC";
                CodeAgent.SetLegacyCode(cmbMember_Type, strQueruy3);

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
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP06_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbSearch_Type.EditValue;

                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtSearch_Name.EditValue;

                        cmd.Parameters.Add("i_member_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = cmbMember_Type.EditValue;

                        //Console.WriteLine(" i_Search_type           ---> [" + cmd.Parameters[0].Value + "]");
                        //Console.WriteLine(" i_search_Name           ---> [" + cmd.Parameters[1].Value + "]");
                        //Console.WriteLine(" i_member_type           ---> [" + cmd.Parameters[2].Value + "]");

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
        private void EfwGridControl1_DoubleClick_1(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            //this.ParentBtn.Text = row["u_name"].ToString();
            //this.ParentBtn.EditValue2 = row["user_id"].ToString();

            this.USER_ID = row["user_id"].ToString();
            this.U_NAME = row["u_name"].ToString();
            this.IDX = row["idx"].ToString();

            this.U_NICKNAME = row["u_nickname"].ToString();
            this.U_ID = row["u_id"].ToString();
            this.BIRTH = row["u_birthday"].ToString();
            this.U_GENDER = row["u_gender"].ToString();
            this.U_CELL_NUM = row["u_cell_num"].ToString();
            this.U_EMAIL = row["u_email"].ToString();
            this.LOGIN_DATE = row["login_date"].ToString();
            this.REG_DATE = row["reg_date"].ToString();
            this.U_ZIP = row["u_zip"].ToString();
            this.U_ADDR = row["u_addr"].ToString();
            this.U_ADDR_DETAIL = row["u_addr_detail"].ToString();
            this.U_CHEF_LEVEL = row["u_chef_level"].ToString();
            this.IS_STOCK_FRIEND = row["is_stock_friend"].ToString();


            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void BtnSearch1_Click(object sender, EventArgs e)
        {
            Open1();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Open1();
        }

        private void GridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = e.RowHandle.ToString();
        }

        public static implicit operator frmMDMemberInfo(frmMemberInfo v)
        {
            throw new NotImplementedException();
        }
    }
}
