using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConvertTifToPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = string.Format(@"{0}\{1}\", Environment.CurrentDirectory, "SampleFile");
            ToPDF(folder, "ExampleTIFF.tiff","FileConverted.PDF");
        }

        public static void ToPDF(string folder,string tifFileName,string pdfFileName) {
            Console.WriteLine("Init");
            PdfDocument doc = new PdfDocument();
           
            string destinaton = folder + pdfFileName;
            Image MyImage = Image.FromFile(folder+ tifFileName);

            for (int PageIndex = 0; PageIndex < MyImage.GetFrameCount(FrameDimension.Page); PageIndex++)
            {
                MyImage.SelectActiveFrame(FrameDimension.Page, PageIndex);

                XImage img = XImage.FromGdiPlusImage(MyImage);
                var page = new PdfPage();
                page.Width = img.PointWidth;
                page.Height = img.PointHeight;
                page.Orientation = PdfSharp.PageOrientation.Portrait;
                doc.Pages.Add(page);
                XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[PageIndex]);
                xgr.DrawImage(img, 0, 0);
            }

            doc.Save(destinaton);
            doc.Close();
            MyImage.Dispose();
            Console.WriteLine("Done!!!");
        }
        
    }
}
