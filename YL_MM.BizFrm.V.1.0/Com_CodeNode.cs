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

    class Com_CodeNode : TreeNode
    {
        public string gcode_id { get; set; }
        public string gcode_nm { get; set; }

        public Com_CodeNode()
        {
        }

        public Com_CodeNode(DataRow dr)
        {
            DataBind(dr, false);
        }

        public void DataBind(DataRow dr, bool isPopContainer)
        {

            if (dr["gcode_id"] != DBNull.Value)
            {
                gcode_id = dr["gcode_id"].ToString();
            }
            if (dr["gcode_nm"] != DBNull.Value)
            {
                gcode_nm = dr["gcode_nm"].ToString();
            }
            Refresh();
        }
        public void Refresh()
        {
            this.Name = gcode_id;

            this.Text = String.Format("{0} [{1}]", gcode_nm, gcode_id);

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
