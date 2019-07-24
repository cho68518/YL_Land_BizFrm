using DevExpress.XtraCharts;
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

namespace YL_GM.BizFrm
{
    public partial class frmTest01 : FrmBase
    {
        public frmTest01()
        {
            InitializeComponent();
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();

            this.IsMenuVw = true;

        }

        private void EfwSimpleButton1_Click(object sender, EventArgs e)
        {
            //View1();
            View2();
        }

        private void View1()
        {
            //Data 생성
            DataTable dt = GetData(); new DataTable();

            ChartControl chart = new ChartControl();
            chart.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(chart);
            Dictionary<string, double> totalInfo = new Dictionary<string, double>();

            //TotalQty 집계
            foreach (DataRow row in dt.Rows)
            {
                string name = row["NAME"].ToString();
                string year = row["YEAR"].ToString();
                int quantity = (int)row["QUANTITY"];

                if (totalInfo.ContainsKey(name) == false)
                    totalInfo.Add(name, 0);

                totalInfo[name] += quantity;
            }

            Series series = new Series("Total", ViewType.Pie);
            chart.Series.Add(series);

            //Label값을 데이터의 값 그대로 나오도록 설정
            (series.Label.PointOptions as PiePointOptions).PercentOptions.ValueAsPercent = false;
            series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.General;

            //Legend 표시부분 변경
            series.LegendTextPattern = "{A}";

            //ExplodeMode 설정 - ExplodeMode에 따라 Pie를 분리시켜 강조할 수 있음
            (series.View as PieSeriesView).ExplodeMode = PieExplodeMode.MaxValue;

            foreach (KeyValuePair<string, double> info in totalInfo)
            {
                string name = info.Key;
                double quantity = info.Value;
                SeriesPoint point = new SeriesPoint(name, quantity);

                series.Points.Add(point);
            }

            ChartTitle title = new ChartTitle();
            title.Text = "누적 실적 수량";
            chart.Titles.Add(title);
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();

            DataColumn colName = new DataColumn("NAME", typeof(string));
            DataColumn colYear = new DataColumn("YEAR", typeof(string));
            DataColumn colQuantity = new DataColumn("Quantity", typeof(int));

            dt.Columns.Add(colName);
            dt.Columns.Add(colYear);
            dt.Columns.Add(colQuantity);

            dt.Rows.Add("사과", "2016년", 10);
            dt.Rows.Add("바나나", "2016년", 25);
            dt.Rows.Add("귤", "2016년", 15);
            dt.Rows.Add("사과", "2017년", 30);
            dt.Rows.Add("바나나", "2017년", 20);
            dt.Rows.Add("귤", "2017년", 10);
            dt.Rows.Add("사과", "2018년", 15);
            dt.Rows.Add("바나나", "2018년", 30);
            dt.Rows.Add("귤", "2018년", 5);

            return dt;
        }

        private void View2()
        {
            for (int i = 0; i < chartControl2.Series.Count; i++)
                this.chartControl2.Series[i].Points.Clear();

            DataTable dt = GetData2(); new DataTable();

            //Label값을 데이터의 값 그대로 나오도록 설정



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //SeriesPoint sPont = new SeriesPoint(dt.Rows[i]["NAME"].ToString(), Convert.ToInt16(dt.Rows[i]["Quantity"]));
                SeriesPoint sPont = new SeriesPoint(dt.Rows[i]["NAME"].ToString(), dt.Rows[i]["Quantity"].ToString());
                this.chartControl2.Series["Series 1"].Points.Add(sPont);
            }

        }

        private DataTable GetData2()
        {
            DataTable dt = new DataTable();

            //DataColumn colName = new DataColumn("NAME", typeof(string));
            //DataColumn colYear = new DataColumn("YEAR", typeof(string));
            //DataColumn colQuantity = new DataColumn("Quantity", typeof(int));
            DataColumn colName = new DataColumn("NAME", typeof(string));
            //DataColumn colQuantity = new DataColumn("Quantity", typeof(int));
            DataColumn colQuantity = new DataColumn("Quantity", typeof(string));

            dt.Columns.Add(colName);
            dt.Columns.Add(colQuantity);

            dt.Rows.Add("셰프이사", "1,000");
            dt.Rows.Add("도마셰프", "2,000");
            dt.Rows.Add("VIP", "3,000");
            dt.Rows.Add("도마", "4,000");

            return dt;
        }

        private static void AddRow(DataTable dt, string sVal1, double nVal1)
        {
            DataRow row = dt.NewRow();
            row[0] = sVal1;
            row[1] = nVal1;
            dt.Rows.Add(row);
        }





    }
}
