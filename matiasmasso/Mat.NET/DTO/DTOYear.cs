using System;

namespace DTO
{
    public class DTOYear : DTOBaseGuid
    {
        public int Year { get; set; }

        public DTOYear() : base()
        {
        }

        public DTOYear(Guid oGuid) : base(oGuid)
        {
        }
    }
}
