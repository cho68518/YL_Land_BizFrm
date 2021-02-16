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

namespace YL_GSHOP.BizFrm
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

        private void BtnGSHOP01_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP01());
        }

        private void BtnGSHOP02_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP02());
        }

        private void BtnGSHOP03_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP03());
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest02());
        }

        private void BtnGSHOP06_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP06());
        }

        private void BtnGSHOP05_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP05());
        }

        private void BtnGSHOP04_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP04());

        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP07());
        }

        private void EfwSimpleButton3_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP08());
        }

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP09());
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP10());
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP11());
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP12());
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP13());
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP14());
        }

        private void efwSimpleButton10_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP15());
        }

        private void efwSimpleButton11_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGSHOP16());
        }

        private void efwSimpleButton12_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest01());
        }
    }
}
