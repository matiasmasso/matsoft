using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Accessories and spares for each product
    /// </summary>
    public partial class ArtSpare
    {
        /// <summary>
        /// Relationship. Enumerable DTOProduct.Relateds 1.accessory, 2.spare
        /// </summary>
        public byte Cod { get; set; }
        /// <summary>
        /// Target product (parent), for which children accessories or spares exist. Foreign key for Art table
        /// </summary>
        public Guid TargetGuid { get; set; }
        /// <summary>
        /// Children product (the accessory or spare part itself). Foreign key for Art table
        /// </summary>
        public Guid ProductGuid { get; set; }

        public virtual Art ProductGu { get; set; } = null!;
    }
}
