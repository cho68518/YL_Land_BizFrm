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

namespace YL_TELECOM.BizFrm
{
    public partial class frmTM03 : FrmBase
    {
        public frmTM03()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmTM03";
            //폼명설정
            this.FrmName = "요금제별 적립 현황";


        }
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

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("idx", txtidx)
            , new ColumnControlSet("pay_code", txtpay_code)
            , new ColumnControlSet("pay_name", txtpay_name)
            , new ColumnControlSet("donut_count", txtdonut_count)
            , new ColumnControlSet("vip_donut_count", txtvip_donut_count)
            , new ColumnControlSet("period_donut_count", txtperiod_donut_count)
            , new ColumnControlSet("period_vip_donut_count", txtperiod_vip_donut_count)
            , new ColumnControlSet("recommend_donut_count", txtrecommend_donut_count)
            , new ColumnControlSet("is_use", cbis_use)
            , new ColumnControlSet("al_open_donut_count", txtal_open_donut_count)
            , new ColumnControlSet("basic_price", txtBasic_Price)
            , new ColumnControlSet("Is_Prepaid", rbIs_Prepaid)
            );
            cbis_use.EditValue = "Y";
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
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_pay_code", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtPay_CodeQ.EditValue;

                        cmd.Parameters.Add("i_pay_name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = txtPay_NameQ.EditValue;

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


        private void txtPay_CodeQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void txtPay_NameQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM03_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pay_code", MySqlDbType.VarChar));
                            cmd.Parameters["i_pay_code"].Value = txtpay_code.EditValue;
                            cmd.Parameters["i_pay_code"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_pay_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_pay_name"].Value = txtpay_name.EditValue;
                            cmd.Parameters["i_pay_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_donut_count"].Value = Convert.ToInt32(txtdonut_count.EditValue);
                            cmd.Parameters["i_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_vip_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_vip_donut_count"].Value = Convert.ToInt32(txtvip_donut_count.EditValue);
                            cmd.Parameters["i_vip_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_period_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_period_donut_count"].Value = Convert.ToInt32(txtperiod_donut_count.EditValue);
                            cmd.Parameters["i_period_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_period_vip_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_period_vip_donut_count"].Value = Convert.ToInt32(txtperiod_vip_donut_count.EditValue);
                            cmd.Parameters["i_period_vip_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_recommend_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_recommend_donut_count"].Value = Convert.ToInt32(txtrecommend_donut_count.EditValue);
                            cmd.Parameters["i_recommend_donut_count"].Direction = ParameterDirection.Input;
                            
                            cmd.Parameters.Add(new MySqlParameter("i_is_use", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_use"].Value = cbis_use.EditValue;
                            cmd.Parameters["i_is_use"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_al_open_donut_count", MySqlDbType.Int32));
                            cmd.Parameters["i_al_open_donut_count"].Value = Convert.ToInt32(txtal_open_donut_count.EditValue);
                            cmd.Parameters["i_al_open_donut_count"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_basic_price", MySqlDbType.Int32));
                            cmd.Parameters["i_basic_price"].Value = Convert.ToInt32(txtBasic_Price.EditValue);
                            cmd.Parameters["i_basic_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_is_prepaid", MySqlDbType.VarChar));
                            cmd.Parameters["i_is_prepaid"].Value = rbIs_Prepaid.EditValue;
                            cmd.Parameters["i_is_prepaid"].Direction = ParameterDirection.Input;

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
                Search();
            }
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtidx.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " ID를 선택하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_TM_TM03_DELETE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtidx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

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
                Search();
            }
        }

        private void efwPanelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

