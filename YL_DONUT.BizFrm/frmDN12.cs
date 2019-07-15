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

namespace YL_DONUT.BizFrm
{
    public partial class frmDN12 : FrmBase
    {
        public frmDN12()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = " frmDN12";
            //폼명설정
            this.FrmName = "D머니 적립";
        }
    }
}
