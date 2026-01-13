using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Manufacturer repairs. The manufacturer picks up the product at customer location, repairs and returns it back
    /// </summary>
    public partial class SatRecall
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Support incidence. Foreign key for Incidencies table
        /// </summary>
        public Guid? Incidencia { get; set; }
        /// <summary>
        /// Description of the problem
        /// </summary>
        public string? Defect { get; set; }
        /// <summary>
        /// Enumerable DTOSatRecall.PickupFroms (1.Store, 2.Our waorshop, 3.Our warehouse)
        /// </summary>
        public int? PickupFrom { get; set; }
        /// <summary>
        /// Date we request the customer to prepare the goods for pickup
        /// </summary>
        public DateTime? FchCustomer { get; set; }
        /// <summary>
        /// Date the manufacturer ships the product back
        /// </summary>
        public DateTime? FchManufacturer { get; set; }
        /// <summary>
        /// Manufacturer tracking number for pickup
        /// </summary>
        public string? PickupRef { get; set; }
        /// <summary>
        /// Credit number is case the product has to be credited
        /// </summary>
        public string? CreditNum { get; set; }
        /// <summary>
        /// Credit date if any
        /// </summary>
        public DateTime? CreditFch { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// Pickup address
        /// </summary>
        public string? Adr { get; set; }
        /// <summary>
        /// Pickup zip; foreign key for Zip table
        /// </summary>
        public Guid? Zip { get; set; }
        /// <summary>
        /// Pickup contact person name
        /// </summary>
        public string? ContactPerson { get; set; }
        /// <summary>
        /// Pickup contact person phone number
        /// </summary>
        public string? Tel { get; set; }
        /// <summary>
        /// Enumerable DTOSatRecall.Modes (0.Credited, 1.Repaired)
        /// </summary>
        public int Mode { get; set; }
        /// <summary>
        /// Tracking number for shipment back from manufacturer to customer
        /// </summary>
        public string? ReturnRef { get; set; }
        /// <summary>
        /// Date of return back to customer
        /// </summary>
        public DateTime? ReturnFch { get; set; }
        /// <summary>
        /// Charge amount, if any
        /// </summary>
        public bool Carrec { get; set; }

        public virtual Incidency? IncidenciaNavigation { get; set; }
        public virtual Zip? ZipNavigation { get; set; }
    }
}
