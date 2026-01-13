using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Text resources in different languages
    /// </summary>
    public partial class LangText
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Pkey { get; set; }
        /// <summary>
        /// Target; foreign key to different target tables
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Enumerable DTOLangText.Srcs
        /// </summary>
        public int Src { get; set; }
        /// <summary>
        /// ISO 639-2 Language code
        /// </summary>
        public string Lang { get; set; } = null!;
        /// <summary>
        /// Text resource
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime FchCreated { get; set; }
    }
}
