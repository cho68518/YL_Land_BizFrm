using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
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
using DevExpress.XtraReports.UI;
using DevExpress.ClipboardSource.SpreadsheetML;

namespace YL_DONUT.BizFrm
{
    public partial class frmTest01 : FrmBase
    {
        public frmTest01()
        {
            InitializeComponent();  

        }

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

            InitAccordionControl();


        }


        #endregion

     


        private void InitAccordionControl()
        {
            accordionControl.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            //accordionControl.OptionsHamburgerMenu.DisplayMode = DevExpress.XtraBars.Navigation.AccordionControlDisplayMode.Overlay;

            //Depth 0
            AccordionControlElement dataElement1 = GetElement("dataElement", ElementStyle.Group, "자료");
            this.accordionControl.Elements.Add(dataElement1);

            //Depth 1
            AccordionControlElement generalElement = GetElement("generalElement", ElementStyle.Group, "일반");

            dataElement1.Elements.Add(generalElement);

            // Depth2
            AccordionControlElement bookElement = GetElement("bookElement", ElementStyle.Item, "도서");

            generalElement.Elements.Add(bookElement);

            // Depth2
            AccordionControlElement encyclopediaElement = GetElement("encyclopediaElement", ElementStyle.Item, "도서");

            generalElement.Elements.Add(encyclopediaElement);

            //Depth 1
            AccordionControlElement warElement = GetElement("warElement", ElementStyle.Group, "전쟁");

            dataElement1.Elements.Add(warElement);

            // Depth2 
            AccordionControlElement warHistoryElement = GetElement("warHistoryElement", ElementStyle.Item, "전쟁사");

            warElement.Elements.Add(warHistoryElement);

            // Depth2
            AccordionControlElement weaponElement = GetElement("weaponElement", ElementStyle.Item, "무기");

            warElement.Elements.Add(weaponElement);

            //Depth 0
            AccordionControlElement dataElement2 = GetElement("dataElement2", ElementStyle.Group, "자료2");
            this.accordionControl.Elements.Add(dataElement2);
        }

        #region 엘리먼트 구하기 - GetElement(name, style, text, expanded)

        /// <summary>
        /// 엘리먼트 구하기
        /// </summary>
        /// <param name="name">명칭</param>
        /// <param name="style">스타일</param>
        /// <param name="text">텍스트</param>
        /// <param name="expanded">확장 여부</param>
        /// <returns>아코디언 컨트롤 엘리먼트</returns>
        private AccordionControlElement GetElement(string name, ElementStyle style, string text, bool expanded = true)
        {
            AccordionControlElement element = new AccordionControlElement
            {
                Name = name,
                Style = style,
                Text = text,
                Expanded = true
            };

            return element;
        }

        #endregion

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            //XtraReport report = new XtraReport();
            //report.DataSource = CreateData();

            //ReportPrintTool tool = new ReportPrintTool(report);
            //tool.ShowPreview();

            using (MySQLConn con = new MySQLConn(ConstantLib.BasicConn_Real))
            {
                con.Query = "select a.id as ID_Q, a.p_code as P_CODE_Q,  a.p_name as P_NAME_Q,  " +
                            "       b.idx as IDX_Q, b.name as NAME_Q, b.unit as UNIT_Q, b.qty as QTY_Q, b.endornot as ENDORNOT_Q, b.remark as REMARK_Q, prodseq as PRODSEQ_Q " +
                            "  from domamall.tb_am_product_masters a,  " +
                            "        domamall.tb_am_setproduct b  " +
                            "  where a.id = 1781 and " +
                            "        a.id = b.id  " +
                            "  order by a.p_name, b.idx ";

                ds = con.selectQueryDataSet();
                //ReportDataAgent.Instance().AddDataSource("DataSet1", ds.Tables[0]);
            }

            ds.WriteXmlSchema("DataSet1.xsd");

            XtraReport1 report = new XtraReport1();
            //report.parameter1.Value = 30;
            //report.parameter1.Visible = false;
            report.DataSource = ds.Tables[0];
            report.ShowPreview();
        }

        
    }
}
