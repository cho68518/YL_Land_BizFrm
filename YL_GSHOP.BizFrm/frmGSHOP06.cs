using Easy.Framework.Common;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using YL_GSHOP.BizFrm.Dlg;

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP06 : FrmBase
    {
        frmMemberInfo popup;
        frmMDMemberInfo MDpopup;

        public EventHandler efwGridControl1_Click { get; private set; }

        public frmGSHOP06()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "GSHOP06";   
            //폼명설정
            this.FrmName = "G멀티샵 MD등록";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = false;
            this.IsSearch = false;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //gridView1.OptionsView.ShowFooter = true;

            //this.efwGridControl1.BindControlSet(
            //          new ColumnControlSet("ID", txtID)
            //        , new ColumnControlSet("SETCODE", txtSETCODE)
            //          );

            //this.efwGridControl1.Click += efwGridControl1_Click;

            SetCmb();


            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("u_name", txtU_NAME)
            , new ColumnControlSet("u_nickname", txtU_NICKNAME)
            , new ColumnControlSet("user_id", txtUSER_ID)
            , new ColumnControlSet("u_birthday", txtBIRTH)
            , new ColumnControlSet("u_gender", txtU_GENDER)
            , new ColumnControlSet("u_email", txtU_EMAIL)
            , new ColumnControlSet("reg_date", txtREG_DATE)
            , new ColumnControlSet("u_zip", txtU_ZIP)
            , new ColumnControlSet("u_addr", txtU_ADDR)
            , new ColumnControlSet("u_addr_detail", txtU_ADDR_DETAIL)
            , new ColumnControlSet("u_chef_level", txtU_CHEF_LEVEL)
            , new ColumnControlSet("is_gr_md", chkIS_GR_MD)
            , new ColumnControlSet("idx", txtIDX)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;

            this.efwGridControl3.BindControlSet(
              new ColumnControlSet("u_id", txtGR_U_ID)
            , new ColumnControlSet("u_name", txtGR_U_NAME)
            , new ColumnControlSet("user_id", txtGR_USER_ID)
            , new ColumnControlSet("u_nickname", txtGR_U_NICKNAME)
            );
            this.efwGridControl3.Click += efwGridControl3_Click;

            this.efwGridControl2.BindControlSet(
              new ColumnControlSet("u_name", txtU_NAME1)
            , new ColumnControlSet("u_nickname", txtU_NICKNAME1)
            , new ColumnControlSet("user_id", txtUSER_ID1)
            , new ColumnControlSet("u_birthday", txtBIRTH1)
            , new ColumnControlSet("u_gender", txtU_GENDER1)
            , new ColumnControlSet("u_email", txtU_EMAIL1)
            , new ColumnControlSet("reg_date", txtREG_DATE1)
            , new ColumnControlSet("u_zip", txtU_ZIP1)
            , new ColumnControlSet("u_addr", txtU_ADDR1)
            , new ColumnControlSet("u_addr_detail", txtU_ADDR_DETAIL1)
            , new ColumnControlSet("idx", txtIDX1) 
            , new ColumnControlSet("md_u_id", txtMD_U_ID)
            , new ColumnControlSet("doramd_type", cbDORAMD_TYPE)
            , new ColumnControlSet("remark", txtREMARK)

            );
            this.efwGridControl2.Click += efwGridControl2_Click;



            BtnNew_Click(null, null);
            Open4();
        }

 
        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl3.GetSelectedRow(0);

            txtU_NAME1.EditValue = null;
            txtU_NICKNAME1.EditValue = null;
            txtUSER_ID1.EditValue = null;
            txtBIRTH1.EditValue = null;
            txtU_GENDER1.EditValue = null;
            txtU_CELL_NUM1.EditValue = null;
            txtU_EMAIL1.EditValue = null;
            txtLOGIN_DATE1.EditValue = null;
            txtREG_DATE1.EditValue = null;
            txtU_ZIP1.EditValue = null;
            txtU_ADDR1.EditValue = null;
            txtU_ADDR_DETAIL1.EditValue = null;
            cbDORAMD_TYPE.EditValue = null;
            txtIDX1.EditValue = null;
            txtREMARK.EditValue = null;

            Open2();
        }
        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);

            if (dr != null && dr["doramd_type"].ToString() != "")
            {
                this.cbDORAMD_TYPE.EditValue = dr["doramd_type"].ToString();
                //this.cbDORAMD_TYPE.Text = Convert.ToInt16(dr["doramd_type"]).ToString();
                this.cbDORAMD_TYPE.Text = dr["doramd_type"].ToString();
            }
            Open3();
        }
        #endregion

        private void SetCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT sido as DCODE ,sido_nm as DNAME  FROM domabiz.place_master GROUP BY sido, sido_nm ";

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

        private void BtnNew_Click(object sender, EventArgs e)
        {
            txtU_NAME.EditValue = null;
            txtU_NICKNAME.EditValue = null;
            txtUSER_ID.EditValue = null;
            txtBIRTH.EditValue = null;
            txtU_GENDER.EditValue = null;
            txtU_CELL_NUM.EditValue = null;
            txtU_EMAIL.EditValue = null;
            txtLOGIN_DATE.EditValue = null;
            txtREG_DATE.EditValue = null;
            txtU_ZIP.EditValue = null;
            txtU_ADDR.EditValue = null;
            txtU_ADDR_DETAIL.EditValue = null;
            txtU_CHEF_LEVEL.EditValue = null;
            chkIS_GR_MD.Checked = false;
            txtIDX.EditValue = null;

        }

        private void popup_FormClosed1(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed1;
            if (popup.DialogResult == DialogResult.OK)
            {
                this.txtU_NAME.Text = popup.U_NAME;
                this.txtIDX.Text = popup.IDX;
                this.txtU_NICKNAME.EditValue = popup.U_NICKNAME;
                this.txtUSER_ID.EditValue = popup.USER_ID;
                this.txtBIRTH.EditValue = popup.BIRTH;
                this.txtU_GENDER.EditValue = popup.U_GENDER;
                this.txtU_CELL_NUM.EditValue = popup.U_CELL_NUM;
                this.txtU_EMAIL.EditValue = popup.U_EMAIL;
                this.txtLOGIN_DATE.EditValue = popup.LOGIN_DATE;
                this.txtREG_DATE.EditValue = popup.REG_DATE;
                this.txtU_ZIP.EditValue = popup.U_ZIP;
                this.txtU_ADDR.EditValue = popup.U_ADDR;
                this.txtU_ADDR_DETAIL.EditValue = popup.U_ADDR_DETAIL;
                this.txtU_CHEF_LEVEL.EditValue = popup.U_CHEF_LEVEL;

                this.chkIS_GR_MD.EditValue = popup.IS_STOCK_FRIEND;


            }
            popup = null;
        }

        private void MDpopup_FormClosed2(object sender, FormClosedEventArgs e)
        {
            MDpopup.FormClosed -= MDpopup_FormClosed2;
            if (MDpopup.DialogResult == DialogResult.OK)
            {
                this.txtU_NAME1.Text = MDpopup.U_NAME;

                this.txtU_NICKNAME1.EditValue = MDpopup.U_NICKNAME;
                this.txtUSER_ID1.EditValue = MDpopup.USER_ID;
                this.txtBIRTH1.EditValue = MDpopup.BIRTH;
                this.txtU_GENDER1.EditValue = MDpopup.U_GENDER;
                this.txtU_CELL_NUM1.EditValue = MDpopup.U_CELL_NUM;
                this.txtU_EMAIL1.EditValue = MDpopup.U_EMAIL;
                this.txtLOGIN_DATE1.EditValue = MDpopup.LOGIN_DATE;
                this.txtREG_DATE1.EditValue = MDpopup.REG_DATE;
                this.txtU_ZIP1.EditValue = MDpopup.U_ZIP;
                this.txtU_ADDR1.EditValue = MDpopup.U_ADDR;
                this.txtU_ADDR_DETAIL1.EditValue = MDpopup.U_ADDR_DETAIL;
                this.txtMD_U_ID.EditValue = MDpopup.U_ID;
            }
            MDpopup = null;
        }


        //#endregion
        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            Open4();
        }

        private void Open4()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP06_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = txtSearch.EditValue;


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
                Cursor.Current = Cursors.Default;
            }
        }
        private void BtnMemberSch_Click(object sender, EventArgs e)
        {
            popup = new frmMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
            };
            popup.FormClosed += popup_FormClosed1;
            PopUpBizAgent.Show(this, popup);
        }

        private void BtnMemberSch1_Click(object sender, EventArgs e)
        {
            txtIDX1.Text = "";
            MDpopup = new frmMDMemberInfo
            {
                COMPANYCD = "YL01",
                COMPANYNAME = "(주)와이엘랜드",
            };
            MDpopup.FormClosed += MDpopup_FormClosed2;
            PopUpBizAgent.Show(this, MDpopup);
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUSER_ID.Text))
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
                        using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GSHOP_GSHOP06_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_chk", MySqlDbType.VarChar));
                            cmd.Parameters["i_chk"].Value = chkIS_GR_MD.EditValue;
                            cmd.Parameters["i_chk"].Direction = ParameterDirection.Input;

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
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
            }
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
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
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP06_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbSearch_Type.EditValue;

                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtSearch_Name.EditValue;

                        //Console.WriteLine(" i_Search_type           ---> [" + cmd.Parameters[0].Value + "]");
                        //Console.WriteLine(" i_search_Name           ---> [" + cmd.Parameters[1].Value + "]");
                        //Console.WriteLine(" i_member_type           ---> [" + cmd.Parameters[2].Value + "]");

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();

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
                Cursor.Current = Cursors.WaitCursor;
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP06_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_gr_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtGR_U_ID.EditValue;

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
                Cursor.Current = Cursors.Default;
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open3()
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtIDX1.Text))
                {
                    return;
                }
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP06_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_md_idx", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtIDX1.EditValue;

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

        private void CmbTAREA1_EditValueChanged(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(this.cmbTAREA1.Text))
            //{
            //    return;
            //}


            string sCSIDO = cmbTAREA1.EditValue.ToString();

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
        private void EfwXtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Open4();
            Open1();
            
        }

        private void EfwSimpleButton5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGR_U_ID.Text)) 
            {
                MessageAgent.MessageShow(MessageType.Warning, " 그룹MD를 선택하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtU_NAME1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회원을 선택하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.cbDORAMD_TYPE.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 프리/로컬 MD 구분을 선택하세요!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GSHOP_GSHOP06_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx"].Value = txtIDX1.EditValue;
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gr_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_gr_u_id"].Value = txtGR_U_ID.EditValue;
                            cmd.Parameters["i_gr_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_u_id"].Value = txtMD_U_ID.EditValue;
                            cmd.Parameters["i_md_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_doramd_type", MySqlDbType.Int32, 2));
                            cmd.Parameters["i_doramd_type"].Value = Convert.ToInt32(cbDORAMD_TYPE.EditValue);
                            cmd.Parameters["i_doramd_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtREMARK.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.Int32, 10));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            //Console.WriteLine(" i_idx           ---> [" + txtIDX1.EditValue + "]");
                            //Console.WriteLine(" i_gr_u_id       ---> [" + txtGR_U_ID.EditValue + "]");
                            //Console.WriteLine(" i_md_u_id       ---> [" + txtMD_U_ID.EditValue + "]");
                            //Console.WriteLine(" i_doramd_type   ---> [" + Convert.ToInt16(cbDORAMD_TYPE.EditValue) + "]");
                            //Console.WriteLine(" i_remark        ---> [" + txtREMARK.EditValue + "]");
                            //Console.WriteLine(" o_idx           ---> [" + ParameterDirection.Output + "]");

                            cmd.ExecuteNonQuery();

                            txtIDX1.Text = (cmd.Parameters["o_idx"].Value.ToString());


                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
                MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                Open2();
            }
        }

        private void EfwSimpleButton6_Click(object sender, EventArgs e)
        {
            txtU_NAME1.EditValue = null;
            txtU_NICKNAME1.EditValue = null;
            txtUSER_ID1.EditValue = null;
            txtBIRTH1.EditValue = null;
            txtU_GENDER1.EditValue = null;
            txtU_CELL_NUM1.EditValue = null;
            txtU_EMAIL1.EditValue = null;
            txtLOGIN_DATE1.EditValue = null;
            txtREG_DATE1.EditValue = null;
            txtU_ZIP1.EditValue = null;
            txtU_ADDR1.EditValue = null;
            txtU_ADDR_DETAIL1.EditValue = null;
            cbDORAMD_TYPE.EditValue = null; 
            txtIDX1.EditValue = null;
            txtREMARK.EditValue = null;
            //cmbTAREA1.EditValue = null;


         }

        private void EfwSimpleButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtU_NAME1.Text))
                {
                    return;
                }

                if (string.IsNullOrEmpty(this.cbDORAMD_TYPE.Text))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 프리/로컬 MD 구분을 선택하세요!");
                    return;
                }

                if (cbDORAMD_TYPE.EditValue.ToString() == "1" )
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 지역구는 로컬MD만 지정 가능합니다");
                    return;
                }

                if (cmbSAREA1.EditValue == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 시/도/구군을 선택하세요 ");
                    return;
                }
                if (txtIDX1.EditValue == null)
                {
                    Save4();
                }
                    
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GSHOP_GSHOP06_SAVE_03", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                        cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtAREAIDX.EditValue);
                        cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_md_idx", MySqlDbType.Int32));
                        cmd.Parameters["i_md_idx"].Value = Convert.ToInt32(txtIDX1.EditValue);
                        cmd.Parameters["i_md_idx"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_sido", MySqlDbType.VarChar));
                        cmd.Parameters["i_sido"].Value = cmbTAREA1.EditValue;
                        cmd.Parameters["i_sido"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_gugun", MySqlDbType.VarChar));
                        cmd.Parameters["i_gugun"].Value = cmbSAREA1.EditValue;
                        cmd.Parameters["i_gugun"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_return", MySqlDbType.VarChar));
                        cmd.Parameters["o_return"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        if (cmd.Parameters["o_Return"].Value.ToString() != "")
                        {
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                EfwSimpleButton1_Click(null, null);
            }
            Open3();
        }

        private void Save4()
        {
            if (string.IsNullOrEmpty(this.txtU_NAME1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회원을 선택하세요!");
                return;
            }


            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GSHOP_GSHOP06_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int16, 8));
                            cmd.Parameters["i_idx"].Value = txtIDX1.EditValue;
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gr_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_gr_u_id"].Value = txtGR_U_ID.EditValue;
                            cmd.Parameters["i_gr_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_u_id"].Value = txtMD_U_ID.EditValue;
                            cmd.Parameters["i_md_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_doramd_type", MySqlDbType.Int16, 2));
                            cmd.Parameters["i_doramd_type"].Value = Convert.ToInt16(cbDORAMD_TYPE.EditValue);
                            cmd.Parameters["i_doramd_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtREMARK.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.UInt64, 50));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();


                            txtIDX1.Text = (cmd.Parameters["o_idx"].Value.ToString());

                    }
                    }
            }
             catch (Exception ex)
            {
               MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                EfwSimpleButton1_Click(null, null);
            }
        }

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtU_NAME1.Text))
                {
                    return;
                }

                if (string.IsNullOrEmpty(this.cbDORAMD_TYPE.Text))
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 프리/로컬 MD 구분을 선택하세요!");
                    return;
                }

                if (cbDORAMD_TYPE.EditValue.ToString() == "1")
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 지역구는 로컬MD만 지정 가능합니다");
                    return;
                }

                if (cmbSAREA1.EditValue == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, " 시/도/구군을 선택하세요 ");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GSHOP_GSHOP06_DELETE_04", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                        cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtAREAIDX.EditValue);
                        cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_md_idx", MySqlDbType.Int32));
                        cmd.Parameters["i_md_idx"].Value = Convert.ToInt32(txtIDX1.EditValue);
                        cmd.Parameters["i_md_idx"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_sido", MySqlDbType.VarChar));
                        cmd.Parameters["i_sido"].Value = cmbTAREA1.EditValue;
                        cmd.Parameters["i_sido"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("i_gugun", MySqlDbType.VarChar));
                        cmd.Parameters["i_gugun"].Value = cmbSAREA1.EditValue;
                        cmd.Parameters["i_gugun"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_return", MySqlDbType.VarChar));
                        cmd.Parameters["o_return"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        if (cmd.Parameters["o_Return"].Value.ToString() != "")
                        {
                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                EfwSimpleButton1_Click(null, null);
            }
            Open3();
        }

        private void EfwSimpleButton7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGR_U_ID.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 그룹MD를 선택하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtU_NAME1.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 회원을 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "MD 정보를 삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GSHOP_GSHOP06_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx"].Value = txtIDX1.EditValue;
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gr_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_gr_u_id"].Value = txtGR_U_ID.EditValue;
                            cmd.Parameters["i_gr_u_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_md_u_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_md_u_id"].Value = txtMD_U_ID.EditValue;
                            cmd.Parameters["i_md_u_id"].Direction = ParameterDirection.Input;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
                MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");

                txtU_NAME1.EditValue = null;
                txtU_NICKNAME1.EditValue = null;
                txtUSER_ID1.EditValue = null;
                txtBIRTH1.EditValue = null;
                txtU_GENDER1.EditValue = null;
                txtU_CELL_NUM1.EditValue = null;
                txtU_EMAIL1.EditValue = null;
                txtLOGIN_DATE1.EditValue = null;
                txtREG_DATE1.EditValue = null;
                txtU_ZIP1.EditValue = null;
                txtU_ADDR1.EditValue = null;
                txtU_ADDR_DETAIL1.EditValue = null;
                cbDORAMD_TYPE.EditValue = null;
                txtIDX1.EditValue = null;
                txtREMARK.EditValue = null;
                Eraser.Clear(this, "CLR2");
                Open2();
            }
        }
    }
}
