using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Unpayments
    /// </summary>
    public partial class Impagat
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to Bank remittance Csb table
        /// </summary>
        public Guid CsbGuid { get; set; }
        /// <summary>
        /// Unpayment bank reference
        /// </summary>
        public string RefBanc { get; set; } = null!;
        /// <summary>
        /// Expenses
        /// </summary>
        public decimal Gastos { get; set; }
        /// <summary>
        /// Partial payments total amount
        /// </summary>
        public decimal PagatAcompte { get; set; }
        /// <summary>
        /// Date the debtor has been notified about the unpayment
        /// </summary>
        public DateTime? FchAfp { get; set; }
        /// <summary>
        /// Date the debt has been canceled
        /// </summary>
        public DateTime? FchSdo { get; set; }
        /// <summary>
        /// Unpaid status DTOImpagat.StatusCodes
        /// </summary>
        public short Status { get; set; }
        /// <summary>
        /// Comments, free text
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// Date the unpayment has been notified to credit  insurance company
        /// </summary>
        public DateTime? AsnefAlta { get; set; }
        /// <summary>
        /// Date the unpayment has been removed from credit insurance company
        /// </summary>
        public DateTime? AsnefBaixa { get; set; }
        /// <summary>
        /// Accounts entry passing the debt to unchargable debts account
        /// </summary>
        public Guid? CcaIncobrable { get; set; }

        public virtual Cca? CcaIncobrableNavigation { get; set; }
        public virtual Csb CsbGu { get; set; } = null!;
    }
}
