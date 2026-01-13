using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Suppliers
    /// </summary>
    public partial class CliPrv
    {
        public CliPrv()
        {
            Tpas = new HashSet<Tpa>();
        }

        /// <summary>
        /// Primary key; foreign key for CliGral table
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Enumerable DTOProductBrand.CodStks (0.Internal, we keep the stocks 1.Extrernal, the supplier keep the stocks)
        /// </summary>
        public short CodStk { get; set; }
        /// <summary>
        /// International Commerce Terms; foreign key to Incoterm table
        /// </summary>
        public string? Incoterm { get; set; }
        /// <summary>
        /// Tax withholdings mode, enumerable DTOProveidor.IRPFCods (0.exempt, 1.standard, 2.reduced, 3.custom)
        /// </summary>
        public short CodIrpf { get; set; }
        /// <summary>
        /// Default currency on invoices
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Default accounts account where to charge invoices; foreign key for DTOPgcCta table
        /// </summary>
        public Guid? CtaCarrec { get; set; }
        /// <summary>
        /// Payment way code. Enumerable DTOPaymentTerms.CodsFormaDePago
        /// </summary>
        public short Cfp { get; set; }
        /// <summary>
        /// Number of months payment terms after invoice date
        /// </summary>
        public short Mes { get; set; }
        /// <summary>
        /// Type of day referred on PaymentDays field. Enumerable DTOPaymentTerms.PaymentDayCods (0.month day, 1.week day)
        /// </summary>
        public short DpgWeek { get; set; }
        /// <summary>
        /// If applicable, 31 digits string built from characters 0 or 1 indicating disabled or enabled payment day 
        /// </summary>
        public string? PaymentDays { get; set; }
        /// <summary>
        /// Payment pause for holidays: 6 comma separated integers indicating starting month, starting day, end mont, end day, forward payment to month, forward payment to day
        /// </summary>
        public string? Vacaciones { get; set; }
        public Guid? CtaCreditora { get; set; }

        public virtual PgcCtum? CtaCarrecNavigation { get; set; }
        public virtual PgcCtum? CtaCreditoraNavigation { get; set; }
        public virtual CliGral Gu { get; set; } = null!;
        public virtual ICollection<Tpa> Tpas { get; set; }
    }
}
