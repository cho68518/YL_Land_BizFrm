#region "frmGSHOP02 설명"
//===================================================================================================
//■Program Name  : frmGSHOP04
//■Description   : G 멀티샵 구분별  현황
//■Author        : 송호철
//■Date          : 2019.06.12
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.06.12][송호철] Base
//[2] [2019.06.12][송호철] 
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
    public partial class frmGSHOP04 : FrmBase
    {
        public frmGSHOP04()
        {
            InitializeComponent();

             //단축코드 설정 
            this.QCode = "GSHOP04";
            //폼명설정
            this.FrmName = "G멀티샵 현황";

                 


        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;
            
            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            //gridView1.OptionsView.ShowFooter = true;

            //this.efwGridControl1.BindControlSet(
            //          new ColumnControlSet("ID", txtID)
            //        , new ColumnControlSet("SETCODE", txtSETCODE)
            //          );

            //this.efwGridControl1.Click += efwGridControl1_Click;

            //setCmb();
        }

        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");

        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_GSHOP_GSHOP04_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;
                        
                        cmd.Parameters.Add("i_road_addr", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtROAD_ADDR.EditValue;

                        cmd.Parameters.Add("i_rec_u_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtREC_U_NAME.EditValue;

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
        #endregion


        private void TxtREC_U_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void TxtROAD_ADDR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
