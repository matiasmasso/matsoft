using System;

namespace DTO
{
    public class DTOWebLogBrowse : DTOBaseGuid
    {
        public DTOBaseGuid Doc { get; set; }
        public DateTime Fch { get; set; }
        public DTOUser User { get; set; }

        public DTOWebLogBrowse() : base()
        {
        }

        public DTOWebLogBrowse(Guid oGuid) : base(oGuid)
        {
        }
    }
}
