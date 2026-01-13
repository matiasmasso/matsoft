using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DTO.Integracions.Promofarma
{
    public class Feed {
        public List<Item> Items = new List<Item>();

        public class Item : DTOBaseGuid
        {
            public string EAN13 { get; set; }
            public string CN { get; set; } = string.Empty;
            public string Laboratorio { get; set; } = "MATIAS MASSO, S.A.";
            public string Marca { get; set; }
            public string Category { get; set; }
            public string NombreProducto { get; set; }
            public decimal? IVAPct { get; set; }
            public decimal? Pvp { get; set; }
            public int? Stock { get; set; }
            public string IdPromofarma { get; set; }
            public string Categoria { get; set; } = "Bebes y Mamás";
            public string Categoria2 { get; set; }
            public string Categoria3 { get; set; }
            public string Formato { get; set; }
            public decimal? Peso { get; set; }
            public string UrlImagen { get; set; }
            public string Descripion { get; set; }

            public bool Enabled { get; set; } = true;

            public Item() : base() { }
            public Item(Guid guid) : base(guid) { }

        }

        public DTOCsv Csv()
        {
            var retval = new DTOCsv();
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            var enabledItems = Items.Where(x => x.Enabled == true).ToList();

            var oRow = retval.AddRow();
            oRow.AddCell("EAN");
            oRow.AddCell("CN");
            oRow.AddCell("LABORATORIO");
            oRow.AddCell("MARCA");
            oRow.AddCell("NOMBRE PRODUCTO");
            oRow.AddCell("IVA");
            oRow.AddCell("PRECIO");
            oRow.AddCell("PVPR");
            oRow.AddCell("STOCK");
            oRow.AddCell("ID PROMOFARMA");
            oRow.AddCell("CATEGORÍA");
            oRow.AddCell("CVATEGORÍA2");
            oRow.AddCell("CATEGORÍA3");
            oRow.AddCell("FORMATO");
            oRow.AddCell("PESO");
            oRow.AddCell("URL IMAGEN");
            oRow.AddCell("DESCRIPCIÓN");

            foreach (var item in enabledItems)
            {
                oRow = retval.AddRow();
                oRow.AddCell(item.EAN13 ?? String.Empty);
                oRow.AddCell(item.CN ?? String.Empty);
                oRow.AddCell(item.Laboratorio);
                oRow.AddCell(item.Marca ?? String.Empty);
                oRow.AddCell(item.NombreProducto ?? String.Empty);
                oRow.AddCell(item.IVAPct?.ToString("N0",culture) ?? String.Empty);
                oRow.AddCell(item.Pvp?.ToString(culture) ?? String.Empty); //precio
                oRow.AddCell(item.Pvp?.ToString(culture) ?? String.Empty); //pvpr
                oRow.AddCell(item.Stock?.ToString() ?? String.Empty);
                oRow.AddCell(item.IdPromofarma ?? String.Empty);
                oRow.AddCell(item.Categoria ?? String.Empty);
                oRow.AddCell(item.Categoria2 ?? String.Empty);
                oRow.AddCell(item.Categoria3 ?? String.Empty);
                oRow.AddCell(item.Formato ?? String.Empty);
                oRow.AddCell(item.Peso?.ToString(culture) ?? String.Empty);
                oRow.AddCell(item.UrlImagen ?? String.Empty);
                oRow.AddCell(item.Descripion?.Trim() ?? String.Empty);
            }
            return retval;
        }
    }
}
