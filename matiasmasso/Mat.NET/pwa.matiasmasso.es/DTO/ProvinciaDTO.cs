using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ProvinciaDTO:BaseGuid
    {
        public string? Nom { get; set; }

        public ProvinciaDTO() : base() { }
        public ProvinciaDTO(Guid guid) : base(guid) { }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom ?? "";
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public new string ToString()
        {
            return string.Format("ProvinciaDTO: {0}", Nom);
        }


    }
}
