using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Alert events
    /// </summary>
    public partial class WinBug
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Event date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// foreign key for Email table, if any
        /// </summary>
        public Guid? User { get; set; }
    }
}
