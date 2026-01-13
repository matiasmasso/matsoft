using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Company phone lines
    /// </summary>
    public partial class LiniaTelefon
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Phone number
        /// </summary>
        public string Num { get; set; } = null!;
        /// <summary>
        /// Friendly name
        /// </summary>
        public string? Alias { get; set; }
        /// <summary>
        /// Owner company
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Line user
        /// </summary>
        public Guid? Contact { get; set; }
        /// <summary>
        /// SIM card Id on mobile phones
        /// </summary>
        public string? Icc { get; set; }
        /// <summary>
        /// IMEI mobile phone serial number
        /// </summary>
        public string? Imei { get; set; }
        /// <summary>
        /// Serial number
        /// </summary>
        public string? Serial { get; set; }
        /// <summary>
        /// Pin to unblock the phone device
        /// </summary>
        public string? Pin { get; set; }
        /// <summary>
        /// Puk to unblock the phone when the Pin is no longer enabled
        /// </summary>
        public string? Puk { get; set; }
        /// <summary>
        /// True if double phone number
        /// </summary>
        public string? Dual { get; set; }
        /// <summary>
        /// Start date
        /// </summary>
        public DateTime? Alta { get; set; }
        /// <summary>
        /// Termination date
        /// </summary>
        public DateTime? Baixa { get; set; }
        /// <summary>
        /// True if private number
        /// </summary>
        public bool Privat { get; set; }
    }
}
