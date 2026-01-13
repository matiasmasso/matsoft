using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Exceptions from different operations that need to be recorded. For example, validation exceptions detected when importing invoices received from Edi console
    /// </summary>
    public partial class Exception
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Object target of the exception. Foreign key to different tables depending on the operation
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Versatile code each target object may refer to with its own enumerable
        /// </summary>
        public int? Cod { get; set; }
        /// <summary>
        /// Optional object to be used when proposing solutions to the exception
        /// </summary>
        public Guid? Tag { get; set; }
        /// <summary>
        /// Descriptive message to the user
        /// </summary>
        public string? Msg { get; set; }
    }
}
