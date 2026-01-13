using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Customer orrders which have not been liquidated yet to the agent
    /// </summary>
    public partial class VwRepPncsLiqPending
    {
        /// <summary>
        /// Purchase order item. Foreign key to Pnc table
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company. Foreign key to Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Purchase order. Foreign key to Pdc table
        /// </summary>
        public Guid PdcGuid { get; set; }
        /// <summary>
        /// Customer. Foreign key to CliGral table
        /// </summary>
        public Guid CliGuid { get; set; }
        /// <summary>
        /// Order number
        /// </summary>
        public int PdcNum { get; set; }
        /// <summary>
        /// Order date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Purchase order item line
        /// </summary>
        public int Lin { get; set; }
        /// <summary>
        /// Agent. Foreign key to CliGral table
        /// </summary>
        public Guid? RepGuid { get; set; }
        /// <summary>
        /// Commission percentage
        /// </summary>
        public decimal? Com { get; set; }
        /// <summary>
        /// If true, conditions (rep or commission) are different from default and should not be overriden during validation process
        /// </summary>
        public bool? RepCustom { get; set; }
        public string FullNom { get; set; } = null!;
        /// <summary>
        /// Contact classification upon activity
        /// </summary>
        public Guid? ContactClass { get; set; }
        /// <summary>
        /// Distribution channel (hypermarket, chain, independant, e-commerce...)
        /// </summary>
        public Guid? DistributionChannel { get; set; }
        /// <summary>
        /// Customer to invoice, if different from default. Foreign key to CliGral
        /// </summary>
        public Guid? CcxGuid { get; set; }
        /// <summary>
        /// Alias (short name) for the agent
        /// </summary>
        public string? Abr { get; set; }
        /// <summary>
        /// If true, no rep should earn a commission for this item
        /// </summary>
        public bool NoRep { get; set; }
        /// <summary>
        /// Customer Postal code, foreign key to Zip table
        /// </summary>
        public Guid? ZipGuid { get; set; }
        /// <summary>
        /// Customer Location, foreign key to Location table
        /// </summary>
        public Guid LocationGuid { get; set; }
        /// <summary>
        /// Customer Zone, foerign key to Zona table
        /// </summary>
        public Guid ZonaGuid { get; set; }
        /// <summary>
        /// Customer Country, foreign key to Country table
        /// </summary>
        public Guid CountryGuid { get; set; }
        public Guid BrandGuid { get; set; }
        public Guid CategoryGuid { get; set; }
        public Guid SkuGuid { get; set; }
        public string? SkuNomLlarg { get; set; }
    }
}
