using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Logs the results found when a user uses the searchbox of our website
    /// </summary>
    public partial class SearchResult
    {
        /// <summary>
        /// Foreign key to parent table SearchRequest
        /// </summary>
        public Guid SearchRequest { get; set; }
        /// <summary>
        /// Result object found
        /// </summary>
        public Guid Source { get; set; }
        /// <summary>
        /// Type of result object. Enumerable DTOSearchRequest.Result.Cods (brand, category, contact, noticia...)
        /// </summary>
        public int Cod { get; set; }

        public virtual SearchRequest SearchRequestNavigation { get; set; } = null!;
    }
}
