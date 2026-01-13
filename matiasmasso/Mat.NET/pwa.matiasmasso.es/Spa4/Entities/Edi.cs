using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Edi messages log (sent and received)
    /// </summary>
    public partial class Edi
    {
        public Edi()
        {
            EdiversaOrdrspHeaderEdiversaOrderNavigations = new HashSet<EdiversaOrdrspHeader>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Name of the file in the server
        /// </summary>
        public string? Filename { get; set; }
        /// <summary>
        /// Message identifier. Enumerable DTOEdiversaFile.Tags (ORDERS_D_96A_UN_EAN008, ...)
        /// </summary>
        public string? Tag { get; set; }
        /// <summary>
        /// Message date
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// GLN EAN 13 code assigned to the sender party of the message
        /// </summary>
        public string? Sender { get; set; }
        /// <summary>
        /// GLN EAN 13 code assigned to the receiver party of the message
        /// </summary>
        public string? Receiver { get; set; }
        /// <summary>
        /// Enumerable DTOEdiversaFile.IOCods (1.Inbox, 2.Outbox)
        /// </summary>
        public int Iocod { get; set; }
        /// <summary>
        /// Total amount, if applicable, in Euros
        /// </summary>
        public decimal? Eur { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string? Cur { get; set; }
        /// <summary>
        /// Total amount, if applicable, in foreign currency
        /// </summary>
        public decimal? Val { get; set; }
        /// <summary>
        /// Document number
        /// </summary>
        public string? Docnum { get; set; }
        /// <summary>
        /// Raw text of the message
        /// </summary>
        public string Text { get; set; } = null!;
        /// <summary>
        /// Enumerable DTOEdiversaFile.Results (0.Pending, 1.Processed, 2.Deleted)
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// Primary key to result table depending on the message type
        /// </summary>
        public Guid? ResultGuid { get; set; }
        /// <summary>
        /// Date the message waas registered
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual EdiRemadvHeader EdiRemadvHeader { get; set; } = null!;
        public virtual EdiversaDesadvHeader EdiversaDesadvHeader { get; set; } = null!;
        public virtual EdiversaOrdrspHeader EdiversaOrdrspHeaderGu { get; set; } = null!;
        public virtual ICollection<EdiversaOrdrspHeader> EdiversaOrdrspHeaderEdiversaOrderNavigations { get; set; }
    }
}
