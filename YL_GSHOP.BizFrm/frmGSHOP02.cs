#region "frmGSHOP02 설명"
//===================================================================================================
//■Program Name  : frmGSHOP02
//■Description   : G 멀티샵 구분별 확정처리
//■Author        : 송호철
//■Date          : 2019.06.03
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.06.03][송호철] Base
//[2] [2019.06.03][송호철] 
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
    public partial class frmGSHOP02 : FrmBase
    {
        public frmGSHOP02()
        {

            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GSHOP02";
            //폼명설정
            this.FrmName = "구분별 확정처리";
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
            dtE_DATE.EditValue = DateTime.Now;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            gridView1.OptionsView.ShowFooter = true;

            //gridView1.Columns["o_purchase_cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["o_purchase_cost"].SummaryItem.FieldName = "o_purchase_cost";
            //gridView1.Columns["o_purchase_cost"].SummaryItem.DisplayFormat = "{0:c}";

            //this.efwGridControl1.BindControlSet(
            //new ColumnControlSet("id", txtID)
            //);
            //this.efwGridControl1.Click += efwGridControl1_Click;
            rbCOMFIRM.EditValue = "N";

            setCmb();
        }

        private void setCmb()
        {
            try
            {
               // Dictionary<string, string> myRecord;

                string strQueruy = @"  SELECT
                              T1.DCODE, T1.DNAME
                              FROM
                              (
	                             SELECT ''  DCODE, N'전체'  DNAME
	                             UNION ALL
	                             SELECT CODE    DCODE
                                       ,CODE_NM DNAME
                                 FROM dbo.ETCCODE
	                             WHERE GRP_CODE = 'MEMBER' " + @") T1 ";

                CodeAgent.SetLegacyCode(cmbO_TYPE, strQueruy);
                cmbO_TYPE.EditValue = "";
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }


        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP02_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = cmbO_TYPE.EditValue;

                        if (rbCOMFIRM.EditValue.ToString() != "Y" && rbCOMFIRM.EditValue.ToString() != "N")
                            sCOMFIRM = null;
                        else
                            sCOMFIRM = rbCOMFIRM.EditValue.ToString();

                        cmd.Parameters.Add("i_Fix", MySqlDbType.VarChar, 10);
                        cmd.Parameters[3].Value = sCOMFIRM;

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
        //public override void Save()
        //{
        //    if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
        //    {
        //        try
        //        {
        //            cmbO_TYPE.Focus();
        //            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
        //            {

        //                for (int i = 0; i < gridView1.DataRowCount; i++)
        //                {
        //                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP02_SAVE_01", con))
        //                    {
        //                       // Console.WriteLine("********" + gridView1.GetRowCellValue(i, "is_fix"));


        //                        con.Open();
        //                        cmd.CommandType = CommandType.StoredProcedure;

        //                        cmd.Parameters.Add("i_id", MySqlDbType.Int16, 50);
        //                        cmd.Parameters[0].Value = Convert.ToInt16(gridView1.GetRowCellValue(i, "id"));

        //                        cmd.Parameters.Add("i_is_fix", MySqlDbType.VarChar, 1);
        //                        cmd.Parameters[1].Value = gridView1.GetRowCellValue(i, "is_fix");

        //                        cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 255);
        //                        cmd.Parameters[2].Value = gridView1.GetRowCellValue(i, "remark");

        //                        cmd.ExecuteNonQuery();
        //                        con.Close();
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageAgent.MessageShow(MessageType.Error, ex.ToString());
        //        }
        //    }
        //}


        public override void Save()
        {
            try
            {

                var saveResult = new SaveTableResultInfo() { IsError = true };

                var dt = efwGridControl1.GetChangeDataWithRowState;
                var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][StatusColumn].ToString() == "U")
                    {
                        //Console.WriteLine("------------------------------------------------------------");
                        //Console.WriteLine("[U] " + dt.Rows[i]["o_code"].ToString());
                        using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP02_SAVE_01", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_id", MySqlDbType.Int32, 50);
                                cmd.Parameters[0].Value = Convert.ToInt32(dt.Rows[i]["id"]).ToString();

                                cmd.Parameters.Add("i_is_fix", MySqlDbType.VarChar, 1);
                                cmd.Parameters[1].Value = dt.Rows[i]["is_fix"].ToString();


                                cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 255);
                                cmd.Parameters[2].Value = dt.Rows[i]["remark"].ToString();



                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }




        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (gridView1.IsRowSelected(e.ControllerRow))
                gridView1.SetRowCellValue(e.ControllerRow, gridView1.Columns["chk"], "Y");
            else
                gridView1.SetRowCellValue(e.ControllerRow, gridView1.Columns["chk"], "N");
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            efwGridControl1.Focus();
        }
    }
}
