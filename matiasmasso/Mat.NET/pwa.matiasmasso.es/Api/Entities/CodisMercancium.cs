using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Customs codes for products
    /// </summary>
    public partial class CodisMercancium
    {
        public CodisMercancium()
        {
            Arts = new HashSet<Art>();
            IntrastatPartida = new HashSet<IntrastatPartidum>();
            Stps = new HashSet<Stp>();
            Tpas = new HashSet<Tpa>();
        }

        /// <summary>
        /// Customs code
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Description of products within this code
        /// </summary>
        public string? Dsc { get; set; }

        public virtual ICollection<Art> Arts { get; set; }
        public virtual ICollection<IntrastatPartidum> IntrastatPartida { get; set; }
        public virtual ICollection<Stp> Stps { get; set; }
        public virtual ICollection<Tpa> Tpas { get; set; }
    }
}
