using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Contact phone numbers and email addresses
    /// </summary>
    public partial class VwTelsyEmail
    {
        public Guid ContactGuid { get; set; }
        public int Cod { get; set; }
        public bool Privat { get; set; }
        public int Ord { get; set; }
        public Guid NumGuid { get; set; }
        public string Num { get; set; } = null!;
        public string? PrefixeTelefonic { get; set; }
        public string? Obs { get; set; }
        public short? Rol { get; set; }
    }
}
