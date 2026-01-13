using System;
using System.Collections.Generic;
using System.Linq;

namespace EdiHelperStd
{
    public class EdiFile
    {
        public string Src { get; set; }
        public Schemas Schema { get; set; }

        public Formats Format { get; set; }
        public List<Segment> Segments { get; set; }


        public List<Exception> Exs { get; set; }

        public enum Schemas
        {
            Unknown,
            ORDERS,
            ORDRSP,
            DESADV,
            INVOIC,
            GENRAL
        }

        public enum Formats
        {
            Unknown,
            Native,
            Ediversa
        }

        public EdiFile()
        {
            Exs = new List<Exception>();
        }

        public EdiFile(string src)
        {
            Exs = new List<Exception>();
            Load(src);
        }

        public static EdiFile FromFile(List<Exception> exs, string filePath, Schemas schema = Schemas.Unknown)
        {
            EdiFile retval = null;
            try
            {
                System.IO.FileInfo oFile = new System.IO.FileInfo(filePath);
                string src = System.IO.File.ReadAllText(oFile.FullName);
                retval = new EdiFile(src);
                switch (retval.Schema)
                {
                    case Schemas.ORDERS:
                        retval = new Genral(src);
                        break;
                    case Schemas.ORDRSP:
                        retval = new Genral(src);
                        break;
                    case Schemas.DESADV:
                        retval = new Genral(src);
                        break;
                    case Schemas.INVOIC:
                        retval = new Genral(src);
                        break;
                    case Schemas.GENRAL:
                        retval = new Genral(src);
                        break;
                    default:
                        retval = new EdiFile(src);
                        break;
                }
            }
            catch (Exception ex)
            {
                exs.Add(ex);
            }
            return retval;
        }


        public void Load(string src)
        {
            this.Src = src;
            this.Format = ReadFormat(src);
            this.Segments = ReadSegments(src, this.Format);
            this.Schema = ReadSchema(Exs, src);
        }

        private static List<Segment> ReadSegments(string src, Formats format)
        {
            List<Segment> retval = new List<Segment>();
            switch (format)
            {
                case Formats.Native:
                    string cleanSrc = src.Replace(System.Environment.NewLine, "");
                    List<string> lines = cleanSrc.Split(new string[] { "'" }, StringSplitOptions.None).ToList();
                    retval = lines.Select(x => Segment.Factory(x, format)).ToList();
                    break;
                case Formats.Ediversa:
                    retval = src.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(x => Segment.Factory(x, format)).ToList();
                    break;
            }
            return retval;
        }

        private static Formats ReadFormat(string src)
        {
            Formats retval = Formats.Unknown;
            if (src.StartsWith("UNH+"))
                retval = Formats.Native;
            else
                retval = Formats.Ediversa;
            return retval;
        }

        public static Schemas ReadSchema(List<Exception> exs, string src)
        {
            Schemas retval = Schemas.Unknown;
            try
            {
                switch (ReadFormat(src))
                {
                    case Formats.Native:
                        int plusSignPos = src.IndexOf("+", 4); //get first Plus sign after UNH+
                        int semicolonPos = src.IndexOf(":", plusSignPos); //get first semicolon after plusSignPos
                        string value = src.Substring(plusSignPos + 1, semicolonPos - plusSignPos - 1).ToUpper();
                        retval = (Schemas)Enum.Parse(typeof(Schemas), value);
                        break;
                    case Formats.Ediversa:
                        string tag = src.Substring(0, src.IndexOf("_"));
                        retval = (Schemas)Enum.Parse(typeof(Schemas), tag);
                        break;
                    default:
                        retval = Schemas.Unknown;
                        break;
                }
            }
            catch (Exception ex)
            {
                exs.Add(ex);
                retval = Schemas.Unknown;
            }
            return retval;
        }

        public string FieldValue(List<Exception> exs, Segment segment, int fieldIdx, int componentIdx = -1)
        {
            string retval = "";

            if (fieldIdx < segment.Fields.Count)
            {
                string dataElement = segment.Fields[fieldIdx];
                if (componentIdx == -1)
                    retval = dataElement;
                else
                {
                    List<string> components = Components(exs, segment, fieldIdx);
                    if (componentIdx < components.Count)
                        retval = components[componentIdx];
                    else
                        exs.Add(new Exception(string.Format("Line {0}: Missing component {1} on field {2} in segment {3}", this.Segments.IndexOf(segment), componentIdx, fieldIdx, segment.Tag.ToString())));
                }
            }
            else
                exs.Add(new Exception(string.Format("Line {0}: Missing field {1} in segment {2}", this.Segments.IndexOf(segment), fieldIdx, segment.Tag.ToString())));

            return retval;
        }

        public List<string> Components(List<Exception> exs, Segment segment, int fieldIdx)
        {
            string dataElement = FieldValue(exs, segment, fieldIdx);
            List<string> retval = dataElement.Split(new string[] { ":" }, StringSplitOptions.None).ToList();
            return retval;
        }

