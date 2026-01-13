using System;

namespace DTO
{
    public class DTOCsbResult
    {
        public DateTime Vto { get; set; }
        public DTOCsb.Results Result { get; set; }
        public decimal Eur { get; set; }
    }
}
