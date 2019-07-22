using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_TELECOM.BizFrm
{
    public partial class frmTM01 : FrmBase
    {
        public frmTM01()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "TM01";
            //폼명설정
            this.FrmName = "회원 탈퇴처리";

            repositoryItemButtonEdit1.ButtonClick += repositoryItemButtonEdit1_ButtonClick;
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

            //GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_write2");

            //gridView1.OptionsView.ShowFooter = true;
            //gridView1.Columns["donut_count"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["donut_count"].SummaryItem.FieldName = "donut_count";
            //gridView1.Columns["donut_count"].SummaryItem.DisplayFormat = "{0:c}";

            //dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            //dt1T.EditValue = DateTime.Now;
            //cmbQ1.EditValue = "1";
            //cmbWriteYn.EditValue = "%";
            //setCmb();
            //chkCmb_Story.CheckAll();
        }



        #endregion

        public override void Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_TM_TM01_SELECT_01"
                    , this.cmbQ1.EditValue
                    , this.txtSearch.Text
                    );

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //MessageBox.Show((string)gridView1.GetFocusedRowCellValue("story_id"));

            string sid       = (string)gridView1.GetFocusedRowCellValue("id");
            string sname     = (string)gridView1.GetFocusedRowCellValue("name");
            string snickname = (string)gridView1.GetFocusedRowCellValue("nickname");
            string sgender   = (string)gridView1.GetFocusedRowCellValue("gender");
            string su_id     = (string)gridView1.GetFocusedRowCellValue("u_id");

            string smsg = "아이디 : " + sid + "\r\n";
            smsg += "이  름 : " + sname + " (" + sgender + ")" + "\r\n";
            smsg += "닉네임 : " + snickname + "\r\n" + "\r\n" + "\r\n";

            if (MessageAgent.MessageShow(MessageType.Confirm, smsg + "회원 탈퇴처리를 하시겠습니까?") == DialogResult.OK)
            {
                memberDel1(sid, su_id);
                //memberDel2(sid);
            }
        }

        private void memberDel1(string pid, string pu_id)
        {
            //[y2k2].[dbo].[Y2K2_member] 업데이트 (아이디 도용 문제가 있을수 있으므로 삭제가 아닌 업데이트처리)
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_TM_TM01_SAVE_01", pid, pu_id);

                if (retVal > 0)
                {
                    MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다.");
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                //MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                Cursor.Current = Cursors.Default;
            }
            finally
            {
                MessageAgent.MessageShow(MessageType.Informational, "처리 되었습니다. 다시 조회 하세요.");
                Cursor.Current = Cursors.Default;
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
