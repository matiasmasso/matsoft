using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCsa : DTOBaseGuid
    {
        public DTOEmp emp { get; set; }
        public int id { get; set; }
        public DateTime fch { get; set; }
        public DTOBanc banc { get; set; }
        public string @ref { get; set; }
        public List<DTOCsb> items { get; set; }

        public bool enabled { get; set; }
        public DTOBancTerm term { get; set; }
        public DTOAmt nominal { get; set; }
        public DTOAmt nominalMinim { get; set; }
        public DTOAmt nominalMaxim { get; set; }
        public DTOAmt classificacio { get; set; }
        public DTOAmt disponible { get; set; }
        public string condicions { get; set; }
        public FileFormats fileFormat { get; set; }
        public DTOAmt despeses { get; set; }
        public int efectes { get; set; }
        public int dias { get; set; }
        public decimal importMig { get; set; }
        public decimal tae { get; set; }
        public DateTime minVto { get; set; }
        public DateTime maxVto { get; set; }

        public DTOCca cca { get; set; }

        public bool descomptat { get; set; }
        public bool andorra { get; set; }


        public enum Types
        {
            alCobro,
            alDescompte
        }

        public enum FileFormats
        {
            notSet,
            norma58,
            norma19,
            remesesExportacioLaCaixa,
            normaAndorrana,
            sepaB2b,
            sepaCore
        }


        public DTOCsa() : base()
        {
            items = new List<DTOCsb>();
        }

        public DTOCsa(Guid oGuid) : base(oGuid)
        {
            items = new List<DTOCsb>();
        }


        public static DTOCsa Factory(DTOEmp oEmp, DTOBanc oBanc = null/* TODO Change to default(_) if this is not a reference type */, DTOCsa.FileFormats oFileFormat = DTOCsa.FileFormats.notSet, bool Descomptat = false)
        {
            DTOCsa retval = new DTOCsa();
            {
                var withBlock = retval;
                withBlock.emp = oEmp;
                withBlock.banc = oBanc;
                withBlock.fch = DTO.GlobalVariables.Today();
                withBlock.fileFormat = oFileFormat;
                withBlock.descomptat = Descomptat;
            }
            return retval;
        }

        public string formattedId()
        {
            string retval = string.Format("{0:0000}{1:000}", fch.Year, id);
            return retval;
        }

        public string readableFormat()
        {
            string retval = string.Format("{0}.{1}", fch.Year, id);
            return retval;
        }

        public static DTOCountry country(DTOCsa oCsa)
        {
            DTOCountry retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oCsa != null && oCsa.items.Count > 0)
            {
                DTOCsb oFirstCsb = oCsa.items.First();
                retval = DTOCsb.Country(oFirstCsb);
            }
            return retval;
        }


        public string filename()
        {
            string retval = "";
            switch (fileFormat)
            {
                case DTOCsa.FileFormats.remesesExportacioLaCaixa:
                    {
                        retval = string.Format("remesa {0:0000}.{1:0000} export a {2}.txt", fch.Year, id, DTOBank.NomComercialORaoSocial(banc.Iban.BankBranch.Bank));
                        break;
                    }

                case DTOCsa.FileFormats.sepaCore:
                    {
                        retval = string.Format("remesa {0:0000}.{1:0000} Sepa Core a {2}.xml", fch.Year, id, DTOBank.NomComercialORaoSocial(banc.Iban.BankBranch.Bank));
                        break;
                    }
            }
            return retval;
        }

        public static DTOAmt totalNominal(DTOCsa oCsa)
        {
            var retval = DTOAmt.Empty();
            foreach (DTOCsb oCsb in oCsa.items)
                retval.Add(oCsb.Amt);
            return retval;
        }

        public static DTOAmt totalDespeses(DTOCsa oCsa)
        {
            DTOAmt retval = DTOCsb.TotalDespeses(oCsa.items, oCsa.term);
            return retval;
        }

        public static decimal getTAE(DTOCsa oCsa)
        {
            decimal iDias = diasVencimientoMedioPonderado(oCsa);
            DTOAmt oDespeses = totalDespeses(oCsa);
            DTOAmt oNominal = totalNominal(oCsa);

            decimal retval = 0;
            if (oNominal.Eur > 0)
                retval = 360 * oDespeses.Eur / (oNominal.Eur * iDias);
            return retval;
        }

        public static decimal diasVencimientoMedioPonderado(DTOCsa oCsa)
        {
            decimal retval = 0;
            decimal DcSumaDeNominalesPorVencimientos = 0;
            decimal DcTotalNominal = totalNominal(oCsa).Eur;
            foreach (DTOCsb oCsb in oCsa.items)
            {
                int iDias = TimeHelper.daysdiff(oCsa.fch, oCsb.Vto);
                DcSumaDeNominalesPorVencimientos += oCsb.Amt.Eur * iDias;
            }
            if (DcTotalNominal > 0)
                retval = DcSumaDeNominalesPorVencimientos / DcTotalNominal;
            return retval;
        }

        public static string sepaFormat(DTOAmt oAmt)
        {
            string retval = sepaFormat(oAmt.Eur);
            return retval;
        }

        public static string sepaFormat(decimal DcEur)
        {
            string retval = DcEur.ToString("F2").Replace(",", ".");
            return retval;
        }

        public static string sepaFormat(DateTime DtFch)
        {
            string retval = VbUtilities.Format(DtFch, "yyyy-MM-dd");
            return retval;
        }

        public static string sepaNormalized(string src)
        {
            char[] validChars = DTOCsa.sepaAllowedChars();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (char oChar in src.ToArray())
            {
                if (validChars.Contains(oChar))
                    sb.Append(oChar);
                else
                {
                    int iHex32 = Convert.ToInt32(oChar);
                    string HexValue = iHex32.ToString("X4");
                    string sAlt = string.Format(@"\u{0}", HexValue);
                    sb.Append(sAlt);
                }
            }
            string retval = sb.ToString();
            return retval;
        }

        public static char[] sepaAllowedChars()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("abcdefghijklmnopqrstuvwxyz");
            sb.AppendLine("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            sb.AppendLine("0123456789");
            sb.AppendLine("/-?:().,'+ ");
            string src = sb.ToString();
            char[] retval = src.ToArray();
            return retval;
        }



        public string sepaMsgId()
        {
            string retval = string.Format("{0}{1}{2:yyyyMMddhhmmss}", this.descomptat ? "FSDD" : "", this.formattedId(), DTO.GlobalVariables.Now());
            return retval;
        }

        public string sepaIdentificacioPresentador()
        {
            string retval = banc.SepaCoreIdentificador; // sb.ToString
            return retval;
        }

        public string sepaFormatSuma()
        {
            decimal DcEur = items.Sum(x => x.Amt.Eur);
            string retval = DTOCsa.sepaFormat(DcEur);
            return retval;
        }

        public static bool validateSepaCore(DTOCsa oCsa, ref List<Exception> exs)
        {

            // check venciments antics
            int iCount = 0;
            foreach (DTOCsb oCsb in oCsa.items)
            {
                if (oCsb.Vto < DTO.GlobalVariables.Today())
                    iCount += 1;
            }
            if (iCount > 0)
            {
            }

            foreach (DTOCsb item in oCsa.items)
            {
                DTOIban oIban = item.Iban;
                if (oIban == null)
                    exs.Add(new Exception(item.Contact.Nom + " no té compte corrent"));
                else
                {
                    if (oIban.Digits == "")
                        exs.Add(new Exception(item.Contact.Nom + " sense compte corrent"));
                    if (oIban.IsActive())
                    {
                        if (oIban.BankBranch == null)
                            exs.Add(new Exception("bank no registrat al compte de " + item.Contact.Nom));
                        else
                        {
                            DTOBankBranch oBankBranch = oIban.BankBranch;
                            if (oBankBranch.Bank == null)
                                exs.Add(new Exception("entitat bancaria no registrada al compte " + DTOIban.Formated(oIban)));
                            else
                            {
                                DTOBank oBank = oBankBranch.Bank;
                                if (oBank.Swift == "")
                                    exs.Add(new Exception("falta codi BIC al banc " + DTOBank.NomComercialORaoSocial(oBank)));
                                else if (!DTOBank.validateBIC(oBank.Swift))
                                    exs.Add(new Exception("codi BIC '" + oBank.Swift + "' erroni al banc " + DTOBank.NomComercialORaoSocial(oBank)));
                            }
                        }
                    }
                    else
                        exs.Add(new Exception("falta mandato de " + item.Contact.Nom));
                }
            }

            bool retval = exs.Count == 0;
            return retval;
        }
    }
}