        public DateTime? FieldDate(List<Exception> exs, Segment segment, int fieldIdx, int componentIdx = -1)
        {
            DateTime? retval = null;
            string value = FieldValue(exs, segment, fieldIdx, componentIdx);
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    if (value.Length == 8)
                    {
                        int year = Int32.Parse(value.Substring(0, 4));
                        int month = Int32.Parse(value.Substring(4, 2));
                        int day = Int32.Parse(value.Substring(6, 2));
                        retval = new DateTime(year, month, day);
                    }
                    else
                        exs.Add(new Exception(string.Format("Line {0}: Could not parse Date '{1}' from field {2} in segment {3}", this.Segments.IndexOf(segment), value, fieldIdx, segment.Tag.ToString())));
                }
                catch (Exception)
                {
                    exs.Add(new Exception(string.Format("Line {0}: Could not parse Date '{1}' from field {2} in segment {3}", this.Segments.IndexOf(segment), value, fieldIdx, segment.Tag.ToString())));
                }
            }
            return retval;
        }

        public int FieldInt(List<Exception> exs, Segment segment, int fieldIdx, int componentIdx = -1)
        {
            int retval = 0;
            string value = FieldValue(exs, segment, fieldIdx, componentIdx);
            if (!Int32.TryParse(value, out retval))
            {
                exs.Add(new Exception(string.Format("Line {0}: Could not parse Integer '{1}' from field {2} in segment {3}", this.Segments.IndexOf(segment), value, fieldIdx, segment.Tag.ToString())));
            }
            return retval;
        }

        public Decimal FieldDecimal(List<Exception> exs, Segment segment, int fieldIdx, int componentIdx = -1)
        {
            Decimal retval = 0;
            string value = FieldValue(exs, segment, fieldIdx, componentIdx);
            double decimalIdx = value.IndexOf(".");
            double decimals = (decimalIdx > 0) ? value.Length - decimalIdx - 1 : 0;
            double factor = Math.Pow(10, decimals);
            value = value.Replace(".", "");
            if (Decimal.TryParse(value, out retval))
            {
                retval = retval / (decimal)factor;
            }
            else
            {
                exs.Add(new Exception(string.Format("Line {0}: Could not parse Decimal '{1}' from field {2} in segment {3}", this.Segments.IndexOf(segment), value, fieldIdx, segment.Tag.ToString())));
            }
            return retval;
        }


        public Decimal FieldEur(List<Exception> exs, Segment segment, int fieldIdx, int componentIdx = -1)
        {
            Decimal retval = FieldDecimal(exs, segment, fieldIdx, componentIdx);
            return retval;
        }


        public class Segment
        {
            public Tags Tag { get; set; }
            public string Src { get; set; }
            public List<string> Fields { get; set; }

            public enum Tags
            {
                Unknown,
                BGM,
                ORD,
                NAD,
                NADMS,
                NADMR,
                NADSU,
                NADIV,
                NADBY,
                NADDP,
                CNT,
                CPS,
                CUX,
                DTM,
                FTX,
                GIN,
                IMD,
                INV,
                LIN,
                PIALIN,
                IMDLIN,
                QTYLIN,
                PRILIN,
                ALCLIN,
                MOALIN,
                RFFLIN,
                MOARES,
                PAC,
                PCI,
                QTY,
                RFF,
                UNH,
                UNT
            }

            public Segment()
            {
                this.Fields = new List<string>();
            }

            public static Segment Factory(string src, EdiFile.Formats format)
            {
                Segment retval = new Segment();
                retval.Src = src;
                retval.Tag = retval.ReadTag(format);

                switch (format)
                {
                    case Formats.Native:
                        retval.Fields = retval.ReadNativeFields();
                        break;
                    case Formats.Ediversa:
                        retval.Fields = retval.ReadEdiversaFields();
                        break;
                }
                return retval;

            }

            public string StringFieldValue(int idx)
            {
                string retval = "";
                if (Fields.Count > idx)
                {
                    retval = Fields[idx];
                }
                return retval;
            }
            public DateTime? DateFieldValue(int idx, List<Exception> exs)
            {
                DateTime? retval = null;
                string src = StringFieldValue(idx);
                if (!string.IsNullOrEmpty(src))
                {
                    string cleanDigits = EdiFile.CleanDigits(src);
                    if (cleanDigits.Length >= 8)
                    {
                        int year = int.Parse(cleanDigits.Substring(0, 4));
                        int month = int.Parse(cleanDigits.Substring(4, 2));
                        int day = int.Parse(cleanDigits.Substring(6, 2));
                        retval = new DateTime(year, month, day);
                    }
                    else
                        exs.Add(new Exception(string.Format("wrong date format '{0}' on field {1} of segment {2}", src, idx, StringFieldValue(0))));
                }
                return retval;
            }

            public Tags ReadTag(EdiFile.Formats format)
            {
                Tags retval;

                try
                {
                    string value = Src;
                    if (format == Formats.Native)
                        value = Src.Substring(0, 3);
                    else if (format == Formats.Ediversa && Src.IndexOf("|") > 0)
                        value = Src.Substring(0, Src.IndexOf("|"));
                    retval = (Tags)Enum.Parse(typeof(Tags), value);
                }
                catch (Exception)
                {
                    retval = Tags.Unknown;
                }

                return retval;
            }

            public List<string> ReadNativeFields()
            {
                List<string> retval = Src.Split(new string[] { "+" }, StringSplitOptions.None).ToList();
                return retval;
            }

            public List<string> ReadEdiversaFields()
            {
                List<string> retval = Src.Split(new string[] { "|" }, StringSplitOptions.None).ToList();
                return retval;
            }


        }

        public static string CleanDigits(string src)
        {
            string retval = new string(src.Where(c => char.IsDigit(c)).ToArray());
            return retval;
        }
    }


}
