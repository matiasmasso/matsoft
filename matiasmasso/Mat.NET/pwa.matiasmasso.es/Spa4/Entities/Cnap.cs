using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Product classification by function. Clasificacion Normalizada de Artículos de Puericultura
    /// </summary>
    public partial class Cnap
    {
        public Cnap()
        {
            Arts = new HashSet<Art>();
            InverseParentNavigation = new HashSet<Cnap>();
            Noticia = new HashSet<Noticium>();
            Stps = new HashSet<Stp>();
            Tpas = new HashSet<Tpa>();
            Depts = new HashSet<Dept>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Parent record; foreign key to self table
        /// </summary>
        public Guid? Parent { get; set; }
        /// <summary>
        /// Classification Id, it takes its first digits from parent and adds a last digit
        /// </summary>
        public string Id { get; set; } = null!;
        public string? Tags { get; set; }

        public virtual Cnap? ParentNavigation { get; set; }
        public virtual ICollection<Art> Arts { get; set; }
        public virtual ICollection<Cnap> InverseParentNavigation { get; set; }
        public virtual ICollection<Noticium> Noticia { get; set; }
        public virtual ICollection<Stp> Stps { get; set; }
        public virtual ICollection<Tpa> Tpas { get; set; }

        public virtual ICollection<Dept> Depts { get; set; }
    }
}
