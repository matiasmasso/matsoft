using System;

namespace DTO
{
    public class DTOPgcGeo
    {
        public Guid Guid { get; set; }
        public string CtaId { get; set; }
        public string CtaNom { get; set; }
        public decimal Tot { get; set; }
        public decimal CEE { get; set; }
        public decimal Esp { get; set; }
        public decimal CCAA { get; set; }

        public decimal RestOfTheWorld()
        {
            return (Tot - CEE);
        }

    }
}
