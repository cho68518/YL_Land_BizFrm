using Easy.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Easy.Framework.WinForm.Control;

namespace YL_GM.BizFrm
{
    public partial class frmGM02_03_Pop01: FrmPopUpBase
    {
        public string pU_ID { get; set; }
        public string pYEAR { get; set; }
        public string pQTYPE { get; set; }
        public string pSIDO { get; set; }
        public string pGUGUN { get; set; }
        public string pQ1 { get; set; }
        public string pQ2 { get; set; }

        public frmGM02_03_Pop01()
        {
            InitializeComponent();
        }

        #region FrmLoadEvent()


        public  void FrmGM02_03_Pop01_Load(object sender, EventArgs e)
        {

            dtS_DATE.EditValue = pYEAR.ToString() + "-01-01";
            dtE_DATE.EditValue = pYEAR.ToString() + "-12-31";
            txtU_ID.EditValue = pU_ID;
            txtQTYPE.EditValue = pQTYPE;
            txtSIDO.EditValue = pSIDO;
            txtGUGUN.EditValue = pGUGUN;
            txtQ1.EditValue = pQ1;
            txtQ2.EditValue = pQ2;
            

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("gshop_id", txtGSHOP_ID)
                    , new ColumnControlSet("ceo_name", txtCEO_NAME)
                    , new ColumnControlSet("gshop_name", txtGSHOP_NAME)
                    , new ColumnControlSet("u_nicknam", txtU_NICKNAME)
                    , new ColumnControlSet("register_no", txtREGISTER_NO)
                    , new ColumnControlSet("email", txtEMAIL)
                    , new ColumnControlSet("tel_no", txtTEL_NO)
                    , new ColumnControlSet("post_no", btnPOST_NO)
                    , new ColumnControlSet("road_addr", txtADDRESS1)
                    //, new ColumnControlSet("jibun_addr", txtADDRESS2)
                    , new ColumnControlSet("sido_code", cmbTAREA1)
                    , new ColumnControlSet("gugun_code", cmbSAREA1)
                    , new ColumnControlSet("recomm_nicknm", txtRECOMM_NM)
                    , new ColumnControlSet("recomm_u_id", txtRECOMM_U_ID)
                    , new ColumnControlSet("hp_no", txtHP_NO)
                   ); ;

            this.efwGridControl1.Click += efwGridControl1_Click;
            efwGridControl1.Focus();
            SetCmb();
            Open();
        }
        #endregion


        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
        }

        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(sido,'') as DCODE ,sido_nm as DNAME  FROM domabiz.place_master GROUP BY sido, sido_nm ";

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
        }
        private void Open()
        {
            try
            {

                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GM_GM_02_03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_qtype", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = txtQTYPE.EditValue;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtU_ID.EditValue;

                        cmd.Parameters.Add("i_sido", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = txtSIDO.EditValue;

                        cmd.Parameters.Add("i_gugun", MySqlDbType.VarChar, 10);
                        cmd.Parameters[5].Value = txtGUGUN.EditValue;

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
    }
}
