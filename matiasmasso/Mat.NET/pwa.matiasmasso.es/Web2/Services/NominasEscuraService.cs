using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Web.Helpers;

namespace Web.Services
{
    public class NominasEscuraService : IDisposable
    {

        public event Action<Exception?>? OnChange;
        public event Action<List<NominaModel>>? NominaLoaded;
        private AppStateService appstate;
        private SessionStateService session;
        private EmpsService empsService;
        private StaffsService staffsService;
        private NominasService nominasService;
        private PgcCtasService pgcCtasService;
        private IbansService ibansService;


        public NominasEscuraService(AppStateService appstate
            , SessionStateService session
            , EmpsService empsService
            , StaffsService staffsService
            , NominasService nominasService
            , PgcCtasService pgcCtasService
            , IbansService ibansService)
        {
            this.appstate = appstate;
            this.session = session;
            this.empsService = empsService;
            this.staffsService = staffsService;
            this.nominasService = nominasService;
            this.pgcCtasService = pgcCtasService;
            this.ibansService = ibansService;

            empsService.OnChange += NotifyChange;
            staffsService.OnChange += NotifyChange;
            nominasService.OnChange += NotifyChange;
            pgcCtasService.OnChange += NotifyChange;
            ibansService.OnChange += NotifyChange;
        }

        public async Task<List<NominaModel>> Nominas(List<IBrowserFile> files, string concept)
        {
            List<NominaModel> retval = new();
            foreach (var file in files)
            {
                var fileNominas = await FileNominas(file, concept);
                retval.AddRange(fileNominas);
            }
            return retval;
        }
        private async Task<List<NominaModel>> FileNominas(IBrowserFile file, string concept)
        {
            List<NominaModel> retval = new();

            var fileBytes = await IBrowserFileHelper.FileBytes(file);
            var filePages = PdfHelper.Split(fileBytes);
            foreach (var pageBytes in filePages)
            {
                var nomina = ReadNomina(pageBytes, concept);
                retval.Add(nomina);
                NominaLoaded?.Invoke(retval);
            }
            return retval;
        }
        private NominaModel ReadNomina(byte[] pageBytes, string concept)
        {
            var fileText = PdfHelper.ReadText(pageBytes);
            var segments = TextHelper.SplitIntoLines(fileText);
            EmpModel? emp = null;
            var nif = ReadEmpNif(segments);
            if (nif != null)
                emp = empsService.FromNif(nif);

            var retval = new NominaModel()
            {
                Staff = (Guid)ReadStaff(segments, emp)!,
                Devengat = ReadDevengat(segments),
                Liquid = ReadLiquid(segments),
                Items = ReadItems(segments),
                IrpfBase = ReadIrpfBase(segments)
            };

            foreach (var oItem in retval.Items)
            {
                switch (oItem.Concepte?.Id)
                {
                    case 602:
                    case 603:
                    case 604:
                    case 678: // dietas
                        retval.Dietes += oItem.Devengo; break;
                    case 703:
                        retval.Embargos += oItem.Deduccio; break;
                    case 709:
                    case 712:
                        retval.Anticips += oItem.Deduccio; break;
                    case 740:
                        retval.Deutes += oItem.Deduccio; break;
                    case 995:
                    case 996:
                    case 997:
                        retval.SegSocial += oItem.Deduccio; break;
                    case 999:
                        retval.Irpf = oItem.Deduccio; break;
                }
            }

            retval.Devengat -= retval.Dietes;
            retval.Cca!.Emp = emp?.Id ?? EmpModel.EmpIds.NotSet;

            retval.Cca!.Fch = ReadFch(segments);
            retval.Cca.Concept = staffsService.GetValue(retval.Staff)?.Abr + " - " + concept;


            var iban = ibansService.FromStaff(retval.Staff);
            var contents = new List<PdfHelper.Content>
            {
             PdfHelper.Content.Text(iban.FormatedDigits() ?? "", 100, 160, 200, 24)
            };

            var editedBytes = PdfHelper.WriteContent(pageBytes, contents);
            var signedBytes = PdfSignHelper.Sign(editedBytes, emp?.Cert, 190, 180, 132, 57);
            retval.Cca.Docfile = Helpers.DocfileHelper.CreatePdfDocfile(signedBytes);
            retval.Cca.Docfile.Nom = retval.Cca.Concept;
            retval.Cca.Ccd = CcaModel.CcdEnum.Nomina;
            retval.Cca.UsrLog = UsrLogModel.Factory(session.User);

            if ((retval.Devengat ?? 0) != 0)
            {
                var oDevengatMenysDietes = retval.Devengat ?? 0 - retval.Dietes ?? 0;
                retval.Cca.AddDebit(oDevengatMenysDietes, pgcCtasService.GetValue(PgcCtaModel.Cods.Nomina)!, retval.Staff);
            }

            if ((retval.Dietes ?? 0) != 0)
            {
                retval.Cca.AddDebit((decimal)retval.Dietes!, pgcCtasService.GetValue(PgcCtaModel.Cods.Dietas)!, retval.Staff);
            }

            if ((retval.SegSocial ?? 0) != 0)
            {
                retval.Cca.AddCredit((decimal)retval.SegSocial!, pgcCtasService.GetValue(PgcCtaModel.Cods.SegSocialDevengo)!, retval.Staff);
            }

            if ((retval.Irpf ?? 0) != 0)
            {
                retval.Cca.AddCredit((decimal)retval.Irpf!, pgcCtasService.GetValue(PgcCtaModel.Cods.IrpfTreballadors)!, retval.Staff);
            }
            if ((retval.Embargos ?? 0) != 0)
            {
                retval.Cca.AddCredit((decimal)retval.Embargos!, pgcCtasService.GetValue(PgcCtaModel.Cods.NominaEmbargos)!, retval.Staff);
            }
            if ((retval.Deutes ?? 0) != 0)
            {
                retval.Cca.AddCredit((decimal)retval.Deutes!, pgcCtasService.GetValue(PgcCtaModel.Cods.NominaDeutes)!, retval.Staff);
            }
            if ((retval.Anticips ?? 0) != 0)
            {
                retval.Cca.AddCredit((decimal)retval.Anticips!, pgcCtasService.GetValue(PgcCtaModel.Cods.AnticipsTreballadors)!, retval.Staff);
            }
            if ((retval.Liquid ?? 0) != 0)
            {
                retval.Cca.AddCredit((decimal)retval.Liquid!, pgcCtasService.GetValue(PgcCtaModel.Cods.PagasTreballadors)!, retval.Staff);
            }

            //var signedFilename = await NominaStamper(exs, filename, oStaff, oCert);
            //retval.Cca.DocFile = DocfileHelper.Factory(signedFilename, exs);

            return retval;
        }



