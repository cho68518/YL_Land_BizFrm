#region "frmGSHOP07 설명"
//===================================================================================================
//■Program Name  : frmGSHOP07
//■Description   : G 멀티샵 MD등록 현황
//■Author        : 송호철
//■Date          : 2019.07.01
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.07.01][송호철] Base
//[2] [2019.07.01][송호철] 
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

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP07 : FrmBase
    {
        public frmGSHOP07()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GSHOP07";
            //폼명설정
            this.FrmName = "도라MD 현황";
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = true;


            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["shop_count"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["shop_count"].SummaryItem.FieldName = "shop_count";
            gridView1.Columns["shop_count"].SummaryItem.DisplayFormat = "합계:   {0}";

            gridView1.Columns["story_count"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["story_count"].SummaryItem.FieldName = "story_count";
            gridView1.Columns["story_count"].SummaryItem.DisplayFormat = "{0}";

            //this.efwGridControl1.BindControlSet(
            //new ColumnControlSet("id", txtID)
            //);
            //this.efwGridControl1.Click += efwGridControl1_Click;
        }


        public override void Search()
        {
            lblDate.Text = DateTime.Now.ToString() + " 현재";
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.USP_GSHOP_GSHOP07_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = cmbSearch_Type.EditValue;

                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtSearch_Name.EditValue;

                        //Console.WriteLine(" i_Search_type           ---> [" + cmd.Parameters[0].Value + "]");
                        //Console.WriteLine(" i_search_Name           ---> [" + cmd.Parameters[1].Value + "]");
                        //Console.WriteLine(" i_member_type           ---> [" + cmd.Parameters[2].Value + "]");

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
