using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class Contact
    {
        public Contact()
        {
            DocFileTags = new HashSet<DocFileTag>();
            PersonMareNavigations = new HashSet<Person>();
            PersonPareNavigations = new HashSet<Person>();
        }

        public Guid Guid { get; set; }
        public int IdOld { get; set; }
        public string Nom { get; set; } = null!;
        public string? Obs { get; set; }
        public int Codi { get; set; }
        public DateTime FchCreated { get; set; }

        public virtual ICollection<DocFileTag> DocFileTags { get; set; }
        public virtual ICollection<Person> PersonMareNavigations { get; set; }
        public virtual ICollection<Person> PersonPareNavigations { get; set; }
    }
}
