#region "frmMM03 설명"
//===================================================================================================
//■Program Name  : frmMM03
//■Description   : 제품코드
//■Author        : 조정현
//■Date          : 2019.04.19
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.04.19][조정현] Base
//[2] [2019.04.19][조정현] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

using Easy.Framework.Common;
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
    public partial class frmMM03 : FrmBase
    {
        #region Fields

        #endregion

        #region 생성자
        public frmMM03()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MM03";
            //폼명설정
            this.FrmName = "제품코드";

            //UserInfo.instance().ORG_CD;
        }
        #endregion

        #region FrmLoadEvent()
        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsNewMode = false;
            this.IsPrint = false;
            this.IsDelete = false;

        }

        #endregion
    }
}
