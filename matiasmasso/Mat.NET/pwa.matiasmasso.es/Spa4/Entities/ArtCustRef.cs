using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Customer product code equivalences
    /// </summary>
    public partial class ArtCustRef
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Customer id; foreign key for CliGral table
        /// </summary>
        public Guid CliGuid { get; set; }
        /// <summary>
        /// Produc tsku id; foreign key for Art table
        /// </summary>
        public Guid ArtGuid { get; set; }
        /// <summary>
        /// Customer code for this product sku
        /// </summary>
        public string Ref { get; set; } = null!;
        /// <summary>
        /// DUN14 bar code
        /// </summary>
        public string? Dun14 { get; set; }
        /// <summary>
        /// Customer product description
        /// </summary>
        public string? Dsc { get; set; }
        /// <summary>
        /// Color
        /// </summary>
        public string? Color { get; set; }
        public Guid? CustomerDept { get; set; }
        /// <summary>
        /// Date of code generation
        /// </summary>
        public DateTime? FchFrom { get; set; }
        /// <summary>
        /// Date when this code turned outdated
        /// </summary>
        public DateTime? FchTo { get; set; }
        /// <summary>
        /// Date this record was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual Art ArtGu { get; set; } = null!;
    }
}
