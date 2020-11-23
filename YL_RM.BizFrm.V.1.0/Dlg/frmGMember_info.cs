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

namespace YL_RM.BizFrm.Dlg
{

    public partial class frmGMember_info : FrmPopUpBase
    {
        public string GSHOP_ID
        { get; set; }

        public string U_ID
        { get; set; }

        public string U_NAME
        { get; set; }

        public string U_NICKNAME
        { get; set; }

        public string U_CELL_NUM
        { get; set; }

         public string U_ADDR
        { get; set; }

        public string GSHHOP_NAME
        { get; set; }

        public string COMPANYNAME
        { get; set; }

        public frmGMember_info()
        {
            InitializeComponent();
        }

        private void frmMember_info_Load(object sender, EventArgs e)
        {
            this.Text = "멀티샵 정보";

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
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM_GMEMBER_SELECT_01", con))
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

            this.GSHOP_ID = row["gshop_id"].ToString();
            this.U_NICKNAME = row["u_nickname"].ToString();
            this.U_ID = row["u_id"].ToString();
            this.U_CELL_NUM = row["tel_no"].ToString();
            this.U_ADDR = row["road_addr"].ToString();
            this.GSHHOP_NAME = row["gshop_name"].ToString();

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
