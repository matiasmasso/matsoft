using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Projects to be referred from other tables
    /// </summary>
    public partial class Projecte
    {
        public Projecte()
        {
            Ccas = new HashSet<Cca>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Start date
        /// </summary>
        public DateTime FchFrom { get; set; }
        /// <summary>
        /// End date
        /// </summary>
        public string? FchTo { get; set; }
        /// <summary>
        /// Project name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Project description
        /// </summary>
        public string? Dsc { get; set; }

        public virtual ICollection<Cca> Ccas { get; set; }
    }
}
