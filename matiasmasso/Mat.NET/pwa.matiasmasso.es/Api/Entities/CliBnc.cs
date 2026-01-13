using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Our banks
    /// </summary>
    public partial class CliBnc
    {
        public CliBnc()
        {
            BancTransferPools = new HashSet<BancTransferPool>();
            Csas = new HashSet<Csa>();
            SepaMes = new HashSet<SepaMe>();
            VisaCards = new HashSet<VisaCard>();
            Xecs = new HashSet<Xec>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Friendly name
        /// </summary>
        public string Abr { get; set; } = null!;
        /// <summary>
        /// Credit limit
        /// </summary>
        public decimal? Classificacio { get; set; }
        /// <summary>
        /// Id for remittances to Andorra
        /// </summary>
        public string? NormaRmecedent { get; set; }
        /// <summary>
        /// Id for Sepa remittances
        /// </summary>
        public string? SepaCoreIdentificador { get; set; }
        /// <summary>
        /// Commission for remittances at sight, VAT applicable part
        /// </summary>
        public decimal? ComisioGestioCobrBase { get; set; }
        /// <summary>
        /// Swift payments conditions
        /// </summary>
        public string? ConditionsTransfers { get; set; }
        /// <summary>
        /// Unpayment conditions
        /// </summary>
        public string? ConditionsUnpayments { get; set; }
        /// <summary>
        /// Used when asking debtors to transfer money, the available accounts are sorted by this field
        /// </summary>
        public int? TransferReceiptPreferencia { get; set; }

        public virtual CliGral Gu { get; set; } = null!;
        public virtual ICollection<BancTransferPool> BancTransferPools { get; set; }
        public virtual ICollection<Csa> Csas { get; set; }
        public virtual ICollection<SepaMe> SepaMes { get; set; }
        public virtual ICollection<VisaCard> VisaCards { get; set; }
        public virtual ICollection<Xec> Xecs { get; set; }
    }
}
