
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

namespace YL_DONUT.BizFrm
{  
    public partial class frmDN22 : FrmBase
    {
        public frmDN22()
        {
            InitializeComponent();
            this.QCode = "DN22";
            //폼명설정
            this.FrmName = "회원 공지 저장";
        }

        private void frmDN22_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 10);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtReg_date.EditValue = DateTime.Now;
            rbLevel.EditValue = "215";
            rbLevelQ.EditValue = "215";
            txtStory_id.EditValue = 0;
            rbIs_use.EditValue = "Y";


            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("reg_date", dtReg_date)
             ,new ColumnControlSet("is_use", rbIs_use)
             ,new ColumnControlSet("subject", txtSubject)
             ,new ColumnControlSet("contents", txtContents)
             ,new ColumnControlSet("story_id", txtStory_id)
             ,new ColumnControlSet("visit_count", txtVisit_count)
            );

            this.efwGridControl1.Click += efwGridControl1_Click;

        }
        private void efwGridControl1_Click(object sender, EventArgs e)
        {

        }

        public override void NewMode()
        {
            base.NewMode();
            dtReg_date.EditValue = DateTime.Now;
            Eraser.Clear(this, "CLR1");
            txtStory_id.EditValue = 0;
        }

        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN22_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_levelQ", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = rbLevelQ.EditValue.ToString();


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                          //  this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void rbLevelQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbLevelQ.EditValue.ToString() == "215")
            {
                rbLevel.EditValue = "215";
            }
            else if (rbLevelQ.EditValue.ToString() == "238")
            {
                rbLevel.EditValue = "238";
            }
            else if (rbLevelQ.EditValue.ToString() == "400")
            {
                rbLevel.EditValue = "400";
            }

            Search();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN22_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_story_id", MySqlDbType.Int32));
                            cmd.Parameters["i_story_id"].Value = Convert.ToInt32(txtStory_id.EditValue);
                            cmd.Parameters["i_story_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_category_no", MySqlDbType.Int32));
                            cmd.Parameters["i_category_no"].Value = Convert.ToInt32(rbLevel.EditValue);
                            cmd.Parameters["i_category_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_reg_date", MySqlDbType.Datetime));
                            cmd.Parameters["i_reg_date"].Value = dtReg_date.EditValue;
                            cmd.Parameters["i_reg_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = rbIs_use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_subject", MySqlDbType.VarChar));
                            cmd.Parameters["i_subject"].Value = txtSubject.EditValue;
                            cmd.Parameters["i_subject"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_contents", MySqlDbType.LongText));
                            cmd.Parameters["i_contents"].Value = txtContents.EditValue;
                            cmd.Parameters["i_contents"].Direction = ParameterDirection.Input;

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

    }
}
