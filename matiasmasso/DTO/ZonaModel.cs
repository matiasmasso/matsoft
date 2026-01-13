using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ZonaModel:BaseGuid,IModel
    {
        public Guid Country { get; set; }
        public string? ISO { get; set; }
        public string? Nom { get; set; }
        public Guid? Provincia { get; set; }
        public LangDTO? Lang { get; set; }
        public ExportCods ExportCod { get; set; }
        public bool Mod347 { get; set; }

        public enum ExportCods
        {
            None = 0,
            National = 1,
            EEC = 2,
            RestOfTheWorld = 3
        }

        public ZonaModel() : base() { }
        public ZonaModel(Guid guid) : base(guid) { }
        public static ZonaModel Factory(CountryModel country, string? nom = null)
        {
            var retval = new ZonaModel();
            retval.Country = country.Guid;
            retval.Nom = nom;
            retval.Lang = country.Lang;
            retval.ExportCod = (ExportCods)country.ExportCod;
            return retval;
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
