using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Digital Certificates
    /// </summary>
    public partial class CliCert
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Byte array
        /// </summary>
        public byte[] Stream { get; set; } = null!;
        /// <summary>
        /// Password
        /// </summary>
        public string Pwd { get; set; } = null!;
        /// <summary>
        /// File extension
        /// </summary>
        public string Ext { get; set; } = null!;
        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime Caduca { get; set; }
        /// <summary>
        /// Signature image
        /// </summary>
        public byte[]? Image { get; set; }

        public virtual CliGral Gu { get; set; } = null!;
    }
}
