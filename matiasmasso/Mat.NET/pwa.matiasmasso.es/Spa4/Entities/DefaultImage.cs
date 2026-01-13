using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Default images for certain occasions
    /// </summary>
    public partial class DefaultImage
    {
        /// <summary>
        /// Enumerable DTO.Defaults.ImgTypes
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Image for this code
        /// </summary>
        public byte[] Image { get; set; } = null!;
    }
}
