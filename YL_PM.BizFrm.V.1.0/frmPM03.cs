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

namespace YL_PM.BizFrm
{
    public partial class frmPM03 :FrmBase
    {
        MySQLConn sconn = new MySQLConn(ConstantLib.BasicConn_Real);
        bool _isNewMode;
        DataSet _dsDeptInfo = null;
        DataSet _dsDeptInfo2 = null;

        public ImageList imgOrganList { get; private set; }

        public frmPM03()
        {
            InitializeComponent();
            //단축코드 설정 
            this.QCode = "PM03";
            //폼명설정
            this.FrmName = "CATEGORY";
        }

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

            if (UserInfo.instance().ORG_CD != null && UserInfo.instance().ORG_CD.ToString() != "")
                txtCOMPANYCD.EditValue = UserInfo.instance().ORG_CD;
            else
                txtCOMPANYCD.EditValue = "YL01";

            SetCmb();

            if (UserInfo.instance().BIZCD != null && UserInfo.instance().BIZCD.ToString() != "")
                cmbBIZCD.EditValue = UserInfo.instance().BIZCD;
            else
                cmbBIZCD.EditValue = "01";


            gridView1.OptionsView.ShowFooter = true;

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl1.BindControlSet(
                      new ColumnControlSet("COMPANYCD", txtCOMPANYCD)
                    , new ColumnControlSet("BIZCD", cmbBIZCD)
                    , new ColumnControlSet("LARGE_CD", txtLARGE_CD)
                    , new ColumnControlSet("LARGE_NM", txtLARGE_NM)
                    );

            this.efwGridControl1.Click += efwGridControl1_Click;

            //그리드로 클릭시 컨트롤 데이터 바인딩
            this.efwGridControl2.BindControlSet(
                      new ColumnControlSet("LARGE_CD", txtLARGE_CD)
                    , new ColumnControlSet("LARGE_NM", txtLARGE_NM)
                    , new ColumnControlSet("MIDDLE_CD", txtMIDDLE_CD)
                    , new ColumnControlSet("MIDDLE_NM", txtMIDDLE_NM)

                     );

            this.efwGridControl2.Click += efwGridControl2_Click;

            this.efwGridControl3.BindControlSet(
                      new ColumnControlSet("LARGE_CD", txtLARGE_CD)
                    , new ColumnControlSet("LARGE_NM", txtLARGE_NM)
                    , new ColumnControlSet("MIDDLE_CD", txtMIDDLE_CD)
                    , new ColumnControlSet("MIDDLE_NM", txtMIDDLE_NM)
                    , new ColumnControlSet("SMALL_CD", txtSMALL_CD)
                    , new ColumnControlSet("SMALL_NM", txtSMALL_NM)
                    , new ColumnControlSet("EVENT", chkEVENT)
                    , new ColumnControlSet("USETYPE", chkUSETYPE)
                    , new ColumnControlSet("REMARK", txtREMARK)
                     );

            this.efwGridControl3.Click += efwGridControl3_Click;


            //CreTree();


            Open1();
        }

