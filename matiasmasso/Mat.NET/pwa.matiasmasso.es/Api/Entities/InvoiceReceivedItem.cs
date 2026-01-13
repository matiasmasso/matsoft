using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Invoices line items
    /// </summary>
    public partial class InvoiceReceivedItem
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to parent table InvoiceReceivedHeader
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Item line number
        /// </summary>
        public int Lin { get; set; }
        /// <summary>
        /// Product; foreign key for Art table
        /// </summary>
        public Guid? Sku { get; set; }
        /// <summary>
        /// EAN 13 product code
        /// </summary>
        public string? SkuEan { get; set; }
        /// <summary>
        /// Manufacturer product code
        /// </summary>
        public string? SkuRef { get; set; }
        /// <summary>
        /// Manufacturer product name
        /// </summary>
        public string? SkuNom { get; set; }
        /// <summary>
        /// Units invoiced
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// Units received
        /// </summary>
        public int? QtyConfirmed { get; set; }
        /// <summary>
        /// Unit price
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// Price discount
        /// </summary>
        public decimal? Dto { get; set; }
        /// <summary>
        /// Line amount
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// Foreign key to Pdc table
        /// </summary>
        public Guid? PurchaseOrder { get; set; }
        /// <summary>
        /// Foreign key to Pnc table
        /// </summary>
        public Guid? PurchaseOrderItem { get; set; }
        /// <summary>
        /// Packing list number
        /// </summary>
        public string? DeliveryNote { get; set; }
        /// <summary>
        /// Order confirmation number
        /// </summary>
        public string? OrderConfirmation { get; set; }
        /// <summary>
        /// Our purchase order Id
        /// </summary>
        public string? PurchaseOrderId { get; set; }

        public virtual InvoiceReceivedHeader ParentNavigation { get; set; } = null!;
        public virtual Pdc? PurchaseOrderNavigation { get; set; }
    }
}
