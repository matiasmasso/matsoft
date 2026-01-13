using System;

namespace DTO
{
    public class DTOProjecte : DTOBaseGuid
    {
        public string nom { get; set; }
        public string dsc { get; set; }
        public DateTime fchFrom { get; set; }
        public DateTime fchTo { get; set; }

        public DTOAmt amt { get; set; }

        public DTOProjecte() : base()
        {
        }

        public DTOProjecte(Guid oGuid) : base(oGuid)
        {
        }
    }
}
