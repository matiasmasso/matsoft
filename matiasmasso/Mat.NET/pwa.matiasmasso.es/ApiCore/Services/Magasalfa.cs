using DTO;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Borders;
using iText.Kernel.Font;
using Api.Entities;
using Azure;
using iText.StyledXmlParser.Jsoup.Helper;
using iText.Layout.Properties;

namespace Api.Services
{
    public class Magasalfa
    {
        public int FraNum { get; set; }
        public DateTime Fch { get; set; }

        private float corporateColWidth = 12F;
        private static float availableWidth;

        public Magasalfa(int fraNum, DateTime fch)
        {
            FraNum = fraNum;
            Fch = fch;
        }

        public byte[] ByteArray()
        {
            var ps = PageSize.A4.Rotate();
            var pageWidth = ps.GetWidth();
            availableWidth = pageWidth - corporateColWidth;

            float[] mainLayoutColumnWidths = { corporateColWidth, availableWidth };
            var mainLayout = new Table(mainLayoutColumnWidths);
            mainLayout.SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);
            mainLayout.SetHorizontalBorderSpacing(25);

            mainLayout.AddCell(CorporateData());
            mainLayout.AddCell(ContentData());

            // Creating a PdfWriter
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = new PdfWriter(ms);

            // Creating a PdfDocument       
            PdfDocument pdf = new PdfDocument(writer);

            // Creating a Document        
            Document document = new Document(pdf);
            document.Add(mainLayout);

            // Closing the document       
            document.Close();
            byte[] retval = ms.ToArray();
            return retval;
        }
        public static Cell CorporateData()
        {
            var paragraph = new Paragraph("TATITA 84, S.L. - Domicilio Social: Diagonal 403, 08008 Barcelona - NIF B67246132 - Inscrita en el Registro Mercantil de Barcelona, Tomo 46497, Folio 16, Hoja B 521947");
                paragraph.SetRotationAngle(Math.PI / 2);
            paragraph.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.VISIBLE);
            return BorderlessCell()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
            .Add(paragraph).SetFontSize(8);
        }
        Cell ContentData()
        {
            float[] columnWidths = { availableWidth };
            var layout = new Table(columnWidths);
            layout.SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);
            layout.SetVerticalBorderSpacing(20);

            layout.AddCell(BorderlessCell().Add(Header()));
            layout.AddCell(BorderlessCell().Add(NumFch()));
            layout.AddCell(BorderlessCell().Add(Customer()));
            layout.AddCell(BorderlessCell().Add(Items()));

            var retval = BorderlessCell()
                .Add(layout);
            return retval;

        }


        Table Customer()
        {
            float[] pointColumnWidths = { availableWidth  };
            Table retval = new Table(pointColumnWidths);

            retval.AddCell(BorderlessCell()
                .Add(new Paragraph("MAGASALFA, S.L.")));
            retval.AddCell(BorderlessCell()
                .Add(new Paragraph("NIF B65712721")));
            retval.AddCell(BorderlessCell()
                .Add(new Paragraph("Diagonal 403")));
            retval.AddCell(BorderlessCell()
                .Add(new Paragraph("08008 Barcelona")));

            return retval;
        }

        Table Items()
        {
            float[] pointColumnWidths = { availableWidth - 150, 150 };
            Table retval = new Table(pointColumnWidths);

            AddItem(retval, "Concepto", "Importe");
            AddItem(retval, "Alquiler de las oficinas situadas en Barcelona, Avda. Diagonal 403 2º 1ª, con ref.catastral 9531605DF2893B0007PL");
            AddItem(retval, "Cuota mensual", "4.600,00 €");
            //AddItem(retval, "Abono reposición fluorescentes", "-600,00 €");
            //AddItem(retval, "Base imponible", "4.000,00 €");
            //AddItem(retval, "IVA 21%", "840,00 €");
            //AddItem(retval, "IRPF 19%", "-760,00 €");
            //AddItem(retval, "Total", "4.080,00 €");
            AddItem(retval, "IVA 21%", "966,00 €");
            AddItem(retval, "IRPF 19%", "-874,00 €");
            AddItem(retval, "Total", "4.692,00 €");


            return retval;
        }

        void AddItem(Table table, string concepte, string? import = null)
        {
            table.AddCell(BorderlessCell()
                .Add(new Paragraph(concepte))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT));

            if (import == null)
                table.AddCell(BorderlessCell());
            else
                table.AddCell(BorderlessCell()
                    .Add(new Paragraph(import))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
        }
        Table NumFch()
        {
            float[] pointColumnWidths = { availableWidth - 160, 80, 80 };
            Table retval = new Table(pointColumnWidths);

            var franum = $"{FraNum}/{Fch.Year}";
            var fch = $"{Fch:dd/MM/yyyy}";

            Cell fraLabel = BorderlessCell()
                .Add(new Paragraph("Factura"))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Cell fraNum = BorderlessCell()
                .Add(new Paragraph(franum))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Cell fchLabel = BorderlessCell()
                .Add(new Paragraph("Fecha"))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Cell fchData = BorderlessCell()
                .Add(new Paragraph(fch))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            retval.AddCell(BorderlessCell());
            retval.AddCell(fraLabel);
            retval.AddCell(fraNum);
            retval.AddCell(BorderlessCell());
            retval.AddCell(fchLabel);
            retval.AddCell(fchData);

            return retval;
        }
        static Table Header()
        {
            var ps = PageSize.A4.Rotate();
            var pageWidth = ps.GetWidth();

            // Creating an ImageData object 
            var rootFolder = Directory.GetCurrentDirectory();
            var imgFilename = System.IO.Path.Combine(rootFolder, @"wwwroot\img\LogoTatita.png");
            ImageData data = ImageDataFactory.Create(imgFilename);

            // Creating an Image object 
            Image logo = new Image(data);
            logo.SetAutoScaleWidth(true);

            // Creating a table object 
            float[] pointColumnWidths = { pageWidth / 3, pageWidth / 3, pageWidth / 3 };
            Table table = new Table(pointColumnWidths);

            table.AddCell(BorderlessCell());

            table.AddCell(BorderlessCell()
                .Add(logo)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

            table.AddCell(BorderlessCell());

            return table;
        }

        static Cell BorderlessCell() => new Cell().SetBorder(Border.NO_BORDER);

    }
}
