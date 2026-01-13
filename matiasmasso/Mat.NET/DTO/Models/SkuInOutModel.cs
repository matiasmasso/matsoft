using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class SkuInOutModel
    {
        public List<DTOGuidNom.Compact> Mgzs { get; set; }
        public List<Item> Items { get; set; }

        public SkuInOutModel()
        {
            Mgzs = new List<DTOGuidNom.Compact>();
            Items = new List<Item>();
        }

        public class Item
        {
            public Guid Guid { get; set; }
            public int TransmId { get; set; }
            public int DeliveryId { get; set; }
            public Guid DeliveryGuid { get; set; }
            public DateTime Fch { get; set; }

            public string Nom { get; set; }
            public Guid ContactGuid { get; set; }
            public int Qty { get; set; }
            public DTODeliveryItem.Cods Cod { get; set; }
            public Decimal Eur { get; set; }
            public Decimal Dto { get; set; }
            public Decimal Dt2 { get; set; }

            public decimal Pmc { get; set; }
            public Guid Mgz { get; set; }
        }
    }
}
