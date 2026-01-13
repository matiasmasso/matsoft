using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BankModel:BaseGuid, IModel
    {
        public string? Id { get; set; }
        public string? RaoSocial { get; set; }
        public string? NomComercial { get; set; }
        public string? Swift { get; set; }
        public string? Svg { get; set; }    
        public Guid? Country { get; set; }
        public bool Obsoleto { get; set; }


        public BankModel() : base() { }
        public BankModel(Guid guid) : base(guid) { }

        public string? NomComercialOrRaoSocial() => NomComercial == null ? RaoSocial : NomComercial;

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = NomComercial + " " + RaoSocial;
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }



        public override string ToString()
        {
            return NomComercialOrRaoSocial() ?? "?";
        }

        public class Branch:BaseGuid, IModel
        {
            public string? Id { get; set; }
            public Guid? Bank { get; set; } = new();
            public string? Adr { get; set; }
            public Guid? Location { get; set; }
            public string? Swift { get; set; }
            public bool Obsoleto { get; set; }

            public Branch() : base() { }
            public Branch(Guid guid) : base(guid) { }

            public bool Matches(string? searchTerm, string? fullAddress)
            {
                bool retval = true;
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                    var searchTarget = fullAddress;
                    retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
                }
                return retval;
            }
        }

    }
}
