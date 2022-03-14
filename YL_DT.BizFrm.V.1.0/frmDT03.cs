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
using DevExpress.XtraGrid.Columns;
using YL_COMM.BizFrm;
using YL_DT.BizFrm.Dlg;
using DevExpress.XtraGrid.Views.Tile;
using System.Data.SqlClient;

namespace YL_DT.BizFrm
{
    public partial class frmDT03 : FrmBase
    {
        public frmDT03()
        {
            InitializeComponent();
            this.QCode = "DT03";
            //폼명설정
            this.FrmName = "회원 가입관리";
        }

        private void frmDT03_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dtE_DATE.EditValue = DateTime.Now;
            gridView2.OptionsView.ShowFooter = true;

            this.cmbP_Code_Id.Visible = false;
            this.efwLabel7.Visible = true;
            this.efwLabel12.Visible = true;
            this.efwLabel7.Text = "기간";
            this.dtS_DATE.Visible = true;
            this.dtE_DATE.Visible = true;
            txtSort.EditValue = "0";

            cbDoma.EditValue = "N";
            cbHelper.EditValue = "N";
            cbOfficial.EditValue = "N";
            cbGshop.EditValue = "N";
            cbTeam_Leader.EditValue = "N";
            cbStock.EditValue = "N";

            this.efwGridControl2.BindControlSet(
             new ColumnControlSet("u_id", txtU_Id)
            );

            this.efwGridControl4.BindControlSet(
               new ColumnControlSet("p_code_id", txtP_Code_Id)
              ,new ColumnControlSet("p_code_nm", txtP_Code_NM)
              ,new ColumnControlSet("code_id", txtCode_Id)
              ,new ColumnControlSet("code_nm", txtCode_NM)
              ,new ColumnControlSet("doma", cbDoma)
              ,new ColumnControlSet("helper", cbHelper)
              ,new ColumnControlSet("official", cbOfficial)
              ,new ColumnControlSet("gshop", cbGshop)
              ,new ColumnControlSet("team_leader", cbTeam_Leader)
              ,new ColumnControlSet("stock", cbStock)
              ,new ColumnControlSet("view_type", cmbView_Type)
              ,new ColumnControlSet("sort", txtSort)
              ,new ColumnControlSet("remark", txtRemark)
            );



