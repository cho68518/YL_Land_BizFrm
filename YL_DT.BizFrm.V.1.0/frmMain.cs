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

namespace YL_DT.BizFrm
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

        private void btnDT01_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDT01());
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDT02());
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDT03());
        }

        private void efwSimpleButton3_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDT04());
        }

        private void efwSimpleButton4_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDT05());
        }

        private void efwSimpleButton5_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDT06());
        }

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            FrmBaseLoad(new frmDT07());
        }
    }
}
