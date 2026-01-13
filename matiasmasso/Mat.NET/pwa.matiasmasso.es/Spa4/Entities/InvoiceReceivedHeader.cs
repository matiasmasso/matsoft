using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Invoices received from suppliers
    /// </summary>
    public partial class InvoiceReceivedHeader
    {
        public InvoiceReceivedHeader()
        {
            InvoiceReceivedItems = new HashSet<InvoiceReceivedItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Invoice number
        /// </summary>
        public string? DocNum { get; set; }
        /// <summary>
        /// invoice date
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// Supplier
        /// </summary>
        public Guid? Proveidor { get; set; }
        /// <summary>
        /// Supplier EAN 13 code
        /// </summary>
        public string? ProveidorEan { get; set; }
        /// <summary>
        /// ISO 4217 currency code
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Import consignment; foreign key for ImportHdr
        /// </summary>
        public Guid? Importacio { get; set; }
        /// <summary>
        /// Shipment number assigned by the supplier; it may be a truck plate, a vessel name or a document Id
        /// </summary>
        public string? ShipmentId { get; set; }
        /// <summary>
        /// Invoice amount before taxes
        /// </summary>
        public decimal? TaxBase { get; set; }
        /// <summary>
        /// Invoice payable amount
        /// </summary>
        public decimal? NetTotal { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual ImportHdr? ImportacioNavigation { get; set; }
        public virtual CliGral? ProveidorNavigation { get; set; }
        public virtual ICollection<InvoiceReceivedItem> InvoiceReceivedItems { get; set; }
    }
}