            SetCmb();
        }
        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(p_code_id,'') as DCODE ,p_code_nm as DNAME  FROM domabiz.tb_notice_grant_master group by 1,2  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbP_Code_Id, codeArray);
            }

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select ifnull(code_id,'') as DCODE, code_nm as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00080' ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbView_Type, codeArray);
            }
            cmbView_Type.EditValue = "A";

        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            txtQuery.Focus();
            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                SqlCommand cmd = new SqlCommand();
                sql.Query = " select t2.idx as idx, t1.reg_date as reg_date, t3.u_nickname as u_nickname, t1.is_use as is_use, t2.image_url as pic_url1, " +
                            "        t2.contents_id as contents_id, t2.order_key as order_key, t2.overlap_pic as overlap_pic, t2.overlap_loc as overlap_loc,  t2.customer_num as customer_num " +
                            "   from domalife.story_list t1  " +
                            "        inner join domalife.y_thumbnail_list t2 on t2.contents_id = t1.story_id  and t2.is_use = 'Y' " +
                            "        inner join domalife.member_master    t3 on t3.u_id = t1.u_id    " +
                            "   where t1.category_no = 249 and t1.u_id =  '" + txtU_Id.EditValue + "'  " +
                            "  order by t1.story_id, t2.order_key ";

                DataSet ds = sql.selectQueryDataSet();
                gridControl1.DataSource = ds.Tables[0];
            }
        }
        public override void NewMode()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                base.NewMode();
                Eraser.Clear(this, "CLR1");
                cmbView_Type.EditValue = "A";
            }
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
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
            }
        }

        public override void Save()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Save2();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Save2();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Save3();
            }
        }

        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtQuery.EditValue;


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

        private void Open2()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT03_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_ProdName", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQuery.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }
        public void Open3()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT03_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = cmbP_Code_Id.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl4.DataBind(ds);
                            this.efwGridControl4.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Save2()
        {
            try
            {
                var saveResult = new SaveTableResultInfo() { IsError = true };
                var dt = efwGridControl1.GetChangeDataWithRowState;
                var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][StatusColumn].ToString() == "U")
                    {
                        //Console.WriteLine("------------------------------------------------------------");
                        //Console.WriteLine("[U] " + dt.Rows[i]["o_code"].ToString());
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT03_SAVE_02", con))
                            {
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_id", MySqlDbType.Int32);
                                cmd.Parameters[0].Value = dt.Rows[i]["id"].ToString();

                                cmd.Parameters.Add("i_is_experience", MySqlDbType.VarChar,1);
                                cmd.Parameters[1].Value = dt.Rows[i]["is_experience"].ToString();

                                cmd.Parameters.Add("i_is_gshop", MySqlDbType.VarChar, 1);
                                cmd.Parameters[2].Value = dt.Rows[i]["is_gshop"].ToString();

                                cmd.Parameters.Add("i_is_helper", MySqlDbType.VarChar, 1);
                                cmd.Parameters[3].Value = dt.Rows[i]["is_helper"].ToString();

                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Open2();
        }

        public void Save3()
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DT_DT03_SAVE_03", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_p_code_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_code_id"].Value = txtP_Code_Id.EditValue;
                            cmd.Parameters["i_p_code_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_p_code_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_p_code_nm"].Value = txtP_Code_NM.EditValue;
                            cmd.Parameters["i_p_code_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_id"].Value = txtCode_Id.EditValue;
                            cmd.Parameters["i_code_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_code_nm", MySqlDbType.VarChar));
                            cmd.Parameters["i_code_nm"].Value = txtCode_NM.EditValue;
                            cmd.Parameters["i_code_nm"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_doma", MySqlDbType.VarChar));
                            cmd.Parameters["i_doma"].Value = cbDoma.EditValue;
                            cmd.Parameters["i_doma"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_helper", MySqlDbType.VarChar));
                            cmd.Parameters["i_helper"].Value = cbHelper.EditValue;
                            cmd.Parameters["i_helper"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gshop", MySqlDbType.VarChar));
                            cmd.Parameters["i_gshop"].Value = cbGshop.EditValue;
                            cmd.Parameters["i_gshop"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_official", MySqlDbType.VarChar));
                            cmd.Parameters["i_official"].Value = cbOfficial.EditValue;
                            cmd.Parameters["i_official"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_team_leader", MySqlDbType.VarChar));
                            cmd.Parameters["i_team_leader"].Value = cbTeam_Leader.EditValue;
                            cmd.Parameters["i_team_leader"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_stock", MySqlDbType.VarChar));
                            cmd.Parameters["i_stock"].Value = cbStock.EditValue;
                            cmd.Parameters["i_stock"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_view_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_view_type"].Value = cmbView_Type.EditValue;
                            cmd.Parameters["i_view_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sort", MySqlDbType.Int32));
                            cmd.Parameters["i_sort"].Value = Convert.ToInt32(txtSort.EditValue);
                            cmd.Parameters["i_sort"].Direction = ParameterDirection.Input;

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
                Search();
            }
        }

        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();
        private void layoutView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DataRow dr = (e.Row as DataRowView).Row;
            string url = string.Empty;

            if (e.Column.FieldName == "Image1")
            {
                url = dr["pic_url1"].ToString();
            }

            if (iconsCache.ContainsKey(url))
            {
                e.Value = iconsCache[url];
                return;
            }

            if (url != "")
            {
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
            Cursor.Current = Cursors.Default;
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

        private void efwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.cmbP_Code_Id.Visible = false;
                this.efwLabel7.Visible = true;
                this.efwLabel12.Visible = true;
                this.efwLabel7.Text = "기간";
                this.dtS_DATE.Visible = true;
                this.dtE_DATE.Visible = true;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                this.cmbP_Code_Id.Visible = false;
                this.efwLabel12.Visible = false;
                this.efwLabel7.Visible = false;
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                this.cmbP_Code_Id.Visible = true;
                this.efwLabel7.Visible = true;
                this.efwLabel12.Visible = false;
                this.efwLabel7.Text = "분류";
                this.dtS_DATE.Visible = false;
                this.dtE_DATE.Visible = false;
            }
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");
            cmbView_Type.EditValue = "A";
        }
    }
}
