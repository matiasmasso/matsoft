using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DocfileSrcModel //:IModel // per immobles
    {
        //public Guid Guid { get; set; } // fake to meet IModel
        public Guid Target { get; set; }
        //public IModel? Tag { get; set; }
        public DocfileModel? Docfile { get; set; }

        public DocfileModel.Cods Cod { get; set; }
        public int Ord { get; set; }

        //public DocfileSrcModel()
        //{
        //    Guid = Guid.NewGuid();
        //}

        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Docfile?.Nom; // + " " + SearchKey;
                retval = searchTerms.All(x => searchTarget?.Contains(x, StringComparison.OrdinalIgnoreCase) ?? false);
            }
            return retval;
        }
    }
}
