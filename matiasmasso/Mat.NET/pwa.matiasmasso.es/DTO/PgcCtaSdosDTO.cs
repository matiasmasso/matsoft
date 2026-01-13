using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{

    public class PgcCtaSdoModel:IModel
    {
        public Guid Guid { get; set; }
        public DateOnly Fch { get; set; }
        public Guid Cta { get; set; }
        public Guid? Contact { get; set; }
        public decimal Deb { get; set; }
        public decimal Hab { get; set; }

        public PgcCtaSdoModel() {
            Guid = Guid.NewGuid();

        }

        public bool Matches(string? searchterm)
        {
            return true;
        }
    }

    public class PgcCtaSdosDTO
    {
        public GuidNom? Contact { get; set; }
        public List<Item> Items { get; set; } = new();


        public class Item
        {
            public int Year { get; set; }
            public Guid CtaGuid { get; set; }
            public string? CtaId { get; set; }
            public string? CtaNom { get; set; }
            public int CtaCod { get; set; }
            public int CtaAct { get; set; }
            public decimal? Deb { get; set; }
            public decimal? Hab { get; set; }

            public decimal Sdo()
            {
                decimal retval;
                if (CtaAct == 1)
                    retval = (Deb ?? 0) - (Hab ?? 0);
                else
                    retval = (Hab ?? 0) - (Deb ?? 0);
                return retval;
            }

            public string Caption() => string.Format("{0} {1}", CtaId, CtaNom);
        }
    }
}
