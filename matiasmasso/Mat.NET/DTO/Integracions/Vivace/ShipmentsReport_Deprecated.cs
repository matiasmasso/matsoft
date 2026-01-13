using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Integracions.Vivace
{
    public class ShipmentsReport_Deprecated
    {
        public DateTime date { get; set; }
        public string sender { get; set; }

        public DTOJsonLog Log { get; set; }
        public List<Shipment> shipments { get; set; }

        public static ShipmentsReport_Deprecated Factory(JObject jObject, DTOJsonLog log)
        {
            ShipmentsReport_Deprecated retval = jObject.ToObject<DTO.Integracions.Vivace.ShipmentsReport_Deprecated>();
            retval.Log = log;
            return retval;
        }



        public class Shipment
        {
            public string expedition { get; set; }
            public string delivery { get; set; }

            public List<Item> items { get; set; }

            public int DeliveryYear()
            {
                return DTODelivery.YearFromFormattedId(delivery);
            }

            public int DeliveryId()
            {
                return DTODelivery.IdFromFormattedId(delivery);
            }

            public DTODelivery.Pallet.Collection Pallets()
            {
                DTODelivery.Pallet.Collection retval = new DTODelivery.Pallet.Collection();
                retval.AddRange(this.items.GroupBy(x => x.pallet)
                    .Select(y => y.First())
                    .Where(p => p.pallet != "")
                    .Select(z => new DTODelivery.Pallet(0, z.pallet)));
                foreach (DTODelivery.Pallet pallet in retval)
                    pallet.Packages.AddRange(Packages(pallet.SSCC));
                return retval;
            }
            public DTODelivery.Package.Collection Packages(string pallet = "")
            {
                DTODelivery.Package.Collection retval = new DTODelivery.Package.Collection();
                retval.AddRange(this.items.Where(p => p.pallet == pallet)
                    .GroupBy(x => x.SSCC)
                    .Select(y => y.First())
                    .Select(z => new DTODelivery.Package(0, z.SSCC)));
                foreach (DTODelivery.Package package in retval)
                    package.Items.AddRange(this.items
                        .Where(x => x.SSCC == package.SSCC)
                        .SelectMany(y => new DTODelivery.Package.Item{ y.));

                return retval;
            }
            public bool Matches(DTODelivery delivery)
            {
                bool sameYear = delivery.Fch.Year == DeliveryYear();
                bool sameId = delivery.Id == this.DeliveryId();
                return sameYear && sameId;
            }

            public class Item
            {
                //public List<int> line { get; set; } //en realitat un array de numeros de linia en cas que un mateix article s'hagi demanat varies vegades en un albarà i vagin en la mateixa caixa
                public List<int> lines { get; set; } //en realitat un array de numeros de linia en cas que un mateix article s'hagi demanat varies vegades en un albarà i vagin en la mateixa caixa
                public int sku { get; set; }
                public int qty { get; set; }
                public string SSCC { get; set; }
                public string pallet { get; set; }
                public List<DTODeliveryItem> DeliveryItems { get; set; } //resultat de assignar els numeros de linia per extreure les quantitats en cas de multiples numeros de linia
            }

            public class Arc : DTODeliveryItem
            {
                public int QtyLeft;
                public int QtyInBox;

                public Arc(Guid oGuid) : base(oGuid) { }
            }
        }
    }
}




