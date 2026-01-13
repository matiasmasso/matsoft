using DTO;
using iText.StyledXmlParser.Jsoup.Helper;
using MatHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatHelperCFwk
{
    public class NominaEscuraHelper
    {
        public string filename { get; set; }

        public List<string> lines { get; set; }



        const string IDSEGMENT = "Nº AFILIACION. S.S. TARIFA COD.CT SECCION PERIODO NRO. TOT. DIAS";

        public enum Headers
        {
            Dades,
            Detall,
            Totals,
            Liquid
        }

        public enum segmentNums
        {
            nifEmpresa = 4,
            segSocial = 12,
            totals = 36,
            fch = 40,
            liquid = 43
        }

        public enum Totals
        {
            RemTotal,
            pagasExtras,
            BaseSegSocial,
            BaseAccYDesempleo,
            BaseIrpf,
            TotalDevengado,
            TotalADeducir
        }



        public static bool isNominaEscura(List<string> segments)
        {
            List<Exception> exs = new List<Exception>();
            var sHeaderCaption = HeaderCaption(Headers.Dades);
            var oMatchingSegments = TextHelper.MatchingSegments(segments, sHeaderCaption);
            var retval = oMatchingSegments.Count > 0;
            return retval;
        }

        public static bool CheckFile(List<Exception> exs, string filename, DTOUser oUser, ref DateTime fch)
        {
            bool retval = false;
            var oSegments = segments(exs, filename);
            fch = readFch(exs, oSegments);
            var sNif = readEmpNif(exs, oSegments);
            if (!string.IsNullOrEmpty(sNif))
            {
                if (!sNif.StartsWith("ES"))
                    sNif = "ES" + sNif;
                var sOrgNif = oUser.Emp.Org.PrimaryNifValue();
                if (!sOrgNif.StartsWith("ES"))
                    sOrgNif = "ES" + sOrgNif;
                retval = sNif == sOrgNif;
            }
            return retval;
        }
        //public static bool CheckFile(List<Exception> exs, string filename, DTOUser oUser, ref DateTime fch)
        //{
        //    var oSegments = segments(exs, filename);
        //    fch = readFch(exs, oSegments);
        //    var sNif = readEmpNif(exs, oSegments);
        //    if (!string.IsNullOrEmpty(sNif))
        //    {
        //        if (!sNif.StartsWith("ES"))
        //            sNif = "ES" + sNif;
        //        var sOrgNif = oUser.Emp.Org.PrimaryNifValue();
        //        if (!sOrgNif.StartsWith("ES"))
        //            sOrgNif = "ES" + sOrgNif;
        //        if (sNif != sOrgNif)
        //        {
        //            var nifOwner = FEB.Contact.SearchSync(exs, oUser, sNif, DTOContact.SearchBy.nif);
        //            if (nifOwner == null)
        //                exs.Add(new Exception("Aquestes nómines no son de " + oUser.Emp.Org.Nom));
        //            else
        //                exs.Add(new Exception("Aquestes nómines son de " + oUser.Emp.Org.Nom + Environment.NewLine + "cal canviar d'empresa per importar-les"));
        //        }
        //    }
        //    return exs.Count == 0;
        //}

        public static bool CheckFile(List<Exception> exs, byte[] byteArray, DTOUser oUser, ref DateTime fch)
        {
            var oSegments = segments(exs, byteArray);
            fch = readFch(exs, oSegments);
            var sNif = readEmpNif(exs, oSegments);
            if (!sNif.StartsWith("ES"))
                sNif = "ES" + sNif;
            var sOrgNif = oUser.Emp.Org.PrimaryNifValue();
            if (!sOrgNif.StartsWith("ES"))
                sOrgNif = "ES" + sOrgNif;
            return sNif == sOrgNif;
        }
        //public static bool CheckFile(List<Exception> exs, byte[] byteArray, DTOUser oUser, ref DateTime fch)
        //{
        //    var oSegments = segments(exs, byteArray);
        //    fch = readFch(exs, oSegments);
        //    var sNif = readEmpNif(exs, oSegments);
        //    if (!sNif.StartsWith("ES"))
        //        sNif = "ES" + sNif;
        //    var sOrgNif = oUser.Emp.Org.PrimaryNifValue();
        //    if (!sOrgNif.StartsWith("ES"))
        //        sOrgNif = "ES" + sOrgNif;
        //    if (sNif != sOrgNif)
        //    {
        //        var nifOwner = FEB.Contact.SearchSync(exs, oUser, sNif, DTOContact.SearchBy.nif);
        //        if (nifOwner == null)
        //            exs.Add(new Exception("Aquestes nómines no son de " + oUser.Emp.Org.Nom));
        //        else
        //            exs.Add(new Exception("Aquestes nómines son de " + oUser.Emp.Org.Nom + Environment.NewLine + "cal canviar d'empresa per importar-les"));
        //    }
        //    return exs.Count == 0;
        //}

        public static async Task<List<DTONomina>> nominas(List<Exception> exs, string filename, DTOUser oUser, DTOCert oCert, List<DTOStaff>oStaffs, List<DTOPgcCta>oCtas, ProgressBarHandler ShowProgress = null)
        {
            List<DTONomina> retval = new List<DTONomina>();
            var sOrgNif = oUser.Emp.Org.PrimaryNifValue();
            if (!sOrgNif.StartsWith("ES"))
                sOrgNif = "ES" + sOrgNif;
            bool cancelRequest = false;
            DTOExercici oExercici = null/* TODO Change to default(_) if this is not a reference type */;

            var filenames = MatHelper.PdfHelper.SplitPdf(filename);
            // Dim filenames = LegacyHelper.iTextPdfHelper.splitFileIntoPages(exs, filename)
            bool firstFile = true;
            foreach (var pageFilename in filenames)
            {
                if (firstFile)
                {
                    var oFirstFileSegments = segments(exs, pageFilename);
                    var empNif = readEmpNif(exs, oFirstFileSegments);
                    if (!empNif.StartsWith("ES"))
                        empNif = "ES" + empNif;

                    if (empNif == sOrgNif)
                    {
                        var fch = readFch(exs, oFirstFileSegments);
                        oExercici = DTOExercici.FromYear(oUser.Emp, fch.Year);
                        firstFile = false;
                    }
                    else
                    {
                        exs.Add(new Exception("Aquestes nomines no son de " + oUser.Emp.Org.Nom + ""));
                        break;
                    }
                }

                List<Exception> exs2 = new List<Exception>();
                var oNomina = await ReadNomina(exs2, pageFilename, oUser, oStaffs, oCtas, oCert);
                if (exs2.Count == 0)
                    retval.Add(oNomina);
                else
                    exs.AddRange(exs2);

                string sProgressText = DTOStaff.AliasOrNom(oNomina.Staff);
                ShowProgress(1, filenames.Count, filenames.IndexOf(pageFilename) + 1, sProgressText, ref cancelRequest);
                if (cancelRequest)
                    break;
            }
            return retval;
        }



        public static async Task<DTONomina> ReadNomina(List<Exception> exs, string filename, DTOUser oUser, List<DTOStaff> oStaffs, List<DTOPgcCta> oCtas, DTOCert oCert)
        {
            DTONomina retval = null/* TODO Change to default(_) if this is not a reference type */;
            var oSegments = segments(exs, filename);
            if (exs.Count == 0)
            {
                var oStaff = readStaff(exs, oSegments, oStaffs);
                if (exs.Count == 0)
                {
                    retval = new DTONomina(oStaff);
                    {
                        var withBlock = retval;
                        retval.Dietes = DTOAmt.Empty();
                        retval.Embargos = DTOAmt.Empty();
                        retval.Deutes = DTOAmt.Empty();
                        retval.Anticips = DTOAmt.Empty();
                        retval.SegSocial = DTOAmt.Empty();
                        retval.Irpf = DTOAmt.Empty();

                        retval.Items = readItems(exs, oSegments);
                        foreach (var oItem in retval.Items)
                        {
                            switch (oItem.Concepte.Id)
                            {
                                case 602:
                                case 603:
                                case 604:
                                case 678 // dietas
                               :
                                    {
                                        retval.Dietes.Add(oItem.Devengo);
                                        break;
                                    }

                                case 703:
                                    {
                                        retval.Embargos.Add(oItem.Deduccio);
                                        break;
                                    }

                                case 709:
                                case 712:
                                    {
                                        retval.Anticips.Add(oItem.Deduccio);
                                        break;
                                    }

                                case 740:
                                    {
                                        retval.Deutes.Add(oItem.Deduccio);
                                        break;
                                    }

                                case 995:
                                case 996:
                                case 997:
                                    {
                                        retval.SegSocial.Add(oItem.Deduccio);
                                        break;
                                    }

                                case 999:
                                    {
                                        retval.Irpf = oItem.Deduccio;
                                        break;
                                    }
                            }
                        }
                        retval.IrpfBase = GetIrpfBase(exs, oSegments);
                        retval.Devengat = GetTotalDevengat(exs, oSegments);
                        retval.Liquid = GetLiquid(exs, oSegments);

                        var dtFch = readFch(exs, oSegments);
                        retval.Cca = FEB.Nomina.Cca(ref retval, dtFch, oCtas, oUser, exs);

                        // Dim signedFilename = LegacyHelper.LegacyDivers.NominaStamper(exs, filename, oStaff, oCert)
                        var signedFilename = await NominaStamper(exs, filename, oStaff, oCert);
                        retval.Cca.DocFile = DocfileHelper.Factory(signedFilename, exs);
                    }
                }
            }
            return retval;
        }

        public static async Task<string> NominaStamper(List<Exception> exs, string filename, DTOStaff oStaff, DTOCert oCert)
        {
            var sIbanDigits = DTOIban.Formated(oStaff.Iban);
            var oIbanText = MatPdfText.Factory(sIbanDigits, new Rectangle(75, 620, 1000, 1000));
            var signedFilename = filename.Replace(".pdf", ".signed.pdf");

            // provisional mentres no funciona la signatura:
            var oSigImgBytes = await FEB.Api.FetchImage(exs, oCert.ImageUri.AbsoluteUri);
            var oLogo = ImageHelper.Converter(oSigImgBytes);
            var oRectangle = new Rectangle(140, 650, 200, 40);
            var oResizedLogo = ImageHelper.GetThumbnailToFitAndFill((Bitmap)oLogo, oRectangle.Width, oRectangle.Height);
            var oResizedRectangle = new Rectangle(oRectangle.X + (oRectangle.Width - oResizedLogo.Width) / 2, oRectangle.Y + (oRectangle.Height - oResizedLogo.Height) / 2, oResizedLogo.Width, oResizedLogo.Height);

            MatPdfImage oIbanSigFake = new MatPdfImage();
            {
                var withBlock = oIbanSigFake;
                withBlock.Image = oResizedLogo;
                withBlock.rectangle = oResizedRectangle;
            }

            var objectsToAdd = new List<MatPdfObject>() { oIbanText, oIbanSigFake };
            PdfHelper.Write(exs, filename, signedFilename, objectsToAdd);
            // iTextPdfHelper.write(exs, filename, destFilename, {oIbanText, oIbanSigFake})

            // Dim signedFilename = destFilename.Replace(".edited.pdf", ".signed.pdf")

            Rectangle signRect = new Rectangle(140, 600, 200, 40);

            // sustitueix la signatura

            // preventivament anulat
            // PdfSignatureHelper.Sign(exs, destFilename, signedFilename, oCert.memoryStream, oCert.Pwd, signRect, oCert.ImageUri)
            return signedFilename;
        }

        public static string PrintLogo(List<Exception> exs, string filename, string logoPath)
        {
            var signedFilename = filename.Replace(".pdf", ".signed.pdf");

            // provisional mentres no funciona la signatura:
            var oLogo = Image.FromFile(logoPath);
            var oRectangle = new Rectangle(140, 650, 200, 40);
            var oResizedLogo = ImageHelper.GetThumbnailToFit((Bitmap)oLogo, oRectangle.Width, oRectangle.Height);
            var oResizedRectangle = new Rectangle(oRectangle.X + (oRectangle.Width - oResizedLogo.Width) / 2, oRectangle.Y + (oRectangle.Height - oResizedLogo.Height) / 2, oResizedLogo.Width, oResizedLogo.Height);
            MatPdfImage oPdfLogo = new MatPdfImage();
            {
                var withBlock = oPdfLogo;
                withBlock.Image = oResizedLogo;
                withBlock.rectangle = oResizedRectangle;
            }


            var objectsToAdd = new List<MatPdfObject>() { oPdfLogo };
            PdfHelper.Write(exs, filename, signedFilename, objectsToAdd);
            return signedFilename;
        }

        private static DateTime readFch(List<Exception> exs, List<string> oSegments)
        {
            DateTime retval = default(DateTime);
            if (oSegments.Count > (int)segmentNums.fch)
            {
                var sFch = oSegments[(int)segmentNums.fch].Trim();
                List<string> sMonths = new List<string>
                {
                "ENERO",
                "FEBRERO",
                "MARZO",
                "ABRIL",
                "MAYO",
                "JUNIO",
                "JULIO",
                "AGOSTO",
                "SEPTIEMBRE",
                "OCTUBRE",
                "NOVIEMBRE",
                "DICIEMBRE"
            };
                var sMonth = sMonths.FirstOrDefault(x => sFch.Contains(x));
                if (string.IsNullOrEmpty(sMonth))
                {
                    var sMonthsPattern = string.Join("|", sMonths);
                    string sPattern = @"\d{1,2} (" + sMonthsPattern + @")\ *\d{4}";

                    var monthSegments = TextHelper.MatchingSegments(oSegments, sPattern);
                    if (monthSegments.All(x => x == monthSegments.First()))
                    {
                        sFch = monthSegments.First().Trim();
                        sMonth = sMonths.FirstOrDefault(x => sFch.Contains(x));
                    }
                    else
                    {
                        exs.Add(new Exception("no s'ha trobat la data del document"));
                        return default(DateTime);
                    }
                }
                var iMonth = sMonths.IndexOf(sMonth) + 1;
                var sYear = sFch.Substring(sFch.Length - 4, 4);
                var iYear = System.Convert.ToInt32(sYear);
                var iPos = sFch.IndexOf(" ");
                int iDay = Convert.ToInt32(sFch.Substring(0, iPos));
                retval = new DateTime(iYear, iMonth, iDay);
            }
            else
                exs.Add(new Exception("No es pot llegir la data del document." + Environment.NewLine + "Verificar que no es tracti de un paper escanejat"));
            return retval;
        }

        private static string readEmpNif(List<Exception> exs, List<string> oSegments)
        {
            string retval = "";
            var segments = TextHelper.MatchingSegments(oSegments, "NIF.");
            if (segments.Count > 0)
                retval = segments.First().Replace("NIF.", "").Trim().Split(' ').First();
            else
                exs.Add(new Exception("No s'ha trobat cap referencia al Nif de la empresa"));
            return retval;
        }

        public static DTOStaff readStaff(List<Exception> exs, List<string> oSegments, List<DTOStaff> oStaffs)
        {
            DTOStaff retval = null/* TODO Change to default(_) if this is not a reference type */;
            var iHeaderIdx = HeaderIdx(exs, oSegments, Headers.Dades);
            if (exs.Count == 0)
            {
                var segment = oSegments[iHeaderIdx + 1].Trim();
                var fields = segment.Split(' ');
                if (fields.Count() > 0)
                {
                    var sSegSocNum = fields.First();
                    retval = oStaffs.FirstOrDefault(x => TextHelper.Match(x.NumSs, sSegSocNum));
                }
            }
            return retval;
        }

        private static List<DTONomina.Item> readItems(List<Exception> exs, List<string> oSegments)
        {
            List<DTONomina.Item> retval = new List<DTONomina.Item>();
            var iHeaderIdx = HeaderIdx(exs, oSegments, Headers.Detall);
            if (exs.Count == 0)
            {
                var startIdx = iHeaderIdx + 1;
                for (int idx = startIdx; idx <= startIdx + 19; idx++)
                {
                    var segment = oSegments[idx];
                    string sCod = segment.Substring(23, 6);
                    string sConcepte = segment.Substring(32, 36).Trim();
                    if (TextHelper.VbIsNumeric(sCod))
                    {
                        DTONomina.Concepte oCod = new DTONomina.Concepte(Convert.ToInt32(sCod), sConcepte);
                        var sQty = segment.Substring(0, 12);
                        string sPrice = segment.Substring(12, 11);
                        string sDevengo = segment.Substring(70, 11);
                        string sDeduccio = segment.Substring(84, 11);
                        DTONomina.Item oItem = new DTONomina.Item(oCod);
                        {
                            var withBlock = oItem;
                            if (TextHelper.VbIsNumeric(sQty))
                                withBlock.Qty = System.Convert.ToInt32(sQty.Trim());
                            if (TextHelper.VbIsNumeric(sPrice))
                                withBlock.Price = DTOAmt.Factory(System.Convert.ToDecimal(sPrice.Trim()));
                            if (TextHelper.VbIsNumeric(sDevengo))
                                withBlock.Devengo = DTOAmt.Factory(System.Convert.ToDecimal(sDevengo.Trim()));
                            if (TextHelper.VbIsNumeric(sDeduccio))
                                withBlock.Deduccio = DTOAmt.Factory(System.Convert.ToDecimal(sDeduccio.Trim()));
                        }
                        retval.Add(oItem);
                    }
                    else
                        break;
                }
            }
            else
                exs.Add(new Exception("S'ha trobat cap o mes d'una capçalera de detall"));
            return retval;
        }

        public static DTOAmt GetTotalDevengat(List<Exception> exs, List<string> oSegments)
        {
            DTOAmt retval = DTOAmt.Factory();
            var oSegmentTotals = SegmentTotals(exs, oSegments);
            if (exs.Count == 0)
            {
                var fields = TextHelper.splitByLength(oSegmentTotals, 14);
                string field = fields[(int)Totals.TotalDevengado].Trim();
                retval = ParseAmt(field);
            }
            return retval;
        }

        public static DTOAmt GetIrpfBase(List<Exception> exs, List<string> oSegments)
        {
            DTOAmt retval = DTOAmt.Factory();
            var oSegmentTotals = SegmentTotals(exs, oSegments);
            if (exs.Count == 0)
            {
                var fields = TextHelper.splitByLength(oSegmentTotals, 14);
                string field = fields[(int)Totals.BaseIrpf].Trim();
                retval = ParseAmt(field);
            }
            return retval;
        }

        public static DTOAmt GetLiquid(List<Exception> exs, List<string> oSegments)
        {
            var oCaptionsSegmentIdx = HeaderIdx(exs, oSegments, Headers.Liquid);
            var segment = oSegments[oCaptionsSegmentIdx + 1].Trim();
            var retval = ParseAmt(segment);
            return retval;
        }

        public static string SegmentTotals(List<Exception> exs, List<string> oSegments)
        {
            var iHeaderIdx = HeaderIdx(exs, oSegments, Headers.Totals);
            var retval = oSegments[iHeaderIdx + 1];
            return retval;
        }


        public static int HeaderIdx(List<Exception> exs, List<string> oSegments, Headers Header)
        {
            // Busca l'index del segment a partir del qual trobar les dades
            int retval = 0;
            var sHeaderCaption = HeaderCaption(Header);
            var oMatchingSegments = TextHelper.MatchingSegments(oSegments, sHeaderCaption);
            switch (oMatchingSegments.Count)
            {
                case 0:
                    {
                        exs.Add(new Exception("no s'ha trobat la capçalera"));
                        break;
                    }

                case 1:
                    {
                        retval = oSegments.IndexOf(oMatchingSegments.First());
                        break;
                    }

                default:
                    {
                        exs.Add(new Exception("s'han trobat " + oMatchingSegments.Count + " capçaleres duplicades"));
                        break;
                    }
            }
            return retval;
        }

        public static string HeaderCaption(Headers Header)
        {
            string retval = "";
            switch (Header)
            {
                case Headers.Dades:
                    {
                        retval = "Nº AFILIACION. S.S. TARIFA COD.CT SECCION PERIODO NRO. TOT. DIAS";
                        break;
                    }

                case Headers.Detall:
                    {
                        retval = "CONCEPTO DEVENGOS DEDUCCIONES";
                        break;
                    }

                case Headers.Totals:
                    {
                        retval = "REM. TOTAL P.P.EXTRAS BASE I.R.P.F. T. DEVENGADO BASE A.T. Y DES."; // "BASE S.S. T.  A DEDUCIR REM. TOTAL P.P.EXTRAS BASE I.R.P.F. T. DEVENGADO BASE A.T. Y DES."
                        break;
                    }

                case Headers.Liquid:
                    {
                        retval = "LIQUIDO A PERCIBIR";
                        break;
                    }
            }
            return retval;
        }

        public static DTOAmt ParseAmt(string src)
        {
            decimal value = 0;
            if (!string.IsNullOrEmpty(src))
                value = decimal.Parse(src, System.Globalization.NumberStyles.Number, new System.Globalization.CultureInfo("es-ES"));
            var retval = DTOAmt.Factory(value);
            return retval;
        }

        public static List<string> segments(List<Exception> exs, string filename)
        {
            // Dim textStream = LegacyHelper.iTextPdfHelper.readText(filename, exs)
            var textStream = MatHelper.PdfHelper.readText(filename, exs);
            var retval = Regex.Split(textStream, "\r\n|\r|\n").ToList();
            return retval;
        }

        public static List<string> segments(List<Exception> exs, byte[] byteArray)
        {
            // Dim textStream = LegacyHelper.iTextPdfHelper.readText(byteArray, exs)
            var textStream = MatHelper.PdfHelper.readText(byteArray, exs);
            var retval = Regex.Split(textStream, "\r\n|\r|\n").ToList();
            return retval;
        }
    }
}
