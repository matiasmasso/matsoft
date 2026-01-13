using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Summary of total of Likes and shares an object (like a blog post, etc) has achieved
    /// </summary>
    public partial class VwFeedbackSum
    {
        public Guid Target { get; set; }
        public int? Shares { get; set; }
    }
}
