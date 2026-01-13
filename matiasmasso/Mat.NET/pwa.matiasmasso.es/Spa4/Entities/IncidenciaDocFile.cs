using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Postsales incidence support documents
    /// </summary>
    public partial class IncidenciaDocFile
    {
        /// <summary>
        /// Foreign key to parent table Incidencies
        /// </summary>
        public Guid Incidencia { get; set; }
        /// <summary>
        /// Foreign key to documents table Docfile
        /// </summary>
        public string Hash { get; set; } = null!;
        /// <summary>
        /// Enumerable DTOIncidencia.AttachmentCods (0.ticket, 1.image, 2.video)
        /// </summary>
        public int Cod { get; set; }

        public virtual DocFile HashNavigation { get; set; } = null!;
    }
}
