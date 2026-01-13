using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOEdiversaDesadv : DTOBaseGuid
    {
        public string Bgm { get; set; } // num de document
        public DateTime FchDoc { get; set; }
        public DateTime FchShip { get; set; }
        public string Rff { get; set; }

        public DTOEan NadBy { get; set; }
        public DTOEan NadSu { get; set; }
        public DTOEan NadDp { get; set; }
        public DTOEan NadMr { get; set; } // todo: assign order message sender NadMs to this property

        public DTOProveidor Proveidor { get; set; }
        public DTOContact Entrega { get; set; }
        public DTOPurchaseOrder PurchaseOrder { get; set; }

        public List<Item> Items { get; set; }

        public List<DTOEdiversaException> Exceptions { get; set; }

        public DTOEdiversaDesadv(DTOEdiversaFile oEdiFile) : base(oEdiFile.Guid)
        {
            Items = new List<Item>();
            Exceptions = new List<DTOEdiversaException>();
        }

        public class Item
        {
            public DTOEdiversaDesadv Parent { get; set; }
            public int Lin { get; set; }
            public DTOEan Ean { get; set; }
            public string Ref { get; set; }
            public string Dsc { get; set; }
            public int Qty { get; set; }
            public DTOProductSku Sku { get; set; }
        }

        public static string GetFullDocNom(DTOEdiversaDesadv value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(value.Bgm))
            {
                sb.Append(value.Bgm + " ");
                if (value.FchDoc != default(DateTime))
                    sb.Append(" del ");
            }
            if (value.FchDoc != default(DateTime))
                sb.Append(value.FchDoc.ToShortDateString());
            string retval = sb.ToString();
            return retval;
        }

        public static string GetPdcText(DTOEdiversaDesadv value)
        {
            string retval = "";
            if (value.PurchaseOrder != null)
                retval = string.Format("{0} del {1:dd/MM/yy}: {2}", value.PurchaseOrder.Num, value.PurchaseOrder.Fch, value.PurchaseOrder.Concept);
            return retval;
        }

        public static string GetProveidorNom(DTOEdiversaDesadv value)
        {
            string retval = "";
            if (value.Proveidor != null)
                retval = value.Proveidor.FullNom;
            return retval;
        }

        public static string GetEntregaNom(DTOEdiversaDesadv value)
        {
            string retval = "";
            if (value.Entrega != null)
                retval = value.Entrega.FullNom;
            return retval;
        }


        public static string GetFchShip(DTOEdiversaDesadv value)
        {
            string retval = "";
            if (value.FchShip != default(DateTime))
                retval = value.FchDoc.ToShortDateString();
            return retval;
        }
    }
}
