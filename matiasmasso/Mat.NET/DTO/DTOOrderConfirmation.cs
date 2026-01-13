using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOOrderConfirmation
    {
        public DTOEdiversaInterlocutor NADMS { get; set; } // message sender (us)
        public DTOEdiversaInterlocutor NADMR { get; set; } // message receiver (same as order message sender NadMr)
        public DTOEdiversaInterlocutor Supplier { get; set; } // NADSU
        public DTOEdiversaInterlocutor Buyer { get; set; } // NADBY
        public DTOEdiversaInterlocutor DeliveryPlatform { get; set; } // NADDP
        public DTOEdiversaInterlocutor InvoiceTo { get; set; } // NADDIV
        public DTOCur Currency { get; set; }
        public DTOPurchaseOrder Order { get; set; }
        public DateTime Fch { get; set; }
        public string Num { get; set; }
        public string OrderNumber { get; set; }
        public List<Item> Items { get; set; }
        public List<Exception> Exceptions { get; set; }

        public DTOOrderConfirmation() : base()
        {
            Items = new List<Item>();
            Exceptions = new List<Exception>();
        }

        public static DTOOrderConfirmation Factory(string sFilename, List<Exception> exs)
        {
            DTOOrderConfirmation retval = null;
            List<string> segments = TextHelper.ReadFileToStringList(sFilename, exs);
            if (segments != null)
                retval = Factory(segments);
            return retval;
        }

        public static DTOOrderConfirmation Factory(List<string> segments)
        {
            List<DTOEdiversaException> exs = new List<DTOEdiversaException>();
            DTOOrderConfirmation retval = new DTOOrderConfirmation();
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;

            DTOOrderConfirmation.Item item = null;
            foreach (string segment in segments)
            {
                List<string> fields = segment.Split('|').ToList();
                switch (fields.First())
                {
                    case "BGM":
                        {
                            retval.Num = fields[1];
                            break;
                        }

                    case "DTM":
                        {
                            retval.Fch = DateTime.ParseExact(fields[1], "yyyyMMdd", provider);
                            break;
                        }

                    case "RFF":
                        {
                            if (fields.Count > 2 && fields[1] == "ON")
                                retval.OrderNumber = fields[2];
                            break;
                        }

                    case "NADSU":
                        {
                            retval.Supplier = new DTOEdiversaInterlocutor(segment);
                            break;
                        }

                    case "NADBY":
                        {
                            retval.Buyer = new DTOEdiversaInterlocutor(segment);
                            break;
                        }

                    case "NADDP":
                        {
                            retval.DeliveryPlatform = new DTOEdiversaInterlocutor(segment);
                            break;
                        }

                    case "NADIV":
                        {
                            retval.InvoiceTo = new DTOEdiversaInterlocutor(segment);
                            break;
                        }

                    case "CUX":
                        {
                            retval.Currency = DTOCur.Factory(fields[1]);
                            break;
                        }

                    case "LIN":
                        {
                            item = new DTOOrderConfirmation.Item(segment);
                            break;
                        }

                    case "PIALIN":
                        {
                            if (fields.Count > 2 && fields[1] == "SA" && item != null)
                                item.SupplierRef = fields[2];
                            break;
                        }

                    case "IMDLIN":
                        {
                            if (fields.Count > 3 && item != null)
                                item.Description = fields[3];
                            break;
                        }

                    case "QTYLIN":
                        {
                            if (fields.Count > 2 && item != null)
                                item.Qty = fields[2].toInteger();
                            break;
                        }

                    case "DTMLIN":
                        {
                            item.DeliveryTime = DateTime.ParseExact(fields[1], "yyyyMMdd", provider);
                            break;
                        }

                    case "PRILIN":
                        {
                            if (fields.Count > 2 && item != null)
                                item.Price = DTOEdiversaFile.ParseAmt(fields[2], exs).Eur;
                            break;
                        }
                }
            }
            return retval;
        }
        public class Item
        {
            public string Ean { get; set; }
            public int Lin { get; set; }
            public string SupplierRef { get; set; }
            public string Description { get; set; }
            public DTOProductSku Sku { get; set; }
            public int Qty { get; set; }
            public decimal Price { get; set; }
            public decimal Dto { get; set; }
            public DateTime DeliveryTime { get; set; }

            public Item(string segment) : base()
            {
                List<string> fields = segment.Split('|').ToList();
                if (fields.Count > 1)
                {
                    Ean = fields[1];
                    if (fields.Count > 3)
                        Lin = fields[3].toInteger();
                }
            }
        }
    }
}
