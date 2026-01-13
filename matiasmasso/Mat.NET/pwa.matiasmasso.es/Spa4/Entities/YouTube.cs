using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product videos stored on Youtube
    /// </summary>
    public partial class YouTube
    {
        public YouTube()
        {
            YouTubeProducts = new HashSet<YouTubeProduct>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Youtube Id of the video
        /// </summary>
        public string YoutubeId { get; set; } = null!;
        /// <summary>
        /// Video name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Video description
        /// </summary>
        public string? Dsc { get; set; }
        /// <summary>
        /// ISO 639-2 language code
        /// </summary>
        public string? Lang { get; set; }
        /// <summary>
        /// True if the video is outdated
        /// </summary>
        public bool Obsoleto { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual ICollection<YouTubeProduct> YouTubeProducts { get; set; }
    }
}
