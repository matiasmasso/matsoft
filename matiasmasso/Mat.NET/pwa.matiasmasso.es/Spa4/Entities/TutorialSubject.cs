using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Groups different tutorials into subjects
    /// </summary>
    public partial class TutorialSubject
    {
        public TutorialSubject()
        {
            Tutorials = new HashSet<Tutorial>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Subject title
        /// </summary>
        public string Nom { get; set; } = null!;

        public virtual ICollection<Tutorial> Tutorials { get; set; }
    }
}
