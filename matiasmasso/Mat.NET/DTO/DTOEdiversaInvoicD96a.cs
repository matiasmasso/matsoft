using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOEdiversaInvoicD96a : DTOBaseGuid
    {
        public string Inv { get; set; }
        public DateTime Dtm { get; set; }
        public string RffDq { get; set; }
        public string RffOn { get; set; }
        public string RffVn { get; set; }
        public string NadSu { get; set; }
        public string NadBy { get; set; }
        public string NadIv { get; set; }
        public string NadDp { get; set; }
        public string Cur { get; set; }
        public decimal MoaresNet { get; set; }
        public decimal MoaresBrut { get; set; }
        public decimal MoaresBase { get; set; }
        public decimal MoaresLiq { get; set; }
        public decimal MoaresTax { get; set; }
        public decimal MoaresDto { get; set; }
        public decimal MoaresCharges { get; set; }
        public List<Item> Items { get; set; }

        public class Item : DTOBaseGuid
        {
            public int Lin { get; set; }
            public string Ean { get; set; }
            public string PiaLin { get; set; }
            public string ImdLin { get; set; }
            public int QtyLin { get; set; }
            public decimal PriLinNet { get; set; }
            public decimal PriLinBrut { get; set; }
            public decimal MoaLinNet { get; set; }

            public Item() : base()
            {
            }

            public Item(Guid oGuid) : base(oGuid)
            {
            }
        }

        public DTOEdiversaInvoicD96a() : base()
        {
            Items = new List<Item>();
        }

        public DTOEdiversaInvoicD96a(Guid oGuid) : base(oGuid)
        {
            Items = new List<Item>();
        }

        public static DTOEdiversaInvoicD96a Factory(string src)
        {
            var segments = src.Split(new String[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            DTOEdiversaInvoicD96a retval = new DTOEdiversaInvoicD96a();
            foreach (var segment in segments)
            {
                var fields = segment.Split('|');
                Item item = new Item();
                if (fields.Length > 1)
                {
                    switch (fields.First())
                    {
                        case "INV":
                            {
                                retval.Inv = Field(segment, 1);
                                break;
                            }

                        case "DTM":
                            {
                                retval.Dtm = ParseFch(segment, 1);
                                break;
                            }

                        case "RFF":
                            {
                                switch (Field(segment, 1))
                                {
                                    case "DQ":
                                        {
                                            retval.RffDq = Field(segment, 2);
                                            break;
                                        }

                                    case "ON":
                                        {
                                            retval.RffOn = Field(segment, 2);
                                            break;
                                        }

                                    case "VN":
                                        {
                                            retval.RffVn = Field(segment, 2);
                                            break;
                                        }
                                }

                                break;
                            }

                        case "NADSU":
                            {
                                retval.NadSu = Field(segment, 2);
                                break;
                            }

                        case "NADBY":
                            {
                                retval.NadBy = Field(segment, 2);
                                break;
                            }

                        case "NADIV":
                            {
                                retval.NadIv = Field(segment, 2);
                                break;
                            }

                        case "NADDP":
                            {
                                retval.NadDp = Field(segment, 2);
                                break;
                            }

                        case "CUX":
                            {
                                retval.Cur = Field(segment, 2);
                                break;
                            }

                        case "ALC":
                            {
                                break;
                            }

                        case "MOARES":
                            {
                                retval.MoaresNet = parseDecimal(segment, 1);
                                retval.MoaresBrut = parseDecimal(segment, 2);
                                retval.MoaresBase = parseDecimal(segment, 3);
                                retval.MoaresLiq = parseDecimal(segment, 4, 10);
                                retval.MoaresTax = parseDecimal(segment, 5);
                                retval.MoaresDto = parseDecimal(segment, 6, 13);
                                retval.MoaresCharges = parseDecimal(segment, 9);
                                break;
                            }

                        case "LIN":
                            {
                                item = new Item();
                                item.Ean = Field(segment, 1);
                                item.Lin = parseInt(segment, 3);
                                break;
                            }

                        case "PIALIN":
                            {
                                item.PiaLin = Field(segment, 1);
                                break;
                            }

                        case "IMDLIN":
                            {
                                item.ImdLin = Field(segment, 1);
                                break;
                            }

                        case "QTYLIN":
                            {
                                item.QtyLin = parseInt(segment, 2);
                                break;
                            }

                        case "MOALIN":
                            {
                                item.MoaLinNet = parseDecimal(segment, 3);
                                break;
                            }

                        case "PRILIN":
                            {
                                switch (Field(segment, 2))
                                {
                                    case "AAA":
                                        {
                                            item.PriLinNet = parseDecimal(segment, 2);
                                            break;
                                        }

                                    case "AAB":
                                        {
                                            item.PriLinBrut = parseDecimal(segment, 2);
                                            break;
                                        }
                                }

                                break;
                            }
                    }
                }
            }
            return retval;
        }

        public static int parseInt(string segment, int fieldIdx, int alternativeIdx = 0)
        {
            int retval = 0;
            string value = Field(segment, fieldIdx, alternativeIdx);
            if (TextHelper.VbIsNumeric(value))
                retval = value.toInteger();
            return retval;
        }

        public static decimal parseDecimal(string segment, int fieldIdx, int alternativeIdx = 0)
        {
            decimal retval = 0;
            string value = Field(segment, fieldIdx, alternativeIdx);
            try
            {
                retval = decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
            }
            return retval;
        }

        public static DateTime ParseFch(string segment, int fieldIdx, int alternativeIdx = 0)
        {
            DateTime retval = default(DateTime);
            string value = Field(segment, fieldIdx, alternativeIdx);
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            try
            {
                switch (value.Length)
                {
                    case 0:
                        {
                            break;
                        }

                    case 6:
                        {
                            retval = DateTime.ParseExact(value, "yyMMdd", provider);
                            break;
                        }

                    case 8:
                        {
                            retval = DateTime.ParseExact(value, "yyyyMMdd", provider);
                            break;
                        }

                    case 12:
                        {
                            retval = DateTime.ParseExact(value, "yyyyMMddHHmm", provider);
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception)
            {
            }

            return retval;
        }

        public static string Field(string segment, int fieldIdx, int alternativeIdx = 0)
        {
            string retval = "";
            var fields = segment.Split('|').ToList();
            if (fields.Count > fieldIdx)
                retval = fields[fieldIdx];
            if (retval == "" & alternativeIdx > 0)
                retval = Field(segment, alternativeIdx);
            return retval;
        }
    }
}
