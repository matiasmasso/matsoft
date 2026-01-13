using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Unpayments
    /// </summary>
    public partial class VwImpagat
    {
        public Guid CliGuid { get; set; }
        public string RaoSocial { get; set; } = null!;
        public decimal? Expr1 { get; set; }
    }
}
