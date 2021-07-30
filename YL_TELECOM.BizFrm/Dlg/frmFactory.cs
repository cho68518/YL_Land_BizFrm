using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_COMM.BizFrm;
using YL_TELECOM.BizFrm.Dlg;
//using YL_COMM.BizFrm;

namespace YL_TELECOM.BizFrm.Dlg
{
    public partial class frmFactory : DevExpress.XtraEditors.XtraForm
    {
        public efwButtonEdit Factory
        {
            get;
            set;
        }

        public efwTextEdit Factory_NM
        {
            get;
            set;
        }
        public frmFactory()
        {
            InitializeComponent();
        }

        private void frmFactory_Load(object sender, EventArgs e)
        {
            rbUse_yn.EditValue = "T";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Open1();
        }

        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_FACTORY_INFO_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txt_search.EditValue; 


                        cmd.Parameters.Add("i_Use_yn", MySqlDbType.VarChar, 1);
                        cmd.Parameters[1].Value = rbUse_yn.EditValue; 

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //this.efwGridControl1.MyGridView.BestFitColumns();

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
            if (Factory != null)
            {
                DataRow dr = this.efwGridControl1.GetSelectedRow(0);
                this.Factory.Text = dr["idx_fac"].ToString();
                this.Factory_NM.Text = dr["u_name"].ToString(); ;
                this.Close();
            }
        }
    }
}
