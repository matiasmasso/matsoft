using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ZipModel:BaseGuid,IModel
    {
        public string? ZipCod { get; set; }
        public Guid Location { get; set; }
        public ZipModel() : base() { }
        public ZipModel(Guid guid) : base(guid) { }

        public static ZipModel Factory(LocationModel location, string? zipcod = null)
        {
            var retval = new ZipModel();
            retval.Location = location.Guid;
            retval.ZipCod = zipcod;
            return retval;
        }


        public static string FullNom(string? zipCod = null, string? location = null, string? zona = null, string? provincia = null, string? countryIso = null, string? countryNom = null)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(zipCod)) sb.Append(zipCod + " ");
            sb.Append(LocationModel.FullNom(location,zona,provincia,countryIso,countryNom));
            return sb.ToString();
        }


        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());
        public string Caption() => ZipCod ?? "?";
        public override string ToString() => Caption();
    }
}
