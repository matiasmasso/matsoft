using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Monthly balances per year each account
    /// </summary>
    public partial class VwCtaMe
    {
        public short Emp { get; set; }
        public int? Year { get; set; }
        public Guid? PgcClass { get; set; }
        public int CtaCod { get; set; }
        public Guid CtaGuid { get; set; }
        public string Id { get; set; } = null!;
        public byte Act { get; set; }
        public string Esp { get; set; } = null!;
        public string? Cat { get; set; }
        public string? Eng { get; set; }
        public decimal? D01 { get; set; }
        public decimal? H01 { get; set; }
        public decimal? D02 { get; set; }
        public decimal? H02 { get; set; }
        public decimal? D03 { get; set; }
        public decimal? H03 { get; set; }
        public decimal? D04 { get; set; }
        public decimal? H04 { get; set; }
        public decimal? D05 { get; set; }
        public decimal? H05 { get; set; }
        public decimal? D06 { get; set; }
        public decimal? H06 { get; set; }
        public decimal? D07 { get; set; }
        public decimal? H07 { get; set; }
        public decimal? D08 { get; set; }
        public decimal? H08 { get; set; }
        public decimal? D09 { get; set; }
        public decimal? H09 { get; set; }
        public decimal? D10 { get; set; }
        public decimal? H10 { get; set; }
        public decimal? D11 { get; set; }
        public decimal? H11 { get; set; }
        public decimal? D12 { get; set; }
        public decimal? H12 { get; set; }
    }
}
