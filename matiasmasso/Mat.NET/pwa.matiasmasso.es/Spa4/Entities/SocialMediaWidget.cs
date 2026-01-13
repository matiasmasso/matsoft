using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Widget details for social media platforms
    /// </summary>
    public partial class SocialMediaWidget
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Product brand; foreign key for Tpa table
        /// </summary>
        public Guid? Brand { get; set; }
        /// <summary>
        /// Enumerable DTOSocialMediaWidget.Platforms (1.Facebook, 2.Twitter...)
        /// </summary>
        public int Platform { get; set; }
        /// <summary>
        /// Platform profile name
        /// </summary>
        public string? Titular { get; set; }
        /// <summary>
        /// Platform profile id
        /// </summary>
        public string? WidgetId { get; set; }

        public virtual Tpa? BrandNavigation { get; set; }
    }
}
