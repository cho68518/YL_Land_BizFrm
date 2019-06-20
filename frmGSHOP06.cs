﻿using Easy.Framework.Common;
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
    public partial class frmGSHOP06 : FrmBase
    {
        public frmGSHOP06()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "GSHOP06";
            //폼명설정
            this.FrmName = "G멀티샵 MD등록";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = false;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //gridView1.OptionsView.ShowFooter = true;

            //this.efwGridControl1.BindControlSet(
            //          new ColumnControlSet("ID", txtID)
            //        , new ColumnControlSet("SETCODE", txtSETCODE)
            //          );

            //this.efwGridControl1.Click += efwGridControl1_Click;

            //setCmb();

            BtnNew_Click(null, null);
        }





        #endregion

        private void BtnNew_Click(object sender, EventArgs e)
        {
            txtU_NAME.EditValue = null;
            txtU_NICKNAME.EditValue = null;
            txtUSER_ID.EditValue = null;
            txtBIRTH.EditValue = null;
            txtU_GENDER.EditValue = null;
            txtU_CELL_NUM.EditValue = null;
            txtU_EMAIL.EditValue = null;
            txtLOGIN_DATE.EditValue = null;
            txtREG_DATE.EditValue = null;
            txtU_ZIP.EditValue = null;
            txtU_ADDR.EditValue = null;
            txtU_ADDR_DETAIL.EditValue = null;
            cmbU_CHEF_LEVEL.EditValue = null;
            chkIS_AL_FRIEND.Checked = false;
            chkIS_STOCK_FRIEND.Checked = false;
            chkIS_GR_MD.Checked = false;

        }
    }
}
