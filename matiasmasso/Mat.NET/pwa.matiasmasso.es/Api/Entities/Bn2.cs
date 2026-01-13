using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Bank branches
    /// </summary>
    public partial class Bn2
    {
        public Bn2()
        {
            BancTransferBeneficiaris = new HashSet<BancTransferBeneficiari>();
            Ibans = new HashSet<Iban>();
            Xecs = new HashSet<Xec>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Bank id, foreignn key for Bn1 table
        /// </summary>
        public Guid? Bank { get; set; }
        /// <summary>
        /// Branch national code within its bank
        /// </summary>
        public string? Agc { get; set; }
        /// <summary>
        /// Location id, foreign key for Location table
        /// </summary>
        public Guid? Location { get; set; }
        /// <summary>
        /// Address (street and number). Free text
        /// </summary>
        public string? Adr { get; set; }
        /// <summary>
        /// Branch phone number
        /// </summary>
        public string? Tel { get; set; }
        /// <summary>
        /// Branch swift code
        /// </summary>
        public string? Swift { get; set; }
        /// <summary>
        /// True if the branch is no longer active
        /// </summary>
        public bool Obsoleto { get; set; }

        public virtual Bn1? BankNavigation { get; set; }
        public virtual Location? LocationNavigation { get; set; }
        public virtual ICollection<BancTransferBeneficiari> BancTransferBeneficiaris { get; set; }
        public virtual ICollection<Iban> Ibans { get; set; }
        public virtual ICollection<Xec> Xecs { get; set; }
    }
}
