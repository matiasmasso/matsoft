using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DTOIban : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public Cods Cod { get; set; }
        public DTOBankBranch BankBranch { get; set; }
        public string Digits { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public DTOContact Titular { get; set; }
        public string PersonNom { get; set; }
        public string PersonDni { get; set; }
        public Formats Format { get; set; }
        public DTODocFile DocFile { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime FchDownloaded { get; set; }
        public DTOUser UsrDownloaded { get; set; }
        public DateTime FchUploaded { get; set; }
        public DTOUser UsrUploaded { get; set; }
        public DateTime FchApproved { get; set; }
        public DTOUser UsrApproved { get; set; }

        public Structure IbanStructure { get; set; }

        public enum Cods
        {
            _NotSet,
            proveidor,
            client,
            staff,
            banc
        }

        public enum Formats
        {
            notSet,
            noValid,
            SEPAB2B,
            SEPACore,
            Q58
        }

        public enum StatusEnum
        {
            all,
            pendingDownload,
            pendingUpload,
            pendingApproval,
            downloaded,
            uploaded,
            approved,
            denied
        }

        public enum Exceptions
        {
            success,
            missingBankBranch,
            missingBankNom,
            missingBIC,
            missingBranchAddress,
            missingBranchLocation,
            wrongDigits,
            missingMandateFch,
            missingMandateDocument
        }

        public DTOIban() : base()
        {
        }

        public DTOIban(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOIban Factory(string sDigits)
        {
            DTOIban retval = new DTOIban();
            {
                var withBlock = retval;
                withBlock.Digits = DTOIban.CleanCcc(sDigits);
            }
            return retval;
        }

        public static DTOIban Factory(DTOEmp oEmp, DTOContact oTitular, DTOIban.Cods oCod)
        {
            DTOIban retval = new DTOIban();
            {
                var withBlock = retval;
                withBlock.Titular = oTitular;
                withBlock.FchFrom = DTO.GlobalVariables.Today();
                withBlock.Format = DTOIban.Formats.SEPACore;
                withBlock.Cod = oCod;
            }
            return retval;
        }

        public static string BranchLocationAndAdr(DTOIban oIban)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oIban.BankBranch != null)
            {
                if (oIban.BankBranch.Location != null)
                {
                    sb.Append(oIban.BankBranch.Location.Nom);
                    sb.Append(" - ");
                    sb.Append(oIban.BankBranch.Address);
                }
            }
            string retval = sb.ToString();
            return retval;
        }


        public bool IsActive()
        {
            bool retval = this.FchFrom <= DTO.GlobalVariables.Today();
            if (this.FchTo > default(DateTime))
            {
                if (this.FchTo < DTO.GlobalVariables.Today())
                    retval = false;
            }
            return retval;
        }

        public bool IsSepa()
        {
            bool retval = false;
            if (this.BankBranch != null)
            {
                if (this.BankBranch.Bank != null)
                    retval = this.BankBranch.Bank.isSepa();
            }
            return retval;
        }

        public static bool IsMissingMandato(DTOIban oIban)
        {
            bool retval = true;
            if (oIban != null)
            {
                if (oIban.DocFile != null)
                    retval = false;
            }
            return retval;
        }

        public static DTOBank Bank(DTOIban oIban)
        {
            DTOBank retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oIban != null)
            {
                if (oIban.BankBranch != null)
                    retval = oIban.BankBranch.Bank;
            }
            return retval;
        }


        public static string BankNom(DTOIban oIban)
        {
            string retval = "";
            DTOBank oBank = DTOIban.Bank(oIban);
            if (oBank != null)
                retval = DTOBank.NomComercialORaoSocial(oBank);
            return retval;
        }

        public static string Swift(DTOIban oIban)
        {
            string retval = "";
            if (oIban.BankBranch == null)
            {
                var oBank = DTOIban.Bank(oIban);
                if (oBank != null)
                    retval = oBank.Swift;
            }
            else
            {
                List<Exception> exs = new List<Exception>();
                retval = DTOIban.Swift(oIban.BankBranch, exs);
            }
            return retval;
        }

        public static string Swift(DTOBankBranch oBranch, List<Exception> exs)
        {
            string retval = "";
            if (oBranch != null)
            {
                retval = oBranch.Swift;
                if (string.IsNullOrEmpty(retval))
                {
                    if (oBranch.Bank != null)
                        retval = oBranch.Bank.Swift;
                }
            }
            return retval;
        }

        public static string Formated(DTOIban oIban)
        {
            string retval = "";
            if (oIban != null)
                retval = Formated(oIban.Digits);
            return retval;
        }

        public static string Formated(string src)
        {
            string cleanString = DTOIban.CleanCcc(src);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int iPos = 0;
            while (iPos < cleanString.Length) // -1
            {
                string tmp = cleanString.Substring(iPos);
                if (sb.Length > 0)
                    sb.Append(".");
                sb.Append(VbUtilities.Left(tmp, 4));
                iPos += 4;
            }
            string retval = sb.ToString();
            return retval;
        }

        public static string CleanCcc(string src)
        {
            string retval = TextHelper.RegexSuppress(src, "[^A-Za-z0-9]").ToUpper();
            return retval;
        }

        public static List<string> SegmentedDigits(DTOIban oIban)
        {
            // per omplir les caselles de 4 digits de la web
            List<string> retval = new List<string>();
            if (oIban != null)
            {
                string src = oIban.Digits;
                int segmentLength = 4;
                retval = src.splitInParts(segmentLength).ToList();
            }
            return retval;
        }

        public static string BranchId(DTOIban oIban)
        {
            string retval = "";
            if (oIban != null)
            {
                if (oIban.BankBranch != null)
                    retval = oIban.BankBranch.Id;
            }
            return retval;
        }


        public static bool ValidateDigits(string bankAccount)
        {
            bool retval = false;
            bankAccount = TextHelper.RegexSuppress(bankAccount, "[^A-Za-z0-9]").ToUpper();
            if (bankAccount.Length > 8)
            {
                string bank = bankAccount.Substring(4, bankAccount.Length - 4) + bankAccount.Substring(0, 4);
                int asciiShift = 55;
                StringBuilder sb = new StringBuilder();
                foreach (char c in bank)
                {
                    int v;
                    if (Char.IsLetter(c)) v = c - asciiShift;
                    else v = int.Parse(c.ToString());
                    sb.Append(v);
                }
                string checkSumString = sb.ToString();
                int checksum = int.Parse(checkSumString.Substring(0, 1));
                for (int i = 1; i < checkSumString.Length; i++)
                {
                    int v = int.Parse(checkSumString.Substring(i, 1));
                    checksum *= 10;
                    checksum += v;
                    checksum %= 97;
                }
                retval = checksum == 1;
            }
            return retval;
        }

        public bool Validate(List<DTOIban.Exceptions> exs)
        {
            if (ValidateDigits(this.Digits))
            {
                if (this.BankBranch == null)
                    exs.Add(DTOIban.Exceptions.missingBankBranch);
                else
                {
                    DTOBankBranch oBranch = this.BankBranch;
                    if (string.IsNullOrEmpty(oBranch.Address))
                        exs.Add(DTOIban.Exceptions.missingBranchAddress);
                    if (oBranch.Location == null)
                        exs.Add(DTOIban.Exceptions.missingBranchLocation);
                    if (oBranch.Bank == null)
                        exs.Add(DTOIban.Exceptions.missingBankNom);
                    else
                    {
                        if (string.IsNullOrEmpty(oBranch.Bank.NomComercial) && string.IsNullOrEmpty(oBranch.Bank.RaoSocial))
                            exs.Add(DTOIban.Exceptions.missingBankNom);
                        if (string.IsNullOrEmpty(oBranch.Bank.Swift))
                            exs.Add(DTOIban.Exceptions.missingBIC);
                    }
                }

                if (this.Cod == DTOIban.Cods.client)
                {
                    if (this.FchFrom == null)
                        exs.Add(DTOIban.Exceptions.missingMandateFch);
                    if (this.DocFile == null)
                        exs.Add(DTOIban.Exceptions.missingMandateDocument);
                }
            }
            else
                exs.Add(DTOIban.Exceptions.wrongDigits);

            return (exs.Count == 0);
        }

        public class Structure
        {
            public DTOCountry Country { get; set; }

            public int BankPosition { get; set; }
            public int BankLength { get; set; }
            public Formats BankFormat { get; set; }

            public int BranchPosition { get; set; }
            public int BranchLength { get; set; }
            public Formats BranchFormat { get; set; }

            public int CheckDigitsPosition { get; set; }
            public int CheckDigitsLength { get; set; }
            public Formats CheckDigitsFormat { get; set; }

            public int AccountPosition { get; set; }
            public int AccountLength { get; set; }
            public Formats AccountFormat { get; set; }


            public bool IsNew { get; set; }
            public bool IsLoaded { get; set; }

            public enum Formats
            {
                Numeric,
                Alfanumeric
            }

            public static Structure Factory(DTOCountry oCountry)
            {
                Structure retval = new Structure();
                {
                    var withBlock = retval;
                    withBlock.Country = oCountry;
                }
                return retval;
            }

            public string GetBankId(string sDigits)
            {
                string retval = "";
                if (sDigits.isNotEmpty())
                {
                    string sCleanString = CleanCcc(sDigits);
                    retval = sCleanString.Substring(BankPosition, BankLength);
                }
                return retval;
            }

            public string GetBranchId(string sDigits)
            {
                string retval = "";
                if (sDigits.isNotEmpty())
                {
                    string sCleanString = CleanCcc(sDigits);
                    retval = sCleanString.Substring(BranchPosition, BranchLength);
                }
                return retval;
            }

            public static string BankDigits(Structure oStructure, string sIBANDigits)
            {
                int iPos = oStructure.BankPosition;
                int iLen = oStructure.BankLength;
                string retval = "";
                if (sIBANDigits.Length >= iPos + iLen)
                    retval = sIBANDigits.Substring(iPos, iLen);
                return retval;
            }

            public static string BranchDigits(Structure oStructure, string sIBANDigits)
            {
                int iPos = oStructure.BranchPosition;
                int iLen = oStructure.BranchLength;
                string retval = "";
                if (sIBANDigits.Length >= iPos + iLen)
                    retval = sIBANDigits.Substring(iPos, iLen);
                return retval;
            }

            public static string CleanCcc(string src)
            {
                string retval = TextHelper.RegexSuppress(src, "[^A-Za-z0-9]").ToUpper();
                return retval;
            }

            public static string IbanDigitsFromEspCcc(string NumeroCuenta)
            {
                string retval = DTOIban.CleanCcc(NumeroCuenta);

                NumeroCuenta = DTOIban.CleanCcc(NumeroCuenta);
                if (NumeroCuenta.Length == 20 & VbUtilities.isNumeric(NumeroCuenta))
                {
                    string ParteCuenta;
                    int ProximosNumeros;

                    // Módulo de los primeros 9 digitos
                    ParteCuenta = string.Format("{0:00}", System.Convert.ToInt32(NumeroCuenta.Substring(0, 9)) % 97);

                    // Cogemos otro grupo de digitos de la cuenta
                    NumeroCuenta = NumeroCuenta.Substring(9, NumeroCuenta.Length - 9);

                    // Recorremos la cuenta hasta el final
                    while (NumeroCuenta != "")
                    {
                        if (System.Convert.ToInt32(ParteCuenta) < 10)
                            ProximosNumeros = 8;
                        else
                            ProximosNumeros = 7;

                        if (NumeroCuenta.Length < ProximosNumeros)
                        {
                            ParteCuenta = ParteCuenta + NumeroCuenta;
                            NumeroCuenta = "";
                        }
                        else
                        {
                            ParteCuenta = ParteCuenta + NumeroCuenta.Substring(0, ProximosNumeros);
                            NumeroCuenta = NumeroCuenta.Substring(ProximosNumeros, NumeroCuenta.Length - ProximosNumeros);
                        }

                        ParteCuenta = string.Format("{0:00}", ParteCuenta.toBigInteger() % 97);
                    }

                    retval = "ES" + string.Format("{0:00}", 98 - ParteCuenta.toBigInteger());
                }

                return retval;
            }

            public static string GetCountryISO(string sIbanDigits)
            {
                string src = DTOIban.CleanCcc(sIbanDigits);
                if (VbUtilities.isNumeric(src) & src.Length == 20)
                    src = Structure.IbanDigitsFromEspCcc(src);
                string retval = "";
                if (src.Length >= 2 & !VbUtilities.isNumeric(src))
                    retval = src.Substring(0, 2);
                return retval;
            }

            public int OverallLength()
            {
                int retval = 4 + BankLength + BranchLength + CheckDigitsLength + AccountLength;
                return retval;
            }
        }


    }
}
