using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ZipDTO : BaseGuid
    {
        public LocationDTO? Location { get; set; }
        public string? ZipCod { get; set; }

        public ZipDTO() : base() { }
        public ZipDTO(Guid guid) : base(guid) { }

        public string FullNom(LangDTO? lang = null) => string.Format("{0} {1} ", ZipCod, Location?.FullNom(lang));

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = ZipCod + " " + Location?.Nom ?? "";
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public ZipModel ZipModel()
        {
            return new ZipModel(base.Guid)
            {
                ZipCod = ZipCod,
                Location = Location!.Guid
            };
        }

        public new string ToString() => string.Format("{DTO.ZipDTO}: {0}", FullNom());


    }


}