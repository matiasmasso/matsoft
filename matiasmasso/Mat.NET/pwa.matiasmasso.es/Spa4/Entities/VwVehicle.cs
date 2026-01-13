using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class VwVehicle
    {
        public Guid Guid { get; set; }
        public int Emp { get; set; }
        public Guid? ModelGuid { get; set; }
        public string? Matricula { get; set; }
        public string? Bastidor { get; set; }
        public Guid? ConductorGuid { get; set; }
        public Guid? VenedorGuid { get; set; }
        public DateTime Alta { get; set; }
        public DateTime? Baixa { get; set; }
        public Guid? Contracte { get; set; }
        public Guid? Insurance { get; set; }
        public byte[]? Image { get; set; }
        public bool Privat { get; set; }
        public string? Model { get; set; }
        public Guid? MarcaGuid { get; set; }
        public string? Marca { get; set; }
        public string? ConductorNom { get; set; }
        public string? VenedorNom { get; set; }
        public string? ContracteNom { get; set; }
        public string? CompraNum { get; set; }
        public string? InsuranceNom { get; set; }
        public string? InsuranceNum { get; set; }
    }
}
