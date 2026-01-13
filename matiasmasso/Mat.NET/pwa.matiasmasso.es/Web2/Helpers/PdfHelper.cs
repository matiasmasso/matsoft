using DocumentFormat.OpenXml.InkML;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection.PortableExecutable;
using System.Text;
using static Web.Helpers.ByteArrayPdfSplitter;
using Rectangle = iText.Kernel.Geom.Rectangle;
using Canvas = iText.Layout.Canvas;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DTO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using iText.Signatures;
using iText.Kernel.Pdf.Xobject;

namespace Web.Helpers
{
    public class PdfHelper
    {
        public static List<Byte[]> Split(Byte[] bytes)
        {
            var retval = new List<Byte[]>();
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                using (PdfReader reader = new PdfReader(memoryStream))
                {
                    PdfDocument docToSplit = new PdfDocument(reader);
                    ByteArrayPdfSplitter splitter = new ByteArrayPdfSplitter(docToSplit);
                    splitter.SplitByPageCount(1, new DocumentReadyListener(splitter, retval));
                }
            }

            return retval;
        }

        public static string ReadText(byte[] oByteArray)
        {
            var sb = new StringBuilder();

            if (oByteArray != null)
            {
                var oMemoryStream = new MemoryStream(oByteArray);
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


        public static byte[]? WriteContent(byte[] oByteArray, List<Content> contents)
        {
            MemoryStream? ms = new MemoryStream(oByteArray);
            MemoryStream? result = new MemoryStream();
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(ms), new PdfWriter(result));
            PdfCanvas pageCanvas;
            PdfPage page = pdfDoc.GetPage(1);
            pageCanvas = new PdfCanvas(page);

            // add content
            foreach (var content in contents)
            {
                Rectangle rect = new Rectangle(content.X, content.Y, content.Width, content.Height);
                var contentCanvas = new Canvas(pageCanvas, rect);
                switch (content.Cod)
                {
                    case Content.Cods.Text:
                        Paragraph p = new Paragraph(content.Value.ToString());
                        contentCanvas.Add(p);
                        break;
                    case Content.Cods.Image:
                        ImageData data = ImageDataFactory.Create((byte[])content.Value);
                        iText.Layout.Element.Image image = new iText.Layout.Element.Image(data);
                        image.ScaleAbsolute(content.Width, content.Height);
                        image.SetFixedPosition(1, content.X, content.Y);
                        contentCanvas.Add(image);
                        //canvas.AddImageAt(data, content.X, content.Y, false);

                        break;
                    default:
                        break;
                }
            }

            pdfDoc.Close();
            return result.ToArray();
        }


        public class Content
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

            public object Value { get; set; }

            public Cods Cod { get; set; }

            public enum Cods
            {
                Text,
                Image
            }

            public Content(Cods cod, Object value)
            {
                Cod = cod;
                Value = value;
            }
            public static Content Text(string value, int x, int y, int width, int height)
            {
                return new Content(Cods.Text, value)
                {
                    Width = width,
                    Height = height,
                    X = x,
                    Y = y
                };
            }
            public static Content Image(byte[] bytes, int x, int y, int width, int height)
            {
                return new Content(Cods.Image, bytes)
                {
                    Width = width,
                    Height = height,
                    X = x,
                    Y = y
                };
            }


        }

    }
}
