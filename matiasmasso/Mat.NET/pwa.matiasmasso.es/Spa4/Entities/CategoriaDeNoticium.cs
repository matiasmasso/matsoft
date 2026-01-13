using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// News categories
    /// </summary>
    public partial class CategoriaDeNoticium
    {
        public CategoriaDeNoticium()
        {
            Noticia = new HashSet<Noticium>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Category name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Category description
        /// </summary>
        public string? Excerpt { get; set; }

        public virtual ICollection<Noticium> Noticia { get; set; }
    }
}
