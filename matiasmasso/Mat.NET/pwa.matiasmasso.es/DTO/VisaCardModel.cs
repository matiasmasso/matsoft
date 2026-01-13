using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO
{
    public class VisaCardModel:BaseGuid
    {
        public EmpModel.EmpIds Emp {  get; set; }
        public Guid? Emisor { get; set; }
        public Guid? Banc { get; set; }
        public Guid? Titular {  get; set; }
        public string? Nom {  get; set; }
        public string? Digits { get; set; }
        public string? Caduca{ get; set; }
        public DateOnly? FchTo { get; set; }


        public VisaCardModel() { }
        public VisaCardModel(Guid guid):base(guid) { }

        public string? FormatedDigits() => string.IsNullOrEmpty(Digits) ? null : Regex.Replace(Digits, ".{4}", "$0 ").Trim();
        public string? FormatedCaducitat() => (string.IsNullOrEmpty(Caduca) || Caduca.Length != 4) ? Caduca : $"{Caduca.Substring(0,2)}/{Caduca.Substring(2, 2)}";


        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom + " " + Digits;
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

    }
}
