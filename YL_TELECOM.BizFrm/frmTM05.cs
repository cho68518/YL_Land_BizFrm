﻿using DevExpress.XtraEditors.Repository;
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
    public partial class frmTM05 : FrmBase
    {
        public frmTM05()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmTM05";
            //폼명설정
            this.FrmName = "영업일지 등록";
        }

        private void FrmLoadEvent(object sender, EventArgs e)
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

            //efwLabel1.Font = new Font(efwLabel1.Font, FontStyle.Bold);
            this.efwLabel1.Font = new System.Drawing.Font("맑은고딕", 26, System.Drawing.FontStyle.Bold);
            //this.efwLabel1.Font = new Font(this.efwLabel1.Font, FontStyle.Bold);

            txtCoidNo.EditValue = UserInfo.instance().LOGIN_ID;
            txt_work_man.EditValue = UserInfo.instance().Name;
            gridView1.OptionsView.ShowFooter = true;

            this.efwGridControl1.BindControlSet(
              new ColumnControlSet("idx", txt_idx)
            , new ColumnControlSet("visit_date", dt_visit_date)
            , new ColumnControlSet("work_man", txt_work_man)
            , new ColumnControlSet("agency_name", txt_agency_name)
            , new ColumnControlSet("person", txt_person)
            , new ColumnControlSet("advice_type_cd", cmb_advice_type)
            , new ColumnControlSet("plan_date", dt_plan_date)
            , new ColumnControlSet("hp_no", txt_hp_no)
            , new ColumnControlSet("tel_no", txt_tel_no)
            , new ColumnControlSet("e_mail", txt_e_mail)
            , new ColumnControlSet("visit_area", txt_visit_area)
            , new ColumnControlSet("visit_distant", txt_visit_distant)
            , new ColumnControlSet("content", txt_content)
            , new ColumnControlSet("remark", txt_remark)
            , new ColumnControlSet("move_type", cmb_move_type)
            , new ColumnControlSet("car_mileage", txt_car_mileage)
            , new ColumnControlSet("oil_price", txt_oil_price)
            , new ColumnControlSet("packing_price", txt_packing_price)
            , new ColumnControlSet("gate_price", txt_gate_price)
            , new ColumnControlSet("sales_price", txt_remark)
            );
            rb_date_type.EditValue = "V";
            dt_s_date.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt_e_date.EditValue = DateTime.Now;
            dt_visit_date.EditValue = DateTime.Now;
            dt_plan_date.EditValue = DateTime.Now;

            setCmb();

        }

        private void setCmb()
        {


            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00051'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmb_advice_type, codeArray);
            }
            cmb_advice_type.EditValue = "1";

            using (MySQLConn con = new MySQLConn(ConstantLib.TelConn_Real))
            {
                con.Query = " select '' as DCODE, '선택하세요' DNAME  UNION all SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM erp.tb_common_code where gcode_id = '00002'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmb_move_type, codeArray);
            }
           // cmb_move_type.EditValue = "1";

        }


        public override void NewMode()
        {
            base.NewMode();
            Eraser.Clear(this, "CLR1");


            txtCoidNo.EditValue = UserInfo.instance().LOGIN_ID;
            txt_work_man.EditValue = UserInfo.instance().Name;
            rb_date_type.EditValue = "V";
            dt_visit_date.EditValue = DateTime.Now;
            dt_plan_date.EditValue = DateTime.Now;
            dt_s_date.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt_e_date.EditValue = DateTime.Now;

        }

        private void Open1()
        {
            try
            {
                string sP_SHOW_TYPE = string.Empty;

                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                // using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM04_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_date_type", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = rb_date_type.EditValue;

                        cmd.Parameters.Add("i_s_date", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dt_s_date.EditValue3;

                        cmd.Parameters.Add("i_e_date", MySqlDbType.VarChar, 8);
                        cmd.Parameters[2].Value = dt_e_date.EditValue3;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txt_search.EditValue;

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

        private void efwSimpleButton6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_work_man.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 처리할 권한이 없습니다!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM04_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "P";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txt_idx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_visit_date", MySqlDbType.Datetime));
                            cmd.Parameters["i_visit_date"].Value = dt_visit_date.EditValue;
                            cmd.Parameters["i_visit_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_work_man", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_man"].Value = txt_work_man.EditValue;
                            cmd.Parameters["i_work_man"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_agency_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_agency_name"].Value = txt_agency_name.EditValue;
                            cmd.Parameters["i_agency_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_person", MySqlDbType.VarChar));
                            cmd.Parameters["i_person"].Value = txt_person.EditValue;
                            cmd.Parameters["i_person"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_advice_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_advice_type"].Value = cmb_advice_type.EditValue;
                            cmd.Parameters["i_advice_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_plan_date", MySqlDbType.Datetime));
                            cmd.Parameters["i_plan_date"].Value = dt_plan_date.EditValue;
                            cmd.Parameters["i_plan_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hp_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_hp_no"].Value = txt_hp_no.EditValue;
                            cmd.Parameters["i_hp_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel_no"].Value = txt_tel_no.EditValue;
                            cmd.Parameters["i_tel_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_e_mail", MySqlDbType.VarChar));
                            cmd.Parameters["i_e_mail"].Value = txt_e_mail.EditValue;
                            cmd.Parameters["i_e_mail"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_visit_area", MySqlDbType.VarChar));
                            cmd.Parameters["i_visit_area"].Value = txt_visit_area.EditValue;
                            cmd.Parameters["i_visit_area"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_visit_distant", MySqlDbType.Int32));
                            cmd.Parameters["i_visit_distant"].Value = Convert.ToInt32(txt_visit_distant.EditValue);
                            cmd.Parameters["i_visit_distant"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_content", MySqlDbType.VarChar));
                            cmd.Parameters["i_content"].Value = txt_content.EditValue;
                            cmd.Parameters["i_content"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txt_remark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_move_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_move_type"].Value = cmb_move_type.EditValue;
                            cmd.Parameters["i_move_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_car_mileage", MySqlDbType.VarChar));
                            cmd.Parameters["i_car_mileage"].Value = txt_car_mileage.EditValue;
                            cmd.Parameters["i_car_mileage"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_oil_price", MySqlDbType.Int32));
                            cmd.Parameters["i_oil_price"].Value = Convert.ToInt32(txt_oil_price.EditValue);
                            cmd.Parameters["i_oil_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_packing_price", MySqlDbType.Int32));
                            cmd.Parameters["i_packing_price"].Value = Convert.ToInt32(txt_packing_price.EditValue);
                            cmd.Parameters["i_packing_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gate_price", MySqlDbType.Int32));
                            cmd.Parameters["i_gate_price"].Value = Convert.ToInt32(txt_gate_price.EditValue);
                            cmd.Parameters["i_gate_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sales_price", MySqlDbType.Int32));
                            cmd.Parameters["i_sales_price"].Value = Convert.ToInt32(txt_sales_price.EditValue);
                            cmd.Parameters["i_sales_price"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        private void efwSimpleButton7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_work_man.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 처리할 권한이 없습니다!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.TelConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("erp.USP_TM_TM04_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new MySqlParameter("i_proc_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_proc_type"].Value = "D";
                            cmd.Parameters["i_proc_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_idx", MySqlDbType.Int32));
                            cmd.Parameters["i_idx"].Value = Convert.ToInt32(txt_idx.EditValue);
                            cmd.Parameters["i_idx"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_visit_date", MySqlDbType.Datetime));
                            cmd.Parameters["i_visit_date"].Value = dt_visit_date.EditValue;
                            cmd.Parameters["i_visit_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_work_man", MySqlDbType.VarChar));
                            cmd.Parameters["i_work_man"].Value = txt_work_man.EditValue;
                            cmd.Parameters["i_work_man"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_agency_name", MySqlDbType.VarChar));
                            cmd.Parameters["i_agency_name"].Value = txt_agency_name.EditValue;
                            cmd.Parameters["i_agency_name"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_person", MySqlDbType.VarChar));
                            cmd.Parameters["i_person"].Value = txt_person.EditValue;
                            cmd.Parameters["i_person"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_advice_type", MySqlDbType.VarChar));
                            cmd.Parameters["i_advice_type"].Value = cmb_advice_type.EditValue;
                            cmd.Parameters["i_advice_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_plan_date", MySqlDbType.Datetime));
                            cmd.Parameters["i_plan_date"].Value = dt_plan_date.EditValue;
                            cmd.Parameters["i_plan_date"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_hp_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_hp_no"].Value = txt_hp_no.EditValue;
                            cmd.Parameters["i_hp_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_tel_no", MySqlDbType.VarChar));
                            cmd.Parameters["i_tel_no"].Value = txt_tel_no.EditValue;
                            cmd.Parameters["i_tel_no"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_e_mail", MySqlDbType.VarChar));
                            cmd.Parameters["i_e_mail"].Value = txt_e_mail.EditValue;
                            cmd.Parameters["i_e_mail"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_visit_area", MySqlDbType.VarChar));
                            cmd.Parameters["i_visit_area"].Value = txt_visit_area.EditValue;
                            cmd.Parameters["i_visit_area"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_visit_distant", MySqlDbType.Int32));
                            cmd.Parameters["i_visit_distant"].Value = Convert.ToInt32(txt_visit_distant.EditValue);
                            cmd.Parameters["i_visit_distant"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_content", MySqlDbType.VarChar));
                            cmd.Parameters["i_content"].Value = txt_content.EditValue;
                            cmd.Parameters["i_content"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_remark", MySqlDbType.VarChar));
                            cmd.Parameters["i_remark"].Value = txt_remark.EditValue;
                            cmd.Parameters["i_remark"].Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(new MySqlParameter("i_move_type", MySqlDbType.VarChar));


                            cmd.Parameters["i_move_type"].Value = cmb_move_type.EditValue;
                            cmd.Parameters["i_move_type"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_car_mileage", MySqlDbType.VarChar));
                            cmd.Parameters["i_car_mileage"].Value = txt_car_mileage.EditValue;
                            cmd.Parameters["i_car_mileage"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_oil_price", MySqlDbType.Int32));
                            cmd.Parameters["i_oil_price"].Value = Convert.ToInt32(txt_remark.EditValue);
                            cmd.Parameters["i_oil_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_packing_price", MySqlDbType.Int32));
                            cmd.Parameters["i_packing_price"].Value = Convert.ToInt32(txt_packing_price.EditValue);
                            cmd.Parameters["i_packing_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_gate_price", MySqlDbType.Int32));
                            cmd.Parameters["i_gate_price"].Value = Convert.ToInt32(txt_remark.EditValue);
                            cmd.Parameters["i_gate_price"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("i_sales_price", MySqlDbType.Int32));
                            cmd.Parameters["i_sales_price"].Value = Convert.ToInt32(txt_remark.EditValue);
                            cmd.Parameters["i_sales_price"].Direction = ParameterDirection.Input;

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
                Open1();
            }
        }

        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void txt_work_man_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_agency_name.Focus();
        }

        private void txt_agency_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_person.Focus();
        }

        private void txt_person_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_hp_no.Focus();
        }

        private void txt_hp_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dt_plan_date.Focus();
        }

        private void cmb_advice_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_tel_no.Focus();
        }

        private void dt_plan_date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_e_mail.Focus();
        }

        private void txt_tel_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_remark.Focus();
        }

        private void txt_e_mail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_visit_area.Focus();
        }

        private void txt_visit_area_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmb_move_type.Focus();
        }

        private void txt_content_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_remark.Focus();
        }

        private void txt_remark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                efwSimpleButton6.Focus();
        }

        private void txt_visit_distant_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_car_mileage.Focus();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Open1();
        }

        private void cmb_move_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_visit_distant.Focus();
        }

        private void txt_car_mileage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_oil_price.Focus();
        }

        private void txt_oil_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_packing_price.Focus();
        }

        private void txt_packing_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_gate_price.Focus();
        }

        private void txt_gate_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_sales_price.Focus();
        }

        private void txt_sales_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmb_advice_type.Focus();
        }

        private void txt_remark_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_content.Focus();
        }
    }
}
