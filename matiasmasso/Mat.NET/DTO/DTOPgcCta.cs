using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOPgcCta : DTOPgcEpgBase
    {
        public string Id { get; set; }
        public DTOPgcPlan Plan { get; set; }
        public string Dsc { get; set; }
        public Acts Act { get; set; }
        public bool IsBaseImponibleIva { get; set; }
        public bool IsQuotaIva { get; set; }
        public bool IsQuotaIrpf { get; set; }
        public DTOPgcCta NextCta { get; set; }
        // Property Epg As DTOPgcEpgBase 'TO DEPRECATE
        public DTOPgcClass PgcClass { get; set; }
        public List<DTOYearMonth> YearMonths { get; set; }


        public DTOPgcPlan.Ctas Codi { get; set; }

        public enum Acts
        {
            NotSet,
            Deutora,
            Creditora
        }

        public enum Bals
        {
            NotSet,
            Balance,
            Explotacion
        }

        public enum Digits
        {
            Digits3,
            Digits4,
            Digits5,
            Full
        }


        public DTOPgcCta() : base()
        {
        }

        public DTOPgcCta(Guid oGuid) : base(oGuid)
        {
        }

        public static string FullNom(DTOPgcCta oCta, DTOLang oLang)
        {
            string retval = "";
            if (oCta != null)
                retval = string.Format("{0} {1}", oCta.Id, oCta.Nom.Tradueix(oLang));
            return retval;
        }

        public static DTOPgcCta FromCodi(List<DTOPgcCta> oCtas, DTOPgcPlan.Ctas oCodi, List<Exception> exs) // TO DEPRECATE
        {
            var retval = oCtas.FirstOrDefault(x => x.Codi == oCodi);
            if (retval == null)
                exs.Add(new Exception(string.Format("No s'ha trobat cap compte amb el codi {0}", oCodi.ToString())));
            return retval;
        }

        public static DTOPgcCta FromCodi(List<DTOPgcCta> oCtas, DTOPgcPlan.Ctas oCodi)
        {
            return oCtas.FirstOrDefault(x => x.Codi == oCodi);
        }

        public decimal YearMonthValue(DTOYearMonth oYearMonth)
        {
            decimal retval = 0;
            if (oYearMonth != null)
            {
                if (YearMonths != null)
                {
                    var pYearMonth = YearMonths.FirstOrDefault(x => x.Equals(oYearMonth));
                    if (pYearMonth != null)
                        retval = pYearMonth.Eur;
                }
            }
            return retval;
        }

        // Shared Function Nom(oCta As DTOPgcCta, oLang As DTOLang) As String
        // Dim retval As String = ""
        // If oCta IsNot Nothing Then
        // retval = oLang.Tradueix(oCta.NomEsp, oCta.NomCat, oCta.NomEng)
        // End If
        // Return retval
        // End Function

        public static string formatAccountId(DTOPgcCta oCta, DTOContact oContact)
        {
            int ContactId = 0;
            if (oContact != null)
                ContactId = oContact.Id;
            var retval = formatAccountId(oCta.Id, ContactId);
            return retval;
        }

        public static string formatAccountId(string sCtaId, int oContactId)
        {
            string retval = string.Format("{0}{1:00000}", sCtaId, oContactId);
            return retval;
        }

        public static string formatAccountDsc(DTOPgcCta oCta, DTOContact oContact, DTOLang oLang)
        {
            var sCtaNom = oCta.Nom.Tradueix(oLang);
            string sContactNom = "";
            if (oContact != null)
            {
                if (oContact.Nom == "")
                    sContactNom = oContact.FullNom;
                else
                    sContactNom = oContact.Nom;
            }
            string retval = FormatAccountDsc(sCtaNom, sContactNom);
            return retval;
        }

        public static string FormatAccountDsc(string sCtaNom, string sContactNom)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(sCtaNom);
            sb.Append(" " + sContactNom);
            string retval = sb.ToString();
            return retval;
        }

        public static DTOPgcCta.Bals Bal(DTOPgcCta value)
        {
            DTOPgcCta.Bals retval = DTOPgcCta.Bals.NotSet;
            if (value.Id.isGreaterThan("6"))
                retval = DTOPgcCta.Bals.Explotacion;
            else
                retval = DTOPgcCta.Bals.Balance;
            return retval;
        }

        public static decimal Saldo(decimal DcSaldoAnterior, DTOCcb oCcb)
        {
            decimal retval = DcSaldoAnterior;
            if ((int)oCcb.Cta.Act == (int)oCcb.Dh)
                retval += oCcb.Amt.Eur;
            else
                retval -= oCcb.Amt.Eur;
            return retval;
        }

        public static bool isBaseIrpf(ref DTOPgcCta oCta)
        {
            bool retval = false;
            if ((oCta.Id.StartsWith("6") | oCta.Id.StartsWith("2")))
            {
                if (!oCta.Id.StartsWith("643") & oCta.Codi != DTOPgcPlan.Ctas.Dietas)
                    retval = true;
            }
            return retval;
        }

        public static DTOPgcPlan.Ctas getCtaProveedors(DTOCur oCur)
        {
            DTOPgcPlan.Ctas retval;
            switch (oCur.Tag)
            {
                case "USD":
                    {
                        retval = DTOPgcPlan.Ctas.ProveidorsUsd;
                        break;
                    }

                case "GBP":
                    {
                        retval = DTOPgcPlan.Ctas.ProveidorsGbp;
                        break;
                    }

                default:
                    {
                        retval = DTOPgcPlan.Ctas.ProveidorsEur;
                        break;
                    }
            }
            return retval;
        }

        public static bool IsExplotacio(DTOPgcCta oCta)
        {
            bool retval = oCta.Id.isGreaterOrEqualThan("6");
            return retval;
        }

        public static bool IsActivable(DTOPgcCta oCta)
        {
            return oCta.Id.StartsWith("2");
        }

        public string Digits3
        {
            get
            {
                string retval = VbUtilities.Left(Id, 3);
                return retval;
            }
        }

        public string Digits4
        {
            get
            {
                string retval = VbUtilities.Left(Id, 4);
                return retval;
            }
        }

        public class Sdo
        {
            public DateTime Fch { get; set; }
            public decimal Eur { get; set; }
        }

        public class Collection : List<DTOPgcCta>
        {
            public static Collection Factory(List<DTOPgcCta> src)
            {
                Collection retval = new Collection();
                retval.AddRange(src);
                return retval;
            }

            public DTOPgcCta Cta(DTOPgcPlan.Ctas codi)
            {
                DTOPgcCta retval = this.FirstOrDefault(x => x.Codi == codi);
                return retval;
            }
        }
    }
}
