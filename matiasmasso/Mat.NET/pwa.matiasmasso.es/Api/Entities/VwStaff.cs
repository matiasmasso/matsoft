using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class VwStaff
    {
        public Guid Guid { get; set; }
        public string Abr { get; set; } = null!;
        public string? NumSs { get; set; }
        public DateTime? Alta { get; set; }
        public DateTime? Baja { get; set; }
        public DateTime? Nac { get; set; }
        public int Sex { get; set; }
        public Guid? Categoria { get; set; }
        public Guid? StaffPos { get; set; }
        public bool TeleTrabajo { get; set; }
        public string RaoSocial { get; set; } = null!;
        public string Nif { get; set; } = null!;
        public int Emp { get; set; }
        public string? StaffPosNomEsp { get; set; }
        public string? StaffPosNomCat { get; set; }
        public string? StaffPosNomEng { get; set; }
        public string? StaffPosNomPor { get; set; }
    }
}
