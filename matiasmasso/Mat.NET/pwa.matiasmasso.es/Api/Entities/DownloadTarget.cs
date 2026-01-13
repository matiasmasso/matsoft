using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Used to specify different targets for a single document
    /// </summary>
    public partial class DownloadTarget
    {
        /// <summary>
        /// foreign key to ProductDownload
        /// </summary>
        public Guid Download { get; set; }
        /// <summary>
        /// Document owner
        /// </summary>
        public Guid Target { get; set; }
        /// <summary>
        /// Type of owner; enumerable DTOBaseGuidCodNom.Cods (1.vehicle, 2.product brand, 3.product category, 4.product sku, 5.phone line...)
        /// </summary>
        public int Cod { get; set; }

        public virtual ProductDownload DownloadNavigation { get; set; } = null!;
    }
}
