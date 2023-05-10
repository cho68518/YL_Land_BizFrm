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

namespace YL_DONUT.BizFrm
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

        private void BtnDN01_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN01());
        }

        private void BtnDN02_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN02());
        }

        private void EfwSimpleButton2_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest01());
        }

        private void BtnDN03_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN03());
        }

        private void BtnDN04_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN04());
        }

        private void BtnDN05_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN05());
        }

        private void BtnDN06_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN06());
        }

        private void BtnDN07_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN07());
        }

        private void BtnDN08_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN08());
        }

        private void BtnDN09_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN09());
        }

        private void BtnDN10_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN10());
        }

        private void BtnDN11_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN11());
        }

        private void BtnDN12_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN12());
        }

        private void BtnDN13_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN13());
        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN14());
        }

        private void EfwSimpleButton3_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN15());
        }

        private void EfwSimpleButton4_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN16());
        }

        private void EfwSimpleButton5_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN17());
        }

        private void EfwSimpleButton6_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN18());
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN19());
        }

        private void efwSimpleButton8_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN20());
        }

        private void efwSimpleButton10_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN21());
        }

        private void efwSimpleButton9_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest02());
        }

        private void efwSimpleButton11_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN22());

        }

        private void efwSimpleButton12_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN24());
        }

        private void efwSimpleButton13_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest04());
        }

        private void efwSimpleButton14_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest03());
        }

        private void efwSimpleButton15_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest100());
        }

        private void efwSimpleButton16_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN25());
        }

        private void efwSimpleButton17_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN26());
        }

        private void efwSimpleButton18_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmTest01());
        }

        private void efwSimpleButton19_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN27());
        }

        private void efwSimpleButton20_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN28());
        }

        private void efwSimpleButton21_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDN29());
        }
    }
}
