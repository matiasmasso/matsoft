using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOStat2
    {
        public List<Item> Items { get; set; }
        public DTOCatalog Catalog { get; set; }
        public List<DTOGuidNom.Compact> Customers { get; set; }

        public enum Units
        {
            Eur,
            Units
        }

        public DTOStat2()
        {
            Items = new List<Item>();
            Catalog = new DTOCatalog();
            Customers = new List<DTOGuidNom.Compact>();
        }
        public class Item
        {
            public int Month { get; set; }
            public Guid Customer { get; set; }
            public Guid Sku { get; set; }
            public int Qty { get; set; }
            public Decimal Eur { get; set; }
        }
    }
}
