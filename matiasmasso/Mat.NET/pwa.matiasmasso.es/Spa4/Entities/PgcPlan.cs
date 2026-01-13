using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Accounts plan
    /// </summary>
    public partial class PgcPlan
    {
        public PgcPlan()
        {
            InverseLastPlanNavigation = new HashSet<PgcPlan>();
            PgcClasses = new HashSet<PgcClass>();
            PgcCta = new HashSet<PgcCtum>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Friendly name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Year of activation
        /// </summary>
        public int YearFrom { get; set; }
        /// <summary>
        /// Last year of validity
        /// </summary>
        public int? YearTo { get; set; }
        /// <summary>
        /// Previous plan; foreign key for self table PgcPlan
        /// </summary>
        public Guid? LastPlan { get; set; }

        public virtual PgcPlan? LastPlanNavigation { get; set; }
        public virtual ICollection<PgcPlan> InverseLastPlanNavigation { get; set; }
        public virtual ICollection<PgcClass> PgcClasses { get; set; }
        public virtual ICollection<PgcCtum> PgcCta { get; set; }
    }
}
