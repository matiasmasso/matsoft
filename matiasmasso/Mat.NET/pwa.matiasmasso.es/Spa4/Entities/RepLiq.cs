using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Commission statements
    /// </summary>
    public partial class RepLiq
    {
        public RepLiq()
        {
            Rps = new HashSet<Rp>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Year of the statement
        /// </summary>
        public int Yea { get; set; }
        /// <summary>
        /// statement Id. It starts again each year
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Agent. Foreign key for CliGral table
        /// </summary>
        public Guid RepGuid { get; set; }
        /// <summary>
        /// Statement date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Sum of customer invoices amounts
        /// </summary>
        public decimal BaseFras { get; set; }
        /// <summary>
        /// Commission in foreign currency, if any
        /// </summary>
        public decimal ComisioDivisa { get; set; }
        /// <summary>
        /// Commission in Euro
        /// </summary>
        public decimal ComisioEur { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// VAT percentage
        /// </summary>
        public decimal Ivapct { get; set; }
        /// <summary>
        /// Irpf percentage
        /// </summary>
        public decimal Irpfpct { get; set; }
        /// <summary>
        /// Accounts entry, foreign key for Cca table
        /// </summary>
        public Guid? CcaGuid { get; set; }

        public virtual ICollection<Rp> Rps { get; set; }
    }
}
