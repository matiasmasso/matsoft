using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Line items from orders received through Edi messages
    /// </summary>
    public partial class EdiversaOrderItem
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Foreign key to parent table EdiversaOrderHeader
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Line number
        /// </summary>
        public int Lin { get; set; }
        /// <summary>
        /// Product EAN 13 code
        /// </summary>
        public string? Ean { get; set; }
        /// <summary>
        /// Customer product code
        /// </summary>
        public string? RefClient { get; set; }
        /// <summary>
        /// Manufacturer product code
        /// </summary>
        public string? RefProveidor { get; set; }
        /// <summary>
        /// Product; foireign key to Art table
        /// </summary>
        public Guid? Sku { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        public string? Dsc { get; set; }
        /// <summary>
        /// Units
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal? Preu { get; set; }
        /// <summary>
        /// Discount
        /// </summary>
        public decimal? Dto { get; set; }
        /// <summary>
        /// If price differs from standard, user who authorized it. Foreign key to Email table
        /// </summary>
        public Guid? SkipPreuValidationUser { get; set; }
        /// <summary>
        /// Date when the user authorised a different price, if any
        /// </summary>
        public DateTime? SkipPreuValidationFch { get; set; }
        /// <summary>
        /// if discount differs from standard, user who autorized it. Foreign key to Email table
        /// </summary>
        public Guid? SkipDtoValidationUser { get; set; }
        /// <summary>
        /// Date it was authorized a different discount, if any
        /// </summary>
        public DateTime? SkipDtoValidationFch { get; set; }
        /// <summary>
        /// If this line should not be processed, user who authorized it. Foreign key to Email table
        /// </summary>
        public Guid? SkipItemUser { get; set; }
        /// <summary>
        /// Date it was denied to be processed
        /// </summary>
        public DateTime? SkipItemFch { get; set; }

        public virtual EdiversaOrderHeader ParentNavigation { get; set; } = null!;
        public virtual Email? SkipDtoValidationUserNavigation { get; set; }
        public virtual Email? SkipItemUserNavigation { get; set; }
        public virtual Email? SkipPreuValidationUserNavigation { get; set; }
        public virtual Art? SkuNavigation { get; set; }
        public virtual EdiversaOrdrspItem EdiversaOrdrspItem { get; set; } = null!;
    }
}
