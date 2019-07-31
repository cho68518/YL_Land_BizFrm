#region "frmDN14 설명"
//===================================================================================================
//■Program Name  : frmDN14
//■Description   : 후기 머니관리
//■Author        : 송호철
//■Date          : 2019.07.30
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.07.30][송호철] Base
//[2] [2019.07.30][송호철] 
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
namespace YL_DONUT.BizFrm
{
    public partial class frmDN14 : FrmBase
    {
        public frmDN14()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "DN14";
            //폼명설정
            this.FrmName = "후기 머니관리";
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
            this.IsExcel = true;

            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("idx", txtIDX)
                    , new ColumnControlSet("product_name", txtPROD_NM)
                    , new ColumnControlSet("customer_price", txtCUSTOMER_PRICE)
                    , new ColumnControlSet("lowest_price", txtLOWEST_PRICE)
                    , new ColumnControlSet("supply_price", txtSUPPLY_PRICE)
                    , new ColumnControlSet("delivery_price", txtDELIVERY_PRICE)
                    , new ColumnControlSet("ps_donut01", txtPS_DONUT01)
                    , new ColumnControlSet("ps_donut02", txtPS_DONUT02)
                    , new ColumnControlSet("vip_price", txtVIP_PRICE)
                    , new ColumnControlSet("ps_price", txtPS_PRICE)
                    , new ColumnControlSet("ps_oper_price", txtPS_OPER_PRICE)
                    , new ColumnControlSet("chef_commission01", txtCHEF_COMMISSION01)
                    , new ColumnControlSet("chef_commission02", txtCHEF_COMMISSION02)
                    , new ColumnControlSet("td_donut", txtTD_DONUT)
                    , new ColumnControlSet("ad_donut", txtAD_DONUT)
                    , new ColumnControlSet("reco_donut", txtRECO_DONUT)
                   );

            this.efwGridControl1.Click += efwGridControl1_Click;

        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
        }


        public override void Search()
        {
            try
            {
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domamall.USP_DN_DN14_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_prod_nm", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtPRODUCT_NAME.EditValue;

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

        private void TxtPRODUCT_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIDX.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 변경할 상품을 선택하세요!");
                return;
            }


            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domamall.USP_DN_DN14_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_customer_price", MySqlDbType.Int32));
                            cmd.Parameters["i_customer_price"].Value = Convert.ToInt32(txtCUSTOMER_PRICE.EditValue);
                            cmd.Parameters["i_customer_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_lowest_price", MySqlDbType.Int32));
                            cmd.Parameters["i_lowest_price"].Value = Convert.ToInt32(txtLOWEST_PRICE.EditValue);
                            cmd.Parameters["i_lowest_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_supply_price", MySqlDbType.Int32));
                            cmd.Parameters["i_supply_price"].Value = Convert.ToInt32(txtSUPPLY_PRICE.EditValue);
                            cmd.Parameters["i_supply_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_delivery_price", MySqlDbType.Int32));
                            cmd.Parameters["i_delivery_price"].Value = Convert.ToInt32(txtDELIVERY_PRICE.EditValue);
                            cmd.Parameters["i_delivery_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ps_donut01", MySqlDbType.Int32));
                            cmd.Parameters["i_ps_donut01"].Value = Convert.ToInt32(txtPS_DONUT01.EditValue);
                            cmd.Parameters["i_ps_donut01"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ps_donut02", MySqlDbType.Int32));
                            cmd.Parameters["i_ps_donut02"].Value = Convert.ToInt32(txtPS_DONUT02.EditValue);
                            cmd.Parameters["i_ps_donut02"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_vip_price", MySqlDbType.Int32));
                            cmd.Parameters["i_vip_price"].Value = Convert.ToInt32(txtVIP_PRICE.EditValue);
                            cmd.Parameters["i_vip_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ps_price", MySqlDbType.Int32));
                            cmd.Parameters["i_ps_price"].Value = Convert.ToInt32(txtPS_PRICE.EditValue);
                            cmd.Parameters["i_ps_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ps_oper_price", MySqlDbType.Int32));
                            cmd.Parameters["i_ps_oper_price"].Value = Convert.ToInt32(txtPS_OPER_PRICE.EditValue);
                            cmd.Parameters["i_ps_oper_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_chef_commission01", MySqlDbType.Int32));
                            cmd.Parameters["i_chef_commission01"].Value = Convert.ToInt32(txtCHEF_COMMISSION01.EditValue);
                            cmd.Parameters["i_chef_commission01"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_chef_commission02", MySqlDbType.Int32));
                            cmd.Parameters["i_chef_commission02"].Value = Convert.ToInt32(txtCHEF_COMMISSION02.EditValue);
                            cmd.Parameters["i_chef_commission02"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_td_donut", MySqlDbType.Int32));
                            cmd.Parameters["i_td_donut"].Value = Convert.ToInt32(txtTD_DONUT.EditValue);
                            cmd.Parameters["i_td_donut"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_ad_donut", MySqlDbType.Int32));
                            cmd.Parameters["i_ad_donut"].Value = Convert.ToInt32(txtAD_DONUT.EditValue);
                            cmd.Parameters["i_ad_donut"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_reco_donut", MySqlDbType.Int32));
                            cmd.Parameters["i_reco_donut"].Value = Convert.ToInt32(txtRECO_DONUT.EditValue);
                            cmd.Parameters["i_reco_donut"].Direction = ParameterDirection.Input;

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
    }
}
