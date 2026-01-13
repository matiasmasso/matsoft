using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Tax declaration documents
    /// </summary>
    public partial class Aeat
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company. Foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Foreign key for parent table Aeat_Mod
        /// </summary>
        public Guid Model { get; set; }
        /// <summary>
        /// Year of the declaration
        /// </summary>
        public int Yea { get; set; }
        /// <summary>
        /// Period within the year
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// Enumerable DTOAeatModel.PeriodTypes (mensual, trimestral, anual...)
        /// </summary>
        public short Tperiod { get; set; }
        /// <summary>
        /// Date of declaration
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Foreign key to document storage Docfile table
        /// </summary>
        public string? Hash { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual DocFile? HashNavigation { get; set; }
        public virtual AeatMod ModelNavigation { get; set; } = null!;
    }
}
