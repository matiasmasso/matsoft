using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Transport to customer conditions
    /// </summary>
    public partial class PortsCondicion
    {
        public PortsCondicion()
        {
            Zonas = new HashSet<Zona>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Friendly name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Enumerable DTOPortsCondicio.Cods (0.paid transport, 1.chargeable....)
        /// </summary>
        public short Cod { get; set; }
        /// <summary>
        /// Minimum order value for free of charge transport
        /// </summary>
        public decimal? PdcMinVal { get; set; }
        /// <summary>
        /// Forfait transport fee if order value does not reach the minimum
        /// </summary>
        public decimal? Fee { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Ord { get; set; }

        public virtual ICollection<Zona> Zonas { get; set; }
    }
}
