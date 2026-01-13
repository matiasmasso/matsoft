using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwStoreLocator
    {
        public Guid Brand { get; set; }
        public Guid? Dept { get; set; }
        public Guid Category { get; set; }
        public Guid? Sku { get; set; }
        public Guid? Location { get; set; }
        public string Nom { get; set; } = null!;
        public string Adr { get; set; } = null!;
        public string Cit { get; set; } = null!;
        public Guid? AreaGuid { get; set; }
        public string? AreaNom { get; set; }
        public Guid? Country { get; set; }
        public string CountryEsp { get; set; } = null!;
        public string CountryCat { get; set; } = null!;
        public string CountryEng { get; set; } = null!;
        public string CountryPor { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public int? ConsumerPriority { get; set; }
        public decimal? Val { get; set; }
        public bool Raffles { get; set; }
        public Guid Client { get; set; }
        public Guid? PremiumLine { get; set; }
    }
}
