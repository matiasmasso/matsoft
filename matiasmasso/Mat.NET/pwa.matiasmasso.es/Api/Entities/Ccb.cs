using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Account entry items
    /// </summary>
    public partial class Ccb
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to parent table Cca
        /// </summary>
        public Guid CcaGuid { get; set; }
        /// <summary>
        /// Line number to sort the items within same accounts entry
        /// </summary>
        public int Lin { get; set; }
        /// <summary>
        /// Account, foreign key to accounts table PgcCta
        /// </summary>
        public Guid CtaGuid { get; set; }
        /// <summary>
        /// Account owner, foreign key to CliGral table
        /// </summary>
        public Guid? ContactGuid { get; set; }
        /// <summary>
        /// Amount, in foreign currency
        /// </summary>
        public decimal Pts { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Amount, in Euro currency
        /// </summary>
        public decimal Eur { get; set; }
        /// <summary>
        /// Enumerable DTOCcb.DhEnum (1.Debe, 2.Haber)
        /// </summary>
        public byte Dh { get; set; }
        /// <summary>
        /// foreign key to debt table Pnd, when applicable
        /// </summary>
        public Guid? PndGuid { get; set; }

        public virtual Cca CcaGu { get; set; } = null!;
        public virtual CliGral? ContactGu { get; set; }
        public virtual PgcCtum CtaGu { get; set; } = null!;
    }
}
