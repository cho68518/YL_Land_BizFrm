#region "frmMM02 설명"
//===================================================================================================
//■Program Name  : frmMM02
//■Description   : 부서코드
//■Author        : 조정현
//■Date          : 2019.04.19
//■etc           : 
//====================================================================================================
//<history>
//====================================================================================================
//[NO][Date]      Author   Description
//====================================================================================================
//[1] [2019.04.19][조정현] Base
//[2] [2019.04.19][조정현] 
//----------------------------------------------------------------------------------------------------
//
//====================================================================================================
//</history>
#endregion


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

namespace YL_MM.BizFrm
{
    public partial class frmMM02 : FrmBase
    {
        #region Fields

        DataSet _dsDeptInfo = null;
        DataSet _dsDeptInfo2 = null;

        #endregion

        #region 생성자
        public frmMM02()
        {
            InitializeComponent();

            //단축코드 설정 
            this.QCode = "MM02";
            //폼명설정
            this.FrmName = "부서코드";

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
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;

            //그리드 컬럼에 체크박스 레포지토리아이템 추가
            GridAgent.RepositoryItemCheckEditAdd(this.efwGridControl1, "Y", "N", "USEYN");

            if (UserInfo.instance().ORG_CD != null && UserInfo.instance().ORG_CD.ToString() != "")
                txtCOMPANYCD.EditValue = UserInfo.instance().ORG_CD;
            else
                txtCOMPANYCD.EditValue = "YL01";

            setCmb();

            if (UserInfo.instance().BIZCD != null && UserInfo.instance().BIZCD.ToString() != "")
                cmbBIZCD.EditValue = UserInfo.instance().BIZCD;
            else
                cmbBIZCD.EditValue = "01";

            CreTree();

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("COMPANYCD", txtCOMPANYCD)
                      , new ColumnControlSet("BIZCD", cmbBIZCD)
                      //, new ColumnControlSet("BIZCD_NM", txtBIZCD_NM)
                      , new ColumnControlSet("DEPTCODE", txtDEPTCODE)
                      , new ColumnControlSet("DEPTNAME", txtDEPTNAME)
                      , new ColumnControlSet("DEPTABBR", txtDEPTABBR)
                      , new ColumnControlSet("STARTYM" , txtSTARTYM)
                      , new ColumnControlSet("ENDYM"   , txtENDYM)
                      , new ColumnControlSet("USEYN"   , chkUSEYN)
                      , new ColumnControlSet("MEMO"    , txtMEMO)
                      , new ColumnControlSet("RNKORDER", txtRNKORDER)
                      , new ColumnControlSet("REFCODE" , txtREFCODE)
                      , new ColumnControlSet("LEVEL"   , txtLEVEL)
                      );

            this.efwGridControl1.Click += efwGridControl1_Click;
            chkUSEYN.Checked = true;

           // Open1();
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
                                     WHERE COMPANYCD = '" + txtCOMPANYCD.EditValue + "' " +
                                       " ORDER BY BIZCD ASC";
                CodeAgent.SetLegacyCode(cmbBIZCD, strQueruy);
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        #endregion

        #region 신규

        public override void NewMode()
        {
            base.NewMode();

            Eraser.Clear(this, "CLR1");
            chkUSEYN.Checked = true;
            txtDEPTCODE.Focus();
        }

        #endregion

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
            _dsDeptInfo = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM02_SELECT_02", txtCOMPANYCD.EditValue, "1");

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
                _dsDeptInfo2 = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM02_SELECT_02", txtCOMPANYCD.EditValue, "2");
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

        #region 조회

        public override void Search()
        {
            base.Search();
            Open1();
        }

        private void Open1()
        {
            //사업장정보조회
            try
            {
                //base.Search();
                DataSet ds = ServiceAgent.ExecuteDataSet(true, "CONIS_IBS", "USP_MM_MM02_SELECT_01"
                , this.txtCOMPANYCD.Text);

                efwGridControl1.DataBind(ds);
                this.efwGridControl1.MyGridView.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        #endregion

        #region 저장

        public override void Save()
        {
            base.Save();

            //부서정보 저장
            if (txtCOMPANYCD.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사코드가 없습니다!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                if (ValidationAgentEx.IsRequireCheck(this.layoutControl1.Controls, "R1"))
                {
                    try
                    {
                        int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MM_MM02_SAVE_01"
                                                         , UserInfo.instance().UserId
                                                        , this.txtCOMPANYCD.EditValue
                                                        , this.cmbBIZCD.EditValue
                                                        , this.txtDEPTCODE.EditValue
                                                        , this.txtDEPTNAME.EditValue
                                                        , this.txtDEPTABBR.EditValue
                                                        , this.txtSTARTYM.EditValue
                                                        , this.txtENDYM.EditValue
                                                        , this.chkUSEYN.EditValue
                                                        , this.txtMEMO.EditValue
                                                        , this.txtREFCODE.EditValue
                                                        , Convert.ToInt16(this.txtRNKORDER.EditValue)
                                                        , Convert.ToInt16(this.txtLEVEL.EditValue)
                                                        );

                        if (retVal > 0)
                        {
                            MessageAgent.MessageShow(MessageType.Informational, "저장 되었습니다.");
                            Open1();
                            this.treeView1.Nodes.Clear();
                            CreTree();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                    }
                }
            }
        }

        #endregion

        #region 삭제

        public override void Delete()
        {
            base.Delete();

            //부서정보 삭제
            if (txtCOMPANYCD.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "회사코드가 없습니다!");
                return;
            }

            if (cmbBIZCD.EditValue == null)
            {
                MessageAgent.MessageShow(MessageType.Warning, "사업장코드가 없습니다!");
                return;
            }

            DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?");

            if (drt == DialogResult.OK)
            {
                try
                {
                    int retVal = ServiceAgent.ExecuteNoneQuery(UserInfo.instance().UserId, "CONIS_IBS", "USP_MM_MM02_DELETE_01"
                                                        , UserInfo.instance().UserId
                                                        , this.txtCOMPANYCD.EditValue
                                                        , this.cmbBIZCD.EditValue
                                                        , this.txtDEPTCODE.EditValue
                                                        );

                    if (retVal > 0)
                    {
                        MessageAgent.MessageShow(MessageType.Informational, "삭제되었습니다.");
                        Open1();
                        this.treeView1.Nodes.Clear();
                        CreTree();
                        NewMode();
                    }
                }
                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }
        }

        #endregion

        #region 이벤트

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);

            //if (dr != null && dr["FLR"].ToString() != "0" && dr["FLR"].ToString() != "")
            //    this.xtraTabPage3.PageEnabled = true;
            //else
            //    this.xtraTabPage3.PageEnabled = false;
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
                    txtRNKORDER.EditValue = oNode.RNKORDER;
                    txtLEVEL.EditValue = oNode.LEVEL;
                    txtREFCODE.EditValue = oNode.REFCODE;
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
