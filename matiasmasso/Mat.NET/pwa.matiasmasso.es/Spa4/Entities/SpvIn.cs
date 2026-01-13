using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Workshop reception of products to repair
    /// </summary>
    public partial class SpvIn
    {
        public SpvIn()
        {
            Spvs = new HashSet<Spv>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company; foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Year of reception
        /// </summary>
        public short Yea { get; set; }
        /// <summary>
        /// Sequential number unique per Company and year
        /// </summary>
        public short Id { get; set; }
        /// <summary>
        /// Forwarder reference number of the delivery
        /// </summary>
        public string Expedicio { get; set; } = null!;
        /// <summary>
        /// Date of reception
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Number of packages received
        /// </summary>
        public short Bultos { get; set; }
        /// <summary>
        /// Weight in Kg
        /// </summary>
        public short Kg { get; set; }
        /// <summary>
        /// Volume in cubic meters
        /// </summary>
        public decimal? M3 { get; set; }
        /// <summary>
        /// Comments to report any incidences on reception (lack of packages, packages received in bad conditions...)
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// User who created this entry; foreign key for Email table
        /// </summary>
        public Guid? UsrGuid { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual Email? UsrGu { get; set; }
        public virtual ICollection<Spv> Spvs { get; set; }
    }
}
