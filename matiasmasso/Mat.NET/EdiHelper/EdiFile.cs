using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace EdiHelper
{
    public class EdiFile
    {
        private string Src { get; set; }
        public Schemas Schema { get; set; }

        public Formats Format { get; set; }
        public List<Segment> Segments { get; set; }

        public enum Schemas {
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


        public void Load(string src)
        {
            this.Src = src;
            this.Format = ReadFormat(src);
            this.Segments = ReadSegments(src, this.Format);
        }

        private static List<Segment> ReadSegments(string src, Formats format)
        {
            List<Segment> retval = new List<Segment>();
            switch (format)
            {
                case Formats.Native:
                    string cleanSrc = src.Replace(System.Environment.NewLine, "");
                    List<string> lines = cleanSrc.Split(new string[] { "'" }, StringSplitOptions.None).ToList();
                    retval = lines.Select(x =>  Segment.Factory(x, format)).ToList();
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
                        string value = src.Substring(plusSignPos + 1, semicolonPos - plusSignPos-1).ToUpper();
                        retval = (Schemas)Enum.Parse(typeof(Schemas), value);
                        break;
                    case Formats.Ediversa:
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

            if (fieldIdx < segment.Fields.Count) {
                string dataElement = segment.Fields[fieldIdx];
                if (componentIdx == -1)
                    retval = dataElement;
                else
                {
                    List<string> components = Components(exs, segment, fieldIdx);
                    if (componentIdx < components.Count)
                        retval = components[componentIdx];
                    else
                        exs.Add(new Exception(string.Format("Line {0}: Missing component {1} on field {2} in segment {3}",  this.Segments.IndexOf(segment), componentIdx, fieldIdx, segment.Tag.ToString())));
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


        public class Segment
        {
            public Tags Tag { get; set; }
            public string Src { get; set; }
            public List<string> Fields { get; set; }

            public enum Tags
            {
                Unknown,
                BGM,
                CNT,
                CPS,
                CUX,
                DTM,
                GIN,
                IMD,
                LIN,
                NAD,
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
                retval.Tag = retval.ReadTag();

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

            public Tags ReadTag()
            {
                Tags retval;

                try
                {
                    string value = Src.Substring(0,3);
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

    }
}
