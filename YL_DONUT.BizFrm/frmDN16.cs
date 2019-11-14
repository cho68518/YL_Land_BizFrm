using DevExpress.XtraGrid.Views.Grid;
using Easy.Framework.Common;
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
    public partial class frmDN16 : FrmBase
    {
        public frmDN16()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = " frmDN16";
            //폼명설정
            this.FrmName = "스토리 검증";
        }

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            this.lblRes1.Font = new System.Drawing.Font("맑은고딕", 16, System.Drawing.FontStyle.Bold);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_write1");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "is_write2");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl2, "Y", "N", "is_write1");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl2, "Y", "N", "is_write2");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl2, "Y", "N", "is_write3");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl3, "Y", "N", "is_write1");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl3, "Y", "N", "is_write2");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl3, "Y", "N", "is_write3");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl3, "Y", "N", "is_write4");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl3, "Y", "N", "is_write5");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl3, "Y", "N", "is_biz");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl4, "Y", "N", "is_write1");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl4, "Y", "N", "is_write2");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl4, "Y", "N", "is_biz");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl5, "Y", "N", "is_write1");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl5, "Y", "N", "is_write2");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl5, "Y", "N", "is_write3");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl6, "Y", "N", "is_write1");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl6, "Y", "N", "is_write2");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl6, "Y", "N", "is_write3");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl6, "Y", "N", "is_write4");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl6, "Y", "N", "is_write5");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl7, "Y", "N", "is_write1");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl7, "Y", "N", "is_write2");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl7, "Y", "N", "is_write3");

            advBandedGridView6.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl6.BindControlSet(
                      new ColumnControlSet("u_id", txt_o_u_id)
                    , new ColumnControlSet("o_code", txt_o_code)
                     );

            this.efwGridControl6.Click += efwGridControl6_Click;

            this.dt1.EditValue = DateTime.Now;
            this.dt2.EditValue = DateTime.Now;
            this.dt3.EditValue = DateTime.Now;
            this.dt4.EditValue = DateTime.Now;
            this.dt5.EditValue = DateTime.Now;
            this.dt6.EditValue = DateTime.Now;
            this.dt7.EditValue = DateTime.Now;
            this.dt8.EditValue = DateTime.Now;
            this.dt9.EditValue = DateTime.Now;
            this.dt10.EditValue = DateTime.Now;
            this.dt11.EditValue = DateTime.Now;
            this.dt12.EditValue = DateTime.Now;
            this.dt13.EditValue = DateTime.Now;
            this.dt14.EditValue = DateTime.Now;

            lblRes1.Text = "";
        }

        #endregion

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();  //일반 회원가입
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2(); //VIP 회원가입
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3(); //도마셰프 회원가입
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open4(); //G멀티샵 회원가입
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                Open5(); //알뜰폰개통
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage6)
            {
                Open6(); //프라이빗샵/G샵 (주문)
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage7)
            {
                Open7(); //G+ (G멀티샵 주문)
            }
        }

        #region 일반 회원가입

        private void Open1()
        {
            //일반회원가입
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN16_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt1.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt2.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl1.DataBind(ds);
                            this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void AdvBandedGridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.FieldName == "yn1")
            {
                DataRow row = advBandedGridView1.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date1"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn2")
            {
                DataRow row = advBandedGridView1.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date2"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
        }

        #endregion

        #region VIP 회원가입

        private void Open2()
        {
            //VIP회원가입 조회
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN16_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt3.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt4.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void AdvBandedGridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.FieldName == "yn1")
            {
                DataRow row = advBandedGridView2.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date1"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn2")
            {
                DataRow row = advBandedGridView2.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date2"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn3")
            {
                DataRow row = advBandedGridView2.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date3"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
        }

        #endregion

        #region 도마셰프 회원가입

        private void Open3()
        {
            //VIP회원가입 조회
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN16_SELECT_03", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt5.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt6.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void AdvBandedGridView3_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.FieldName == "yn1")
            {
                DataRow row = advBandedGridView3.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date1"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn2")
            {
                DataRow row = advBandedGridView3.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date2"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn3")
            {
                DataRow row = advBandedGridView3.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date3"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn4")
            {
                DataRow row = advBandedGridView3.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date4"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn5")
            {
                DataRow row = advBandedGridView3.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date5"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
        }

        #endregion

        #region G멀티샵 회원가입

        private void Open4()
        {
            //G멀티샵 회원가입 조회
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN16_SELECT_04", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt7.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt8.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl4.DataBind(ds);
                            this.efwGridControl4.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void AdvBandedGridView4_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.FieldName == "yn1")
            {
                DataRow row = advBandedGridView4.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date1"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn2")
            {
                DataRow row = advBandedGridView4.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date2"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
        }

        #endregion

        #region 알뜰폰개통

        private void Open5()
        {
            //G멀티샵 회원가입 조회
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN16_SELECT_05", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt9.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt10.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl5.DataBind(ds);
                            this.efwGridControl5.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void AdvBandedGridView5_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.FieldName == "yn1")
            {
                DataRow row = advBandedGridView5.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date1"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn2")
            {
                DataRow row = advBandedGridView5.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date2"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn3")
            {
                DataRow row = advBandedGridView5.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date3"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
        }

        #endregion

        #region 프라이빗샵/G샵 (주문)

        private void Open6()
        {
            //G멀티샵 회원가입 조회
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN16_SELECT_06", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt11.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt12.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl6.DataBind(ds);
                            this.efwGridControl6.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void AdvBandedGridView6_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.FieldName == "yn1")
            {
                DataRow row = advBandedGridView6.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date1"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn2")
            {
                DataRow row = advBandedGridView6.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date2"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn3")
            {
                DataRow row = advBandedGridView6.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date3"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn4")
            {
                DataRow row = advBandedGridView6.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date4"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn5")
            {
                DataRow row = advBandedGridView6.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date5"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
        }

        #endregion


        #region G+ (G멀티샵 주문)

        private void Open7()
        {
            //G+ (G멀티샵 주문)
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sLevel = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN16_SELECT_07", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = this.dt13.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = this.dt14.EditValue3;

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);

                            efwGridControl7.DataBind(ds);
                            this.efwGridControl7.MyGridView.BestFitColumns();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void AdvBandedGridView7_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.FieldName == "yn1")
            {
                DataRow row = advBandedGridView7.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date1"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn2")
            {
                DataRow row = advBandedGridView7.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date2"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
            else if (e.IsGetData && e.Column.FieldName == "yn3")
            {
                DataRow row = advBandedGridView7.GetDataRow(e.ListSourceRowIndex);
                if (row == null) return;

                string retVal = row["reg_date3"].ToString();

                if (retVal != "" && retVal != null)
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Apply_16x16;
                else
                    e.Value = YL_DONUT.BizFrm.Properties.Resources.Cancel_16x16;
            }
        }

        #endregion

        private void efwGridControl6_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl6.GetSelectedRow(0);

            lblRes1.Text = string.Empty;

            txt_o_u_id.EditValue = dr["u_id"].ToString();
            txt_o_code.EditValue = dr["o_code"].ToString();

            txt_p_id.EditValue = dr["p_id"].ToString();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_o_u_id.Text) || string.IsNullOrEmpty(this.txt_o_code.Text))
            {
                return;
            }

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "select count(*) " +
                            "  from                      " +
                            "  (                         " +
                            "   select a.id                    as src_idx " +
                            "        , 221                     as contents_type " +
                            "        , b.send_id               as user_id " +
                            "        , c.u_name                as user_name " +
                            "        , c.u_nickname            as user_nickname " +
                            "        , c.login_id              as login_id " +
                            "        , a.o_code                as o_code " +
                            "        , (SELECT p_id FROM domamall.tb_am_product_order_datas where o_code = a.o_code limit 1) as p_id " +
                            "        , b.recv_id               as recomender_u_id       " +
                            "        , d.u_name                as recomender_u_name " +
                            "        , d.u_nickname            as recomender_u_nickname " +
                            "        , d.login_id              as recomender_login_id " +
                            "        , date_format(adddate(a.o_delivery_end_date, interval 72 hour), '%Y-%m-%d 23:59:59') as expiration_date " +
                            "        , 'N'                     as is_write " +
                            "        , a.o_date " +
                            "        , a.o_deposit_confirm_date " +
                            "        , (SELECT IFNULL(group_concat(t.pm_id), 0) FROM domamall.tb_promotion_products t left join domamall.tb_am_product_order_datas tt on t.p_id = tt.p_id where tt.o_code = a.o_code ) as pm_id " +
                            "             , (select IF((select count(1) from domamall.tb_cate_product t  left join domamall.tb_am_product_order_datas tt on t.p_id = tt.p_id where tt.o_code = a.o_code " +
                            "           and (t.c_org_code like 'DC001#DC00115%' or t.c_org_code like 'DC001#DC00120%' or t.c_org_code like 'DC001#DC00121%' or t.c_org_code like 'DC001#DC00122%') ) > 0, 'Y', 'N')) as hangawi_yn " +
                            "        , a.reco_possible_donut as reco_donut " +
                            "        , (select chef_id from  domamall.tb_doma_chef_relations where user_id = b.recv_id and chef_type = 'C' and is_use = 'Y' limit 1) as chef_id " +
                            "     from domamall.tb_am_product_orders a " +
                            "      left join domalife.member_relations b on a.o_u_id  = b.recv_id " +
                            "      left join domalife.member_master c    on b.send_id = c.u_id " +
                            "      inner join domalife.member_master d   on a.o_u_id  = d.u_id " +
                            "    where a.o_type IN ('O','D','F') " +
                            "      and a.o_deposit_confirm_date is not null " +
                            "      and a.order_mall_type = 'PRIVATE' " +
                            "      and a.o_u_id          = '" + txt_o_u_id.EditValue + "' " +
                            "      and a.o_code          = '" + txt_o_code.EditValue + "' " +
                            "   order by a.o_date desc " +
                            "  ) pp " +
                            "where pp.pm_id not like '%3%' " +
                            "  and pp.hangawi_yn = 'N' " +
                            "  and pp.reco_donut > 0 " +
                            "  and pp.user_id != pp.chef_id ";
                DataSet ds = sql.selectQueryDataSet();

                int ncnt = Convert.ToInt32(sql.selectQueryForSingleValue());

                if (ncnt > 0)
                    lblRes1.Text = "생성!";
                else
                    lblRes1.Text = "비생성!";
            }
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            lblRes1.Text = "";
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_o_u_id.Text) || string.IsNullOrEmpty(this.txt_o_code.Text))
            {
                return;
            }

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "select ifnull(td_donut,0) " +
                            "  from domamall.z_product_masters " +
                            " where p_id = " + Convert.ToInt32(txt_p_id.EditValue.ToString());
                DataSet ds = sql.selectQueryDataSet();

                int ncnt = Convert.ToInt32(sql.selectQueryForSingleValue());

                if (ncnt > 0)
                    lblRes1.Text = ncnt.ToString();
                else
                    lblRes1.Text = "0";
            }
        }

        private void EfwGridControl7_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl7.GetSelectedRow(0);

            txtContents.EditValue= null;
            txtStory_id.EditValue = dr["story_id3"].ToString();
        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtStory_id.Text))
            {
                return;
            }

            using (MySQLConn sql = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                sql.Query = "select contents " +
                            "  from domalife.story_list " +
                            " where story_id = " + Convert.ToInt32(txtStory_id.EditValue);
                DataSet ds = sql.selectQueryDataSet();

                txtContents.EditValue = sql.selectQueryForSingleValue();
            }
        }

        private void advBandedGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                e.Handled = true;
            }
        }
    }
}
