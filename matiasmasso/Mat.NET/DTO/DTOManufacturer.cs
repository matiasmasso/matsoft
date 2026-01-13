using System;

namespace DTO
{
    public class DTOManufacturer : DTOContact
    {
        public DTOManufacturer() : base()
        {
        }

        public DTOManufacturer(Guid oGuid) : base(oGuid)
        {
        }
    }
}
