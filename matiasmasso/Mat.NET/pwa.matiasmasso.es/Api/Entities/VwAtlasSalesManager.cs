using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customer list for each sales manager
    /// </summary>
    public partial class VwAtlasSalesManager
    {
        public Guid CountryGuid { get; set; }
        public string CountryEsp { get; set; } = null!;
        public string CountryCat { get; set; } = null!;
        public string CountryEng { get; set; } = null!;
        public Guid ZonaGuid { get; set; }
        public string ZonaNom { get; set; } = null!;
        public Guid LocationGuid { get; set; }
        public string LocationNom { get; set; } = null!;
        public Guid ContactGuid { get; set; }
        public string RaoSocial { get; set; } = null!;
        public string NomCom { get; set; } = null!;
        public Guid User { get; set; }
    }
}
