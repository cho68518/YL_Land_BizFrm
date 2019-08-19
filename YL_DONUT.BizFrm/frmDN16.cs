using Easy.Framework.Common;
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
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl3, "Y", "N", "is_biz");

            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl4, "Y", "N", "is_write1");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl4, "Y", "N", "is_write2");
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl4, "Y", "N", "is_biz");

            this.dt1.EditValue = DateTime.Now;
            this.dt2.EditValue = DateTime.Now;
            this.dt3.EditValue = DateTime.Now;
            this.dt4.EditValue = DateTime.Now;
            this.dt5.EditValue = DateTime.Now;
            this.dt6.EditValue = DateTime.Now;
            this.dt7.EditValue = DateTime.Now;
            this.dt8.EditValue = DateTime.Now;
        }

        #endregion

        public override void Search()
        {
            if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                Open1();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                Open2();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                Open3();
            }
            else if (efwXtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                Open4();
            }
        }

        #region 일반회원가입 조회

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

        #endregion

        #region VIP회원가입 조회

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

        #endregion

        #region 도마셰프 회원가입 조회

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

        #endregion

        #region G멀티샵 회원가입 조회

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

        #endregion

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
    }
}
