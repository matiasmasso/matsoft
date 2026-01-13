using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Newsletters sent to subscribed leads
    /// </summary>
    public partial class Newsletter
    {
        public Newsletter()
        {
            NewsletterSources = new HashSet<NewsletterSource>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Newsletter date
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// Sequential Id, to refer to each newsletter by a number
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Newsletter subject
        /// </summary>
        public string? Title { get; set; }

        public virtual ICollection<NewsletterSource> NewsletterSources { get; set; }
    }
}
