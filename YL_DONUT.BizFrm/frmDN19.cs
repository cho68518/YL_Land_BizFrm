using Easy.Framework.Common;
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
    public partial class frmDN19 : FrmBase
    {
        frmDN07_Pop01 popup;

        public frmDN19()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "DN19";
            //폼명설정
            this.FrmName = "스토리별 머니적립내역";
        }

        #region FrmLoadEvent()

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

            //gridView1.OptionsView.ShowFooter = true;
            //gridView1.Columns["donut_count"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["donut_count"].SummaryItem.FieldName = "donut_count";
            //gridView1.Columns["donut_count"].SummaryItem.DisplayFormat = "{0:c}";

            //gridView2.OptionsView.ShowFooter = true;
            //gridView2.Columns["RECV_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView2.Columns["RECV_AMOUNT"].SummaryItem.FieldName = "RECV_AMOUNT";
            //gridView2.Columns["RECV_AMOUNT"].SummaryItem.DisplayFormat = "사용머니: {0:c}";

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_biz1");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_biz2");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_write");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_use");

            advBandedGridView1.OptionsView.ShowFooter = true;
            advBandedGridView1.Columns["donut_count"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["donut_count"].SummaryItem.FieldName = "donut_count";
            advBandedGridView1.Columns["donut_count"].SummaryItem.DisplayFormat = "{0:c}";

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt1T.EditValue = DateTime.Now;
            cmbQ1.EditValue = "1";
            cmbWriteYn.EditValue = "%";
            setCmb();
            //chkCmb_Story.CheckAll();

            this.cmbStory.EditValue = "";
        }

        #endregion

        private void setCmb()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT category_no as DCODE ,story_name as DNAME  FROM domaadmin.tb_story_info ORDER BY sort_id ";

                DataSet ds = con.selectQueryDataSet();
                ////DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                //// cmbTAREA1.EditValue = "";
                //// cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbStory, codeArray);

               
            }
        }

        public override void Search()
        {
            if (this.cmbStory.EditValue == null || this.cmbStory.EditValue.ToString() == "")
            {
                MessageAgent.MessageShow(MessageType.Error, "스토리를 선택하세요!");
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sP_SHOW_TYPE = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN19_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt1F.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt1T.EditValue3;

                        cmd.Parameters.Add("i_writeyn", MySqlDbType.VarChar, 2);
                        cmd.Parameters[2].Value = this.cmbWriteYn.EditValue;

                        cmd.Parameters.Add("i_qtype", MySqlDbType.VarChar, 2);
                        cmd.Parameters[3].Value = this.cmbQ1.EditValue;

                        cmd.Parameters.Add("i_qtext", MySqlDbType.VarChar, 50);
                        cmd.Parameters[4].Value = this.txtSearch.EditValue;

                        cmd.Parameters.Add("i_qstory", MySqlDbType.Int16, 10);
                        cmd.Parameters[5].Value = Convert.ToInt16(this.cmbStory.EditValue);

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ////MessageBox.Show((string)gridView1.GetFocusedRowCellValue("String"));
            ////object story_id = gridView1.GetFocusedRowCellValue("story_id");
            ////object info = gridView1.GetFocusedRowCellValue("category_no");
            ////object value = gridView1.GetFocusedRowCellValue("Value");

            //long nstory_id = Convert.ToInt64(advBandedGridView1.GetFocusedRowCellValue("story_id"));
            //string wcategory_no = advBandedGridView1.GetFocusedRowCellValue("category_no").ToString();

            ////MessageBox.Show(string.Format("{0}: {1}", wstory_id, wcategory_no));

            //if (wcategory_no != "240" && wcategory_no != "205" && wcategory_no != "223")
            //{
            //    MessageAgent.MessageShow(MessageType.Error, "상세보기는 PR상담스토리, PR후기스토리, PR등록스토리만 보실 수 있습니다!");
            //    return;
            //}
            //else
            //{
            //    popup = new frmDN07_Pop01();
            //    //popup.Owner = this;
            //    popup.pSTORY_ID = nstory_id;
            //    popup.pLATITUDE = "";
            //    popup.pLONGITUDE = "";
            //    popup.pNICKNAME = advBandedGridView1.GetFocusedRowCellValue("u_nickname").ToString();
            //    popup.pNAME = advBandedGridView1.GetFocusedRowCellValue("u_name").ToString();
            //    popup.pLOGIN_ID = advBandedGridView1.GetFocusedRowCellValue("u_login_id").ToString();
            //    popup.pSTORY_NAME = advBandedGridView1.GetFocusedRowCellValue("category_nm").ToString();
            //    popup.pREG_DATE = advBandedGridView1.GetFocusedRowCellValue("reg_date").ToString();
            //    popup.pPR_NAME = "";
            //    popup.pPR_NAVER_NAME = "";
            //    popup.pPR_CELL_NUM = advBandedGridView1.GetFocusedRowCellValue("pr_cell_num").ToString();
            //    popup.pPR_JIBUN_ADDR = advBandedGridView1.GetFocusedRowCellValue("pr_jibun_addr").ToString();
            //    popup.pPR_ROAD_ADDR = advBandedGridView1.GetFocusedRowCellValue("pr_road_addr").ToString();
            //    popup.pCONTENTS = advBandedGridView1.GetFocusedRowCellValue("contents").ToString();
            //    popup.pWK_HAN = advBandedGridView1.GetFocusedRowCellValue("wk_han").ToString();
            //    popup.pCHEF_LEVEL = advBandedGridView1.GetFocusedRowCellValue("u_chef_level").ToString();
            //    popup.pIS_USE = advBandedGridView1.GetFocusedRowCellValue("is_use").ToString();

            //    popup.FormClosed += popup_FormClosed;
            //    popup.ShowDialog();
            //}
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //if (popup.DialogResult == DialogResult.OK)
            //{
            //    this.txtX.EditValue = popup.nX;
            //    this.txtY.EditValue = popup.nY;
            //}

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }

    }
}
