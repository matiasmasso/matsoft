using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Countries
    /// </summary>
    public partial class Country
    {
        public Country()
        {
            Arts = new HashSet<Art>();
            CliTels = new HashSet<CliTel>();
            IntrastatPartidumCountryNavigations = new HashSet<IntrastatPartidum>();
            IntrastatPartidumMadeInNavigations = new HashSet<IntrastatPartidum>();
            RecallClis = new HashSet<RecallCli>();
            Regios = new HashSet<Regio>();
            Sorteos = new HashSet<Sorteo>();
            Stps = new HashSet<Stp>();
            Tpas = new HashSet<Tpa>();
            Webs = new HashSet<Web>();
            Zonas = new HashSet<Zona>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// ISO 3166 country code
        /// </summary>
        public string Iso { get; set; } = null!;
        /// <summary>
        /// Country name in Spanish
        /// </summary>
        public string NomEsp { get; set; } = null!;
        /// <summary>
        /// Country name in Catalan
        /// </summary>
        public string NomCat { get; set; } = null!;
        /// <summary>
        /// Country name in English
        /// </summary>
        public string NomEng { get; set; } = null!;
        /// <summary>
        /// Country name in Portuguese
        /// </summary>
        public string NomPor { get; set; } = null!;
        /// <summary>
        /// Country phone prefix
        /// </summary>
        public string? PrefixeTelefonic { get; set; }
        /// <summary>
        /// whether the country belongs to UE
        /// </summary>
        public bool Cee { get; set; }
        /// <summary>
        /// Export code for Customs: National (1), European (2) others (3)
        /// </summary>
        public short ExportCod { get; set; }
        /// <summary>
        /// Country flag icon
        /// </summary>
        public byte[]? Flag { get; set; }
        /// <summary>
        /// ISO 639-2 language code (3 letters) to address to residents
        /// </summary>
        public string? LangIso { get; set; }

        public virtual ICollection<Art> Arts { get; set; }
        public virtual ICollection<CliTel> CliTels { get; set; }
        public virtual ICollection<IntrastatPartidum> IntrastatPartidumCountryNavigations { get; set; }
        public virtual ICollection<IntrastatPartidum> IntrastatPartidumMadeInNavigations { get; set; }
        public virtual ICollection<RecallCli> RecallClis { get; set; }
        public virtual ICollection<Regio> Regios { get; set; }
        public virtual ICollection<Sorteo> Sorteos { get; set; }
        public virtual ICollection<Stp> Stps { get; set; }
        public virtual ICollection<Tpa> Tpas { get; set; }
        public virtual ICollection<Web> Webs { get; set; }
        public virtual ICollection<Zona> Zonas { get; set; }
    }
}
