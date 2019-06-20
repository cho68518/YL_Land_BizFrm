using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_MM.BizFrm
{
    [Serializable]
    class DeptNode : TreeNode
    {
        public string COMPANYCD { get; set; }
        public string COMPANYNAME { get; set; }
        public string BIZCD { get; set; }
        public string DEPTCODE { get; set; }
        public string DEPTNAME { get; set; }
        public string DEPTABBR { get; set; }
        public string STARTYM { get; set; }
        public string ENDYM { get; set; }
        public string USEYN { get; set; }
        public string MEMO { get; set; }
        public int RNKORDER { get; set; }
        public string REFCODE { get; set; }
        public int LEVEL { get; set; }

        public DeptNode()
        {
        }

        public DeptNode(DataRow dr)
        {
            DataBind(dr, false);
        }

        public void DataBind(DataRow dr, bool isPopContainer)
        {

            if (dr["COMPANYCD"] != DBNull.Value)
            {
                COMPANYCD = dr["COMPANYCD"].ToString();
            }
            if (dr["COMPANYNAME"] != DBNull.Value)
            {
                COMPANYNAME = dr["COMPANYNAME"].ToString();
            }
            if (dr["BIZCD"] != DBNull.Value)
            {
                BIZCD = dr["BIZCD"].ToString();
            }
            if (dr["DEPTCODE"] != DBNull.Value)
            {
                DEPTCODE = dr["DEPTCODE"].ToString();
            }
            if (dr["DEPTNAME"] != DBNull.Value)
            {
                DEPTNAME = dr["DEPTNAME"].ToString();
            }
            if (dr["DEPTABBR"] != DBNull.Value)
            {
                DEPTABBR = dr["DEPTABBR"].ToString();
            }
            if (dr["STARTYM"] != DBNull.Value)
            {
                STARTYM = dr["STARTYM"].ToString();
            }
            if (dr["ENDYM"] != DBNull.Value)
            {
                ENDYM = dr["ENDYM"].ToString();
            }
            if (dr["USEYN"] != DBNull.Value)
            {
                USEYN = dr["USEYN"].ToString();
            }
            if (dr["MEMO"] != DBNull.Value)
            {
                MEMO = dr["MEMO"].ToString();
            }
            if (dr["RNKORDER"] != DBNull.Value)
            {
                RNKORDER = Convert.ToInt32(dr["RNKORDER"]);
            }
            if (dr["REFCODE"] != DBNull.Value)
            {
                REFCODE = dr["REFCODE"].ToString();
            }
            if (dr["LEVEL"] != DBNull.Value)
            {
                LEVEL = Convert.ToInt32(dr["LEVEL"]);
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
            this.Name = COMPANYCD;

            this.Text = String.Format("{0} [{1}]", DEPTNAME, DEPTCODE);

            string toolTxt = string.Empty;

            this.ImageIndex = 0;
            this.SelectedImageIndex = 0;

            if (!string.IsNullOrEmpty(USEYN))
                if (this.USEYN == "N")  //사용안함 설정시
                {
                    this.NodeFont = new System.Drawing.Font("돔움체", 9, System.Drawing.FontStyle.Strikeout);
                    toolTxt = "사용안함";
                    this.ImageIndex = 1;
                    this.SelectedImageIndex = 1;
                }
                else
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
