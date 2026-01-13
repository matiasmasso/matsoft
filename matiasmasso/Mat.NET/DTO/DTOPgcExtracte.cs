using System.Collections.Generic;

namespace DTO
{
    public class DTOPgcExtracte
    {
        public DTOExercici Exercici { get; set; }
        public DTOPgcCta Cta { get; set; }
        public DTOContact Contact { get; set; }
        public List<DTOCcb> items { get; set; }
    }
}
