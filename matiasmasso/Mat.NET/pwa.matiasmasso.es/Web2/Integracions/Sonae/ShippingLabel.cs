using iText.IO.Image;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Geom;
using DTO;
using iText.Kernel.Pdf.Xobject;
using Rectangle = iText.Kernel.Geom.Rectangle;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using DocumentFormat.OpenXml.Drawing.Charts;
using iText.Barcodes;
using iText.Kernel.Colors;

namespace Web.Integracions.Sonae
{
    public class ShippingLabel
    {
        PdfWriter pdfWriter;
        PdfDocument pdfDocument;
        PageSize ps = PageSize.A4.Rotate();
        float hPadding = 20.0F;
        private List<Model> labels = new List<Model>();

        public ShippingLabel(List<Model> labels)
        {
            this.labels = labels;
        }

        public byte[] ByteArray()
        {
            MemoryStream ms = new MemoryStream();
            pdfWriter = new PdfWriter(ms);
            pdfDocument = new PdfDocument(new PdfWriter(pdfWriter));

            foreach (var label in labels)
            {
                var page = pdfDocument.AddNewPage(ps);
                DrawPage(ref page, label);
            }

            pdfDocument.Close();
            return ms.ToArray();
        }

        public static Byte[] Pdf(List<Model> labels)
        {
            var shippingLabels = new ShippingLabel(labels);
            return shippingLabels.ByteArray();
        }

        private void DrawPage(ref PdfPage page, Model label)
        {
            var canvas = new PdfCanvas(page);

            var pageWidth = ps.GetWidth();
            var pageHeight = ps.GetHeight();

            float top, right, bottom, left, width, height;

            float outerMarginTop = 40;
            float outerMarginRight = 100;
            float outerMarginBottom = 40;
            float outerMarginLeft = 100;

            //Store a "backup" of the current graphical state
            canvas.SaveState();

            //Change the page's coordinate system so that 0,0 is at the top left
            canvas.ConcatMatrix(1, 0, 0, 1, ps.GetWidth() / 2, ps.GetHeight() / 2);

            // When joining lines we want them to use a rounded corner
            canvas.SetLineJoinStyle(PdfCanvasConstants.LineJoinStyle.ROUND);

            //Set canvas borders
            var canvasTop = pageHeight / 2 - outerMarginTop;
            var canvasRight = pageWidth / 2 - outerMarginRight;
            var canvasBottom = -pageHeight / 2 + outerMarginBottom;
            var canvasLeft = -pageWidth / 2 + outerMarginLeft;


            //Draw recycle image
            var rootFolder = Directory.GetCurrentDirectory();
            var imgFilename = System.IO.Path.Combine(rootFolder, @"wwwroot\img\Recycle.jpg");
            ImageData data = ImageDataFactory.Create(imgFilename);
            //Image img = new Image(data);
            canvas.AddImageAt(data, canvasRight - 70, canvasTop - 120, false);

            //Draw main rectangle

            float topGap = 65;
            top = canvasTop - topGap;
            right = canvasRight;
            bottom = canvasBottom;
            left = canvasLeft;
            DrawRectangle(canvas, top, right, bottom, left);

            float boxHeight = 24;
            float hSpacer = 18;
            float vSpacer = 24;

            float firstRowTopGap = 65;
            float firstRowTop = canvasTop - topGap - firstRowTopGap;

            //draw madein
            width = 220;
            height = 24;
            top = canvasTop-20;
            right = canvasRight;
            left =right-width;
            bottom = top - boxHeight;
            DrawString(canvas, top, right+hPadding, bottom, left, string.Format("Made in {0}", label.MadeIn), TextAlignment.RIGHT);


            //draw proveedorNum box
            width = 220;
            height = 24;
            top = firstRowTop;
            left = canvasLeft + hSpacer;
            right = left + width;
            bottom = top - boxHeight;
            DrawRectangle(canvas, top, right, bottom, left);
            DrawString(canvas, top, right, bottom, left, label.ProveedorNum ?? "");
            DrawLabel(canvas, bottom, right, left, "Cód. Form / Supplier code");

            //draw M+O box
            left = right + hSpacer;
            right = canvasRight - hSpacer;
            top = firstRowTop;
            bottom = top - boxHeight;
            DrawRectangle(canvas, top, right, bottom, left);
            DrawString(canvas, top, right, bottom, left, "MATIAS MASSO, S.A.");


            //draw 2nd row
            float secondRowTop = firstRowTop - boxHeight - vSpacer;
            top = secondRowTop;
            left = canvasLeft + hSpacer;
            right = canvasRight - hSpacer;
            bottom = top - boxHeight;
            DrawRectangle(canvas, top, right, bottom, left);

            var boxWidth = 100;
            var bultoLeft = right - 4 * boxWidth;
            canvas.MoveTo(bultoLeft, top)
                .LineTo(bultoLeft, bottom);
            var bultosLeft = right - 2 * boxWidth;
            canvas.MoveTo(bultosLeft, top)
                .LineTo(bultosLeft, bottom);

            DrawString(canvas, top, bultoLeft, bottom, left, label.PurchaseOrder ?? "");
            DrawLabel(canvas, bottom, right, left, "Nº Nota Encomenda / Order Number");

            DrawString(canvas, top, bultosLeft, bottom, bultoLeft, label.Bulto.ToString(), TextAlignment.CENTER);
            DrawLabel(canvas, bottom, bultosLeft, bultoLeft, "Nº Caixa / Carton Number", TextAlignment.CENTER);

            DrawString(canvas, top, right, bottom, bultosLeft, label.Bultos.ToString(), TextAlignment.CENTER);
            DrawLabel(canvas, bottom, right, bultosLeft, "Total de Caixas / Total Cartons", TextAlignment.CENTER);

            //draw 3rd row
            //float thirdRowTop = secondRowTop - boxHeight - 2 * vSpacer;
            float thirdRowTop = secondRowTop - boxHeight -  vSpacer;
            top = thirdRowTop;
            left = canvasLeft + hSpacer;
            right = canvasRight - hSpacer;
            bottom = top - boxHeight;
            DrawRectangle(canvas, top, right, bottom, left);
            DrawString(canvas, top, right, bottom, left, label.Ean13 ?? "");
            DrawLabel(canvas, bottom, right, left, "EAN 14");


            //draw 4th row
            float fourthRowTop = thirdRowTop - boxHeight - vSpacer;
            top = fourthRowTop;
            left = canvasLeft + hSpacer;
            right = canvasRight - hSpacer;
            bottom = top - boxHeight;
            DrawRectangle(canvas, top, right, bottom, left);

            var qtyLeft = right - 3 * boxWidth;
            canvas.MoveTo(qtyLeft, top)
                .LineTo(qtyLeft, bottom);

            DrawString(canvas, top, qtyLeft, bottom, left, label.SkuRefCustomer ?? "");
            DrawLabel(canvas, bottom, right, left, "Sku");

            DrawString(canvas, top, right, bottom, qtyLeft, label.Qty.ToString());
            DrawLabel(canvas, bottom, right, qtyLeft, "Quantidade / Quantity");

            //draw 5th row
            float fifthRowTop = fourthRowTop - boxHeight - vSpacer;
            top = fifthRowTop;
            left = canvasLeft + hSpacer;
            right = canvasRight - hSpacer;
            bottom = top - boxHeight;
            DrawRectangle(canvas, top, right, bottom, left);
            DrawString(canvas, top, right, bottom, left, label.SkuNom ?? "");
            DrawLabel(canvas, bottom, right, left, "Descrição do produto / Product description");


            //draw bottom row
            float bottomRowTop = fifthRowTop - boxHeight - vSpacer;
            top = bottomRowTop;
            left = canvasLeft + hSpacer;
            right = canvasRight - hSpacer;
            bottom = canvasBottom + vSpacer;
            DrawRectangle(canvas, top, right, bottom, left);

            DrawEan13(canvas, label.Ean13 ?? "", top, right, bottom, left);

            //"Restore" our "backup" which resets any changes that the above made
            canvas.RestoreState();

        }

