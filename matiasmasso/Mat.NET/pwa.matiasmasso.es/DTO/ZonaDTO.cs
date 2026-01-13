using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ZonaDTO:BaseGuid
    {
        public CountryDTO? Country { get; set; }
        public ProvinciaDTO? Provincia { get; set; }
        public string? Nom { get; set; }

        public ZonaDTO():base(){}
        public ZonaDTO(Guid guid):base(guid){}

        public string FullNom(LangDTO? lang = null)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} ", Nom ?? "");
            if(Country != null && !Country.IsSpain())
            {
                if (lang == null) lang = LangDTO.Default();
                sb.AppendFormat("({0})", Country!.Nom?.Tradueix(lang));
            }
            return sb.ToString();
        }


        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom + " " + Country?.Nom?.Esp ?? "";
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public new string ToString()
        {
            return string.Format("ZonaDTO: {0}", Nom);
        }


    }
}
