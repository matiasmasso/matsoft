using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Spanish Tax Agency form models (AEAT=Agencia Estatal de Administración Tributaria)
    /// </summary>
    public partial class AeatMod
    {
        public AeatMod()
        {
            Aeats = new HashSet<Aeat>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Model name
        /// </summary>
        public string Mod { get; set; } = null!;
        /// <summary>
        /// Model description
        /// </summary>
        public string Dsc { get; set; } = null!;
        /// <summary>
        /// Enumerable DTOAeatModel.PeriodTypes (mensual, trimestral, anual...)
        /// </summary>
        public int Tperiod { get; set; }
        /// <summary>
        /// If true, no economic transaction applicable, just informative declaration
        /// </summary>
        public bool SoloInfo { get; set; }
        /// <summary>
        /// Specific for PYMEs (small companies)
        /// </summary>
        public bool? Pyme { get; set; }
        /// <summary>
        /// If true, specific form for big companies
        /// </summary>
        public bool? GranEmpresa { get; set; }
        /// <summary>
        /// Purpose. Ennumerable DTOAeatModel.Cods
        /// </summary>
        public int? Cod { get; set; }
        /// <summary>
        /// Our extranet publishes some tax declarations depending on the users rol.
        /// This field controls which models are accessible to bank employees.
        /// </summary>
        public bool VisibleBancs { get; set; }
        /// <summary>
        /// If true, the model is outdated and no longer used
        /// </summary>
        public bool Obsolet { get; set; }

        public virtual ICollection<Aeat> Aeats { get; set; }
    }
}