        private static List<NominaModel.Item> ReadItems(List<string> oSegments)
        {
            var localCulture = new System.Globalization.CultureInfo("es-ES");
            List<NominaModel.Item> retval = new List<NominaModel.Item>();
            var iHeaderIdx = HeaderIdx(oSegments, Headers.Detall);
            var startIdx = iHeaderIdx + 1;
            for (int idx = startIdx; idx <= startIdx + 19; idx++)
            {
                var segment = oSegments[idx];
                string sCod = segment.Substring(23, 6);
                string sConcepte = segment.Substring(32, 36).Trim();
                if (sCod.IsNumeric())
                {
                    var cod = Convert.ToInt32(sCod);
                    NominaModel.Concepte oCod = new NominaModel.Concepte(cod, sConcepte);
                    var sQty = segment.Substring(0, 12);
                    string sPrice = segment.Substring(12, 11);
                    string sDevengo = segment.Substring(70, 11);
                    string sDeduccio = segment.Substring(84, 11);
                    NominaModel.Item oItem = new NominaModel.Item(oCod);
                    if (sQty.IsNumeric(localCulture))
                        oItem.Qty = Convert.ToInt32(decimal.Parse(sQty.Trim(), localCulture));
                    if (sPrice.IsNumeric(localCulture))
                        oItem.Price = decimal.Parse(sPrice.Trim(), localCulture);
                    if (sDevengo.IsNumeric(localCulture))
                        oItem.Devengo = decimal.Parse(sDevengo.Trim(), localCulture);
                    if (sDeduccio.IsNumeric(localCulture))
                        oItem.Deduccio = decimal.Parse(sDeduccio.Trim(), localCulture);

                    retval.Add(oItem);
                }
            };

            return retval;
        }


        public async Task<List<BancModel.Transfer>?> StaffPendingTransfers()
        {
            List<BancModel.Transfer>? transfers = await appstate.GetAsync<List<BancModel.Transfer>>("Staffs/transfers/pending");
            var user = session.User!;
            var emps = user.Emps;
            var retval = transfers?.Where(x => emps.Contains(x.Emp())).ToList();
            return retval;
        }

        public async Task UpdateAsync(List<NominaModel> nominas)
        {
            await nominasService.UpdateAsync(nominas);
        }



        #region ReadFields

        public enum SegmentNums
        {
            nifEmpresa = 4,
            segSocial = 12,
            totals = 36,
            fch = 40,
            liquid = 43
        }

