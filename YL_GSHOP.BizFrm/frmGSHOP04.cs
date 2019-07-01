#region "frmGSHOP02 설명"
//===================================================================================================
//■Program Name  : frmGSHOP04
//■Description   : G 멀티샵 구분별  현황
//■Author        : 송호철
//■Date          : 2019.06.12
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.06.12][송호철] Base
//[2] [2019.06.12][송호철] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

using DevExpress.XtraTreeList;
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
namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP04 : FrmBase
    {
        public frmGSHOP04()
        {
            InitializeComponent();

             //단축코드 설정 
            this.QCode = "GSHOP04";
            //폼명설정
            this.FrmName = "G멀티샵 현황";

                 


        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;
            
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("gshop_id", txtGSHOP_ID)
                    , new ColumnControlSet("ceo_name", txtCEO_NAME)
                    , new ColumnControlSet("gshop_name", txtGSHOP_NAME)
                    , new ColumnControlSet("recomm_nm", txtRECOMM_NM)
                    , new ColumnControlSet("register_no", txtREGISTER_NO)
                    , new ColumnControlSet("email", txtEMAIL)
                    , new ColumnControlSet("tel_no", txtTEL_NO)
                    , new ColumnControlSet("post_no", btnPOST_NO)
                    , new ColumnControlSet("road_addr", txtADDRESS1)
                    //, new ColumnControlSet("jibun_addr", txtADDRESS2)
                    , new ColumnControlSet("sido_code", cmbTAREA1)
                    , new ColumnControlSet("gugun_code", cmbSAREA1)

                      );

            this.efwGridControl1.Click += efwGridControl1_Click;

            SetCmb();
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

        private void CmbTAREA1_EditValueChanged(object sender, EventArgs e)
        {
            string sCSIDO = cmbTAREA1.EditValue.ToString();

            if (string.IsNullOrEmpty(this.cmbTAREA1.EditValue.ToString()))
            {
                return;
            }


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                //con.Query = "select DCODE, DNAME from ( select gugun as DCODE, concat(gugun_nm, IFNULL((select '   ( 선점 )' from domabiz.md_place where sido = a.sido and gugun = a.gugun), '')) as DNAME from domabiz.place_master a  where sido = " + sCSIDO + "  ) AS t1 group by dcode, dname ";
                con.Query = "select DCODE, DNAME from " +
                            " ( select gugun as DCODE, " +
                            "          concat(gugun_nm, IFNULL((select '   ( 선점 )' from domabiz.md_place where sido = a.sido and gugun = a.gugun), '')) as DNAME " +
                            "     from domabiz.place_master a  " +
                            "    where sido = " + sCSIDO + "  ) AS t1 " +
                            "group by dcode, dname ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbSAREA1, codeArray);

            }
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
        }
        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");

        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GSHOP_GSHOP04_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;
                        
                        cmd.Parameters.Add("i_road_addr", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtROAD_ADDR.EditValue;

                        cmd.Parameters.Add("i_rec_u_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtREC_U_NAME.EditValue;

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
        #endregion

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGSHOP_ID.Text))
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
                        using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GSHOP_GSHOP04_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGSHOP_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ceo_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_ceo_name"].Value = txtCEO_NAME.EditValue;
                            cmd.Parameters["i_ceo_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_gshop_name"].Value = txtGSHOP_NAME.EditValue;
                            cmd.Parameters["i_gshop_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recomm_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_recomm_nm"].Value = txtRECOMM_NM.EditValue;
                            cmd.Parameters["i_recomm_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_register_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_register_no"].Value = txtREGISTER_NO.EditValue;
                            cmd.Parameters["i_register_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_email", MySqlDbType.VarChar));
                            cmd.Parameters["i_email"].Value = txtEMAIL.EditValue;
                            cmd.Parameters["i_email"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel_no"].Value = txtTEL_NO.EditValue;
                            cmd.Parameters["i_tel_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hp_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_hp_no"].Value = txtHP_NO.EditValue;
                            cmd.Parameters["i_hp_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_post_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_post_no"].Value = btnPOST_NO.EditValue;
                            cmd.Parameters["i_post_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_jibun_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_jibun_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_jibun_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr"].Value = txtADDRESS1.EditValue;
                            cmd.Parameters["i_road_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sido", MySqlDbType.VarChar));
                            cmd.Parameters["i_sido"].Value = cmbTAREA1.EditValue;
                            cmd.Parameters["i_sido"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gugun", MySqlDbType.VarChar));
                            cmd.Parameters["i_gugun"].Value = cmbSAREA1.EditValue;
                            cmd.Parameters["i_gugun"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                Search();
            }
        }

        private void TxtREC_U_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void TxtROAD_ADDR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }


    }
}
