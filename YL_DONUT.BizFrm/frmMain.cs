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
    }
}
