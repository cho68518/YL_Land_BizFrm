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
using YL_GSHOP.BizFrm.Dlg;

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP18 : FrmBase
    {
        frmGSHOP08_Pop01 popup;
        public frmGSHOP18()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GSHOP08";
            //폼명설정
            this.FrmName = "MD별 PR체험_스토리 작성현황";
            btnDispYes.ButtonClick += btnDispYes_ButtonClick;
        }


        private void frmGSHOP18_Load(object sender, EventArgs e)
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

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt1T.EditValue = DateTime.Now;

            gridView2.OptionsView.ShowFooter = true;

            gridView2.Columns["cnt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView2.Columns["cnt"].SummaryItem.FieldName = "cnt";
            gridView2.Columns["cnt"].SummaryItem.DisplayFormat = "방문수: {0}";

            this.efwGridControl2.BindControlSet(
               new ColumnControlSet("u_nickname", lb1)
             , new ColumnControlSet("cnt", lb2)
             , new ColumnControlSet("U_ID", txtU_ID)
           );
            this.efwGridControl2.Click += efwGridControl2_Click;


            Search();
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            if (dr != null && dr["u_id"].ToString() != "")
            {
                Open1();
            }
            Cursor.Current = Cursors.Arrow;

        }
        public override void Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sP_SHOW_TYPE = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP18_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt1T.EditValue3;


                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
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

        private void btnDispYes_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //MessageBox.Show((string)gridView1.GetFocusedRowCellValue("String"));
            //object story_id = gridView1.GetFocusedRowCellValue("story_id");
            //object info = gridView1.GetFocusedRowCellValue("category_no");
            //object value = gridView1.GetFocusedRowCellValue("Value");

            long nstory_id = Convert.ToInt64(gridView1.GetFocusedRowCellValue("story_id"));
            string wcategory_no = gridView1.GetFocusedRowCellValue("category_no").ToString();

            //MessageBox.Show(string.Format("{0}: {1}", wstory_id, wcategory_no));

            if (wcategory_no != "240" && wcategory_no != "205" && wcategory_no != "243")
            {
                MessageAgent.MessageShow(MessageType.Error, "상세보기는 PR상담스토리 또는 PR후기스토리만 보실 수 있습니다!");
                return;
            }
            else
            {
                popup = new frmGSHOP08_Pop01();
                //popup.Owner = this;
                popup.pSTORY_ID = nstory_id;
                popup.pLATITUDE = gridView1.GetFocusedRowCellValue("latitude").ToString();
                popup.pLONGITUDE = gridView1.GetFocusedRowCellValue("longitude").ToString();
                popup.pNICKNAME = gridView1.GetFocusedRowCellValue("u_nickname").ToString();
                popup.pNAME = gridView1.GetFocusedRowCellValue("u_name").ToString();
                popup.pLOGIN_ID = gridView1.GetFocusedRowCellValue("u_login_id").ToString();
                popup.pSTORY_NAME = gridView1.GetFocusedRowCellValue("story_name").ToString();
                popup.pREG_DATE = gridView1.GetFocusedRowCellValue("reg_date").ToString();
                popup.pPR_NAME = gridView1.GetFocusedRowCellValue("pr_name").ToString();
                popup.pPR_NAVER_NAME = gridView1.GetFocusedRowCellValue("pr_naver_name").ToString();
                popup.pPR_CELL_NUM = gridView1.GetFocusedRowCellValue("pr_cell_num").ToString();
                popup.pPR_JIBUN_ADDR = gridView1.GetFocusedRowCellValue("pr_jibun_addr").ToString();
                popup.pPR_ROAD_ADDR = gridView1.GetFocusedRowCellValue("pr_road_addr").ToString();
                popup.pCONTENTS = gridView1.GetFocusedRowCellValue("contents").ToString();
                popup.pWK_HAN = gridView1.GetFocusedRowCellValue("wk_han").ToString();
                popup.pCHEF_LEVEL = gridView1.GetFocusedRowCellValue("u_chef_level").ToString();
                popup.pIS_USE = gridView1.GetFocusedRowCellValue("is_use").ToString();
                popup.pCATEGORY_NO = wcategory_no;

                popup.FormClosed += popup_FormClosed;
                popup.ShowDialog();
            }
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;
            popup = null;
        }

        public void Open1()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sP_SHOW_TYPE = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP18_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt1T.EditValue3;

                        cmd.Parameters.Add("i_u_id", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = this.txtU_ID.EditValue;


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

    }
}
