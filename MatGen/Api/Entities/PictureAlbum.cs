using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class PictureAlbum
    {
        public PictureAlbum()
        {
            TaggedPictures = new HashSet<TaggedPicture>();
        }

        public Guid Guid { get; set; }
        public string? Fch { get; set; }
        public string? Caption { get; set; }

        public virtual ICollection<TaggedPicture> TaggedPictures { get; set; }
    }
}
