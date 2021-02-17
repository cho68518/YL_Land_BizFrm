using DevExpress.XtraTreeList;
using Easy.Framework.Common;
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
using YL_COMM.BizFrm;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;

namespace YL_DONUT.BizFrm
{
    public partial class frmDN20_Pop02 : FrmPopUpBase
    {
        public frmDN20_Pop02()
        {
            InitializeComponent();
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                //con.Query = "select a.id as ID_Q, a.p_code as P_CODE_Q,  a.p_name as P_NAME_Q,  " +
                //            "       b.idx as IDX_Q, b.name as NAME_Q, b.unit as UNIT_Q, b.qty as QTY_Q, b.endornot as ENDORNOT_Q, b.remark as REMARK_Q, prodseq as PRODSEQ_Q " +
                //            "  from domabiz.table8  " +
                //            "  where a.id = '" + txtSETCODE.EditValue + "'  and " +
                //            "        a.id = b.id  " +
                //            "  order by a.p_name, b.idx ";


                con.Query = "select join_code " +
                            "  from domabiz.table8  ";

                ds = con.selectQueryDataSet();
            }

            ds.WriteXmlSchema("DataSet3.xsd");


            //    //파라매터전달(옵션)

            frmDN20_Report report = new frmDN20_Report();

            report.DataSource = ds.Tables[0];
            report.ShowPreview();

        }
    }
}
