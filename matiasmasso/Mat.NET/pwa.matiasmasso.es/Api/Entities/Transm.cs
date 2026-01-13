using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Sets of deliveries sent to the logistic center to prepare picking and packing
    /// </summary>
    public partial class Transm
    {
        public Transm()
        {
            AlbsNavigation = new HashSet<Alb>();
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
        /// Creation year
        /// </summary>
        public short Yea { get; set; }
        /// <summary>
        /// Sequential number, unique for each Company/year combination
        /// </summary>
        public int Transm1 { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTimeOffset? Fch { get; set; }
        /// <summary>
        /// Number of delivery notes included
        /// </summary>
        public int? Albs { get; set; }
        /// <summary>
        /// Total amount, foreign curreny
        /// </summary>
        public decimal? Val { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string? Cur { get; set; }
        /// <summary>
        /// Total amount in Euros
        /// </summary>
        public decimal Eur { get; set; }
        /// <summary>
        /// Logistic center preparing the deliveries; foreign key for CliGral table
        /// </summary>
        public Guid? MgzGuid { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual CliGral? MgzGu { get; set; }
        public virtual ICollection<Alb> AlbsNavigation { get; set; }
    }
}
