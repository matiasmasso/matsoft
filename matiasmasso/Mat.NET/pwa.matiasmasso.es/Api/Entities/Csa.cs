using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Bank remittances (remesas de efectos bancarios al cobro o al descuento)
    /// </summary>
    public partial class Csa
    {
        public Csa()
        {
            Csbs = new HashSet<Csb>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Our bank; foreign key for CliBnc table
        /// </summary>
        public Guid? BancGuid { get; set; }
        /// <summary>
        /// Accounts entry; foreign key for Cca table
        /// </summary>
        public Guid? CcaGuid { get; set; }
        /// <summary>
        /// Company; foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Remittance year
        /// </summary>
        public short Yea { get; set; }
        /// <summary>
        /// Remittance number, unique for each Company/year
        /// </summary>
        public int Csb { get; set; }
        /// <summary>
        /// Remittance date
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// Expenses
        /// </summary>
        public decimal Gts { get; set; }
        /// <summary>
        /// True if payments are advanced by the bank
        /// </summary>
        public bool Descomptat { get; set; }
        /// <summary>
        /// Amount in foreign currency
        /// </summary>
        public decimal Pts { get; set; }
        /// <summary>
        /// Number of days the bank is advancing the payment
        /// </summary>
        public short Dias { get; set; }
        /// <summary>
        /// Average amount of the payments
        /// </summary>
        public decimal ImportMig { get; set; }
        /// <summary>
        /// Equivalent annual rate
        /// </summary>
        public decimal? Tae { get; set; }
        /// <summary>
        /// Number of payments included in the remittance
        /// </summary>
        public short Efts { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Amount in Euro
        /// </summary>
        public decimal Eur { get; set; }
        /// <summary>
        /// Enumerable DTOCsa.FileFormats (norma 58, norma 19, remesa Caixa export, SEPA B2B, SEPA Core...)
        /// </summary>
        public short FileFormat { get; set; }

        public virtual CliBnc? BancGu { get; set; }
        public virtual Cca? CcaGu { get; set; }
        public virtual ICollection<Csb> Csbs { get; set; }
    }
}
