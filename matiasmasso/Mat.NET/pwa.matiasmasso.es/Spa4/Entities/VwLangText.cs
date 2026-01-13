using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Text resources per language and target
    /// </summary>
    public partial class VwLangText
    {
        public Guid Guid { get; set; }
        public int Src { get; set; }
        public string? Esp { get; set; }
        public string? Cat { get; set; }
        public string? Eng { get; set; }
        public string? Por { get; set; }
    }
}
