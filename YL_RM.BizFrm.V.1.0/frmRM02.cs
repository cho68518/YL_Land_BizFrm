using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Report;
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
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

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

            //gridView1.OptionsView.ShowFooter = true;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_use");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_notice");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_file");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_comment");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl2, "Y", "N", "is_open");

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
                      new ColumnControlSet("idx", txt_idx)
                      , new ColumnControlSet("board_cd", txt_board_cd2)
                      , new ColumnControlSet("board_name", txt_board_name2)
                      , new ColumnControlSet("subject", chk_is_use)
                      , new ColumnControlSet("content", txt_content)
                      , new ColumnControlSet("is_open", chk_is_file)
                      , new ColumnControlSet("read_cnt", chk_is_comment)
                      , new ColumnControlSet("remark", txt_remark)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;
            this.efwGridControl2.Click += efwGridControl2_Click;

            Open1();
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
                        cmd.Parameters[0].Value = cmbBoard.EditValue;

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

            //chk_is_use.Checked = true;
            //chk_is_notice.Checked = false;
            //chk_is_file.Checked = false;
            //chk_is_comment.Checked = false;
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

            //if (dr != null && dr["FLR"].ToString() != "0" && dr["FLR"].ToString() != "")
            //    this.xtraTabPage3.PageEnabled = true;
            //else
            //    this.xtraTabPage3.PageEnabled = false;
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);

            if (dr != null && dr["content"].ToString() != "0" && dr["content"].ToString() != "")
                this.txt_content.DocumentHtml = dr["content"].ToString();
        }

        private void btn_new1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btn_new2_Click(object sender, EventArgs e)
        {
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

        private void btn_save2_Click(object sender, EventArgs e)
        {
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

                            cmd.Parameters.Add(new MySqlParameter("i_content", MySqlDbType.Text));
                            //cmd.Parameters["i_content"].Value = txt_content.DocumentHtml;
                            cmd.Parameters["i_content"].Value = txt_content.BodyHtml;
                            cmd.Parameters["i_content"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_idx", MySqlDbType.VarChar));
                            cmd.Parameters["o_idx"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();
                            txt_idx2.EditValue = cmd.Parameters["o_idx"].Value.ToString();

                            con.Close();
                        }
                    }

                    MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
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

        private void btn_del2_Click(object sender, EventArgs e)
        {

        }

        private void cmbBoard_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbBoard.EditValue != null && cmbBoard.EditValue.ToString() != "System.Data.DataRowView")
            {
                //MessageBox.Show(cmbBoard.EditValue.ToString());
                txt_board_cd2.EditValue = cmbBoard.EditValue.ToString();
                txt_board_name2.EditValue = cmbBoard.Text.ToString();
                Open2();
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
        }





        #endregion


    }
}
