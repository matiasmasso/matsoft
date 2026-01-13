using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Log provided by our logistic center reporting package transport tracking and costs
    /// </summary>
    public partial class DeliveryTracking
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company; foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Message received. Foreign key to JsonLog table
        /// </summary>
        public Guid? Log { get; set; }
        /// <summary>
        /// Date 
        /// </summary>
        public DateTime? Fch { get; set; }
        /// <summary>
        /// VAT number of the sender partner
        /// </summary>
        public string? Sender { get; set; }
        /// <summary>
        /// Our delivery number, formated YYYYNNNNNN
        /// </summary>
        public string? Delivery { get; set; }
        /// <summary>
        /// Total number of packages
        /// </summary>
        public int? Packages { get; set; }
        /// <summary>
        /// VAT number of the forwarder company
        /// </summary>
        public string? Forwarder { get; set; }
        /// <summary>
        /// Number of pallets, if any
        /// </summary>
        public int? Pallets { get; set; }
        /// <summary>
        /// Shipment tracking number
        /// </summary>
        public string? Tracking { get; set; }
        /// <summary>
        /// Weight adjusted by forwarder rate Kg/m3
        /// </summary>
        public decimal? CubicKg { get; set; }
        /// <summary>
        /// Weight in Kg
        /// </summary>
        public decimal? Weight { get; set; }
        /// <summary>
        /// Volume in m3
        /// </summary>
        public decimal? Volume { get; set; }
        /// <summary>
        /// Cost in Eur
        /// </summary>
        public decimal? Cost { get; set; }
        /// <summary>
        /// Package number
        /// </summary>
        public int? Package { get; set; }
        /// <summary>
        /// Package plate
        /// </summary>
        public string? Sscc { get; set; }
        /// <summary>
        /// Packaging (logistic center custom code)
        /// </summary>
        public string? Packaging { get; set; }
        /// <summary>
        /// Package length, mm
        /// </summary>
        public int? Length { get; set; }
        /// <summary>
        /// Package width, mm
        /// </summary>
        public int? Width { get; set; }
        /// <summary>
        /// Package height, mm
        /// </summary>
        public int? Height { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual JsonLog? LogNavigation { get; set; }
    }
}
