using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DTO
{
    public class DTOEdiversaFile : DTOBaseGuid
    {
        public string FileName { get; set; }
        public DTOBaseGuid Source { get; set; }
        public string Tag { get; set; }
        public DateTime Fch { get; set; }
        public DateTime FchCreated { get; set; }
        public string Docnum { get; set; }
        public DTOAmt Amount { get; set; }
        public DTOEdiversaContact Sender { get; set; }
        public DTOEdiversaContact Receiver { get; set; }
        public Results Result { get; set; }
        public DTOBaseGuid ResultBaseGuid { get; set; }
        public string Stream { get; set; }
        public List<DTOEdiversaException> Exceptions { get; set; }

        public IOcods IOCod { get; set; }
        public List<DTOEdiversaSegment> Segments { get; set; }

        public Interlocutors Interlocutor;

        public const string PrefixMatiasMasso = "84353165";
        public const string PrefixBritax = "4000984";
        public const string PrefixElCorteIngles = "8422416";
        public const string PrefixElCorteInglesPt = "5600000025";
        public const string PrefixSonae = "84365316";
        public const string PrefixAmazon = "5450534";
        public const string PrefixCarrefour = "8480015";
        public const string PrefixAlcampo = "XX";
        public const string PrefixToysRUs = "8421703";
        public const string PrefixMiFarma = "8437022";
        public const string PrefixContinente = "5600000022";

        public enum Interlocutors
        {
            matiasmasso,
            unknown,
            britax,
            ElCorteIngles,
            amazon,
            carrefour,
            alcampo,
            sonae,
            toysrus,
            mifarma,
            continente
        }

        public enum Results
        {
            pending,
            processed,
            deleted
        }

        public enum IOcods
        {
            notSet,
            inbox,
            outbox
        }

        public enum Tags
        {
            DESADV_D_96A_UN_EAN005,
            DESADV_D_96A_UN_EAN008,
            GENRAL_D_96A_UN_EAN003,
            INVRPT_D_96A_UN_EAN008,
            INVOIC_D_01B_UN_EAN010,
            INVOIC_D_93A_UN_EAN007,
            INVOIC_D_96A_UN_EAN008,
            REMADV_D_96A_UN_EAN003,
            SLSRPT_D_96A_UN_EAN004,
            ORDERS_D_96A_UN_EAN008,
            ORDRSP_D_96A_UN_EAN005,
            APERAK_D_01B_UN_EAN003,
            RECADV_D_96A_UN_EAN003,
            RECADV_D_01B_UN_EAN005
        }

        public DTOEdiversaFile() : base()
        {
            Segments = new List<DTOEdiversaSegment>();
            Exceptions = new List<DTOEdiversaException>();
        }

        public DTOEdiversaFile(Guid oGuid) : base(oGuid)
        {
            Segments = new List<DTOEdiversaSegment>();
            Exceptions = new List<DTOEdiversaException>();
        }

        public static DTOEdiversaFile Factory(string filePath)
        {
            System.IO.FileInfo oFile = new System.IO.FileInfo(filePath);
            var retval = new DTOEdiversaFile();
            {
                var withBlock = retval;
                withBlock.FileName = System.IO.Path.GetFileName(oFile.FullName);
                withBlock.FchCreated = oFile.CreationTime;
                withBlock.Stream = System.IO.File.ReadAllText(oFile.FullName);
            }
            return retval;
        }

        public static DTOEdiversaFile.Interlocutors ReadInterlocutor(DTOEan Gln)
        {
            DTOEdiversaFile.Interlocutors retval = DTOEdiversaFile.Interlocutors.unknown;
            if (Gln != null)
            {
                string sEan = Gln.Value;
                if (sEan.Length == 13)
                {
                    if (sEan.StartsWith(DTOEdiversaFile.PrefixMatiasMasso))
                        retval = DTOEdiversaFile.Interlocutors.matiasmasso;
                    else if (sEan.StartsWith(DTOEdiversaFile.PrefixBritax))
                        retval = DTOEdiversaFile.Interlocutors.britax;
                    else if (sEan.StartsWith(DTOEdiversaFile.PrefixElCorteIngles))
                        retval = DTOEdiversaFile.Interlocutors.ElCorteIngles;
                    else if (sEan.StartsWith(DTOEdiversaFile.PrefixElCorteInglesPt))
                        retval = DTOEdiversaFile.Interlocutors.ElCorteIngles;
                    else if (sEan.StartsWith(DTOEdiversaFile.PrefixAmazon))
                        retval = DTOEdiversaFile.Interlocutors.amazon;
                    else if (sEan.StartsWith(DTOEdiversaFile.PrefixCarrefour))
                        retval = DTOEdiversaFile.Interlocutors.carrefour;
                    else if (sEan.StartsWith(DTOEdiversaFile.PrefixSonae))
                        retval = DTOEdiversaFile.Interlocutors.sonae;
                    else if (sEan.StartsWith(DTOEdiversaFile.PrefixContinente))
                        retval = DTOEdiversaFile.Interlocutors.sonae;
                    else if (sEan.StartsWith(DTOEdiversaFile.PrefixAlcampo))
                        retval = DTOEdiversaFile.Interlocutors.alcampo;
                    else if (sEan.StartsWith(DTOEdiversaFile.PrefixMiFarma))
                        retval = DTOEdiversaFile.Interlocutors.mifarma;
                }
            }

            return retval;
        }

        public HashSet<String> EansInterlocutors()
        {
            HashSet<String> retval = new HashSet<String>();
            if (Segments.Count == 0)
                LoadSegments();
            foreach (DTOEdiversaSegment segment in Segments.Where(x => x.Tag().StartsWith("NAD")))
            {
                if (segment.Fields.Count > 1)
                    retval.Add(segment.Fields[1]);
            }
            return retval;
        }

        public List<String> EansSkus()
        {
            List<String> retval = new List<String>();
            if (Segments.Count == 0)
                LoadSegments();
            foreach (DTOEdiversaSegment segment in Segments.Where(x => x.Tag() == "LIN"))
            {
                if (segment.Fields.Count > 1)
                    retval.Add(segment.Fields[1]);
            }
            return retval;
        }


        public void LoadSegments()
        {
            Segments = new List<DTOEdiversaSegment>();
            string[] sLines = Regex.Split(Stream, "\r\n|\r|\n"); // Environment.NewLine);
            if (sLines.Length > 0)
            {
                Tag = sLines[0];
                foreach (string sLine in sLines)
                {
                    DTOEdiversaSegment oSegment = DTOEdiversaSegment.Factory(sLine);
                    Segments.Add(oSegment);
                }
            }
        }

        public string FieldValue(string segmentTag, int fieldIdx)
        {
            string retval = "";
            DTOEdiversaSegment oSegment = Segments.FirstOrDefault(x => x.Fields[0] == segmentTag);
            if (oSegment != null)
            {
                if (oSegment.Fields.Count > fieldIdx)
                    retval = oSegment.Fields[fieldIdx];
            }
            return retval;
        }

        public string TagNom()
        {
            Regex r = new Regex("^[a-zA-Z0-9_.-]*", RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Match oMatch = r.Match(Stream);
            string retval = oMatch.Value;
            return retval;
        }

        public DTOEdiversaFile.Tags TagCod()
        {
            string sTag = this.TagNom();
            DTOEdiversaFile.Tags retval = sTag.ToEnum<DTOEdiversaFile.Tags>();
            return retval;
        }

        public DateTime getFch(List<DTOEdiversaException> exs)
        {
            DateTime retval;
            switch (Tag)
            {
                case "RECADV_D_01B_UN_EAN005":
                    retval = ParseFch(this.FieldValue("DTM", 2), exs);
                    break;
                default:
                    retval = ParseFch(this.FieldValue("DTM", 1), exs);
                    break;
            }
            return retval;
        }

        public static DTOEan parseEAN(string src, List<DTOEdiversaException> exs)
        {
            DTOEan retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (src.isNotEmpty())
            {
                string pattern = "[^ -~]+"; // selecciona tots els caracters entre l'espai i la tilde (ASCII 32 - ASCII 126)
                System.Text.RegularExpressions.Regex reg_exp = new System.Text.RegularExpressions.Regex(pattern);
                src = reg_exp.Replace(src, " "); // (clean non ASCII chars)

                if (src.Length == 13)
                    retval = DTOEan.Factory(src);
                else
                    exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatEAN, src, src + " EAN no valid"));
            }
            else
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingEan, src, src + " falta EAN"));

            return retval;
        }

        public static DateTime ParseFch(string src, List<DTOEdiversaException> exs)
        {
            DateTime retval = default(DateTime);
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            try
            {
                switch (src.Length)
                {
                    case 0:
                        {
                            break;
                        }

                    case 6:
                        {
                            retval = DateTime.ParseExact(src, "yyMMdd", provider);
                            break;
                        }

                    case 8:
                        {
                            retval = DateTime.ParseExact(src, "yyyyMMdd", provider);
                            break;
                        }

                    case 12:
                        {
                            retval = DateTime.ParseExact(src, "yyyyMMddHHmm", provider);
                            break;
                        }

                    default:
                        {
                            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, src, "No es pot convertir " + src + " en una data"));
                            break;
                        }
                }
            }
            catch (Exception)
            {
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, src));
            }

            return retval;
        }

        public static Decimal ParseDecimal(string src, List<DTOEdiversaException> exs)
        {
            Decimal retval = 0;
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            try
            {
                retval = decimal.Parse(src, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatDecimal, src, src + " no identificat com a import"));
            }

            return retval;
        }

        public static DTOAmt ParseAmt(string src, List<DTOEdiversaException> exs)
        {
            DTOAmt retval = null;
            Decimal value = ParseDecimal(src, exs);
            if (exs.Count == 0)
                retval = DTOAmt.Factory(value);
            return retval;
        }


        public string GetDocNum()
        {
            string retval = "";
            switch (Tag)
            {
                case "ORDERS_D_96A_UN_EAN008":
                    {
                        retval = this.FieldValue("ORD", 1);
                        break;
                    }

                case "INVOIC_D_93A_UN_EAN007":
                case "INVOIC_D_96A_UN_EAN008":
                case "INVOIC_D_01B_UN_EAN010":
                    {
                        retval = this.FieldValue("INV", 1);
                        break;
                    }

                case "REMADV_D_96A_UN_EAN003":
                case "DESADV_D_96A_UN_EAN005":
                    {
                        retval = this.FieldValue("BGM", 1);
                        break;
                    }
                case "ORDRSP_D_96A_UN_EAN005":
                case "SLSRPT_D_96A_UN_EAN004":
                case "APERAK_D_01B_UN_EAN003":
                case "GENRAL_D_96A_UN_EAN003":
                    {
                        retval = this.FieldValue("BGM", 1);
                        break;
                    }
            }
            return retval;
        }

        public DTOAmt GetAmount(List<DTOEdiversaException> exs)
        {
            DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
            string sEur = this.FieldValue("MOARES", 1);
            if (!string.IsNullOrEmpty(sEur))
                TryParseAmt(sEur, ref retval, exs);
            return retval;
        }

        public static bool TryParseAmt(string src, ref DTOAmt oAmt, List<DTOEdiversaException> exs)
        {
            bool retval = false;
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            try
            {
                decimal DcValue = decimal.Parse(src, System.Globalization.CultureInfo.InvariantCulture);
                oAmt = DTOAmt.Factory(DcValue);
                retval = true;
            }
            catch (Exception)
            {
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatDecimal, null, src + " no identificat com a import"));
            }

            return retval;
        }

        public static string EdiFormat(DTOAmt oAmt)
        {
            string retval = EdiFormat(oAmt.Eur);
            return retval;
        }

        public static string EdiFormat(decimal DcValue)
        {
            string sValue = VbUtilities.Format(DcValue, "#0.00");
            string retval = sValue.Replace(",", ".");
            return retval;
        }

        public static string EdiFormat(DateTime DtFch)
        {
            string retval = VbUtilities.Format(DtFch, "yyyyMMdd");
            return retval;
        }

        public DTOEdiversaFile.IOcods GetIOCod(DTOContact oOrg, List<DTOEdiversaException> exs)
        {
            DTOEan oOrgEan = oOrg.GLN;
            DTOEdiversaFile.IOcods retval = IOcods.notSet;
            if (oOrgEan == null)
            {
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatEAN, oOrg, "No es pot identificar si es d'entrada o de sortida"));

            }
            else
            {
                DTOEan oSenderEan = this.Sender.Ean;
                if (oOrgEan.Equals(oSenderEan))
                    retval = DTOEdiversaFile.IOcods.outbox;
                else
                    retval = DTOEdiversaFile.IOcods.inbox;

            }
            return retval;
        }


        public static DTOEdiversaContact GetSenderFromSegments(DTOEdiversaFile oFile, List<DTOContact> oInterlocutors = null)
        {
            DTOEdiversaContact retval = null;
            string sEan = oFile.FieldValue("NADMS", 1);
            if (sEan == "")
            {
                switch (oFile.TagCod())
                {
                    case DTOEdiversaFile.Tags.DESADV_D_96A_UN_EAN008:
                        {
                            sEan = oFile.FieldValue("NADSU", 1); // suplier
                            break;
                        }

                    case DTOEdiversaFile.Tags.GENRAL_D_96A_UN_EAN003:
                        {
                            break;
                        }

                    case DTOEdiversaFile.Tags.INVOIC_D_01B_UN_EAN010:
                        {
                            sEan = oFile.FieldValue("NADSU", 1); // suplier
                            break;
                        }

                    case DTOEdiversaFile.Tags.INVOIC_D_93A_UN_EAN007:
                        {
                            sEan = oFile.FieldValue("NADSU", 1); // suplier
                            break;
                        }

                    case DTOEdiversaFile.Tags.INVOIC_D_96A_UN_EAN008:
                        {
                            sEan = oFile.FieldValue("NADSU", 1); // suplier
                            break;
                        }

                    case DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008:
                        {
                            sEan = oFile.FieldValue("NADSU", 1); // suplier
                            break;
                        }

                    case DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008:
                        {
                            sEan = oFile.FieldValue("NADBY", 1); // buyer
                            break;
                        }

                    case DTOEdiversaFile.Tags.ORDRSP_D_96A_UN_EAN005:
                        {
                            sEan = oFile.FieldValue("NADSU", 1); // suplier
                            break;
                        }

                    case DTOEdiversaFile.Tags.REMADV_D_96A_UN_EAN003:
                        {
                            sEan = oFile.FieldValue("NADPR", 1); // emisor del pago
                            break;
                        }

                    case DTOEdiversaFile.Tags.SLSRPT_D_96A_UN_EAN004:
                        {
                            sEan = oFile.FieldValue("NADFR", 1);
                            break;
                        }
                    case DTOEdiversaFile.Tags.RECADV_D_01B_UN_EAN005:
                        {
                            sEan = oFile.FieldValue("NADBY", 1);
                            break;
                        }
                }
            }

            if (sEan != "")
            {
                retval = new DTOEdiversaContact();
                retval.Ean = DTOEan.Factory(sEan);
                if (oInterlocutors == null)
                {
                }
                else
                    retval.Contact = oInterlocutors.FirstOrDefault(x => x.GLN.Value == sEan);
            }
            return retval;
        }

        public static DTOEdiversaContact GetReceiverFromSegments(DTOEdiversaFile oFile, List<DTOContact> oInterlocutors = null)
        {
            DTOEdiversaContact retval = null;
            string sEan = oFile.FieldValue("NADMR", 1);
            if (sEan == "")
            {
                switch (oFile.TagCod())
                {
                    case DTOEdiversaFile.Tags.DESADV_D_96A_UN_EAN008:
                        {
                            sEan = oFile.FieldValue("NADBY", 1); // buyer
                            break;
                        }

                    case DTOEdiversaFile.Tags.GENRAL_D_96A_UN_EAN003:
                        {
                            break;
                        }

                    case DTOEdiversaFile.Tags.INVOIC_D_01B_UN_EAN010:
                        {
                            sEan = oFile.FieldValue("NADBY", 1); // buyer
                            break;
                        }

                    case DTOEdiversaFile.Tags.INVOIC_D_93A_UN_EAN007:
                        {
                            sEan = oFile.FieldValue("NADBY", 1); // buyer
                            break;
                        }

                    case DTOEdiversaFile.Tags.INVOIC_D_96A_UN_EAN008:
                        {
                            sEan = oFile.FieldValue("NADBY", 1); // buyer
                            break;
                        }

                    case DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008:
                        {
                            sEan = oFile.FieldValue("NADBY", 1); // buyer
                            break;
                        }

                    case DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008:
                        {
                            sEan = oFile.FieldValue("NADSU", 1); // suplier
                            break;
                        }

                    case DTOEdiversaFile.Tags.ORDRSP_D_96A_UN_EAN005:
                        {
                            sEan = oFile.FieldValue("NADBY", 1); // buyer
                            break;
                        }

                    case DTOEdiversaFile.Tags.REMADV_D_96A_UN_EAN003:
                        {
                            sEan = oFile.FieldValue("NADPE", 1); // receptor del pago
                            break;
                        }

                    case DTOEdiversaFile.Tags.SLSRPT_D_96A_UN_EAN004:
                        {
                            break;
                        }
                    case DTOEdiversaFile.Tags.RECADV_D_01B_UN_EAN005:
                        {
                            sEan = oFile.FieldValue("NADSU", 1);
                            break;
                        }
                }
            }
            if (sEan != "")
            {
                retval = new DTOEdiversaContact();
                retval.Ean = DTOEan.Factory(sEan);
                if (oInterlocutors != null)
                    retval.Contact = oInterlocutors.FirstOrDefault(x => x.GLN.Value == sEan);
            }
            return retval;
        }

        public static string ReadDocNum(DTOEdiversaFile oFile)
        {
            string retval = "";
            switch (oFile.Tag)
            {
                case "ORDERS_D_96A_UN_EAN008":
                    {
                        retval = oFile.FieldValue("ORD", 1);
                        break;
                    }

                case "INVOIC_D_93A_UN_EAN007":
                case "INVOIC_D_96A_UN_EAN008":
                case "INVOIC_D_01B_UN_EAN010":
                    {
                        retval = oFile.FieldValue("INV", 1);
                        break;
                    }

                case "REMADV_D_96A_UN_EAN003":
                case "DESADV_D_96A_UN_EAN008":
                case "ORDRSP_D_96A_UN_EAN005":
                case "SLSRPT_D_96A_UN_EAN004":
                case "APERAK_D_01B_UN_EAN003":
                    {
                        retval = oFile.FieldValue("BGM", 1);
                        break;
                    }
            }
            return retval;
        }

        public static void addSegment(ref System.Text.StringBuilder sb, string tag, params object[] fields)
        {
            string segment = DTOEdiversaSegment.Factory(tag, fields);
            sb.AppendLine(segment);
        }

        public static MatHelper.Excel.Sheet excel(List<DTOEdiversaFile> oFiles)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(oFiles.First().Tag, "Edi");
            {
                var withBlock = retval;
                withBlock.AddColumn("fitxer", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("rebut", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("document", MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn("data doc", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("import", MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn("remitent/destinatari", MatHelper.Excel.Cell.NumberFormats.PlainText);

                foreach (DTOEdiversaFile oFile in oFiles)
                {
                    MatHelper.Excel.Row oRow = withBlock.AddRow();
                    oRow.AddCell(oFile.FileName);
                    oRow.AddCell(oFile.FchCreated);
                    oRow.AddCell(oFile.Fch);

                    if (oFile.Amount == null)
                        oRow.AddCell();
                    else
                        oRow.AddCellAmt(oFile.Amount);

                    switch (oFile.IOCod)
                    {
                        case DTOEdiversaFile.IOcods.inbox:
                            {
                                if (oFile.Sender != null)
                                {
                                    if (oFile.Sender.Contact == null)
                                        oRow.AddCell();
                                    else
                                        oRow.AddCell(oFile.Sender.Contact.Nom);
                                }

                                break;
                            }

                        case DTOEdiversaFile.IOcods.outbox:
                            {
                                if (oFile.Receiver != null)
                                {
                                    if (oFile.Receiver.Contact == null)
                                        oRow.AddCell();
                                    else
                                        oRow.AddCell(oFile.Receiver.Contact.Nom);
                                }

                                break;
                            }
                    }

                    oRow.AddCell(oFile.Docnum);
                }
            }
            return retval;
        }
    }

    public class DTOEdiversaSegment
    {
        public List<string> Fields { get; set; }


        public DTOEdiversaSegment() : base()
        {
            Fields = new List<string>();
        }

        public string Tag()
        {
            string retval = "";
            if (this.Fields.Count > 0)
                retval = this.Fields[0];
            return retval;
        }

        public string ParseString(int iField, List<DTOEdiversaException> exs)
        {
            string retval = "";
            if (Fields.Count > iField)
                retval = Fields[iField];
            return retval;
        }

        public decimal ParseDecimal(int iField, List<DTOEdiversaException> exs)
        {
            decimal retval = 0;
            if (Fields.Count > iField)
                retval = decimal.Parse(Fields[iField], System.Globalization.CultureInfo.InvariantCulture);
            return retval;
        }

        public int ParseInteger(int iField, List<DTOEdiversaException> exs)
        {
            int retval = 0;
            if (Fields.Count > iField)
                retval = System.Convert.ToInt32(Fields[iField]);
            return retval;
        }

        public DateTime ParseFch(int iField, List<DTOEdiversaException> exs)
        {
            DateTime retval = TimeHelper.bottomDate();
            if (Fields.Count > iField)
            {
                string src = Fields[iField];
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                try
                {
                    switch (src.Length)
                    {
                        case 6:
                            {
                                retval = DateTime.ParseExact(src, "yyMMdd", provider);
                                break;
                            }

                        case 8:
                            {
                                retval = DateTime.ParseExact(src, "yyyyMMdd", provider);
                                break;
                            }

                        default:
                            {
                                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, string.Format("No es pot convertir {0} en una data", src)));
                                break;
                            }
                    }
                }
                catch (Exception)
                {
                    exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, src));
                }
            }

            return retval;
        }

        public DTOEan ParseEan(int iField, List<DTOEdiversaException> exs)
        {
            DTOEan retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (Fields.Count > iField)
                retval = DTOEan.Factory(Fields[iField]);
            return retval;
        }

        public static DTOEdiversaSegment Factory(string src)
        {
            DTOEdiversaSegment retval = new DTOEdiversaSegment();
            string[] sFields = Regex.Split(src, @"\|");
            foreach (string sField in sFields)
                retval.Fields.Add(sField);
            return retval;
        }

        public new string ToString()
        {
            string retval = string.Join("|", Fields.ToArray());
            return retval;
        }

        public static string Factory(string tag, params object[] fields)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(tag);
            foreach (var field in fields)
            {
                sb.Append("|");
                if (field is string | field is int)
                    sb.Append(field);
                else if (field is DateTime)
                    sb.Append(DTOEdiversaFile.EdiFormat((DateTime)field));
                else if (field is DTOAmt)
                    sb.Append(DTOEdiversaFile.EdiFormat((DTOAmt)field));
                else if (field is decimal)
                    sb.Append(DTOEdiversaFile.EdiFormat(System.Convert.ToDecimal(field)));
            }
            string retval = sb.ToString();
            return retval;
        }
    }

    public class DTOEdiversaContact
    {
        public Cods Cod { get; set; }
        public DTOEan Ean { get; set; }
        public string Nom { get; set; }
        public string DadesRegistrals { get; set; }
        public string Domicili { get; set; }
        public string Poblacio { get; set; }
        public string Zip { get; set; }
        public string Nif { get; set; }

        public DTOContact Contact { get; set; }
        public enum Cods
        {
            NotSet,
            NADSCO // _Proveidor
    ,
            NADBCO // _Comprador
    ,
            NADSU // _Proveidor
    ,
            NADBY // _Comprador
    ,
            NADII // _EmisorFactura
    ,
            NADIV // _ReceptorFactura
    ,
            NADMS // _EmisorMissatge
    ,
            NADMR // _ReceptorMissatge
    ,
            NADDP // _ReseptorMercancia
    ,
            NADPR // _Pagador
    ,
            NADPE // _Cobrador
    ,
            NADPW // _PuntDeRecollida
    ,
            NADUD // _ClientFinal
    ,
            NADFW // _Forwarder
    ,
            NADSE // _Venedor
    ,
            NADRE // _Cobrador
    ,
            NADCO // _Venedor
        }
    }

    public class DTOEdiversaException : DTOBaseGuid
    {
        public Cods Cod { get; set; }
        public DTOBaseGuid Tag { get; set; }
        public TagCods TagCod { get; set; }
        public string Msg { get; set; }

        public enum TagCods
        {
            NotSet,
            Sku,
            PurchaseOrder,
            EdiversaOrder,
            Contact,
            EdiversaFile,
            EdiversaOrderItem,
            Gln,
            Ean,
            NADBY,
            NADIV,
            NADDP,
            NADMR,
            NADMS
        }

        public enum Cods
        {
            NotSet,
            SkuNotFound,
            WrongPrice,
            MissingPrice,
            WrongDiscount,
            BadFormatFch,
            MissingSegmentFields,
            ContactCompradorNotFound,
            ReceptorMercanciaNotFound,
            PlatformNotFound,
            PlatformNoValid,
            BadFormatDecimal,
            SkuObsolet,
            DuplicatedOrder,
            InterlocutorNotFound,
            MissingSkuEAN,
            BadFormatEAN,
            MissingEan,
            PurchaseOrderNotFound
        }

        public DTOEdiversaException() : base()
        {
        }

        public DTOEdiversaException(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOEdiversaException Factory(Cods oCod, object oTag, string sMsg = "")
        {
            DTOEdiversaException retval = new DTOEdiversaException();
            {
                var withBlock = retval;
                withBlock.Cod = oCod;
                withBlock.Tag = (DTOBaseGuid)oTag;
                withBlock.Msg = sMsg;
            }
            return retval;
        }

        public static List<DTOEdiversaException> FromSystemExceptions(List<Exception> exs)
        {
            List<DTOEdiversaException> retval = new List<DTOEdiversaException>();
            foreach (var item in exs)
            {
                string sMsg = item.Message;
                retval.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, null, sMsg));
            }
            return retval;
        }

        public static List<Exception> ToSystemExceptions(List<DTOEdiversaException> exs)
        {
            List<Exception> retval = new List<Exception>();
            foreach (var item in exs)
                retval.Add(new Exception(item.Msg));
            return retval;
        }
    }
}
