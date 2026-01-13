using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwStat
    {
        public int Emp { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public Guid ArtGuid { get; set; }
        public Guid Category { get; set; }
        public Guid Brand { get; set; }
        public Guid? RepGuid { get; set; }
        public Guid CliGuid { get; set; }
        public Guid LocationGuid { get; set; }
        public Guid ZonaGuid { get; set; }
        public Guid CountryGuid { get; set; }
        public Guid? DistributionChannel { get; set; }
        public Guid? Proveidor { get; set; }
        public Guid? CcxGuid { get; set; }
        public Guid? Holding { get; set; }
        public int? Qty { get; set; }
        public decimal? Eur { get; set; }
        public Guid? Expr1 { get; set; }
    }
}
