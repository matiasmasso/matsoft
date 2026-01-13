using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Employee salary concepts
    /// </summary>
    public partial class NominaConcepte
    {
        public NominaConcepte()
        {
            NominaItems = new HashSet<NominaItem>();
        }

        /// <summary>
        /// Concept cdefined by externalized salary management company
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Concept description
        /// </summary>
        public string Concepte { get; set; } = null!;

        public virtual ICollection<NominaItem> NominaItems { get; set; }
    }
}
