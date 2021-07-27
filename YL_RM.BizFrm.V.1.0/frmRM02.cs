using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Common.PopUp;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_RM.BizFrm;
using Tamir.SharpSsh;
using System.Collections;
using System.Net.Sockets;

namespace YL_RM.BizFrm
{
    public partial class frmRM02 : FrmBase
    {
        public frmRM02()
        {
            InitializeComponent();
            this.QCode = "RM02";
            //폼명설정
            this.FrmName = "게시판관리";
        }

        private void frmRM02_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //dtS_DATE.EditValue = DateTime.Now;
            //dtE_DATE.EditValue = DateTime.Now;
            rbis_notice.EditValue = "Y";

            //gridView1.OptionsView.ShowFooter = true;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_use");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_notice");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_file");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_comment");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl2, "Y", "N", "is_open");
            picBanner1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBanner2.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBanner3.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            txtfile1.EditValue = "";

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("idx", txt_idx)
                      , new ColumnControlSet("board_cd", txt_board_cd)
                      , new ColumnControlSet("board_name", txt_board_name)
                      , new ColumnControlSet("is_use", chk_is_use)
                      , new ColumnControlSet("is_notice", chk_is_notice)
                      , new ColumnControlSet("is_file", chk_is_file)
                      , new ColumnControlSet("is_comment", chk_is_comment)
                      , new ColumnControlSet("remark", txt_remark)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;

            this.efwGridControl2.BindControlSet(
                      new ColumnControlSet("idx", txt_idx2)
                      , new ColumnControlSet("board_cd", txt_board_cd2)
                      , new ColumnControlSet("board_name", txt_board_name2)
                      , new ColumnControlSet("subject", chk_is_use)
                      , new ColumnControlSet("content", txt_content)
                      , new ColumnControlSet("is_open", chk_is_file)
                      , new ColumnControlSet("read_cnt", chk_is_comment)
                      , new ColumnControlSet("img_url1", txtimg_url1)
                      , new ColumnControlSet("img_url2", txtimg_url2)
                      , new ColumnControlSet("img_url3", txtimg_url3)
                      , new ColumnControlSet("file1", txtfile1)
                      , new ColumnControlSet("subject", txtsubject)
                      , new ColumnControlSet("is_notice", rbis_notice)
                      , new ColumnControlSet("summury", txtsummury)
                      , new ColumnControlSet("board_type", rbboard_type)
                      , new ColumnControlSet("is_file", chkis_file)
                      , new ColumnControlSet("notice_seq", cknotice_seq)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;
            this.efwGridControl2.Click += efwGridControl2_Click;

           // Open1();
            Clear();
            SetCmb();
        }

        #region 조회

        private void Open1()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM02_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sch", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txt_sch1.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Open2()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM02_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_board_cd", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbBoard.EditValue.ToString();

