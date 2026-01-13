using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Contact phone numbers
    /// </summary>
    public partial class CliTel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Contact. Foreign key for CliGral table
        /// </summary>
        public Guid CliGuid { get; set; }
        /// <summary>
        /// Device. Enumerable DTOContactTel.Cods (1.Phone, 2.Fax, 3.Cellular...)
        /// </summary>
        public short Cod { get; set; }
        /// <summary>
        /// ISO 3166-1 country  code
        /// </summary>
        public Guid Country { get; set; }
        /// <summary>
        /// Phone number
        /// </summary>
        public string Num { get; set; } = null!;
        /// <summary>
        /// If true this number is jkust for call identification number, we shouldn&apos;t use it to call.
        /// </summary>
        public bool Privat { get; set; }
        /// <summary>
        /// Comments; usually the owner person
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public short Ord { get; set; }

        public virtual CliGral CliGu { get; set; } = null!;
        public virtual Country CountryNavigation { get; set; } = null!;
    }
}
