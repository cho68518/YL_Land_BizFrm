using DevExpress.XtraTreeList;
using Easy.Framework.Common;
using Easy.Framework.SrvCommon;
using Easy.Framework.WinForm.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace YL_MM.BizFrm
{
    public partial class Test03 : FrmBase
    {
        public Test03()
        {
            InitializeComponent();
        }

        private void Test03_Load(object sender, EventArgs e)
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
            CreTree();
        }
        private void CreTree()
        {
            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = " select gcode_id, gcode_nm FROM domaadmin.tb_common_code where gcode_id in ( '00042', '00055')  and use_yn = 'Y' group by gcode_id, gcode_nm ";
                DataSet ds = con.selectQueryDataSet();
                DataRow[] drs1 = ds.Tables[0].Select();

                TreeNode rootNode = new TreeNode("기초코드");
                this.treeView1.Nodes.Add(rootNode);
                this.treeView1.ImageList = this.imgOrganList;

                for (int i = 0; i < drs1.Length; i++)
                {
                    Com_CodeNode Onode = new Com_CodeNode(drs1[i]);
                    CreateNode2(Onode);

                    rootNode.Nodes.Add(Onode);

                    if (treeView1.Nodes.Count > 0)
                        treeView1.Nodes[0].Expand();
                }
                this.treeView1.EndUpdate();
            }
        }

        void CreateNode2(Com_CodeNode OrNode)
        {
            if (OrNode == null) return;

            try
            {
                using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
                {
                    con.Query = " select code_id, code_nm FROM domaadmin.tb_common_code where gcode_id = '" + OrNode.gcode_id + "' ";

                    DataSet ds = con.selectQueryDataSet();
                    DataRow[] drs2 = ds.Tables[0].Select();

                    for (int i = 0; i < drs2.Length; i++)
                    {
                        Com_CodeNode1 OrNode2 = new Com_CodeNode1(drs2[i]);
                        OrNode.Nodes.Add(OrNode2);
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
