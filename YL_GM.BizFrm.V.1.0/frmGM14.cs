using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using YL_GM.BizFrm.Dlg;

namespace YL_GM.BizFrm
{
    public partial class frmGM14 : FrmBase
    {
        public frmGM14()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GM14";
            //폼명설정
            this.FrmName = "회원별 미사용 도넛소멸 현황";
        }
        private void frmGM14_Load(object sender, EventArgs e)
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
            setCmb();
            Clear();
        }
        private void setCmb()
        {

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " SELECT ifnull(code_id,'') as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00054'   ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbDate, codeArray);
            }
            cmbDate.EditValue = "1";
        }

        private void Clear()
        {
            //lblCD.Text = "0";
            lblID.Text = "0";
            cmbType1.EditValue = "GD";
            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01";
            dt1T.EditValue = DateTime.Now;
        }


        public override void Search()
        {
            Cursor.Current = Cursors.WaitCursor;


            if (dt1F.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "기간을 선택하세요!");
                dt1F.Focus();
                return;
            }

            if (dt1T.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "기간을 선택하세요!");
                dt1T.Focus();
                return;
            }

            if (this.cmbType1.EditValue == null)
                this.cmbType1.EditValue = "";

            try
            {
                if (cmbDate.EditValue == "1")
                {
                    DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_GM14_SELECT_03"
                                                                , this.txtQ.Text
                                                                , this.dt1F.EditValue3
                                                                , this.dt1T.EditValue3
                                                                , this.cmbType1.EditValue.ToString().Replace("%", "")
                                                                );
                    efwGridControl1.DataBind(ds);
                }
                else
                {
                    DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_GM14_SELECT_03"
                                                                , this.txtQ.Text
                                                                , this.dt1F.EditValue3
                                                                , this.dt1T.EditValue3
                                                                , this.cmbType1.EditValue.ToString().Replace("%", "")
                                                                );
                    efwGridControl1.DataBind(ds);
                }


                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }

            Cursor.Current = Cursors.Default;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void advBandedGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(advBandedGridView1.GetFocusedDisplayText());
                e.Handled = true;
            }
        }



    }
}
