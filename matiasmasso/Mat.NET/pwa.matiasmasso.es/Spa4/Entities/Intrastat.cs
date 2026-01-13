using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Customs monthly declaration of statistics on the exchange of goods between Member States of the European Union 
    /// </summary>
    public partial class Intrastat
    {
        public Intrastat()
        {
            Fras = new HashSet<Fra>();
            IntrastatPartida = new HashSet<IntrastatPartidum>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company. Foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Enumerable DTOIntrastat.Flujos: 0.Introduccion (input), 1.Expedicion (output)
        /// </summary>
        public int Flujo { get; set; }
        /// <summary>
        /// Declaration year
        /// </summary>
        public int Yea { get; set; }
        /// <summary>
        /// Declaration month
        /// </summary>
        public int Mes { get; set; }
        /// <summary>
        /// Consecutive number for different declarations from same Company, Year, Month and Flujo
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// Signature code received as response when uploading the Declaration online
        /// </summary>
        public string? Csv { get; set; }
        /// <summary>
        /// Pdf document, foreign key for Docfile table
        /// </summary>
        public string? Hash { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual DocFile? HashNavigation { get; set; }
        public virtual ICollection<Fra> Fras { get; set; }
        public virtual ICollection<IntrastatPartidum> IntrastatPartida { get; set; }
    }
}
