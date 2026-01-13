using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTORegistroJornadaLaboral : DTOBaseGuid
    {
        public DTOStaff Staff { get; set; }
        public DTOYearMonth YearMonth { get; set; }
        public decimal Hours { get; set; }
        public DTODocFile DocFile { get; set; }

        public DTORegistroJornadaLaboral() : base()
        {
            YearMonth = new DTOYearMonth();
        }
        public DTORegistroJornadaLaboral(Guid guid) : base(guid)
        {
            YearMonth = new DTOYearMonth();
        }


        public class Collection : List<DTORegistroJornadaLaboral>
        {

        }
    }


}
