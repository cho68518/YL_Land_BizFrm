#region "frmGM03 설명"
//===================================================================================================
//■Program Name  : frmGSHOP03
//■Description   : 지역별 G멀티샵 등록현황
//■Author        : 송호철
//■Date          : 2019.07.03
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.07.03][송호철] Base
//[2] [2019.07.03][송호철] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

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
    public partial class frmGM03 : FrmBase
    {
        public frmGM03()
        {
            InitializeComponent();
            this.QCode = "GM03";
            //폼명설정
            this.FrmName = "지역별 G멀티샵 등록현황";
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["month1"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month1"].SummaryItem.FieldName = "month1";
            gridView1.Columns["month1"].SummaryItem.DisplayFormat = "{0}";


            gridView1.Columns["month2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month2"].SummaryItem.FieldName = "month2";
            gridView1.Columns["month2"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month3"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month3"].SummaryItem.FieldName = "month3";
            gridView1.Columns["month3"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month4"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month4"].SummaryItem.FieldName = "month4";
            gridView1.Columns["month4"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month5"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month5"].SummaryItem.FieldName = "month5";
            gridView1.Columns["month5"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month6"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month6"].SummaryItem.FieldName = "month6";
            gridView1.Columns["month6"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month7"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month7"].SummaryItem.FieldName = "month7";
            gridView1.Columns["month7"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month8"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month8"].SummaryItem.FieldName = "month8";
            gridView1.Columns["month8"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month9"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month9"].SummaryItem.FieldName = "month9";
            gridView1.Columns["month9"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month10"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month10"].SummaryItem.FieldName = "month10";
            gridView1.Columns["month10"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month11"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month11"].SummaryItem.FieldName = "month11";
            gridView1.Columns["month11"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["month12"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["month12"].SummaryItem.FieldName = "month12";
            gridView1.Columns["month12"].SummaryItem.DisplayFormat = "{0}";

            gridView1.Columns["total"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["total"].SummaryItem.FieldName = "total";
            gridView1.Columns["total"].SummaryItem.DisplayFormat = "{0}";

        }


        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GM_GM03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_year", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 4);



                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

    }
}
