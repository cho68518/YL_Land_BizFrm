#region "frmGSHOP03 설명"
//===================================================================================================
//■Program Name  : frmGSHOP02
//■Description   : G 멀티샵 구분별 확정처리
//■Author        : 송호철
//■Date          : 2019.06.07
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.06.07][송호철] Base
//[2] [2019.06.07][송호철] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Report;
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
    public partial class frmGSHOP03 : FrmBase
    {
        public frmGSHOP03()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "GSHOP03";
            //폼명설정
            this.FrmName = "G멀티샵 사은품출고처리";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = true;
            this.IsExcel = false;

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            dataGridView.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("ID", txtID)
                    , new ColumnControlSet("setcode", txtSETCODE)
                    , new ColumnControlSet("u_name", txtU_NAME)
                    , new ColumnControlSet("member_type", txtMEMBER_TYPE)
                    , new ColumnControlSet("u_cell_num", txtU_CELL_NUM)
                    , new ColumnControlSet("u_addr", txtU_ADDR)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;



            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT d_code as DCODE ,d_name as DNAME  FROM domamall.tb_am_product_delivers";
                
                DataSet ds = con.selectQueryDataSet();
                DataTable retDT = ds.Tables[0];

                repositoryItemLookUpEdit1.DataSource = retDT;
                //컨트롤 초기화
                InitCodeControl(repositoryItemLookUpEdit1);

                repositoryItemLookUpEdit1.EndUpdate();
            }
          

            setCmb();
        }



        private void SetLegacyCode_Mysql(RepositoryItemLookUpEdit cdControl, string codeGroup)
        {
            DataTable retDT = CodeAgent.GetLegacyCodeCollection(codeGroup);


            cdControl.DataSource = retDT;
            //컨트롤 초기화
            InitCodeControl(cdControl);
        }

        private void InitCodeControl(object cdControl)
        {
            string DNAME = string.Empty;

            DNAME = "DNAME";

            CodeAgent.InitCodeControl(cdControl, "코드명", "코드", DNAME, "DCODE", "선택하세요");
        }

        private void setCmb()
        {
            try
            {
              //  Dictionary<string, string> myRecord;

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
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real)) 

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                        cmd.Parameters.Add("i_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = cmbO_TYPE.EditValue;

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

        private void Open1()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP03_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_id", MySqlDbType.Int32, 10);
                        cmd.Parameters[0].Value = txtSETCODE.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
                // id in ( 1781,1780,1779,1793 )
                efwGridControl1.Focus();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public override void Save()
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    cmbO_TYPE.Focus();
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {

                        for (int i = 0; i < dataGridView.DataRowCount; i++)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP03_SAVE_01", con))
                            {
                               // Console.WriteLine("********" + gridView1.GetRowCellValue(i, "is_fix"));


                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_id", MySqlDbType.Int16, 50);
                                cmd.Parameters[0].Value = Convert.ToInt16(dataGridView.GetRowCellValue(i, "id"));

                                cmd.Parameters.Add("i_invoice", MySqlDbType.VarChar, 50);
                                cmd.Parameters[1].Value = dataGridView.GetRowCellValue(i, "invoice");

                                Console.WriteLine("********" + dataGridView.GetRowCellValue(i, "delivers"));

                                cmd.Parameters.Add("i_delivers", MySqlDbType.VarChar, 20);
                                cmd.Parameters[2].Value = dataGridView.GetRowCellValue(i, "delivers");

                                cmd.Parameters.Add("i_remark", MySqlDbType.VarChar, 250);
                                cmd.Parameters[3].Value = dataGridView.GetRowCellValue(i, "remark");

                                cmd.ExecuteNonQuery();
                                con.Close();
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

        #endregion
        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            //DataRow dr = this.efwGridControl1.GetSelectedRow(0);



            Open1();
        }

        public override void InitPrint()
        {
            DataSet ds = new DataSet();

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "select a.id as ID_Q, a.p_code as P_CODE_Q,  a.p_name as P_NAME_Q,  " +
                            "       b.idx as IDX_Q, b.name as NAME_Q, b.unit as UNIT_Q, b.qty as QTY_Q, b.endornot as ENDORNOT_Q, b.remark as REMARK_Q, prodseq as PRODSEQ_Q " +
                            "  from domamall.tb_am_product_masters a,  " +
                            "        domamall.tb_am_setproduct b  " +
                            "  where a.id = '" + txtSETCODE.EditValue + "'  and " +
                            "        a.id = b.id  " +
                            "  order by a.p_name, b.idx ";

                ds = con.selectQueryDataSet();
            }

            ds.WriteXmlSchema("DataSet1.xsd");


            //    //파라매터전달(옵션)
            string sMEMBER_TYPE = txtMEMBER_TYPE.EditValue.ToString();
            string sTEL_NO = txtU_CELL_NUM.EditValue.ToString();
            string sADDR = txtU_ADDR.EditValue.ToString();
            string sU_NAME = txtU_NAME.EditValue.ToString();

            GSHOP03_RPT report = new GSHOP03_RPT();
            report.Parameters["TEL_NO"].Value = sTEL_NO;
            report.Parameters["MEMBER_TYPE"].Value = sMEMBER_TYPE;
            report.Parameters["U_NAME"].Value = sU_NAME;
            report.Parameters["Addr"].Value = sADDR;

            report.DataSource = ds.Tables[0];
            report.ShowPreview();

        }

        private void Button1_Click(object sender, EventArgs e)
        {



            var saveResult = new SaveTableResultInfo() { IsError = true };

            var dt = efwGridControl1.GetChangeDataWithRowState;
            var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;



            for (var i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][StatusColumn].ToString() == "U")
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("[U] " + dt.Rows[i]["chef_nickname"].ToString());
                }
            }

            //var saveResult =  new SaveTableResultInfo() { IsError = true };
            //try
            //{
            //    var dt = efwGridControl1.GetChangeDataWithRowState;
            //    var StatusColumn = Easy.Framework.WinForm.Control.ConstantLib.StatusColumn;

            //    //변경된 데이터 카운트 구하기(차후 메시지 리턴시 사용함)
            //    saveResult.InsertRowcount = dt.Select(StatusColumn + "='I'").Length;
            //    saveResult.UpdateRowcount = dt.Select(StatusColumn + "='U'").Length;
            //    saveResult.DeleteRowcount = dt.Select(StatusColumn + "='D'").Length;
            //    saveResult.TranRowcount = dt.Rows.Count;
            //    var tPack = new TransactionPack();



            //    for (int cnt = 0; cnt < dataGridView.DataRowCount; cnt++)
            //    {
            //        Console.WriteLine("1 ---> [" + dt.Rows[cnt][StatusColumn].ToString() + "]");
            //        Console.WriteLine("2 ---> [" + dt.Rows.Count + "]");
            //        //if (dt.Rows[cnt][StatusColumn].ToString() == "U")
            //        //{
            //        //    Console.WriteLine("3 ---> [" + cnt + "]");
            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            //}
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (dataGridView.IsRowSelected(e.ControllerRow))
                dataGridView.SetRowCellValue(e.ControllerRow, dataGridView.Columns["rowchk"], "U");
            else
                dataGridView.SetRowCellValue(e.ControllerRow, dataGridView.Columns["rowchk"], "");
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            efwGridControl1.Focus();
        }

    }
}

