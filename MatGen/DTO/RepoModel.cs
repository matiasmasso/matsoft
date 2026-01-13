using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RepoModel:BaseGuid, IModel
    {
        public string? Nom { get; set; }
        public string? Abr { get; set; }
        public string? Adr { get; set; }
        public string? Location { get; set; }
        public string? Zip { get; set; }
        public string? Country { get; set; }

        public RepoModel() : base() { }
        public RepoModel(Guid guid) : base(guid) { }
        public static RepoModel Factory() => new RepoModel { Nom = "(nou repositori)" };

        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom + " " + Abr ?? "";
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;
                retval = searchTerms.All(x => compareInfo.IndexOf(searchTarget, x, options) >= 0);
            }
            return retval;
        }
        public string CreatePageUrl() => Globals.PageUrl("repo");

        public string PropertyPageUrl()
        {
            string retval = Globals.PageUrl("repo");
            return retval;
        }
        public string Caption() => $"{Abr} ({Nom})";

        public override string ToString() => Caption();


    }
}
