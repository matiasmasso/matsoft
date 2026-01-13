using System;

namespace DTO
{
    public class DTOCompactGuid
    {
        public Guid Guid { get; set; }

        public DTOCompactGuid() : base()
        {
        }

        public DTOCompactGuid(Guid oGuid) : base()
        {
            Guid = oGuid;
        }
    }
}
