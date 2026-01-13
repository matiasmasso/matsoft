using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Platforms through which we directly sell to consumers (for example Amazon seller)
    /// </summary>
    public partial class MarketPlace
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Url for market place website
        /// </summary>
        public string? Web { get; set; }
        /// <summary>
        /// Commission rate the platform earns for each conversion
        /// </summary>
        public decimal? Commission { get; set; }
        /// <summary>
        /// Platform name
        /// </summary>
        public string? Nom { get; set; }

        public virtual CliGral Gu { get; set; } = null!;
    }
}
