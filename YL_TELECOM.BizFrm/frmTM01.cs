using Easy.Framework.Common;
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
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = false;
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

        private void BtnOpen_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
