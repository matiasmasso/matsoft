using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.Kernel.Geom;
using System.Drawing;
using MatHelperCFwk.Components;

namespace MatHelperCFwk
{
    public class PdfHelper
    {
        public static List<string> SplitPdf(string filename, string outputDir ="")
        {
            List<string> retval = new List<string>();
            
            using (var pdfDoc = new PdfDocument(new PdfReader(filename)))
            {
                var splitter = new CustomPdfSplitter(pdfDoc,filename, outputDir);
                var splittedDocs = splitter.SplitByPageCount(1);

                foreach (var splittedDoc in splittedDocs)
                {
                    splittedDoc.Close();
                }
                retval = splitter.PageFilenames;
            }
            return retval;
        }

        public static string readText(string filename, List<Exception> exs)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (System.IO.File.Exists(filename))
            {
                using (PdfReader reader = new PdfReader(filename))
                {
                    using (PdfDocument pdf = new PdfDocument(reader))
                    {
                        for (int page = 1; page <= pdf.GetNumberOfPages(); page++)
                        {
                            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                            var oPage = pdf.GetPage(page);
                            string currentText = PdfTextExtractor.GetTextFromPage(oPage, strategy);
                            var res = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                            sb.Append(res);
                        }
                    }
                }
            }
            return sb.ToString();
        }

        public static string readText(byte[] oByteArray, List<Exception> exs)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (oByteArray != null)
            {
                System.IO.MemoryStream oMemoryStream = new System.IO.MemoryStream(oByteArray);
                using (PdfReader reader = new PdfReader(oMemoryStream))
                {
                    using (PdfDocument pdf = new PdfDocument(reader))
                    {
                        for (int page = 1; page <= pdf.GetNumberOfPages(); page++)
                        {
                            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                            var oPage = pdf.GetPage(page);
                            string currentText = PdfTextExtractor.GetTextFromPage(oPage, strategy);
                            var res = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                            sb.Append(res);
                        }
                    }
                }
            }
            return sb.ToString();
        }


        public static bool Write(List<Exception> exs, string srcFilename, string destFilename, IEnumerable<MatPdfObject> oObjectsToAdd)
        {
            if (System.IO.File.Exists(srcFilename))
            {
                try
                {

                    // Modify PDF located at "source" And save to "target"
                    PdfDocument PdfDocument = new PdfDocument(new PdfReader(srcFilename), new PdfWriter(destFilename));
                    // Document to add layout elements: paragraphs, images etc
                    iText.Layout.Document Document = new iText.Layout.Document(PdfDocument);

                    foreach (var oObject in oObjectsToAdd)
                    {
                        if (oObject is MatPdfText)
                        {
                            MatPdfText oPdfText = (MatPdfText)oObject;
                            PdfFont oFont = PdfFontFactory.CreateFont(oPdfText.font.ToString()); // oPdfText.font.ToString())
                            iText.Layout.Element.Text oText = new iText.Layout.Element.Text(oPdfText.text);
                            oText.SetFont(oFont);
                            iText.Layout.Element.Paragraph p = new iText.Layout.Element.Paragraph();
                            p.Add(oText);
                            p.SetMarginTop(oPdfText.rectangle.Y);
                            p.SetMarginLeft(oPdfText.rectangle.X);
                            Document.Add(p);
                        }
                        else if (oObject is MatPdfImage)
                        {
                            MatPdfImage oPdfImage = (MatPdfImage)oObject;
                            var oImageByteArray = ImageHelper.GetByteArrayFromImg(oPdfImage.Image);
                            ImageData ImageData = ImageDataFactory.Create(oImageByteArray);

                            // Create layout image object And provide parameters. Page number = 1
                            iText.Kernel.Geom.Rectangle oCropBox = Document.GetPdfDocument().GetPage(1).GetCropBox();
                            iText.Layout.Element.Image Image = new iText.Layout.Element.Image(ImageData);
                            Image.ScaleAbsolute(oPdfImage.rectangle.Width, oPdfImage.rectangle.Height);
                            Image.SetFixedPosition(1, oPdfImage.rectangle.Left, oCropBox.GetHeight() - oPdfImage.rectangle.Top);

                            // This adds the image to the page
                            Document.Add(Image);
                        }
                    }

                    // Don't forget to close the document.
                    // When you use Document, you should close it rather than PdfDocument instance
                    Document.Close();
                }
                catch (Exception ex)
                {
                    exs.Add(ex);
                }
            }
            return exs.Count == 0;
        }


    }

    public class MatPdfObject
    {
        public System.Drawing.Rectangle rectangle { get; set; }

        public enum Fonts
        {
            COURIER,
            COURIER_BOLD,
            COURIER_OBLIQUE,
            COURIER_BOLDOBLIQUE,
            Helvetica,
            HELVETICA_BOLD,
            HELVETICA_OBLIQUE,
            HELVETICA_BOLDOBLIQUE,
            SYMBOL,
            TIMES_ROMAN,
            TIMES_BOLD,
            TIMES_ITALIC,
            TIMES_BOLDITALIC,
            ZAPFDINGBATS
        }
    }

    public class MatPdfText : MatPdfObject
    {
        public string text { get; set; }
        public MatPdfObject.Fonts font { get; set; }


        public static MatPdfText Factory(string text, System.Drawing.Rectangle rectangle, MatPdfObject.Fonts font = MatPdfObject.Fonts.Helvetica)
        {
            MatPdfText retval = new MatPdfText();
            {
                var withBlock = retval;
                withBlock.text = text;
                withBlock.rectangle = rectangle;
                withBlock.font = font;
            }
            return retval;
        }
    }

    public class MatPdfImage : MatPdfObject
    {
        public Image Image { get; set; }

        public static MatPdfImage Factory(Image oImage, System.Drawing.Rectangle rectangle)
        {
            MatPdfImage retval = new MatPdfImage();
            {
                var withBlock = retval;
                withBlock.Image = oImage;
                withBlock.rectangle = rectangle;
            }
            return retval;
        }
    }

}
