using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Country Areas
    /// </summary>
    public partial class Zona
    {
        public Zona()
        {
            CliAperturas = new HashSet<CliApertura>();
            Comarcas = new HashSet<Comarca>();
            Locations = new HashSet<Location>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to Country table
        /// </summary>
        public Guid Country { get; set; }
        /// <summary>
        /// Name of the zone
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Foreign key to Province table
        /// </summary>
        public Guid? Provincia { get; set; }
        /// <summary>
        /// Wether Customs considers it National (1), European (2) or rest of the world (3)
        /// </summary>
        public short ExportCod { get; set; }
        /// <summary>
        /// Map
        /// </summary>
        public byte[]? Img { get; set; }
        /// <summary>
        /// Wether operations with this zone should be declared on Spanish Model 347 form
        /// </summary>
        public bool? Mod347 { get; set; }
        /// <summary>
        /// ISO code for the zone
        /// </summary>
        public string? Iso { get; set; }
        /// <summary>
        /// Official lang to write there, if different from the country it belongs
        /// </summary>
        public string? Lang { get; set; }
        /// <summary>
        /// Whether the zone may be divided in comarcas
        /// </summary>
        public bool SplitByComarcas { get; set; }
        /// <summary>
        /// Foreign key for PortsCondicions table
        /// </summary>
        public Guid? PortsCondicions { get; set; }

        public virtual Country CountryNavigation { get; set; } = null!;
        public virtual PortsCondicion? PortsCondicionsNavigation { get; set; }
        public virtual Provincium? ProvinciaNavigation { get; set; }
        public virtual ICollection<CliApertura> CliAperturas { get; set; }
        public virtual ICollection<Comarca> Comarcas { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
