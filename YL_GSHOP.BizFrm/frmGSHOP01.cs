#region "frmGSHOP01 설명"
//===================================================================================================
//■Program Name  : frmGSHOP01
//■Description   : SET품목관리
//■Author        : 송호철
//■Date          : 2019.05.30
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.05.30][송호철] Base
//[2] [2019.05.30][송호철] 
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
    public partial class frmGSHOP01 : FrmBase
    {
        public frmGSHOP01()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "GSHOP01";
            //폼명설정
            this.FrmName = "단품코드 등록";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw  = true;
            this.IsSearch  = true;
            this.IsNewMode = true;
            this.IsSave    = true;
            this.IsDelete  = true;
            this.IsCancel  = false;
            this.IsPrint   = false;
            this.IsExcel   = false;

            gridView1.OptionsView.ShowFooter = true;

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("ID", txtID)
                    , new ColumnControlSet("P_CODE", txtP_CODE)
                    , new ColumnControlSet("P_NAME", txtP_NAME)
                     );

            this.efwGridControl1.Click += efwGridControl1_Click;

            gridView2.OptionsView.ShowFooter = true;

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl2.BindControlSet(
                      new ColumnControlSet("ID_Q", txtID)
                    , new ColumnControlSet("P_CODE_Q", txtP_CODE)
                    , new ColumnControlSet("P_NAME_Q", txtP_NAME)
                    , new ColumnControlSet("IDX_Q", txtIDX)
                    , new ColumnControlSet("NAME_Q", txtNAME)
                    , new ColumnControlSet("UNIT_Q", txtUNIT)
                    , new ColumnControlSet("QTY_Q", txtQTY)
                    , new ColumnControlSet("ENDORNOT_Q", rbENDORNOT)
                    , new ColumnControlSet("REMARK_Q", txtREMARK)
                     );

            this.efwGridControl2.Click += efwGridControl2_Click;
            rbENDORNOT.EditValue = "Y";
            EfwSimpleButton1_Click(null, null);
        }
        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
        }

        #endregion

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            txtIDX.EditValue = 0;
            txtNAME.Text = "";
            txtUNIT.Text = "";
            txtQTY.EditValue = 1;
            txtREMARK.Text = "";
            rbENDORNOT.EditValue = "Y";
        }

        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);

        }

        public override void Search()
        {
            EfwSimpleButton1_Click(null, null);
        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP01_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_qname", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQ_NAME.EditValue;

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
            Open1();
        }

        private void Open1()
        {
            try
            {
                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP01_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_qname", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQ_NAME.EditValue;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            

        }
        
        public override void Save()
        {
            if (string.IsNullOrEmpty(this.txtP_NAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " SET 품목을 선택하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtNAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 품명을 입력하세요!");
                return;
            }

            
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP01_SAVE_01", con))
                        {
                            con.Open();

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_worktype", MySqlDbType.VarChar));
                            cmd.Parameters["i_worktype"].Value = "A";
                            cmd.Parameters["i_worktype"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                            cmd.Parameters["i_id"].Value = Convert.ToInt32(txtID.EditValue);
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_name"].Value = txtNAME.EditValue;
                            cmd.Parameters["i_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_unit", MySqlDbType.VarChar));
                            cmd.Parameters["i_unit"].Value = txtUNIT.EditValue;
                            cmd.Parameters["i_unit"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_qty"].Value = Convert.ToInt32(txtQTY.EditValue);
                            cmd.Parameters["i_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_endornot", MySqlDbType.VarChar));
                            cmd.Parameters["i_endornot"].Value = rbENDORNOT.EditValue;
                            cmd.Parameters["i_endornot"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value =txtREMARK.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

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
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
            }


        }

        public override void Delete()
        {
            if (string.IsNullOrEmpty(this.txtP_NAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " SET 품목을 선택하세요!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtNAME.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 품명을 선택 하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP01_SAVE_01", con))
                        {
                            con.Open();

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_worktype", MySqlDbType.VarChar));
                            cmd.Parameters["i_worktype"].Value = "D";
                            cmd.Parameters["i_worktype"].Direction = ParameterDirection.Input;


                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txtIDX.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_id", MySqlDbType.Int32));
                            cmd.Parameters["i_id"].Value = Convert.ToInt32(txtID.EditValue);
                            cmd.Parameters["i_id"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_name"].Value = txtNAME.EditValue;
                            cmd.Parameters["i_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_unit", MySqlDbType.VarChar));
                            cmd.Parameters["i_unit"].Value = txtUNIT.EditValue;
                            cmd.Parameters["i_unit"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_qty", MySqlDbType.Int32));
                            cmd.Parameters["i_qty"].Value = Convert.ToInt32(txtQTY.EditValue);
                            cmd.Parameters["i_qty"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_endornot", MySqlDbType.VarChar));
                            cmd.Parameters["i_endornot"].Value = rbENDORNOT.EditValue;
                            cmd.Parameters["i_endornot"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txtREMARK.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

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
                finally
                {
                    EfwSimpleButton1_Click(null, null);
                }
            }


        }
    }
}
