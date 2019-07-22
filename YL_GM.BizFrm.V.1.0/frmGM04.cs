﻿#region "frmGM04 설명"
//===================================================================================================
//■Program Name  : frmGM04
//■Description   : 월별매출현황
//■Author        : 송호철
//■Date          : 2019.07.22
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.07.22][송호철] Base
//[2] [2019.07.22][송호철] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

using DevExpress.XtraCharts;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
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
    public partial class frmGM04 : FrmBase
    {
        public frmGM04()
        {
            InitializeComponent();
            this.QCode = "GM04";
            //폼명설정
            this.FrmName = "월별 매출현황";
        }
    }
}
