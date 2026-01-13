using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Documents thumbnails
    /// </summary>
    public partial class VwDocfileThumbnail
    {
        public string DocfileHash { get; set; } = null!;
        public int DocfileMime { get; set; }
        public int DocfileLength { get; set; }
        public int DocfilePags { get; set; }
        public int DocfileWidth { get; set; }
        public int DocfileHeight { get; set; }
        public int DocfileHres { get; set; }
        public int DocfileVres { get; set; }
        public DateTime DocfileFch { get; set; }
        public string? DocfileNom { get; set; }
        public byte[]? DocfileThumbnail { get; set; }
    }
}
