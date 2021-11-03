using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using YL_GM.BizFrm.Dlg;
namespace YL_GM.BizFrm
{
    public partial class frmGM13 : FrmBase
    {
        frmGM13_Pop01 popup;
        public frmGM13()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmGM13";
            //폼명설정
            this.FrmName = "스토리 현황";
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;
            rbq_type.EditValue = "1";

            //this.efwGridControl1.Click += efwGridControl1_Click;

            SetCmb();
        }
        private void SetCmb()
        {
            // 회원검색

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00034'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbMember_Search, codeArray);
            }

            cmbMember_Search.EditValue = "01";



        }


        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();
            }

        }

        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbMember_Search.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtI_SEARCH.EditValue;

                        cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtO_CodeQ.EditValue;

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

        private void Open2()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM06_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = cmbMember_Search.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtI_SEARCH.EditValue;

                        cmd.Parameters.Add("i_o_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = txtO_CodeQ.EditValue;

                        cmd.Parameters.Add("i_q_type", MySqlDbType.VarChar, 50);
                        cmd.Parameters[5].Value = rbq_type.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            if (rbq_type.EditValue.ToString() == "0")
            {
                popup = new frmGM13_Pop01();

                popup.pO_Code = bandedGridView1.GetFocusedRowCellValue("o_code").ToString();
                popup.pShop_Code = bandedGridView1.GetFocusedRowCellValue("shop_order").ToString();
                popup.pStory_208 = bandedGridView1.GetFocusedRowCellValue("ps_story1").ToString();
                popup.pStory_221 = bandedGridView1.GetFocusedRowCellValue("ps_story2").ToString();
                popup.pStory_248 = bandedGridView1.GetFocusedRowCellValue("gd_story").ToString();
                popup.pStory_232 = bandedGridView1.GetFocusedRowCellValue("tel_story").ToString();
                popup.pStory_247 = bandedGridView1.GetFocusedRowCellValue("gv_story").ToString();
                popup.pStory_244 = bandedGridView1.GetFocusedRowCellValue("gm_story").ToString();
                popup.pStory_223 = bandedGridView1.GetFocusedRowCellValue("pr_story1").ToString();
                popup.pStory_243 = bandedGridView1.GetFocusedRowCellValue("pr_story2").ToString();

                popup.pU_NickNAme = bandedGridView1.GetFocusedRowCellValue("u_nickname").ToString();
                popup.pU_Chef_Level = bandedGridView1.GetFocusedRowCellValue("u_chef_level").ToString();
                popup.pDoma = bandedGridView1.GetFocusedRowCellValue("doma").ToString();
                popup.pVip = bandedGridView1.GetFocusedRowCellValue("vip").ToString();
                popup.pBiz = bandedGridView1.GetFocusedRowCellValue("biz").ToString();

                popup.FormClosed += popup_FormClosed;
                popup.ShowDialog();
            }
            else
            {
                MessageAgent.MessageShow(MessageType.Informational, "생성유무로 조회 하신후 스토리를 생성하세요!.");
            }
            
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }
    }

}
