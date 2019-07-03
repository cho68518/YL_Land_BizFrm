﻿using DevExpress.XtraEditors;
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
            FrmBaseLoad(new frmTest01());
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
    }
}
