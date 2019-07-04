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

namespace YL_DONUT.BizFrm
{
    public partial class frmDN07_Pop01 : FrmPopUpBase
    {
        public long pSTORY_ID { get; set; }
        public string pLATITUDE { get; set; }
        public string pLONGITUDE { get; set; }
        public string pNICKNAME { get; set; }
        public string pNAME { get; set; }
        public string pLOGIN_ID { get; set; }
        public string pSTORY_NAME { get; set; }
        public string pREG_DATE { get; set; }
        public string pPR_NAME { get; set; }
        public string pPR_CELL_NUM { get; set; }
        public string pPR_JIBUN_ADDR { get; set; }
        public string pPR_ROAD_ADDR { get; set; }
        public string pCONTENTS { get; set; }
        public string pWK_HAN { get; set; }
        public string pCHEF_LEVEL { get; set; }
        public string pIS_USE { get; set; }

        frmDN07_Pop02 popup;

        public frmDN07_Pop01()
        {
            InitializeComponent();
        }

        private void FrmDN07_Pop01_Load(object sender, EventArgs e)
        {
            this.Text = "상세정보";
            efwLabel1.Text = pLATITUDE;
            efwLabel2.Text = pLONGITUDE;

            txt1.EditValue = pLATITUDE;
            txt2.EditValue = pLONGITUDE;

            lblStory_Name.Text = pSTORY_NAME;
            lblReg_Date.Text = " (작성일: " + pREG_DATE + " (" + pWK_HAN + "))";

            lblStory_Name.Font = new Font(lblStory_Name.Font, FontStyle.Bold);

            setInfo();
            setMap();
            SetPic();
        }

        private void setInfo()
        {
            txtLOGIN_ID.EditValue = pLOGIN_ID;
            txtNICKNAME.EditValue = pNICKNAME;
            txtNAME.EditValue = pNAME;
            txtCHEF_LEVEL.EditValue = pCHEF_LEVEL;
            chkIS_USE.EditValue = pIS_USE;
            txtCONTENTS.EditValue = pCONTENTS;
            //txtPR_NAME.EditValue = pPR_NAME;
            //txtPR_CELL_NUM.EditValue = pPR_CELL_NUM;
            txtPR_JIBUN_ADDR.EditValue = pPR_JIBUN_ADDR;
            txtPR_ROAD_ADDR.EditValue = pPR_ROAD_ADDR;

            txtPR_NAME.EditValue = null;
            txtPR_CELL_NUM.EditValue = null;

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT pr_name, pr_cell_num FROM domalife.story_location WHERE story_id = " + pSTORY_ID;
                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                for (int i = 0; i < dr.Length; i++)
                {
                    txtPR_NAME.EditValue = dr[i]["pr_name"].ToString();
                    txtPR_CELL_NUM.EditValue = dr[i]["pr_cell_num"].ToString();
                }
            }
        }

        private void setMap()
        {
            this.webBrowser1.Navigate("http://dotoc3.eyeoyou.com/maps/maptest06.asp?p1=" + pLATITUDE + "&p2=" + pLONGITUDE);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SetPic()
        {
            picImg1.Image = null;
            picImg2.Image = null;
            picImg3.Image = null;
            picImg4.Image = null;
            picImg5.Image = null;

            lblpic1.Text = "";
            lblpic2.Text = "";
            lblpic3.Text = "";
            lblpic4.Text = "";
            lblpic5.Text = "";

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "SELECT image_url FROM domalife.y_thumbnail_list WHERE contents_id = " + pSTORY_ID;
                DataSet ds = con.selectQueryDataSet();
                //DataTable retDT = ds.Tables[0];
                DataRow[] dr = ds.Tables[0].Select();
                CodeData[] codeArray = new CodeData[dr.Length];

                //pictureEdit.Image = Image.FromFile(@"c:\sample.jpg");
                for (int i = 0; i < dr.Length; i++)
                {
                    if (i == 0)
                    {
                        picImg1.LoadAsync(dr[i]["image_url"].ToString());
                        lblpic1.Text = dr[i]["image_url"].ToString();
                    }
                    else if (i == 1)
                    {
                        picImg2.LoadAsync(dr[i]["image_url"].ToString());
                        lblpic2.Text = dr[i]["image_url"].ToString();
                    }
                    else if (i == 2)
                    {
                        picImg3.LoadAsync(dr[i]["image_url"].ToString());
                        lblpic3.Text = dr[i]["image_url"].ToString();
                    }
                    else if (i == 3)
                    {
                        picImg4.LoadAsync(dr[i]["image_url"].ToString());
                        lblpic4.Text = dr[i]["image_url"].ToString();
                    }
                    else if (i == 4)
                    {
                        picImg5.LoadAsync(dr[i]["image_url"].ToString());
                        lblpic5.Text = dr[i]["image_url"].ToString();
                    }
                }
            }
        }

        private void PicImg1_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("1");
        }

        private void PicImg2_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("2");
        }

        private void PicImg3_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("3");
        }

        private void PicImg4_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("4");
        }

        private void PicImg5_DoubleClick(object sender, EventArgs e)
        {
            OpenDlg("5");
        }

        private void OpenDlg(string s1)
        {
            popup = new frmDN07_Pop02();
            //popup.Owner = this;

            if (s1 =="1")
                popup.pURL = lblpic1.Text;
            else if (s1 == "2")
                popup.pURL = lblpic2.Text;
            else if (s1 == "3")
                popup.pURL = lblpic3.Text;
            else if (s1 == "4")
                popup.pURL = lblpic4.Text;
            else if (s1 == "5")
                popup.pURL = lblpic5.Text;

            popup.FormClosed += popup_FormClosed;
            popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            popup.FormClosed -= popup_FormClosed;

            //if (popup.DialogResult == DialogResult.OK)
            //{
            //    this.txtX.EditValue = popup.nX;
            //    this.txtY.EditValue = popup.nY;
            //}

            //OpenTag(gfloorInfo.FLR, "LDT");
            popup = null;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //정보변경
            if (MessageAgent.MessageShow(MessageType.Confirm, "저장 하시겠습니까?") == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(ConstantLib.BasicConn_Real))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("domalife.USP_DN_DN07_SAVE_01", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("i_story_id", MySqlDbType.Int64, 20);
                            cmd.Parameters[0].Value = pSTORY_ID;

                            cmd.Parameters.Add("i_pr_name", MySqlDbType.VarChar, 100);
                            cmd.Parameters[1].Value = this.txtPR_NAME.EditValue;

                            cmd.Parameters.Add("i_pr_cell_num", MySqlDbType.VarChar, 45);
                            cmd.Parameters[2].Value = this.txtPR_CELL_NUM.EditValue;

                            cmd.ExecuteNonQuery();

                            MessageAgent.MessageShow(MessageType.Informational, "처리되었습니다!");
                            con.Close();

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
}
