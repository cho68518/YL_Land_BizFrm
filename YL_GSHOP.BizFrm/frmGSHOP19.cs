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
using System.IO;
using System.Collections;
using System.Net.Sockets;
using Tamir.SharpSsh;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Tile;

namespace YL_GSHOP.BizFrm
{

    public partial class frmGSHOP19 : FrmBase
    {
        frmMemberInfo popup;
        frmGSHOP12_Pop01 popup1;
        public frmGSHOP19()
        {
            InitializeComponent();
            this.QCode = "GSHOP12";
            //폼명설정
            this.FrmName = "G멀티샵 현수막 X배너 지급관리";
        }

        private void frmGSHOP19_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;


            this.label2.Visible = false;
            this.txtQRank1.Visible = false;
            this.label3.Visible = false;
            this.txtQRank2.Visible = false;

            this.label4.Visible = true;
            this.txtMD.Visible = true;
            this.label6.Visible = true;
            this.dtS_DATE.Visible = true;
            this.dtE_DATE.Visible = true;
            this.efwLabel8.Visible = true;
            this.btnQuery.Visible = true;

            rbShowType.EditValue = "Y";
            rbShowType1.EditValue = "Y";
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            dtReg_Date.EditValue = DateTime.Now;
            dtReg_Date1.EditValue = DateTime.Now;

            gridView2.OptionsView.ShowFooter = true;

            this.efwGridControl2.BindControlSet(
              new ColumnControlSet("gshop_id", txtGShop_ID)
             ,new ColumnControlSet("gshop_name", dfGShop_name)
            );
            this.efwGridControl2.Click += efwGridControl2_Click;


            gridView3.OptionsView.ShowFooter = true;

            this.efwGridControl3.BindControlSet(
               new ColumnControlSet("gshop_id", txtGShop_ID)
             , new ColumnControlSet("reg_date", dtReg_Date)
             , new ColumnControlSet("banner", chkBanner)
             , new ColumnControlSet("x_banner", chkX_Banner)
             , new ColumnControlSet("remark", txtRemark)
             , new ColumnControlSet("rank1", txtRank1)
             , new ColumnControlSet("rank2", txtRank2)
             , new ColumnControlSet("stock_qty", txtStock_Qty)
             , new ColumnControlSet("youtube_qty", txtYoutube_Qty)
             , new ColumnControlSet("ceo_pic_qty", txtCeo_Pic_Qty)
             , new ColumnControlSet("youtube_relations", txtYouTube_Relations)
             , new ColumnControlSet("utube_nic", txtUtube_nic)
            );
            this.efwGridControl3.Click += efwGridControl3_Click;


            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
               new ColumnControlSet("gshop_id", txtShop_id)
             , new ColumnControlSet("u_id", txtU_ID)
             , new ColumnControlSet("show_type", rbShowType)
             , new ColumnControlSet("story", txtStory)
             , new ColumnControlSet("ImageURL", txtimg_url)
             , new ColumnControlSet("idx", txtIdx)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;


            gridView4.OptionsView.ShowFooter = true;

            this.efwGridControl4.BindControlSet(
               new ColumnControlSet("u_id", txtU_ID1)
             , new ColumnControlSet("show_type", rbShowType1)
             , new ColumnControlSet("story", txtStory1)
             , new ColumnControlSet("ImageURL", txtimg_url1)
             , new ColumnControlSet("u_name", txtU_Name)
             , new ColumnControlSet("u_nickname", txtU_Nickname)
             , new ColumnControlSet("login_id", txtLogin_Id)
             , new ColumnControlSet("reg_date", dtReg_Date1)
             , new ColumnControlSet("idx", txtIdx1)
             , new ColumnControlSet("area", txtArea)
             , new ColumnControlSet("age", txtAge)
            );
            this.efwGridControl4.Click += efwGridControl4_Click;


            SetCmb();
        }