        void DrawRectangle(PdfCanvas canvas, float top, float right, float bottom, float left)
        {
            float lineWidth = 0.1F;
            canvas.SetLineWidth(lineWidth)
            .MoveTo(left, top)
            .LineTo(right, top)
            .LineTo(right, bottom)
            .LineTo(left, bottom)
            .LineTo(left, top)
            .Stroke();
        }

        void DrawString(PdfCanvas pageCanvas, float top, float right, float bottom, float left, string value, TextAlignment alignment = TextAlignment.LEFT)
        {
            Rectangle rectangle = new Rectangle(left + hPadding, bottom, right - left - 2 * hPadding, top - bottom);
            Canvas canvas2 = new Canvas(pageCanvas, rectangle);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            Text caption = new Text(value).SetFont(bold);
            Paragraph paragraph = new Paragraph()
                .SetFontSize(18)
                .SetTextAlignment(alignment)
                .Add(caption);
            canvas2.Add(paragraph);
            canvas2.Close();
        }

        void DrawLabel(PdfCanvas pageCanvas, float top, float right, float left, string value, TextAlignment alignment = TextAlignment.LEFT)
        {
            var bottom = top - 12;
            Rectangle rectangle = new Rectangle(left + hPadding, bottom, right - left - 2 * hPadding, top - bottom);
            Canvas canvas2 = new Canvas(pageCanvas, rectangle);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            Text caption = new Text(value).SetFont(font);
            Paragraph paragraph = new Paragraph()
                .SetFontSize(8)
                .SetTextAlignment(alignment)
                .Add(caption);
            canvas2.Add(paragraph);
            canvas2.Close();
        }

        private void DrawEan13(PdfCanvas pageCanvas, String ean13, float top, float right, float bottom, float left)
        {
            var vPadding = 20.0F;
            var hPadding = 20.0F;
            var barWidth = 6.0F;
            BarcodeEAN barcode = new BarcodeEAN(pdfDocument);
            barcode.SetCodeType(BarcodeEAN.EAN13);
            barcode.SetBarHeight(top-bottom-2* vPadding); // great! but what about width???
            //barcode.SetX(barWidth);
            barcode.SetCode(ean13);
            barcode.FitWidth(right - left - 2*hPadding);

            PdfFormXObject barcodeXObject = barcode.CreateFormXObject(ColorConstants.BLACK, ColorConstants.BLACK, pdfDocument);
            pageCanvas.AddXObjectAt(barcodeXObject, left+hPadding, bottom+ vPadding);
        }

        public class Model
        {
            public string? ProveedorNum { get; set; }
            public string? PurchaseOrder { get; set; }
            public int Bultos { get; set; }
            public int Bulto { get; set; }
            public string? SkuNom { get; set; }
            public string? SkuRefCustomer { get; set; }
            public string? Ean13 { get; set; }
            public string? MadeIn { get; set; }
            public int Qty { get; set; }
        }
    }



}




