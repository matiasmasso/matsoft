using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProfessionModel:BaseGuid,IModel
    {
        public string? Nom { get; set; }
        public string? Llati { get; set; }
        public string? Obs { get; set; }


        public ProfessionModel() : base() { }
        public ProfessionModel(Guid guid) : base(guid) { }
        public static ProfessionModel Factory() => new ProfessionModel { Nom = "(nova professió)" };

        public static string CollectionPageUrl() => Globals.PageUrl("professions");
        public string PropertyPageUrl() => Globals.PageUrl("profession", Guid.ToString());
        public string CreatePageUrl() => Globals.PageUrl("profession");


        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom ?? "";
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;
                retval = searchTerms.All(x => compareInfo.IndexOf(searchTarget, x, options) >= 0);
            }
            return retval;
        }

        public MenuModel ContextMenu() => MenuModel.Factory(this);

        public string Caption() => Nom ?? ""; //To implement iModel Interface for property grid selectors
        public override string ToString() => Caption();
    }
}
