using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_MA.BizFrm
{
    [Serializable]
    class BizNode : TreeNode
    {
        public string BIZCD { get; set; }
        public string BIZCD_NM { get; set; }

        public BizNode()
        {
        }

        public BizNode(DataRow dr)
        {
            DataBind(dr, false);
        }

        public void DataBind(DataRow dr, bool isPopContainer)
        {

            if (dr["BIZCD"] != DBNull.Value)
            {
                BIZCD = dr["BIZCD"].ToString();
            }
            if (dr["BIZCD_NM"] != DBNull.Value)
            {
                BIZCD_NM = dr["BIZCD_NM"].ToString();
            }

            //if (!isPopContainer)
            //{

            //}

            Refresh();
        }

        public void DataBind(DataRow dr)
        {
            DataBind(dr, false);

        }

        public void Refresh()
        {
            this.Name = BIZCD;

            //this.Text = String.Format("{0} [{1}]", BIZCD_NM, BIZCD);
            this.Text = String.Format("{0}", BIZCD_NM);

            string toolTxt = string.Empty;

            this.ImageIndex = 0;
            this.SelectedImageIndex = 0;

            //if (!string.IsNullOrEmpty(USEYN))
            //    if (this.USEYN == "N")  //사용안함 설정시
            //    {
            //        this.NodeFont = new System.Drawing.Font("돔움체", 9, System.Drawing.FontStyle.Strikeout);
            //        toolTxt = "사용안함";
            //        this.ImageIndex = 1;
            //        this.SelectedImageIndex = 1;
            //    }
            //    else
            //        this.NodeFont = new System.Drawing.Font("맑은고딕", 9, System.Drawing.FontStyle.Regular);

            this.NodeFont = new System.Drawing.Font("맑은고딕", 9, System.Drawing.FontStyle.Regular);

            //if (!string.IsNullOrEmpty(EXPIRE_DAY_TO))
            //    if (Easy.Framework.Util.Date.DtFrYYYYMMDD(this.EXPIRE_DAY_TO) < SysInfo.Instance().GetSysDateTime)
            //    {
            //        this.ForeColor = System.Drawing.Color.Red;
            //        toolTxt += " 유효기간만료";
            //        this.ImageIndex = 2;
            //        this.SelectedImageIndex = 2;
            //    }
            //    else
            //        this.ForeColor = System.Drawing.Color.Black;

            this.ForeColor = System.Drawing.Color.Black;

            if (!String.IsNullOrEmpty(toolTxt.Replace(" ", "")))
                this.ToolTipText = toolTxt;

        }

    }
}
