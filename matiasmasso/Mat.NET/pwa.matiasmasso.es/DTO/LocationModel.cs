using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LocationModel:BaseGuid,IModel
    {
        public string? Nom { get; set; }
        public Guid Zona { get; set; }

        public enum Wellknowns
        {
            Barcelona
        }

        public LocationModel() : base() { }
        public LocationModel(Guid guid) : base(guid) { }

        public static LocationModel Factory(ZonaModel zona, string? nom = null)
        {
            var retval = new LocationModel();
            retval.Zona = zona.Guid;
            retval.Nom = nom;
            return retval;
        }

        public static string FullNom(string? location = null, string? zona = null, string? provincia = null, string? countryIso = null, string? countryNom = null)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(location)) sb.Append(location + " ");
            if (countryIso == "ES")
            {
                if (provincia != location) sb.AppendFormat("({0})", provincia);
            }
            else if (countryNom != null)
            {
                if (zona != null && zona == countryNom)
                    sb.AppendFormat("({0})", countryNom);
                else
                    sb.AppendFormat("({0}, {1})", zona, countryNom);
            }
            return sb.ToString().Trim();
        }

        public override bool Matches(string searchKey)
        {
            return Nom?.Contains(searchKey) ?? false;
        }
        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());

        public string Caption() => Nom ?? "?";
        public override string ToString() => Caption();
    }
}
