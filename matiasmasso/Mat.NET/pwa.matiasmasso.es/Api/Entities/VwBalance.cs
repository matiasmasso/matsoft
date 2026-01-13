using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Account balance and Profit&amp;loss reports
    /// </summary>
    public partial class VwBalance
    {
        public short? Emp { get; set; }
        public Guid ClassGuid { get; set; }
        public Guid? ClassParent { get; set; }
        public string ClassNomEsp { get; set; } = null!;
        public string? ClassNomCat { get; set; }
        public string? ClassNomEng { get; set; }
        public int? ClassCod { get; set; }
        public int ClassOrd { get; set; }
        public bool ClassHideFigures { get; set; }
        public Guid? CtaGuid { get; set; }
        public string? CtaId { get; set; }
        public string? CtaEsp { get; set; }
        public string? CtaCat { get; set; }
        public string? CtaEng { get; set; }
        public byte? CtaAct { get; set; }
        public int? CtaCod { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public string? YearMonth { get; set; }
        public decimal? Eur { get; set; }
    }
}