        private void SetCmb()
        {

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT  code_id as DCODE, code_nm as DNAME FROM domaadmin.tb_common_code " +
                    "         where gcode_id = '00105' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataTable retDT = ds.Tables[0];

                repositoryItemLookUpEdit1.DataSource = retDT;
                //컨트롤 초기화
                InitCodeControl(repositoryItemLookUpEdit1);

                repositoryItemLookUpEdit1.EndUpdate();
            }
            // 샵구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00036' and code_id = '1000' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCategory_no, codeArray);
            }
            cmbCategory_no.EditValue = "1000";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code " +
                    "         where gcode_id = '00036' and code_id = '1100' " +
                    "         group by code_id, code_nm ";

                DataSet ds = con.selectQueryDataSet();
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbCategory_no1, codeArray);
            }
            cmbCategory_no1.EditValue = "1100";

        }

        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();

        private void SetLegacyCode_Mysql(RepositoryItemLookUpEdit cdControl, string codeGroup)
        {
            DataTable retDT = CodeAgent.GetLegacyCodeCollection(codeGroup);


            cdControl.DataSource = retDT;
            //컨트롤 초기화
            InitCodeControl(cdControl);
        }
        private void efwGridControl4_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl4.GetSelectedRow(0);
            if (dr != null && dr["ImageURL"].ToString() != "")
            {
                picBanner1.LoadAsync(dr["ImageURL"].ToString());
            }
            Cursor.Current = Cursors.Default;
        }
        private void InitCodeControl(object cdControl)
        {
            string DNAME = string.Empty;

            DNAME = "DNAME";

            CodeAgent.InitCodeControl(cdControl, "코드명", "코드", DNAME, "DCODE", "선택하세요");
        }


        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["gshop_id"].ToString() != "")
            {
                Open1();
            }
            Cursor.Current = Cursors.Default;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            if (dr != null && dr["ImageURL"].ToString() != "")
            {
                picBanner.LoadAsync(dr["ImageURL"].ToString());
            }
            Cursor.Current = Cursors.Default;
        }


        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["gshop_id"].ToString() != "")
            {
                this.txtGShop_ID.EditValue = dr["gshop_id"].ToString();
            }
            Cursor.Current = Cursors.Default;
        }

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open2();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open3();

            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open4();

            }
        }

        private void Open2()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;
                        if (string.IsNullOrEmpty(this.txtQRank1.Text))
                        {
                            txtRank1.EditValue = "0";
                        }
                        cmd.Parameters.Add("i_QRank1", MySqlDbType.Int32);
                        cmd.Parameters[1].Value = Convert.ToInt32(txtQRank1.EditValue);
                        if (string.IsNullOrEmpty(this.txtQRank2.Text))
                        {
                            txtRank1.EditValue = "0";
                        }
                        cmd.Parameters.Add("i_QRank2", MySqlDbType.Int32);
                        cmd.Parameters[2].Value = Convert.ToInt32(txtQRank2.EditValue);

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

        }

        private void Open1()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_gshow_id", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtGShop_ID.EditValue);


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open3()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar,50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

                        cmd.Parameters.Add("i_md", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtMD.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                           // this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        private void Open4()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

                        cmd.Parameters.Add("i_md", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtMD.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            // this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public override void Save()
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    var saveResult = new SaveTableResultInfo() { IsError = true };

                    var dt = efwGridControl1.GetChangeDataWithRowState;
                    var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {

                            if (gridView1.GetRowCellValue(i, "send_qty").ToString().Length > 0)
                            {
                                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SAVE_02", con))
                                {
                                    con.Open();
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("i_gshop_id", MySqlDbType.Int32, 11);
                                    cmd.Parameters[0].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "gshop_id"));

                                    cmd.Parameters.Add("i_send_date", MySqlDbType.DateTime);
                                    cmd.Parameters[1].Value = gridView1.GetRowCellValue(i, "send_date").ToString().Substring(0, 10) + " 12:01:01";

                                    //cmd.Parameters.Add("i_send_qty", MySqlDbType.Int32, 11);
                                    //cmd.Parameters[2].Value = Convert.ToInt32(gridView1.GetRowCellValue(i, "send_qty")).ToString();

                                    int nId = 0;
                                    string sId = "";

                                    sId = gridView1.GetRowCellValue(i, "send_qty").ToString();
                                    if (sId == "")
                                        nId = 0;
                                    else
                                        nId = Convert.ToInt32(gridView1.GetRowCellValue(i, "send_qty"));
                                    cmd.Parameters.Add("i_send_qty", MySqlDbType.Int32, 11);
                                    cmd.Parameters[2].Value = nId;


                                    cmd.Parameters.Add("i_send_type", MySqlDbType.VarChar,1);
                                    cmd.Parameters[3].Value = gridView1.GetRowCellValue(i, "send_type").ToString();

                                    cmd.Parameters.Add("i_is_install", MySqlDbType.VarChar, 1);
                                    cmd.Parameters[4].Value = gridView1.GetRowCellValue(i, "is_install").ToString();

                                    cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 255);
                                    cmd.Parameters[5].Value = gridView1.GetRowCellValue(i, "remark").ToString();

                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                    Search();
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }


        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.dtReg_Date.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 지급일을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGShop_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Reg_Date", MySqlDbType.DateTime));
                            cmd.Parameters["i_Reg_Date"].Value = dtReg_Date.EditValue;
                            cmd.Parameters["i_Reg_Date"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_Banner", MySqlDbType.VarChar));
                            cmd.Parameters["i_Banner"].Value = chkBanner.EditValue;
                            cmd.Parameters["i_Banner"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_X_Banner", MySqlDbType.VarChar));
                            cmd.Parameters["i_X_Banner"].Value = chkX_Banner.EditValue;
                            cmd.Parameters["i_X_Banner"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_Remark"].Value = txtRemark.EditValue;
                            cmd.Parameters["i_Remark"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtRank1.Text))
                            {
                                txtRank1.EditValue = "0";
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_rank1", MySqlDbType.Int32));
                            cmd.Parameters["i_rank1"].Value = Convert.ToInt32(txtRank1.EditValue);
                            cmd.Parameters["i_rank1"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtRank2.Text))
                            {
                                txtRank1.EditValue = "0";
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_rank2", MySqlDbType.Int32));
                            cmd.Parameters["i_rank2"].Value = Convert.ToInt32(txtRank2.EditValue);
                            cmd.Parameters["i_rank2"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtStock_Qty.Text))
                            {
                                txtStock_Qty.EditValue = "0"; 
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_stock_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_stock_qty"].Value = Convert.ToInt32(txtStock_Qty.EditValue);
                            cmd.Parameters["i_stock_qty"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtYoutube_Qty.Text))
                            {
                                txtYoutube_Qty.EditValue = "0"; 
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_youtube_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_youtube_qty"].Value = Convert.ToInt32(txtYoutube_Qty.EditValue);
                            cmd.Parameters["i_youtube_qty"].Direction = ParameterDirection.Input;

                            if (string.IsNullOrEmpty(this.txtCeo_Pic_Qty.Text))
                            {
                                txtCeo_Pic_Qty.EditValue = "0";
                            }
                            cmd.Parameters.Add(new MySqlParameter("i_ceo_pic_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_ceo_pic_qty"].Value = Convert.ToInt32(txtCeo_Pic_Qty.EditValue);
                            cmd.Parameters["i_ceo_pic_qty"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_YouTube_Relations", MySqlDbType.VarChar));
                            cmd.Parameters["i_YouTube_Relations"].Value = txtYouTube_Relations.EditValue;
                            cmd.Parameters["i_YouTube_Relations"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtGShop_ID.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_Reg_Date", MySqlDbType.DateTime));
                            cmd.Parameters["i_Reg_Date"].Value = dtReg_Date.EditValue;
                            cmd.Parameters["i_Reg_Date"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void popup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed2;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtYouTube_Relations.EditValue = popup.U_ID;
                this.txtUtube_nic.EditValue = popup.U_NICKNAME;
            }
            popup = null;
        }

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.label2.Visible = true;
                this.txtQRank1.Visible = true;
                this.label3.Visible = true;
                this.txtQRank2.Visible = true;

                this.label4.Visible = false;
                this.txtMD.Visible = false;
                this.label6.Visible = false;
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
                this.efwLabel8.Visible = false;
                this.btnQuery.Visible = false;


            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                this.label2.Visible = false;
                this.txtQRank1.Visible = false;
                this.label3.Visible = false;
                this.txtQRank2.Visible = false;

                this.label4.Visible = true;
                this.txtMD.Visible = true;
                this.label6.Visible = true;
                this.dtS_DATE.Visible = true;
                this.dtE_DATE.Visible = true;
                this.efwLabel8.Visible = true;
                this.btnQuery.Visible = true;

            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

                        cmd.Parameters.Add("i_md", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtMD.EditValue;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[2].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[3].Value = dtE_DATE.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            // this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    if (string.IsNullOrEmpty(this.txtIdx.Text))
                    {
                        txtIdx.EditValue = "0";
                    }
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SAVE_04", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtIdx.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtShop_id.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_category_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_category_no"].Value = Convert.ToInt32(cmbCategory_no.EditValue);
                            cmd.Parameters["i_category_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_link_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_link_type"].Value = rbLink.EditValue;
                            cmd.Parameters["i_link_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_img_url1", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url1"].Value = txtimg_url.EditValue;
                            cmd.Parameters["i_img_url1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_img_url2", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url2"].Value = txtImg_link.EditValue;
                            cmd.Parameters["i_img_url2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_show_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_show_type"].Value = rbShowType.EditValue;
                            cmd.Parameters["i_show_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtStory.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

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

        private void btnNew_Click(object sender, EventArgs e)
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");

            rbShowType.EditValue = "Y";
        }

        private void btnFileOpen4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            //string sftpDirectory = "/domalifefiles/files/events/banner/" + Convert.ToString(System.DateTime.Now.ToString("yyyyMMdd"));
            string sftpDirectory = "/domalifefiles/files/events/banner/";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtOrg_FileName.EditValue = openFileDialog.SafeFileName;
                    txtPicPath1.EditValue = openFileDialog.FileName;
                    picBanner.LoadAsync(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }

                finally
                {
                    Cursor = Cursors.Default;
                }


                // 폴더생성

                DirectoryInfo di = new DirectoryInfo(@"c:\temp");
                if (di.Exists == false)
                {
                    di.Create();
                }

                // 선택된 파일을 위에서 만든 폴더에 이름을 바꿔 저장

                string sOldFile = txtPicPath1.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                //string sFileName = txtOrg_FileName.EditValue.ToString();
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Delete(sNewFile);
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                //저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/events/banner/";
                string sFtpPath2 = "/domalifefiles/files/events/banner/" + sFileName;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtPicPath1.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }
                //if (isdir == false)
                //{

                //    sSftp.Mkdir(sFtpPath2);
                //}

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtimg_url.EditValue = "https://media.domalife.net/files/events/banner/" + sFileName;
            }
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            //string sftpDirectory = "/uploadFiles/files/landing/" + Convert.ToString(System.DateTime.Now.ToString("yyyyMMdd"));
            string sftpDirectory = "/domalifefiles/files/events/landing/";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtOrg_FileName.EditValue = openFileDialog.SafeFileName;
                    txtPicPath1.EditValue = openFileDialog.FileName;
                    picLanding.LoadAsync(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }

                finally
                {
                    Cursor = Cursors.Default;
                }


                // 폴더생성

                DirectoryInfo di = new DirectoryInfo(@"c:\temp");
                if (di.Exists == false)
                {
                    di.Create();
                }

                // 선택된 파일을 위에서 만든 폴더에 이름을 바꿔 저장

                string sOldFile = txtPicPath1.EditValue.ToString();
                //string sFileName = txtOrg_FileName.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Delete(sNewFile);
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                //저장 경로에 있는지 체크

                ArrayList ay = sSftp.GetFileList(sftpDirectory);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtPicPath1.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtImg_link.EditValue = "https://media.domalife.net/files/events/landing/" + sFileName;

            }
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = dr["ImageURL"].ToString();
                if (iconsCache.ContainsKey(url))
                {
                    e.Value = iconsCache[url];
                    return;
                }
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        e.Value = Bitmap.FromStream(stream);
                        iconsCache.Add(url, (Bitmap)e.Value);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void gridView4_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string url = dr["ImageURL"].ToString();
                if (iconsCache.ContainsKey(url))
                {
                    e.Value = iconsCache[url];
                    return;
                }
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        e.Value = Bitmap.FromStream(stream);
                        iconsCache.Add(url, (Bitmap)e.Value);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
                MEMBER_TYPE = "ALL",
            };
            popup.FormClosed += popup_FormClosed3;
            PopUpBizAgent.Show(this, popup);
        }
        private void popup_FormClosed3(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed3;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtU_ID1.EditValue = popup.U_ID;
                this.txtU_Nickname.EditValue = popup.U_NICKNAME;
                this.txtLogin_Id.EditValue = popup.USER_ID;
                this.txtU_Name.EditValue = popup.U_NAME;
            }
            popup = null;
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.xls";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            //string sftpDirectory = "/domalifefiles/files/events/banner/" + Convert.ToString(System.DateTime.Now.ToString("yyyyMMdd"));
            string sftpDirectory = "/domalifefiles/files/events/banner/";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtOrg_FileName1.EditValue = openFileDialog.SafeFileName;
                    txtPicPath2.EditValue = openFileDialog.FileName;
                    picBanner1.LoadAsync(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지파일이 문제가 있습니다." + "\r\n" + ex.ToString());
                }

                finally
                {
                    Cursor = Cursors.Default;
                }


                // 폴더생성

                DirectoryInfo di = new DirectoryInfo(@"c:\temp");
                if (di.Exists == false)
                {
                    di.Create();
                }

                // 선택된 파일을 위에서 만든 폴더에 이름을 바꿔 저장

                string sOldFile = txtPicPath2.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                //string sFileName = txtOrg_FileName.EditValue.ToString();
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Delete(sNewFile);
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                //저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/events/banner/";
                string sFtpPath2 = "/domalifefiles/files/events/banner/" + sFileName;

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == txtPicPath2.EditValue.ToString())
                    {
                        isdir = true;
                    }
                }
                //if (isdir == false)
                //{

                //    sSftp.Mkdir(sFtpPath2);
                //}

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtimg_url1.EditValue = "https://media.domalife.net/files/events/banner/" + sFileName;
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
            rbShowType1.EditValue = "Y";
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP19_SAVE_05", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_Idx", MySqlDbType.Int32));
                            cmd.Parameters["i_Idx"].Value = Convert.ToInt32(txtIdx1.EditValue);
                            cmd.Parameters["i_Idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_u_id"].Value = txtU_ID1.EditValue;
                            cmd.Parameters["i_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop_id", MySqlDbType.Int32));
                            cmd.Parameters["i_gshop_id"].Value = Convert.ToInt32(txtShop_id.EditValue);
                            cmd.Parameters["i_gshop_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_category_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_category_no"].Value = Convert.ToInt32(cmbCategory_no1.EditValue);
                            cmd.Parameters["i_category_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_link_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_link_type"].Value = rbLink1.EditValue;
                            cmd.Parameters["i_link_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_img_url1", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url1"].Value = txtimg_url1.EditValue;
                            cmd.Parameters["i_img_url1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_img_url2", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url2"].Value = txtImg_link1.EditValue;
                            cmd.Parameters["i_img_url2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_show_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_show_type"].Value = rbShowType1.EditValue;
                            cmd.Parameters["i_show_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtStory1.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_reg_date", MySqlDbType.VarChar));
                            cmd.Parameters["i_reg_date"].Value = dtReg_Date1.EditValue3;
                            cmd.Parameters["i_reg_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_area", MySqlDbType.VarChar));
                            cmd.Parameters["i_area"].Value = txtArea.EditValue;
                            cmd.Parameters["i_area"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_age", MySqlDbType.VarChar));
                            cmd.Parameters["i_age"].Value = txtAge.EditValue;
                            cmd.Parameters["i_age"].Direction = ParameterDirection.Input;

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


        private void picBanner_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("1");
        }

        private void OpenDlg(string s1)
        {
            popup1 = new frmGSHOP12_Pop01();
            //popup.Owner = this;

            if (s1 == "1")
                popup1.pURL = txtimg_url.Text;
            else if (s1 == "2")
                popup1.pURL = txtimg_url1.Text;
 
            popup1.FormClosed += popup1_FormClosed;
            popup1.ShowDialog();
        }
        private void popup1_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup1.FormClosed -= popup1_FormClosed;

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup1 = null;
        }

        private void picBanner1_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("2");
        }
    }
}
