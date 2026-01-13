using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LocationDTO : BaseGuid
    {
        public ZonaDTO? Zona { get; set; }
        public string? Nom { get; set; }

        public LocationDTO() : base() { }
        public LocationDTO(Guid guid) : base(guid) { }

        public string FullNom(LangDTO? lang = null)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} ", Nom ?? "");
            if (Zona?.Provincia != null)
            {
                if (Zona?.Country?.IsSpain() ?? false)
                {
                    if (Zona?.Provincia.Nom != Nom)
                        sb.AppendFormat("({0})", Zona?.Provincia.Nom);
                }
                else if (Zona?.Country?.Nom != null)
                {
                    lang = lang == null ? LangDTO.Default() : lang;
                    sb.Append(Zona?.Country?.Nom?.Tradueix(lang));
                }
            }
            return sb.ToString();
        }


        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom + " " + Zona?.Nom ?? "";
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public new string ToString() => string.Format("LocationDTO: {0}", FullNom());


    }
}
