using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Posts of interest from external bloggers
    /// </summary>
    public partial class Bloggerpost
    {
        public Bloggerpost()
        {
            BloggerpostProducts = new HashSet<BloggerpostProduct>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to parent table Blogger
        /// </summary>
        public Guid? Blogger { get; set; }
        /// <summary>
        /// Post title
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Post landing page
        /// </summary>
        public string Url { get; set; } = null!;
        /// <summary>
        /// Post date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// ISO 639-2 language code (3 digits)
        /// </summary>
        public string Lang { get; set; } = null!;
        /// <summary>
        /// Date to start publishing on our website
        /// </summary>
        public DateTime? HighlightFrom { get; set; }
        /// <summary>
        /// Date to terminate publication on our website
        /// </summary>
        public DateTime? HighlightTo { get; set; }

        public virtual Blogger? BloggerNavigation { get; set; }
        public virtual ICollection<BloggerpostProduct> BloggerpostProducts { get; set; }
    }
}
