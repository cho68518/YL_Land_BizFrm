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

namespace YL_MM.BizFrm
{
    public partial class frmMM27 : FrmBase
    {
        public frmMM27()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = " frmMM27";
            //폼명설정
            this.FrmName = "회원별 머니 이체 현황";
        }

        private void frmMM27_Load(object sender, EventArgs e)
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

            dt1F.EditValue = DateTime.Now.ToString("yyyy-MM") + "-01"; ;
            dt1T.EditValue = DateTime.Now;
            //그리드 컬럼에 체크박스 레포지토리아이템 추가

            advBandedGridView1.OptionsView.ShowFooter = true;

            advBandedGridView1.Columns["amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            advBandedGridView1.Columns["amount"].SummaryItem.FieldName = "amount";
            advBandedGridView1.Columns["amount"].SummaryItem.DisplayFormat = "합계 : {0}";

        }

        //public override void Search()
        //{
        //    try
        //    {

        //        int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MM_MM27_SAVE_01"
        //                                                   , this.dt1F.EditValue3
        //                                                   , this.dt1T.EditValue3
        //                                                  );

        //        if (retVal > 0)
        //        {
        //            using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

        //            {
        //                using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_DN_DN27_SELECT_01", con))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;

        //                    cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
        //                    cmd.Parameters[0].Value = txtQ.EditValue;

        //                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
        //                    {
        //                        DataTable ds = new DataTable();
        //                        sda.Fill(ds);
        //                        efwGridControl1.DataBind(ds);
        //                        this.efwGridControl1.MyGridView.BestFitColumns();
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
        //    }
        //}


        public override void Search()
        {
            try
            {
                base.Search();

                DataSet ds1 = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM27_SELECT_01"
                    , dt1F.EditValue3
                    , dt1T.EditValue3
                    );

                //efwGridControl1.DataBind(ds);
                //   this.efwGridControl1.MyGridView.BestFitColumns();

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_MM_MM27_SELECT_02", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_search", MySqlDbType.VarChar, 50);
                        cmd.Parameters[0].Value = txtQ.EditValue;

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

        private void txtQ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}
