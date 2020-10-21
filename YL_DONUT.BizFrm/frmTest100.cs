using Easy.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YL_DONUT.BizFrm
{
    public partial class frmTest100 : FrmBase
    {
        public frmTest100()
        {
            InitializeComponent();
        }

        public override void FrmLoadEvent()
        {
            base.FrmLoadEvent();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("맑은고딕", 9);

            this.IsMenuVw = true;
            this.IsSearch = true;
            this.IsNewMode = false;
            this.IsSave = true;
            this.IsDelete = false;
            this.IsCancel = false;
            this.IsPrint = false;
            this.IsExcel = false;
        }

        public static void DrawText(String text, Font font, Color textColor, int maxWidth, String path)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font, maxWidth);

            //set the stringformat flags to rtl
            StringFormat sf = new StringFormat();
            //uncomment the next line for right to left languages
            //sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            sf.Trimming = StringTrimming.Word;
            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);
            //Adjust for high quality
            drawing.CompositingQuality = CompositingQuality.HighQuality;
            drawing.InterpolationMode = InterpolationMode.HighQualityBilinear;
            drawing.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawing.SmoothingMode = SmoothingMode.HighQuality;
            drawing.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            //paint the background
            drawing.Clear(Color.Transparent);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, new RectangleF(0, 0, textSize.Width, textSize.Height), sf);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();
            img.Save(path, ImageFormat.Png);
            img.Dispose();

        }

        private void efwSimpleButton1_Click(object sender, EventArgs e)
        {
            Font font = new Font("굴림", 10);

            //DrawText(efwMemoEdit1.EditValue.ToString(), font, Color.Red, efwMemoEdit1.Width, "D:\\aaa.png");
            DrawText(efwMemoEdit1.EditValue.ToString(), font, Color.Red, 100, "D:\\aaa.png");
        }

        private void samp1()
        {
            string firstText = "2020년 07월";
            string secondText = "2,400 원";

            PointF firstLocation = new PointF(16f, 9f);
            PointF secondLocation = new PointF(310f, 80f);

            Font firstFont = new Font("돋움체", 14, FontStyle.Bold);
            Font secondFont = new Font("돋움체", 26, FontStyle.Bold);

            string imageFilePath = @"D:\작업1\back1.PNG";
            string imageFilePath_res = @"D:\작업1\res.png";

            Bitmap newBitmap;
            using (var bitmap = (Bitmap)Image.FromFile(imageFilePath))//load the image file
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    //using (Font arialFont = new Font("굴림", 9))
                    //{
                    //    graphics.DrawString(firstText, firstFont, Brushes.Blue, firstLocation);
                    //    graphics.DrawString(secondText, arialFont, Brushes.Red, secondLocation);
                    //}
                    graphics.DrawString(firstText, firstFont, Brushes.Black, firstLocation);
                    graphics.DrawString(secondText, secondFont, Brushes.Black, secondLocation);
                }
                newBitmap = new Bitmap(bitmap);
            }

            newBitmap.Save(imageFilePath_res);//save the image file
            newBitmap.Dispose();
        }

        private void efwSimpleButton2_Click(object sender, EventArgs e)
        {
            samp1();
        }
    }
}
