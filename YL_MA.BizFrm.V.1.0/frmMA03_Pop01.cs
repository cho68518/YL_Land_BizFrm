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
using YL_MA.BizFrm.Dlg;

namespace YL_MA.BizFrm
{
    public partial class frmMA03_Pop01 : FrmPopUpBase
    {
        public string pSDate { get; set; }
        public string pEDate { get; set; }
        public frmMA03_Pop01()
        {
            InitializeComponent();
        }

        private void FrmGM04_Pop01_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            txtSDate.EditValue = pSDate;
            txtEDate.EditValue = pEDate;
            
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["FinalOrderPay"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["FinalOrderPay"].SummaryItem.FieldName = "FinalOrderPay";
            gridView1.Columns["FinalOrderPay"].SummaryItem.DisplayFormat = "{0:c}";

            rbType.EditValue = "1";
            Open1();
        }


        private void Open1()
        {
            try
            {

                //base.Search();
                string sSDate = string.Empty;
                string sEDate = string.Empty;

                sSDate = txtSDate.EditValue + "-01";
                sEDate = txtEDate.EditValue + "-31";

                DataSet ds = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MA03_SELECT_04"
                    , sSDate
                    , sEDate
                    , rbType.EditValue
                    );

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            Open1();
        }
    }
}
