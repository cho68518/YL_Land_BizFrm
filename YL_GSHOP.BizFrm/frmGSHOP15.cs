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
    public partial class frmGSHOP15 : FrmBase
    {
        public frmGSHOP15()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GSHOP15";
            //폼명설정
            this.FrmName = "발모후기 작성건수 현황";

        }
        private void frmGSHOP12_Load(object sender, EventArgs e)
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

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            gridView1.OptionsView.ShowFooter = true;
            this.efwGridControl1.BindControlSet(
                new ColumnControlSet("story_id", txtStory_ID)
           ); ;

            this.efwGridControl1.Click += efwGridControl1_Click;
        }


        public override void Search()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP12_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_gshop_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtgshop_name.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
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

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            txtInformant.EditValue = "";
            txtStory_ID.EditValue = "";

            if (dr != null && dr["story_id"].ToString() != "")
            {
                this.txtInformant.EditValue = dr["Informant"].ToString();
                this.txtStory_ID.EditValue = dr["story_id"].ToString();
            }
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStory_ID.EditValue.ToString() == "" ^ txtStory_ID.EditValue.ToString() == null)
                {
                    MessageAgent.MessageShow(MessageType.Warning, "변경할 발모후기를 선택하세요!");
                    return;
                }
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {

                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP15_SAVE_01", con))
                    {

                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_story_id", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(txtStory_ID.EditValue.ToString());

                        cmd.Parameters.Add("i_informant", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtInformant.EditValue;

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
            Search();
        }
    }
}
