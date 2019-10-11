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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_MM.BizFrm.Dlg;

namespace YL_MM.BizFrm
{
    public partial class frmMM03 : FrmBase
    {
        #region Fields

        #endregion

        #region 생성자
        public frmMM03()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MM03";
            //폼명설정
            this.FrmName = "제품코드";

            //UserInfo.instance().ORG_CD;
        }
        #endregion

        #region FrmLoadEvent()
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

            gridView1.OptionsView.ShowFooter = true;
            rbShowType.EditValue = "N";

            SetCmb();

        }
        private void SetLegacyCode_Mysql(RepositoryItemLookUpEdit cdControl, string codeGroup)
        {
            DataTable retDT = CodeAgent.GetLegacyCodeCollection(codeGroup);


            cdControl.DataSource = retDT;
            //컨트롤 초기화
            InitCodeControl(cdControl);
        }

        private void InitCodeControl(object cdControl)
        {
            string DNAME = string.Empty;

            DNAME = "DNAME";

            CodeAgent.InitCodeControl(cdControl, "코드명", "코드", DNAME, "DCODE", "선택하세요");
        }

        private void SetCmb()
        {
            // 공급자구분
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT ifnull(s_idx,'') as DCODE ,s_company_name as DNAME  FROM domaadmin.tb_sellers_info where s_status = 'Y' order by s_company_name ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbSellers, codeArray);
            }
            cmbSellers.EditValue = "1";
        }


        public override void Search()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_SCM_SCM04_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_StoreCode", MySqlDbType.Int32);
                        cmd.Parameters[0].Value = Convert.ToInt32(cmbSellers.EditValue);

                        cmd.Parameters.Add("i_ProdName", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtProdName.EditValue;

                        if (rbShowType.EditValue.ToString() != "Y" && rbShowType.EditValue.ToString() != "N")
                            sShow_Type = null;
                        else
                            sShow_Type = rbShowType.EditValue.ToString();

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[4].Value = sShow_Type;
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
        #endregion
    }
}
