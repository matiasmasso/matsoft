using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Likes and shares an object (like a blog post, etc) has achieved per user
    /// </summary>
    public partial class VwFeedback
    {
        public Guid Target { get; set; }
        public Guid? UserOrCustomer { get; set; }
        public int? Likes { get; set; }
        public int? Shares { get; set; }
        public int? HasLiked { get; set; }
        public int? HasShared { get; set; }
    }
}
