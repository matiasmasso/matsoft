using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO
{
    public class IbanStructureModel
    {
        public string? CountryISO { get; set; }
        public Guid Country { get; set; }

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


        public IbanStructureModel() : base() { }
        public IbanStructureModel(string countryISO) { 
        this.CountryISO = countryISO;
        }

        public string GetBankId(string? ccc)
        {
            string retval = "";
            var cleanCcc = CleanCcc(ccc);
            if (!string.IsNullOrEmpty(cleanCcc) && cleanCcc.Length >= BankPosition+BankLength)
                retval = cleanCcc.Substring(BankPosition, BankLength);
            return retval;
        }

        public string GetBranchId(string? ccc)
        {
            string retval = "";
            var cleanCcc = CleanCcc(ccc);
            if (!string.IsNullOrEmpty(cleanCcc) && cleanCcc.Length >= BranchPosition + BranchLength)
                retval = cleanCcc.Substring(BranchPosition, BranchLength);
            return retval;
        }

        public static string CleanCcc(string? src)
        {
            var regex = new Regex("[^A-Za-z0-9]");
            var retval = regex.Replace(src ?? "", "").ToUpper();
            return retval;
        }

        public static string IbanDigitsFromEspCcc(string NumeroCuenta)
        {
            string retval = CleanCcc(NumeroCuenta);

            NumeroCuenta = retval;
            if (NumeroCuenta.Length == 20 & NumeroCuenta.IsNumeric())
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

                    ParteCuenta = string.Format("{0:00}", Double.Parse(ParteCuenta) % 97);
                }

                retval = "ES" + string.Format("{0:00}", 98 - Double.Parse(ParteCuenta));
            }

            return retval;
        }

        public  string GetCountryISO(string sIbanDigits)
        {
            string src = CleanCcc(sIbanDigits);
            if (src.IsNumeric() && src.Length == 20)
                src = IbanDigitsFromEspCcc(src);
            string retval = "";
            if (src.Length >= 2 & !src.IsNumeric())
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
