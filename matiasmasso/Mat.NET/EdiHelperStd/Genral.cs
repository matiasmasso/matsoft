using System;
using System.Collections.Generic;

namespace EdiHelperStd
{
    public class Genral : EdiFile
    {
        public string Docnum { get; set; }
        public DateTime? Fch { get; set; }
        public GLN Sender { get; set; }
        public GLN Receiver { get; set; }
        public string Text { get; set; }

        public Genral(string src) : base(src)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Segment segment in base.Segments)
            {
                switch (segment.Tag)
                {
                    case Segment.Tags.BGM:
                        Docnum = segment.StringFieldValue(1);
                        break;
                    case Segment.Tags.DTM:
                        Fch = segment.DateFieldValue(1, Exs);
                        break;
                    case Segment.Tags.NAD:
                        if (segment.StringFieldValue(0) == "NADMS")
                            Sender = GLN.Factory(segment.StringFieldValue(1));
                        else if (segment.StringFieldValue(0) == "NADMR")
                            Receiver = GLN.Factory(segment.StringFieldValue(1));
                        break;
                    case Segment.Tags.FTX:
                        sb.AppendLine(segment.StringFieldValue(2));
                        break;
                }
            }
            Text = sb.ToString();
        }

        public static Genral FromFile(List<Exception> exs, String filePath)
        {
            Genral retval = (Genral)EdiFile.FromFile(exs, filePath, Schemas.GENRAL);
            return retval;
        }
    }
}
