using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CognomModel:BaseGuid, IModel
    {
        public string? Nom { get; set; }

        public CognomModel() : base() { }
        public CognomModel(Guid guid) : base(guid) { }
        public static CognomModel Factory() => new CognomModel { Nom = "(nou cognom)" };

        public static string CollectionPageUrl() => Globals.PageUrl("Cognoms");
        public string CreatePageUrl() => Globals.PageUrl("Cognom");
        public string PropertyPageUrl() => Globals.PageUrl("Cognom", Guid.ToString());
        public string Caption() => Nom ?? "" ?? "?"; //To implement iModel Interface for property grid selectors

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

        public override string ToString() => Caption();


    }
}
