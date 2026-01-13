using System;
using System.Collections.Generic;

namespace Api.Entities
{
    public partial class ContactGlnDeprecated
    {
        /// <summary>
        /// Global Location Number for EDI partner identification
        /// </summary>
        public string Gln { get; set; } = null!;
        public Guid? Contact { get; set; }

        public virtual CliGral? ContactNavigation { get; set; }
    }
}
