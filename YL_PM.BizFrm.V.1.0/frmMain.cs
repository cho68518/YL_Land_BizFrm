using DevExpress.XtraEditors;
using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using System;

namespace YL_PM.BizFrm
{
    public partial class frmMain : XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void FrmBaseLoad(FrmBase fBase)
        {
            fBase.ShowWaitForm("잠시만 기다려 주십시오.", fBase.FrmName + " 로딩 중입니다.");
            ServiceAgent.IsActProfiler = true;
            DebugAgent.ControlAddFrm(this.efwPnlBody, fBase);
            fBase.CloseWaitForm();
        }

        private void btnTest01_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmPM01());
        }

        private void BtnTest01_Click_1(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmPM01());
        }


        private void BtnTest02_Click_1(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmPM02());
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmPM03());
        }

        private void EfwSimpleButton3_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmPM04());
        }
    }
}
