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

namespace YL_MA.BizFrm
{
    public partial class frmDeptInfo : DevExpress.XtraEditors.XtraForm
    {
        #region Propertys

        public string COMPANYCD
        {
            get;
            set;
        }

        public string COMPANYNAME
        {
            get;
            set;
        }

        public efwButtonEdit ParentBtn
        {
            get;
            set;
        }

        #endregion

        #region Fields

        DataSet _dsDeptInfo = null;
        DataSet _dsDeptInfo2 = null;

        #endregion

        public frmDeptInfo()
        {
            InitializeComponent();
        }

        private void frmDeptInfo_Load(object sender, EventArgs e)
        {
            txtCOMPANYNAME.Text = COMPANYNAME;

            setCmb();

            if (UserInfo.instance().BIZCD != null && UserInfo.instance().BIZCD.ToString() != "")
                cmbBIZCD.EditValue = UserInfo.instance().BIZCD;
            else
                cmbBIZCD.EditValue = "01";

            CreTree();
        }

        private void setCmb()
        {
            try
            {
                //사업장콤보
                string strQueruy = @"SELECT
                                        BIZCD    DCODE
                                       ,BIZCD_NM DNAME
                                     FROM TBL_BIZCD
                                     WHERE COMPANYCD = '" + COMPANYCD + "' " +
                                       " ORDER BY BIZCD ASC";
                CodeAgent.SetLegacyCode(cmbBIZCD, strQueruy);
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        #region 부서트리생성

        private void CreTree()
        {
            //string sIBS_CD = UserInfo.instance().IBS_CD;
            //string sORG_CD = string.Empty;

            //if (sIBS_CD == "IBS01")
            //    sORG_CD = null;
            //else
            //    sORG_CD = UserInfo.instance().IBS_CD;

            //부서정보
            _dsDeptInfo = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM02_SELECT_02", COMPANYCD, "1");

            TreeNode rootNode = new TreeNode("사업장별 부서정보");
            this.treeView1.Nodes.Add(rootNode);
            this.treeView1.ImageList = this.imgOrganList;

            //DataRow[] drs = _dsDeptInfo.Tables[0].Select("LEVEL=0", " RNKORDER ASC");
            //DataRow[] drs = _dsDeptInfo.Tables[0].Select("GROUP BY '{0}','{1}', BIZCD, BIZCD_NM ");
            DataRow[] drs1 = _dsDeptInfo.Tables[0].Select();
            //노드 생성하기
            for (int i = 0; i < drs1.Length; i++)
            {
                BizNode Onode = new BizNode(drs1[i]);
                CreateNode2(Onode);

                rootNode.Nodes.Add(Onode);

                if (treeView1.Nodes.Count > 0)
                    treeView1.Nodes[0].Expand();
            }

            this.treeView1.EndUpdate();
            //this.treeView1.ExpandAll();
        }

        void CreateNode2(BizNode OrNode)
        {
            if (OrNode == null) return;

            try
            {
                _dsDeptInfo2 = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM02_SELECT_02", COMPANYCD, "2");
                DataRow[] drs2 = _dsDeptInfo2.Tables[0].Select("LEVEL=0 AND BIZCD = '" + OrNode.BIZCD + "'", " RNKORDER ASC");

                for (int i = 0; i < drs2.Length; i++)
                {
                    DeptNode OrNode2 = new DeptNode(drs2[i]);
                    OrNode.Nodes.Add(OrNode2);

                    CreateNode3(OrNode2, _dsDeptInfo2);
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        void CreateNode3(DeptNode OrNode3, DataSet DeptDs)
        {
            if (OrNode3 == null || DeptDs == null) return;

            try
            {
                DataRow[] drs3 = DeptDs.Tables[0].Select("LEVEL=1 AND BIZCD = '" + OrNode3.BIZCD + "' AND REFCODE='" + OrNode3.DEPTCODE + "'", " RNKORDER ASC");

                for (int i = 0; i < drs3.Length; i++)
                {
                    DeptNode OrNode4 = new DeptNode(drs3[i]);
                    OrNode3.Nodes.Add(OrNode4);
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        #endregion

        #region 이벤트

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            if (txtDEPTCODE.EditValue != null && txtDEPTCODE.EditValue.ToString() != "")
            {
                //DataRow dr = this.efwGridControl3.GetSelectedRow(0);
                //this.ParentBtn.Text = dr["CAPTION"].ToString();
                //this.ParentBtn.EditValue2 = dr["ID"];

                this.ParentBtn.Text = txtDEPTNAME.EditValue.ToString();
                this.ParentBtn.EditValue2 = txtDEPTCODE.EditValue.ToString();

                this.Close();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.treeView1.SelectedNode is DeptNode)
                {
                    DeptNode oNode = (DeptNode)this.treeView1.SelectedNode;

                    cmbBIZCD.EditValue = oNode.BIZCD;
                    txtDEPTCODE.EditValue = oNode.DEPTCODE;
                    txtDEPTNAME.EditValue = oNode.DEPTNAME;
                    txtDEPTABBR.EditValue = oNode.DEPTABBR;
                    txtSTARTYM.EditValue = oNode.STARTYM;
                    txtENDYM.EditValue = oNode.ENDYM;
                    chkUSEYN.EditValue = oNode.USEYN;
                    //txtRNKORDER.EditValue = oNode.RNKORDER;
                    //txtLEVEL.EditValue = oNode.LEVEL;
                    //txtREFCODE.EditValue = oNode.REFCODE;
                    txtMEMO.EditValue = oNode.MEMO;
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion


    }
}
