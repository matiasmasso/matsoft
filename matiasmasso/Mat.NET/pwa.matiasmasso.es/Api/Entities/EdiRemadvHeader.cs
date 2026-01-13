using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Remittance advice messages
    /// </summary>
    public partial class EdiRemadvHeader
    {
        public EdiRemadvHeader()
        {
            EdiRemadvItems = new HashSet<EdiRemadvItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Document number
        /// </summary>
        public string DocNum { get; set; } = null!;
        /// <summary>
        /// Document date
        /// </summary>
        public DateTime? FchDoc { get; set; }
        /// <summary>
        /// Due date
        /// </summary>
        public DateTime? FchVto { get; set; }
        /// <summary>
        /// Document reference
        /// </summary>
        public string? DocRef { get; set; }
        /// <summary>
        /// Remittance issuer; foreign key for CliGral table
        /// </summary>
        public Guid EmisorPago { get; set; }
        /// <summary>
        /// Remittance receiver; foreign key for CliGral table
        /// </summary>
        public Guid ReceptorPago { get; set; }
        /// <summary>
        /// Total remittance amount
        /// </summary>
        public decimal? Amt { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Accounts entry; foreign key for Cca table
        /// </summary>
        public Guid? Result { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual CliGral EmisorPagoNavigation { get; set; } = null!;
        public virtual Edi Gu { get; set; } = null!;
        public virtual CliGral ReceptorPagoNavigation { get; set; } = null!;
        public virtual Cca? ResultNavigation { get; set; }
        public virtual ICollection<EdiRemadvItem> EdiRemadvItems { get; set; }
    }
}
