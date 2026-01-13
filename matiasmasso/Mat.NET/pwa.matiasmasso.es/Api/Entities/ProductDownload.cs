using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Product documents
    /// </summary>
    public partial class ProductDownload
    {
        public ProductDownload()
        {
            DownloadTargets = new HashSet<DownloadTarget>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Target object owner of the document, mainly a product
        /// </summary>
        public Guid? Product { get; set; }
        /// <summary>
        /// Foreign key for Pdf document DocFile table
        /// </summary>
        public string Hash { get; set; } = null!;
        /// <summary>
        /// Enumerable DTOProductDownload.TargetCods (0.Product)
        /// </summary>
        public int Target { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// Type of document; enumerable DTOProductDownload.Srcs
        /// </summary>
        public int Src { get; set; }
        /// <summary>
        /// ISO 639-2 language code
        /// </summary>
        public string? Lang { get; set; }
        /// <summary>
        /// True if visible to consumer
        /// </summary>
        public bool? PublicarAlConsumidor { get; set; }
        /// <summary>
        /// True if visible to proffessional
        /// </summary>
        public bool? PublicarAlDistribuidor { get; set; }
        /// <summary>
        /// True if outdated
        /// </summary>
        public bool Obsoleto { get; set; }

        public virtual DocFile HashNavigation { get; set; } = null!;
        public virtual ICollection<DownloadTarget> DownloadTargets { get; set; }
    }
}
