using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Links objects with documents
    /// </summary>
    public partial class DocFileSrc
    {
        /// <summary>
        /// Target object; foreign key to different tables like Vehicle, Mem, Immoble...
        /// </summary>
        public Guid SrcGuid { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int SrcOrd { get; set; }
        /// <summary>
        /// Document; foreign key for Docfile table
        /// </summary>
        public string Hash { get; set; } = null!;
        /// <summary>
        /// Type of target object, enumerable DTODocfile.Cods
        /// </summary>
        public int SrcCod { get; set; }

        public virtual DocFile HashNavigation { get; set; } = null!;
    }
}
