using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Region provinces
    /// </summary>
    public partial class Provincium
    {
        public Provincium()
        {
            IntrastatPartida = new HashSet<IntrastatPartidum>();
            Zonas = new HashSet<Zona>();
        }

        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Provincia name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Foreign key for Region table
        /// </summary>
        public Guid? Regio { get; set; }
        /// <summary>
        /// Code for model 347 Spanish tax form
        /// </summary>
        public string? Mod347 { get; set; }
        public string? Intrastat { get; set; }

        public virtual Regio? RegioNavigation { get; set; }
        public virtual ICollection<IntrastatPartidum> IntrastatPartida { get; set; }
        public virtual ICollection<Zona> Zonas { get; set; }
    }
}
