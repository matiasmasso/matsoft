using System;

namespace DTO
{
    public class DTOCliProductDto : DTOBaseGuid
    {
        public DTOCustomer Customer { get; set; }
        public DTOProduct Product { get; set; }
        public decimal Dto { get; set; }

        public DTOCliProductDto() : base()
        {
        }

        public DTOCliProductDto(Guid oGuid) : base(oGuid)
        {
        }
    }
}
