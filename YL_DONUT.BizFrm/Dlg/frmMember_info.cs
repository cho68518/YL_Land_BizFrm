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

namespace YL_DONUT.BizFrm.Dlg
{

    public partial class frmMember_info : FrmPopUpBase
    {

        public string COMPANYCD
        { get; set; }

        public string COMPANYNAME
        { get; set; }

        public string IDX
        { get; set; }

        public string USER_ID
        { get; set; }

        public string U_ID
        { get; set; }

        public string U_NAME
        { get; set; }

        public string U_NICKNAME
        { get; set; }

        public string BIRTH
        { get; set; }

        public string U_GENDER
        { get; set; }

        public string U_CELL_NUM
        { get; set; }

        public string U_EMAIL
        { get; set; }

        public string LOGIN_DATE
        { get; set; }

        public string REG_DATE
        { get; set; }

        public string U_ZIP
        { get; set; }

        public string U_ADDR
        { get; set; }

        public string U_ADDR_DETAIL
        { get; set; }

        public string U_CHEF_LEVEL
        { get; set; }

        public string IS_STOCK_FRIEND
        { get; set; }

        public string MEMBER_TYPE
        { get; set; }


        public frmMember_info()
        {
            InitializeComponent();
        }

        private void frmMember_info_Load(object sender, EventArgs e)
        {
            this.Text = " 회원정보 (" + COMPANYNAME + ")";

            //txtCOMPANYNAME.Text = COMPANYNAME;
            gridView1.IndicatorWidth = 24;
        }


        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN_MEMBER_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;



                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtSearch_Name.EditValue;


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

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            Open1();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void efwGridControl1_DoubleClick(object sender, EventArgs e)
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

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        private void txtSearch_Name_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Open1();
        }
    }
}
