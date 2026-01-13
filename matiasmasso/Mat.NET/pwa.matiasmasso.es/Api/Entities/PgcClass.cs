using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Recursive account groups
    /// </summary>
    public partial class PgcClass
    {
        public PgcClass()
        {
            PgcCta = new HashSet<PgcCtum>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Accounts plan; foreign key for PgcPlan table
        /// </summary>
        public Guid Plan { get; set; }
        /// <summary>
        /// Parent accounts group, foreign key for self table PgcClass
        /// </summary>
        public Guid? Parent { get; set; }
        /// <summary>
        /// Sort order within same parent
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// Depth level
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Name, Spanish language
        /// </summary>
        public string NomEsp { get; set; } = null!;
        /// <summary>
        /// Name, catalan language
        /// </summary>
        public string? NomCat { get; set; }
        /// <summary>
        /// Name, English language
        /// </summary>
        public string? NomEng { get; set; }
        /// <summary>
        /// DTOEnumerable DTOPgcClass.Cods, to programatically refer to it
        /// </summary>
        public int? Cod { get; set; }
        /// <summary>
        /// True if a report should not display amounts for this item
        /// </summary>
        public bool HideFigures { get; set; }
        /// <summary>
        /// Comma separated Cod field values which this item value is the sum result, if any
        /// </summary>
        public string? Sumandos { get; set; }

        public virtual PgcPlan PlanNavigation { get; set; } = null!;
        public virtual ICollection<PgcCtum> PgcCta { get; set; }
    }
}
