using System;

namespace DTO
{
    public class DTOImportTransit : DTOImportacio
    {
        public DTOYearMonth YearMonthFras { get; set; }
        public DTOYearMonth YearMonthAlbs { get; set; }

        public DTOImportTransit() : base()
        {
        }

        public DTOImportTransit(Guid oGuid) : base(oGuid)
        {
        }
    }
}
