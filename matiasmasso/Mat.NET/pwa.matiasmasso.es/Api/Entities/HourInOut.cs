using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Defines daily time intervals between hours, for example to register employees workdays
    /// </summary>
    public partial class HourInOut
    {
        /// <summary>
        /// Parent table, may be different , one of them table StaffLog for employee workdays registry
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Starting hour
        /// </summary>
        public int HourFrom { get; set; }
        /// <summary>
        /// Starting minute
        /// </summary>
        public int? MinuteFrom { get; set; }
        /// <summary>
        /// Ending hour
        /// </summary>
        public int? HourTo { get; set; }
        /// <summary>
        /// Ending minute
        /// </summary>
        public int? MinuteTo { get; set; }
    }
}
