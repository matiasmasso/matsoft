using Api.Entities;
using Azure;
using DocumentFormat.OpenXml.Presentation;
using DTO;
using DTO.Integracions.Verifactu;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;

namespace Api.Services
{

    public class CustomerInvoiceService
    {
        private static float corporateColWidth = 12F;
        private static float availableWidth;
        private static Cell BorderlessCell() => new Cell().SetBorder(Border.NO_BORDER);
        //private static Cell BorderlessCell() => new Cell().SetBorder(new iText.Layout.Borders.SolidBorder(1));


        public static CustomerInvoiceModel? GetValue(Guid guid)
        {

            using (var db = new Entities.MaxiContext())
            {

                return db.Fras
                    .Include(x => x.Cca)
                    .Include(x => x.FraItems)
                     .Where(x => x.Guid == guid)
                     .Select(x => new CustomerInvoiceModel(x.Guid)
                     {
                         Emp = (EmpModel.EmpIds?)x.Emp,
                         Serie = x.Serie.ToString(),
                         FraNum = x.Fra1,
                         Fch = DateOnly.FromDateTime(x.Fch),
                         Customer = x.CliGuid,
                         RaoSocial = x.Nom,
                         NIF = x.Nif,
                         Address = x.Adr,
                         Zip = new ZipDTO((Guid)x.Zip!),
                         IVApct = x.IvaStdPct,
                         IRPFpct = x.IrpfPct,
                         Cca = (x.Cca != null) ? new CcaModel(x.Cca.Guid) : null,
                         CtaCredit = x.CtaCredit,
                         CtaDebit = x.CtaDebit,
                         Docfile = (x.Cca != null && x.Cca.Hash != null) ? new DocfileModel(x.Cca.Hash) : null,
                         VfCsv = x.SiiCsv,
                         VfFch = x.SiiFch,
                         VfConcept = x.VfConcept,
                         VfTaxScheme = x.VfTaxScheme,
                         VfTaxType = x.VfTaxType,
                         VfTaxException = x.VfTaxException,
                         Lang = new LangDTO(x.Lang),
                         Items = x.FraItems.OrderBy(x => x.Idx).Select(y => new CustomerInvoiceModel.Item()
                         {
                             Concept = y.Concept,
                             Price = y.Price
                         }).ToList()

                     }).FirstOrDefault();
            }
        }

        public static bool SetCsv(Guid guid, string csv)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Fras.FirstOrDefault(x => x.Guid == guid);
                if (entity == null) throw new System.Exception($"No invoices found with guid {guid.ToString()}");

