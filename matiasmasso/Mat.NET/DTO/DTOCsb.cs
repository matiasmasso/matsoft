using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCsb : DTOBaseGuid
    {
        public DTOCsa Csa { get; set; }
        public int Id { get; set; }
        public DTOContact Contact { get; set; }
        public string Txt { get; set; }
        public DTOAmt Amt { get; set; }
        public DateTime Vto { get; set; }
        public DTOIban Iban { get; set; }
        public string FraNum { get; set; }
        public int FraYea { get; set; }

        // Property Reclamat As Boolean
        // Property Impagat As Boolean

        public Results Result { get; set; }
        public DTOCca ResultCca { get; set; }

        public DTOInvoice Invoice { get; set; }
        public DTOPnd Pnd { get; set; }
        public TiposAdeudo SepaTipoAdeudo { get; set; }

        public ExceptionCodes ExceptionCode { get; set; }

        public DTOAmt Despesa { get; set; }

        public List<DTOMailingLog> MailingLogs { get; set; }

        public enum TiposAdeudo
        {
            NotSet,
            FRST,
            RCUR,
            FNAL,
            OOFF
        }

        public enum ExceptionCodes
        {
            Success,
            NoValue,
            Negative,
            NoIban,
            NoBn2,
            NoBn1,
            WrongBIC,
            NoMandate,
            NoMandateFch
        }

        public enum Results
        {
            Pendent,
            Vençut,
            Reclamat,
            Impagat
        }

        public DTOCsb() : base()
        {
            this.MailingLogs = new List<DTOMailingLog>();
        }

        public DTOCsb(Guid oGuid) : base(oGuid)
        {
            this.MailingLogs = new List<DTOMailingLog>();
        }

        public string FormattedId()
        {
            string retval = string.Format("{0}{1:000}", this.Csa.formattedId(), this.Id);
            return retval;
        }

        public string ReadableFormat()
        {
            string retval = string.Format("{0}.{1}", this.Csa.readableFormat(), this.Id);
            return retval;
        }

        public static DTOCountry Country(DTOCsb oCsb)
        {
            DTOCountry retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oCsb != null)
            {
                if (oCsb.Iban != null)
                {
                    if (oCsb.Iban.BankBranch != null)
                    {
                        if (oCsb.Iban.BankBranch.Bank != null)
                            retval = oCsb.Iban.BankBranch.Bank.Country;
                    }
                }
            }
            return retval;
        }

        public static bool validate(List<DTOCsb> oCsbs, DTOCsa.FileFormats oFormat)
        {
            bool retval = true;
            foreach (DTOCsb item in oCsbs)
            {
                if (!DTOCsb.validate(item, oFormat))
                    retval = false;
            }
            return retval;
        }

        public static bool validate(DTOCsb oCsb, DTOCsa.FileFormats oFormat)
        {
            // If oCsb.Iban.Digits.StartsWith("AD55") Then Stop
            {
                var withBlock = oCsb;
                withBlock.ExceptionCode = DTOCsb.ExceptionCodes.Success;
                if (withBlock.Amt.Eur == 0)
                    withBlock.ExceptionCode = DTOCsb.ExceptionCodes.NoValue;
                else if (withBlock.Amt.Eur < 0)
                    withBlock.ExceptionCode = DTOCsb.ExceptionCodes.Negative;
                else if (withBlock.Iban == null)
                    withBlock.ExceptionCode = DTOCsb.ExceptionCodes.NoIban;
                else if (withBlock.Iban.BankBranch == null)
                    withBlock.ExceptionCode = DTOCsb.ExceptionCodes.NoBn1;
                else if (withBlock.Iban.BankBranch.Bank == null)
                    withBlock.ExceptionCode = DTOCsb.ExceptionCodes.NoBn2;
                else if (!DTOBank.validateBIC(withBlock.Iban.BankBranch.Bank.Swift))
                    withBlock.ExceptionCode = DTOCsb.ExceptionCodes.WrongBIC;
                else if (oFormat != DTOCsa.FileFormats.normaAndorrana & oFormat != DTOCsa.FileFormats.remesesExportacioLaCaixa & withBlock.Iban.FchFrom == null/* TODO Change to default(_) if this is not a reference type */ )
                    withBlock.ExceptionCode = DTOCsb.ExceptionCodes.NoMandateFch;
                else if (oFormat != DTOCsa.FileFormats.normaAndorrana & oFormat != DTOCsa.FileFormats.remesesExportacioLaCaixa & withBlock.Iban.DocFile == null)
                    withBlock.ExceptionCode = DTOCsb.ExceptionCodes.NoMandate;
            }
            bool retval = oCsb.ExceptionCode == DTOCsb.ExceptionCodes.Success;
            return retval;
        }

        public static string ValidationText(DTOCsb.ExceptionCodes oCode)
        {
            string retval = "";
            switch (oCode)
            {
                case DTOCsb.ExceptionCodes.NoValue:
                    {
                        retval = "sense import";
                        break;
                    }

                case DTOCsb.ExceptionCodes.Negative:
                    {
                        retval = "import negatiu";
                        break;
                    }

                case DTOCsb.ExceptionCodes.NoIban:
                    {
                        retval = "sense Iban vigent";
                        break;
                    }

                case DTOCsb.ExceptionCodes.NoBn1:
                    {
                        retval = "entitat bancaria no registrada";
                        break;
                    }

                case DTOCsb.ExceptionCodes.NoBn2:
                    {
                        retval = "oficina bancaria no registrada";
                        break;
                    }

                case DTOCsb.ExceptionCodes.WrongBIC:
                    {
                        retval = "BIC incorrecte a entitat bancaria";
                        break;
                    }

                case DTOCsb.ExceptionCodes.NoMandateFch:
                    {
                        retval = "mandat sense data";
                        break;
                    }

                case DTOCsb.ExceptionCodes.NoMandate:
                    {
                        retval = "sense document de mandat";
                        break;
                    }
            }
            return retval;
        }


        public static DTOAmt TotalNominal(List<DTOCsb> oCsbs)
        {
            decimal DcEur = oCsbs.Sum(x => x.Amt.Eur);
            DTOAmt retval = DTOAmt.Factory(DcEur);
            return retval;
        }

        public static DTOAmt TotalDespeses(List<DTOCsb> oCsbs, DTOBancTerm oTerm)
        {
            var retval = DTOAmt.Empty();
            foreach (DTOCsb oCsb in oCsbs)
                retval.Add(DTOBancTerm.cost(oCsb, oTerm));
            return retval;
        }
    }
}