        private void SetCmb()
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
        }

        #endregion

        private void Open1()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PM_PM03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("P_COMPANYCD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtCOMPANYCD.EditValue;

                        cmd.Parameters.Add("P_BIZCD", MySqlDbType.VarChar, 2);
                        cmd.Parameters[1].Value = cmbBIZCD.EditValue;

                        cmd.Parameters.Add("P_LARGE_CD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = txtLARGE_CD.EditValue;
                        cmd.Parameters.Add("P_LARGE_NM", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtLARGE_NM.EditValue;

                        cmd.Parameters.Add("P_MIDDLE_CD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = txtMIDDLE_CD.EditValue;
                        cmd.Parameters.Add("P_MIDDLE_NM", MySqlDbType.VarChar, 50);
                        cmd.Parameters[5].Value = txtMIDDLE_NM.EditValue;

                        cmd.Parameters.Add("P_SMALL_CD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = txtSMALL_CD.EditValue;
                        cmd.Parameters.Add("P_SMALL_NM", MySqlDbType.VarChar, 50);
                        cmd.Parameters[7].Value = txtSMALL_NM.EditValue;

                        cmd.Parameters.Add("P_CHK", MySqlDbType.VarChar, 1);
                        cmd.Parameters[8].Value = "L";

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
        private void Open2()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PM_PM03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_COMPANYCD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtCOMPANYCD.EditValue;

                        cmd.Parameters.Add("P_BIZCD", MySqlDbType.VarChar, 2);
                        cmd.Parameters[1].Value = cmbBIZCD.EditValue;

                        cmd.Parameters.Add("P_LARGE_CD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = txtLARGE_CD.EditValue;
                        cmd.Parameters.Add("P_LARGE_NM", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtLARGE_NM.EditValue;

                        cmd.Parameters.Add("P_MIDDLE_CD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = txtMIDDLE_CD.EditValue;
                        cmd.Parameters.Add("P_MIDDLE_NM", MySqlDbType.VarChar, 50);
                        cmd.Parameters[5].Value = txtMIDDLE_NM.EditValue;

                        cmd.Parameters.Add("P_SMALL_CD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = txtSMALL_CD.EditValue;
                        cmd.Parameters.Add("P_SMALL_NM", MySqlDbType.VarChar, 50);
                        cmd.Parameters[7].Value = txtSMALL_NM.EditValue;

                        cmd.Parameters.Add("P_CHK", MySqlDbType.VarChar, 1);
                        cmd.Parameters[8].Value = "M";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl2.DataBind(ds);
                            this.efwGridControl2.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        private void Open3()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PM_PM03_SELECT_01", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_COMPANYCD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[0].Value = txtCOMPANYCD.EditValue;

                        cmd.Parameters.Add("P_BIZCD", MySqlDbType.VarChar, 2);
                        cmd.Parameters[1].Value = cmbBIZCD.EditValue;

                        cmd.Parameters.Add("P_LARGE_CD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[2].Value = txtLARGE_CD.EditValue;
                        cmd.Parameters.Add("P_LARGE_NM", MySqlDbType.VarChar, 50);
                        cmd.Parameters[3].Value = txtLARGE_NM.EditValue;

                        cmd.Parameters.Add("P_MIDDLE_CD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[4].Value = txtMIDDLE_CD.EditValue;
                        cmd.Parameters.Add("P_MIDDLE_NM", MySqlDbType.VarChar, 50);
                        cmd.Parameters[5].Value = txtMIDDLE_NM.EditValue;

                        cmd.Parameters.Add("P_SMALL_CD", MySqlDbType.VarChar, 10);
                        cmd.Parameters[6].Value = txtSMALL_CD.EditValue;
                        cmd.Parameters.Add("P_SMALL_NM", MySqlDbType.VarChar, 50);
                        cmd.Parameters[7].Value = txtSMALL_NM.EditValue;

                        cmd.Parameters.Add("P_CHK", MySqlDbType.VarChar, 1);
                        cmd.Parameters[8].Value = "S";

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            DataTable ds = new DataTable();
                            sda.Fill(ds);
                            efwGridControl3.DataBind(ds);
                            this.efwGridControl3.MyGridView.BestFitColumns();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAgent.MessageShow(MessageType.Error, ex.ToString());
            }
        }

        public override void Save()
        {
            if (string.IsNullOrEmpty(this.txtLARGE_CD.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 대분류를 입력하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtMIDDLE_CD.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 중분류를 입력하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtSMALL_CD.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 소분류를 입력하세요!");
                return;
            }

            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PM_PM03_SAVE_01", con))
                        {
                            con.Open();

                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.Add(new MySqlParameter("p_COMPANYCD", MySqlDbType.VarChar));
                            cmd.Parameters["p_COMPANYCD"].Value = txtCOMPANYCD.EditValue;
                            cmd.Parameters["p_COMPANYCD"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_BIZCD", MySqlDbType.VarChar));
                            cmd.Parameters["p_BIZCD"].Value = cmbBIZCD.EditValue;
                            cmd.Parameters["p_BIZCD"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_LARGE_CD", MySqlDbType.VarChar));
                            cmd.Parameters["p_LARGE_CD"].Value = txtLARGE_CD.EditValue;
                            cmd.Parameters["p_LARGE_CD"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_LARGE_NM", MySqlDbType.VarChar));
                            cmd.Parameters["p_LARGE_NM"].Value = txtLARGE_NM.EditValue;
                            cmd.Parameters["p_LARGE_NM"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_MIDDLE_CD", MySqlDbType.VarChar));
                            cmd.Parameters["p_MIDDLE_CD"].Value = txtMIDDLE_CD.EditValue;
                            cmd.Parameters["p_MIDDLE_CD"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_MIDDLE_NM", MySqlDbType.VarChar));
                            cmd.Parameters["p_MIDDLE_NM"].Value = txtMIDDLE_NM.EditValue;
                            cmd.Parameters["p_MIDDLE_NM"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_SMALL_CD", MySqlDbType.VarChar));
                            cmd.Parameters["p_SMALL_CD"].Value = txtSMALL_CD.EditValue;
                            cmd.Parameters["p_SMALL_CD"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_SMALL_NM", MySqlDbType.VarChar));
                            cmd.Parameters["p_SMALL_NM"].Value = txtSMALL_NM.EditValue;
                            cmd.Parameters["p_SMALL_NM"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_REMARK", MySqlDbType.VarChar));
                            cmd.Parameters["p_REMARK"].Value = txtREMARK.EditValue;
                            cmd.Parameters["p_REMARK"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_EVENT", MySqlDbType.VarChar));
                            cmd.Parameters["p_EVENT"].Value = chkEVENT.Checked == true ? "Y" : "N";
                            cmd.Parameters["p_EVENT"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("p_USETYPE", MySqlDbType.VarChar));
                            cmd.Parameters["p_USETYPE"].Value = chkUSETYPE.Checked == true ? "Y" : "N";
                            cmd.Parameters["p_USETYPE"].Direction = ParameterDirection.Input;

                            cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                            cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();


                            string sResult = cmd.Parameters["o_Return"].Value.ToString();
                            if (sResult == "0")
                                MessageAgent.MessageShow(MessageType.Warning, " 저장 되었습니다");
                            else
                                MessageAgent.MessageShow(MessageType.Warning, " 저장중 오류가 발생하였습니다");
                            Open1();
                            Open2();
                            Open3();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
            }


        }


        #region 삭제

        public override void Delete()
        {
            base.Delete();

            if (string.IsNullOrEmpty(this.txtLARGE_CD.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 대분류를 선택하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtMIDDLE_CD.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 중분류를 선택하세요!");
                return;
            }
            if (string.IsNullOrEmpty(this.txtSMALL_CD.Text))
            {
                MessageAgent.MessageShow(MessageType.Warning, " 소분류를 선택하세요!");
                return;
            }


            DialogResult drt = MessageAgent.MessageShow(MessageType.Confirm, "삭제 하시겠습니까?");

            try
            {
                using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                {
                    using (MySqlCommand cmd = new MySqlCommand("domabiz.USP_PM_PM03_DELETE_01", con))
                    {
                        con.Open();

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new MySqlParameter("p_COMPANYCD", MySqlDbType.VarChar));
                        cmd.Parameters["p_COMPANYCD"].Value = txtCOMPANYCD.EditValue;
                        cmd.Parameters["p_COMPANYCD"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("p_BIZCD", MySqlDbType.VarChar));
                        cmd.Parameters["p_BIZCD"].Value = cmbBIZCD.EditValue;
                        cmd.Parameters["p_BIZCD"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("p_LARGE_CD", MySqlDbType.VarChar));
                        cmd.Parameters["p_LARGE_CD"].Value = txtLARGE_CD.EditValue;
                        cmd.Parameters["p_LARGE_CD"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("p_MIDDLE_CD", MySqlDbType.VarChar));
                        cmd.Parameters["p_MIDDLE_CD"].Value = txtMIDDLE_CD.EditValue;
                        cmd.Parameters["p_MIDDLE_CD"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("p_SMALL_CD", MySqlDbType.VarChar));
                        cmd.Parameters["p_SMALL_CD"].Value = txtSMALL_CD.EditValue;
                        cmd.Parameters["p_SMALL_CD"].Direction = ParameterDirection.Input;

                        cmd.Parameters.Add(new MySqlParameter("o_Return", MySqlDbType.VarChar));
                        cmd.Parameters["o_Return"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();


                        string sResult = cmd.Parameters["o_Return"].Value.ToString();
                        if (sResult == "0")
                            MessageAgent.MessageShow(MessageType.Warning, " 삭제 되었습니다");
                        else
                            MessageAgent.MessageShow(MessageType.Warning, " 삭제중 오류가 발생하였습니다");
                        Open1();
                        Open2();
                        Open3();
                        NewMode();
                    }
                }
            }

            catch (Exception ex)
                {
                    MessageAgent.MessageShow(MessageType.Error, ex.ToString());
                }
        }

        #endregion

        #region 카테고리 트리생성

        //private void CreTree()
        //{

        //    //부서정보
        //    _dsDeptInfo = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM02_SELECT_02", txtCOMPANYCD.EditValue, "1");

        //    TreeNode rootNode = new TreeNode("카테고리 정보");
        //    this.treeView1.Nodes.Add(rootNode);
        //    this.treeView1.ImageList = this.imgOrganList;

        //    //DataRow[] drs = _dsDeptInfo.Tables[0].Select("LEVEL=0", " RNKORDER ASC");
        //    //DataRow[] drs = _dsDeptInfo.Tables[0].Select("GROUP BY '{0}','{1}', BIZCD, BIZCD_NM ");
        //    DataRow[] drs1 = _dsDeptInfo.Tables[0].Select();
        //    //노드 생성하기
        //    for (int i = 0; i < drs1.Length; i ++)
        //    {
        //        BizNode Onode = new BizNode(drs1[i]);
        //        CreateNode2(Onode);


        //        rootNode.Nodes.Add(Onode);

        //        if (treeView1.Nodes.Count > 0)
        //            treeView1.Nodes[0].Expand();
        //    }


        //    this.treeView1.EndUpdate();
        //    //this.treeView1.ExpandAll();
        //}


        //private void CreTree()
        //{

        //    //카테고리정보
        //    _dsDeptInfo = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM02_SELECT_02", txtCOMPANYCD.EditValue, "1");

        //    CategoryNode_L rootNode = new CategoryNode_L();
        //    this.treeView1.Nodes.Add(rootNode);
        //    this.treeView1.ImageList = this.imgOrganList;

        //    //DataRow[] drs = _dsDeptInfo.Tables[0].Select("LEVEL=0", " RNKORDER ASC");
        //    //DataRow[] drs = _dsDeptInfo.Tables[0].Select("GROUP BY '{0}','{1}', BIZCD, BIZCD_NM ");
        //    DataRow[] drs1 = _dsDeptInfo.Tables[0].Select();
        //    //노드 생성하기
        //    for (int i = 0; i < drs1.Length; i++)
        //    {
        //        CategoryNode_L Onode = new CategoryNode_L(drs1[i]);
        //        CreateNode2(Onode);


        //        rootNode.Nodes.Add(Onode);

        //        if (treeView1.Nodes.Count > 0)
        //            treeView1.Nodes[0].Expand();
        //    }


        //    this.treeView1.EndUpdate();
        //    //this.treeView1.ExpandAll();
        //}



        //void CreateNode2(BizNode OrNode)
        //{
        //    if (OrNode == null) return;

        //    try
        //    {
        //        _dsDeptInfo2 = ServiceAgent.ExecuteDataSet(false, "CONIS_IBS", "USP_MM_MM02_SELECT_02", txtCOMPANYCD.EditValue, "2");
        //        DataRow[] drs2 = _dsDeptInfo2.Tables[0].Select("LEVEL=0 AND BIZCD = '" + OrNode.BIZCD + "'", " RNKORDER ASC");

        //        for (int i = 0; i < drs2.Length; i++)
        //        {
        //            DeptNode OrNode2 = new DeptNode(drs2[i]);
        //            OrNode.Nodes.Add(OrNode2);

        //            CreateNode3(OrNode2, _dsDeptInfo2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
        //    }
        //}

        //void CreateNode3(DeptNode OrNode3, DataSet DeptDs)
        //{
        //    if (OrNode3 == null || DeptDs == null) return;

        //    try
        //    {
        //        DataRow[] drs3 = DeptDs.Tables[0].Select("LEVEL=1 AND BIZCD = '" + OrNode3.BIZCD + "' AND REFCODE='" + OrNode3.DEPTCODE + "'", " RNKORDER ASC");

        //        for (int i = 0; i < drs3.Length; i++)
        //        {
        //            DeptNode OrNode4 = new DeptNode(drs3[i]);
        //            OrNode3.Nodes.Add(OrNode4);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
        //    }
        //}

        #endregion

        //private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    try
        //    {
        //        this.Cursor = Cursors.WaitCursor;

        //        if (this.treeView1.SelectedNode is DeptNode)
        //        {
        //            DeptNode oNode = (DeptNode)this.treeView1.SelectedNode;

        //            cmbBIZCD.EditValue = oNode.BIZCD;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageAgent.MessageShow(MessageType.Error, ex.ToString());
        //    }
        //    finally
        //    {
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        private void efwGridControl1_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl1.GetSelectedRow(0);
            txtMIDDLE_CD.EditValue = "";
            txtMIDDLE_NM.EditValue = "";
            txtSMALL_CD.EditValue = "";
            txtSMALL_NM.EditValue = "";

            Open2();
            _isNewMode = false;
        }
        private void efwGridControl2_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl2.GetSelectedRow(0);
            txtSMALL_CD.EditValue = "";
            txtSMALL_NM.EditValue = "";
            Open3();
            _isNewMode = false;
        }

        private void efwGridControl3_Click(object sender, EventArgs e)
        {
            DataRow dr = this.efwGridControl3.GetSelectedRow(0);

            _isNewMode = false;
        }

    }

}
