using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Employee salary sheets
    /// </summary>
    public partial class Nomina
    {
        public Nomina()
        {
            NominaItems = new HashSet<NominaItem>();
        }

        /// <summary>
        /// Accounts registry; foreign key to accounts entry Cca table
        /// </summary>
        public Guid CcaGuid { get; set; }
        /// <summary>
        /// Employee; foreign key to CliGral table
        /// </summary>
        public Guid Staff { get; set; }
        /// <summary>
        /// Salary date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Accrued amount
        /// </summary>
        public decimal Devengat { get; set; }
        /// <summary>
        /// Subsistence allowances
        /// </summary>
        public decimal Dietes { get; set; }
        /// <summary>
        /// Tax base for withholdings
        /// </summary>
        public decimal IrpfBase { get; set; }
        /// <summary>
        /// Tax withholdings amount
        /// </summary>
        public decimal Irpf { get; set; }
        /// <summary>
        /// Social security charges
        /// </summary>
        public decimal SegSocial { get; set; }
        /// <summary>
        /// Wage garnishment
        /// </summary>
        public decimal Embargos { get; set; }
        /// <summary>
        /// Debts
        /// </summary>
        public decimal Deutes { get; set; }
        /// <summary>
        /// Advanced amounts
        /// </summary>
        public decimal Anticips { get; set; }
        /// <summary>
        /// Payable cash
        /// </summary>
        public decimal Liquid { get; set; }
        /// <summary>
        /// Bank account to transfer the salary
        /// </summary>
        public string? Iban { get; set; }

        public virtual Cca CcaGu { get; set; } = null!;
        public virtual CliGral StaffNavigation { get; set; } = null!;
        public virtual ICollection<NominaItem> NominaItems { get; set; }
    }
}
