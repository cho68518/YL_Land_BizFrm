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

    class Com_CodeNode1 : TreeNode
    {
        public string code_id { get; set; }
        public string code_nm { get; set; }

        public Com_CodeNode1()
        {
        }

        public Com_CodeNode1(DataRow dr)
        {
            DataBind(dr, false);
        }

        public void DataBind(DataRow dr, bool isPopContainer)
        {

            if (dr["code_id"] != DBNull.Value)
            {
                code_id = dr["code_id"].ToString();
            }
            if (dr["code_nm"] != DBNull.Value)
            {
                code_nm = dr["code_nm"].ToString();
            }
            Refresh();
        }
        public void Refresh()
        {
            this.Name = code_id;

            this.Text = String.Format("{0} [{1}]", code_nm, code_id);

            string toolTxt = string.Empty;

            this.ImageIndex = 0;
            this.SelectedImageIndex = 0;

            //if (!string.IsNullOrEmpty(use_yn))
            //    if (this.use_yn == "N")  //사용안함 설정시
            //    {
            //        this.NodeFont = new System.Drawing.Font("돔움체", 9, System.Drawing.FontStyle.Strikeout);
            //        toolTxt = "사용안함";
            //        this.ImageIndex = 1;
            //        this.SelectedImageIndex = 1;
            //    }
            //    else
            //        this.NodeFont = new System.Drawing.Font("맑은고딕", 9, System.Drawing.FontStyle.Regular);

            this.ForeColor = System.Drawing.Color.Black;

            if (!String.IsNullOrEmpty(toolTxt.Replace(" ", "")))
                this.ToolTipText = toolTxt;

        }
    }

}
