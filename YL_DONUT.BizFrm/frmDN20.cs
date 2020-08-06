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

namespace YL_DONUT.BizFrm
{
    public partial class frmDN20 : FrmBase
    {
        frmDN20_Pop01 popup;
        public frmDN20()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmDN20";
            //폼명설정
            this.FrmName = "주문 간편양식";
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

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT d_code as DCODE ,d_name as DNAME  FROM domamall.tb_am_product_delivers order by sort";

                DataSet ds = con.selectQueryDataSet();
                DataTable retDT = ds.Tables[0];

                repositoryItemLookUpEdit1.DataSource = retDT;
                //컨트롤 초기화
                InitCodeControl(repositoryItemLookUpEdit1);

                repositoryItemLookUpEdit1.EndUpdate();

            }

        }

        private void InitCodeControl(object cdControl)
        {
            string DNAME = string.Empty;

            DNAME = "DNAME";

            CodeAgent.InitCodeControl(cdControl, "코드명", "코드", DNAME, "DCODE", "선택하세요");
        }


        public override void Search()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN20_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;

                         using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                           // this.efwGridControl1.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void btnExcelUpdate_Click(object sender, EventArgs e)
        {
            popup = new frmDN20_Pop01();
            popup.ShowDialog();
        }

        public override void Save()
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM04_SAVE_02", con))
                            {

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("i_id", MySqlDbType.VarChar, 50);
                                cmd.Parameters[0].Value = gridView1.GetRowCellValue(i, "id");

                                cmd.Parameters.Add("i_delivery_num", MySqlDbType.VarChar, 50);
                                cmd.Parameters[1].Value = gridView1.GetRowCellValue(i, "o_delivery_num");

                                cmd.Parameters.Add("i_delivery_code", MySqlDbType.VarChar, 2);
                                cmd.Parameters[2].Value = gridView1.GetRowCellValue(i, "delivers");

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


    }
}
