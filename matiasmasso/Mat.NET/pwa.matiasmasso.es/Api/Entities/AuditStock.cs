using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Stocks reported by logistic center in order to match our records
    /// </summary>
    public partial class AuditStock
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Report year
        /// </summary>
        public int Yea { get; set; }
        /// <summary>
        /// Sequential number unique per Company/product Sku which logistic center uses as primary key. Stored on Art field from Art table
        /// </summary>
        public int? Ref { get; set; }
        /// <summary>
        /// Product sku; foreign key for Art table
        /// </summary>
        public Guid? Sku { get; set; }
        /// <summary>
        /// Logistic center product sku description
        /// </summary>
        public string? Dsc { get; set; }
        /// <summary>
        /// Units is stock reported by the logistic center
        /// </summary>
        public int? Qty { get; set; }
        /// <summary>
        /// Palet plate number
        /// </summary>
        public string? Palet { get; set; }
        /// <summary>
        /// Date of entrance into logistic center
        /// </summary>
        public DateTime? FchEntrada { get; set; }
        /// <summary>
        /// Days kept in stock, so we can devaluate it if it taklse too long to be sold
        /// </summary>
        public int? Dias { get; set; }
        /// <summary>
        /// Logistic center entrance reference
        /// </summary>
        public string? Entrada { get; set; }
        /// <summary>
        /// Product origin
        /// </summary>
        public string? Procedencia { get; set; }
        /// <summary>
        /// Product cost in order to value the inventory
        /// </summary>
        public decimal? Cost { get; set; }
        /// <summary>
        /// Report date
        /// </summary>
        public DateTime? Fch { get; set; }

        public virtual Art? SkuNavigation { get; set; }
    }
}
