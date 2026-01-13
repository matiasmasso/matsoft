using System;
using System.Collections.Generic;

namespace EdiHelperStd
{
    public class Invoic : EdiFile
    {
        public GLN ProveedorEAN { get; set; }
        public GLN ReceptorMercanciaEAN { get; set; }
        public string Docnum { get; set; }
        public DateTime? Fch { get; set; }

        public string PurchaseOrderId { get; set; }
        public string OrderCondirmation { get; set; }
        public string DeliveryNote { get; set; }
        public string Cur { get; set; }
        public decimal TaxBase { get; set; }
        public decimal NetTotal { get; set; }

        public string PurchaseOrderOrDefault(Item item)
        {
            string retval = string.IsNullOrEmpty(item.PurchaseOrderId) ? PurchaseOrderId : item.PurchaseOrderId;
            return retval;
        }
        public string OrderCondirmationOrDefault(Item item)
        {
            string retval = string.IsNullOrEmpty(item.OrderCondirmation) ? OrderCondirmation : item.OrderCondirmation;
            return retval;
        }
        public string DeliveryNoteOrDefault(Item item)
        {
            string retval = string.IsNullOrEmpty(item.DeliveryNote) ? DeliveryNote : item.DeliveryNote;
            return retval;
        }

        public List<Item> Items { get; set; }

        public Invoic() : base()
        {
        }

        public static Invoic Factory(List<Exception> exs, string src)
        {
            Invoic retval = new Invoic();
            retval.Load(exs, src);
            return retval;
        }

        public void Load(List<Exception> exs, string src)
        {
            base.Load(src);
            switch (base.Format)
            {
                case Formats.Native:
                    this.LoadFromNative(exs);
                    break;
                case Formats.Ediversa:
                    this.LoadFromEdiversa(exs);
                    break;
            }
        }

        public bool LoadFromNative(List<Exception> exs)
        {
            return true;
        }

        public bool LoadFromEdiversa(List<Exception> exs)
        {
            this.Items = new List<Item>();
            Item oItem = null;

            foreach (EdiFile.Segment segment in base.Segments)
            {
                switch (segment.Tag)
                {
                    case Segment.Tags.INV:
                        this.Docnum = this.FieldValue(exs, segment, 1);
                        break;

                    case Segment.Tags.DTM:
                        this.Fch = base.FieldDate(exs, segment, 1);
                        break;

                    case Segment.Tags.RFF:
                        switch (this.FieldValue(exs, segment, 1))
                        {
                            case "DQ":
                                this.DeliveryNote = this.FieldValue(exs, segment, 2);
                                break;
                            case "ON":
                                this.PurchaseOrderId = this.FieldValue(exs, segment, 2);
                                break;
                            case "VN":
                                this.OrderCondirmation = this.FieldValue(exs, segment, 2);
                                break;
                        }
                        break;

                    case Segment.Tags.NADSU: //supplier
                        this.ProveedorEAN = GLN.Factory(segment.Fields[1]);
                        break;

                    case Segment.Tags.NADDP: //receptor  de la mercancia
                        this.ReceptorMercanciaEAN = GLN.Factory(segment.Fields[1]);
                        break;

                    case Segment.Tags.CUX:
                        if (segment.Fields.Count > 1)
                            this.Cur = segment.Fields[1];
                        break;

                    case Segment.Tags.LIN:
                        oItem = new Item();
                        if (segment.Fields.Count > 3)
                        {
                            oItem.Ean = GLN.Factory(segment.Fields[1]);
                            oItem.Lin = this.FieldInt(exs, segment, 3);
                        }
                        else
                            exs.Add(new Exception("segment LIN amb menys de 3 camps"));
                        this.Items.Add(oItem);
                        break;

                    case Segment.Tags.PIALIN:
                        if (segment.Fields.Count == 2)
                            oItem.SupplierRef = base.FieldValue(exs, segment, 1);
                        else
                        {
                            switch (segment.Fields[1])
                            {
                                case "SA":
                                    oItem.SupplierRef = base.FieldValue(exs, segment, 2);
                                    break;
                                case "IN":
                                    oItem.CustomerRef = base.FieldValue(exs, segment, 2);
                                    break;
                            }
                        }
                        break;

                    case Segment.Tags.IMDLIN:
                        switch (segment.Fields[1])
                        {
                            case "F":
                                oItem.Description = base.FieldValue(exs, segment, 4);
                                break;
                            default:
                                oItem.Description = base.FieldValue(exs, segment, 1);
                                break;
                        }
                        break;

                    case Segment.Tags.QTYLIN:
                        switch (segment.Fields[1])
                        {
                            case "47": //Unidades facturadas
                                oItem.Qty = base.FieldInt(exs, segment, 2);
                                break;
                        }
                        break;

                    case Segment.Tags.PRILIN:
                        switch (segment.Fields[1])
                        {
                            case "AAB": //Preu brut abans de descomptes
                                oItem.GrossPrice = base.FieldDecimal(exs, segment, 2);
                                break;
                            case "AAA": //Preu brut despres de descomptes pero abans de impostos (Amazon)
                                oItem.NetPrice = base.FieldDecimal(exs, segment, 2);
                                break;
                        }
                        break;

                    case Segment.Tags.ALCLIN:
                        switch (segment.Fields[1])
                        {
                            case "A": //Descomptes
                                oItem.Discount = base.FieldDecimal(exs, segment, 10);
                                break;
                        }
                        break;

                    case Segment.Tags.MOALIN:
                        switch (segment.Fields[1])
                        {
                            case "A": //Import net
                                oItem.Amount = base.FieldDecimal(exs, segment, 3);
                                break;
                            case "": //Import net
                                oItem.Amount = this.FieldEur(exs, segment, 3);
                                break;
                        }
                        break;

                    case Segment.Tags.RFFLIN:
                        switch (this.FieldValue(exs, segment, 1))
                        {
                            case "DQ":
                                this.DeliveryNote = this.FieldValue(exs, segment, 2);
                                break;
                            case "ON":
                                this.PurchaseOrderId = this.FieldValue(exs, segment, 2);
                                break;
                            case "VN":
                                this.OrderCondirmation = this.FieldValue(exs, segment, 2);
                                break;
                        }
                        break;

                    case Segment.Tags.MOARES:
                        this.NetTotal = this.FieldEur(exs, segment, 1);
                        this.TaxBase = this.FieldEur(exs, segment, 3);
                        break;

                    default:
                        //exs.Add(new Exception("segment " + segment.Src + " no reconegut"));
                        break;

                }
            }
            return exs.Count == 0;
        }

        public class Item
        {
            public int Lin { get; set; }
            public GLN Ean { get; set; }
            public int Qty { get; set; }
            public decimal NetPrice { get; set; }
            public decimal GrossPrice { get; set; }
            public decimal Amount { get; set; }
            public decimal Discount { get; set; }
            public string SupplierRef { get; set; }
            public string CustomerRef { get; set; }
            public string Description { get; set; }
            public string PurchaseOrderId { get; set; }
            public string OrderCondirmation { get; set; }
            public string DeliveryNote { get; set; }
        }

    }
}
