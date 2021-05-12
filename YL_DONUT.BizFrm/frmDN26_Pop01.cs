using Easy.Framework.Common;
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

namespace YL_DONUT.BizFrm
{

    public partial class frmDN26_Pop01 : FrmPopUpBase
    {
        public string pU_NICKNAME { get; set; }
        public string pU_ID { get; set; }
        public string pMD_U_ID { get; set; }
        public string pMD_U_NICKNAME { get; set; }
        public frmDN26_Pop01()
        {
            InitializeComponent();
        }

        private void frmDN26_Pop01_Load(object sender, EventArgs e)
        {
            txtU_NickName.EditValue = pU_NICKNAME;
            AutoOpen();
        }
        private void AutoOpen()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN26_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

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

        private void efwGridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);


            this.pMD_U_ID = row["u_id"].ToString();
            this.pMD_U_NICKNAME = row["u_nickname"].ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
