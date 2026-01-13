using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// High resolution brand resources, filed in the server by its MD5 hash signature as filename
    /// </summary>
    public partial class Gallery
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Resource name
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Thumbnail
        /// </summary>
        public byte[] Image { get; set; } = null!;
        public byte[]? Thumbnail { get; set; }
        /// <summary>
        /// Original width, in pixels
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Original height, in pixels
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Weu¡ight, in bytes
        /// </summary>
        public int Kb { get; set; }
        /// <summary>
        /// Mime type, enumerable MatHelperStd.Enums.MimeCods
        /// </summary>
        public int Mime { get; set; }
        /// <summary>
        /// Entry creation date
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// MD5 signature hash of the resource
        /// </summary>
        public string? Hash { get; set; }
    }
}
