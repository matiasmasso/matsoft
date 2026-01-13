using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Terms and conditions chapters
    /// </summary>
    public partial class CondCapitol
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Parent id, foreign key to parent table Cond
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Chapter order
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// User who edited this chapter for last time
        /// </summary>
        public Guid? UsrLastEdited { get; set; }
        /// <summary>
        /// Date and time this chapter was edited for last time
        /// </summary>
        public DateTime FchLastEdited { get; set; }
        /// <summary>
        /// User who registered this chapter for first time
        /// </summary>
        public Guid? UsrCreated { get; set; }
        /// <summary>
        /// Date this chapter was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual Cond ParentNavigation { get; set; } = null!;
        public virtual Email? UsrCreatedNavigation { get; set; }
        public virtual Email? UsrLastEditedNavigation { get; set; }
    }
}