                        cmd.Parameters.Add("i_sch", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txt_sch2.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Clear()
        {
            Eraser.Clear(this, "ER1");

            chk_is_use.Checked = true;
            chk_is_notice.Checked = false;
            chk_is_file.Checked = false;
            chk_is_comment.Checked = false;
        }

        private void Clear2()
        {
            Eraser.Clear(this, "ER2");
            txt_content.BodyHtml = "[[IMG_1]]<br /><br />[[IMG_2]]<br /><br />[[IMG_3]]";
            picBanner1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBanner2.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBanner3.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            //chk_is_use.Checked = true;
            //chk_is_notice.Checked = false;
            //chk_is_file.Checked = false;
            //chk_is_comment.Checked = false;
            txtimg_url1.EditValue = "";
            txtimg_url2.EditValue = "";
            txtimg_url3.EditValue = "";
        }

        #endregion



        #region 이벤트

        private void btnSch1_Click(object sender, EventArgs e)
        {
            //게시판명 조회
            Open1();
        }

        private void btnSch2_Click(object sender, EventArgs e)
        {
            //게시판글 검색
            Open2();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            //if (dr != null && dr["FLR"].ToString() != "0" && dr["content"].ToString() != "")
            //    this.xtraTabPage3.PageEnabled = true;
            //else
            //    this.xtraTabPage3.PageEnabled = false;
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["content"].ToString() != "0" && dr["content"].ToString() != "")
            {
                txt_content.BodyHtml = dr["content"].ToString();
                if (dr != null && dr["img_url1"].ToString() != "0" && dr["img_url1"].ToString() != "")
                {
                    picBanner1.LoadAsync(dr["img_url1"].ToString());
                }
                else
                {
                    picBanner1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
                }
                if (dr != null && dr["img_url2"].ToString() != "0" && dr["img_url2"].ToString() != "")
                {
                    picBanner2.LoadAsync(dr["img_url2"].ToString());
                }
                else
                {
                    picBanner2.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
                }
                if (dr != null && dr["img_url3"].ToString() != "0" && dr["img_url3"].ToString() != "")
                {
                    picBanner3.LoadAsync(dr["img_url3"].ToString());
                }
                else
                {
                    picBanner3.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
                }

                if (dr != null && dr["is_notice"].ToString() != "0" && dr["is_notice"].ToString() != "")
                {
                    rbis_notice.EditValue = (dr["is_notice"].ToString());
                }
                if (dr != null )
                {
                    cknotice_seq.EditValue = (dr["notice_seq"].ToString());
                }

                if (dr != null && dr["board_type"].ToString() != "0" && dr["board_type"].ToString() != "")
                {
                    rbboard_type.EditValue = (dr["board_type"].ToString());
                }
            }                
        }
        private void btn_new1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btn_new2_Click(object sender, EventArgs e)
        {
            rbis_notice.EditValue = "Y";
            picBanner1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBanner2.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            picBanner3.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
            txtfile1.EditValue = "";
            txtsubject.EditValue = "";
            txt_idx2.EditValue = "";
            txtimg_url1.EditValue = "";
            txtimg_url2.EditValue = "";
            txtimg_url3.EditValue = "";
            txtsubject.EditValue = "";
            txtfile1.EditValue = "";
            txtsummury.EditValue = "";
            Clear2();

        }

