using System;
using System.Collections.Generic;

namespace EdiHelperStd
{
    public class Order : EdiFile
    {
        public DateTime? FchDoc { get; set; }
        public DateTime? FchDeliveryMin { get; set; }
        public DateTime? FchDeliveryMax { get; set; }

        public string Docnum { get; set; }
        public string Tipo { get; set; }
        public string Obs { get; set; }
        public string Cur { get; set; }

        public GLN NADMS { get; set; }
        public GLN ProveedorEAN { get; set; }
        public GLN CompradorEAN { get; set; }
        public GLN FacturarAEAN { get; set; }
        public GLN ReceptorMercanciaEAN { get; set; }

        public string Departamento { get; set; }
        public string Centro { get; set; }

        public List<Item> Items { get; set; }

        public Order() : base()
        {
        }

        public static Order Factory(List<Exception> exs, string src)
        {
            Order retval = new Order();
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
                    case Segment.Tags.DTM:
                        {
                            this.FchDoc = base.FieldDate(exs, segment, 1);
                            this.FchDeliveryMin = base.FieldDate(exs, segment, 2);
                            this.FchDeliveryMax = base.FieldDate(exs, segment, 3);

                            break;
                        }
                    case Segment.Tags.ORD:
                        this.Docnum = segment.Fields[1];
                        this.Tipo = segment.Fields[2];
                        break;

                    case Segment.Tags.FTX:
                        if (!string.IsNullOrEmpty(this.Obs))
                            this.Obs += Environment.NewLine;
                        this.Obs += segment.Fields[3];
                        break;

                    case Segment.Tags.NADMS: //message sender
                        this.NADMS = GLN.Factory(segment.Fields[1]);
                        break;

                    case Segment.Tags.NADSU: //proveidor
                        this.ProveedorEAN = GLN.Factory(segment.Fields[1]);
                        break;

                    case Segment.Tags.NADIV: //facturar a
                        this.FacturarAEAN = GLN.Factory(segment.Fields[1]);
                        break;

                    case Segment.Tags.NADBY: //buyer
                        this.CompradorEAN = GLN.Factory(segment.Fields[1]);
                        if (segment.Fields.Count > 2)
                            this.Departamento = segment.Fields[2];
                        if (segment.Fields.Count > 4)
                            this.Centro = segment.Fields[4];
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
                            exs.Add(new Exception("segment PIALIN.IN (ref.client) amb menys de 3 camps"));
                        this.Items.Add(oItem);
                        break;

                    case Segment.Tags.PIALIN:
                        switch (segment.Fields[1])
                        {
                            case "SA":
                                oItem.SupplierRef = base.FieldValue(exs, segment, 2);
                                break;
                            case "IN":
                                oItem.CustomerRef = base.FieldValue(exs, segment, 2);
                                break;
                        }
                        break;

                    case Segment.Tags.IMDLIN:
                        switch (segment.Fields[1])
                        {
                            case "F":
                                oItem.Description = base.FieldValue(exs, segment, 4);
                                break;
                        }
                        break;

                    case Segment.Tags.QTYLIN:
                        switch (segment.Fields[1])
                        {
                            case "21": //Unidades pedidas del artículo
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
            public decimal Discount { get; set; }
            public string SupplierRef { get; set; }
            public string CustomerRef { get; set; }
            public string Description { get; set; }
        }

    }
}
