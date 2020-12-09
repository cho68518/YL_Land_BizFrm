using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.Common.PopUp;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_RM.BizFrm;
using Tamir.SharpSsh;
using System.Collections;
using System.Net.Sockets;


namespace YL_RM.BizFrm
{
    public partial class frmRM04 : FrmBase
    {
        public frmRM04()
        {
            InitializeComponent();
            InitializeComponent();
            this.QCode = "RM04";
            //폼명설정
            this.FrmName = "1:1 문의";

        }

        private void frmRM04_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.efwGridControl1.BindControlSet(
                new ColumnControlSet("idx", txt_idx2)
              , new ColumnControlSet("subject", txtsubject)
              , new ColumnControlSet("is_notice", rbis_notice)
              , new ColumnControlSet("summury", txtcontent)
          );
            this.efwGridControl1.Click += efwGridControl1_Click;
            SetCmb();
        }

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            if (dr != null && dr["content"].ToString() != "0" && dr["content"].ToString() != "")
                txt_content.BodyHtml = dr["content"].ToString();
        }

        private void SetCmb()
        {
            try
            {
                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = " SELECT code_id as DCODE ,code_nm as DNAME  FROM domaadmin.tb_common_code where gcode_id = '00049' order by code_id ";

                    DataSet ds = con.selectQueryDataSet();
                    //DataTable retDT = ds.Tables[0];
                    DataRow[] dr = ds.Tables[0].Select();
                    CodeData[] codeArray = new CodeData[dr.Length];

                    for (int i = 0; i < dr.Length; i++)
                        codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                    CodeAgent.MakeCodeControl(this.cmbBoard_Type, codeArray);
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            cmbBoard_Type.EditValue = "";
        }




    }
}
