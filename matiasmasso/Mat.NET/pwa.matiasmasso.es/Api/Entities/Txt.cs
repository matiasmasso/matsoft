using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Text resources in 4 languages accessible programmatically by an integer key
    /// </summary>
    public partial class Txt
    {
        /// <summary>
        /// Enumerable DTOTxt.Ids to access the resource programmatically
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Resource text in Spanish
        /// </summary>
        public string? Esp { get; set; }
        /// <summary>
        /// Resource text in Catalan
        /// </summary>
        public string? Cat { get; set; }
        /// <summary>
        /// Resource text in English
        /// </summary>
        public string? Eng { get; set; }
        /// <summary>
        /// Resource text in Portuguese
        /// </summary>
        public string? Por { get; set; }
    }
}
