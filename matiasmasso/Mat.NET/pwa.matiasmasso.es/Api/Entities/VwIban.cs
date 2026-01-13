using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// All bank accounts
    /// </summary>
    public partial class VwIban
    {
        public Guid IbanGuid { get; set; }
        public string IbanCcc { get; set; } = null!;
        public short IbanCod { get; set; }
        public Guid IbanContactGuid { get; set; }
        public DateTime? IbanMandatoFch { get; set; }
        public DateTime? IbanCaducaFch { get; set; }
        public Guid? BankGuid { get; set; }
        public string? BankNom { get; set; }
        public string? BankAlias { get; set; }
        public string? BankSwift { get; set; }
        public Guid? BankBranchGuid { get; set; }
        public string? BankBranchAdr { get; set; }
        public Guid? BankBranchLocationGuid { get; set; }
        public string? BankBranchLocationNom { get; set; }
        public Guid? BankBranchZonaGuid { get; set; }
        public string? BankBranchZonaNom { get; set; }
        public Guid? BankBranchCountryGuid { get; set; }
        public string? BankBranchCountryIso { get; set; }
    }
}
