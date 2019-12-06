using DevExpress.XtraEditors;
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

namespace YL_SCM.BizFrm
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

        private void BtnSCM01_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmSCM01());
        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmSCM02());
        }

        private void EfwSimpleButton3_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmSCM03());
        }

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmSCM04());
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmSCM05());
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmSCM06());
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmSCM07());
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmSCM08());
        }
    }
}
