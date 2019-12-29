
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

namespace YL_MA.BizFrm
{
    public partial class frmMA03_Pop02 : FrmPopUpBase
    {
        public string pSDate { get; set; }
        public string pEDate { get; set; }
        public frmMA03_Pop02()
        {
            InitializeComponent();
            
        }

        private void FrmGM04_Pop02_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            txtSDate.EditValue = pSDate;
            txtEDate.EditValue = pEDate;

            Open1();
        }
        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real)) 

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MA_MA03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtSDate.EditValue + "-01";

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = txtEDate.EditValue + "-31";

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

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            Open1();
        }
    }
}
