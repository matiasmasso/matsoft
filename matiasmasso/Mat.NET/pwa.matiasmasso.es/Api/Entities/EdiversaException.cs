using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Exceptions thrown when validating Edi files
    /// </summary>
    public partial class EdiversaException
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Parent Edi message where the exception was thrown
        /// </summary>
        public Guid Parent { get; set; }
        /// <summary>
        /// Reason code. Enumerable DTOEdiversaException.Cods
        /// </summary>
        public int Cod { get; set; }
        /// <summary>
        /// Object the exception refers to
        /// </summary>
        public Guid? TagGuid { get; set; }
        /// <summary>
        /// Object type of TagGuid, enumerable DTOEdiversaException.TagCods
        /// </summary>
        public int? TagCod { get; set; }
        /// <summary>
        /// Exception message
        /// </summary>
        public string? Msg { get; set; }
        public string? Tag { get; set; }
    }
}
