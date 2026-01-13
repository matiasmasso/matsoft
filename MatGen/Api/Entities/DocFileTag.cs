using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class DocFileTag
    {
        public string Hash { get; set; } = null!;
        public Guid Contact { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public virtual Contact ContactNavigation { get; set; } = null!;
        public virtual TaggedPicture HashNavigation { get; set; } = null!;
    }
}
