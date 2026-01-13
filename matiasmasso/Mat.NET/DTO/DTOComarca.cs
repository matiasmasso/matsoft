using System;

namespace DTO
{
    public class DTOComarca : DTOArea
    {
        public DTOZona Zona { get; set; }

        public DTOComarca() : base()
        {
            base.Cod = Cods.Comarca;
        }

        public DTOComarca(Guid oGuid) : base(oGuid)
        {
            base.Cod = Cods.Comarca;
        }
    }
}
