using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    public partial class EdiInvrptHeader
    {
        public EdiInvrptHeader()
        {
            EdiInvrptItems = new HashSet<EdiInvrptItem>();
        }

        public Guid Guid { get; set; }
        public string? DocNum { get; set; }
        public DateTime Fch { get; set; }
        public Guid? Customer { get; set; }
        public DateTime FchCreated { get; set; }

        public virtual CliGral? CustomerNavigation { get; set; }
        public virtual ICollection<EdiInvrptItem> EdiInvrptItems { get; set; }
    }
}
