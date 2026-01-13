using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class PortadaImg
    {
        public string Id { get; set; } = null!;
        public byte[]? Img { get; set; }
        public int? Mime { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
    }
}
