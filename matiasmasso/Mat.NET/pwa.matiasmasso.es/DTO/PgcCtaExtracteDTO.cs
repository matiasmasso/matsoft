using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PgcCtaExtracteDTO
    {
        public LangDTO Lang { get; set; }
        public int Emp { get; set; }
        public int Year { get; set; }
        public GuidNom Cta { get; set; }
        public int CtaAct { get; set; }
        public GuidNom? Contact { get; set; }
        public List<Item> Items { get; set; } = new();
        public class Item
        {
            public Guid CcbGuid { get; set; }
            public Guid CcaGuid { get; set; }
            public int CcaId { get; set; }
            public string? Concept { get; set; }
            public DateOnly Fch { get; set; }
            public Decimal Eur { get; set; }
            public int DH { get; set; }
            public bool HasDoc { get; set; }
            public decimal Saldo { get; set; } = (decimal)123.45;

            public decimal Deb() => DH == 1 ? Eur : 0;
            public decimal Hab() => DH == 2 ? Eur : 0;
            public int Ord { get; set; }

            public string DownloadUrl() => Globals.ApiUrl("cca/pdf", CcaGuid.ToString());


            public bool Matches(string searchterm)
            {
                bool retval=false;
                if (string.IsNullOrEmpty(searchterm)) {
                    retval=true ;
                } else if( Concept.Contains(searchterm, StringComparison.OrdinalIgnoreCase)) {
                    retval=true ;
                } else if( decimal.TryParse(searchterm, out decimal n))
                {
                    retval = Eur == n;
                }
                return retval;
            }
        }
    }
}
