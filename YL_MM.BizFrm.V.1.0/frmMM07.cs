using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
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

namespace YL_MM.BizFrm
{
    public partial class frmMM07 : FrmBase
    {
        frmMM07_Pop01 popup;
        public frmMM07()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "MM07";
            //폼명설정
            this.FrmName = "추천인 관리";
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

            //dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            //dt1T.EditValue = DateTime.Now;
            cmbQ1.EditValue = "1";
            rbLevel.EditValue = "T";

        }
        public override void Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM07_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_qtype", MySqlDbType.VarChar, 20);
                        cmd.Parameters[0].Value = this.cmbQ1.EditValue;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = this.txtSearch.EditValue;

                        if (rbLevel.EditValue.ToString() == "T" )
                            sLevel = null;
                        else
                            sLevel = rbLevel.EditValue.ToString();

                        cmd.Parameters.Add("i_level", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = sLevel;


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

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void BtnDispYes_Click(object sender, EventArgs e)
        {

            popup = new frmMM07_Pop01();

            popup.pIDX = advBandedGridView1.GetFocusedRowCellValue("res_u_id").ToString();
            popup.pLEVEL = advBandedGridView1.GetFocusedRowCellValue("u_chef_level_cd").ToString();

            popup.pSEND_ID = advBandedGridView1.GetFocusedRowCellValue("res_login_id").ToString();
            popup.pU_NAME = advBandedGridView1.GetFocusedRowCellValue("res_u_name").ToString();
            popup.pU_NICKNAME = advBandedGridView1.GetFocusedRowCellValue("res_u_nickname").ToString();


            string sLevel = string.Empty;
            sLevel = advBandedGridView1.GetFocusedRowCellValue("u_chef_level_cd").ToString();

            if (Convert.ToInt16(sLevel.ToString()) >= 3)
            {
                MessageAgent.MessageShow(MessageType.Warning, " 변경할수 없는 등급의 추천인 입니다!");
                return;
            }

            if (sLevel.ToString() == "0")
            {
                popup.pRECV_ID = advBandedGridView1.GetFocusedRowCellValue("res_gen_u_id").ToString();
                popup.pRECV_U_NAME = advBandedGridView1.GetFocusedRowCellValue("res_gen_u_name").ToString();
                popup.pRECV_U_NICKNAME = advBandedGridView1.GetFocusedRowCellValue("res_gen_u_nickname").ToString();
            }
            if (sLevel.ToString() == "1")
            {
                popup.pRECV_ID = advBandedGridView1.GetFocusedRowCellValue("top_u_id").ToString();
                popup.pRECV_U_NAME = advBandedGridView1.GetFocusedRowCellValue("top_name").ToString();
                popup.pRECV_U_NICKNAME = advBandedGridView1.GetFocusedRowCellValue("top_nickname").ToString();
            }
            if (sLevel.ToString() == "2")
            {
                popup.pRECV_ID = advBandedGridView1.GetFocusedRowCellValue("res_chef_u_id").ToString();
                popup.pRECV_U_NAME = advBandedGridView1.GetFocusedRowCellValue("res_chef_u_name").ToString();
                popup.pRECV_U_NICKNAME = advBandedGridView1.GetFocusedRowCellValue("res_chef_u_nickname").ToString();
            }


            popup.pRECV_GEN_ID = advBandedGridView1.GetFocusedRowCellValue("res_gen_u_id").ToString();
            popup.pRECV_GEN_U_NAME = advBandedGridView1.GetFocusedRowCellValue("res_gen_u_name").ToString();
            popup.pRECV_GEN_U_NICKNAME = advBandedGridView1.GetFocusedRowCellValue("res_gen_u_nickname").ToString();

            popup.pRECV_VIP_ID = advBandedGridView1.GetFocusedRowCellValue("top_u_id").ToString();
            popup.pRECV_VIP_U_NAME = advBandedGridView1.GetFocusedRowCellValue("top_name").ToString();
            popup.pRECV_VIP_U_NICKNAME = advBandedGridView1.GetFocusedRowCellValue("top_nickname").ToString();

            popup.pRECV_SHEF_ID = advBandedGridView1.GetFocusedRowCellValue("res_chef_u_id").ToString();
            popup.pRECV_SHEF_U_NAME = advBandedGridView1.GetFocusedRowCellValue("res_chef_u_name").ToString();
            popup.pRECV_SHEF_U_NICKNAME = advBandedGridView1.GetFocusedRowCellValue("res_chef_u_nickname").ToString();

            popup.pDOMA_ID = advBandedGridView1.GetFocusedRowCellValue("res_doma_u_id").ToString();
            popup.pDOMA_U_NAME = advBandedGridView1.GetFocusedRowCellValue("res_doma_u_name").ToString();
            popup.pDOMA_U_NICKNAME = advBandedGridView1.GetFocusedRowCellValue("res_doma_u_nickname").ToString();

            popup.pmd_leader_id = advBandedGridView1.GetFocusedRowCellValue("md_leader_id").ToString();
            popup.pmd_leader_name = advBandedGridView1.GetFocusedRowCellValue("md_leader_name").ToString();
            popup.pmd_leader_nickname = advBandedGridView1.GetFocusedRowCellValue("md_leader_nickname").ToString();
            popup.pmd_leader = advBandedGridView1.GetFocusedRowCellValue("md_leader").ToString();
            // 판매샵 구분
            popup.sale_shop_cd = advBandedGridView1.GetFocusedRowCellValue("sale_shop_cd").ToString();
            popup.pRes_gshop_u_id_co = advBandedGridView1.GetFocusedRowCellValue("res_gshop_u_id_co").ToString();
            popup.pRes_gen_u_id_co = advBandedGridView1.GetFocusedRowCellValue("res_gen_u_id_co").ToString();

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
            //Search();

        }


        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            popup = null;
        }
    }
}
