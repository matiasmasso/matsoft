using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PubModel:BaseGuid, IModel
    {
        public string? Nom { get; set; }
        public DateTime FchCreated { get; set; }
        public DocfileModel? Docfile { get; set; }

        public List<CitaModel> Citas { get; set; } = new();

        public PubModel() { }
        public PubModel(Guid guid):base(guid) { }

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

        public string Caption()
        {
            throw new NotImplementedException();
        }

        public string PropertyPageUrl()
        {
            throw new NotImplementedException();
        }

        public string CreatePageUrl()
        {
            throw new NotImplementedException();
        }
    }
}
