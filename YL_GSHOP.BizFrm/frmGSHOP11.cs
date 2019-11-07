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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_GSHOP.BizFrm.Dlg;

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP11 : FrmBase
    {
        public frmGSHOP11()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GSHOP11";
            //폼명설정
            this.FrmName = "DM 관리";
        }
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
            dtAdvice_Date.EditValue = DateTime.Now;
            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            //gridView1.OptionsView.ShowFooter = true;

            //gridView1.Columns["o_purchase_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["o_purchase_cost"].SummaryItem.FieldName = "o_purchase_cost";
            //gridView1.Columns["o_purchase_cost"].SummaryItem.DisplayFormat = "{0:c}";

            this.efwGridControl1.BindControlSet(
            new ColumnControlSet("shop_id", txtShopId)
            , new ColumnControlSet("shop_type_co", cmbShop_Type)
            , new ColumnControlSet("shop_name", txtShopName)
            , new ColumnControlSet("ceo_name", txtCeoName)
            , new ColumnControlSet("tel_no", txtTel_No)
            //, new ColumnControlSet("last_advice", txtLast_Advice)
            , new ColumnControlSet("advice_date", dtAdvice_Date)
            , new ColumnControlSet("road_addr", txtRoad_Addr)
            , new ColumnControlSet("jibun_addr", txtJibun_Addr)
            , new ColumnControlSet("rank", txtRank)
            , new ColumnControlSet("memo", txtRemark)
            //, new ColumnControlSet("dm_send", cbDmSend)


            );
            this.efwGridControl1.Click += efwGridControl1_Click;
            cbDmSend.EditValue = "N";
            rbDM.EditValue = "T";
            txtLast_Advice.EditValue = UserInfo.instance().Name;
            txtRank.EditValue = "0";
            SetCmb();

        }
        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
            txtRank.EditValue = "0";
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            txtU_ZIP.EditValue = "";
            if (dr != null && dr["post_no"].ToString() != "")
            {
                this.txtU_ZIP.EditValue = dr["post_no"].ToString();
            }
            if (dr != null && dr["shop_type_co"].ToString() != "")
            { 
                cmbShop_Type.EditValue = dr["shop_type_co"].ToString();
            }

            if (dr != null && dr["last_advice"].ToString() != "")
            {
                txtLast_Advice.EditValue = UserInfo.instance().Name;
            }

            if (dr != null && dr["place1"].ToString() != "")
            {
                cmbPlace1_1.Text = dr["place1"].ToString();
            }

            if (dr != null && dr["place2"].ToString() != "")
            {
                cmbPlace2_1.Text = dr["place2"].ToString();
            }

            if (dr != null && dr["dm_send"].ToString() != "")
            {
                cbDmSend.EditValue = dr["dm_send"].ToString();
            }

        }


        private void SetCmb()
        {
            // 공급자구분

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT count(*) as DCODE, place1  as DNAME  FROM  domabiz.beauty_shop group by place1 ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbPlace1, codeArray);
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT count(*) as DCODE, place1  as DNAME  FROM  domabiz.beauty_shop group by place1 ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbPlace1_1, codeArray);
            }


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00014'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbShopType, codeArray);
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00014'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbShop_Type, codeArray);
            }

        }

        private void cmbPlace1_EditValueChanged(object sender, EventArgs e)
        {
            string sCate_Code1 = cmbPlace1.EditValue.ToString();

            if (this.cmbPlace1.EditValue == null)
            {
                return;
            }

            string sPlace1 = cmbPlace1.Text.ToString();

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT count(*) as DCODE, place2  as DNAME  FROM  domabiz.beauty_shop where place1 = " + "'" + sPlace1 + "'" + " group by place2 ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbPlace2, codeArray);
            }
        }

        private void cmbPlace1_1_EditValueChanged(object sender, EventArgs e)
        {

            if (this.cmbPlace1_1.EditValue == null)
            {
                return;
            }

            string sPlace1_1 = cmbPlace1_1.Text.ToString();

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT count(*) as DCODE, place2  as DNAME  FROM  domabiz.beauty_shop where place1 = " + "'" + sPlace1_1 + "'" + " group by place2 ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbPlace2_1, codeArray);
            }


        }

        public override void Search()
        {
            try
            {

                if (cmbPlace1.Text.ToString() == "선택하세요")
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 시,도를 선택하세요!");
                    return;
                }
                if (cmbPlace2.Text.ToString() == "선택하세요")
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 구,군을 선택하세요!");
                    return;
                }
                string sDM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP11_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_place1", MySqlDbType.VarChar, 20);
                        cmd.Parameters[0].Value = cmbPlace1.Text.ToString();

                        cmd.Parameters.Add("i_place2", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbPlace2.Text.ToString();

                        cmd.Parameters.Add("i_addr", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = dfAddr.EditValue;

                        cmd.Parameters.Add("i_shoptype", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = cmbShopType.EditValue;

                        if (rbDM.EditValue.ToString() != "Y" && rbDM.EditValue.ToString() != "N")
                            sDM = null;
                        else
                            sDM = rbDM.EditValue.ToString();
                        cmd.Parameters.Add("i_dm", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = sDM;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            // this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void txtU_ZIP_Click(object sender, EventArgs e)
        {
            frmZipNoInfo FrmInfo = new frmZipNoInfo() { ParentBtn = txtU_ZIP, ParentAddr1 = txtRoad_Addr, ParentAddr2 = txtJibun_Addr };
            FrmInfo.COMPANYCD = "YL01";
            FrmInfo.COMPANYNAME = "(주)와이엘랜드";
            FrmInfo.ShowDialog();
            txtRoad_Addr.Focus();
        }

        private void btnAddress_Save_Click(object sender, EventArgs e)
        {
            if (cmbPlace1_1.Text.ToString() == "선택하세요")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 시,도를 선택하세요!");
                return;
            }
            if (cmbPlace2_1.Text.ToString() == "선택하세요")
            {
                MessageAgent.MessageShow(MessageType.Warning, " 구,군을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP11_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_shop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_shop_id"].Value = Convert.ToInt32(txtShopId.EditValue);
                            cmd.Parameters["i_shop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_shop_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_shop_name"].Value = txtShopName.EditValue;
                            cmd.Parameters["i_shop_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_shoop_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_shoop_type"].Value = cmbShop_Type.EditValue;
                            cmd.Parameters["i_shoop_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ceo_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_ceo_name"].Value = txtCeoName.EditValue;
                            cmd.Parameters["i_ceo_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel_no"].Value = txtTel_No.EditValue;
                            cmd.Parameters["i_tel_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_zip", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_zip"].Value = txtU_ZIP.EditValue;
                            cmd.Parameters["i_u_zip"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_last_advice", MySqlDbType.VarChar));
                            cmd.Parameters["i_last_advice"].Value = UserInfo.instance().Name;
                            cmd.Parameters["i_last_advice"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_advice_date", MySqlDbType.DateTime));
                            cmd.Parameters["i_advice_date"].Value = dtAdvice_Date.EditValue;
                            cmd.Parameters["i_advice_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_dm_send", MySqlDbType.VarChar));
                            cmd.Parameters["i_dm_send"].Value = cbDmSend.EditValue;
                            cmd.Parameters["i_dm_send"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_rank", MySqlDbType.Int32));
                            cmd.Parameters["i_rank"].Value = Convert.ToInt32(txtRank.EditValue);
                            cmd.Parameters["i_rank"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_road_addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_road_addr"].Value = txtRoad_Addr.EditValue;
                            cmd.Parameters["i_road_addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Jibun_Addr", MySqlDbType.VarChar));
                            cmd.Parameters["i_Jibun_Addr"].Value = txtJibun_Addr.EditValue;
                            cmd.Parameters["i_Jibun_Addr"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_place1", MySqlDbType.VarChar));
                            cmd.Parameters["i_place1"].Value = cmbPlace1_1.Text.ToString();
                            cmd.Parameters["i_place1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_place2", MySqlDbType.VarChar));
                            cmd.Parameters["i_place2"].Value = cmbPlace2_1.Text.ToString();
                            cmd.Parameters["i_place2"].Direction = ParameterDirection.Input;
                            
                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;
                            
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
                
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                txtRank.EditValue = Convert.ToInt32(txtRank.EditValue.ToString()) + 1;
                Search();
            }
        }

        private void dfAddr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            string filePath = "C:\\temp\\스토리.jpg";
            string uName = "twoone@admin";
            string password = "twoone5906";
            try
            {
                WebClient client = new WebClient();

                NetworkCredential nc = new NetworkCredential(uName, password);

                Uri addy = new Uri("\\\\14.34.8.38\\Upgrade\\ikoas\\");
                client.Credentials = nc;
                byte[] arrReturn = client.UploadFile(addy, filePath);
                Console.WriteLine(arrReturn.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

