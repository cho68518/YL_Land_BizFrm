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
    public partial class frmMM19_Pop02 : FrmPopUpBase
    {
        public String sFildQuery { get; set; }
        public int nIdx { get; set; }
        public String sName { get; set; }
        public String sHp_No { get; set; }
        public String sShop_Name { get; set; }
        public String sPost_No { get; set; }
        public String sAddr { get; set; }
        public String sAddr_Detail { get; set; }
        public String sAdvice_Type { get; set; }
        public String sCoid_Name { get; set; }
        public String sMd_Name { get; set; }
        public String sPic_Send { get; set; }
        public String sLast_Coid_Name { get; set; }


        public frmMM19_Pop02()
        {
            InitializeComponent();
        }

        private void frmMM19_Pop02_Load(object sender, EventArgs e)
        {
            this.Text = " 상담 회원정보 ";
            txtFildQuery.EditValue = sFildQuery;

            //txtCOMPANYNAME.Text = COMPANYNAME;
            gridView1.IndicatorWidth = 48;

            Open1();
        }


        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM19_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_FildQuery", MySqlDbType.VarChar, 500);
                        cmd.Parameters[0].Value = txtFildQuery.EditValue;

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

            //public int nIdx { get; set; }
            //public String sName { get; set; }
            //public String sHp_No { get; set; }
            //public String sShop_Name { get; set; }
            //public String sPost_No { get; set; }
            //public String sAddr { get; set; }
            //public String sAddr_Detail { get; set; }
            //public String sAdvice_Type { get; set; }
            //public String sCoid_Name { get; set; }
            //public String sMd_Name { get; set; }
            //public String sPic_Send { get; set; }
            //public String sLast_Coid_Name { get; set; }

            this.nIdx = Convert.ToInt32(row["idx"].ToString());
            this.sName = row["name"].ToString();
            this.sHp_No = row["hp_no"].ToString();
            this.sShop_Name = row["shop_name"].ToString();
            this.sPost_No = row["post_no"].ToString();
            this.sAddr = row["addr"].ToString();
            this.sAddr_Detail = row["addr_detail"].ToString();
            this.sAdvice_Type = row["advice_type"].ToString();
            this.sMd_Name = row["md_name"].ToString();
            this.sPic_Send = row["pic_send"].ToString();
            this.sLast_Coid_Name = row["Last_coid_name"].ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void efwGridControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            //public int nIdx { get; set; }
            //public String sName { get; set; }
            //public String sHp_No { get; set; }
            //public String sShop_Name { get; set; }
            //public String sPost_No { get; set; }
            //public String sAddr { get; set; }
            //public String sAddr_Detail { get; set; }
            //public String sAdvice_Type { get; set; }
            //public String sCoid_Name { get; set; }
            //public String sMd_Name { get; set; }
            //public String sPic_Send { get; set; }
            //public String sLast_Coid_Name { get; set; }

            this.nIdx = Convert.ToInt32(row["idx"].ToString());
            this.sName = row["name"].ToString();
            this.sHp_No = row["hp_no"].ToString();
            this.sShop_Name = row["shop_name"].ToString();
            this.sPost_No = row["post_no"].ToString();
            this.sAddr = row["addr"].ToString();
            this.sAddr_Detail = row["addr_detail"].ToString();
            this.sAdvice_Type = row["advice_type"].ToString();
            this.sMd_Name = row["md_name"].ToString();
            this.sPic_Send = row["pic_send"].ToString();
            this.sLast_Coid_Name = row["Last_coid_name"].ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