                entity.SiiCsv = csv;
                db.SaveChanges();
                return true;
            }
        }

        public static bool Update(CustomerInvoiceModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var cca = UpdateCca(db, value);
                UpdateFra(db, value, cca);
                db.SaveChanges();
                return true;
            }
        }

        public static bool RebuildPdf(CustomerInvoiceModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Fras.FirstOrDefault(x => x.Guid == value.Guid);
                if (entity == null) throw new System.Exception($"invoice not found at {value.Guid.ToString()}");
                if (entity.CcaGuid == null) throw new System.Exception($"no Cca available at invoice {value.Guid.ToString()}");

                var cca = CcaService.Find((Guid)entity.CcaGuid);
                if (cca == null) throw new System.Exception("Cca not found at {value.Guid.ToString()}");

                cca.Docfile = value.Docfile;
                CcaService.Update(db, cca);
                db.SaveChanges();
            }
            return true;
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Fras.FirstOrDefault(x => x.Guid == guid);
                if (entity != null)
                {
                    CcaService.Delete(db, (Guid)entity.CcaGuid!);
                    db.Fras.Remove(entity);
                }
                db.SaveChanges();
            }
            return true;
        }


        public static void UpdateFra(MaxiContext db, CustomerInvoiceModel value, CcaModel cca)
        {
            Entities.Fra? entity;
            var fch = value.Fch!;
            if (value.IsNew)
            {
                entity = new Entities.Fra();
                entity.Guid = value.Guid;
                entity.FchCreated = DateTime.Now;
                db.Fras.Add(entity);
            }
            else
            {
                entity = db.Fras
                    .FirstOrDefault(x => x.Guid == value.Guid);

                if (entity == null) throw new System.Exception("Fra not found");
            }

            entity.Emp = (int)value.Emp!;
            entity.Yea = (short)value.Fch.Year;
            entity.Serie = 0;
            entity.Fra1 = value.FraNum;
            entity.Fch = value.Fch.ToDateTime(TimeOnly.MinValue);
            entity.Vto = value.Vto.ToDateTime(TimeOnly.MinValue);
            entity.Nom = value.RaoSocial;
            entity.Nif = value.NIF;
            entity.NifCod = 1;
            entity.Adr = value.Address;
            entity.Zip = value.Zip?.Guid;
            entity.SumItems = (decimal)value.BaseImponible!;
            entity.Cur = "EUR";
            entity.EurBase = entity.SumItems;
            entity.IvaStdBase = entity.SumItems;
            entity.IvaStdPct = value.IVApct ?? 0;
            entity.IvaStdAmt = value.IVA;
            entity.EurLiq = value.Total;
            if (value.IRPFpct != null && value.IRPFpct != 0)
            {
                entity.IrpfPct = value.IRPFpct;
                entity.IrpfBase = entity.SumItems;
                entity.IrpfAmt = value.IRPF;
            }
            entity.CliGuid = (Guid)value.Customer!;
            entity.CcaGuid = cca.Guid;
            entity.CtaCredit = value.CtaCredit;
            entity.CtaDebit = value.CtaDebit;
            entity.Lang = value.Lang?.Tag() ?? "ESP";
            entity.SiiCsv = value.VfCsv;
            entity.SiiFch = value.VfFch;
            entity.VfConcept = value.VfConcept;
            entity.VfTaxScheme = value.VfTaxScheme;
            entity.VfTaxType = value.VfTaxType;
            entity.VfTaxException = value.VfTaxException;

            entity.FchLastEdited = entity.FchCreated;

            // --- Safely remove existing items from the database to avoid duplicate PKs ---
            // Query FraItem rows that belong to this Fra (don't rely on navigation being loaded)
            var existingItems = db.FraItems
                .Where(fi => fi.Fra == entity.Guid)
                .ToList();

            if (existingItems.Count > 0)
            {
                db.FraItems.RemoveRange(existingItems);
            }

            // Build new items list explicitly, set foreign key (Fra) and index (Idx)
            var newItems = new List<Entities.FraItem>();
            var items = value.Items ?? new List<CustomerInvoiceModel.Item>();
            for (int i = 0; i < items.Count; i++)
            {
                var x = items[i];
                var fraItem = new Entities.FraItem()
                {
                    Fra = entity.Guid, // explicit FK to avoid ambiguity
                    Idx = (short)(i + 1),
                    Concept = x.Concept,
                    Price = x.Price
                };
                newItems.Add(fraItem);
            }

            // Replace navigation collection with the new list (safe even if it was null)
            entity.FraItems = newItems;

        }


        public static CcaModel UpdateCca(MaxiContext db, CustomerInvoiceModel value)
        {
            CcaModel cca;
            var ctas = PgcCtasService.GetValues();
            if (value.Cca == null)
                cca = CcaModel.Factory(value.Emp!, value.User!, CcaModel.CcdEnum.FacturaNostre, value.Fch);
            else
            {
                cca = CcaService.Find(value.Cca.Guid);
                cca.Items.Clear();
            }

            cca.Ccd = CcaModel.CcdEnum.FacturaNostre;
            cca.Cdn = value.FraNum;
            cca.Concept = $"Factura {value.FraNum:000}/{value.Fch.Year} a {value.RaoSocial}";
            cca.AddCredit(value.BaseImponible, new PgcCtaModel((Guid)value.CtaCredit!), value.Customer);
            if (value.ShouldShowIVA())
                cca.AddCredit(value.IVA, ctas.First(x => x.Cod == PgcCtaModel.Cods.IvaRepercutitNacional), value.Customer);
            if (value.ShouldShowIRPF())
                cca.AddDebit(value.IRPF, ctas.First(x => x.Cod == PgcCtaModel.Cods.IrpfLloguers), value.Customer);
            cca.AddSaldo(new PgcCtaModel((Guid)value.CtaDebit!), value.Customer);
            cca.Docfile = value.Docfile;
            CcaService.Update(cca);
            return cca;
        }

        public static Media Media(CustomerInvoiceModel value)
        {
            return new Media(DTO.Media.MimeCods.Pdf, ByteArray(value));
        }

        public static byte[] ByteArray(CustomerInvoiceModel value)
        {
            var ps = PageSize.A4.Rotate();
            var pageWidth = ps.GetWidth();
            availableWidth = pageWidth - corporateColWidth;

            float[] mainLayoutColumnWidths = { corporateColWidth, availableWidth };
            var mainLayout = new Table(mainLayoutColumnWidths);
            mainLayout.SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);
            mainLayout.SetHorizontalBorderSpacing(25);

            mainLayout.AddCell(CorporateData(value));
            mainLayout.AddCell(ContentData(value));

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


        static Cell CorporateData(CustomerInvoiceModel value)
        {
            string dadesRegistrals = string.Empty;
            switch (value.Emp)
            {
                case EmpModel.EmpIds.MMC:
                    dadesRegistrals = "Massó Cases, Matias - Domicilio Fiscal: Plaça Santa Maria 3, 17520 Puigcerdà (Girona) - NIF 37697236Y";
                    break;
                case EmpModel.EmpIds.Tatita:
                    dadesRegistrals = "TATITA 84, S.L. - Domicilio Social: Diagonal 403, 08008 Barcelona - NIF B67246132 - Inscrita en el Registro Mercantil de Barcelona, Tomo 46497, Folio 16, Hoja B 521947";
                    break;
            }
            var paragraph = new Paragraph(dadesRegistrals);
            paragraph.SetRotationAngle(Math.PI / 2);
            paragraph.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.VISIBLE);
            return BorderlessCell()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
            .Add(paragraph).SetFontSize(8);
        }

        static Cell ContentData(CustomerInvoiceModel value)
        {
            float[] columnWidths = { availableWidth };
            var layout = new Table(columnWidths);
            layout.SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);
            layout.SetVerticalBorderSpacing(20);

            layout.AddCell(BorderlessCell().Add(Header(value)));
            layout.AddCell(BorderlessCell().Add(NumFch(value)));
            layout.AddCell(BorderlessCell().Add(Customer(value)));
            layout.AddCell(BorderlessCell().Add(Items(value)));

            var retval = BorderlessCell()
                .Add(layout);
            return retval;

        }

        static Table Customer(CustomerInvoiceModel value)
        {
            float[] pointColumnWidths = { availableWidth };
            Table retval = new Table(pointColumnWidths);

            retval.AddCell(BorderlessCell()
                .Add(new Paragraph(value.RaoSocial)));
            retval.AddCell(BorderlessCell()
                .Add(new Paragraph($"NIF {value.NIF}")));
            retval.AddCell(BorderlessCell()
                .Add(new Paragraph(value.Address)));
            retval.AddCell(BorderlessCell()
                .Add(new Paragraph(value.Zip?.FullNom())));

            return retval;
        }

        static Table Items(CustomerInvoiceModel value)
        {
            float[] pointColumnWidths = { availableWidth - 150, 150 };
            Table retval = new Table(pointColumnWidths);

            AddItem(retval, value.Lang!.Tradueix("Concepto", "Concepte", "Concept"), value.Lang!.Tradueix("Importe", "Import", "Amount"));

            foreach (var item in value.Items)
            {
                if (item.Total == null || item.Total == 0)
                    AddItem(retval, item.Concept);
                else
                    AddItem(retval, item.Concept, item.Total?.ToString("N2") + " €");
            }

            if (value.ShouldShowBaseImponible())
                AddItem(retval, "Base imponible", value.BaseImponible.ToString("N2") + " €");
            if (value.ShouldShowIVA())
            {
                string format = $"N{value.IVApct.GetDecimalPlaces()}";
                AddItem(retval, $"{value.Lang!.Tradueix("IVA","IVA","VAT")} {((decimal)value.IVApct!).ToString(format)}%", $"{value.IVA:N2} €");
            }
            if (value.ShouldShowIRPF())
            {
                string format = $"N{value.IRPFpct.GetDecimalPlaces()}";
                AddItem(retval, $"{value.Lang!.Tradueix("retención","retenció")} IRPF {((decimal)value.IRPFpct!).ToString(format)}%", $"-{value.IRPF:N2} €");
            }
            if (value.ShouldShowTotal())
                AddItem(retval, "Total", value.Total.ToString("N2") + " €");

            if(value.Notas.Count>0)
            {
                AddItem(retval, ""); // línea en blanco antes de las notas
                AddItem(retval,value.Lang.Tradueix("Notas:","Notes:","Notes:")); // línea en blanco antes de las notas
            }

            foreach (var item in value.Notas)
            {
                    AddItem(retval, item.Concept);
            }

            return retval;
        }

        static void AddItem(Table table, string? concepte, string? import = null)
        {
            table.AddCell(BorderlessCell()
                .Add(new Paragraph(concepte ?? ""))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT));

            if (import == null)
                table.AddCell(BorderlessCell());
            else
                table.AddCell(BorderlessCell()
                    .Add(new Paragraph(import))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
        }

        static Table NumFch(CustomerInvoiceModel value)
        {
            float[] pointColumnWidths = { availableWidth - 160, 80, 80 };
            Table retval = new Table(pointColumnWidths);

            var sFranum = $"{value.FraNum}/{value.Fch.Year}";
            var sFch = $"{value.Fch:dd/MM/yyyy}";

            Cell fraLabel = BorderlessCell()
                .Add(new Paragraph(value.Lang!.Tradueix("Factura", "Factura", "Invoice")))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Cell fraNum = BorderlessCell()
                .Add(new Paragraph(sFranum))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Cell fchLabel = BorderlessCell()
                .Add(new Paragraph(value.Lang!.Tradueix("Fecha","Data","Date")))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            Cell fchData = BorderlessCell()
                .Add(new Paragraph(sFch))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            retval.AddCell(BorderlessCell());
            retval.AddCell(fraLabel);
            retval.AddCell(fraNum);
            retval.AddCell(BorderlessCell());
            retval.AddCell(fchLabel);
            retval.AddCell(fchData);

            return retval;
        }

        static Table Header(CustomerInvoiceModel value)
        {
            var ps = PageSize.A4.Rotate();
            var pageWidth = ps.GetWidth();

            var percentCols = UnitValue.CreatePercentArray(new float[] { 1, 1, 1 });
            Table table = new Table(percentCols)
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);

            table.AddCell(QrCell(value));
            table.AddCell(LogoCell(value)); 
            table.AddCell(EmptyCell())
