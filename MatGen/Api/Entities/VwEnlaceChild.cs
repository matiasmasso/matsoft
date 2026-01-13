using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwEnlaceChild
    {
        public Guid? Guid { get; set; }
        public string? Fch { get; set; }
        public Guid? Marit { get; set; }
        public Guid? Muller { get; set; }
        public string? MaritFchFrom { get; set; }
        public string? MaritFchTo { get; set; }
        public string? MaritNom { get; set; }
        public string? MullerFchFrom { get; set; }
        public string? MullerFchTo { get; set; }
        public string? MullerNom { get; set; }
        public int NupciesMarit { get; set; }
        public int NupciesMuller { get; set; }
        public Guid? ChildGuid { get; set; }
        public string? ChildNom { get; set; }
        public short? ChildSex { get; set; }
        public string? ChildFchFrom { get; set; }
        public string? ChildFchTo { get; set; }
    }
}
