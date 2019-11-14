using DevExpress.XtraGrid.Columns;
using Easy.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace YL_DONUT.BizFrm
{
    public partial class frmTest02 : FrmBase
    {
        public frmTest02()
        {
            InitializeComponent();

            gridControl1.DataSource = GetData();

            var riPicEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            riPicEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;

            GridColumn images = gridView1.Columns.AddField("Image");
            images.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            images.Visible = true;
            images.ColumnEdit = riPicEdit;
            //images.ColumnEdit.AutoHeight = false;
            //images.ColumnEdit.AutoHeight = false;

            gridView1.Columns["ImageURL"].Visible = false;

            //gridView1.Columns["ImageURL"].OptionsColumn.FixedWidth = false;
            gridView1.Columns["ImageURL"].Width = 500;
            gridView1.RowHeight = 100;

            gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
        }

        Dictionary<String, Bitmap> iconsCache = new Dictionary<string, Bitmap>();

        #region FrmLoadEvent()

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Pumpkin");

            //This set the style to use skin technology
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;

            //Here we specify the skin to use by its name           
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = true;
            this.IsSave = true;
            this.IsDelete = true;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;
        }


        #endregion

        void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            //throw new NotImplementedException();
            DataRow dr = (e.Row as DataRowView).Row;
            string url = dr["ImageURL"].ToString();
            if (iconsCache.ContainsKey(url))
            {
                e.Value = iconsCache[url];
                return;
            }
            var request = WebRequest.Create(url);
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    e.Value = Bitmap.FromStream(stream);
                    iconsCache.Add(url, (Bitmap)e.Value);
                }
            }
        }

        DataTable GetData()
        {
            DataTable res = new DataTable();
            res.Columns.Add("Name");
            res.Columns.Add("ImageURL");
            res.Columns.Add("Remark");
            res.Rows.Add("John", @"http://blog.snsdoma.net/files/domashop/gshop_00000002/gshop_00000002.jpg", "비고");
            res.Rows.Add("John", @"http://blog.snsdoma.net/files/domashop/gshop_00000004/gshop_00000004.jpg", "비고");
            res.Rows.Add("John", @"http://blog.snsdoma.net/files/domashop/bae0097_00000001/bae0097_00000001_thumbnail.jpg", "비고");
            res.Rows.Add("John", @"http://blog.snsdoma.net/files/domashop/ss6064_00000001/ss6064_00000001_thumbnail.jpg", "비고");
            res.Rows.Add("John", @"http://blog.snsdoma.net/files/domashop/y1044_00000015/y1044_00000015_thumbnail.jpg", "비고");
            return res;

        }
    }
}
