using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Delivery note items table for shipped goods, either received or sent
    /// </summary>
    public partial class Arc
    {
        public Arc()
        {
            InverseBundleNavigation = new HashSet<Arc>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Enumerable DeliveryItem.Cods. Input or output and purpose
        /// </summary>
        public short Cod { get; set; }
        /// <summary>
        /// Foreign key to parent table Alb
        /// </summary>
        public Guid AlbGuid { get; set; }
        /// <summary>
        /// Line number
        /// </summary>
        public int Lin { get; set; }
        /// <summary>
        /// Units delivered
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// Unitary price in Euros
        /// </summary>
        public decimal? Eur { get; set; }
        /// <summary>
        /// Currency in ISO 4217
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Price in foreign currency
        /// </summary>
        public decimal? Pts { get; set; }
        /// <summary>
        /// Discount on price
        /// </summary>
        public decimal Dto { get; set; }
        public decimal Dt2 { get; set; }
        /// <summary>
        /// Net price
        /// </summary>
        public decimal Net { get; set; }
        /// <summary>
        /// Stock in this moment, in order to calculate the average price for inventory
        /// </summary>
        public int Stk { get; set; }
        /// <summary>
        /// Unit average cost price, for inventory
        /// </summary>
        public decimal Pmc { get; set; }
        /// <summary>
        /// Agent commission
        /// </summary>
        public decimal? Com { get; set; }
        /// <summary>
        /// Enumerable DTOTax.Codis (standard, reduced, exempt...)
        /// </summary>
        public byte IvaCod { get; set; }
        /// <summary>
        /// Product sku. Foreign key to Art table
        /// </summary>
        public Guid ArtGuid { get; set; }
        /// <summary>
        /// Purchase order item. Foreign key to Pnc table but not linked since it gives conflict problems when deleting Arc
        /// </summary>
        public Guid? PncGuid { get; set; }
        /// <summary>
        /// Purchase order. Foreign key to Pdc table
        /// </summary>
        public Guid? PdcGuid { get; set; }
        /// <summary>
        /// Service. Foreign key to Spv table
        /// </summary>
        public Guid? SpvGuid { get; set; }
        /// <summary>
        /// Commercial agent earning a commission. Foreign key to CliGral table
        /// </summary>
        public Guid? RepGuid { get; set; }
        /// <summary>
        /// Foreign key to Rps table which lists what delivery items are to be liquidated to what agent
        /// </summary>
        public Guid? RepComLiquidable { get; set; }
        /// <summary>
        /// Related to promos. Foreign key to Incentiu table
        /// </summary>
        public Guid? Incentiu { get; set; }
        /// <summary>
        /// Warehouse. Foreign key for CliGral table
        /// </summary>
        public Guid? MgzGuid { get; set; }
        /// <summary>
        /// Bundle is a virtual agregation of different products sold at once.
        /// Deliveries register one item for the bundle itself (parent bundle) and one item for each component (child bundle)
        /// Parent bundles get on bundle field their Guid value
        /// Children bundles get on bundle field their parent bundle Guid value
        /// </summary>
        public Guid? Bundle { get; set; }

        public virtual Alb AlbGu { get; set; } = null!;
        public virtual Arc? BundleNavigation { get; set; }
        public virtual Pdc? PdcGu { get; set; }
        public virtual ICollection<Arc> InverseBundleNavigation { get; set; }
    }
}
