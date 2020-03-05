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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YL_GSHOP.BizFrm.Dlg;
using DevExpress.XtraGrid.Columns;

namespace YL_GSHOP.BizFrm
{
    public partial class frmGSHOP12 : FrmBase
    {
        public frmGSHOP12()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "GSHOP11";
            //폼명설정
            this.FrmName = "체험 고객선정 등록";


            //    efwGridControl1.DataSource = GetData();

            

        }



        private void frmGSHOP12_Load(object sender, EventArgs e)
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = false;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            gridView1.OptionsView.ShowFooter = true;

            rbP_SHOW_TYPE.EditValue = "T";



            //GridColumn images1 = gridView1.Columns.AddField("Image1");
            //images1.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            //images1.Visible = true;
            //images1.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();


            //GridColumn images2 = gridView1.Columns.AddField("Image2");
            //images2.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            //images2.Visible = true;
            //images2.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();


            //GridColumn images3 = gridView1.Columns.AddField("Image3");
            //images3.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            //images3.Visible = true;
            //images3.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();


            //GridColumn images4 = gridView1.Columns.AddField("Image4");
            //images4.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            //images4.Visible = true;
            //images4.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();


            //GridColumn images5 = gridView1.Columns.AddField("Image5");
            //images5.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            //images5.Visible = true;
            //images5.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();


            SetCmb();

            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
        }
        
        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();

        void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DataRow dr = (e.Row as DataRowView).Row;
            string url = string.Empty;

            if (e.Column.FieldName == "Image1")
            {
                url = dr["pic_url1"].ToString();
            }

            if (e.Column.FieldName == "Image2")
            {
                url = dr["pic_url2"].ToString();
            }
            
            if (e.Column.FieldName == "Image3")
            {
                url = dr["pic_url3"].ToString();
            }

            if (iconsCache.ContainsKey(url))
            {
                e.Value = iconsCache[url];
                return;
            }

            if (url != "")
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        e.Value = Bitmap.FromStream(stream);
                        iconsCache.Add(url, (Bitmap)e.Value);
                    }
                }
            }


        }



        private void SetCmb()
        {
            // 공급자구분

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT code_id as DCODE, code_nm  as DNAME  FROM  domaadmin.tb_common_code where gcode_id = '00033'  ";

                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                // cmbTAREA1.EditValue = "";
                // cmbTAREA1.EditValue = ds.Tables[0].Rows[0]["RESIDENTTYPE"].ToString();

                for (int i = 0; i < dr.Length; i++)
                    codeArray[i] = new CodeData(dr[i]["DCODE"].ToString(), dr[i]["DNAME"].ToString());

                CodeAgent.MakeCodeControl(this.cmbMember_Search, codeArray);
            }

            cmbMember_Search.EditValue = "00";

        }

        public override void Search()
        {
            try
            {
                string sShow_Type = string.Empty;
                string sCOMFIRM = string.Empty;
                string sShowType = string.Empty;
                //using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Dev))
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))

                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_GSHOP_GSHOP13_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("i_sdate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[0].Value = dtS_DATE.EditValue3;

                        cmd.Parameters.Add("i_edate", MySqlDbType.VarChar, 8);
                        cmd.Parameters[1].Value = dtE_DATE.EditValue3;


                        cmd.Parameters.Add("i_Query", MySqlDbType.VarChar, 50);
                        cmd.Parameters[1].Value = cmbMember_Search.EditValue.ToString();


                        if (rbP_SHOW_TYPE.EditValue.ToString() != "Y" && rbP_SHOW_TYPE.EditValue.ToString() != "N")
                            sShow_Type = null;
                        else
                            sShow_Type = rbP_SHOW_TYPE.EditValue.ToString();

                        cmd.Parameters.Add("i_ShowType", MySqlDbType.VarChar, 1);
                        cmd.Parameters[3].Value = sShow_Type;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl1.DataBind(ds);
                            //  this.efwGridControl1.MyGridView.BestFitColumns();
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

 
    }
}
