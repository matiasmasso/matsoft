using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Orders received through Edi messages
    /// </summary>
    public partial class EdiversaOrderHeader
    {
        public EdiversaOrderHeader()
        {
            EdiversaOrderItems = new HashSet<EdiversaOrderItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Order number
        /// </summary>
        public string DocNum { get; set; } = null!;
        /// <summary>
        /// Order date
        /// </summary>
        public DateTime FchDoc { get; set; }
        /// <summary>
        /// Delivery date
        /// </summary>
        public DateTime? FchDelivery { get; set; }
        /// <summary>
        /// Delivery deadline
        /// </summary>
        public DateTime? FchLast { get; set; }
        /// <summary>
        /// Document name code, as defined in https://www.gs1.org/sites/default/files/docs/eancom/s4/orders.pdf
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// Third field of &quot;ORD&quot; segment from Ediversa ORDERS message
        /// </summary>
        public int Funcion { get; set; }
        /// <summary>
        /// Supplier. GLN EAN 13 code from NADSU segment
        /// </summary>
        public Guid? Proveedor { get; set; }
        /// <summary>
        /// Buyer. GLN EAN 13 code from NADBY segment
        /// </summary>
        public string CompradorEan { get; set; } = null!;
        /// <summary>
        /// Buyer. Foreign key for CliGral table
        /// </summary>
        public Guid? Comprador { get; set; }
        /// <summary>
        /// Customer who should be invoiced for this order. GLN EAN 13 code from NADIV segment
        /// </summary>
        public string? FacturarAean { get; set; }
        /// <summary>
        /// Customer who should be invoiced for this order. Foreign key for CliGral table
        /// </summary>
        public Guid? FacturarA { get; set; }
        /// <summary>
        /// Deliver point. GLN EAN 13 code for NADDP segment
        /// </summary>
        public string ReceptorMercanciaEan { get; set; } = null!;
        /// <summary>
        /// Deliver point. Foreign key for CliGral table
        /// </summary>
        public Guid? ReceptorMercancia { get; set; }
        /// <summary>
        /// Order value in foreign currency
        /// </summary>
        public decimal Val { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Order value in Euros
        /// </summary>
        public decimal Eur { get; set; }
        /// <summary>
        /// For El Corte Ingles, sales center
        /// </summary>
        public string? Centro { get; set; }
        /// <summary>
        /// For El Corte Ingles, Department
        /// </summary>
        public string? Departamento { get; set; }
        /// <summary>
        /// Comments. Contents from FTX segment
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// Purchase order. Foreign key for Pdc table
        /// </summary>
        public Guid? Result { get; set; }
        /// <summary>
        /// Date and time this record was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Edi message sender (to be used as message receiver NADMR on DESADV messages)
        /// </summary>
        public string? Nadms { get; set; }

        public virtual CliGral? CompradorNavigation { get; set; }
        public virtual CliGral? FacturarANavigation { get; set; }
        public virtual CliGral? ProveedorNavigation { get; set; }
        public virtual CliGral? ReceptorMercanciaNavigation { get; set; }
        public virtual Pdc? ResultNavigation { get; set; }
        public virtual ICollection<EdiversaOrderItem> EdiversaOrderItems { get; set; }
    }
}
