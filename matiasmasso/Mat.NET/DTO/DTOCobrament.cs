using System.Collections.Generic;

namespace DTO
{
    public class DTOCobrament
    {
        public DTOCca Cca { get; set; }
        public List<DTOPnd> Pnds { get; set; }
        public List<DTOImpagat> Impagats { get; set; }
    }
}
