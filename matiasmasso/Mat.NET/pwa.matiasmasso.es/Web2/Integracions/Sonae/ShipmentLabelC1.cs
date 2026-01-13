using System.Runtime.Versioning;

namespace Web.Integracions.Sonae
{
    [SupportedOSPlatform("windows")]
    public class ShipmentLabelC1
    {
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

        private Helpers.PdfBase pdf ;

        public ShipmentLabelC1(List<Model> labels)
        {
            pdf = new Helpers.PdfBase(System.Drawing.Printing.PaperKind.A4, true);
            pdf.SetMargins(30, 100, 100, 30);
            foreach (var label in labels)
            {
                if (label != labels.First()) pdf.NewPage();
                DrawPage(label);
            }
        }

        public byte[] ByteArray()
        {
            return pdf.Stream();
        }
        public static Byte[] Pdf(List<Model> labels)
        {
            var shipmentlabels = new ShipmentLabelC1(labels);
            return shipmentlabels.ByteArray();
        }

        private void DrawPage(Model label)
        {
            int textLeftMargin = 40;

            pdf.DrawRectangle(0, 100); // frame
            pdf.DrawRectangle(20, 180, 200, 28); // cod proveidor
            pdf.DrawRectangle(240, 180, pdf.MarginsRectangle().Width - 240 - 20, 28); // nom proveidor
            pdf.DrawRectangle(20, 226, pdf.MarginsRectangle().Width - 240 - 40, 28); // num.comanda
            pdf.DrawRectangle(pdf.MarginsRectangle().Width - 240 - 20, 226, 120, 28); // num.caixa
            pdf.DrawRectangle(pdf.MarginsRectangle().Width - 120 - 20, 226, 120, 28); // total caixas
            pdf.DrawRectangle(20, 288, pdf.MarginsRectangle().Width - 40, 28); // EAN14
            pdf.DrawRectangle(20, 334, pdf.MarginsRectangle().Width - 360 - 40, 28); // Sku
            pdf.DrawRectangle(pdf.MarginsRectangle().Width - 360 - 20, 334, 360, 28); // Qty
            pdf.DrawRectangle(20, 380, pdf.MarginsRectangle().Width - 40, 28); // descripc.producte
            pdf.DrawRectangle(20, 426, pdf.MarginsRectangle().Width - 40, pdf.MarginsRectangle().Height - 426 - 20); // obs

            pdf.Font = new System.Drawing.Font("Helvetica", 9);
            pdf.X = 20;
            pdf.Y = 210;
            pdf.DrawString("Cód. Form / Supplier Code");

            pdf.Y += 46;
            pdf.DrawString("N° Nota Encomenda / Order number");
            pdf.X = pdf.MarginsRectangle().Width - 240 - 20;
            pdf.DrawString("N° Caixa");
            pdf.X = pdf.MarginsRectangle().Width - 120 - 20;
            pdf.DrawString("Total de Caixas");
            pdf.Y += pdf.Font.Height;
            pdf.X = pdf.MarginsRectangle().Width - 240 - 20;
            pdf.DrawString("Carton Number");
            pdf.X = pdf.MarginsRectangle().Width - 120 - 20;
            pdf.DrawString("Total Cartons");

            pdf.X = 20;
            pdf.Y = 318;
            pdf.DrawString("EAN14");

            pdf.Y += 46;
            pdf.DrawString("Sku");

            pdf.X = pdf.MarginsRectangle().Width - 360 - 20;
            pdf.DrawString("Quantidade");

            pdf.X = 20;
            pdf.Y += 46;
            pdf.DrawString("Descrição do produto / Product description");

            pdf.Font = new System.Drawing.Font("Helvetica", 19, System.Drawing.FontStyle.Bold);
                pdf.X = 350;
                pdf.Y = 20;
            pdf.DrawStringLine(string.Format("MADE IN {0}", label.MadeIn));

            pdf.X = textLeftMargin;
            pdf.Y = 183;
            pdf.DrawString(label.ProveedorNum ?? "");

            pdf.X = 350;
            pdf.DrawString("MATIAS MASSO, S.A.");

            pdf.X = textLeftMargin;
            pdf.Y = 229;
            pdf.DrawString(label.PurchaseOrder ?? "");

            int iDataWidth = pdf.MeasureStringWidth(label.Bulto.ToString());
            pdf.X =  pdf.MarginsRectangle().Width - 20 - 240 + (120 - iDataWidth) / 2;
            pdf.DrawString(label.Bulto.ToString());

            iDataWidth = pdf.MeasureStringWidth(label.Bultos.ToString());
            pdf.X = pdf.MarginsRectangle().Width - 20 - 120 + (120 - iDataWidth) / 2;
            pdf.DrawString(label.Bultos.ToString());

                pdf.X = textLeftMargin;
                pdf.Y = 291;
                pdf.DrawString(label.Ean13 ?? "");


                pdf.X = textLeftMargin;
                pdf.Y = 337;
                pdf.DrawString(label.SkuRefCustomer ?? "");

            pdf.Y = 337;
            pdf.X = pdf.MarginsRectangle().Width - 360 - 20 + 20;
            pdf.DrawString(label.Qty.ToString());

            pdf.X = textLeftMargin;
            pdf.Y = 383;
            pdf.DrawString(label.SkuNom ?? "");

            var rootFolder = Directory.GetCurrentDirectory();
            var imgFilename = Path.Combine(rootFolder, @"wwwroot\img\integracions\Sonae\Recycle.jpg");
            System.Drawing.Image recycleImage = System.Drawing.Image.FromFile(imgFilename);
            System.Drawing.Rectangle rc = new System.Drawing.Rectangle(pdf.MarginsRectangle().Width, 140, 60, 59);
            pdf.DrawImage(recycleImage, rc);

            if (!string.IsNullOrEmpty(label.Ean13))
            {
                int barcodeWidth =500;
                int barcodeHeight = pdf.MarginsRectangle().Height - 426 - 20 -10; //pdf.MarginsRectangle().Height - 426 - 20;
                var oBarcodeImg = Helpers.BarcodeHelper.EAN13(label.Ean13, barcodeWidth, barcodeHeight).Image()!;

                pdf.X = pdf.MarginsRectangle().Left + (pdf.MarginsRectangle().Width - oBarcodeImg.Width) / 2;
                pdf.Y = pdf.MarginsRectangle().Bottom - 20 - barcodeHeight -5; // Bottom - 20 - barcodeHeight;
                rc = new System.Drawing.Rectangle(pdf.X, pdf.Y, oBarcodeImg.Width, oBarcodeImg.Height);
                pdf.DrawImage(oBarcodeImg, rc);
            }
        }

    }
}
