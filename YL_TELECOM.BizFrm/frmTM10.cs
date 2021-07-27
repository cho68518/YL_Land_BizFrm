using DevExpress.XtraEditors.Repository;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using YL_TELECOM.BizFrm.Dlg;

namespace YL_TELECOM.BizFrm
{
    public partial class frmTM10 : FrmBase
    {
        frmTM10_Pop01 popup;
        public frmTM10()
        {
            InitializeComponent();
            this.QCode = "TM10";
            //폼명설정
            this.FrmName = "상품입고 등록";
        }

        private void frmTM10_Load(object sender, EventArgs e)
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

            dtS_DATE.EditValue = DateTime.Now;
            dtE_DATE.EditValue = DateTime.Now;

            gridView1.OptionsView.ShowFooter = true;

            gridView1.Columns["qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["qty"].SummaryItem.FieldName = "qty";
            gridView1.Columns["qty"].SummaryItem.DisplayFormat = "수량: {0}";

            gridView1.Columns["amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["amt"].SummaryItem.FieldName = "amt";
            gridView1.Columns["amt"].SummaryItem.DisplayFormat = "금액: {0:c}";

            this.efwGridControl1.BindControlSet(
               new ColumnControlSet("ser_no", txtser_no)
            );
            this.efwGridControl1.Click += efwGridControl1_Click;
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            if (dr != null && dr["ser_no"].ToString() != "")
            {
                this.txtser_no.EditValue = dr["ser_no"].ToString();
            }

        }

        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM10_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3.Substring(0, 8);

                        cmd.Parameters.Add("i_Search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtSearch.EditValue;


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

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            this.txtser_no.EditValue = dr["ser_no"].ToString();

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM10_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_ser_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_ser_no"].Value = txtser_no.EditValue;
                            cmd.Parameters["i_ser_no"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(cmd.Parameters["o_Return"].Value.ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
                txtser_no.EditValue = "";
                Search();
            }
        }

        private void btnExcelUpdate_Click(object sender, EventArgs e)
        {
            popup = new frmTM10_Pop01();
            popup.ShowDialog();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
