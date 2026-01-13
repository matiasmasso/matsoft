using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class TaggedPicture
    {
        public TaggedPicture()
        {
            DocFileTags = new HashSet<DocFileTag>();
        }

        public string Hash { get; set; } = null!;
        public Guid Album { get; set; }
        public int Ord { get; set; }
        public string? Caption { get; set; }
        public string? Fch { get; set; }

        public virtual PictureAlbum AlbumNavigation { get; set; } = null!;
        public virtual ICollection<DocFileTag> DocFileTags { get; set; }
    }
}
