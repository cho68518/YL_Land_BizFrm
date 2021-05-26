using DevExpress.XtraEditors.Repository;
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
using DevExpress.XtraGrid.Columns;

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP17 : FrmBase
    {
        frmMemberInfo popup;
        public frmGSHOP17()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GSHOP17";
            //폼명설정
            this.FrmName = "영업사원 마스터";
        }

        private void frmGSHOP17_Load(object sender, EventArgs e)
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

            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("id", txtID)
            , new ColumnControlSet("username", txtUserName)
            , new ColumnControlSet("u_name", txtU_Name)
            , new ColumnControlSet("u_cell", txtU_Cell)
            , new ColumnControlSet("u_email", txtU_Email)
            , new ColumnControlSet("u_nickname", txtU_Nickname)
            , new ColumnControlSet("team_leader", txtTeam_Leader)
            , new ColumnControlSet("enabled", chkEnabled)
            , new ColumnControlSet("u_id", txtU_ID)
            );

            this.efwGridControl1.Click += efwGridControl1_Click;
            SetCmb();
        }
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            if (dr != null && dr["Region"].ToString() != "")
            {
                this.cmbRegion.EditValue = dr["Region"].ToString();
                this.cmbRegion_Area.EditValue = dr["Region_Area"].ToString();
            }
        }

        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT ifnull(sido,'00') as DCODE ,ifnull(sido_nm,'전국') as DNAME  FROM domabiz.place_master GROUP BY sido, sido_nm ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbRegion, codeArray);


            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT code_id as DCODE, code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00055' ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbQuery, codeArray);


            }

        }

        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
            cmbRegion.EditValue = "00";
            cmbRegion_Area.EditValue = "00";
        }

        public override void Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sP_SHOW_TYPE = string.Empty;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.Dev_Conn))
               // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP17_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = this.cmbQuery.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = this.txtSearch.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrEmpty(this.txtUserName.Text))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " ID를 입력하세요");
                    return;
                }

                if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
                {
                    try
                    {
                        // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.Dev_Conn))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP17_SAVE_01", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                                cmd.Parameters["i_id"].Value = Convert.ToInt32(txtID.EditValue);
                                cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_username", MySqlDbType.VarChar));
                                cmd.Parameters["i_username"].Value = txtUserName.EditValue;
                                cmd.Parameters["i_username"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_u_name", MySqlDbType.VarChar));
                                cmd.Parameters["i_u_name"].Value = txtU_Name.EditValue;
                                cmd.Parameters["i_u_name"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_u_cell", MySqlDbType.VarChar));
                                cmd.Parameters["i_u_cell"].Value = txtU_Cell.EditValue;
                                cmd.Parameters["i_u_cell"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_u_email", MySqlDbType.VarChar));
                                cmd.Parameters["i_u_email"].Value = txtU_Email.EditValue;
                                cmd.Parameters["i_u_email"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_region", MySqlDbType.VarChar));
                                cmd.Parameters["i_region"].Value = cmbRegion.EditValue;
                                cmd.Parameters["i_region"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_region_area", MySqlDbType.VarChar));
                                cmd.Parameters["i_region_area"].Value = cmbRegion_Area.EditValue;
                                cmd.Parameters["i_region_area"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_enabled", MySqlDbType.VarChar));
                                cmd.Parameters["i_enabled"].Value = chkEnabled.EditValue;
                                cmd.Parameters["i_enabled"].Direction = ParameterDirection.Input;

                                cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                                cmd.Parameters["i_u_id"].Value = txtU_ID.EditValue;
                                cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;


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
        }

        private void cmbRegion_EditValueChanged(object sender, EventArgs e)
        {
            string sCSIDO = string.Empty;


            sCSIDO = cmbRegion.EditValue.ToString();

            if (string.IsNullOrEmpty(this.cmbRegion.EditValue.ToString()))
            {
                return;
            }


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "select DCODE, DNAME from " +
                            " ( select ifnull(gugun,'00') as DCODE, " +
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

                CodeAgent.MakeCodeControl(this.cmbRegion_Area, codeArray);

            }
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
            };
            popup.FormClosed += popup_FormClosed2;
            PopUpBizAgent.Show(this, popup);
        }

        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed2;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtU_ID.EditValue = popup.U_ID;
                this.txtU_Nickname.EditValue = popup.U_NICKNAME;
            }
            popup = null;
        }
    }
}
