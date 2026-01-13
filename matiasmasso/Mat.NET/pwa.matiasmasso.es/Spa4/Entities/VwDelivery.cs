using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Delivery notes
    /// </summary>
    public partial class VwDelivery
    {
        public Guid AlbGuid { get; set; }
        public int? Emp { get; set; }
        public int? Yea { get; set; }
        public int AlbId { get; set; }
        public DateTime AlbFch { get; set; }
        public short Cod { get; set; }
        public byte PortsCod { get; set; }
        public byte CashCod { get; set; }
        public decimal AlbEur { get; set; }
        public Guid? TransmGuid { get; set; }
        public int? Transm { get; set; }
        public decimal? AlbImportAdicional { get; set; }
        public DateTime? AlbCobro { get; set; }
        public byte AlbRetencioCod { get; set; }
        public bool Facturable { get; set; }
        public Guid? FraGuid { get; set; }
        public int? FraId { get; set; }
        public int? FraSerie { get; set; }
        public Guid? CliGuid { get; set; }
        public string FullNom { get; set; } = null!;
        public string LangId { get; set; } = null!;
        public string? ClientRef { get; set; }
        public Guid? TrpGuid { get; set; }
        public string? TrpNom { get; set; }
        public string? TrpNif { get; set; }
        public string? Tracking { get; set; }
        public Guid? UsrCreated { get; set; }
        public string? UsrCreatedNickname { get; set; }
        public int? TicketId { get; set; }
        public string? TicketNom { get; set; }
        public string? TicketCognom { get; set; }
    }
}