        public enum Headers
        {
            Dades,
            Detall,
            Totals,
            Liquid
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
        private static DateTime ReadFch(List<string> segments)
        {
            DateTime retval = default(DateTime);
            if (segments.Count > (int)SegmentNums.fch)
            {
                var sFch = segments[(int)SegmentNums.fch].Trim();
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

                    var monthSegments = TextHelper.MatchingSegments(segments, sPattern);
                    if (monthSegments.All(x => x == monthSegments.First()))
                    {
                        sFch = monthSegments.First().Trim();
                        sMonth = sMonths.FirstOrDefault(x => sFch.Contains(x));
                    }
                    else
                        throw new Exception("no s'ha trobat la data del document");
                }
                var iMonth = sMonths.IndexOf(sMonth) + 1;
                var sYear = sFch.Substring(sFch.Length - 4, 4);
                var iYear = System.Convert.ToInt32(sYear);
                var iPos = sFch.IndexOf(" ");
                int iDay = Convert.ToInt32(sFch.Substring(0, iPos));
                retval = new DateTime(iYear, iMonth, iDay);
            }
            else
                throw new Exception("No es pot llegir la data del document." + Environment.NewLine + "Verificar que no es tracti de un paper escanejat");
            return retval;
        }

        private static string ReadEmpNif(List<string> segments)
        {
            string retval = "";
            var nifSegments = TextHelper.MatchingSegments(segments, "NIF.");
            if (segments.Count > 0)
                retval = nifSegments.First().Replace("NIF.", "").Trim().Split(' ').First();
            else
                throw new Exception("No s'ha trobat cap referencia al Nif de la empresa");
            return retval;
        }

        public Guid? ReadStaff(List<string> segments, EmpModel? emp)
        {
            Guid? retval = null/* TODO Change to default(_) if this is not a reference type */;
            var iHeaderIdx = HeaderIdx(segments, Headers.Dades);
            var segment = segments[iHeaderIdx + 1].Trim();
            var fields = segment.Split(' ');
            if (fields.Count() > 0)
            {
                var sSegSocNum = fields.First().Trim();
                if (string.IsNullOrEmpty(sSegSocNum))
                    throw new Exception("full de nómina sense num.de Seg.Social");
                else
                {
                    var staff = staffsService.GetValueByNumSegSocial(sSegSocNum, emp);
                    if (staff == null)
                        throw new Exception($"No s'ha trobat cap empleat amb el num.Seg.Social '{sSegSocNum}'");
                    else
                        retval = staff?.Guid;
                }
            }
            else
            {
                throw new Exception("full de nómina sense dades");
            }
            return retval;
        }

        public static Decimal? ReadDevengat(List<string> segments)
        {
            var segmentTotals = ReadTotals(segments);
            var fields = segmentTotals.SplitByLength(14);
            string field = fields[(int)Totals.TotalDevengado].Trim();
            var retval = field.ParseDecimal();
            return retval;
        }

        public static Decimal? ReadLiquid(List<string> segments)
        {
            var oCaptionsSegmentIdx = HeaderIdx(segments, Headers.Liquid);
            var segment = segments[oCaptionsSegmentIdx + 1].Trim();
            var retval = segment.ParseDecimal();
            return retval;
        }

        public static Decimal? ReadIrpfBase(List<string> segments)
        {
            var segmentTotals = ReadTotals(segments);
            var fields = segmentTotals.SplitByLength(14);
            string field = fields[(int)Totals.BaseIrpf].Trim();
            var retval = field.ParseDecimal();
            return retval;
        }



        public static string ReadTotals(List<string> segments)
        {
            var iHeaderIdx = HeaderIdx(segments, Headers.Totals);
            return segments[iHeaderIdx + 1];
        }




        public static int HeaderIdx(List<string> oSegments, Headers Header)
        {
            // Busca l'index del segment a partir del qual trobar les dades
            var sHeaderCaption = HeaderCaption(Header);
            var oMatchingSegments = TextHelper.MatchingSegments(oSegments, sHeaderCaption);
            switch (oMatchingSegments.Count)
            {
                case 0:
                    throw new Exception("no s'ha trobat la capçalera");
                case 1:
                    return oSegments.IndexOf(oMatchingSegments.First());
                default:
                    throw new Exception("s'han trobat " + oMatchingSegments.Count + " capçaleres duplicades");
            }
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


        #endregion

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            empsService.OnChange -= NotifyChange;
            staffsService.OnChange -= NotifyChange;
            nominasService.OnChange -= NotifyChange;
            pgcCtasService.OnChange -= NotifyChange;
            ibansService.OnChange -= NotifyChange;

        }
    }
}
