using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// pre-defined commercial terms published by the International Chamber of Commerce (ICC)
    /// </summary>
    public partial class Incoterm
    {
        public Incoterm()
        {
            CliClients = new HashSet<CliClient>();
            Fras = new HashSet<Fra>();
            IntrastatPartida = new HashSet<IntrastatPartidum>();
        }

        /// <summary>
        /// Primary key, Incoterm acronym
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Description
        /// </summary>
        public string? Nom { get; set; }

        public virtual ICollection<CliClient> CliClients { get; set; }
        public virtual ICollection<Fra> Fras { get; set; }
        public virtual ICollection<IntrastatPartidum> IntrastatPartida { get; set; }
    }
}
