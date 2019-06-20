using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_PM.BizFrm
{
    [Serializable]
    class CategoryNode_L : TreeNode
    {
        public string LARGE_CD { get; set; }
        public string LARGE_NM { get; set; }
        public string CD_TYPE { get; set; }      // L, M, S

        public CategoryNode_L()
        {
        }

        public CategoryNode_L(DataRow dr)
        {
            DataBind(dr, false);
        }

        public void DataBind(DataRow dr, bool isPopContainer)
        {

            if (dr["LARGE_CD"] != DBNull.Value)
            {
                LARGE_CD = dr["LARGE_CD"].ToString();
            }
            if (dr["LARGE_NM"] != DBNull.Value)
            {
                LARGE_NM = dr["LARGE_NM"].ToString();
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
            this.Name = LARGE_CD;

            this.Text = String.Format("{0} [{1}]", LARGE_NM, LARGE_CD);

            string toolTxt = string.Empty;

            this.ImageIndex = 0;
            this.SelectedImageIndex = 0;

           
            this.ForeColor = System.Drawing.Color.Black;

            if (!String.IsNullOrEmpty(toolTxt.Replace(" ", "")))
                this.ToolTipText = toolTxt;

        }
    }
}