        private void btn_save1_Click(object sender, EventArgs e)
        {
            if (ValidationAgentEx.IsRequireCheck(this.layoutControl1.Controls, "R1"))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM02_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_userid", MySqlDbType.VarChar));
                            cmd.Parameters["i_userid"].Value = UserInfo.instance().UserId;
                            cmd.Parameters["i_userid"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx"].Value = txt_idx.EditValue;
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_board_cd", MySqlDbType.VarChar));
                            cmd.Parameters["i_board_cd"].Value = txt_board_cd.EditValue;
                            cmd.Parameters["i_board_cd"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_board_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_board_name"].Value = txt_board_name.EditValue;
                            cmd.Parameters["i_board_name"].Direction = ParameterDirection.Input;

                            string s_is_use = string.Empty;
                            if (chk_is_use.Checked == true)
                                s_is_use = "Y";
                            else
                                s_is_use = "N";
                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = s_is_use;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            string s_is_notice = string.Empty;
                            if (chk_is_notice.Checked == true)
                                s_is_notice = "Y";
                            else
                                s_is_notice = "N";
                            cmd.Parameters.Add(new MySqlParameter("i_is_notice", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_notice"].Value = s_is_notice;
                            cmd.Parameters["i_is_notice"].Direction = ParameterDirection.Input;

                            string s_is_file = string.Empty;
                            if (chk_is_file.Checked == true)
                                s_is_file = "Y";
                            else
                                s_is_file = "N";
                            cmd.Parameters.Add(new MySqlParameter("i_is_file", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_file"].Value = s_is_file;
                            cmd.Parameters["i_is_file"].Direction = ParameterDirection.Input;

                            string s_is_comment = string.Empty;
                            if (chk_is_comment.Checked == true)
                                s_is_comment = "Y";
                            else
                                s_is_comment = "N";
                            cmd.Parameters.Add(new MySqlParameter("i_is_comment", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_comment"].Value = s_is_comment;
                            cmd.Parameters["i_is_comment"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txt_remark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.VarChar));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();
                            txt_idx.EditValue = cmd.Parameters["o_idx"].Value.ToString();

                            con.Close();
                        }
                    }

                    MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                    Cursor.Current = Cursors.Default;
                    //Clear();
                    Open1();
                    SetCmb();
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void save2()
        {
            if (string.IsNullOrEmpty(this.txt_board_cd2.Text) ^ txt_board_cd2.EditValue.ToString() == "01")
            {
                MessageAgent.MessageShow(MessageType.Warning, "게시판 코드를 선택하세요 !");
                return;
            }

            if (string.IsNullOrEmpty(this.txtsubject.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "글 제목을 입력하세요 !");
                return;
            }
            if (ValidationAgentEx.IsRequireCheck(this.layoutControl1.Controls, "R2"))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM02_SAVE_02", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_userid", MySqlDbType.VarChar));
                            cmd.Parameters["i_userid"].Value = UserInfo.instance().UserId;
                            cmd.Parameters["i_userid"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx"].Value = txt_idx2.EditValue;
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_board_cd", MySqlDbType.VarChar));
                            cmd.Parameters["i_board_cd"].Value = txt_board_cd2.EditValue;
                            cmd.Parameters["i_board_cd"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_subject", MySqlDbType.VarChar));
                            cmd.Parameters["i_subject"].Value = txtsubject.EditValue;
                            cmd.Parameters["i_subject"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_content", MySqlDbType.Text));
                            //cmd.Parameters["i_content"].Value = txt_content.DocumentHtml;
                            cmd.Parameters["i_content"].Value = txt_content.BodyHtml;
                            cmd.Parameters["i_content"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_img_url1", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url1"].Value = txtimg_url1.EditValue;
                            cmd.Parameters["i_img_url1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_img_url2", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url2"].Value = txtimg_url2.EditValue;
                            cmd.Parameters["i_img_url2"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_img_url3", MySqlDbType.VarChar));
                            cmd.Parameters["i_img_url3"].Value = txtimg_url3.EditValue;
                            cmd.Parameters["i_img_url3"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_file1", MySqlDbType.VarChar));
                            cmd.Parameters["i_file1"].Value = txtfile1.EditValue;
                            cmd.Parameters["i_file1"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_notice", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_notice"].Value = rbis_notice.EditValue;
                            cmd.Parameters["i_is_notice"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_summury", MySqlDbType.VarChar));
                            cmd.Parameters["i_summury"].Value = txtsummury.EditValue;
                            cmd.Parameters["i_summury"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_board_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_board_type"].Value = rbboard_type.EditValue;
                            cmd.Parameters["i_board_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_file", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_file"].Value = chkis_file.EditValue;
                            cmd.Parameters["i_is_file"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_notice_seq", MySqlDbType.VarChar));
                            cmd.Parameters["i_notice_seq"].Value = cknotice_seq.EditValue;
                            cmd.Parameters["i_notice_seq"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.VarChar));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;



                            cmd.ExecuteNonQuery();
                            txt_idx2.EditValue = cmd.Parameters["o_idx"].Value.ToString();

                            con.Close();
                        }
                    }

                    //MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                    Cursor.Current = Cursors.Default;
                    //Clear();
                    Open2();
                    //SetCmb();
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void btn_save2_Click(object sender, EventArgs e)
        {
            save2();
        }

        private void btn_del2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_idx2.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "게시판 코드를 선택하세요 !");
                return;
            }

            if (ValidationAgentEx.IsRequireCheck(this.layoutControl1.Controls, "R2"))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_RM02_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.VarChar));
                            cmd.Parameters["i_idx"].Value = txt_idx2.EditValue;
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;


                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                    Cursor.Current = Cursors.Default;
                    //Clear();
                    rbis_notice.EditValue = "Y";
                    picBanner1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
                    picBanner2.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
                    picBanner3.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
                    txtfile1.EditValue = "";
                    txtsubject.EditValue = "";
                    txt_idx2.EditValue = "";
                    txtimg_url1.EditValue = "";
                    txtimg_url2.EditValue = "";
                    txtimg_url3.EditValue = "";
                    Clear2();

                    Open2();
                    //SetCmb();
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    Cursor.Current = Cursors.Default;
                }

            }
        }
        private void cmbBoard_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbBoard.EditValue != null && cmbBoard.EditValue.ToString() != "System.Data.DataRowView")
            {
                //MessageBox.Show(cmbBoard.EditValue.ToString());
                txt_board_cd2.EditValue = cmbBoard.EditValue.ToString();
                txt_board_name2.EditValue = cmbBoard.Text.ToString();
                txt_idx2.EditValue = "";
                txtsubject.EditValue = "";
                txtfile1.EditValue = "";

                //if (cmbBoard.EditValue.ToString() == "2")
                //    rbboard_type.Enabled = true;
                //else
                //    rbboard_type.Enabled = false;

                if (cmbBoard.EditValue.ToString() == "2")
                    rbboard_type_hid.Enabled  = true;
                else
                    rbboard_type_hid.Enabled = false;


                if (cmbBoard.EditValue.ToString() == "3")
                    chkis_file.Enabled = true;
                else
                    chkis_file.Enabled = false;

                if (cmbBoard.EditValue.ToString() == "2")
                    chkis_file.EditValue = "Y";
                else
                    chkis_file.EditValue = "N";

                if (cmbBoard.EditValue.ToString() == "5")
                {
                    efwSimpleButton7.Enabled = false;
                    efwSimpleButton7.Enabled = true;
                }
                else
                {
                    efwSimpleButton7.Enabled = false;
                }
                   

                if (cmbBoard.EditValue.ToString() == "5")
                {
                    cknotice_seq.Enabled = false;
                    cknotice_seq.Enabled = true;
                }    
                else
                {
                    cknotice_seq.Enabled = false;
                }
                    

                Open2();
                Clear2();
            }
        }

        #endregion

        #region 메서드

        private void SetCmb()
        {
            try
            {
                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = " SELECT board_cd as DCODE ,board_name as DNAME  FROM domalife.g_board where is_use = 'Y' order by board_cd ";

                    DataSet ds = con.selectQueryDataSet();
                    //DataTable retDT = ds.Tables[0];
                    DataRow[] dr = ds.Tables[0].Select();
                    CodeData[] codeArray = new CodeData[dr.Length];

                    for (int i = 0; i < dr.Length; i++)
                        codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                    CodeAgent.MakeCodeControl(this.cmbBoard, codeArray);
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            cmbBoard.EditValue = "01";
        }






        #endregion

        private void btnFileOpen4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_idx2.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "내용을 저장후 이미지를 선택하세요 !");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.jpg";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtPicPath1.EditValue = openFileDialog.FileName;
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

                string sOldFile = txtPicPath1.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/ghomepage/";
                string sFtpPath2 = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == Convert.ToString(txt_idx2.EditValue).ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtimg_url1.EditValue = "https://media.domalife.net/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue) + "/" + sFileName;
                save2();

            }

        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_idx2.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "내용을 저장후 이미지를 선택하세요 !");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.jpg";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtPicPath1.EditValue = openFileDialog.FileName;
                    picBanner2.LoadAsync(openFileDialog.FileName);
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
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/ghomepage/";
                string sFtpPath2 = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == Convert.ToString(txt_idx2.EditValue).ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtimg_url2.EditValue = "https://media.domalife.net/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue) + "/" + sFileName;

                save2();
            }

        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txt_idx2.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "내용을 저장후 이미지를 선택하세요 !");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "jpg";
            openFileDialog.FileName = "*.jpg";
            openFileDialog.Filter = "이미지파일|*.jpg";
            openFileDialog.Title = "이미지파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    txtPicPath1.EditValue = openFileDialog.FileName;
                    picBanner3.LoadAsync(openFileDialog.FileName);
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
                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/ghomepage/";
                string sFtpPath2 = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == Convert.ToString(txt_idx2.EditValue).ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }

                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtimg_url3.EditValue = "https://media.domalife.net/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue) + "/" + sFileName;
                save2();

            }



        }
    

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_idx2.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "내용을 저장후 이미지를 선택하세요 !");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "pdf";
            openFileDialog.FileName = "*.*";
            openFileDialog.Filter = "이미지파일|*.jpg";
            openFileDialog.Title = "파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtfile1.EditValue = openFileDialog.SafeFileName;
                    txtPicPath1.EditValue = openFileDialog.FileName;
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
                string sFileName = txtfile1.EditValue.ToString();


                // 삭제 - 먼저 삭제할 파일을 FileInfo로 연다.
                FileInfo fileDel = new FileInfo(@"C:\\temp\\" + sFileName);
                if (fileDel.Exists) // 삭제할 파일이 있는지
                {
                    fileDel.Delete(); // 없어도 에러안남
                }

                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/ghomepage/";
                string sFtpPath2 = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == Convert.ToString(txt_idx2.EditValue).ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }



                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtfile1.EditValue = "https://media.domalife.net/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue) + "/" + sFileName;


                save2();
            }
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            txtimg_url1.EditValue = "";
            picBanner1.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            txtimg_url2.EditValue = "";
            picBanner2.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            txtimg_url3.EditValue = "";
            picBanner3.LoadAsync("http://media.domalife.net:8080/files/product/donutbiz/mori_00000009/2019101884241596.jpg");
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_idx2.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, "내용을 저장후 이미지를 선택하세요 !");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "pdf";
            openFileDialog.FileName = "*.*";
            openFileDialog.Filter = "이미지파일|*.jpg";
            openFileDialog.Title = "파일 가져오기";

            string sftpURL = "14.63.165.36";
            string sUserName = "root";
            string sPassword = "@dhkdldpf2!";
            int nPort = 22023;
            string sftpDirectory = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    txtfile1.EditValue = openFileDialog.SafeFileName;
                    txtPicPath1.EditValue = openFileDialog.FileName;
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
                //string sFileName = txtfile1.EditValue.ToString();
                string sFileName = Convert.ToString(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".jpg";

                // 삭제 - 먼저 삭제할 파일을 FileInfo로 연다.

                FileInfo fileDel = new FileInfo(@"C:\\temp\\" + sFileName);
                if (fileDel.Exists) // 삭제할 파일이 있는지
                {
                    fileDel.Delete(); // 없어도 에러안남
                }

                string sNewFile = "c:\\temp\\" + sFileName;
                System.IO.File.Copy(sOldFile, sNewFile);


                string LocalDirectory = "C:\\temp"; //Local directory from where the files will be uploaded


                Sftp sSftp = new Sftp(sftpURL, sUserName, sPassword);

                sSftp.Connect(nPort);

                // 저장 경로에 있는지 체크
                string sFtpPath = "/domalifefiles/files/ghomepage/";
                string sFtpPath2 = "/domalifefiles/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue);

                ArrayList ay = sSftp.GetFileList(sFtpPath);
                bool isdir = false;

                for (int i = 0; i < ay.Count; i++)
                {
                    // string sChk = ay[i].ToString();
                    if (ay[i].ToString() == Convert.ToString(txt_idx2.EditValue).ToString())
                    {
                        isdir = true;
                    }
                }
                if (isdir == false)
                {

                    sSftp.Mkdir(sFtpPath2);
                }



                sSftp.Put(LocalDirectory + "/" + sFileName, sftpDirectory + "/" + sFileName);
                sSftp.Close();
                txtsummury.EditValue = "https://media.domalife.net/files/ghomepage/" + Convert.ToString(txt_idx2.EditValue) + "/" + sFileName;


                save2();
            }
        }


    }
}
