using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Sale operation to consumer
    /// </summary>
    public partial class ConsumerTicket
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company; foreign key for Emp table
        /// </summary>
        public int? Emp { get; set; }
        /// <summary>
        /// Ticket Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Ticket date
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// ISO 639-2 language code
        /// </summary>
        public string Lang { get; set; } = null!;
        /// <summary>
        /// Market place where the conversion has been carried out, if any; foreign key for CliGral table
        /// </summary>
        public Guid? MarketPlace { get; set; }
        /// <summary>
        /// Order number assigned by the market place
        /// </summary>
        public string? OrderNum { get; set; }
        /// <summary>
        /// Consumer destination name
        /// </summary>
        public string? Nom { get; set; }
        /// <summary>
        /// Consumer destination first surname
        /// </summary>
        public string? Cognom1 { get; set; }
        /// <summary>
        /// Consumer destination second surname, if any
        /// </summary>
        public string? Cognom2 { get; set; }
        /// <summary>
        /// Consumer delivery address
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Consumer zip code
        /// </summary>
        public string? ConsumerZip { get; set; }
        /// <summary>
        /// Consumer location name
        /// </summary>
        public string? ConsumerLocation { get; set; }
        /// <summary>
        /// Consumer province name
        /// </summary>
        public string? ConsumerProvincia { get; set; }
        /// <summary>
        /// Foreign key for Zip table
        /// </summary>
        public Guid? Zip { get; set; }
        /// <summary>
        /// Consumer purchaser name
        /// </summary>
        public string? BuyerNom { get; set; }
        /// <summary>
        /// Consumer purchaser email address
        /// </summary>
        public string? BuyerEmail { get; set; }
        /// <summary>
        /// Contact phone number
        /// </summary>
        public string? Tel { get; set; }
        /// <summary>
        /// Transport charges
        /// </summary>
        public decimal? Portes { get; set; }
        /// <summary>
        /// Goods charges
        /// </summary>
        public decimal? Goods { get; set; }
        /// <summary>
        /// Market place commission
        /// </summary>
        public decimal? Comision { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// User who created this entry; foreign key for Email table
        /// </summary>
        public Guid UsrCreated { get; set; }
        /// <summary>
        /// Operation cod as per DTOPurchaseOrder.Codis
        /// </summary>
        public int Op { get; set; }
        /// <summary>
        /// PurchaseOrder or Spv Guid
        /// </summary>
        public Guid? Delivery { get; set; }
        public string? Emailaddress { get; set; }
        /// <summary>
        /// Accounts entry; foreign key for Email table
        /// </summary>
        public Guid? Cca { get; set; }
        /// <summary>
        /// Foreign key for Pdc table
        /// </summary>
        public Guid? PurchaseOrder { get; set; }
        /// <summary>
        /// If repair job, foreign key for Spv table
        /// </summary>
        public Guid? Spv { get; set; }
        /// <summary>
        /// VAT number
        /// </summary>
        public string? Nif { get; set; }
        /// <summary>
        /// Consumer name for invoice
        /// </summary>
        public string? FraNom { get; set; }
        /// <summary>
        /// Invoice address
        /// </summary>
        public string? FraAddress { get; set; }
        /// <summary>
        /// Invoice postal code
        /// </summary>
        public Guid? FraZip { get; set; }
        /// <summary>
        /// Date of efective deliver
        /// </summary>
        public DateTime? FchTrackingNotified { get; set; }
        /// <summary>
        /// Delivery date
        /// </summary>
        public DateTime? FchDelivered { get; set; }
        /// <summary>
        /// Date a review has been requested to the consumer
        /// </summary>
        public DateTime? FchReviewRequest { get; set; }
        /// <summary>
        /// User who has been notified to receipt the package
        /// </summary>
        public Guid? UsrTrackingNotified { get; set; }
        /// <summary>
        /// User who issued the delivery
        /// </summary>
        public Guid? UsrDelivered { get; set; }
        /// <summary>
        /// User who requested the review
        /// </summary>
        public Guid? UsrReviewRequest { get; set; }
    }
}
