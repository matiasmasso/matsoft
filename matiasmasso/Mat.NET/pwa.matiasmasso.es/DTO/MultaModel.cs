using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MultaModel:BaseGuid
    {
        public Guid? Emisor { get; set; }
        public string? Expedient { get; set; }
        public Guid? Subjecte { get; set; }
        public DateOnly? Fch { get; set; }
        public DateOnly? Vto { get; set; }
        public DateOnly? Pagat { get; set; }
        public Decimal? Amt { get; set; }
        //public List<DocfileModel> Docfiles { get; set; } = new();
        public List<DocfileSrcModel> Docfiles { get; set; } = new();


        public MultaModel() : base() { }
        public MultaModel(Guid guid) : base(guid) { }

        public string Url() => string.Format("multa/{0}", Guid.ToString());

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Expedient;
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public string PageUrl() => Globals.PageUrl("PgMulta", Guid.ToString());
        public override string ToString()
        {
            return string.Format("{MultaModel: {0}}", Expedient ?? "");
        }
    }
}
