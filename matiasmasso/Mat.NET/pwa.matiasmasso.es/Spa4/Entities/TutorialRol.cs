using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// User rols target for this tutorial
    /// </summary>
    public partial class TutorialRol
    {
        /// <summary>
        /// Foreign key for Tutorial table
        /// </summary>
        public Guid Tutorial { get; set; }
        /// <summary>
        /// User rol; foreign key for UsrRols
        /// </summary>
        public int Rol { get; set; }

        public virtual Tutorial TutorialNavigation { get; set; } = null!;
    }
}
