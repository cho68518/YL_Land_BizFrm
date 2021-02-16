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

namespace YL_GSHOP.BizFrm
{
    public partial class frmTest01 : FrmBase
    {
        public frmTest01()
        {
            InitializeComponent();
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = false;
            this.IsSearch = false;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            efwListBoxControl1.Items.Clear();

            string v_return_key = string.Empty;


            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;

                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domalife.create_random_key", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("p_length", MySqlDbType.Int16));
                        cmd.Parameters["p_length"].Value = 9;
                        cmd.Parameters["p_length"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("p_include_upper_case", true));
                        //cmd.Parameters["p_include_upper_case"].Value = true;
                        //cmd.Parameters["p_include_upper_case"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("p_include_lower_case", false));
                        //cmd.Parameters["p_include_lower_case"].Value = false;
                        //cmd.Parameters["p_include_lower_case"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("p_include_number", true));
                        //cmd.Parameters["p_include_number"].Value = true;
                        //cmd.Parameters["p_include_number"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("p_first_block_type", MySqlDbType.Int16));
                        cmd.Parameters["p_first_block_type"].Value = 0;
                        cmd.Parameters["p_first_block_type"].Direction = ParameterDirection.Input;

                        //cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                        //cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;

                        //v_return_key = cmd.ExecuteNonQuery().ToString();

                        cmd.ExecuteNonQuery();

                        efwListBoxControl1.Items.Add(v_return_key);

                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }




            //for (int i = 1; i < Convert.ToInt32(efwTextEdit1.EditValue) + 1; i++)
            //{
            //    efwListBoxControl1.Items.Add(i.ToString());
            //}
        }
    }
}
