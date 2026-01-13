using System;

namespace DTO
{
    public class DTOECITransmCentre : DTOBaseGuid
    {
        public DTOECITransmGroup Parent { get; set; }

        public DTOContact Centre { get; set; }

        public DTOECITransmCentre() : base()
        {
        }

        public DTOECITransmCentre(Guid oGuid) : base(oGuid)
        {
        }
    }
}