;

            return table;
        }

        static Cell EmptyCell()
        {
            return BorderlessCell();
;
        }

        static Cell QrCell(CustomerInvoiceModel invoice)
        {
            Cell retval = BorderlessCell();
            if (!string.IsNullOrEmpty(invoice.VfQr))
            {
                retval = BorderlessCell()
                    .Add(VfQR(invoice))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);

            }
            return retval;
        }
        static Cell LogoCell(CustomerInvoiceModel invoice)
        {
            float fixedHeight = 100f;
            string imgFilename;
            // Creating an ImageData object 
            var rootFolder = Directory.GetCurrentDirectory();

            if (invoice.Emp == EmpModel.EmpIds.Tatita)
                imgFilename = System.IO.Path.Combine(rootFolder, @"wwwroot\img\LogoTatita.png");
            else //pending other companies
                imgFilename = System.IO.Path.Combine(rootFolder, @"wwwroot\img\LogoMMO.png");

            ImageData data = ImageDataFactory.Create(imgFilename);

            // Creating an Image object 
            Image logo = new Image(data);
            logo.ScaleToFit(float.MaxValue, fixedHeight);
            logo.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

            var retval = BorderlessCell()
                .Add(logo)
                .SetMinHeight(50)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE); 

            return retval;
        }

        static Div VfQR(CustomerInvoiceModel invoice)
        {
            string base64Qr = invoice.VfQr!;
            byte[] qrBytes = Convert.FromBase64String(base64Qr);
            ImageData qrData = ImageDataFactory.Create(qrBytes);
            Image qrImage = new Image(qrData);
            qrImage.ScaleToFit(float.MaxValue, 100f);


            var div = new Div();
            div.SetWidth(100);
            //div.SetHeight(100);
            div.SetTextAlignment(TextAlignment.CENTER);

            Paragraph header = new Paragraph(invoice.Lang!.Tradueix("QR tributario", "QR tributari", "Tax QR")).SetFontSize(8);
            div.Add(header);
            div.Add(qrImage);
            Paragraph footer = new Paragraph("VERI*FACTU").SetFontSize(8);
            div.Add(footer);
            div.SetMarginLeft(-6f); // compensa el padding del Qr
            return div;
        }

    }



    public class CustomerInvoicesService
    {
        public static List<CustomerInvoiceModel> GetValues(EmpModel.EmpIds emp)
        {
            using (var db = new Entities.MaxiContext())
            {

                return db.Fras
                    .Include(x => x.Cca)
                    .Include(x => x.FraItems)
                     .Where(x => x.Emp == (int)emp)
                     .OrderBy(x => x.Serie)
                     .ThenByDescending(x => x.Fra1)
                     .Select(x => new CustomerInvoiceModel(x.Guid)
                     {
                         Emp = (EmpModel.EmpIds?)x.Emp,
                         Serie = x.Serie.ToString(),
                         FraNum = x.Fra1,
                         Fch = DateOnly.FromDateTime(x.Fch),
                         Customer = x.CliGuid,
                         RaoSocial = x.Nom,
                         NIF = x.Nif,
                         Address = x.Adr,
                         Zip = new ZipDTO((Guid)x.Zip!),
                         IVApct = x.IvaStdPct,
                         IRPFpct = x.IrpfPct,
                         VfCsv = x.SiiCsv,
                         VfFch = x.SiiFch,
                         VfConcept = x.VfConcept,
                         VfTaxScheme = x.VfTaxScheme,
                         VfTaxType = x.VfTaxType,
                         VfTaxException = x.VfTaxException,
                         Cca = (x.Cca != null) ? new CcaModel(x.Cca.Guid) : null,
                         CtaCredit = x.CtaCredit,
                         CtaDebit = x.CtaDebit,
                         Docfile = (x.Cca != null && x.Cca.Hash != null) ? new DocfileModel(x.Cca.Hash) : null,
                         Lang = new LangDTO(x.Lang),
                         Items = x.FraItems.OrderBy(x => x.Idx).Select(y => new CustomerInvoiceModel.Item()
                         {
                             Concept = y.Concept,
                             Price = y.Price
                         }).ToList()
                     }).ToList();
            }
        }
    }
}
