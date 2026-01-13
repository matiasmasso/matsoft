using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Document details
    /// </summary>
    public partial class VwDocfile
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
        public DateTime DocFileFchCreated { get; set; }
        public string? DocfileNom { get; set; }
    }
}
