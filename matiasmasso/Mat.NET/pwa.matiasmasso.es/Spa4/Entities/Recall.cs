using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Manufacturer requests to recall defectuous products
    /// </summary>
    public partial class Recall
    {
        public Recall()
        {
            RecallClis = new HashSet<RecallCli>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Date of alert
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Friendly name
        /// </summary>
        public string Nom { get; set; } = null!;

        public virtual ICollection<RecallCli> RecallClis { get; set; }
    }
}
