using System;

namespace DTO
{
    public class DTOProductChannel : DTOBaseGuid
    {
        public DTOProduct Product { get; set; }
        public DTODistributionChannel DistributionChannel { get; set; }
        public Cods Cod { get; set; }

        public bool Inherited { get; set; }

        public enum Cods
        {
            Inclou,
            Exclou
        }

        public DTOProductChannel() : base()
        {
        }

        public DTOProductChannel(Guid oGuid) : base(oGuid)
        {
        }
    }
}
