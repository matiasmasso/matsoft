using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Errors received from Redsys Api. Redsys is our bank payment gateway platform for customer payments through credit cards
    /// </summary>
    public partial class RedsysErr
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Error description
        /// </summary>
        public string ErrDsc { get; set; } = null!;
    }
}
