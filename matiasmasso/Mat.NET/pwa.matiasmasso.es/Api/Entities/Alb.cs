using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Delivery note parent table for shipped goods, either received or sent
    /// </summary>
    public partial class Alb
    {
        public Alb()
        {
            Arcs = new HashSet<Arc>();
            CliReturns = new HashSet<CliReturn>();
            RecallClis = new HashSet<RecallCli>();
            Sorteos = new HashSet<Sorteo>();
            Spvs = new HashSet<Spv>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company, foreign key for Emp table
        /// </summary>
        public int? Emp { get; set; }
        /// <summary>
        /// Enumerable DTOPurchaseOrder.Codis: 1.Entrance from suplier, 2.Shipment to customer...
        /// </summary>
        public short Cod { get; set; }
        /// <summary>
        /// Year (delivery number is reinitialized each year per Company)
        /// </summary>
        public short Yea { get; set; }
        /// <summary>
        /// Delivery note number. It is restarted each year per Company
        /// </summary>
        public int Alb1 { get; set; }
        /// <summary>
        /// Date when the delivery note is generated
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Either customer destination or supplier origin, depending on Cod field value
        /// </summary>
        public Guid? CliGuid { get; set; }
        /// <summary>
        /// Warehouse to add or deduct from inventory
        /// </summary>
        public Guid? MgzGuid { get; set; }
        /// <summary>
        /// A platform is an entity which collects the goods to be delivered to its final destination.
        /// It may be a customer warehouse or a logistic platform to ship for example to Canary Islands, etc
        /// </summary>
        public Guid? PlatformGuid { get; set; }
        /// <summary>
        /// Batch of delivery notes sent at once to the warehouse to be prepared. Foreign key to Transm table
        /// </summary>
        public Guid? TransmGuid { get; set; }
        /// <summary>
        /// Forwarder used to deliver the goods.
        /// </summary>
        public Guid? TrpGuid { get; set; }
        /// <summary>
        /// Destinatary of the invoice if different from the destination of the goods
        /// </summary>
        public Guid? FacturarA { get; set; }
        /// <summary>
        /// Invoice. Foreign key to Fra table
        /// </summary>
        public Guid? FraGuid { get; set; }
        /// <summary>
        /// True if these goods should be invoiced
        /// </summary>
        public bool? Facturable { get; set; }
        /// <summary>
        /// Name of the destinatary of the goods
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Address (street/road and number, without location) to deliver the goods
        /// </summary>
        public string? Adr { get; set; }
        /// <summary>
        /// Foreign location to Zip table, which cares for country, location and postal code
        /// </summary>
        public Guid? Zip { get; set; }
        /// <summary>
        /// Contact phone to provide to the forwarder in case of transport incidences
        /// </summary>
        public string Tel { get; set; } = null!;
        /// <summary>
        /// Total weight in Kg
        /// </summary>
        public int Kgs { get; set; }
        /// <summary>
        /// Total volume in cubic meters
        /// </summary>
        public decimal M3 { get; set; }
        /// <summary>
        /// Number of packages
        /// </summary>
        public int Bultos { get; set; }
        /// <summary>
        /// Enumerable DTOCustomer.PortsCodes about whether transport is paid on origin or destination etc
        /// </summary>
        public byte PortsCod { get; set; }
        /// <summary>
        /// International Commerce Terms
        /// </summary>
        public string? Incoterm { get; set; }
        /// <summary>
        /// Enumerable DTOInvoice.ExportCods: 1.National, 2.EEC, 3.Rest of the world
        /// </summary>
        public int ExportCod { get; set; }
        /// <summary>
        /// Total amount in foreign currency
        /// </summary>
        public decimal Pts { get; set; }
        /// <summary>
        /// Extra charges when paid cash not related to the shipment (for example debts)
        /// </summary>
        public decimal? Pt2 { get; set; }
        /// <summary>
        /// Total amount in Eur
        /// </summary>
        public decimal Eur { get; set; }
        /// <summary>
        /// Currency in ISO 4217
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Global discount
        /// </summary>
        public float Dt2 { get; set; }
        /// <summary>
        /// Early payment discount
        /// </summary>
        public float Dpp { get; set; }
        /// <summary>
        /// True if VAT not applicable
        /// </summary>
        public bool IvaExempt { get; set; }
        /// <summary>
        /// Specific payment terms for this shipment, if different from customer&apos;s default terms
        /// </summary>
        public string? Fpg { get; set; }
        /// <summary>
        /// For cash against goods, date of payment recovery
        /// </summary>
        public DateTime? Cobro { get; set; }
        /// <summary>
        /// Enumerable DTOCustomer.CashCodes: 1.Credit, 2.Cash against goods, 3.Transfer, 4.Credit card, 5.Deposit
        /// </summary>
        public byte CashCod { get; set; }
        /// <summary>
        /// Enumerable DTODelivery.RetencioCods. Used to prevent delivery instructions to be sent to our warehouse until cash is in our account
        /// </summary>
        public byte RetencioCod { get; set; }
        /// <summary>
        /// If true, prices are displayed on delivery note
        /// </summary>
        public bool? Valorado { get; set; }
        /// <summary>
        /// Enumerable DTODelivery.JustificanteCodes. Whether we have requested delivery receipt and if we have received it
        /// </summary>
        public short Justificante { get; set; }
        /// <summary>
        /// Date we received delivery prove
        /// </summary>
        public DateTime? FchJustificante { get; set; }
        /// <summary>
        /// Url to download customer documentation to be included on the package, usually invoices from e-commerce
        /// </summary>
        public string? CustomerDocUrl { get; set; }
        /// <summary>
        /// Transport labels to be attached to package if different from warehouse defaults. Foreign key to DocFile table
        /// </summary>
        public string? EtiquetesTransport { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }
        public string? ObsTransp { get; set; }
        /// <summary>
        /// Date and time the delivery note was issued
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// User who generated the delivery note. Foreign key for Email table
        /// </summary>
        public Guid? UsrCreatedGuid { get; set; }
        /// <summary>
        /// Date and time the document was last edited
        /// </summary>
        public DateTime FchLastEdited { get; set; }
        /// <summary>
        /// User who edited last time this document. Foreign key for Email table
        /// </summary>
        public Guid? UsrLastEditedGuid { get; set; }

        public virtual CliGral? CliGu { get; set; }
        public virtual Emp? EmpNavigation { get; set; }
        public virtual DocFile? EtiquetesTransportNavigation { get; set; }
        public virtual CliGral? FacturarANavigation { get; set; }
        public virtual Fra? FraGu { get; set; }
        public virtual CliGral? MgzGu { get; set; }
        public virtual CliGral? PlatformGu { get; set; }
        public virtual Transm? TransmGu { get; set; }
        public virtual CliGral? TrpGu { get; set; }
        public virtual Email? UsrCreatedGu { get; set; }
        public virtual Email? UsrLastEditedGu { get; set; }
        public virtual Zip? ZipNavigation { get; set; }
        public virtual ICollection<Arc> Arcs { get; set; }
        public virtual ICollection<CliReturn> CliReturns { get; set; }
        public virtual ICollection<RecallCli> RecallClis { get; set; }
        public virtual ICollection<Sorteo> Sorteos { get; set; }
        public virtual ICollection<Spv> Spvs { get; set; }
    }
}
