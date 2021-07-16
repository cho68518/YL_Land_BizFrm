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

namespace YL_GM.BizFrm
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

        private void BtnSI0102_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM01());
        }

        private void EfwSimpleButton3_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM02());
        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM03());
        }

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM04());
        }

        private void EfwSimpleButton5_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM05());
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest01());
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM06());
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM07());

        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM08());
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM09());
        }

        private void efwSimpleButton10_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM10());
        }

        private void efwSimpleButton11_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM11());
        }

        private void efwSimpleButton12_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM12());
        }

        private void efwSimpleButton13_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM13());
        }

        private void efwSimpleButton14_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM14());
        }

        private void efwSimpleButton15_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM15());
        }

        private void efwSimpleButton16_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM16());
        }

        private void efwSimpleButton17_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM17());
        }

        private void efwSimpleButton18_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM18());
        }

        private void efwSimpleButton19_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM19());
        }

        private void efwSimpleButton20_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM20());
        }

        private void efwSimpleButton21_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmGM21());
        }
    }
}
