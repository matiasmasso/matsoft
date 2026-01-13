using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Sales managers
    /// </summary>
    public partial class SalesManager
    {
        /// <summary>
        /// Sales manager Id; foreign key for CliGral
        /// </summary>
        public Guid Contact { get; set; }
        /// <summary>
        /// Distribution channel Id; foreign key for CliGral table
        /// </summary>
        public Guid Channel { get; set; }
        /// <summary>
        /// Product Id, foreign key for VwProductNom view
        /// </summary>
        public Guid Brand { get; set; }
        /// <summary>
        /// Area Id, foreign key for VwArea view
        /// </summary>
        public Guid Area { get; set; }
        /// <summary>
        /// Date of agreement start
        /// </summary>
        public DateTime? FchFrom { get; set; }
        /// <summary>
        /// Date of agreement termination
        /// </summary>
        public DateTime? FchTo { get; set; }
    }
}
