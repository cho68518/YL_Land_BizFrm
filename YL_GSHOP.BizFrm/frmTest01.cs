using Easy.Framework.Common;
using Easy.Framework.Report;
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

namespace YL_GSHOP.BizFrm
{
    public partial class frmTest01 : FrmBase
    {
        public frmTest01()
        {
            InitializeComponent();
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = true;
            this.IsExcel = false;

        }

        public override void InitPrint()
        {
            ReportAgent rptAgent = new ReportAgent();
            rptAgent.InitPrint = this.InitPrint_01;
            rptAgent.PrintViewerShow();
        }

        public void InitPrint_01()
        {
            //리포트 데이터 초기화
            ReportDataAgent.Clear();

            //리포트파일명 설정(BaseDirectory에 반드시 배포되야 한다.)
            ReportDataAgent.Instance().ReportDocument = "RPT01.rdlc";

            //파라매터전달(옵션)
            string sCOMPANY_CD = "YL01";

            ReportDataAgent.Instance().AddParams("IBS_CD", sCOMPANY_CD);

            try
            {
                //DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_FMS0904_SELECT_01", sIBS_CD);
                DataSet ds = new DataSet();
                ReportDataAgent.Instance().AddDataSource("DataSet3", ds.Tables[0]);
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }


    }
}
