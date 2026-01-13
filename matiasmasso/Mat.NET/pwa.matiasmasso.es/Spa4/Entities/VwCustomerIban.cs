using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Active customer bank accounts
    /// </summary>
    public partial class VwCustomerIban
    {
        public Guid Guid { get; set; }
        public Guid Customer { get; set; }
        public Guid? Bank { get; set; }
        public Guid? BankBranch { get; set; }
        public string Ccc { get; set; } = null!;
        public string? BankNom { get; set; }
        public string? BankAlias { get; set; }
        public Guid? Location { get; set; }
        public string? LocationNom { get; set; }
        public string? BankBranchAdr { get; set; }
        public DateTime? MandatoFch { get; set; }
        public DateTime? CaducaFch { get; set; }
    }
}
