using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class ContactGln
    {
        public ContactGln()
        {
            EdiInvrptHeaderNadgyNavigations = new HashSet<EdiInvrptHeader>();
            EdiInvrptHeaderNadmsNavigations = new HashSet<EdiInvrptHeader>();
        }

        /// <summary>
        /// Global Location Number for EDI partner identification
        /// </summary>
        public string Gln { get; set; } = null!;
        public Guid? Contact { get; set; }

        public virtual CliGral? ContactNavigation { get; set; }
        public virtual ICollection<EdiInvrptHeader> EdiInvrptHeaderNadgyNavigations { get; set; }
        public virtual ICollection<EdiInvrptHeader> EdiInvrptHeaderNadmsNavigations { get; set; }
    }
}
