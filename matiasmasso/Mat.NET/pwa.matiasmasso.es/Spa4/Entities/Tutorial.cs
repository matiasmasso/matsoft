using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Tutorials to use certain processes
    /// </summary>
    public partial class Tutorial
    {
        public Tutorial()
        {
            TutorialRols = new HashSet<TutorialRol>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Subject; foreign key to parent table TutorialSubject
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Tutorial title
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Brief description about what is the tutorial about
        /// </summary>
        public string? Excerpt { get; set; }
        /// <summary>
        /// Id required to browse the video in youtube
        /// </summary>
        public string YoutubeId { get; set; } = null!;
        /// <summary>
        /// Date the tutorial was launched
        /// </summary>
        public DateTime Fch { get; set; }

        public virtual TutorialSubject ParentNavigation { get; set; } = null!;
        public virtual ICollection<TutorialRol> TutorialRols { get; set; }
    }
}
