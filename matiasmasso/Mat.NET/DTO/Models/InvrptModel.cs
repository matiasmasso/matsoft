using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class InvrptModel
    {
        public HashSet<DTOGuidNom.Compact> Customers { get; set; }
        public CatalogModel Catalog { get; set; }
        public HashSet<Item> Items { get; set; }
        public HashSet<DateTime> Fchs { get; set; }
        public enum Modes
        {
            Catalog,
            Centros
        }

        public InvrptModel()
        {
            Customers = new HashSet<DTOGuidNom.Compact>();
            Catalog = new CatalogModel();
            Items = new HashSet<Item>();
            Fchs = new HashSet<DateTime>();
        }

        public class Item
        {
            public int Qty { get; set; }
            public decimal Retail { get; set; }
            public string Ean { get; set; }
            public DateTime Fch { get; set; }
            public Guid CustomerGuid { get; set; }
            public Guid ProductGuid { get; set; }
        }
    }
}
