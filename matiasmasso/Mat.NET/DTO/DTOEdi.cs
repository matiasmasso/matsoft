using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class Edi
    {
        protected string Src { get; set; }
        protected List<Segment> Segments { get; set; }
        public Tags Tag { get; set; }
        public enum Tags
        {
            Orders,
            Ordrsp,
            Desadv,
            Invoic,
            Genral
        }

        public Edi(string src, Tags tag)
        {
            this.Src = src;
            this.Tag = tag;
            List<string> lines = TextHelper.GetArrayListFromSplitCharSeparatedString(src, "'").Where(x => x.isNotEmpty()).ToList();
            this.Segments = lines.Select(x => new Segment(x)).ToList();
        }


        public static Edi Factory(List<Exception> exs, string src)
        {
            Edi retval = null;
            retval.Src = src;
            if (MatHelperStd.TextHelper.RegexMatch(src, "ORDERS: D:96A"))
                retval = Edi.Order.Factory(exs, src);
            return retval;
        }


        public class Segment
        {
            string Src { get; set; }
            public string Tag { get; set; }
            public List<string> Fields { get; set; }

            public Segment(string src)
            {
                Src = src;
                Fields = src.Split(new char[] { '+', ':' }).ToList();
                Tag = Fields.First();
            }

        }


        public class Order : Edi
        {
            string Buyer { get; set; }
            string Supplier { get; set; }
            string InvoiceTo { get; set; }
            string DeliverTo { get; set; }
            string Docnum { get; set; }
            DateTime FchDoc { get; set; }
            DateTime FchDeliveryMin { get; set; }
            DateTime FchDeliveryMax { get; set; }
            String CustomerSupplierNumber { get; set; }
            String Nif { get; set; }
            String Currency { get; set; }

            public Order(string src) : base(src, Tags.Orders)
            {
            }

            public static new Edi.Order Factory(List<Exception> exs, string src)
            {
                Edi.Order retval = null;
                try
                {
                    retval = new Edi.Order(src);
                    foreach (Segment segment in retval.Segments)
                    {
                        switch (segment.Tag)
                        {
                            case "BGM":
                                retval.Docnum = segment.Fields[2];
                                break;
                            case "DTM":
                                switch (segment.Fields[1])
                                {
                                    case "137":
                                        retval.FchDoc = DateTime.Parse(segment.Fields[2]);
                                        break;
                                    case "63":
                                        retval.FchDeliveryMax = DateTime.Parse(segment.Fields[2]);
                                        break;
                                    case "64":
                                        retval.FchDeliveryMin = DateTime.Parse(segment.Fields[2]);
                                        break;
                                }
                                break;
                            case "RFF":
                                switch (segment.Fields[1])
                                {
                                    case "CR":
                                        retval.CustomerSupplierNumber = segment.Fields[2];
                                        break;
                                    case "VA":
                                        retval.Nif = segment.Fields[2];
                                        break;
                                }
                                break;
                            case "NAD":
                                switch (segment.Fields[1])
                                {
                                    case "BY":
                                        retval.Buyer = segment.Fields[2];
                                        break;
                                    case "SU":
                                        retval.Supplier = segment.Fields[2];
                                        break;
                                    case "DP":
                                        retval.DeliverTo = segment.Fields[2];
                                        break;
                                    case "IV":
                                        retval.InvoiceTo = segment.Fields[2];
                                        break;
                                }
                                break;
                            case "CUX":
                                retval.Currency = segment.Fields[2];
                                break;
                            case "LIN":
                                break;
                            case "QTY":
                                break;
                            case "PRI":
                                break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    exs.Add(ex);
                }
                return retval;
            }

        }
    }
}
