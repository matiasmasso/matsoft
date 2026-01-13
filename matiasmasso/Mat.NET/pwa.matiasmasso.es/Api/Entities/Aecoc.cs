using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// EAN numbers assigned from our own range (AECOC=Asociación Española de Codificiación Comercial)
    /// </summary>
    public partial class Aecoc
    {
        /// <summary>
        /// EAN 13 code (primary key)
        /// </summary>
        public string Ean { get; set; } = null!;
        /// <summary>
        /// Target. It may be the foreign key to a Customer (for GLN code for Edi destinations) or a product Sku 
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Date the Ean code was generated
        /// </summary>
        public DateTime FchCreated { get; set; }
    }
}
