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

namespace YL_MM.BizFrm
{
    public partial class frmMain : XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void FrmBaseLoad(FrmBase fBase)
        {
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            fBase.ShowWaitForm("잠시만 기다려 주십시오.", fBase.FrmName + " 로딩 중입니다.");
            ServiceAgent.IsActProfiler = true;
            DebugAgent.ControlAddFrm(this.efwPnlBody, fBase);
            fBase.CloseWaitForm();
        }

        private void btnMM01_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmMM01());
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest01());
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmMM02());
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmMM03());
        }

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmMM05());
        }

        private void EfwSimpleButton5_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest02());

        }

        private void EfwSimpleButton6_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmMM06());
        }

        private void EfwSimpleButton7_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmMM07());
        }

        private void EfwSimpleButton8_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmMM08());
        }

        private void EfwSimpleButton9_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmMM09());
        }

        private void efwSimpleButton10_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmMM10());
        }
    }
}
