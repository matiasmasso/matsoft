using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FirstnomModel : BaseGuid, IModel
    {
        public string? Nom { get; set; }

        public PersonModel.Sexs Sex { get; set; }


        public FirstnomModel() : base() { }
        public FirstnomModel(Guid guid) : base(guid) { }

        public static FirstnomModel Factory() => new FirstnomModel { Nom = "(nou nom de pila)"};


        public static string CollectionPageUrl() => Globals.PageUrl("Firstnoms");
        public string PropertyPageUrl() => Globals.PageUrl("Firstnom", Guid.ToString());
        public string CreatePageUrl() => Globals.PageUrl("firstnom");


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

        public string Caption() => Nom ?? "" ?? "?"; //To implement iModel Interface for property grid selectors
        public override string ToString() => Caption();


    }
}
