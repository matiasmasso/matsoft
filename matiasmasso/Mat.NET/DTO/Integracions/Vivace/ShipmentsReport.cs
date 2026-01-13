using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DTO.Integracions.Vivace
{
    public class ShipmentsReport
    {
        public DateTime date { get; set; }
        public string sender { get; set; }
        public List<Shipment> shipments { get; set; }
        public DTOJsonLog Log { get; set; }


        public static ShipmentsReport Factory(JObject jObject, DTOJsonLog log)
        {
            ShipmentsReport retval = jObject.ToObject<DTO.Integracions.Vivace.ShipmentsReport>();
            retval.Log = log;
            return retval;
        }

        public class Shipment
        {
            public string expedition { get; set; }
            public string delivery { get; set; }
            public int packages { get; set; }
            public List<Item> items { get; set; }

            public Shipment()
            {
                items = new List<Item>();
            }

        }


        public class Item
        {
            public string pallet { get; set; }
            public int package { get; set; } // nº de bulto
            public string SSCC { get; set; }
            public int sku { get; set; }
            public int qty { get; set; }
            public List<int> lines { get; set; } //un array de numeros de linia en cas que un mateix article s'hagi demanat varies vegades en un albarà i vagin en la mateixa caixa
            public List<LineQty> lineQties { get; set; } //un array de numeros de linia en cas que un mateix article s'hagi demanat varies vegades en un albarà i vagin en la mateixa caixa

            public Item()
            {
                lines = new List<int>();
                lineQties = new List<LineQty>();
            }

            public override string ToString()
            {
                string l = string.Join(", ",lines.ToArray());
                string retval = string.Format("package {0} sku {1} qty {2} lines:{3}", package, sku, qty, l);
                return retval;
            }
        }

        public class LineQty
        {
            public int line { get; set; }
            public int qty { get; set; }
        }
        public class AlbLineQty : LineQty
        {
            public string Delivery { get; set; }
            public override string ToString()
            {
                return string.Format("delivery:{0} line:{1} qty:{2}", Delivery, base.line, base.qty);
            }
        }

    }
}




