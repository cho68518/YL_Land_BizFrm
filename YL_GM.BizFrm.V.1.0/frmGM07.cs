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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_GM.BizFrm
{
    public partial class frmGM07 : FrmBase
    {
        public frmGM07()
        {
            InitializeComponent();
            this.QCode = "GM07";
            //폼명설정
            this.FrmName = "스토리 마스터";
        }

        private void frmGM07_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("id", nID)
                    , new ColumnControlSet("category_no", nCategory_No)
                    , new ColumnControlSet("story_name", txtStory_Name)
                    , new ColumnControlSet("story_category", txtStory_Category)
                    , new ColumnControlSet("story_donut_cost", nStory_Donut_Cost)
                    , new ColumnControlSet("story_donut_type", txtStory_Donut_Type)
                    , new ColumnControlSet("payment_cost", nPayment_Cost)
                    , new ColumnControlSet("expiration_add_time", nExpiration_Add_Time)
                    , new ColumnControlSet("sort_id", nSort_Id)
                    , new ColumnControlSet("comment", txtComment)
                    , new ColumnControlSet("etc", txtEtc)

                      );

            this.efwGridControl1.Click += efwGridControl1_Click;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                string sCom = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = "";

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

        private void efwTextEdit11_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM07_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                            cmd.Parameters["i_id"].Value = Convert.ToInt32(nID.EditValue);
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_category_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_category_no"].Value = Convert.ToInt32(nCategory_No.EditValue);
                            cmd.Parameters["i_category_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_name"].Value = txtStory_Name.EditValue;
                            cmd.Parameters["i_story_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_category", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_category"].Value = txtStory_Category.EditValue;
                            cmd.Parameters["i_story_category"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_donut_cost", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_donut_cost"].Value = Convert.ToInt32(nStory_Donut_Cost.EditValue);
                            cmd.Parameters["i_story_donut_cost"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_story_donut_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_story_donut_type"].Value = txtStory_Donut_Type.EditValue;
                            cmd.Parameters["i_story_donut_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_payment_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_payment_type"].Value = txtPayment_Type.EditValue;
                            cmd.Parameters["i_payment_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_payment_cost", MySqlDbType.VarChar));
                            cmd.Parameters["i_payment_cost"].Value = Convert.ToInt32(nPayment_Cost.EditValue);
                            cmd.Parameters["i_payment_cost"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_expiration_add_time", MySqlDbType.VarChar));
                            cmd.Parameters["i_expiration_add_time"].Value = Convert.ToInt32(nExpiration_Add_Time.EditValue);
                            cmd.Parameters["i_expiration_add_time"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sort_id", MySqlDbType.VarChar));
                            cmd.Parameters["i_sort_id"].Value = Convert.ToInt32(nSort_Id.EditValue);
                            cmd.Parameters["i_sort_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_comment", MySqlDbType.VarChar));
                            cmd.Parameters["i_comment"].Value = txtComment.EditValue;
                            cmd.Parameters["i_comment"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_etc", MySqlDbType.VarChar));
                            cmd.Parameters["i_etc"].Value = txtEtc.EditValue;
                            cmd.Parameters["i_etc"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_return", MySqlDbType.VarChar));
                            cmd.Parameters["o_return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show(cmd.Parameters["o_return"].Value.ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }
    }
}
