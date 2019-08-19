using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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

namespace YL_GM.BizFrm.Dlg
{
    public partial class frmGM01_Pop02 : FrmPopUpBase
    {
        public string pQ3 { get; set; }
        public string pMember_NM { get; set; }
        public frmGM01_Pop02()
        {
            InitializeComponent();
        }

        private void FrmGM01_Pop02_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            gridView1.OptionsView.ShowFooter = true;


            efwLabel.Text = pMember_NM;
            txtMember.EditValue = pQ3;
            setCmb();
            Open1();
        }

        private void setCmb()
        {
            try
            {


                //대분류
                string strQueruy = @"SELECT
                                        CODE    DCODE
                                       ,CODE_NM DNAME
                                     FROM ETCCODE
                                    WHERE GRP_CODE = 'MEMBERQ_GBN1' and CODE != 'X'
                                    ORDER BY GRP_CODE ASC";
                CodeAgent.SetLegacyCode(cmbSearch_Type, strQueruy);
                cmbSearch_Type.EditValue = "0";

            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public void Open1()
        {

            try
            {
                string sMem_Level = string.Empty;
                if (this.txtMember.EditValue == null)
                {
                    sMem_Level = "";
                }
                else
                {
                    sMem_Level = this.txtMember.EditValue.ToString().Replace("%", "");
                }

                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GM_GM01_POP02_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_mem_level", MySqlDbType.VarChar, 1);
                        cmd.Parameters[0].Value = sMem_Level;

                        cmd.Parameters.Add("i_Search_type", MySqlDbType.VarChar, 10);
                        cmd.Parameters[1].Value = cmbSearch_Type.EditValue;

                        cmd.Parameters.Add("i_search_Name", MySqlDbType.VarChar, 50);
                        cmd.Parameters[2].Value = txtQName.EditValue;

                        //Console.WriteLine(" i_Search_type           ---> [" + cmd.Parameters[0].Value + "]");
                        //Console.WriteLine(" i_search_Name           ---> [" + cmd.Parameters[1].Value + "]");
                        //Console.WriteLine(" i_member_type           ---> [" + cmd.Parameters[2].Value + "]");

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            Open1();
        }

        private void TxtQName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Open1();
        }
    }
}
